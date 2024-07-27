using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ffmpeg_ytdlp_gui.libs;

namespace ffmpeg_ytdlp_gui
{
  public partial class StdoutForm : Form
  {
    private bool _imp_pause = false;
    public bool Pause
    {
      get => _imp_pause;
      set => _imp_pause = BtnSubmitSaveFile.Enabled = value;
    }

    public List<string> LogData { get; private set; } = new List<string>();
    public Button CustomButton
    {
      get => _CustomButton;
      private set => throw new Exception("can not set button instance");
    }
    public RedirectedProcess Redirected { get; private set; }

    public event Action<string> DataReceived;
    public event Action ProcessExit;
    public event Action<object, MouseEventArgs> CustomButtonClick;


    [GeneratedRegex(@"\.(?:exe|cmd|ps1|bat)$")]
    private static partial Regex IsExecutableFile();

    public StdoutForm()
    {
      InitializeComponent();

      ProcessExit += () =>
      {
        Release();

        if (LogData.Count > 0)
        {
          StdOutAndErrorView.AppendText("\r\n");
          StdOutAndErrorView.AppendText(string.Join("\r\n", LogData));
          LogData.Clear();
        }
      };
    }

    public StdoutForm(string executable, string arguments, string title = "") : this()
    {
      DataReceived += data => WriteLine(data);
      ProcessExit += () =>
      {
        Release();
        Pause = true;

        StdOutAndErrorView.Focus();
      };

      Redirected = new RedirectedProcess(executable, arguments);
      Redirected.ProcessExited += (s, e) => Invoke(OnProcessExit);
      Redirected.StdErrReceived += data => Invoke(OnDataReceived, [data]);
      Redirected.StdOutReceived += data => Invoke(OnDataReceived, [data]);

      if (!string.IsNullOrEmpty(title))
        Text = title;

      BtnClose.Enabled = true;
    }

    public void WriteLine(object data)
    {
      StdOutAndErrorView.AppendText($"{data.ToString()}\r\n");
    }

    public void Release()
    {
      BtnClose.Enabled = BtnSubmitSaveFile.Enabled = true;
      BtnToggleReader.Enabled = false;
    }
    public void Lock()
    {
      BtnClose.Enabled = BtnSubmitSaveFile.Enabled = false;
      BtnToggleReader.Enabled = true ;
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
    }

    private async void BtnSubmitSaveFile_Click(object sender, EventArgs e)
    {
      if (!Pause)
        return;

      var dlg = new SaveFileDialog()
      {
        FileName = "output.log",
        Filter = "テキストファイル|*.log,*.txt",
        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        OkRequiresInteraction = true,
        Title = "ログを保存します。"
      };

      if (DialogResult.Cancel == dlg.ShowDialog())
        return;

      string filename = dlg.FileName;
      using (var sw = new StreamWriter(filename, false))
      {
        foreach (var line in StdOutAndErrorView.Lines)
          await sw.WriteLineAsync(line);
      }
      dlg.FileName = string.Empty;
    }

    public virtual void OnDataReceived(string data)
    {
      DataReceived?.Invoke(data);
    }

    public virtual void OnProcessExit()
    {
      Pause = true;
      ProcessExit?.Invoke();
    }

    public virtual void OnCustomButtonClick(object sender, EventArgs e)
    {
      CustomButtonClick?.Invoke(sender, (MouseEventArgs)e);
    }
  }
}
