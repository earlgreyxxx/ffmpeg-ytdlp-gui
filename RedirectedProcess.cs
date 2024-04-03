using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ffmpeg_command_builder
{
  internal class RedirectedProcess
  {
    public Process Current { get; private set; } = new Process();
    public event Action<object, EventArgs> OnProcessExited;
    public event Action<string> OnStdOutReceived;
    public event Action<string> OnStdErrReceived;
    public StreamWriter StdInWriter { get; private set; }
    public bool Exited { private set; get; } = false;

    private ProcessStartInfo psi { get; set; } = new ProcessStartInfo()
    {
      CreateNoWindow = true,
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      RedirectStandardInput = true,
      UseShellExecute = false
    };

    private void Process_Exited(object sender, EventArgs e)
    {
      StdInWriter.Dispose();
      StdInWriter = null;
      OnProcessExited(this, new RedirectedProcessEventArgs(psi.FileName, psi.Arguments));
      Exited = Current.HasExited;
      Current.Dispose();
    }

    public RedirectedProcess(string filename, string arguments)
    {
      psi.FileName = filename;
      psi.Arguments = arguments;

      Current.StartInfo = psi;
      Current.Exited += Process_Exited;
      Current.EnableRaisingEvents = true;
    }

    public bool Start()
    {
      Current.OutputDataReceived += (sender, ev) => OnStdOutReceived(ev.Data ?? string.Empty);
      Current.ErrorDataReceived += (sender, ev) => OnStdErrReceived(ev.Data ?? string.Empty);

      bool rv = Current.Start();
      if (rv)
      {
        Current.BeginOutputReadLine();
        Current.BeginErrorReadLine();
        StdInWriter = Current.StandardInput;
      }
      return rv;
    }

    public void Abort()
    {
      if (!Current.HasExited)
        Current.Kill();
    }
  }

  internal class RedirectedProcessEventArgs(string filename, string arguments) : EventArgs
  {
    public string FileName { get; private set; } = filename;
    public string Arguments { get; private set; } = arguments;
  }
}
