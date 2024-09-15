using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ffmpeg_ytdlp_gui
{
  internal static class Program
  {
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    static void Main()
    {
      bool mutexCreated;
#if DEBUG
      string appname = "FFMPEG-YTDLP-GUI-DEBUG";
#else
      string appname = "FFMPEG-YTDLP-GUI";
#endif
      using Mutex mutex = new Mutex(true, $"mutex-{appname}", out mutexCreated);
      using var hEvent = new EventWaitHandle(false, EventResetMode.AutoReset, $"event-{appname}");
      using var cts = new CancellationTokenSource();

      try
      {
        if (!mutexCreated)
        {
          hEvent.Set();
          return;
        }
        else
        {
          Task.Run(() =>
          {
            try
            {
              var token = cts.Token;
              while (true)
              {
                token.ThrowIfCancellationRequested();
                hEvent.WaitOne();
                token.ThrowIfCancellationRequested();

                using var process = Process.GetCurrentProcess();
                var hWnd = process.MainWindowHandle;
                if (hWnd != IntPtr.Zero)
                  SetForegroundWindow(hWnd);
              }
            }
            catch (OperationCanceledException)
            {
              Debug.WriteLine("Exit event waiting loop");
            }
          });
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
      }
      finally
      {
        cts.Cancel();
        hEvent.Set();

        if(mutexCreated)
          mutex.ReleaseMutex();
      }
    }

  }
}
