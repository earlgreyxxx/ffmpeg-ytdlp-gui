using ffmpeg_command_builder.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ffmpeg_command_builder
{
  using CodecListItem = ListItem<Codec>;
  using CodecListItems = List<ListItem<Codec>>;
  using StringListItem = ListItem<string>;
  using StringListItems = List<ListItem<string>>;
  using DecimalListItem = ListItem<decimal>;
  using DecimalListItems = List<ListItem<decimal>>;

  partial class Form1 : Form
  {
    // Static members
    // ---------------------------------------------------------------------------------
    [GeneratedRegex(@"\.(?:exe|cmd|ps1|bat)$")]
    private static partial Regex IsExecutableFile();

    private static void CheckDirectory(string strPath)
    {
      string path = strPath.Trim();
      if (string.IsNullOrEmpty(path))
        throw new Exception("出力先フォルダを指定してください。");

      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
    }

    private static bool IsFile(string path)
    {
      return (File.GetAttributes(path) & FileAttributes.Archive) == FileAttributes.Archive || (File.GetAttributes(path) & FileAttributes.Normal) == FileAttributes.Normal;
    }

    private static bool IsDirectory(string path)
    {
      return (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
    }

    private static List<ManagementObject> GpuDevices;
    private static StringListItems GetGPUDeviceList()
    {
      if (GpuDevices == null)
      {
        var VideoDevices = new ManagementObjectSearcher("SELECT * FROM WIN32_VIDEOCONTROLLER");
        GpuDevices = VideoDevices.Get().Cast<ManagementObject>().ToList();
      }

      var deviceList = new StringListItems();
      foreach (var device in GpuDevices)
      {
        deviceList.Add(
          new StringListItem(
            device["AdapterCompatibility"].ToString(),
            device["Name"].ToString()
          )
        );
      }
      deviceList.Add(new StringListItem("cpu","CPU(Software)"));
      return deviceList;
    }

    private static void ChangeCurrentDirectory(string dir = null)
    {
      if (string.IsNullOrEmpty(dir))
        dir = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

      Environment.CurrentDirectory = Environment.ExpandEnvironmentVariables(dir);
    }

    // Instance members
    // ---------------------------------------------------------------------------------

    private void InitializeSettingsBinding()
    {
      CookiePath.DataBindings.Add("Text", Settings.Default, "cookiePath");
      FrameRate.DataBindings.Add("Value", Settings.Default, "fps");
      FreeOptions.DataBindings.Add("Text", Settings.Default, "free");
      ImageHeight.DataBindings.Add("Value", Settings.Default, "imageHeight");
      ImageWidth.DataBindings.Add("Value", Settings.Default, "imageWidth");
      IsOpenStderr.DataBindings.Add("Checked", Settings.Default, "OpenStderr");
      LookAhead.DataBindings.Add("Value", Settings.Default, "lookAhead");
      OutputFileFormat.DataBindings.Add("Text", Settings.Default, "downloadFileName");
      Overwrite.DataBindings.Add("Checked", Settings.Default, "overwrite");
      TileColumns.DataBindings.Add("Value", Settings.Default, "tileColumns");
      TileRows.DataBindings.Add("Value", Settings.Default, "tileRows");
      chkAfterDownload.DataBindings.Add("Checked", Settings.Default, "downloadCompleted");
      chkConstantQuality.DataBindings.Add("Checked", Settings.Default, "cq");
      resizeTo.DataBindings.Add("Value", Settings.Default, "resizeTo");
    }

    private void InitializeDataSource()
    {
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
      videoEncoders.Add(new CodecListItem(new Codec("gif", "cpu", "gif"), "アニメーションGIF"));
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
        new StringListItem(".weba","WebA"),
        new StringListItem(".gif","gif")
      };

      ImageType.DataSource = new StringListItems()
      {
        new StringListItem("mjpeg","JPEG形式",".jpg"),
        new StringListItem("png","PNG形式",".png")
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

      UseCookie.DataSource = new StringListItems()
      {
        new StringListItem("none","使用しない"),
        new StringListItem("file","Cookieファイル"),
        new StringListItem("edge","Microsoft Edge"),
        new StringListItem("chrome","Google Chrome"),
        new StringListItem("firefox","Mozilla FireFox"),
        new StringListItem("opera","Opera Software"),
        new StringListItem("brave","Brave"),
        new StringListItem("vivaldi","VIVALDI")
      };

      VideoFrameRate.DataSource = new DecimalListItems()
      {
        new DecimalListItem(0,"元動画と同じ"),
        new DecimalListItem(25,"PAL(25 fps)"),
        new DecimalListItem(29.97m,"NTSC(29.97 fps)"),
        new DecimalListItem(59.94m,"59.94 fps"),
        new DecimalListItem(30,"30 fps"),
        new DecimalListItem(60,"60 fps"),
        new DecimalListItem(24,"FILM"),
        new DecimalListItem(23.98m,"NTSC-FILM(23.98 fps)")
      };

      AudioOnlyFormatSource.DataSource = new StringListItems();
      AudioOnlyFormat.DataSource = AudioOnlyFormatSource;
      VideoOnlyFormatSource.DataSource = new StringListItems();
      VideoOnlyFormat.DataSource = VideoOnlyFormatSource;
      MovieFormatSource.DataSource = new StringListItems();
      MovieFormat.DataSource = MovieFormatSource;
    }

    private void RuntimeSetting(ffmpeg_command command, string filename)
    {
      if (command.bAudioOnly)
        return;

      if (!File.Exists(filename))
      {
        command.IsLandscape = true;
        command.Width = 1920;
        command.Height = 1080;
      }
      else
      {
        var ffprobe = ffprobe_process.CreateInstance(filename);
        var stream = ffprobe.getStreamProperties()?.FirstOrDefault(msp => msp.codec_type == "video");

        command.IsLandscape = stream.width > stream.height;
        command.Width = (int)stream.width;
        command.Height = (int)stream.height;
      }

      if (chkCrop.Checked)
      {
        if (chkUseHWDecoder.Checked && HWDecoder.SelectedValue.ToString().EndsWith("_cuvid"))
          command
            .size(command.Width, command.Height)
            .crop(true, CropWidth.Value, CropHeight.Value, CropX.Value, CropY.Value);
        else
          command.crop(CropWidth.Value, CropHeight.Value, CropX.Value, CropY.Value);
      }
      else
      {
        // クロップ削除
        command.crop();
      }
    }

    private ffmpeg_process Proceeding;
    private ffmpeg_process CreateFFmpegProcess(ffmpeg_command command)
    {
      command.Overwrite = Overwrite.Checked;

      var process = new ffmpeg_process(command,FileListBindingSource);
      process.ReceiveData += data => Invoke(() => OutputStderr.Text = data);
      process.ProcessExit += filename => Invoke(() =>
      {
        var item = FileListBindingSource.OfType<StringListItem>().FirstOrDefault(item => item.Value == filename);
        if (item == null)
          return;

        FileListBindingSource.Remove(item);
      });
      process.ProcessesDone += () => Invoke(() =>
      {
        btnStop.Enabled = btnStopAll.Enabled = btnStopUtil.Enabled = btnStopAllUtil.Enabled = false;
        OpenLogFile.Enabled = true;
        if (FileListBindingSource.Count > 0)
          btnSubmitInvoke.Enabled = true;

        Proceeding = null;
        MessageBox.Show("変換処理が終了しました。");
      });

      if (IsOpenStderr.Checked)
      {
        var form = new StdoutForm();
        process.ReceiveData += data =>
        {
          if (string.IsNullOrEmpty(data))
            return;

          if (form.Pause)
            form.LogData.Add(data);
          else
            form.Invoke(() => form.WriteLine(data));
        };
        process.ProcessesDone += () => form.Invoke(form.OnProcessExit);
        form.Show();
      }

      Proceeding = process;
      return process;
    }

    private ffmpeg_command CreateFFMpegCommandInstance()
    {
      ffmpeg_command ffcommand;

      var codec = UseVideoEncoder.SelectedValue as Codec;
      if (codec.GpuSuffix == "nvenc")
        ffcommand = new ffmpeg_command_cuda(ffmpeg.Text);
      else if (codec.GpuSuffix == "qsv")
        ffcommand = new ffmpeg_command_qsv(ffmpeg.Text);
      else if(codec.Name != "copy" && codec.GpuSuffix == "cpu")
        ffcommand = new ffmpeg_command_cpu(ffmpeg.Text);
      else
        ffcommand = new ffmpeg_command(ffmpeg.Text);

      return ffcommand;
    }

    private ffmpeg_command CreateAudioCommand()
    {
      var ffcommand = CreateFFMpegCommandInstance().audioOnly(true);

      ffcommand
        .audioOnly(true)
        .OutputDirectory(cbOutputDir.Text)
        .OutputPrefix(FilePrefix.Text)
        .OutputSuffix(FileSuffix.Text)
        .OutputContainer(FileContainer.SelectedValue.ToString());

      if (chkEncodeAudio.Checked)
        ffcommand.acodec(UseAudioEncoder.SelectedValue.ToString()).aBitrate((int)aBitrate.Value);
      else
        ffcommand.acodec("copy").aBitrate(0);

      if (!string.IsNullOrEmpty(txtSS.Text))
        ffcommand.Starts(txtSS.Text);
      if (!string.IsNullOrEmpty(txtTo.Text))
        ffcommand.To(txtTo.Text);

      if (InputFileList.Count > 1)
        ffcommand.MultiFileProcess = true;

      return ffcommand;
    }

    private ffmpeg_command CreateCommand(bool isAudioOnly = false)
    {
      if (isAudioOnly)
        return CreateAudioCommand();

      var ffcommand = CreateFFMpegCommandInstance();

      if (!string.IsNullOrEmpty(txtSS.Text))
        ffcommand.Begin = txtSS.Text;
      if (!string.IsNullOrEmpty(txtTo.Text))
        ffcommand.End = txtTo.Text;

      if (InputFileList.Count > 1)
        ffcommand.MultiFileProcess = true;

      var codec = UseVideoEncoder.SelectedValue as Codec;
      if (codec.Name == "copy")
      {
        ffcommand
          .OutputDirectory(cbOutputDir.Text)
          .OutputPrefix(FilePrefix.Text)
          .OutputSuffix(FileSuffix.Text)
          .OutputContainer(FileContainer.SelectedValue.ToString());

        if (chkEncodeAudio.Checked)
          ffcommand.acodec(UseAudioEncoder.SelectedValue.ToString()).aBitrate((int)aBitrate.Value);
        else
          ffcommand.acodec("copy").aBitrate(0);

        return ffcommand;
      }

      ffcommand.vcodec(
        UseVideoEncoder.SelectedValue.ToString(),
        cbDevices.Items.Count > 1 ? cbDevices.SelectedIndex : 0
      );

      if (codec.Name != "gif")
      {
        ffcommand
          .vBitrate((int)vBitrate.Value, chkConstantQuality.Checked)
          .lookAhead((int)LookAhead.Value)
          .preset(cbPreset.SelectedValue.ToString());
      }

      var vfr = (decimal?)VideoFrameRate.SelectedValue;
      if (vfr == null)
      {
        var match = Regex.Match(VideoFrameRate.Text, @"^(?<int>\d+)/(?<decimal>\d+)$");
        if (match.Success)
        {
          ffcommand.vFrameRate(
            Math.Round(
              decimal.Parse(match.Groups["int"].Value) / decimal.Parse(match.Groups["decimal"].Value),
              2,
              MidpointRounding.AwayFromZero
            )
          );
        }
        else if (decimal.TryParse(VideoFrameRate.Text, out decimal temp))
        {
          ffcommand.vFrameRate(temp);
        }
        else
        {
          VideoFrameRate.SelectedIndex = 0;
          ffcommand.vFrameRate(0);
        }
      }
      else
      {
        ffcommand.vFrameRate((decimal)vfr);
      }

      if (chkUseHWDecoder.Checked)
        ffcommand.hwdecoder(HWDecoder.SelectedValue.ToString());

      if (codec.Name == "gif")
        ffcommand.acodec(null);
      else if (chkEncodeAudio.Checked)
        ffcommand.acodec(UseAudioEncoder.SelectedValue.ToString()).aBitrate((int)aBitrate.Value);
      else
        ffcommand.acodec("copy").aBitrate(0);

      ffcommand
        .OutputDirectory(cbOutputDir.Text)
        .OutputPrefix(FilePrefix.Text)
        .OutputSuffix(FileSuffix.Text)
        .OutputContainer(FileContainer.SelectedValue.ToString());

      var deinterlaces = new List<string>() { "bwdif","yadif","bob","adaptive" };

      if (chkFilterDeInterlace.Checked)
      {
        string value = cbDeinterlaceAlg.SelectedValue.ToString();
        if (cbDeinterlaceAlg.Text == "bwdif")
        {
          ffcommand.setFilter("bwdif", value);
        }
        else if (cbDeinterlaceAlg.Text == "yadif")
        {
          ffcommand.setFilter("yadif", value);
        }
        else if (value == "bob" || value == "adaptive")
        {
          ffcommand.setFilter(value, value);
        }
      }
      else
      {
        foreach (var name in deinterlaces)
          ffcommand.removeFilter(name);
      }

      var tag = int.Parse(GetCheckedRadioButton(ResizeBox).Tag as string);
      var size = tag switch
      {
        0 => 0,
        480 or 720 or 900 or 1080 => int.Parse(tag.ToString()),
        -1 => (int)resizeTo.Value,
        _ => throw new Exception("size error"),
      };

      if (size > 0)
        ffcommand.setFilter("scale", size.ToString());
      else
        ffcommand.removeFilter("scale");

      var rotate = int.Parse(GetCheckedRadioButton(RotateBox).Tag.ToString());
      if (rotate == 0)
        ffcommand.removeFilter("transpose");
      else
        ffcommand.setFilter("transpose", rotate.ToString());

      if (!string.IsNullOrEmpty(FreeOptions.Text))
        ffcommand.setOptions(FreeOptions.Text);

      return ffcommand;
    }

    private static RadioButton GetCheckedRadioButton(GroupBox groupBox)
    {
      return groupBox.Controls.OfType<RadioButton>().FirstOrDefault(radio => radio.Checked);
    }

    private void StopCurrentProcess() => StopProcess(false);
    private void StopAllProcess() => StopProcess(true);

    private void StopProcess(bool stopAll = false)
    {
      if (stopAll)
        FileListBindingSource.Clear();

      Proceeding.Kill(stopAll);
    }

    private void OpenOutputView(string executable, string arg,string formTitle = "ffmpeg outputs")
    {
      if(!File.Exists(executable))
      {
        var exepathes = CustomProcess.FindInPath(executable);
        if (exepathes.Length <= 0)
          return;
      }

      var form = new StdoutForm(executable, arg, formTitle);

      if (HelpFormSize.Width > 0 && HelpFormSize.Height > 0)
      {
        form.Width = HelpFormSize.Width;
        form.Height = HelpFormSize.Height;
      }

      form.Shown += (s, e) => form.Redirected.Start();
      form.FormClosing += (s, e) =>
      {
        HelpFormSize.Width = form.Width;
        HelpFormSize.Height = form.Height;
      };

      form.Show();
    }

    private void InitPresetAndDevice(Codec codec)
    {
      if (!PresetList.ContainsKey(codec.FullName))
      {
        cbPreset.DataSource = null;
        return;
      }

      cbPreset.DataSource = PresetList[codec.FullName];
      string hardwareName = string.Empty;

      if (codec.GpuSuffix == "nvenc")
      {
        cbPreset.SelectedIndex = 16;
        hardwareName = "nvidia";
      }
      else if (codec.GpuSuffix == "qsv")
      {
        cbPreset.SelectedIndex = 3;
        hardwareName = "intel";
      }
      else
      {
        if (codec.Name != "copy")
          cbPreset.SelectedIndex = 5;

        hardwareName = "cpu";
      }

      cbDevices.SelectedIndex = cbDevices.FindString(hardwareName);

      if(chkConstantQuality.Checked)
      {
        vQualityLabel.Text = codec.GpuSuffix switch
        {
          "qsv" => "ICQ",
          "nvenc" => "-cq",
          _ => "-crf",
        };
      }
    }

    private void OnBeginProcess()
    {
      btnStop.Enabled = btnStopAll.Enabled = btnStopUtil.Enabled = btnStopAllUtil.Enabled = true;
      OpenLogFile.Enabled = false;

      AddDirectoryListItem();
    }

    private void AddDirectoryListItem()
    {
      if (cbOutputDir.SelectedIndex < 0 && !OutputDirectoryList.Any(item => item.Value == cbOutputDir.Text))
      {
        var listitem = new StringListItem(cbOutputDir.Text, DateTime.Now);
        DirectoryListBindingSource.Add(listitem);
        cbOutputDir.SelectedItem = listitem;
      }
    }

    /// <summary>
    /// ダウンロードメディア情報
    /// </summary>
    private MediaInformation mediaInfo = null;

    /// <summary>
    /// ダウンロードプロセス
    /// </summary>
    private ytdlp_process ytdlp = null;
    private string DownloadFileName = null;
    private readonly Regex DownloadRegex = new Regex(@"\[download\]\s+Destination:\s+(?<filename>.+)$");
    private readonly Regex MergerRegex = new Regex(@"\[Merger\]\s+Merging formats into ""(?<filename>.+)""$");

    private void YtdlpClearDownload()
    {
      DownloadUrl.Text = string.Empty;
      DownloadUrl.Enabled = true;
      DownloadUrl.Focus();

      MediaTitle.Text = string.Empty;
      ThumbnailBox.Image = null;
      ThumbnailBox.ContextMenuStrip = null;

      SubmitDownload.Enabled = false;
      SubmitSeparatedDownload.Enabled = true;

      AudioOnlyFormatSource.Clear();
      VideoOnlyFormatSource.Clear();
      MovieFormatSource.Clear();

      ytdlp = null;
      mediaInfo = null;
      DownloadFileName = null;
    }

    private void YtdlpPreDownload()
    {
      ytdlp = null;
      DownloadFileName = null;
      SubmitDownload.Enabled = SubmitSeparatedDownload.Enabled = false;
      StopDownload.Enabled = true;
    }

    private void YtdlpPostDownload()
    {
      SubmitDownload.Enabled = SubmitSeparatedDownload.Enabled = true;
      StopDownload.Enabled = false;
    }

    private async Task YtdlpParseDownloadUrl()
    {
      try
      {
        Tab.Enabled = false;
        var url = DownloadUrl.Text.Trim();
        if (url.Length == 0)
          throw new Exception("URLが入力されていません。");

        var ytdlp = new ytdlp_process() { Url = url };

        var cookieKind = UseCookie.SelectedValue.ToString();
        if (cookieKind == "file" && !string.IsNullOrEmpty(CookiePath.Text) && File.Exists(CookiePath.Text))
          ytdlp.CookiePath = CookiePath.Text;
        else if (cookieKind != "none" && cookieKind != "file")
          ytdlp.CookieBrowser = cookieKind;

        OutputStderr.Text = "ダウンロード先の情報の取得及び解析中...";
        mediaInfo = await ytdlp.getMediaInformation();
        OutputStderr.Text = "";

        if (mediaInfo == null)
          throw new Exception("解析に失敗しました。");

        using (var pngStream = await mediaInfo.GetThumbnailImage())
        {
          if (pngStream != null)
          {
            ThumbnailBox.ContextMenuStrip = ImageContextMenu;
            ThumbnailBox.Image = Image.FromStream(pngStream);
          }
        }

        DownloadUrl.Enabled = false;
        MediaTitle.Text = mediaInfo.title;

        // format_id 構築
        VideoOnlyFormatSource.Clear();
        VideoOnlyFormatSource.Add(new StringListItem(string.Empty, "使用しない"));
        AudioOnlyFormatSource.Clear();
        AudioOnlyFormatSource.Add(new StringListItem(string.Empty, "使用しない"));

        MovieFormatSource.Clear();

        foreach (var format in mediaInfo.formats)
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
        if (mediaInfo.requested_formats.Count > 0)
        {
          var items = mediaInfo.requested_formats.Select(f => new
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
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
      finally
      {
        Tab.Enabled = true;
      }
    }

    private async Task YtdlpInvokeDownload(bool separatedDownload = false)
    {
      StdoutForm form = null;

      if (mediaInfo == null)
        return;

      try
      {
        YtdlpPreDownload();

        var outputdir = string.IsNullOrEmpty(cbOutputDir.Text) ? Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) : cbOutputDir.Text;
        if (!Directory.Exists(outputdir))
          Directory.CreateDirectory(outputdir);

        ytdlp = new ytdlp_process()
        {
          Url = mediaInfo.webpage_url,
          OutputPath = outputdir
        };

        if (!string.IsNullOrEmpty(OutputFileFormat.Text))
          ytdlp.OutputFile = OutputFileFormat.Text;

        var cookieKind = UseCookie.SelectedValue.ToString();

        if (cookieKind == "file" && !string.IsNullOrEmpty(CookiePath.Text) && File.Exists(CookiePath.Text))
          ytdlp.CookiePath = CookiePath.Text;
        else if (cookieKind != "none" && cookieKind != "file")
          ytdlp.CookieBrowser = cookieKind;

        ytdlp.StdOutReceived += data => Invoke(() => OutputStderr.Text = data);
        ytdlp.ProcessExited += (s, e) => Invoke(YtdlpPostDownload);

        if (chkAfterDownload.Checked)
        {
          ytdlp.StdOutReceived += YtdlpReceiver;
          ytdlp.ProcessExited += (s, e) => Invoke(() =>
          {
            Debug.WriteLine($"exitcode = {ytdlp.ExitCode},DownloadName = {DownloadFileName}");

            if (ytdlp.ExitCode == 0 && !string.IsNullOrEmpty(DownloadFileName))
            {
              FileListBindingSource.Add(new StringListItem(DownloadFileName));
              btnSubmitInvoke.Enabled = true;
            }
          });
        }

        if (IsOpenStderr.Checked)
        {
          form = new StdoutForm();

          Action<string> receiver = data =>
          {
            if (form.Pause)
              form.LogData.Add(data);
            else
              form.Invoke(form.WriteLine, [data]);
          };

          ytdlp.StdOutReceived += receiver;
          ytdlp.StdErrReceived += receiver;
          ytdlp.ProcessExited += (s,e) =>
          {
            form.Invoke(() =>
            {
              var button = form.Controls["BtnClose"] as Button;
              button.Enabled = true;
            });

            ytdlp = null;
          };

          form.Show();
        }

        if (separatedDownload)
        {
          await ytdlp.DownloadAsync(
            VideoOnlyFormat.SelectedValue.ToString(),
            AudioOnlyFormat.SelectedValue.ToString()
          );
        }
        else
        {
          await ytdlp.DownloadAsync(
            MovieFormat.SelectedValue.ToString()
          );
        }
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message,"エラー");
        if (form != null)
        {
          var button = form.Controls["BtnClose"] as Button;
          button.Enabled = true;
        }
      }
      finally
      {
        YtdlpPostDownload();
      }
    }

    private void YtdlpReceiver(string data)
    {
      var match = DownloadRegex.Match(data);
      if (match.Success)
      {
        DownloadFileName = match.Groups["filename"].Value;
      }
      else
      {
        match = MergerRegex.Match(data);
        if (match.Success)
        {
          DownloadFileName = match.Groups["filename"].Value;
          ytdlp.StdOutReceived -= YtdlpReceiver;
        }
      }
    }

    private void YtdlpAbortDownload()
    {
      if (ytdlp != null)
        ytdlp.Interrupt();
    }
  }
}
