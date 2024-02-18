using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ffmpeg_command_builder
{
  partial class Form1 : Form
  {
    private Process CurrentProcess;
    private Queue<string> inputFileList;
    Action<string> UpdateControl;
    Action<string> WriteOutput;
    Action ProcessDoneCallback;

    private void InitializeMembers()
    {
      inputFileList = new Queue<string>();

      WriteOutput = output => { OutputProcess.Text = output; };
      UpdateControl = f =>
      {
        FileList.Items.Remove(f);
        CurrentFileName.Text = f;
      };
      ProcessDoneCallback = () =>
      {
        CurrentFileName.Text = "";
        btnStop.Enabled = btnStopAll.Enabled = false;
        if(inputFileList.Count > 0)
          btnSubmitInvoke.Enabled = true;

        MessageBox.Show("変換処理が終了しました。");
      };
    }

    private ffmpeg_command CreateAudioCommand()
    {
      var ffcommand = new ffmpeg_command();

      ffcommand.audioOnly(true);

      if (chkEncodeAudio.Checked)
        ffcommand.acodec(UseAudioEncoder.Text).aBitrate(192);
      else
        ffcommand.acodec("copy").aBitrate(0);

      if (!string.IsNullOrEmpty(txtSS.Text))
        ffcommand.Starts(txtSS.Text);
      if (!string.IsNullOrEmpty(txtTo.Text))
        ffcommand.To(txtTo.Text);

      ffcommand.outputPath(cbOutputDir.Text);

      return ffcommand; 
    }

    private ffmpeg_command CreateCommand(bool isAudioOnly = false)
    {
      if (isAudioOnly)
        return CreateAudioCommand();

      var ffcommand = new ffmpeg_command();

      if (!string.IsNullOrEmpty(txtSS.Text))
        ffcommand.Starts(txtSS.Text);
      if (!string.IsNullOrEmpty(txtTo.Text))
        ffcommand.To(txtTo.Text);

      ffcommand.vcodec(UseVideoEncoder.Text);
      ffcommand.vBitrate((int)bitrate.Value, chkConstantQuality.Checked);

      if (chkEncodeAudio.Checked)
        ffcommand.acodec(UseAudioEncoder.Text).aBitrate(192);
      else
        ffcommand.acodec("copy").aBitrate(0);

      ffcommand.outputPath(cbOutputDir.Text);

      if (chkFilterDeInterlace.Checked)
        ffcommand.setFilter("bwdif_cuda", "0:-1:0");
      else
        ffcommand.removeFilter("bwdif_cuda");

      if (rbResizeNone.Checked)
        ffcommand.removeFilter("scale_cuda");
      else if (rbResizeFullHD.Checked)
        ffcommand.setFilter("scale_cuda", rbLandscape.Checked ? $"-2:{(string)rbResizeFullHD.Tag}" : $"{(string)rbResizeFullHD.Tag}:-2");
      else if (rbResizeHD.Checked)
        ffcommand.setFilter("scale_cuda", rbLandscape.Checked ? $"-2:{(string)rbResizeHD.Tag}" : $"{(string)rbResizeHD.Tag}:-2");

      if (rbRotateNone.Checked)
        ffcommand.removeFilter("transpose");
      else if (rbRotateRight.Checked)
        ffcommand.setFilter("transpose", "1");
      else if (rbRotateLeft.Checked)
        ffcommand.setFilter("transpose", "2");

      return ffcommand;
    }

    private void BeginFFmpegProcess()
    {
      string filename = inputFileList.Dequeue();
      try
      {
        var process = currentCommand.InvokeCommand(filename, true);

        Invoke(UpdateControl, new object[] { filename });

        process.Exited += Process_Exited;
        process.ErrorDataReceived += new DataReceivedEventHandler(StdErrRead);
        process.Start();
        process.BeginErrorReadLine();

        CurrentProcess = process;
      }
      catch(Exception e)
      {
        MessageBox.Show(e.Message);
        Process_Exited(null, null);
      }
    }

    private void StdErrRead(object sender, DataReceivedEventArgs e)
    {
      if (!String.IsNullOrEmpty(e.Data))
        Invoke(WriteOutput, new Object[] { e.Data });
    }

    private void Process_Exited(object sender, EventArgs e)
    {
      try
      {
        CurrentProcess = null;
        BeginFFmpegProcess();
      }
      catch (InvalidOperationException) {
        Invoke(ProcessDoneCallback);
      }
    }

    private void StopProcess(bool stopAll = false)
    {
      if (stopAll)
      {
        inputFileList.Clear();
        FileList.Items.Clear();
      }

      if (CurrentProcess == null || CurrentProcess.HasExited)
        return;

      using (StreamWriter sw = CurrentProcess.StandardInput)
      {
        sw.Write('q');
      }
    }
  }
}
