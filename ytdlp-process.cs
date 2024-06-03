using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ffmpeg_command_builder
{
  internal class ytdlp_process
  {
    public event Action<string> OnDataOutputReceived;
    public event Action<string> OnDataErrorReceived;
    public event Action<Process> OnProcessDone;

    public string Url { get; set; }
    public string Command { get; set; } = "yt-dlp";
    public string CookieBrowser { get; set; } = "";
    public string CookiePath { get; set; }
    public string OutputPath { get; set; }
    public string OutputFile { get; set; }

    private RedirectedProcess Current = null;

    private IEnumerable<string> CreateArguments(string[] additionalOptions = null)
    {
      var options = new List<string>() { Url };

      if (!string.IsNullOrEmpty(CookiePath) && File.Exists(CookiePath))
        options.Add($"--cookies \"{CookiePath}\"");
      else if (!string.IsNullOrEmpty(CookieBrowser))
        options.Add($"--cookies-from-browser {CookieBrowser}");

      if (!string.IsNullOrEmpty(OutputPath))
        options.Add($"-P \"{OutputPath}\"");

      if(!string.IsNullOrEmpty(OutputFile))
        options.Add($"-o \"{OutputFile}\"");

      if(additionalOptions != null && additionalOptions.Length > 0)
        options.AddRange(additionalOptions);

      return options;
    }

    public async Task Download(string fid1, string fid2 = null)
    {
      var sb = new List<string>();

      if (!string.IsNullOrEmpty(fid1))
        sb.Add(fid1);
      if (!string.IsNullOrEmpty(fid2))
        sb.Add(fid2);

      var strarr = sb.Count > 0 ? new string[] { $"-f {string.Join('+', sb.ToArray())}" } : null;
      if (strarr == null)
        throw new Exception("ダウンロードする対象が指定されていません。");

      await Download(strarr);
    }


    public async Task Download(string[] additionals = null)
    {
      string arguments = string.Join(' ',CreateArguments(additionals).ToArray());
      Debug.WriteLine($"{Command} {arguments}");

      var process = Current = new RedirectedProcess(Command,arguments);
      process.OnStdOutReceived += data => OnDataOutputReceived?.Invoke(data);
      process.OnStdErrReceived += data=> OnDataErrorReceived?.Invoke(data);
      process.OnProcessExited += (s, e) =>
      {
        OnProcessDone?.Invoke(process.Current);
        Current = null;
      };

      await process.StartAsync();
    }

    public void Kill()
    {
      if (Current == null)
        throw new Exception("既にダウンロードプロセスは終了しています。");

      Current.Abort();
    }

    public async Task<MediaInformation> getMediaInformation()
    {
      if (Url == null)
        return null;

      var options = new List<string>() { Url, "-j" };
      if (!string.IsNullOrEmpty(CookiePath) && File.Exists(CookiePath))
        options.Add($"--cookies \"{CookiePath}\"");
      else if (!string.IsNullOrEmpty(CookieBrowser))
        options.Add($"--cookies-from-browser {CookieBrowser}");

      var arguments = string.Join(' ', options.ToArray());
      Debug.WriteLine($"{Command} {arguments}");
      var process = new RedirectedProcess(Command, arguments);
      MediaInformation info = null;

      var log = new List<string>();

      process.OnStdOutReceived += data => log.Add(data);
      process.OnStdErrReceived += OnDataErrorReceived;
      process.OnProcessExited += (s, e) =>
      {
        if(process.Current.ExitCode == 0)
          info = new MediaInformation(string.Join(string.Empty, log.ToArray()));
      };

      await process.StartAsync();
      return info;
    }
  }
}
