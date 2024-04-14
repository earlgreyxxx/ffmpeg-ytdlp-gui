using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ffmpeg_command_builder
{
  using StringListItem = ListItem<string>;

  internal class ffmpeg_process
  {
    public static string GetLogFileName()
    {
      return Path.Combine(
        Environment.ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("TEMP")),
        $"ffmpeg-stderr-{Process.GetCurrentProcess().Id}.log"
      );
    }

    BindingSource FileList;

    public CustomProcess Current { get; private set; }
    private List<CustomProcess> Processes = new List<CustomProcess>();

    private StreamWriter LogWriter;
    private Encoding CP932;

    public event Action<string> OnProcessExit;
    public event Action<string> OnReceiveData;
    public event Action OnProcessesDone;

    public ffmpeg_command Command { get; private set; }
    public string LogFileName { get; set; } = GetLogFileName();

    public ffmpeg_process(ffmpeg_command command,IEnumerable<string> list)
      : this(command,new BindingSource() { DataSource = list.ToList(), }) { }

    public ffmpeg_process(ffmpeg_command command,BindingSource bs)
    {
      try
      {
        CP932 = Encoding.GetEncoding(932);
      }
      catch
      {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        CP932 = Encoding.GetEncoding(932);
      }
      Command = command;
      FileList = bs;
    }

    ~ffmpeg_process()
    {
      foreach(var process in Processes)
        process.Dispose();
    }

    public void Begin()
    {
      if (Current != null && !Current.HasExited)
        throw new Exception("現在実行中のプロセスが終了するまで新しいプロセスを開始できません。");

      if (!string.IsNullOrEmpty(LogFileName))
        LogWriter = new StreamWriter(LogFileName, true);

      CreateProcess();
    }

    private void CreateProcess()
    {
      var filename = FileList.OfType<StringListItem>().FirstOrDefault()?.Value;
      if (filename == null)
        throw new Exception("no file");

      var process = Command.InvokeCommand(filename, true);
      Processes.Add(process);

      process.Exited += OnProcessExited;
      process.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceived);
      if (!process.Start())
        throw new Exception("プロセスが正常に開始できませんでした。");

      process.BeginErrorReadLine();

      Current = process;
    }

    public void One(string filename)
    {
      if (!string.IsNullOrEmpty(LogFileName))
        LogWriter = new StreamWriter(LogFileName, false);

      var process = Command.InvokeCommand(filename, true);
      process.Exited += OnAllProcessExited;
      process.ErrorDataReceived += new DataReceivedEventHandler(OnDataReceived);
      if (!process.Start())
      {
        OnAllProcessExited(process, new EventArgs());
        return;
      }
      process.BeginErrorReadLine();
      Current = process;
    }

    private void OnDataReceived(object sender, DataReceivedEventArgs e)
    {
      if (e.Data == null)
        return;

      if (LogWriter != null)
      {
        byte[] bytes = CP932.GetBytes(e.Data);
        LogWriter.WriteLine(Encoding.UTF8.GetString(bytes));
      }

      if (!String.IsNullOrEmpty(e.Data))
        OnReceiveData(e.Data);
    }

    private void OnProcessExited(object sender, EventArgs e)
    {
      try
      {
        string filename = Current.CustomFileName;
        Current.Dispose();
        Current = null;
        OnProcessExit(filename);
        CreateProcess();
      }
      catch
      {
        OnAllProcessExited(sender, e);
      }
    }

    private void OnAllProcessExited(object sender, EventArgs e)
    {
      OnProcessesDone();
      FileList.Clear();
      FileList.ResetBindings(false);
      LogWriter.Flush();
      if (LogWriter != null)
      {
        LogWriter.Dispose();
        LogWriter = null;
      }
    }

    public void Abort(bool stopAll = false)
    {
      if (stopAll)
        FileList.Clear();

      if (Current == null || Current.HasExited)
        return;

      using StreamWriter sw = Current.StandardInput;
      sw.Write('q');
    }
  }
}
