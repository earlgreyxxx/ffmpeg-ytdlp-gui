using ffmpeg_command_builder.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ffmpeg_command_builder
{
  using StringListItem = ListItem<string>;
  using CodecListItem = ListItem<Codec>;
  using StringListItems = List<ListItem<string>>;
  using CodecListItems = List<ListItem<Codec>>;

  public partial class Form1 : Form
  {
    private ffmpeg_command currentCommand;
    private Dictionary<string, StringListItems> PresetList;
    private Dictionary<string, CodecListItems> HardwareDecoders;
    private StringListItems DeInterlaces;
    private CodecListItems HardwareEncoders;
    private CodecListItems AudioEncoders;
    private StringListItems InputFileList;

    public Form1()
    {
      InitializeComponent();

      var nvencPresetList = new StringListItems()
      {
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
      };
      var qsvPresetList = new StringListItems()
      {
        new StringListItem("veryfast"),
        new StringListItem("faster"),
        new StringListItem("fast"),
        new StringListItem("medium"),
        new StringListItem("slow"),
        new StringListItem("slower"),
        new StringListItem("veryslow")
      };

      PresetList = new Dictionary<string, StringListItems>()
      {
        { "h264_nvenc", nvencPresetList },
        { "hevc_nvenc", nvencPresetList },
        { "h264_qsv",qsvPresetList },
        { "hevc_qsv",qsvPresetList }
      };

      HardwareDecoders = new Dictionary<string, CodecListItems>()
      {
        { 
          "nvidia",
          new CodecListItems()
          {
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
          }
        },
        {
          "intel",
          new CodecListItems()
          {
            new CodecListItem(new Codec("vp9","qsv"),"VP9"),
            new CodecListItem(new Codec("h264","qsv"),"H264"),
            new CodecListItem(new Codec("hevc","qsv"),"HEVC"),
            new CodecListItem(new Codec("mpeg2","qsv"),"MPEG2"),
            new CodecListItem(new Codec("mjpeg","qsv"),"MJPEG"),
            new CodecListItem(new Codec("vc1","qsv"),"VC1"),
            new CodecListItem(new Codec("vp8","qsv"),"VP8"),
            new CodecListItem(new Codec("av1","qsv"),"AV1")
          }
        }
      };

      DeInterlaces = new StringListItems()
      {
        new StringListItem("1:-1:0","bwdif"),
        new StringListItem("2:-1:0","yadif")
      };

      AudioEncoders = new CodecListItems()
      {
        new CodecListItem(new Codec("aac")),
        new CodecListItem(new Codec("libmp3lame"))
      };

      var GpuDeviceList = GetGPUDeviceList();

      cbDevices.DataSource = GpuDeviceList;
      cbDevices.SelectedIndex = 0;

      HardwareEncoders = new CodecListItems();
      foreach(var device in GpuDeviceList)
      {
        if (Regex.IsMatch(device.Value,"^intel",RegexOptions.IgnoreCase))
        {
          HardwareEncoders.Add(new CodecListItem(new Codec("hevc", "qsv")));
          HardwareEncoders.Add(new CodecListItem(new Codec("h264", "qsv")));
        }
        else if (Regex.IsMatch(device.Value,"^nvidia",RegexOptions.IgnoreCase))
        {
          HardwareEncoders.Add(new CodecListItem(new Codec("hevc", "nvenc")));
          HardwareEncoders.Add(new CodecListItem(new Codec("h264", "nvenc")));
        }
      }

      InputFileList = new StringListItems();
      InitializeMembers();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      ActiveControl = ffmpeg;
      var folders = Settings.Default.outputFolders;
      if (folders != null && folders.Count > 0)
      {
        foreach (string item in folders)
        {
          if (cbOutputDir.Items.Contains(item) || !Directory.Exists(item))
            continue;

          cbOutputDir.Items.Add(item);
        }
      }

      if (Settings.Default.ffmpeg?.Count > 0)
      {
        foreach (string item in Settings.Default.ffmpeg)
          ffmpeg.Items.Add(item);

        ffmpeg.SelectedIndex = 0;
      }

      rbResizeNone.Checked = Settings.Default.resizeNone;
      rbResizeFullHD.Checked = Settings.Default.resizeFullHD;
      rbResizeHD.Checked = Settings.Default.resizeHD;
      rbResizeNum.Checked = Settings.Default.resizeNum;

      vUnit.Text = chkConstantQuality.Checked ? "" : "Kbps";

      UseVideoEncoder.DataSource = HardwareEncoders;
      UseVideoEncoder.SelectedIndex = 0;

      UseAudioEncoder.DataSource = AudioEncoders;
      UseAudioEncoder.SelectedIndex = 0;
      UseAudioEncoder.Enabled = chkEncodeAudio.Checked;

      chkConstantQuality.Checked = Settings.Default.cq;
      aBitrate.Enabled = chkEncodeAudio.Checked;
      OutputStderr.Text = "";

      FileName.SelectedIndex = 0;

      chkCrop_CheckedChanged(null, null);
      cbDevices_SelectedIndexChanged(null, null);

      cbDeinterlaceAlg.DataSource = DeInterlaces;

      FileListBindingSource.DataSource = InputFileList;
      FileList.DataSource = FileListBindingSource; 
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.outputFolders = new StringCollection();
      Settings.Default.ffmpeg = new StringCollection();

      foreach (string item in cbOutputDir.Items)
        Settings.Default.outputFolders.Add(item);

      Settings.Default.cq = chkConstantQuality.Checked;
      Settings.Default.resizeNone = rbResizeNone.Checked;
      Settings.Default.resizeFullHD = rbResizeFullHD.Checked;
      Settings.Default.resizeHD = rbResizeHD.Checked;
      Settings.Default.resizeNum = rbResizeNum.Checked;

      foreach (string item in ffmpeg.Items)
      {
        if (!string.IsNullOrEmpty(item) && !Settings.Default.ffmpeg.Contains(item))
          Settings.Default.ffmpeg.Add(item);
      }

      Settings.Default.Save();
    }

    private void btnClearDirs_Click(object sender, EventArgs e)
    {
      cbOutputDir.Items.Clear();
    }

    private void btnSubmitInvoke_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(cbOutputDir.Text) || !Directory.Exists(cbOutputDir.Text))
      {
        MessageBox.Show("存在しない出力先フォルダが指定されました。");
        return;
      }

      if (!cbOutputDir.Items.Contains(cbOutputDir.Text))
        cbOutputDir.Items.Add(cbOutputDir.Text);

      if (chkCrop.Checked && chkUseHWDecoder.Checked && (VideoWidth.Value <= 0 || VideoHeight.Value <= 0))
      {
        MessageBox.Show("ハードウェアデコーダーでクロップを行う場合は、動画のサイズを指定する必要があります。");
        VideoWidth.Focus();
        return;
      }

      if (InputFileList != null && InputFileList.Count > 0)
      {
        btnSubmitInvoke.Enabled = false;
        currentCommand = CreateCommand(chkAudioOnly.Checked);
        if (FileName.Text.Trim() != "元ファイル名")
          currentCommand.OutputBaseName(FileName.Text);

        btnStop.Enabled = btnStopAll.Enabled = true;
        OpenLogFile.Enabled = false;
        OpenLogWriter();
        BeginFFmpegProcess();
      }
    }

    private void btnSubmitOpenDlg_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(cbOutputDir.Text) && Directory.Exists(cbOutputDir.Text))
        findFolder.SelectedPath = cbOutputDir.Text;

      if (DialogResult.OK == findFolder.ShowDialog())
        cbOutputDir.Text = findFolder.SelectedPath;
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
      if (!string.IsNullOrEmpty(cbOutputDir.Text) && Directory.Exists(cbOutputDir.Text) && !cbOutputDir.Items.Contains(cbOutputDir.Text))
        cbOutputDir.Items.Add(cbOutputDir.Text);

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
      txtSS.Text = "";
    }

    private void btnClearTo_Click(object sender, EventArgs e)
    {
      txtTo.Text = "";
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
      UseVideoEncoder.Enabled = cbPreset.Enabled = vBitrate.Enabled = chkConstantQuality.Enabled = !chkAudioOnly.Checked;
      chkFilterDeInterlace.Enabled = !chkAudioOnly.Checked;
      CropBox.Enabled = ResizeBox.Enabled = RotateBox.Enabled = LayoutBox.Enabled = !chkAudioOnly.Checked;

      if (chkAudioOnly.Checked)
      {
        cbDeinterlaceAlg.Enabled = false;
      }
      else
      {
        if(chkFilterDeInterlace.Checked)
          cbDeinterlaceAlg.Enabled = true;
        else
          cbDeinterlaceAlg.Enabled = false;
      }

      if (chkAudioOnly.Checked && !chkEncodeAudio.Checked)
      {
        chkEncodeAudio.Checked = true;
        aBitrate.Enabled = true;
      }

      if(chkAudioOnly.Checked && !UseAudioEncoder.Enabled)
        UseAudioEncoder.Enabled = true;
      else if (!chkAudioOnly.Checked && !chkEncodeAudio.Checked)
        UseAudioEncoder.Enabled = false;
    }

    private void DropArea_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        return;

      string[] dragFilePathArr = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      foreach (var filePath in dragFilePathArr)
        FileListBindingSource.Add(new StringListItem(filePath));

      FileListBindingSource.ResetBindings(false);

      btnSubmitInvoke.Enabled = true;
    }

    private void DropArea_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.Copy;
    }

    private void OpenFolder_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(cbOutputDir.Text))
        Process.Start(cbOutputDir.Text);
    }

    private void DropArea_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if(DialogResult.Cancel == openInputFile.ShowDialog())
        return;

      foreach(var filename in openInputFile.FileNames)
        FileListBindingSource.Add(new StringListItem(filename));

      btnSubmitInvoke.Enabled = true;
    }

    private void btnSubmitSaveToFile_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(cbOutputDir.Text) || !Directory.Exists(cbOutputDir.Text))
      {
        MessageBox.Show("存在しない出力先フォルダが指定されました。");
        return;
      }

      if (!cbOutputDir.Items.Contains(cbOutputDir.Text))
        cbOutputDir.Items.Add(cbOutputDir.Text);

      if (chkCrop.Checked && chkUseHWDecoder.Checked && (VideoWidth.Value <= 0 || VideoHeight.Value <= 0))
      {
        MessageBox.Show("ハードウェアデコーダーでクロップを行う場合は、動画のサイズを指定する必要があります。");
        VideoWidth.Focus();
        return;
      }

      if (InputFileList != null && InputFileList.Count > 0)
      {
        if (DialogResult.Cancel == findSaveBatchFile.ShowDialog())
          return;

        var command = CreateCommand(chkAudioOnly.Checked);
        if (FileName.Text.Trim() != "元ファイル名")
          command.OutputBaseName(FileName.Text);

        command.ToBatchFile(findSaveBatchFile.FileName, InputFileList.Select(item => item.Value));
      }
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
      StopProcess();
    }

    private void btnStopAll_Click(object sender, EventArgs e)
    {
      FileList.Items.Clear();
      StopProcess(true);
    }

    private void UseVideoEncoder_SelectedIndexChanged(object sender, EventArgs e)
    {
      InitPresetAndDevice();
    }

    private void btnFFmpeg_Click(object sender, EventArgs e)
    {
      if (DialogResult.Cancel == openFFMpegFileDlg.ShowDialog())
        return;

      ffmpeg.Text = openFFMpegFileDlg.FileName;
      if(!ffmpeg.Items.Contains(ffmpeg.Text))
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
          Process.Start("winget install -id Gyan.FFmpeg");

        ffmpeg.Text = string.Empty;
      }
      else
      {
        foreach (var ffmpegPath in ffmpegPathes.Reverse())
        {
          if(!ffmpeg.Items.Contains(ffmpegPath))
            ffmpeg.Items.Insert(0, ffmpegPath);
        }

        ffmpeg.SelectedIndex = 0;
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("https://ffmpeg.org/ffmpeg-utils.html#time-duration-syntax");
    }

    private void rbResizeNum_CheckedChanged(object sender, EventArgs e)
    {
      resizeTo.Enabled = rbResizeNum.Checked;
    }

    private void OpenLogFile_Click(object sender, EventArgs e)
    {
      var filename = GetLogFileName();
      if (!System.IO.File.Exists(filename))
      {
        MessageBox.Show("ログファイルが存在しません。");
        return;
      }

      Process.Start(filename);
    }

    private void ClearFileList_Click(object sender, EventArgs e)
    {
      InputFileList.Clear();
      FileListBindingSource.ResetBindings(false);
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
      var m = Regex.Match(cbDevices.Text, "^(Intel|Nvidia)", RegexOptions.IgnoreCase);
      if(m.Success)
      {
        var decodersItems = HardwareDecoders[m.Groups[1].Value.ToLower()];
        HWDecoder.DataSource = decodersItems;
      }
    }

    private void chkUseHWDecoder_CheckedChanged(object sender, EventArgs e)
    {
      var codec = HWDecoder.SelectedValue as Codec;
      VideoWidth.Enabled = VideoHeight.Enabled = chkCrop.Checked && chkUseHWDecoder.Checked && codec.GpuSuffix == "cuvid";
      HWDecoder.Enabled = chkUseHWDecoder.Checked;
    }
  }
}
