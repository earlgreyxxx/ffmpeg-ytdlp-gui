using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ffmpeg_command_builder
{
  public partial class StdoutForm : Form
  {
    private Action<string> StdoutErrorAndWrite;
    private Action ProcessExit;
    private RedirectedProcess redirected;

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

    public bool StartProcess(string fileName, string arguments = "")
    {
      redirected = new RedirectedProcess(fileName, arguments);
      redirected.OnProcessExited += (s, e) => Invoke(ProcessExit);
      redirected.OnStdOutReceived += data => Invoke(StdoutErrorAndWrite,[data]);
      redirected.OnStdErrReceived += data => Invoke(StdoutErrorAndWrite,[data]);

      return redirected.Start();
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      if(redirected.Exited)
        Close();
    }
  }
}
