using ffmpeg_ytdlp_gui.libs;
using ffmpeg_ytdlp_gui.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ffmpeg_ytdlp_gui
{
  public partial class Form1 : Form
  {
    [GeneratedRegex(@"^(?:\d{2}:)?\d{2}:\d{2}(?:\.\d+)?$")]
    private static partial Regex IsDateTime();

    [GeneratedRegex(@"^\d+(?:\.\d+)?(?:s|ms|us)?$", RegexOptions.IgnoreCase, "ja-JP")]
    private static partial Regex IsSecondTime();

    private const int MemoryLength = 20;

    private Dictionary<string, StringListItems>? PresetList;
    private Dictionary<string, CodecListItems>? HardwareDecoders;
    private StringListItems? DeInterlacesCuvid;
    private StringListItems? InputFileList;
    private StringListItems? OutputDirectoryList;
    private Size StdoutFormSize = new(0, 0);
    private FFmpegBatchList? BatchList;
    private PictureBoxSizeMode? SizeMode;

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
      InitializeToastNotify();

      ChangeCurrentDirectory();
#if DEBUG
      CommandInvoker.Enabled = true;
      CommandInvoker.Visible = true;
#endif
    }

    /// <summary>
    /// 出力フォームのサイズを設定
    /// </summary>
    private void StdoutFormLoadAction(Object? sender, EventArgs? e)
    {
      var form = sender as Form ?? throw new Exception("Form not initialized");

      if (StdoutFormSize.Width > 0 && StdoutFormSize.Height > 0)
        form.Size = StdoutFormSize;
    }

    /// <summary>
    /// 出力フォームのサイズを保存
    /// </summary>
    private readonly object _locking = new();
    private void StdoutFormClosingAction(Object? sender, EventArgs? e)
    {
      var form = sender as Form ?? throw new Exception("Form not initialized");

      lock (_locking)
      {
        StdoutFormSize = form.Size;
      }
    }

    private void ThumbnailBox_ChangeUICues(object sender, UICuesEventArgs e)
    {
      throw new NotImplementedException();
    }

    private void InitOutputFolder(StringCollection? folders)
    {
      if (folders != null && folders.Count > 0)
      {
        foreach (string? info in folders)
        {
          var items = info!.Split(['|']);
          if (items!.Length == 2)
          {
            var item = new StringListItem(items[0], DateTime.Parse(items[1]));
            DirectoryListBindingSource.Add(item);
          }
        }
      }
    }

    private IEnumerable<string> GetOutputFolders(StringListItems items)
    {
      return items.Where(item => Directory.Exists(item.Value))
                  .OrderByDescending(item => (DateTime?)item.Data)
                  .Take(MemoryLength)
                  .OrderBy(item => item.Value)
                  .Select(item => $"{item.Value}|{((DateTime?)item.Data).ToString()}");
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      SizeMode = ThumbnailBox.SizeMode;
      ActiveControl = ffmpeg;

      // 元の値を退避
      string backup = DirectoryListBindingSource.DataMember;
      DirectoryListBindingSource.DataMember = "Item1";
      InitOutputFolder(Settings.Default.outputFolders);

      DirectoryListBindingSource.DataMember = "Item2";
      InitOutputFolder(Settings.Default.downloadFolders);

      // 元に戻す 
      DirectoryListBindingSource.DataMember = backup;
      if (cbOutputDir.Items.Count > 0)
        cbOutputDir.SelectedIndex = 0;

      if (Settings.Default.ffmpeg?.Count > 0)
      {
        foreach (string? item in Settings.Default.ffmpeg)
          ffmpeg.Items.Add(item!);

        ffmpeg.SelectedIndex = 0;
      }

      if (Settings.Default.ytdlp?.Count > 0)
      {
        foreach (string? item in Settings.Default.ytdlp)
          YtdlpPath.Items.Add(item!);

        YtdlpPath.SelectedIndex = 0;
      }

      if (Settings.Default.downloadFileNames?.Count > 0)
      {
        foreach (string? item in Settings.Default.downloadFileNames)
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

      chkCrop_CheckedChanged(this, new EventArgs());
      cbDevices_SelectedIndexChanged(this, new EventArgs());

      StdoutFormSize.Width = Settings.Default.OutputWindowWidth;
      StdoutFormSize.Height = Settings.Default.OutputWindowHeight;

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
      Settings.Default.OutputWindowHeight = StdoutFormSize.Height;
      Settings.Default.OutputWindowWidth = StdoutFormSize.Width;
      Settings.Default.bitrate = vBitrate.Value;

      var set = DirectoryListBindingSource.DataSource as StringListItemsSet ?? throw new Exception("DataSource not initialized yet");

      Settings.Default.outputFolders = [.. GetOutputFolders(set.Item1)];
      Settings.Default.downloadFolders = [.. GetOutputFolders(set.Item2)];

      // ffmpegパス
      Settings.Default.ffmpeg = [.. ffmpeg.Items.Cast<string>().Where(item => !string.IsNullOrEmpty(item))];
      Settings.Default.ytdlp = [.. YtdlpPath.Items.Cast<string>().Where(item => !string.IsNullOrEmpty(item))];

      // ダウンロードURL
      //var urls = UrlBindingSource.DataSource as StringListItems;
      //var urllist = new StringCollection();
      //urllist.AddRange(urls.TakeLast(MemoryLength).Reverse().Select(item => $"{item.Value}｜{item.Label}").ToArray());
      //Settings.Default.downloadUrls = urllist;

      var radio = GetCheckedRadioButton(ResizeBox);
      Settings.Default.resize = radio!.Name.Substring(8);

      // 出力ファイル名形式
      var formats = OutputFileFormatBindingSource.DataSource as List<string>;
      Settings.Default.downloadFileNames = [.. formats?.TakeLast(MemoryLength).Reverse()];

      Settings.Default.Save();
    }

    private void ClearListItem_Click(object sender, EventArgs e)
    {
      if (DialogResult.Yes == MessageBox.Show("本当に設定を全てリセットしていいですか？", "クリア確認", MessageBoxButtons.YesNo))
      {
        ffmpeg.Items.Clear();
        DirectoryListBindingSource.Clear();
        OutputFileFormatBindingSource.Clear();
        UrlBindingSource.Clear();
        Settings.Default.Reset();

        ToastShow("設定を全てリセットしました。");
      }
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
        var process = CreateFFmpegProcess(command, "PageConvert");
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

      var selectedItem = OutputDirectoryList!.FirstOrDefault(item => item.Value == FindFolder.SelectedPath);
      if (selectedItem != null)
        cbOutputDir.SelectedItem = selectedItem;
      else
        cbOutputDir.SelectedIndex = DirectoryListBindingSource.Add(new StringListItem(FindFolder.SelectedPath, DateTime.Now));
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      var ffcommand = CreateCommand(chkAudioOnly.Checked);
      string sampleName = "sample.mp4";

      RuntimeSetting(ffcommand, sampleName);

      if (!string.IsNullOrEmpty(cbOutputDir.Text) && cbOutputDir.SelectedIndex < 0 && !OutputDirectoryList!.Any(item => item.Value == cbOutputDir.Text))
        cbOutputDir.SelectedIndex = DirectoryListBindingSource.Add(new StringListItem(cbOutputDir.Text, cbOutputDir.Text, DateTime.Now));

      Commandlines.Text = ffcommand.GetCommandLine(sampleName) ?? string.Empty;
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
        var codec = UseVideoEncoder.SelectedValue as Codec ?? throw new NullReferenceException("codec is null");
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
        UseVideoEncoder_SelectedIndexChanged(this, new EventArgs());
    }

    private void DropArea_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data!.GetDataPresent(DataFormats.FileDrop))
        return;

      string[]? dragFilePathArr = (string[]?)e.Data.GetData(DataFormats.FileDrop, false);

      foreach (var filePath in dragFilePathArr!)
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
        sw.WriteLine(ffmpeg_command.CreateBatch(BatchList ?? throw new NullReferenceException("BatchList is null"), RuntimeSetting));
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
      BatchList!.Clear();
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
      var codec = UseVideoEncoder.SelectedValue as Codec ?? throw new NullReferenceException("Codec is null");
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
      OpenCommandFileDlg.Title = "ffmpeg実行ファイルを指定してください。";
      if (DialogResult.Cancel == OpenCommandFileDlg.ShowDialog())
        return;

      ffmpeg.Text = OpenCommandFileDlg.FileName;
      if (!ffmpeg.Items.Contains(ffmpeg.Text))
        ffmpeg.Items.Add(ffmpeg.Text);
    }

    private void btnYtdlp_Click(object sender, EventArgs e)
    {
      OpenCommandFileDlg.Title = "yt-dlp実行ファイルを指定してください。";
      if (DialogResult.Cancel == OpenCommandFileDlg.ShowDialog())
        return;

      YtdlpPath.Text = OpenCommandFileDlg.FileName;
      if (!YtdlpPath.Items.Contains(YtdlpPath.Text))
        YtdlpPath.Items.Add(YtdlpPath.Text);
    }

    private void FindInPath_Click(object sender, EventArgs e)
    {
      var button = sender as Button;
      if (button == null)
        return;

      var command = button.Tag as string;
      if (command == null)
        return;

      foreach (var path in ffmpeg.Items.Cast<string>().Where(item => item != command && !System.IO.File.Exists(item)))
        ffmpeg.Items.Remove(path);

      string[] commandPathes = CustomProcess.FindInPath(command);
      var cb = command == "ffmpeg" ? ffmpeg : YtdlpPath;

      if (commandPathes.Length == 0)
      {
        MessageBox.Show($"環境変数PATHから{command}コマンドが見つかりませんでした。\nWingetコマンドを利用して{command}をインストールしてください。", "警告");
        cb.Text = string.Empty;
      }
      else
      {
        foreach (var path in commandPathes.Reverse())
        {
          if (!cb.Items.Contains(path))
            cb.Items.Insert(0, path);
        }

        cb.SelectedIndex = 0;
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
      var filename = RedirectedProcess.GetTemporaryFileName("ffmpeg-stderr-", ".log");
      if (!System.IO.File.Exists(filename))
      {
        MessageBox.Show("ログファイルが存在しません。");
        return;
      }
      CustomProcess.ShellExecute(filename);
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

      var decodersItems = HardwareDecoders![key];

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
        foreach (var algo in DeInterlacesCuvid!)
          DeInterlaceListBindingSource.Add(algo);
      }
      else
      {
        foreach (var algo in DeInterlacesCuvid!)
          DeInterlaceListBindingSource.Remove(algo);
      }
      DeInterlaceListBindingSource.ResetBindings(false);
    }

    private void OpenEncoderHelp_Click(object sender, EventArgs e)
    {
      var encoder = EncoderHelpList.SelectedValue?.ToString();
      if (encoder == "copy")
        return;

      OpenOutputView(string.IsNullOrEmpty(ffmpeg.Text) ? "ffmpeg.exe" : ffmpeg.Text, $"-hide_banner -h encoder={encoder}");
    }

    private void OpenDecoderHelp_Click(object sender, EventArgs e)
    {
      var decoder = DecoderHelpList.SelectedValue?.ToString();
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
          .OutputContainer(FileContainer.SelectedValue?.ToString()!);

        if (FileName.Text.Trim() != "元ファイル名")
          command.OutputBaseName(FileName.Text);

        command.MultiFileProcess = InputFileList!.Count > 1;

        OnBeginProcess();
        CreateFFmpegProcess(command, "PageUtility")?.Begin();
      }
    }

    private void SubmitConcat_Click(object sender, EventArgs e)
    {
      string? listfile = null;
      try
      {
        if (InputFileList!.Count <= 1)
          throw new Exception("二つ以上の入力ファイルが必要です。");

        if (FileName.Text.Trim() == "元ファイル名")
          throw new Exception("元ファイル名は使用できません、出力ファイル名を指定してください。");

        listfile = Path.Combine(
          Environment.ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("TEMP")!),
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
          .OutputContainer(FileContainer.SelectedValue?.ToString()!);

        OnBeginProcess();
        CreateFFmpegProcess(command, "PageUtility")?.One(listfile);
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

        if (InputFileList!.Count < 1)
          throw new Exception("一つ以上の入力ファイルが必要です。");

        var imagetype = ImageType.SelectedItem as StringListItem ?? throw new NullReferenceException("SelectedItem is null");
        string extension = imagetype.Data?.ToString()!;
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

        List<string>? additionals = null;
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
        var process = CreateFFmpegProcess(command, "PageUtility");
        if (process == null)
          return;

        process.PreProcess += (command, filename) =>
        {
          if (command == null)
            return;

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
          var stream = ffprobe.getStreamProperties()?.FirstOrDefault(stream => stream.codec_type == "video");
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

          decimal duration = container?.duration ?? 0m;
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
      //OpenOutputView("git.exe", "help -a", "git help");
      ToastShow("これはテストです。");
    }

    private void DownloadUrl_Leave(object sender, EventArgs e)
    {
      if (DownloadUrl.Text.Length > 0 && !Regex.IsMatch(DownloadUrl.Text, "^https?://"))
        TooltipShow(DownloadUrl, "フォーマットエラー");
    }

    private void AddDownloadQueue_Click(object sender, EventArgs e)
    {
      var ytdlpItem = DownloadUrl.SelectedItem as YtdlpItem;
      if (ytdlpItem == null)
        return;

      if (PlaylistGroup.Enabled)
      {
        var mi = Playlist.SelectedItem as MediaInformation;
        var url = mi?.webpage_url;
        if (url == null)
          return;

        ytdlpItem = new YtdlpItem(url, mi, mi?.image, null);
      }

      var format = OutputFileFormat.Text;
      var list = OutputFileFormatBindingSource.DataSource as List<string> ?? throw new NullReferenceException("Datasource is null");
      if (false == list.Any(item => item == format))
        OutputFileFormat.SelectedIndex = OutputFileFormatBindingSource.Add(format);

      YtdlpAddDownloadQueue(ytdlpItem!, FmtSeparated.Checked);
    }

    private void BeginDequeue_Click(object sender, EventArgs e)
    {
      YtdlpBeginDequeue();
    }

    private void StopDownload_Click(object sender, EventArgs e)
    {
      YtdlpAbortDownload();
    }

    private async void SubmitParseUrl_Click(object sender, EventArgs e)
    {
      var url = DownloadUrl.Text;
      var list = UrlBindingSource.DataSource as YtdlpItems ?? throw new NullReferenceException("YtdlpItems is null");
      if (false == list.Any(item => item?.Item1 == url))
      {
        var ytdlpItem = await YtdlpParseDownloadUrl(url);
        if (ytdlpItem == null)
          return;

        DownloadUrl.SelectedIndex = UrlBindingSource.Add(ytdlpItem);
        DownloadUrl_SelectedIndexChanged(DownloadUrl, new EventArgs());
      }
    }

    private void DownloadUrl_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DownloadUrl.SelectedIndex < 0)
      {
        ThumbnailBox.ContextMenuStrip = null;
        ThumbnailBox.Image = null;
        DurationTime.Text = "00:00:00.0000";
        DurationTime.Visible = false;
        MediaTitle.Text = string.Empty;
        VideoOnlyFormatSource.Clear();
        AudioOnlyFormatSource.Clear();
        MovieFormatSource.Clear();
        AddDownloadQueue.Enabled = false;
        SubmitDownloadDequeue.Enabled = false;
        return;
      }

      var item = DownloadUrl.SelectedItem as YtdlpItem;

      if (item?.Item4 != null)
      {
        // ytdlpItem is playlist, then expand to Playlist listbox control. 
        Debug.WriteLine("This is playlist url");
        EnablePlaylist(item);
        return;
      }
      else if (PlaylistGroup.Enabled)
      {
        DisablePlaylist();
      }

      SetDownloadFormats(item);
    }

    private void Tab_SelectedIndexChanged(object sender, EventArgs e)
    {
      string tabname = Tab.TabPages[Tab.SelectedIndex].Name;
      bool IsDownloader = tabname == "PageDownloader";
      bool IsConverter = tabname == "PageConvert" || tabname == "PageUtility";
      bool IsSetting = tabname == "PageSetting";

      InputBox.Enabled = IsConverter;
      FilePrefix.Enabled = FileSuffix.Enabled = FileName.Enabled = FileContainer.Enabled = IsConverter;

      var set = DirectoryListBindingSource.DataSource as StringListItemsSet ?? throw new Exception("DataSource is not initialize yet");
      if (IsConverter)
      {
        DirectoryListBindingSource.DataMember = "Item1";
        OutputDirectoryList = set?.Item1 ?? throw new Exception("Item not initialize");
        cbOutputDir.SelectedIndex = set.Item4[0];
        OutputBox.Enabled = true;
      }
      else if (IsDownloader)
      {
        DirectoryListBindingSource.DataMember = "Item2";
        OutputDirectoryList = set?.Item2 ?? throw new Exception("Item not initialize");
        cbOutputDir.SelectedIndex = set.Item4[1];
        OutputBox.Enabled = true;

        DownloadUrl.Focus();
      }
      else if (IsSetting)
      {
        DirectoryListBindingSource.DataMember = "Item3";
        OutputDirectoryList = [];
        cbOutputDir.SelectedIndex = -1;

        OutputBox.Enabled = false;
      }
    }

    private void LinkYdlOutputTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      CustomProcess.ShellExecute("https://github.com/yt-dlp/yt-dlp/#output-template");
    }

    private void UseCookie_SelectedIndexChanged(object sender, EventArgs e)
    {
      var value = UseCookie.SelectedValue?.ToString() ?? "none";
      if (value != "none" && value != "file" && value != "firefox")
        MessageBox.Show("予め選択されたブラウザを全て終了させてください。", "ブラウザのCookie使用について");
    }

    private void SubmitOpenCookie_Click(object sender, EventArgs e)
    {
      if (DialogResult.Cancel == OpenCookieFileDialog.ShowDialog())
        return;

      Settings.Default.cookiePath = OpenCookieFileDialog.FileName;
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
          FileName = $"{mi?.title!}-thumbnail",
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

    private void FileListBindingSource_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
    {
      var bs = sender as BindingSource;
      if (bs?.Count > 0 && FileList.ContextMenuStrip == null)
        FileList.ContextMenuStrip = FileListMenu;
      else if (bs?.Count == 0 && FileList.ContextMenuStrip != null)
        FileList.ContextMenuStrip = null;
    }

    private void FileListMenuItemClear_Click(object sender, EventArgs e)
    {
      FileListBindingSource.Clear();
    }

    private void FileListMenuItemDelete_Click(object sender, EventArgs e)
    {
      foreach (var item in FileList.SelectedItems.Cast<object>().Select(item => item).ToArray())
        FileListBindingSource.Remove(item);
    }

    private void EditListItems(object sender, LinkLabelLinkClickedEventArgs e)
    {
      string? target = (sender as LinkLabel)?.Tag?.ToString();
      if (target == null)
        return;

      var controls = Controls.Find(target, true);
      if (controls.Length > 0)
      {
        var cb = controls[0] as ComboBox;
        var bindingSource = cb?.DataSource as BindingSource;
        if (bindingSource != null)
        {
          Tuple<string, ListItemType> property;

          switch (cb?.Name)
          {
            case "cbOutputDir":
              property = new Tuple<string, ListItemType>("出力フォルダ一覧", ListItemType.FileOrDirectory);
              break;
            case "OutputFileFormat":
              property = new Tuple<string, ListItemType>("出力ファイル名テンプレート一覧", ListItemType.PlainText);
              break;
            case "DownloadUrl":
              property = new Tuple<string, ListItemType>("ダウンロードURL一覧", ListItemType.Url);
              bindingSource.ListChanged += (s, e) => DownloadUrl_SelectedIndexChanged(DownloadUrl, new EventArgs());
              break;
            default:
              return;
          }

          var form = new ListEditor(bindingSource, property.Item2);
          var listbox = form.GetListEditorControl();

          form.TopMost = true;
          form.Text = property.Item1;
          form.FormClosing += (s, e) => listbox.SelectionMode = SelectionMode.One;
          form.Owner = this;

          listbox.SelectionMode = SelectionMode.MultiExtended;

          form.ShowDialog();
          bindingSource.ResetBindings(false);
        }
      }
    }

    private void TimeFormatValidating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      var textbox = sender as TextBox;
      if (textbox == null)
      {
        e.Cancel = true;
        return;
      }

      var text = textbox.Text.Trim();
      if (!string.IsNullOrEmpty(text) && !IsDateTime().IsMatch(text) && !IsSecondTime().IsMatch(text))
      {
        MessageBox.Show("入力されたテキストはフォーマットが異なります。\n正しいフォーマットで入力してください。");
        e.Cancel = true;
      }

      textbox.Text = text;
    }

    private void cbOutputDir_SelectedIndexChanged(object sender, EventArgs e)
    {
      var set = DirectoryListBindingSource.DataSource as StringListItemsSet;

      set?.Item4.SetValue(
        cbOutputDir.SelectedIndex,
        Tab.TabPages[Tab.SelectedIndex].Name != "PageDownloader" ? 0 : 1
      );
    }

    private void Playlist_SelectedIndexChanged(object sender, EventArgs e)
    {
      var listbox = sender as ListBox;
      var item = listbox?.SelectedItem as MediaInformation;
      var url = item?.webpage_url;
      if (url == null)
        return;

      SetDownloadFormats(new YtdlpItem(url, item, item?.image, null));
    }

    private void UrlBindingSource_ListChanged(object sender, ListChangedEventArgs e)
    {
      DisablePlaylist();
      if (DownloadUrl.SelectedIndex >= 0)
      {
        var item = DownloadUrl.SelectedItem as YtdlpItem;
        if (item != null && item.Item4 != null)
          EnablePlaylist(item);
      }
      else
      {
        AddDownloadQueue.Enabled = false;
        SubmitDownloadDequeue.Enabled = false;
      }
    }

    private void PlayListDownloadAll_Click(object sender, EventArgs e)
    {
      var ytdlpItem = DownloadUrl.SelectedItem as YtdlpItem;

      var format = OutputFileFormat.Text;
      var list = OutputFileFormatBindingSource.DataSource as List<string>;
      if (list == null || ytdlpItem?.Item4 == null)
        return;

      if (false == list.Any(item => item == format))
        OutputFileFormat.SelectedIndex = OutputFileFormatBindingSource.Add(format);

      foreach (var mediaInfo in ytdlpItem.Item4)
      {
        if (mediaInfo == null || mediaInfo.webpage_url == null)
          continue;

        var item = new YtdlpItem(mediaInfo.webpage_url, mediaInfo, mediaInfo.image, null);
        YtdlpAddDownloadQueue(item, false, true);
      }
    }

    private void DummyProgressBar_VisibleChanged(object sender, EventArgs e)
    {
      var pb = sender as ToolStripProgressBar;
      if (pb == null)
        return;

      pb.Width = pb.Visible ? 150 : 1;
    }

    private void VideoOnlyFormatSource_ListChanged(object sender, ListChangedEventArgs e)
    {
      FormatSourceChange(VideoOnlyFormat, VideoOnlyFormatSource);
    }

    private void AudioOnlyFormatSource_ListChanged(object sender, ListChangedEventArgs e)
    {
      FormatSourceChange(AudioOnlyFormat, AudioOnlyFormatSource);
    }

    private void MovieFormatSource_ListChanged(object sender, ListChangedEventArgs e)
    {
      FormatSourceChange(MovieFormat, MovieFormatSource);
    }

    private void OutputFileFormatBindingSource_ListChanged(object sender, ListChangedEventArgs e)
    {
      FormatSourceChange(OutputFileFormat, OutputFileFormatBindingSource);
    }

    private void DirectoryListBindingSource_ListChanged(object sender, ListChangedEventArgs e)
    {
      FormatSourceChange(cbOutputDir, DirectoryListBindingSource);
    }

    private void DirectoryListBindingSource_DataMemberChanged(object sender, EventArgs e)
    {
      FormatSourceChange(cbOutputDir, DirectoryListBindingSource);
    }

    private void VideoOnlyFormatSource_DataSourceChanged(object sender, EventArgs e)
    {
      VideoOnlyFormat.DropDownWidth = VideoOnlyFormat.Width;
    }

    private void AudioOnlyFormatSource_DataSourceChanged(object sender, EventArgs e)
    {
      AudioOnlyFormat.DropDownWidth = AudioOnlyFormat.Width;
    }

    private void MovieFormatSource_DataSourceChanged(object sender, EventArgs e)
    {
      MovieFormat.DropDownWidth = MovieFormat.Width;
    }

    private void OutputFileFormatBindingSource_DataSourceChanged(object sender, EventArgs e)
    {
      OutputFileFormat.DropDownWidth = OutputFileFormat.Width;
    }

    private void DirectoryListBindingSource_DataSourceChanged(object sender, EventArgs e)
    {
      cbOutputDir.DropDownWidth = cbOutputDir.Width;
    }

    private void Format_Click(object sender, EventArgs e)
    {
      var cb = sender as ComboBox;
      if (cb == null)
        return;

      if (cb == AudioOnlyFormat || cb == VideoOnlyFormat)
        FmtSeparated.Checked = true;
      else if (cb == MovieFormat)
        FmtWhole.Checked = true;
    }

    private void StatusBarMenuItemClearQueue_Click(object sender, EventArgs e)
    {
      YtdlpClearDequeue();
    }

    private void HideThumbnail_CheckStateChanged(object sender, EventArgs e)
    {
      Invoke(DownloadUrl_SelectedIndexChanged, [DownloadUrl, new EventArgs()]);
    }
  }
}
