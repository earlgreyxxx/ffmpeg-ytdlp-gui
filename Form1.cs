﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ffmpeg_ytdlp_gui.libs;
using ffmpeg_ytdlp_gui.Properties;

namespace ffmpeg_ytdlp_gui
{
  using CodecListItems = List<ListItem<Codec>>;
  using StringListItem = ListItem<string>;
  using StringListItems = List<ListItem<string>>;
  using FFmpegBatchList = Dictionary<ffmpeg_command, IEnumerable<string>>;
  using YtdlpItem = Tuple<string, MediaInformation, System.Drawing.Image>;
  using YtdlpItems = List<Tuple<string, MediaInformation, System.Drawing.Image>>;

  public partial class Form1 : Form
  {
    private const int MemoryLength = 20;

    private Dictionary<string, StringListItems> PresetList;
    private Dictionary<string, CodecListItems> HardwareDecoders;
    private StringListItems DeInterlacesCuvid;
    private StringListItems InputFileList;
    private StringListItems OutputDirectoryList;
    private Size HelpFormSize = new(0, 0);
    private FFmpegBatchList BatchList;
    private PictureBoxSizeMode SizeMode;

    [GeneratedRegex(@"\.(?:mp4|mpg|avi|mkv|webm|m4v|wmv|ts|m2ts)$", RegexOptions.IgnoreCase, "ja-JP")]
    private static partial Regex RegexMovieFile();

    [GeneratedRegex("^(Intel|Nvidia)", RegexOptions.IgnoreCase, "ja-JP")]
    private static partial Regex IsIntelOrNvidia();

    public Form1()
    {
      InitializeComponent();
      InitializeSettingsBinding();
      InitializeDataSource();
      InitializeYtdlpQueue();

      ChangeCurrentDirectory();
    }

    private void ThumbnailBox_ChangeUICues(object sender, UICuesEventArgs e)
    {
      throw new NotImplementedException();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      SizeMode = ThumbnailBox.SizeMode;

      ActiveControl = ffmpeg;
      var folders = Settings.Default.outputFolders;
      if (folders != null && folders.Count > 0)
      {
        foreach (string info in folders)
        {
          var items = info.Split(['|']);
          if (items.Length == 2)
          {
            var item = new StringListItem(items[0], DateTime.Parse(items[1]));
            DirectoryListBindingSource.Add(item);
          }
        }
        cbOutputDir.SelectedIndex = 0;
      }

      if (Settings.Default.ffmpeg?.Count > 0)
      {
        foreach (string item in Settings.Default.ffmpeg)
          ffmpeg.Items.Add(item);

        ffmpeg.SelectedIndex = 0;
      }

      if (Settings.Default.downloadFileNames?.Count > 0)
      {
        foreach (string item in Settings.Default.downloadFileNames)
          OutputFileFormatBindingSource.Add(item);
      }
      else
      {
        OutputFileFormatBindingSource.Add("%(title)s-%(id)s.%(ext)s");
      }

      var radio = (ResizeBox.Controls[$"rbResize{Settings.Default.resize}"] as RadioButton) ?? rbResizeNone;
      radio.Checked = true;

      vUnit.Text = chkConstantQuality.Checked ? "" : "Kbps";

      vBitrate.Value = chkConstantQuality.Checked && Settings.Default.bitrate > 100 ? 25 : Settings.Default.bitrate;

      aBitrate.Enabled = chkEncodeAudio.Checked;
      OutputStderr.Text = "";

      FileName.SelectedIndex = 0;

      chkCrop_CheckedChanged(null, null);
      cbDevices_SelectedIndexChanged(null, null);

      HelpFormSize.Width = Settings.Default.HelpWidth;
      HelpFormSize.Height = Settings.Default.HelpHeight;

      FilePrefix.Text = FileSuffix.Text = string.Empty;

      UseCookie.SelectedIndex = 0;
      VideoFrameRate.SelectedIndex = 0;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (Proceeding != null)
      {
        e.Cancel = true;
        return;
      }
      Settings.Default.outputFolders = null;
      Settings.Default.ffmpeg = null;
      Settings.Default.HelpHeight = HelpFormSize.Height;
      Settings.Default.HelpWidth = HelpFormSize.Width;
      Settings.Default.bitrate = vBitrate.Value;

      var items =
        OutputDirectoryList
          .Where(item => Directory.Exists(item.Value))
          .OrderByDescending(item => (DateTime)item.Data)
          .Take(MemoryLength)
          .OrderBy(item => item.Value)
          .Select(item => $"{item.Value}|{((DateTime)item.Data).ToString()}");

      Settings.Default.outputFolders = [.. items];

      // ffmpegパス
      Settings.Default.ffmpeg = [.. ffmpeg.Items.Cast<string>().Where(item => !string.IsNullOrEmpty(item))];

      // ダウンロードURL
      //var urls = UrlBindingSource.DataSource as StringListItems;
      //var urllist = new StringCollection();
      //urllist.AddRange(urls.TakeLast(MemoryLength).Reverse().Select(item => $"{item.Value}｜{item.Label}").ToArray());
      //Settings.Default.downloadUrls = urllist;

      var radio = GetCheckedRadioButton(ResizeBox);
      Settings.Default.resize = radio.Name.Substring(8);

      // 出力ファイル名形式
      var formats = OutputFileFormatBindingSource.DataSource as List<string>;
      Settings.Default.downloadFileNames = [.. formats.TakeLast(MemoryLength).Reverse()];

      Settings.Default.Save();
    }

    private void btnClearDirs_Click(object sender, EventArgs e)
    {
      ffmpeg.Items.Clear();
      DirectoryListBindingSource.Clear();
      Settings.Default.Reset();
    }

    private void btnSubmitInvoke_Click(object sender, EventArgs e)
    {
      try
      {
        CheckDirectory(cbOutputDir.Text);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "エラー");
        return;
      }

      if (FileListBindingSource.Count > 0)
      {
        btnSubmitInvoke.Enabled = false;
        var command = CreateCommand(chkAudioOnly.Checked);
        if (FileName.Text.Trim() != "元ファイル名")
          command.OutputBaseName(FileName.Text);

        OnBeginProcess();
        var process = CreateFFmpegProcess(command);
        if (process == null)
          return;

        process.PreProcess += RuntimeSetting;
        process.Begin();
      }
    }

    private void btnSubmitOpenDlg_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(cbOutputDir.Text) && Directory.Exists(cbOutputDir.Text))
        FindFolder.SelectedPath = cbOutputDir.Text;

      if (DialogResult.Cancel == FindFolder.ShowDialog())
        return;

      if (OutputDirectoryList.Any(item => item.Value == FindFolder.SelectedPath))
        cbOutputDir.SelectedValue = FindFolder.SelectedPath;
      else
        cbOutputDir.SelectedIndex = DirectoryListBindingSource.Add(new StringListItem(FindFolder.SelectedPath, DateTime.Now));
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      var ffcommand = CreateCommand(chkAudioOnly.Checked);
      string sampleName = "sample.mp4";

      RuntimeSetting(ffcommand, sampleName);

      if (!string.IsNullOrEmpty(cbOutputDir.Text) && cbOutputDir.SelectedIndex < 0 && !OutputDirectoryList.Any(item => item.Value == cbOutputDir.Text))
        cbOutputDir.SelectedIndex = DirectoryListBindingSource.Add(new StringListItem(cbOutputDir.Text, cbOutputDir.Text, DateTime.Now));

      Commandlines.Text = ffcommand.GetCommandLine(sampleName);
    }

    private void chkConstantQuality_CheckedChanged(object sender, EventArgs e)
    {
      vBitrate.Increment = chkConstantQuality.Checked ? 1 : 100;
      vBitrate.Minimum = chkConstantQuality.Checked ? 0 : 100;
      vBitrate.Maximum = chkConstantQuality.Checked ? 100 : 1000000;
      vBitrate.Value = chkConstantQuality.Checked ? 25 : 6000;

      vUnit.Text = chkConstantQuality.Checked ? "" : "Kbps";

      if (chkConstantQuality.Checked)
      {
        var codec = UseVideoEncoder.SelectedValue as Codec;
        vQualityLabel.Text = codec.GpuSuffix == "qsv" ? "ICQ" : "CQ";
      }
      else
      {
        vQualityLabel.Text = "-b:v";
      }
    }

    private void btnClearSS_Click(object sender, EventArgs e)
    {
      txtSS.Text = string.Empty;
    }

    private void btnClearTo_Click(object sender, EventArgs e)
    {
      txtTo.Text = string.Empty;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      Commandlines.Clear();
    }

    private void chkEncodeAudio_CheckedChanged(object sender, EventArgs e)
    {
      if (chkAudioOnly.Checked)
        return;

      UseAudioEncoder.Enabled = chkEncodeAudio.Checked;
      aBitrate.Enabled = chkEncodeAudio.Checked;
    }

    private void chkAudioOnly_CheckedChanged(object sender, EventArgs e)
    {
      bool isChecked = chkAudioOnly.Checked;
      VideoFrameRate.Enabled = LookAhead.Enabled = UseVideoEncoder.Enabled = cbPreset.Enabled = vBitrate.Enabled = chkConstantQuality.Enabled = !isChecked;
      chkFilterDeInterlace.Enabled = chkUseHWDecoder.Enabled = !isChecked;
      CropBox.Enabled = ResizeBox.Enabled = RotateBox.Enabled = !isChecked;
      if (!chkUseHWDecoder.Enabled)
        chkUseHWDecoder.Checked = false;

      if (isChecked)
      {
        cbDeinterlaceAlg.Enabled = false;
      }
      else
      {
        if (chkFilterDeInterlace.Checked)
          cbDeinterlaceAlg.Enabled = true;
        else
          cbDeinterlaceAlg.Enabled = false;
      }

      if (isChecked && !chkEncodeAudio.Checked)
      {
        chkEncodeAudio.Checked = true;
        aBitrate.Enabled = true;
      }

      if (isChecked && !UseAudioEncoder.Enabled)
        UseAudioEncoder.Enabled = true;
      else if (!isChecked && !chkEncodeAudio.Checked)
        UseAudioEncoder.Enabled = false;

      if (!isChecked)
        UseVideoEncoder_SelectedIndexChanged(null, null);
    }

    private void DropArea_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        return;

      string[] dragFilePathArr = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      foreach (var filePath in dragFilePathArr)
      {
        if (IsFile(filePath))
          FileListBindingSource.Add(new StringListItem(filePath));
        else if (IsDirectory(filePath))
          Directory
            .GetFiles(filePath)
            .Where(f => RegexMovieFile().IsMatch(f))
            .ToList()
            .ForEach(f => FileListBindingSource.Add(new StringListItem(f)));
      }

      FileListBindingSource.ResetBindings(false);
      btnSubmitInvoke.Enabled = true;
    }

    private void DropArea_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.Copy;
    }

    private void OpenFolder_Click(object sender, EventArgs e)
    {
      string path = cbOutputDir.Text;
      if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
      {
        MessageBox.Show("フォルダが存在しません。", "エラー");
        return;
      }

      CustomProcess.ShellExecute(cbOutputDir.Text);
    }

    private void DropArea_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (DialogResult.Cancel == OpenInputFile.ShowDialog())
        return;

      foreach (var filename in OpenInputFile.FileNames)
        FileListBindingSource.Add(new StringListItem(filename));

      btnSubmitInvoke.Enabled = true;
    }

    private void btnSubmitSaveToFile_Click(object sender, EventArgs e)
    {
      if (DialogResult.Cancel == FindSaveBatchFile.ShowDialog())
        return;

      string filename = FindSaveBatchFile.FileName;
      if (File.Exists(filename) && DialogResult.No == MessageBox.Show("ファイルを上書きしてもよろしいですか？", "警告", MessageBoxButtons.YesNo))
        return;

      using (var sw = new StreamWriter(filename, false, Encoding.GetEncoding(932)))
      {
        sw.WriteLine(ffmpeg_command.CreateBatch(BatchList, RuntimeSetting));
      }

      BatchList.Clear();
      BatchList = null;
      btnSubmitBatchClear.Enabled = btnSubmitSaveToFile.Enabled = false;
    }

    private void btnSubmitAddToFile_Click(object sender, EventArgs e)
    {
      if (BatchList == null)
      {
        BatchList = new FFmpegBatchList();
        btnSubmitBatchClear.Enabled = btnSubmitSaveToFile.Enabled = true;
      }

      try
      {
        CheckDirectory(cbOutputDir.Text);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "エラー");
        return;
      }

      AddDirectoryListItem();

      if (FileListBindingSource.Count > 0)
      {
        var command = CreateCommand(chkAudioOnly.Checked);
        command.Overwrite = Overwrite.Checked;

        if (FileName.Text.Trim() != "元ファイル名")
          command.OutputBaseName(FileName.Text);

        BatchList.Add(
          command,
          FileListBindingSource.OfType<StringListItem>().Select(item => item.Value).ToList()
        );

        FileListBindingSource.Clear();
      }
    }

    private void btnSubmitBatchClear_Click(object sender, EventArgs e)
    {
      BatchList.Clear();
      BatchList = null;
      btnSubmitBatchClear.Enabled = btnSubmitSaveToFile.Enabled = false;
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
      StopCurrentProcess();
    }

    private void btnStopAll_Click(object sender, EventArgs e)
    {
      StopAllProcess();
    }

    private void UseVideoEncoder_SelectedIndexChanged(object sender, EventArgs e)
    {
      var codec = UseVideoEncoder.SelectedValue as Codec;
      // copyの場合は、動画品質指定はすべてdisabledにする。
      bool isCopy = codec.Name == "copy";
      bool isCpu = codec.GpuSuffix == "cpu";

      CropBox.Enabled = ResizeBox.Enabled = RotateBox.Enabled = !isCopy;
      cbPreset.Enabled = chkConstantQuality.Enabled = vBitrate.Enabled = !isCopy;
      LookAhead.Enabled = chkUseHWDecoder.Enabled = OpenEncoderHelp.Enabled = !isCopy;
      chkUseHWDecoder.Enabled = !isCpu;
      DeInterlaceBox.Enabled = OthersBox.Enabled = !isCopy;
      VideoFrameRate.Enabled = !isCopy;

      InitPresetAndDevice(codec);

      if (FileContainer.Items.Count > 0)
      {
        if (codec.Name == "gif")
          FileContainer.SelectedValue = ".gif";
        else
          FileContainer.SelectedIndex = 0;
      }
    }

    private void btnFFmpeg_Click(object sender, EventArgs e)
    {
      if (DialogResult.Cancel == OpenFFMpegFileDlg.ShowDialog())
        return;

      ffmpeg.Text = OpenFFMpegFileDlg.FileName;
      if (!ffmpeg.Items.Contains(ffmpeg.Text))
        ffmpeg.Items.Add(ffmpeg.Text);
    }

    private void btnFindInPath_Click(object sender, EventArgs e)
    {
      foreach (var path in ffmpeg.Items.Cast<string>().Where(item => item != "ffmpeg" && !System.IO.File.Exists(item)))
        ffmpeg.Items.Remove(path);

      string[] ffmpegPathes = CustomProcess.FindInPath("ffmpeg");
      if (ffmpegPathes.Length == 0)
      {
        if (DialogResult.Yes == MessageBox.Show("環境変数PATHからffmpegコマンドが見つかりませんでした。\nWingetコマンドを利用してffmpegをインストールしますか？", "警告", MessageBoxButtons.YesNo))
          Process.Start("winget", "install -id Gyan.FFmpeg");

        ffmpeg.Text = string.Empty;
      }
      else
      {
        foreach (var ffmpegPath in ffmpegPathes.Reverse())
        {
          if (!ffmpeg.Items.Contains(ffmpegPath))
            ffmpeg.Items.Insert(0, ffmpegPath);
        }

        ffmpeg.SelectedIndex = 0;
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      CustomProcess.ShellExecute("https://ffmpeg.org/ffmpeg-utils.html#time-duration-syntax");
    }

    private void rbResizeNum_CheckedChanged(object sender, EventArgs e)
    {
      resizeTo.Enabled = rbResizeNum.Checked;
    }

    private void OpenLogFile_Click(object sender, EventArgs e)
    {
      var filename = ffmpeg_process.GetLogFileName();
      if (!System.IO.File.Exists(filename))
      {
        MessageBox.Show("ログファイルが存在しません。");
        return;
      }
      CustomProcess.ShellExecute(filename);
    }

    private void ClearFileList_Click(object sender, EventArgs e)
    {
      FileListBindingSource.Clear();
    }

    private void chkFilterDeInterlace_CheckedChanged(object sender, EventArgs e)
    {
      cbDeinterlaceAlg.Enabled = chkFilterDeInterlace.Checked;
    }

    private void chkCrop_CheckedChanged(object sender, EventArgs e)
    {
      bool bChecked = chkCrop.Checked;
      foreach (var control in CropBox.Controls.OfType<NumericUpDown>())
        control.Enabled = bChecked;

      if (bChecked)
        CropWidth.Focus();
    }

    private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
    {
      var m = IsIntelOrNvidia().Match(cbDevices.Text);
      string key = m.Success ? m.Groups[1].Value.ToLower() : "cpu";

      var decodersItems = HardwareDecoders[key];

      HWDecoder.DataSource = decodersItems;
      DecoderHelpList.DataSource = decodersItems.Select(decoder => decoder.Clone()).ToList();

      chkUseHWDecoder.Enabled = key != "cpu";
      if (!chkUseHWDecoder.Enabled)
        chkUseHWDecoder.Checked = false;
    }

    private void chkUseHWDecoder_CheckedChanged(object sender, EventArgs e)
    {
      var codec = HWDecoder.SelectedValue as Codec;
      HWDecoder.Enabled = chkUseHWDecoder.Checked;

      if (chkUseHWDecoder.Checked)
      {
        foreach (var algo in DeInterlacesCuvid)
          DeInterlaceListBindingSource.Add(algo);
      }
      else
      {
        foreach (var algo in DeInterlacesCuvid)
          DeInterlaceListBindingSource.Remove(algo);
      }
      DeInterlaceListBindingSource.ResetBindings(false);
    }

    private void OpenEncoderHelp_Click(object sender, EventArgs e)
    {
      var encoder = EncoderHelpList.SelectedValue.ToString();
      if (encoder == "copy")
        return;

      OpenOutputView(string.IsNullOrEmpty(ffmpeg.Text) ? "ffmpeg.exe" : ffmpeg.Text, $"-hide_banner -h encoder={encoder}");
    }

    private void OpenDecoderHelp_Click(object sender, EventArgs e)
    {
      var decoder = DecoderHelpList.SelectedValue.ToString();
      OpenOutputView(string.IsNullOrEmpty(ffmpeg.Text) ? "ffmpeg" : ffmpeg.Text, $"-hide_banner -h decoder={decoder}");
    }

    private void SubmitCopy_Click(object sender, EventArgs e)
    {
      try
      {
        CheckDirectory(cbOutputDir.Text);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "エラー");
        return;
      }

      if (FileListBindingSource.Count > 0)
      {
        btnSubmitInvoke.Enabled = false;
        var command = new ffmpeg_command(string.IsNullOrEmpty(ffmpeg.Text) ? "ffmpeg" : ffmpeg.Text);
        command
          .vcodec("copy")
          .acodec("copy")
          .OutputDirectory(cbOutputDir.Text)
          .OutputPrefix(FilePrefix.Text)
          .OutputSuffix(FileSuffix.Text)
          .OutputContainer(FileContainer.SelectedValue.ToString());

        if (FileName.Text.Trim() != "元ファイル名")
          command.OutputBaseName(FileName.Text);

        command.MultiFileProcess = InputFileList.Count > 1;

        OnBeginProcess();
        CreateFFmpegProcess(command)?.Begin();
      }
    }

    private void SubmitConcat_Click(object sender, EventArgs e)
    {
      string listfile = null;
      try
      {
        if (InputFileList.Count <= 1)
          throw new Exception("二つ以上の入力ファイルが必要です。");

        if (FileName.Text.Trim() == "元ファイル名")
          throw new Exception("元ファイル名は使用できません、出力ファイル名を指定してください。");

        listfile = Path.Combine(
          Environment.ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("TEMP")),
          $"ffmpeg-command-builder-{Process.GetCurrentProcess().Id}.txt"
        );

        using (var sw = new StreamWriter(listfile))
        {
          foreach (var item in InputFileList)
          {
            string filename = item.Value.Replace("\\", "\\\\");
            sw.WriteLine($"file '{filename}'");
          }
        }

        var command = new ffmpeg_command(string.IsNullOrEmpty(ffmpeg.Text) ? "ffmpeg" : ffmpeg.Text);

        command
          .setPreOptions("-f concat,-safe 0")
          .vcodec("copy")
          .acodec("copy")
          .OutputDirectory(cbOutputDir.Text)
          .OutputPrefix(FilePrefix.Text)
          .OutputBaseName(FileName.Text)
          .OutputSuffix(FileSuffix.Text)
          .OutputContainer(FileContainer.SelectedValue.ToString());

        OnBeginProcess();
        CreateFFmpegProcess(command)?.One(listfile);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "警告");
      }
      finally
      {
        if (string.IsNullOrEmpty(listfile) && File.Exists(listfile))
          File.Delete(listfile);
      }
    }

    private void SubmitThumbnail_Click(object sender, EventArgs e)
    {
      try
      {
        if (string.IsNullOrEmpty(cbOutputDir.Text))
          throw new Exception("出力ディレクトリを指定してください。");

        var re = new Regex(@"%\d*d");
        if (!useTiledImage.Checked && !re.IsMatch(FileName.Text) && !re.IsMatch(FilePrefix.Text) && !re.IsMatch(FileSuffix.Text))
          throw new Exception("画像出力の際は、%d などの連番号フォーマットが含まれている必要があります。");

        if (InputFileList.Count < 1)
          throw new Exception("一つ以上の入力ファイルが必要です。");

        var imagetype = ImageType.SelectedItem as StringListItem;
        string extension = imagetype.Data.ToString();
        string codec = imagetype.Value;

        var command = new ffmpeg_command(string.IsNullOrEmpty(ffmpeg.Text) ? "ffmpeg" : ffmpeg.Text);

        if (!Directory.Exists(cbOutputDir.Text))
          Directory.CreateDirectory(cbOutputDir.Text);

        command
          .Starts(ImageSS.Text)
          .To(ImageTo.Text)
          .vcodec(codec)
          .acodec(null)
          .OutputDirectory(cbOutputDir.Text)
          .OutputPrefix(FilePrefix.Text)
          .OutputSuffix(FileSuffix.Text)
          .OutputContainer(extension);

        List<string> additionals = null; ;
        if (ImageFreeOptions.Text.Trim().Length > 0)
        {
          additionals = ImageFreeOptions.Text.Trim().Split(
            new char[] { ',', ';', ':' },
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
          ).ToList();
        }

        Debug.WriteLine(command.GetCommandLine("sample.mp4"));

        if (FileName.Text.Trim() != "元ファイル名")
          command.OutputBaseName(FileName.Text);

        command.MultiFileProcess = InputFileList.Count > 1;

        OnBeginProcess();
        var process = CreateFFmpegProcess(command);
        if (process == null)
          return;

        process.PreProcess += (command, filename) =>
        {
          command.clearOptions();
          if (additionals != null && additionals.Count > 0)
            command.setOptions(additionals);

          List<string> list = ["-vsync vfr"];
          List<string> vfilter = [];
          if (FrameRate.Value > 0)
            vfilter.Add($"fps=fps=1/{FrameRate.Value}:round=up");

          if (codec == "mjpeg")
            list.Add("-q:v 5");

          var ffprobe = ffprobe_process.CreateInstance(filename);
          var container = ffprobe.getContainerProperty();
          var stream = ffprobe.getStreamProperties().FirstOrDefault(stream => stream.codec_type == "video");
          if (stream == null)
            return;

          decimal? aspect = stream.width / stream.height;

          if (CropTB.Value > 0 && CropLR.Value > 0)
            vfilter.Add($"crop=iw-{CropLR.Value * 2}:ih-{CropTB.Value * 2}:{CropLR.Value}:{CropTB.Value}");
          else if (CropTB.Value <= 0 && CropLR.Value > 0)
            vfilter.Add($"crop=iw-{CropLR.Value * 2}:ih:{CropLR.Value}:0");
          else if (CropTB.Value > 0 && CropLR.Value <= 0)
            vfilter.Add($"crop=iw:ih-{CropTB.Value * 2}:0:{CropTB.Value}");

          decimal w = ImageWidth.Value;
          decimal h = ImageHeight.Value;

          if (w > 0 && h > 0)
            vfilter.Add($"scale={w}:{h}");
          else if (w > 0 && h <= 0)
            vfilter.Add($"scale={w}:-1");
          else if (w <= 0 && h > 0)
            vfilter.Add($"scale=-1:{h}");

          decimal duration = container.duration ?? 0m;
          decimal ss = MediaProperty.ConvertDuration(ImageSS.Text) ?? 0;
          decimal to = MediaProperty.ConvertDuration(ImageTo.Text) ?? 0;
          if (ss > to)
            ss = 0;
          if (ss == to)
            ss = to = 0;

          if (ss > 0 && to > 0)
            duration = to - ss;
          else if (ss > 0 && to <= 0)
            duration -= ss;
          else if (ss <= 0 && to > 0)
            duration = to;

          decimal per = FrameRate.Value;
          decimal frames = Math.Ceiling(duration / per);

          decimal col = TileColumns.Value;
          decimal row = TileRows.Value;
          if (useTiledImage.Checked)
          {
            if (col > 0 || row > 0)
            {
              if (col > 0 && row <= 0)
                row = Math.Ceiling(frames / col);
              else if (col <= 0 && row > 0)
                col = Math.Ceiling(frames / row);
            }
            else
            {
              col = 6;
              row = 5;
            }
            vfilter.Add($"tile={col}x{row}");
          }

          if (vfilter.Count > 0)
            list.Add($"-vf \"{string.Join(',', vfilter.ToArray())}\"");

          command.setOptions(list);
        };
        process.Begin();
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message, "警告");
      }
    }

    private void CommandInvoker_Click(object sender, EventArgs e)
    {
      OpenOutputView("git.exe", "help -a", "git help");
    }

    private void DownloadUrl_Leave(object sender, EventArgs e)
    {
      if (DownloadUrl.Text.Length > 0 && !Regex.IsMatch(DownloadUrl.Text, "^https?://"))
      {
        var tooltip = new ToolTip();
        tooltip.SetToolTip(DownloadUrl, "フォーマットエラー");
        tooltip.AutomaticDelay = 10;
        tooltip.Show("正しいURLを入力してください。", DownloadUrl);
        DownloadUrl.Focus();
        Task.Delay(5000).ContinueWith(_ => Invoke(() => tooltip.Hide(DownloadUrl)));
      }
    }

    private void SubmitDownload_Click(object sender, EventArgs e)
    {
      var button = (Button)sender;
      var ytdlpItem = DownloadUrl.SelectedItem as YtdlpItem;

      var format = OutputFileFormat.Text;
      var list = OutputFileFormatBindingSource.DataSource as List<string>;
      if (false == list.Any(item => item == format))
        OutputFileFormat.SelectedIndex = OutputFileFormatBindingSource.Add(format);

      if (button.Tag.GetType().Name != "Boolean")
        throw new Exception("not boolean type");

      var isSeparate = (bool)button.Tag;

      YtdlpInvokeDownload(ytdlpItem, isSeparate);
    }

    private async void SubmitConfirmFormat_Click(object sender, EventArgs e)
    {
      var url = DownloadUrl.Text;
      var ytdlpItem = await YtdlpParseDownloadUrl(url);

      if (ytdlpItem == null)
        return;

      var list = UrlBindingSource.DataSource as YtdlpItems;
      if (false == list.Any(item => item.Item1 == url))
      {
        DownloadUrl.SelectedIndex = UrlBindingSource.Add(ytdlpItem);
        DownloadUrl_SelectedIndexChanged(DownloadUrl, new EventArgs());
      }
    }

    private void DownloadUrl_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DownloadUrl.SelectedIndex < 0)
        return;

      var ytdlpItem = DownloadUrl.SelectedItem as YtdlpItem;
      var mi = ytdlpItem.Item2;
      var image = ytdlpItem.Item3;

      string time = mi.GetDurationTime();

      ThumbnailBox.ContextMenuStrip = ImageContextMenu;
      ThumbnailBox.Image = ytdlpItem.Item3;
      DurationTime.Text = time;
      DurationTime.Visible = true;

      //DownloadUrl.Enabled = false;
      MediaTitle.Text = mi.title;

      // format_id 構築
      VideoOnlyFormatSource.Clear();
      VideoOnlyFormatSource.Add(new StringListItem(string.Empty, "使用しない"));
      AudioOnlyFormatSource.Clear();
      AudioOnlyFormatSource.Add(new StringListItem(string.Empty, "使用しない"));
      MovieFormatSource.Clear();

      foreach (var format in mi.formats)
      {
        if (format.vcodec == "none" && format.acodec != "none")
          AudioOnlyFormatSource.Add(new StringListItem(format.format_id, format.ToString()));
        else if (format.acodec == "none" && format.vcodec != "none")
          VideoOnlyFormatSource.Add(new StringListItem(format.format_id, format.ToString()));
        else if (format.acodec != "none" && format.vcodec != "none")
          MovieFormatSource.Add(new StringListItem(format.format_id, format.ToString()));
      }

      MovieFormat.SelectedIndex = MovieFormatSource.Count - 1;

      // requested_formats? があれば
      if (mi.requested_formats.Count > 0)
      {
        var items = mi.requested_formats.Select(f => new
        {
          Value = f.format_id,
          Label = f.ToString(),
          Video = f.acodec == "none",
          Audio = f.vcodec == "none",
        });

        foreach (var item in items)
        {
          ComboBox cb = null;
          if (item.Video)
            cb = VideoOnlyFormat;
          else if (item.Audio)
            cb = AudioOnlyFormat;

          cb.SelectedValue = item.Value;
        }
      }

      SubmitDownload.Enabled = true;
      SubmitSeparatedDownload.Enabled = true;
    }

    private void Tab_SelectedIndexChanged(object sender, EventArgs e)
    {
      bool IsNotDownloader = Tab.TabPages[Tab.SelectedIndex].Name != "PageDownloader";
      InputBox.Enabled = IsNotDownloader;
      FilePrefix.Enabled = FileSuffix.Enabled = FileName.Enabled = FileContainer.Enabled = IsNotDownloader;
    }

    private void LinkYdlOutputTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      CustomProcess.ShellExecute("https://github.com/yt-dlp/yt-dlp/#output-template");
    }

    private void UseCookie_SelectedIndexChanged(object sender, EventArgs e)
    {
      var value = UseCookie.SelectedValue.ToString();
      CookieAttn.Visible = value != "none" && value != "file" && value != "firefox";
    }

    private void SubmitOpenCookie_Click(object sender, EventArgs e)
    {
      if (DialogResult.Cancel == OpenCookieFileDialog.ShowDialog())
        return;

      CookiePath.Text = OpenCookieFileDialog.FileName;
    }

    private void StopDownload_Click(object sender, EventArgs e)
    {
      YtdlpAbortDownload();
    }

    private void CookieAttn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      CustomProcess.ShellExecute("https://github.com/yt-dlp/yt-dlp/issues/7271");
    }

    private void CommandSaveImage_Click(object sender, EventArgs e)
    {
      var item = DownloadUrl.SelectedItem as YtdlpItem;
      if (item == null)
        return;

      var mi = item.Item2;
      var image = ThumbnailBox.Image;

      if (item.Item3 != image)
        return;
      
      if (image != null)
      {
        var modal = new SaveFileDialog()
        {
          DefaultExt = "jpg",
          InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
          FileName = $"{mi.title}-thumbnail",
          Filter = "JPEGファイル|*.jpg|PNGファイル|*.png",
          OverwritePrompt = true,
          Title = "名前を付けて画像を保存",
        };

        if (DialogResult.OK == modal.ShowDialog())
          image.Save(modal.FileName);
      }
    }

    private void useTiledImage_CheckedChanged(object sender, EventArgs e)
    {
      TileColumns.Enabled = TileRows.Enabled = useTiledImage.Checked;
    }
  }
}
