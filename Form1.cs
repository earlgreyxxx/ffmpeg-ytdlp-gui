using ffmpeg_command_builder.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ffmpeg_command_builder
{
  using CodecListItem = ListItem<Codec>;
  using CodecListItems = List<ListItem<Codec>>;
  using StringListItem = ListItem<string>;
  using StringListItems = List<ListItem<string>>;
  using FFmpegBatchList = Dictionary<ffmpeg_command, IEnumerable<string>>;

  public partial class Form1 : Form
  {
    private const int SaveFolderLength = 15;

    private Dictionary<string, StringListItems> PresetList;
    private Dictionary<string, CodecListItems> HardwareDecoders;
    private StringListItems DeInterlacesCuvid;
    private StringListItems InputFileList;
    private StringListItems OutputDirectoryList;
    private Size HelpFormSize = new(0, 0);
    private FFmpegBatchList BatchList;

    [GeneratedRegex(@"\.(?:mp4|mpg|avi|mkv|webm|m4v|wmv|ts|m2ts)$", RegexOptions.IgnoreCase, "ja-JP")]
    private static partial Regex RegexMovieFile();

    [GeneratedRegex("^(Intel|Nvidia)", RegexOptions.IgnoreCase, "ja-JP")]
    private static partial Regex IsIntelOrNvidia();

    public Form1()
    {
      InitializeComponent();
      ChangeCurrentDirectory();

      StringListItems nvencPresetList =
      [
        new StringListItem("default"),
        new StringListItem("slow"),
        new StringListItem("medium"),
        new StringListItem("fast"),
        new StringListItem("hp"),
        new StringListItem("hq"),
        new StringListItem("bd"),
        new StringListItem("ll","low latency"),
        new StringListItem("llhq","low latency hq"),
        new StringListItem("llhp","low latency hp"),
        new StringListItem("lossless"),
        new StringListItem("losslesshp"),
        new StringListItem("p1","p1:fastest"),
        new StringListItem("p2","p2:faster"),
        new StringListItem("p3","p3:fast"),
        new StringListItem("p4","p4:medium"),
        new StringListItem("p5","p5:slow"),
        new StringListItem("p6","p6:slower"),
        new StringListItem("p7","p7:slowest")
      ];

      StringListItems qsvPresetList =
      [
        new StringListItem("veryfast"),
        new StringListItem("faster"),
        new StringListItem("fast"),
        new StringListItem("medium"),
        new StringListItem("slow"),
        new StringListItem("slower"),
        new StringListItem("veryslow")
      ];

      StringListItems cpuPresetList =
      [
        new StringListItem("ultrafast"),
        new StringListItem("superfast"),
        new StringListItem("veryfast "),
        new StringListItem("faster"),
        new StringListItem("fast"),
        new StringListItem("medium"),
        new StringListItem("slow"),
        new StringListItem("slower"),
        new StringListItem("veryslow"),
        new StringListItem("placebo")
      ];

      PresetList = new Dictionary<string, StringListItems>()
      {
        { "h264_nvenc", nvencPresetList },
        { "hevc_nvenc", nvencPresetList },
        { "h264_qsv",qsvPresetList },
        { "hevc_qsv",qsvPresetList },
        { "copy",new StringListItems() },
        { "libx264",cpuPresetList },
        { "hevc",cpuPresetList },
        { "libx265",cpuPresetList },
      };

      HardwareDecoders = new Dictionary<string, CodecListItems>()
      {
        {
          "nvidia",
          [
            new CodecListItem(new Codec("vp9","cuvid"),"VP9"),
            new CodecListItem(new Codec("h264","cuvid"),"H264"),
            new CodecListItem(new Codec("hevc","cuvid"),"HEVC"),
            new CodecListItem(new Codec("mpeg4","cuvid"),"MPEG4"),
            new CodecListItem(new Codec("mpeg2","cuvid"),"MPEG2"),
            new CodecListItem(new Codec("mjpeg","cuvid"),"MJPEG"),
            new CodecListItem(new Codec("mpeg1","cuvid"),"MPEG1"),
            new CodecListItem(new Codec("vc1","cuvid"),"VC1"),
            new CodecListItem(new Codec("vp8","cuvid"),"VP8"),
            new CodecListItem(new Codec("av1","cuvid"),"AV1")
          ]
        },
        {
          "intel",
          [
            new CodecListItem(new Codec("vp9","qsv"),"VP9"),
            new CodecListItem(new Codec("h264","qsv"),"H264"),
            new CodecListItem(new Codec("hevc","qsv"),"HEVC"),
            new CodecListItem(new Codec("mpeg2","qsv"),"MPEG2"),
            new CodecListItem(new Codec("mjpeg","qsv"),"MJPEG"),
            new CodecListItem(new Codec("vc1","qsv"),"VC1"),
            new CodecListItem(new Codec("vp8","qsv"),"VP8"),
            new CodecListItem(new Codec("av1","qsv"),"AV1")
          ]
        },
        {
          "cpu",
          [
            new CodecListItem(new Codec("h264","cpu","h264"),"H264"),
            new CodecListItem(new Codec("hevc","cpu","hevc"),"HEVC"),
            new CodecListItem(new Codec("mjpeg","cpu","mjpeg"),"MJPEG"),
            new CodecListItem(new Codec("av1","cpu","av1"),"AV1")
          ]
        }
      };

      DeInterlacesCuvid =
      [
        new StringListItem("bob","bob:cuvid"),
        new StringListItem("adaptive","adaptive:cuvid")
      ];

      DeInterlaceListBindingSource.DataSource = new StringListItems()
      {
        new StringListItem("1:-1:0","bwdif"),
        new StringListItem("2:-1:0","yadif")
      };
      cbDeinterlaceAlg.DataSource = DeInterlaceListBindingSource;

      UseAudioEncoder.DataSource = new CodecListItems()
      {
        new CodecListItem(new Codec("aac"),"AAC"),
        new CodecListItem(new Codec("libmp3lame"),"MP3"),
        new CodecListItem(new Codec("libvorbis"),"Ogg"),
      };
      UseAudioEncoder.SelectedIndex = 0;
      UseAudioEncoder.Enabled = chkEncodeAudio.Checked;

      var GpuDeviceList = GetGPUDeviceList();

      cbDevices.DataSource = GpuDeviceList;
      cbDevices.SelectedIndex = 0;

      var videoEncoders = new CodecListItems();
      var ci = new CultureInfo("en-US");
      foreach (var device in GpuDeviceList)
      {
        if (device.Value.StartsWith("intel", true, ci))
        {
          videoEncoders.Add(new CodecListItem(new Codec("hevc", "qsv"), "HEVC(QSV)"));
          videoEncoders.Add(new CodecListItem(new Codec("h264", "qsv"), "H264(QSV)"));
        }
        if (device.Value.StartsWith("nvidia", true, ci))
        {
          videoEncoders.Add(new CodecListItem(new Codec("hevc", "nvenc"), "HEVC(NVEnc)"));
          videoEncoders.Add(new CodecListItem(new Codec("h264", "nvenc"), "H264(NVEnc)"));
        }
      }
      videoEncoders.Add(new CodecListItem(new Codec("hevc", "cpu", "libx265"), "HEVC(libx265)"));
      videoEncoders.Add(new CodecListItem(new Codec("libx264", "cpu", "libx264"), "H264(libx264)"));
      videoEncoders.Add(new CodecListItem(new Codec("copy", "cpu", "copy"), "COPY"));

      UseVideoEncoder.DataSource = videoEncoders;
      UseVideoEncoder.SelectedIndex = 0;

      EncoderHelpList.DataSource = videoEncoders.Select(encoder => encoder.Clone()).ToList();
      EncoderHelpList.SelectedIndex = 0;

      FileListBindingSource.DataSource = InputFileList = [];
      FileList.DataSource = FileListBindingSource;

      DirectoryListBindingSource.DataSource = OutputDirectoryList = [];
      cbOutputDir.DataSource = DirectoryListBindingSource;

      FileContainer.DataSource = new StringListItems()
      {
        new StringListItem(".mp4","mp4"),
        new StringListItem(".mkv","mkv"),
        new StringListItem(".mp3","MP3"),
        new StringListItem(".aac","AAC"),
        new StringListItem(".ogg","Vorbis"),
        new StringListItem(".webm","WebM"),
        new StringListItem(".webA","WebA")
      };

      ImageType.DataSource = new StringListItems()
      {
        new StringListItem("mjpeg","JPEG形式",".jpg"),
        new StringListItem("png","PNG形式",".png"),
        new StringListItem("gif","GIF形式",".gif")
      };

      FilePrefix.DataSource = new List<string>()
      {
        "%d-",
        "%02d-",
        "%03d-",
        "%04d-",
        "%05d-"
      };
      FileSuffix.DataSource = new List<string>()
      {
        "-%d",
        "-%02d",
        "-%03d",
        "-%04d",
        "-%05d"
      };

      FreeOptions.DataBindings.Add("Text", Settings.Default, "free");
      resizeTo.DataBindings.Add("Value", Settings.Default, "resize");
      LookAhead.DataBindings.Add("Value", Settings.Default, "lookAhead");
      VideoWidth.DataBindings.Add("Value", Settings.Default, "videoWidth");
      VideoHeight.DataBindings.Add("Value", Settings.Default, "videoHeight");
      FrameRate.DataBindings.Add("Value", Settings.Default, "fps");
      IsOpenStderr.DataBindings.Add("Checked", Settings.Default, "OpenStderr");
      Overwrite.DataBindings.Add("Checked", Settings.Default, "overwrite");

      rbResizeNone.DataBindings.Add("Checked",Settings.Default,"resizeNone");
      rbResizeFullHD.DataBindings.Add("Checked",Settings.Default,"resizeFullHD");
      rbResizeHD.DataBindings.Add("Checked",Settings.Default,"resizeHD");
      rbResizeNum.DataBindings.Add("Checked",Settings.Default,"resizeNum");
      chkConstantQuality.DataBindings.Add("Checked",Settings.Default,"cq");
    }

    private void Form1_Load(object sender, EventArgs e)
    {
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
      }

      if (Settings.Default.ffmpeg?.Count > 0)
      {
        foreach (string item in Settings.Default.ffmpeg)
          ffmpeg.Items.Add(item);

        ffmpeg.SelectedIndex = 0;
      }

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
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (Proceeding != null)
      {
        e.Cancel = true;
        return;
      }
      Settings.Default.outputFolders = [];
      Settings.Default.ffmpeg = [];
      Settings.Default.HelpHeight = HelpFormSize.Height;
      Settings.Default.HelpWidth = HelpFormSize.Width;
      Settings.Default.bitrate = vBitrate.Value;

      var checks = new List<string>();
      var items = OutputDirectoryList.OrderByDescending(item => (DateTime)item.Data).Take(SaveFolderLength);
      foreach (var item in items)
      {
        if (Directory.Exists(item.Value) && !checks.Contains(item.Value))
        {
          checks.Add(item.Value);
          Settings.Default.outputFolders.Add($"{item.Value}|{((DateTime)item.Data).ToString()}");
        }
      }

      foreach (string item in ffmpeg.Items)
      {
        if (!string.IsNullOrEmpty(item) && !Settings.Default.ffmpeg.Contains(item))
          Settings.Default.ffmpeg.Add(item);
      }

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

      if (chkCrop.Checked && chkUseHWDecoder.Checked && (VideoWidth.Value <= 0 || VideoHeight.Value <= 0))
      {
        MessageBox.Show("ハードウェアデコーダーでクロップを行う場合は、動画のサイズを指定する必要があります。");
        VideoWidth.Focus();
        return;
      }

      if (FileListBindingSource.Count > 0)
      {
        btnSubmitInvoke.Enabled = false;
        var command = CreateCommand(chkAudioOnly.Checked);
        if (FileName.Text.Trim() != "元ファイル名")
          command.OutputBaseName(FileName.Text);

        OnBeginProcess();
        CreateFFmpegProcess(command)?.Begin();
      }
    }

    private void btnSubmitOpenDlg_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(cbOutputDir.Text) && Directory.Exists(cbOutputDir.Text))
        FindFolder.SelectedPath = cbOutputDir.Text;

      if (DialogResult.Cancel == FindFolder.ShowDialog())
        return;

      var item = new StringListItem(FindFolder.SelectedPath, DateTime.Now);
      var same = OutputDirectoryList.FirstOrDefault(item => item.Value == FindFolder.SelectedPath);
      if (same == null)
        DirectoryListBindingSource.Add(item);
      else
        item = same;

      cbOutputDir.SelectedItem = item;
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      if (chkCrop.Checked && chkUseHWDecoder.Checked && (VideoWidth.Value <= 0 || VideoHeight.Value <= 0))
      {
        MessageBox.Show("ハードウェアデコーダーでクロップを行う場合は、動画のサイズを指定する必要があります。");
        VideoWidth.Focus();
        return;
      }

      var ffcommand = CreateCommand(chkAudioOnly.Checked);

      if (!string.IsNullOrEmpty(cbOutputDir.Text) && cbOutputDir.SelectedIndex < 0 && !OutputDirectoryList.Any(item => item.Value == cbOutputDir.Text))
      {
        var item = new StringListItem(cbOutputDir.Text, cbOutputDir.Text, DateTime.Now);
        DirectoryListBindingSource.Add(item);
        cbOutputDir.SelectedItem = item;
      }

      Commandlines.Text = ffcommand.GetCommandLine("sample.mp4");
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
        vQualityLabel.Text = codec.GpuSuffix == "qsv" ? "ICQ" : "-cq";
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
      LookAhead.Enabled = UseVideoEncoder.Enabled = cbPreset.Enabled = vBitrate.Enabled = chkConstantQuality.Enabled = !isChecked;
      chkFilterDeInterlace.Enabled = chkUseHWDecoder.Enabled = !isChecked;
      CropBox.Enabled = ResizeBox.Enabled = RotateBox.Enabled = LayoutBox.Enabled = !isChecked;
      if(!chkUseHWDecoder.Enabled)
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
        sw.WriteLine(ffmpeg_command.CreateBatch(BatchList));
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

      if (chkCrop.Checked && chkUseHWDecoder.Checked && (VideoWidth.Value <= 0 || VideoHeight.Value <= 0))
      {
        MessageBox.Show("ハードウェアデコーダーでクロップを行う場合は、動画のサイズを指定する必要があります。");
        VideoWidth.Focus();
        return;
      }

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

      CropBox.Enabled = LayoutBox.Enabled = ResizeBox.Enabled = RotateBox.Enabled = !isCopy;
      cbPreset.Enabled = chkConstantQuality.Enabled = vBitrate.Enabled = !isCopy;
      LookAhead.Enabled = chkUseHWDecoder.Enabled = OpenEncoderHelp.Enabled = !isCopy;
      chkUseHWDecoder.Enabled = !isCpu;
      DeInterlaceBox.Enabled = OthersBox.Enabled = !isCopy;

      InitPresetAndDevice(codec);
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

      string[] ffmpegPathes = FindInPath("ffmpeg");
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

      VideoWidth.Enabled = VideoHeight.Enabled = (bChecked && chkUseHWDecoder.Checked);
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
      VideoWidth.Enabled = VideoHeight.Enabled = chkCrop.Checked && chkUseHWDecoder.Checked && codec.GpuSuffix == "cuvid";
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
        if (!re.IsMatch(FileName.Text) && !re.IsMatch(FilePrefix.Text) && !re.IsMatch(FileSuffix.Text))
          throw new Exception("画像出力の際は、%d などの連番号フォーマットが含まれている必要があります。");

        if (InputFileList.Count < 1)
          throw new Exception("一つ以上の入力ファイルが必要です。");

        var imagetype = ImageType.SelectedItem as StringListItem;
        string extension = imagetype.Data.ToString();
        string codec = imagetype.Value;

        var command = new ffmpeg_command(string.IsNullOrEmpty(ffmpeg.Text) ? "ffmpeg" : ffmpeg.Text);

        List<string> list = ["-vsync cfr", "-f image2"];
        if (FrameRate.Value > 0)
          list.Add($"-vf fps=fps=1/{FrameRate.Value}:round=down");

        if (ImageWidth.Value > 0 && ImageHeight.Value > 0)
          list.Add($"-s {ImageWidth.Value}x{ImageHeight.Value}");

        command
          .Starts(ImageSS.Text)
          .To(ImageTo.Text)
          .vcodec(null)
          .acodec(null)
          .setOptions(list)
          .OutputDirectory(cbOutputDir.Text)
          .OutputPrefix(FilePrefix.Text)
          .OutputSuffix(FileSuffix.Text)
          .OutputContainer(extension);

        if (FileName.Text.Trim() != "元ファイル名")
          command.OutputBaseName(FileName.Text);

        command.MultiFileProcess = InputFileList.Count > 1;

        OnBeginProcess();
        CreateFFmpegProcess(command).Begin();
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
  }
}
