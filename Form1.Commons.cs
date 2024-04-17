using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ffmpeg_command_builder
{
  using StringListItem = ListItem<string>;
  using StringListItems = List<ListItem<string>>;

  partial class Form1 : Form
  {
    // Static members
    // ---------------------------------------------------------------------------------
    [GeneratedRegex(@"\.(?:exe|cmd|ps1|bat)$")]
    private static partial Regex IsExecutableFile();

    private static List<ManagementObject> GpuDevices;

    private static string[] FindInPath(string CommandName)
    {
      //環境変数%PATH%取得し、カレントディレクトリを連結。配列への格納
      IEnumerable<string> dirPathList =
        Environment
          .ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("PATH"))
          .Split([';'])
          .Prepend(Directory.GetCurrentDirectory());

      //正規表現に使用するため、%PATHEXT%の取得・ピリオド文字の変換及び配列への格納
      string[] pathext = Environment.GetEnvironmentVariable("PATHEXT").Replace(".", @"\.").Split([';']);

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

    private static void CheckDirectory(string path)
    {
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

    private static StringListItems GetGPUDeviceList()
    {
      if (GpuDevices == null)
        CreateGPUDeviceList();

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
      deviceList.Add(new StringListItem("cpu"));
      return deviceList;
    }

    private static void CreateGPUDeviceList()
    {
      var VideoDevices = new ManagementObjectSearcher("select * from Win32_VideoController");
      GpuDevices = VideoDevices.Get().Cast<ManagementObject>().ToList();
    }

    // Instance members
    // ---------------------------------------------------------------------------------
    private ffmpeg_process Proceeding;
    private ffmpeg_process CreateFFmpegProcess(ffmpeg_command command)
    {
      var process = new ffmpeg_process(command,FileListBindingSource);

      Action ProcessesDoneInvoker = () =>
      {
        btnStop.Enabled = btnStopAll.Enabled = btnStopUtil.Enabled = btnStopAllUtil.Enabled = false;
        OpenLogFile.Enabled = true;
        if (FileListBindingSource.Count > 0)
          btnSubmitInvoke.Enabled = true;

        Proceeding = null;
        MessageBox.Show("変換処理が終了しました。");
      };
      Action<string> ReceiveDataInvoker = output => OutputStderr.Text = output; 
      Action<string> ProcessExitInvoker = filename =>
      {
        var item = FileListBindingSource.OfType<StringListItem>().FirstOrDefault(item => item.Value == filename);
        if (item == null)
          return;

        FileListBindingSource.Remove(item);
      };

      process.OnProcessesDone += () => Invoke(ProcessesDoneInvoker);
      process.OnReceiveData += data => Invoke(ReceiveDataInvoker, [data]);
      process.OnProcessExit += filename => Invoke(ProcessExitInvoker, [filename]);

      if (IsOpenStderr.Checked)
      {
        var form = new StdoutForm();
        var cp932 = Encoding.GetEncoding(932);
        process.OnReceiveData += data =>
        {
          if (string.IsNullOrEmpty(data))
            return;

          byte[] bytes = cp932.GetBytes(data);
          string encoded = Encoding.UTF8.GetString(bytes);

          if (form.Pause)
            form.LogData.Add(encoded);
          else
            form.Invoke(form.WriteLine, [encoded]);
        };
        process.OnProcessesDone += () => form.Invoke(form.OnProcesssDoneInvoker);
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

      ffcommand
        .vcodec(UseVideoEncoder.SelectedValue.ToString(), cbDevices.Items.Count > 1 ? cbDevices.SelectedIndex : 0)
        .vBitrate((int)vBitrate.Value, chkConstantQuality.Checked)
        .lookAhead((int)LookAhead.Value)
        .preset(cbPreset.SelectedValue.ToString());

      if (chkUseHWDecoder.Checked)
        ffcommand.hwdecoder(HWDecoder.SelectedValue.ToString());

      if (chkCrop.Checked)
      {
        if (chkUseHWDecoder.Checked && HWDecoder.SelectedValue.ToString().EndsWith("_cuvid"))
          ffcommand.size(decimal.ToInt32(VideoWidth.Value), decimal.ToInt32(VideoHeight.Value)).crop(true, CropWidth.Value, CropHeight.Value, CropX.Value, CropY.Value);
        else
          ffcommand.crop(CropWidth.Value, CropHeight.Value, CropX.Value, CropY.Value);
      }

      if (chkEncodeAudio.Checked)
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
        720 or 1080 => int.Parse(tag.ToString()),
        -1 => (int)resizeTo.Value,
        _ => throw new Exception("size error"),
      };

      if (size > 0)
      {
        ffcommand.IsLandscape = rbLandscape.Checked;
        ffcommand.setFilter("scale", size.ToString());
      }
      else
      {
        ffcommand.removeFilter("scale");
      }

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

    private void StopProcess(bool stopAll = false)
    {
      if (stopAll)
        FileListBindingSource.Clear();

      Proceeding.Abort(stopAll);
    }

    private void OpenOutputView(string executable, string arg,string formTitle = "ffmpeg outputs")
    {
      if(!File.Exists(executable))
      {
        var exepathes = FindInPath(executable);
        if (exepathes.Length <= 0)
          return;
      }

      var form = new StdoutForm(executable, arg, formTitle);
      form.OnProcessExit += form.OnProcesssDoneInvoker;

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

      if (!cbOutputDir.Items.Contains(cbOutputDir.Text))
        cbOutputDir.Items.Add(cbOutputDir.Text);
    }
  }
}
