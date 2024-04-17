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
    // statics
    // ---------------------------------------------------------------------------------------
    public static string GetLogFileName()
    {
      return Path.Combine(
        Environment.ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("TEMP")),
        $"ffmpeg-stderr-{Process.GetCurrentProcess().Id}.log"
      );
    }

    private static RedirectedProcess CreateRedirectedProcess(string filename,ffmpeg_command command)
    {
      var redirected = new RedirectedProcess(command.ffmpegPath, command.GetCommandLineArguments(filename));
      var process = redirected.Current as CustomProcess;
      process.CustomFileName = filename;
      process.StartInfo.Environment.Add("AV_LOG_FORCE_NOCOLOR", "1");

      return redirected;
    }

    // instances
    // ---------------------------------------------------------------------------------------
    BindingSource FileList;

    public RedirectedProcess Redirected { get; private set; }

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

    public void Begin()
    {
      if (Redirected != null && !Redirected.Current.HasExited)
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

      Redirected = CreateRedirectedProcess(filename,Command);
      Redirected.OnProcessExited += OnProcessExited;
      Redirected.OnStdErrReceived += WriteLog;
      Redirected.OnStdErrReceived += data => OnReceiveData.Invoke(data);
      if (!Redirected.Start())
      {
        OnProcessExited(Redirected.Current, new EventArgs());
        return;
      }
    }

    public void One(string filename)
    {
      if (!string.IsNullOrEmpty(LogFileName))
        LogWriter = new StreamWriter(LogFileName, false);

      Redirected = CreateRedirectedProcess(filename,Command);
      Redirected.OnProcessExited += OnAllProcessExited;
      Redirected.OnStdErrReceived += WriteLog;
      Redirected.OnStdErrReceived += data => OnReceiveData.Invoke(data);
      
      if (!Redirected.Start())
      {
        OnAllProcessExited(Redirected.Current, new EventArgs());
        return;
      }
    }

    private void WriteLog(string data)
    {
      if (string.IsNullOrEmpty(data))
        return;

      byte[] bytes = CP932.GetBytes(data);
      LogWriter?.WriteLine(Encoding.UTF8.GetString(bytes));
    }

    private void OnProcessExited(object sender, EventArgs e)
    {
      try
      {
        var process = Redirected.Current as CustomProcess;
        string filename = process.CustomFileName;
        OnProcessExit?.Invoke(filename);
        Redirected = null;
        CreateProcess();
      }
      catch
      {
        OnAllProcessExited(sender, e);
      }
    }

    private void OnAllProcessExited(object sender, EventArgs e)
    {
      OnProcessesDone?.Invoke();
      FileList.Clear();
      FileList.ResetBindings(false);
      LogWriter?.Flush();
      LogWriter?.Dispose();
      LogWriter = null;
    }

    public void Abort(bool stopAll = false)
    {
      if (stopAll)
        FileList.Clear();

      if (Redirected == null || Redirected.Current.HasExited)
        return;

      Redirected.StdInWriter.Write('q');
    }
  }
}
