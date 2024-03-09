using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
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
    private StreamWriter LogWriter;
    private readonly Encoding CP932 = Encoding.GetEncoding(932);

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
        OpenLogFile.Enabled = true;

        if (inputFileList.Count > 0)
          btnSubmitInvoke.Enabled = true;

        MessageBox.Show("変換処理が終了しました。");
      };

      foreach(var device in GetGPUDevices())
        cbDevices.Items.Add(device);

      cbDevices.SelectedIndex = 0;
    }

    private ffmpeg_command CreateFFMpegCommandInstance()
    {
      ffmpeg_command ffcommand = null;
      
      if(UseVideoEncoder.Text.EndsWith("_nvenc"))
        ffcommand = new ffmpeg_command_cuda(ffmpeg.Text);
      else if(UseVideoEncoder.Text.EndsWith("_qsv"))
        ffcommand = new ffmpeg_command_qsv(ffmpeg.Text);

      return ffcommand;
    }

    private ffmpeg_command CreateAudioCommand()
    {
      var ffcommand = CreateFFMpegCommandInstance().audioOnly(true);

      ffcommand
        .audioOnly(true)
        .OutputPrefix(FilePrefix.Text)
        .OutputSuffix(FileSuffix.Text);

      if (chkEncodeAudio.Checked)
        ffcommand.acodec(UseAudioEncoder.Text).aBitrate((int)aBitrate.Value);
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

      var ffcommand = CreateFFMpegCommandInstance();

      if (!string.IsNullOrEmpty(txtSS.Text))
        ffcommand.Starts(txtSS.Text);
      if (!string.IsNullOrEmpty(txtTo.Text))
        ffcommand.To(txtTo.Text);

      ffcommand
        .vcodec(UseVideoEncoder.Text, cbDevices.Items.Count > 1 ? cbDevices.SelectedIndex : 0)
        .vBitrate((int)vBitrate.Value, chkConstantQuality.Checked)
        .preset(cbPreset.Text)
        .OutputPrefix(FilePrefix.Text)
        .OutputSuffix(FileSuffix.Text);

      if (chkEncodeAudio.Checked)
        ffcommand.acodec(UseAudioEncoder.Text).aBitrate((int)aBitrate.Value);
      else
        ffcommand.acodec("copy").aBitrate(0);

      ffcommand.outputPath(cbOutputDir.Text);

      var deinterlaces = new List<string>() { "bwdif","bwdif_cuda","yadif","yadif_cuda" };

      if (chkFilterDeInterlace.Checked)
      {
        if (cbDeinterlaceAlg.SelectedIndex == 0)
        {
          if (UseVideoEncoder.Text.EndsWith("_nvenc"))
            ffcommand.setFilter("bwdif_cuda", "0:-1:0");
          else if (UseVideoEncoder.Text.EndsWith("_qsv"))
            ffcommand.setFilter("bwdif", "0:-1:0");
        }
        else if(cbDeinterlaceAlg.SelectedIndex == 1)
        {
          if (UseVideoEncoder.Text.EndsWith("_nvenc"))
            ffcommand.setFilter("yadif_cuda", "2:-1:0");
          else if (UseVideoEncoder.Text.EndsWith("_qsv"))
            ffcommand.setFilter("yadif", "2:-1:0");
        }
      }
      else
      {
        foreach (var name in deinterlaces)
          ffcommand.removeFilter(name);
      }

      var tag = int.Parse(GetCheckedRadioButton(ResizeBox).Tag as string);
      int size;
      switch (tag)
      {
        case 0:
          size = 0;
          break;

        case 720:
        case 1080:
          size = int.Parse(tag.ToString());
          break;

        case -1:
          size = (int)resizeTo.Value;
          break;

        default:
          throw new Exception("size error");
      }

      if (size > 0)
      {
        if (UseVideoEncoder.Text.EndsWith("_nvenc"))
          ffcommand.setFilter("scale_cuda", rbLandscape.Checked ? $"-2:{size}" : $"{size}:-2");
        else if (UseVideoEncoder.Text.EndsWith("_qsv"))
          ffcommand.setFilter("scale_qsv", rbLandscape.Checked ? $"-1:{size}" : $"{size}:-1");
      }
      else
      {
        ffcommand.removeFilter("scale_cuda").removeFilter("scale_qsv");
      }

      var rotate = int.Parse(GetCheckedRadioButton(RotateBox).Tag.ToString());
      if (rotate == 0)
        ffcommand.removeFilter("transpose");
      else
        ffcommand.setFilter("transpose", rotate.ToString());

      return ffcommand;
    }

    private RadioButton GetCheckedRadioButton(GroupBox groupBox)
    {
      return groupBox.Controls.OfType<RadioButton>().FirstOrDefault(radio => radio.Checked);
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
      if (e.Data == null)
        return;

      byte[] bytes = CP932.GetBytes(e.Data);
      LogWriter.WriteLine(Encoding.UTF8.GetString(bytes));

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
        if (LogWriter != null)
          LogWriter.Dispose();
      }
    }

    private string GetLogFileName()
    {
      return Path.Combine(
        Environment.ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("TEMP")),
        "ffmpeg.stderr.log"
      );
    }

    private void OpenLogWriter()
    {
      LogWriter = new StreamWriter(GetLogFileName(), false);
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

    private void InitPresetAndDevice()
    {
      cbPreset.Items.Clear();
      foreach (var item in Presets[UseVideoEncoder.Text])
        cbPreset.Items.Add(item);

      var m = Regex.Match(UseVideoEncoder.Text, @"^(?:\w+)_(nvenc|qsv)$");
      if (m.Success)
      {
        string hardware = m.Groups[1].Value;
        string hardwareName = "";
        if (hardware == "nvenc")
        {
          cbPreset.SelectedIndex = 16;
          hardwareName = "nvidia";
        }
        else if (hardware == "qsv")
        {
          cbPreset.SelectedIndex = 3;
          hardwareName = "intel";
        }

        cbDevices.SelectedIndex = cbDevices.FindString(hardwareName);

        if(chkConstantQuality.Checked)
          vQualityLabel.Text = hardware == "qsv" ? "ICQ" : "-cq";
      }
    }

    private IEnumerable<string> GetGPUDevices()
    {
      ManagementObjectSearcher videoDevices = new ManagementObjectSearcher("select * from Win32_VideoController");
      var devices = new List<string>();

      foreach (ManagementObject device in videoDevices.Get().Cast<ManagementObject>())
        devices.Add($"{device["Name"]}:{device["DeviceId"]}");

      return devices;
    }

    private string[] FindInPath(string CommandName)
    {
      //環境変数%PATH%取得し、カレントディレクトリを連結。配列への格納
      var dirPathList =
        Environment
          .ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("PATH"))
          .Split(new char[] { ';' })
          .Prepend(Directory.GetCurrentDirectory());

      //正規表現に使用するため、%PATHEXT%の取得・ピリオド文字の変換及び配列への格納
      var pathext = Environment.GetEnvironmentVariable("PATHEXT").Replace(".", @"\.").Split(new Char[] { ';' });

      //検索するファイル名の正規表現
      var regex = new Regex(
        $"^{CommandName}(?:{String.Join("|", pathext)})?$",
        RegexOptions.IgnoreCase
      );

      return
        dirPathList
          .Where(dirPath => Directory.Exists(dirPath))
          .SelectMany(dirPath => Directory.GetFiles(dirPath).Where(file => regex.IsMatch(Path.GetFileName(file))))
          .ToArray();
    }
  }
}
