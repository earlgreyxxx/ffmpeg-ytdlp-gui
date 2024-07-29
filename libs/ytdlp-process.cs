using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  internal class ytdlp_process : RedirectedProcess
  {
    public string Url { get; set; }
    public string CookieBrowser { get; set; } = "";
    public string CookiePath { get; set; }
    public string OutputPath { get; set; }
    public string OutputFile { get; set; }
    public string DownloadFile { get; set; }
    public bool Separated { get; set; }
    public string AudioFormat { private get; set; }
    public string VideoFormat { private get; set; }
    public string MovieFormat { private get; set; }

    public ytdlp_process() : base("yt-dlp")
    {
      ProcessExited += (s, e) => Debug.WriteLine("yt-dlpプロセス終了");
    }

    private IEnumerable<string> CreateArguments(string[] additionalOptions = null)
    {
      var options = new List<string>()
      { 
        Url,
        "-w",
        "--encoding UTF-8",
        "--no-continue",
        "--no-mtime",
        "--progress-delta 1"
      };

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

    public async void DownloadAsync()
    {
      var sb = new List<string>();

      if (!Separated && !string.IsNullOrEmpty(MovieFormat))
      {
        sb.Add(MovieFormat);
      }
      else if(Separated && (!string.IsNullOrEmpty(VideoFormat) || !string.IsNullOrEmpty(AudioFormat)))
      {
        if (!string.IsNullOrEmpty(VideoFormat))
          sb.Add(VideoFormat);
        if (!string.IsNullOrEmpty(AudioFormat))
          sb.Add(AudioFormat);
      }
      else
      {
        throw new Exception("ダウンロードする対象が指定されていません。");
      }

      string[] formats = sb.Count > 0 ? [$"-f {string.Join('+', sb.ToArray())}"] : null;

      psi.StandardOutputEncoding = Encoding.UTF8;
      psi.StandardErrorEncoding = Encoding.UTF8;

      await StartAsync(string.Join(' ',CreateArguments(formats).ToArray()));
    }

    public async Task<MediaInformation> getMediaInformation()
    {
      if (Url == null)
        return null;

      var options = new List<string>()
      {
        Url,
        "-j"
      };

      if (!string.IsNullOrEmpty(CookiePath) && File.Exists(CookiePath))
        options.Add($"--cookies \"{CookiePath}\"");
      else if (!string.IsNullOrEmpty(CookieBrowser))
        options.Add($"--cookies-from-browser {CookieBrowser}");

      var log = new List<string>();
      var arguments = string.Join(' ', options.ToArray());
      MediaInformation info = null;

      Debug.WriteLine($"{Command} {arguments}");

      StdOutReceived += data => log.Add(data);
      ProcessExited += (s, e) =>
      {
        if(Current.ExitCode == 0)
          info = new MediaInformation(string.Join(string.Empty, log.ToArray()));
      };

      await StartAsync(arguments);
      return info;
    }
  }
}
