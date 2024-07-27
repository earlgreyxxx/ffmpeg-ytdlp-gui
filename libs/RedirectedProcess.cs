using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  /// <summary>
  /// 標準入出力をリダイレクトしたプロセスを表すヘルパークラス
  /// </summary>
  public class RedirectedProcess
  {
    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern bool AttachConsole(uint dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern bool FreeConsole();

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern bool SetConsoleCtrlHandler(ConsoleEventHandler handlerRoutine, bool add);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

    delegate bool ConsoleEventHandler(uint handler);

    const uint CTRL_C_EVENT = 0;
    const uint CTRL_BRAKE_EVENT = 1;

    public Process Current { get; private set; } = new CustomProcess();
    public event Action<object, EventArgs> ProcessExited;
    public event Action<string> StdOutReceived;
    public event Action<string> StdErrReceived;
    public StreamWriter StdInWriter { get; protected set; }
    public bool Exited { private set; get; } = false;
    public string Command { get; protected set; }

    public int ExitCode 
    {
      get => Current.ExitCode;
      private set => throw new Exception("exit code can not set");
    }

    protected ProcessStartInfo psi { get; set; } = new ProcessStartInfo()
    {
      CreateNoWindow = true,
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      RedirectStandardInput = true,
      UseShellExecute = false
    };

    private void default_Process_Exited(object sender, EventArgs e)
    {
      StdInWriter.Dispose();
      StdInWriter = null;
      Exited = Current.HasExited;
    }

    public RedirectedProcess(string filename, string arguments = "")
    {
      Command = filename;

      if(!string.IsNullOrEmpty(arguments))
        psi.Arguments = arguments;

      Current.StartInfo = psi;
      Current.EnableRaisingEvents = true;
      Current.Exited += default_Process_Exited; 

      Current.Exited += (sender, ev) => OnProcessExited(sender, new RedirectedProcessEventArgs(psi.FileName,psi.Arguments));
      Current.OutputDataReceived += (sender, ev) => OnStdOutReceived(ev.Data ?? string.Empty);
      Current.ErrorDataReceived += (sender, ev) => OnStdErrReceived(ev.Data ?? string.Empty);
    }

    ~RedirectedProcess()
    {
      Current.Dispose();
    }

    /// <summary>
    ///  プロセスを開始します。
    /// </summary>
    /// <param name="arguments">追加の引数</param>
    /// <returns></returns>
    public virtual bool Start(string arguments = "")
    {
      Current.StartInfo.FileName = Command;

      if(!string.IsNullOrEmpty(arguments))
        Current.StartInfo.Arguments = arguments;

      bool rv = Current.Start();
      if (rv)
      {
        Current.BeginOutputReadLine();
        Current.BeginErrorReadLine();
        StdInWriter = Current.StandardInput;
      }
      return rv;
    }

    /// <summary>
    /// プロセスを開始してブロックせずに終了まで待ちます。
    /// </summary>
    /// <param name="arguments">追加の引数</param>
    /// <returns></returns>
    public Task StartAsync(string arguments = "")
    {
      if(!Start(arguments))
        return null;

       return Current.WaitForExitAsync();
    }

    /// <summary>
    /// 強制プロセス終了
    /// </summary>
    public void Kill()
    {
      if (!Current.HasExited)
        Current.Kill(true);
    }
   
    /// <summary>
    /// CTRL-Cイベントを起こしてプロセスを終了する。
    /// </summary>
    public void Interrupt()
    {
      if (Current.HasExited)
      {
        Debug.WriteLine("既にダウンロードプロセスは終了しています。");
        return;
      }

      uint processId = (uint)Current.Id;

      AttachConsole(processId);
      SetConsoleCtrlHandler(null, true);

      if (!GenerateConsoleCtrlEvent(CTRL_C_EVENT, (uint)Current.Id))
      {
        Debug.WriteLine("CTRL-Cイベント発行失敗");
        return;
      }

      SetConsoleCtrlHandler(null, false);
      FreeConsole();
    }

    // イベント実行
    protected virtual void OnStdOutReceived(string message)
    {
      StdOutReceived?.Invoke(message);
    }

    protected virtual void OnStdErrReceived(string message)
    {
      StdErrReceived?.Invoke(message);
    }

    protected virtual void OnProcessExited(object sender, EventArgs e)
    {
      ProcessExited?.Invoke(sender, e);
    }
  }

  internal class RedirectedProcessEventArgs(string filename, string arguments) : EventArgs
  {
    public string FileName { get; private set; } = filename;
    public string Arguments { get; private set; } = arguments;
  }
}
