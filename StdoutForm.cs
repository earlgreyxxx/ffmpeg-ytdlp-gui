using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ffmpeg_command_builder
{
  public partial class StdoutForm : Form
  {
    private Process process;
    private Action<string> StdoutErrorAndWrite;
    private Action ProcessExit;

    [GeneratedRegex(@"\.(?:exe|cmd|ps1|bat)$")]
    private static partial Regex IsExecutableFile();

    public StdoutForm()
    {
      InitializeComponent();

      StdoutErrorAndWrite = output => StdOutAndErrorView.AppendText($"{output}\r\n");
      ProcessExit = () =>
      {
        BtnClose.Enabled = true;
        StdOutAndErrorView.Focus();

        StdOutAndErrorView.SelectionStart = 0;
        StdOutAndErrorView.ScrollToCaret();
      };
    }

    public Process StartProcess(string fileName, string arguments = "")
    {
      var psi = new ProcessStartInfo()
      {
        FileName = fileName,
      };

      if (IsExecutableFile().IsMatch(fileName) || fileName == "ffmpeg")
      {
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;
        psi.RedirectStandardInput = true;
        psi.UseShellExecute = false;
        if (!string.IsNullOrEmpty(arguments))
          psi.Arguments = arguments;
      }
      else
      {
        psi.UseShellExecute = true;
      }

      process = new Process()
      {
        StartInfo = psi,
        EnableRaisingEvents = !psi.UseShellExecute
      };

      if (psi.UseShellExecute)
      {
        process.Start();
      }
      else
      {
        void DataReceivedHandler(string data) => Invoke(StdoutErrorAndWrite, [data]);

        process.OutputDataReceived += (sender, e) => DataReceivedHandler(e.Data ?? string.Empty);
        process.ErrorDataReceived += (sender, e) => DataReceivedHandler(e.Data ?? string.Empty);

        process.Exited += (sender, e) => Invoke(ProcessExit);

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
      }

      return process;
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      if(process != null && process.HasExited)
        process.Dispose();

      Close();
    }
  }
}
