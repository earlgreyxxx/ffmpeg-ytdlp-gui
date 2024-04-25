using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ffmpeg_command_builder
{
  public partial class StdoutForm : Form
  {
    public bool Pause { get; private set; } = false;
    public List<string> LogData { get; private set; } = new List<string>();

    public event Action<string> OnDataReceived;
    public event Action OnProcessExit;

    public RedirectedProcess Redirected { get; private set; }

    [GeneratedRegex(@"\.(?:exe|cmd|ps1|bat)$")]
    private static partial Regex IsExecutableFile();

    public StdoutForm()
    {
      InitializeComponent();
    }

    public StdoutForm(string executable, string arguments, string title = "") : this()
    {
      OnDataReceived += data => WriteLine(data);
      OnProcessExit += () =>
      {
        BtnClose.Enabled = BtnSubmitSaveFile.Enabled = Pause = true;

        StdOutAndErrorView.Focus();
        StdOutAndErrorView.SelectionStart = 0;
        StdOutAndErrorView.ScrollToCaret();
      };

      Redirected = new RedirectedProcess(executable, arguments);
      Redirected.OnProcessExited += (s, e) => Invoke(OnProcessExit);
      Redirected.OnStdErrReceived += data => Invoke(OnDataReceived, [data]);
      Redirected.OnStdOutReceived += data => Invoke(OnDataReceived, [data]);

      if (!string.IsNullOrEmpty(title))
        Text = title;

      BtnClose.Enabled = true;
    }

    public void OnProcesssDoneInvoker()
    {
      BtnClose.Enabled = true;
      BtnToggleReader.Enabled = false;
      if (LogData.Count > 0)
      {
        StdOutAndErrorView.AppendText("\r\n");
        StdOutAndErrorView.AppendText(string.Join("\r\n", LogData));
        LogData.Clear();
      }
    }

    public void WriteLine(object data)
    {
      StdOutAndErrorView.AppendText($"{data.ToString()}\r\n");
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void StdoutForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!BtnClose.Enabled)
        e.Cancel = true;
    }

    private void ToggleReader_Click(object sender, EventArgs e)
    {
      Button btn = sender as Button;

      btn.Text = Pause ? "読込中断" : "読込再開";
      if (Pause)
      {
        StdOutAndErrorView.AppendText("\r\n");
        StdOutAndErrorView.AppendText(string.Join("\r\n", LogData));
      }
      LogData.Clear();

      Pause = !Pause;

      BtnSubmitSaveFile.Enabled = Pause;
    }

    private async void BtnSubmitSaveFile_Click(object sender, EventArgs e)
    {
      if (!Pause)
        return;

      if (DialogResult.Cancel == SaveFileDlg.ShowDialog())
        return;

      string filename = SaveFileDlg.FileName;
      using (var sw = new StreamWriter(filename, false))
      {
        foreach (var line in StdOutAndErrorView.Lines)
          await sw.WriteLineAsync(line);
      }
      SaveFileDlg.FileName = string.Empty;
    }
  }
}
