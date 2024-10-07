using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ffmpeg_ytdlp_gui.libs
{
  public class ffmpeg_process
  {
    // statics
    // ---------------------------------------------------------------------------------------
    protected static RedirectedProcess? CreateRedirectedProcess(string filename,ffmpeg_command? command)
    {
      string? arguments = command?.GetCommandLineArguments(filename) ?? null;
      string? execfile = command?.ffmpegPath ?? null;

      Debug.WriteLine($"{execfile} {arguments} on RedirectedProcess.CreateRedirectedProcess");
      if (string.IsNullOrEmpty(arguments) || string.IsNullOrEmpty(execfile))
        return null;

      var redirected = new RedirectedProcess(execfile!, arguments);
      var process = redirected.Current as CustomProcess ?? throw new NullReferenceException("Current process is null");
      process.CustomFileName = filename;
      process.StartInfo.Environment.Add("AV_LOG_FORCE_NOCOLOR", "1");
      process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
      process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

      return redirected;
    }

    // instances
    // ---------------------------------------------------------------------------------------
    protected BindingSource? FileListBindingSource;

    public RedirectedProcess? Redirected { get; protected set; }

    private StreamWriter? LogWriter;

    public event Action<string>? ProcessExit;
    public event Action<string>? ReceiveData;
    public event Action<bool>? ProcessesDone;
    public event Action<ffmpeg_command?,string>? PreProcess;

    public ffmpeg_command? Command { get; protected set; }
    public string? LogFileName { get; set; } = RedirectedProcess.GetTemporaryFileName("ffmpeg-stderr-",".log");

    public ffmpeg_process(ffmpeg_command command,IEnumerable<string> list)
      : this(command,new BindingSource() { DataSource = list.ToList(), }) { }

    public ffmpeg_process(ffmpeg_command command,BindingSource bs)
    {
      Command = command;
      FileListBindingSource = bs;
    }

    public ffmpeg_process()
    {
      throw new ArgumentException();
    }

    public virtual void Begin()
    {
      if (Redirected != null && !Redirected.Current.HasExited)
        throw new Exception("現在実行中のプロセスが終了するまで新しいプロセスを開始できません。");

      if (!string.IsNullOrEmpty(LogFileName))
        LogWriter = new StreamWriter(LogFileName, true);

      if (false == CreateProcess() && FileListBindingSource?.Count > 0)
        CreateProcess();
    }

    protected virtual bool CreateProcess()
    {
      var filename = FileListBindingSource?.OfType<StringListItem>().FirstOrDefault()?.Value;
      if (filename == null)
        throw new Exception("no file");

      if (!File.Exists(filename))
      {
        OnProcessExit(filename);
        Redirected = null;
        CreateProcess();
        return true;
      }

      OnPreProcess(Command, filename);

      if (null == (Redirected = CreateRedirectedProcess(filename, Command)))
      {
        OnProcessesDone(true);
        LogWriter?.Dispose();
        LogWriter = null;
        var item = FileListBindingSource?.OfType<StringListItem>().FirstOrDefault(item => filename == item.Value);
        if (item != null)
        {
          FileListBindingSource?.Remove(item);
          FileListBindingSource?.ResetBindings(false);
        }

        return false;
      }

      Redirected.ProcessExited += OnProcessExited;
      Redirected.StdErrReceived += WriteLog;
      Redirected.StdErrReceived += data => OnReceiveData(data);
      if (!Redirected.Start())
      {
        OnProcessExited(Redirected.Current, new EventArgs());
        return true;
      }

      return true;
    }

    public virtual void One(string filename)
    {
      if (!string.IsNullOrEmpty(LogFileName))
        LogWriter = new StreamWriter(LogFileName, false);

      OnPreProcess(Command, filename);

      if (null == (Redirected = CreateRedirectedProcess(filename, Command)))
      {
        OnAllProcessExited(null, null);
        return;
      }

      Redirected.ProcessExited += OnAllProcessExited;
      Redirected.StdErrReceived += WriteLog;
      Redirected.StdErrReceived += data => OnReceiveData(data);
      
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

      LogWriter?.WriteLine(data);
    }

    private void OnProcessExited(object? sender, EventArgs? e)
    {
      try
      {
        var process = Redirected?.Current as CustomProcess ?? null;
        string? filename = process?.CustomFileName ?? null;
        if (process != null && !string.IsNullOrEmpty(filename))
          OnProcessExit(filename);

        Redirected = null;
        if(false == CreateProcess())
          throw new Exception("入力および出力のファイルパスが同じファイルを指しています。");
      }
      catch
      {
        OnAllProcessExited(sender, e);
      }
    }

    private void OnAllProcessExited(object? sender, EventArgs? e)
    {
      OnProcessesDone();
      FileListBindingSource?.Clear();
      FileListBindingSource?.ResetBindings(false);
      LogWriter?.Flush();
      LogWriter?.Dispose();
      LogWriter = null;
    }

    public void Kill(bool stopAll = false)
    {
      if (stopAll)
        FileListBindingSource?.Clear();

      if (Redirected == null || Redirected.Current.HasExited)
        return;

      Redirected.StdInWriter?.Write('q');
    }

    // イベント
    public virtual void OnProcessExit(string filename)
    {
      ProcessExit?.Invoke(filename);
    }
    public virtual void OnReceiveData(string data)
    {
      ReceiveData?.Invoke(data);
    }
    public virtual void OnProcessesDone(bool abnormal = false)
    {
      ProcessesDone?.Invoke(abnormal);
    }
    protected void OnPreProcess(ffmpeg_command? command,string filename)
    {
      PreProcess?.Invoke(command,filename);
    }
  }
}
