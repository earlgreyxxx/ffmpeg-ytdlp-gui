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
      string appname = "FFMPEG-YTDLP-GUI";
      var eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, $"event-{appname}");
      bool terminate = false;
      bool mutexCreated;

      Mutex mutex = new Mutex(true, $"mutex-{appname}", out mutexCreated);

      try
      {
        if (!mutexCreated)
        {
          using (eventHandle)
          {
            eventHandle.Set();
          }
          return;
        }
        else
        {
          Task.Run(() =>
          {
            while(true)
            {
              eventHandle.WaitOne();

              if (terminate)
                break;

              using (var process = Process.GetCurrentProcess())
              {
                var hWnd = process.MainWindowHandle;
                if(hWnd != IntPtr.Zero)
                  SetForegroundWindow(hWnd);
              }
            }
          });
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());

        terminate = true;
        eventHandle.Set();
      }
      finally
      {
        if(mutexCreated)
          mutex.ReleaseMutex();

        mutex.Dispose();
      }
    }

  }
}
