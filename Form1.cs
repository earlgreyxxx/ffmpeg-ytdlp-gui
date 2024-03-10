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
  public partial class Form1 : Form
  {
    private ffmpeg_command currentCommand;
    Dictionary<string, string[]> Presets;
    Dictionary<string, bool> HardwareEncoders;

    public Form1()
    {
      InitializeComponent();
      InitializeMembers();

      Presets = new Dictionary<string, string[]>()
      {
        { "h264_nvenc", new string[] { "default","slow","medium","fast","hp","hq","bd","ll","llhq","llhp","lossless","losslesshp","p1","p2","p3","p4","p5","p6","p7" } },
        { "hevc_nvenc", new string[] { "default","slow","medium","fast","hp","hq","bd","ll","llhq","llhp","lossless","losslesshp","p1","p2","p3","p4","p5","p6","p7" } },
        { "h264_qsv",new string[] { "veryfast","faster","fast","medium","slow","slower","veryslow" } },
        { "hevc_qsv",new string[] { "veryfast","faster","fast","medium","slow","slower","veryslow" } },
      };

      HardwareEncoders = new Dictionary<string,bool>()
      {
        { "hevc_nvenc",false},
        { "hevc_qsv",false},
        { "h264_nvenc",false},
        { "h264_qsv",false}
      };

      foreach (string name in cbDevices.Items)
      {
        if (Regex.IsMatch(name,"^intel",RegexOptions.IgnoreCase))
        {
          HardwareEncoders["hevc_qsv"] = true;
          HardwareEncoders["h264_qsv"] = true;
        }
        else if (Regex.IsMatch(name,"^nvidia",RegexOptions.IgnoreCase))
        {
          HardwareEncoders["hevc_nvenc"] = true;
          HardwareEncoders["h264_nvenc"] = true;
        }
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      ActiveControl = null;
      var folders = Settings.Default.outputFolders;
      if(folders != null && folders.Count > 0)
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

      var encoderNames = HardwareEncoders.Where(kv => kv.Value).Select(kv => kv.Key);
      foreach (string encoderName in encoderNames)
        UseVideoEncoder.Items.Add(encoderName);

      UseVideoEncoder.SelectedIndex = 0;
      UseAudioEncoder.SelectedIndex = 0;

      UseAudioEncoder.Enabled = chkEncodeAudio.Checked;

      CurrentFileName.Text = string.Empty;

      cbDeinterlaceAlg.SelectedIndex = 0;

      chkConstantQuality.Checked = Settings.Default.cq;
      aBitrate.Enabled = chkEncodeAudio.Checked;
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
        if (!string.IsNullOrEmpty(item))
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

      if (inputFileList != null && inputFileList.Count > 0)
      {
        btnSubmitInvoke.Enabled = false;
        currentCommand = CreateCommand(chkAudioOnly.Checked);

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
        vQualityLabel.Text = UseVideoEncoder.Text.EndsWith("_qsv") ? "ICQ" : "-cq";
      else
        vQualityLabel.Text = "-b:v";
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
      ResizeBox.Enabled = RotateBox.Enabled = LayoutBox.Enabled = !chkAudioOnly.Checked;

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

      var commandlines = new List<string>();
      string[] dragFilePathArr = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      foreach (var filePath in dragFilePathArr)
      {
        inputFileList.Enqueue(filePath);
        FileList.Items.Add(filePath);
      }

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
      {
        inputFileList.Enqueue(filename);
        FileList.Items.Add(filename);
      }

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

      if (inputFileList != null && inputFileList.Count > 0)
      {
        if (DialogResult.Cancel == findSaveBatchFile.ShowDialog())
          return;

        currentCommand = CreateCommand(chkAudioOnly.Checked);
        currentCommand.ToBatchFile(findSaveBatchFile.FileName, inputFileList);
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
      foreach (var path in ffmpeg.Items.Cast<string>().Where(item => !File.Exists(item)))
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
      if (!File.Exists(filename))
      {
        MessageBox.Show("ログファイルが存在しません。");
        return;
      }

      Process.Start(filename);
    }

    private void ClearFileList_Click(object sender, EventArgs e)
    {
      inputFileList.Clear();
      FileList.Items.Clear();
    }

    private void chkFilterDeInterlace_CheckedChanged(object sender, EventArgs e)
    {
      cbDeinterlaceAlg.Enabled = chkFilterDeInterlace.Checked;
    }
  }
}
