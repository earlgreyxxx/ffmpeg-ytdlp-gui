using ffmpeg_command_builder.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ffmpeg_command_builder
{
  public partial class Form1 : Form
  {
    private ffmpeg_command currentCommand;

    public Form1()
    {
      InitializeComponent();
      InitializeMembers();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      ActiveControl = this.Commandlines;
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

      chkConstantQuality.Checked = Settings.Default.cq;
      rbResizeNone.Checked = Settings.Default.resizeNone;
      rbResizeFullHD.Checked = Settings.Default.resizeFullHD;
      rbResizeHD.Checked = Settings.Default.resizeHD; 

      Unit.Text = chkConstantQuality.Checked ? "" : "Kbps";
      UseVideoEncoder.SelectedIndex = 0;
      UseAudioEncoder.SelectedIndex = 0;

      UseAudioEncoder.Enabled = chkEncodeAudio.Checked;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.outputFolders = new System.Collections.Specialized.StringCollection();

      foreach (string item in cbOutputDir.Items)
        Settings.Default.outputFolders.Add(item);

      Settings.Default.cq = chkConstantQuality.Checked;
      Settings.Default.resizeNone = rbResizeNone.Checked;
      Settings.Default.resizeFullHD = rbResizeFullHD.Checked;
      Settings.Default.resizeHD = rbResizeHD.Checked;

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
        BeginFFmpegProcess();
      }
    }

    private void btnSubmitOpenDlg_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(cbOutputDir.Text) && Directory.Exists(cbOutputDir.Text))
        findFolder.SelectedPath = cbOutputDir.Text;

      if (DialogResult.OK == findFolder.ShowDialog())
      {
        cbOutputDir.Text = findFolder.SelectedPath;
      }
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
      bitrate.Increment = chkConstantQuality.Checked ? 1 : 100;
      bitrate.Minimum = chkConstantQuality.Checked ? 0 : 100;
      bitrate.Maximum = chkConstantQuality.Checked ? 100 : 1000000;
      bitrate.Value = chkConstantQuality.Checked ? 25 : 6000;

      Unit.Text = chkConstantQuality.Checked ? "" : "KBps";
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
    }

    private void chkAudioOnly_CheckedChanged(object sender, EventArgs e)
    {
      UseVideoEncoder.Enabled = !chkAudioOnly.Checked;
      if (chkAudioOnly.Checked && !chkEncodeAudio.Checked)
        chkEncodeAudio.Checked = true;

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
        var commandlines = inputFileList.Select(file => currentCommand.GetCommandLine(file));

        using (var sw = new StreamWriter(findSaveBatchFile.FileName, false, Encoding.GetEncoding(932)))
        {
          sw.WriteLine("@ECHO OFF");
          foreach (var commandline in commandlines)
            sw.WriteLine(commandline);

          sw.WriteLine("PAUSE");
        }
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
  }
}
