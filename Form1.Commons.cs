﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ffmpeg_ytdlp_gui.libs;
using ffmpeg_ytdlp_gui.Properties;
using Microsoft.Toolkit.Uwp.Notifications;

namespace ffmpeg_ytdlp_gui
{
  partial class Form1
  {
    // Static members
    // ---------------------------------------------------------------------------------
    static private readonly string TOAST_ID = "9998";

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

    private static List<ManagementObject>? GpuDevices;
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
            device["AdapterCompatibility"].ToString() ?? "Unknown",
            device["Name"].ToString()
          )
        );
      }
      deviceList.Add(new StringListItem("cpu", "CPU(Software)"));
      return deviceList;
    }

    private static void ChangeCurrentDirectory(string? dir = null)
    {
      if (string.IsNullOrEmpty(dir))
        dir = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

      Environment.CurrentDirectory = Environment.ExpandEnvironmentVariables(dir);
    }

    private static void TooltipShow(Control control,string text,int delay = 5000)
    {
      var form = control.FindForm();
      var tooltip = new ToolTip();
      tooltip.SetToolTip(control, text);
      tooltip.AutomaticDelay = 10;
      tooltip.Show("正しいURLを入力してください。", control);
      control.Focus();
      Task.Delay(delay).ContinueWith(
        _ => form?.Invoke(() =>
        {
          tooltip.Hide(control);
          tooltip.Dispose();
        })
      );
    }

    // Instance members
    // ---------------------------------------------------------------------------------

    private void InitializeSettingsBinding()
    {
      BatExecWithConsole.DataBindings.Add("Checked", Settings.Default, "batExecWithConsole");
      ConfigDirectory.DataBindings.Add("Text", Settings.Default, "configDirectory");
      CookiePath.DataBindings.Add("Text", Settings.Default, "cookiePath");
      DeleteUrlAfterDownloaded.DataBindings.Add("Checked", Settings.Default, "deleteUrlAfterDownload");
      FrameRate.DataBindings.Add("Value", Settings.Default, "fps");
      FreeOptions.DataBindings.Add("Text", Settings.Default, "free");
      HideThumbnail.DataBindings.Add("Checked", Settings.Default, "hideThumbnail");
      ImageHeight.DataBindings.Add("Value", Settings.Default, "imageHeight");
      ImageWidth.DataBindings.Add("Value", Settings.Default, "imageWidth");
      IsOpenStderr.DataBindings.Add("Checked", Settings.Default, "openStderr");
      LookAhead.DataBindings.Add("Value", Settings.Default, "lookAhead");
      MaxListItems.DataBindings.Add("Value", Settings.Default, "maxListItems");
      Overwrite.DataBindings.Add("Checked", Settings.Default, "overwrite");
      PrimaryAudioFormatId.DataBindings.Add("Text", Settings.Default, "primaryAudioFormatId");
      PrimaryMovieFormatId.DataBindings.Add("Text", Settings.Default, "primaryMovieFormatId");
      PrimaryVideoFormatId.DataBindings.Add("Text", Settings.Default, "primaryVideoFormatId");
      TileColumns.DataBindings.Add("Value", Settings.Default, "tileColumns");
      TileRows.DataBindings.Add("Value", Settings.Default, "tileRows");
      UseCustomConfig.DataBindings.Add("Checked", Settings.Default, "useCustomConfig");
      chkAfterDownload.DataBindings.Add("Checked", Settings.Default, "downloadCompleted");
      chkConstantQuality.DataBindings.Add("Checked", Settings.Default, "cq");
      resizeTo.DataBindings.Add("Value", Settings.Default, "resizeTo");
      RemoveBatListAfterDone.DataBindings.Add("Checked", Settings.Default, "removeBatListAfterDone");

      SubmitConfigDirDlg.DataBindings.Add("Enabled", UseCustomConfig, "Checked");
      ConfigDirectory.DataBindings.Add("Enabled", UseCustomConfig, "Checked");
    }

    private void InitializeSettingsApply()
    {
      BatExecWithConsole.Checked = Settings.Default.batExecWithConsole;
      ConfigDirectory.Text = Settings.Default.configDirectory;
      CookiePath.Text = Settings.Default.cookiePath;
      DeleteUrlAfterDownloaded.Checked = Settings.Default.deleteUrlAfterDownload;
      FrameRate.Value = Settings.Default.fps;
      FreeOptions.Text = Settings.Default.free;
      HideThumbnail.Checked = Settings.Default.hideThumbnail;
      ImageHeight.Value = Settings.Default.imageHeight;
      ImageWidth.Value = Settings.Default.imageWidth;
      IsOpenStderr.Checked = Settings.Default.openStderr;
      LookAhead.Value = Settings.Default.lookAhead;
      MaxListItems.Value = Settings.Default.maxListItems;
      Overwrite.Checked = Settings.Default.overwrite;
      PrimaryAudioFormatId.Text = Settings.Default.primaryAudioFormatId;
      PrimaryMovieFormatId.Text = Settings.Default.primaryMovieFormatId;
      PrimaryVideoFormatId.Text = Settings.Default.primaryVideoFormatId;
      TileColumns.Value = Settings.Default.tileColumns;
      TileRows.Value = Settings.Default.tileRows;
      UseCustomConfig.Checked = Settings.Default.useCustomConfig;
      chkAfterDownload.Checked = Settings.Default.downloadCompleted;
      chkConstantQuality.Checked = Settings.Default.cq;
      resizeTo.Value = Settings.Default.resizeTo;
      RemoveBatListAfterDone.Checked = Settings.Default.removeBatListAfterDone;
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
            new CodecListItem(new Codec("none"),"不使用"),
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
            new CodecListItem(new Codec("none"),"不使用"),
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
            new CodecListItem(new Codec("none"),"不使用"),
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

      ///
      var set = new StringListItemsSet([],[],[],[-1,-1,-1]);
      cbOutputDir.DataSource = DirectoryListBindingSource;
      DirectoryListBindingSource.DataSource = set;

      // タブと連動して出力フォルダリストを変更
      DirectoryListBindingSource.DataMember = "Item1";
      OutputDirectoryList = set?.Item1 ?? throw new Exception("Item not initialize yet");
      ///

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

      UrlBindingSource.DataSource = new YtdlpItems();
      DownloadUrl.DataSource = UrlBindingSource;

      OutputFileFormatBindingSource.DataSource = new List<string>();
      OutputFileFormat.DataSource = OutputFileFormatBindingSource;

      Playlist.DataSource = PlaylistBindingSource;
    }

    private void InitializeToastNotify()
    {
      ToastNotificationManagerCompat.OnActivated += toastArgs =>
      {
        var args = ToastArguments.Parse(toastArgs.Argument);
        var pageName = args.Get("page");

        var pages = Controls.Find(pageName, true);
        if (pages.Length <= 0)
          return;

        var page = pages[0] as TabPage;
        if (page == null)
          return;

        Invoke(() => Tab.SelectedTab = page);
      };
    }

    private void ToastPush(string message,string? pageName = "PageConvert")
    {
      var lines = message.Split(['\n', '\r']);
      if (lines.Length > 1)
      {
        var title = lines.First();
        ToastPush(title, lines.Skip(1), pageName ?? "PageConvert");
        return;
      }

      new ToastContentBuilder()
        .AddArgument("page", pageName)
        .AddArgument("conversationId",TOAST_ID)
        .AddText(message, AdaptiveTextStyle.Title)
        .Show();
    }

    private void ToastPush(string title,IEnumerable<string> messages,string pageName = "pageConvert")
    {
      var toast = new ToastContentBuilder()
                    .AddArgument("page", pageName)
                    .AddArgument("conversationId",TOAST_ID)
                    .AddText(Text);

      foreach (var message in messages)
        toast.AddText(message);

      toast.Show();
    }

    private void WriteBatListStatus()
    {
      BatListCount.Text = $"Bat List: {BatchList?.Count ?? 0}";
    }

    /// <summary>
    /// ffmpegpプロセス起動前に実行
    /// </summary>
    /// <param name="command"></param>
    /// <param name="filename"></param>
    private void RuntimeSetting(ffmpeg_command? command, string filename)
    {
      if (command == null || string.IsNullOrEmpty(filename))
        return;

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

        if (stream != null)
        {
          command.IsLandscape = stream.width > stream.height;
          command.Width = (int?)stream.width!;
          command.Height = (int?)stream.height!;
        }
      }

      if (chkCrop.Checked)
      {
        if (chkUseHWDecoder.Checked && HWDecoder.SelectedValue!.ToString()!.EndsWith("_cuvid"))
          command
            .size(command.Width ?? 0, command.Height ?? 0)
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

    /// <summary>
    /// ffmpegプロセス
    /// </summary>
    private ffmpeg_process? Proceeding;
    private StdoutForm? ffmpegfm;

    private ffmpeg_process CreateFFmpegProcess(ffmpeg_command command,string tabpageName)
    {
      command.Overwrite = Overwrite.Checked;

      var process = new ffmpeg_process(command, FileListBindingSource);
      process.ReceiveData += data => Invoke(() => OutputStderr.Text = data);
      process.ProcessExit += filename => Invoke(() =>
      {
        var item = FileListBindingSource.OfType<StringListItem>().FirstOrDefault(item => item.Value == filename);
        if (item == null)
          return;

        FileListBindingSource.Remove(item);
        FileListBindingSource.ResetBindings(false);
      });

      process.ProcessesDone += abnormal => Invoke(() =>
      {
        OnEndFFmpegProcess();
        if (FileListBindingSource.Count > 0)
          btnSubmitInvoke.Enabled = true;

        Proceeding = null;
        if (!abnormal)
          ToastPush("変換処理が終了しました。",tabpageName);
      });

      if (IsOpenStderr.Checked)
      {
        StdoutForm? form;
        Action<string> dataReceiver = data =>
        {
          if (string.IsNullOrEmpty(data) || ffmpegfm == null)
            return;

          if (ffmpegfm.Pause)
            ffmpegfm.LogData.Add(data);
          else
            ffmpegfm.Invoke(() => ffmpegfm.WriteLine(data));
        };

        Action<bool> processDone = b =>
        {
          if (Proceeding == null)
          {
            ffmpegfm?.Invoke(ffmpegfm.OnProcessExit);
            ffmpegfm?.Release();
          }
        };

        if(ffmpegfm == null)
        {
          ffmpegfm = form = new StdoutForm();
          form.FormClosing += StdoutFormClosingAction;
          form.FormClosing += (s, e) => ffmpegfm = null;
          form.Load += StdoutFormLoadAction;

          form.CustomButton.Visible = true;
          form.CustomButton.Text = "出力を中断して閉じる";
          form.CustomButtonClick += (sender, e) =>
          {
            process.ReceiveData -= dataReceiver;
            process.ProcessesDone -= processDone;
            form.Release();
            form.Close();
            ffmpegfm = form = null;
          };
        }
        else
        {
          form = ffmpegfm;
          form.Lock();
          form.Pause = false;
        }

        process.ReceiveData += dataReceiver;
        process.ProcessesDone += processDone;
        
        if(!form.Visible)
          form.Show();
      }

      Proceeding = process;
      return process;
    }

    private ffmpeg_command CreateFFMpegCommandInstance()
    {
      ffmpeg_command ffcommand;

      var codec = UseVideoEncoder.SelectedValue as Codec ?? throw new NullReferenceException("UseVideoEncoder is null");
      if (codec.GpuSuffix == "nvenc")
        ffcommand = new ffmpeg_command_cuda(ffmpeg.Text);
      else if (codec.GpuSuffix == "qsv")
        ffcommand = new ffmpeg_command_qsv(ffmpeg.Text);
      else if (codec.Name != "copy" && codec.GpuSuffix == "cpu")
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
        .OutputContainer(FileContainer.SelectedValue?.ToString() ?? "mp4");

      if (chkEncodeAudio.Checked)
        ffcommand.acodec(UseAudioEncoder.SelectedValue?.ToString() ?? "").aBitrate((int)aBitrate.Value);
      else
        ffcommand.acodec("copy").aBitrate(0);

      if (!string.IsNullOrEmpty(txtSS.Text))
        ffcommand.Starts(txtSS.Text);
      if (!string.IsNullOrEmpty(txtTo.Text))
        ffcommand.To(txtTo.Text);

      if (InputFileList!.Count > 1)
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

      if (InputFileList!.Count > 1)
        ffcommand.MultiFileProcess = true;

      var codec = UseVideoEncoder.SelectedValue as Codec;
      if (codec!.Name == "copy")
      {
        ffcommand
          .OutputDirectory(cbOutputDir.Text)
          .OutputPrefix(FilePrefix.Text)
          .OutputSuffix(FileSuffix.Text)
          .OutputContainer(FileContainer.SelectedValue?.ToString() ?? "");

        if (chkEncodeAudio.Checked)
          ffcommand.acodec(UseAudioEncoder.SelectedValue?.ToString()!).aBitrate((int)aBitrate.Value);
        else
          ffcommand.acodec("copy").aBitrate(0);

        return ffcommand;
      }

      ffcommand.vcodec(
        UseVideoEncoder.SelectedValue?.ToString()!,
        cbDevices.Items.Count > 1 ? cbDevices.SelectedIndex : 0
      );

      if (codec.Name != "gif")
      {
        ffcommand
          .vBitrate((int)vBitrate.Value, chkConstantQuality.Checked)
          .lookAhead((int)LookAhead.Value)
          .preset(cbPreset.SelectedValue?.ToString()!);
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
        ffcommand.hwdecoder(HWDecoder.SelectedValue?.ToString()!);

      if (codec.Name == "gif")
        ffcommand.acodec(null);
      else if (chkEncodeAudio.Checked)
        ffcommand.acodec(UseAudioEncoder.SelectedValue?.ToString()!).aBitrate((int)aBitrate.Value);
      else
        ffcommand.acodec("copy").aBitrate(0);

      ffcommand
        .OutputDirectory(cbOutputDir.Text)
        .OutputPrefix(FilePrefix.Text)
        .OutputSuffix(FileSuffix.Text)
        .OutputContainer(FileContainer.SelectedValue?.ToString()!);

      var deinterlaces = new List<string>() { "bwdif", "yadif", "bob", "adaptive" };

      if (chkFilterDeInterlace.Checked)
      {
        string value = cbDeinterlaceAlg.SelectedValue?.ToString()!;
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

      var tag = int.Parse(GetCheckedRadioButton(ResizeBox)?.Tag as string ?? "");
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

      var rotate = int.Parse(GetCheckedRadioButton(RotateBox)?.Tag?.ToString()!);
      if (rotate == 0)
        ffcommand.removeFilter("transpose");
      else
        ffcommand.setFilter("transpose", rotate.ToString());

      if (!string.IsNullOrEmpty(FreeOptions.Text))
        ffcommand.setOptions(FreeOptions.Text);

      return ffcommand;
    }

    private static RadioButton? GetCheckedRadioButton(GroupBox groupBox)
    {
      return groupBox.Controls.OfType<RadioButton>().FirstOrDefault(radio => radio.Checked);
    }

    private void StopCurrentProcess() => StopProcess(false);
    private void StopAllProcess() => StopProcess(true);

    private void StopProcess(bool stopAll = false)
    {
      if (stopAll)
        FileListBindingSource.Clear();

      Proceeding?.Kill(stopAll);
    }


    private StdoutForm? OpenOutputView(string executable, string arg, string formTitle = "Output viewer")
    {
      return OpenOutputView(executable, arg, null, formTitle);
    }

    private StdoutForm? OpenOutputView(string executable, string arg, Encoding? outputEncoding, string formTitle = "Output viewer")
    {
      if (!File.Exists(executable))
      {
        var exepathes = CustomProcess.FindInPath(executable);
        if (exepathes.Length <= 0)
          return null;
      }

      var form = new StdoutForm(executable, arg, formTitle);
      form.Load += StdoutFormLoadAction;
      form.Load += (s, e) => form.HidePause();
      form.FormClosing += StdoutFormClosingAction;
      form.Shown += (s, e) =>
      {
        form.Lock();
        form.Pause = false;
        form.Redirected?.Start();
      };

      if (form.Redirected != null && outputEncoding != null)
      {
        form.Redirected.StdOutEncoding = outputEncoding;
        form.Redirected.StdErrEncoding = outputEncoding;
      }

      return form;
    }

    private void InitPresetAndDevice(Codec codec)
    {
      if (!PresetList!.ContainsKey(codec.FullName))
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

      if (chkConstantQuality.Checked)
      {
        vQualityLabel.Text = codec.GpuSuffix switch
        {
          "qsv" => "ICQ",
          "nvenc" => "-cq",
          _ => "-crf",
        };
      }
    }

    private void OnBeginFFmpegProcess()
    {
      btnStop.Enabled = btnStopAll.Enabled = btnStopUtil.Enabled = btnStopAllUtil.Enabled = true;
      OpenLogFile.Enabled = false;

      AddDirectoryListItem();
    }

    private void OnEndFFmpegProcess()
    {
      btnStop.Enabled = btnStopAll.Enabled = btnStopUtil.Enabled = btnStopAllUtil.Enabled = false;
      OpenLogFile.Enabled = true;
    }

    private void AddDirectoryListItem()
    {
      if (cbOutputDir.SelectedIndex < 0 && !OutputDirectoryList!.Any(item => item.Value == cbOutputDir.Text))
        cbOutputDir.SelectedIndex = DirectoryListBindingSource.Add(new StringListItem(cbOutputDir.Text, DateTime.Now));
    }

    private void SetDownloadFormats(YtdlpItem? ytdlpItem)
    {
      if (ytdlpItem == null)
        return;

      var mi = ytdlpItem?.Item2;
      var image = ytdlpItem?.Item3;

      string time = mi?.GetDurationTime() ?? "--:--:--";

      if (HideThumbnail.Checked == false)
      {
        ThumbnailBox.ContextMenuStrip = ImageContextMenu;
        ThumbnailBox.Image = ytdlpItem?.Item3;
      }
      else
      {
        ThumbnailBox.ContextMenuStrip = null;
        ThumbnailBox.Image = null;
      }
      DurationTime.Text = time;
      DurationTime.Visible = true;

      MediaTitle.Text = mi?.title;

      // format_id 構築
      VideoOnlyFormatSource.Clear();
      VideoOnlyFormatSource.Add(new StringListItem(string.Empty, "使用しない"));
      AudioOnlyFormatSource.Clear();
      AudioOnlyFormatSource.Add(new StringListItem(string.Empty, "使用しない"));
      MovieFormatSource.Clear();

      foreach (var format in mi?.formats!)
      {
        if (format == null || format.format_id == null)
          continue;

        if (format.vcodec == "none" && format.acodec != "none")
          AudioOnlyFormatSource.Add(new StringListItem(format.format_id, format.ToString()));
        else if (format.acodec == "none" && format.vcodec != "none")
          VideoOnlyFormatSource.Add(new StringListItem(format.format_id, format.ToString()));
        else if (format.acodec != "none" && format.vcodec != "none")
          MovieFormatSource.Add(new StringListItem(format.format_id, format.ToString()));
      }

      MovieFormat.SelectedIndex = MovieFormatSource.Count - 1;

      // requested_formats? があれば
      if (mi.requested_formats!.Count > 0)
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
          ComboBox? cb = null;
          if (item.Video)
            cb = VideoOnlyFormat;
          else if (item.Audio)
            cb = AudioOnlyFormat;
          else
            continue;

          cb.SelectedValue = item.Value;
        }
      }

      Func<string,IEnumerable<string>> Split = src => Regex.Split(src, @"[,;:\s]+").Where(str => !string.IsNullOrEmpty(str));

      // 優先するフォーマットに選択しなおし。
      if (!string.IsNullOrEmpty(PrimaryVideoFormatId.Text))
      {
        foreach (var div in Split(PrimaryVideoFormatId.Text))
        {
          var selectedItem = VideoOnlyFormatSource.List.Cast<StringListItem>().FirstOrDefault(item => item.Value == div);
          if (selectedItem != null)
          {
            VideoOnlyFormat.SelectedItem = selectedItem;
            break;
          }
        }
      }

      if (!string.IsNullOrEmpty(PrimaryAudioFormatId.Text))
      {
        foreach (var div in Split(PrimaryAudioFormatId.Text))
        {
          var selectedItem = AudioOnlyFormatSource.List.Cast<StringListItem>().FirstOrDefault(item => item.Value == div);
          if (selectedItem != null)
          {
            AudioOnlyFormat.SelectedItem = selectedItem;
            break;
          }
        }
      }

      if (!string.IsNullOrEmpty(PrimaryMovieFormatId.Text))
      {
        foreach (var div in Split(PrimaryMovieFormatId.Text))
        {
          var selectedItem = MovieFormatSource.List.Cast<StringListItem>().FirstOrDefault(item => item.Value == div);
          if (selectedItem != null)
          {
            MovieFormat.SelectedItem = selectedItem;
            break;
          }
        }
      }

      AddDownloadQueue.Enabled = true;
      SubmitDownloadDequeue.Enabled = true;
    }
  }
}
