﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  public class ytdlp_process : RedirectedProcess
  {
    const string YTDLPNAME = "yt-dlp";

    public string? Url { get; set; }
    public string? CookieBrowser { get; set; } = "";
    public string? CookiePath { get; set; }
    public string? OutputPath { get; set; }
    public string? OutputFile { get; set; }
    public string? DownloadFile { get; set; }
    public bool Separated { get; set; }
    public string? AudioFormat { private get; set; }
    public string? VideoFormat { private get; set; }
    public string? MovieFormat { private get; set; }

    private string? _json_text;
    public string? JsonText
    {
      get
      {
        return _json_text;
      }
      set
      {
        _json_text = value;
        if (!string.IsNullOrEmpty(value))
        {
          if(string.IsNullOrEmpty(JsonFileName))
            JsonFileName = GetTemporaryFileName($"ytdlp.{Guid.NewGuid()}.", ".json");

          using (var sw = new StreamWriter(JsonFileName, false))
          {
            sw.WriteLine(value);
          }
        }
      }
    }
    public string? JsonFileName { get; private set; } 

    private string[]? _downloadfiles;
    public string[]? DownloadFiles
    {
      get
      {
        return _downloadfiles;
      }
      set
      {
        _downloadfiles = value;
        if(_downloadfiles != null)
          DownloadFile = _downloadfiles?.GetValue(0) as string;
        else
          DownloadFile = null;
      }
    }

    public ytdlp_process() : base(YTDLPNAME)
    {
      if (CustomProcess.FindInPath(YTDLPNAME).Length <= 0)
        throw new Exception($"{YTDLPNAME} not found in PATH envrionment.");

      ProcessExited += (s, e) => Debug.WriteLine("yt-dlpプロセス終了");
    }

    ~ytdlp_process()
    {
      if(!string.IsNullOrEmpty(JsonFileName) && File.Exists(JsonFileName))
        File.Delete(JsonFileName);
    }

    private ytdlp_process Clone()
    {
      return new ytdlp_process()
      {
        Url = Url,
        CookieBrowser = CookieBrowser,
        CookiePath = CookiePath,
        OutputPath = OutputPath,
        OutputFile = OutputFile,
        AudioFormat = AudioFormat,
        VideoFormat = VideoFormat,
        MovieFormat = MovieFormat,
        Separated = Separated,
      };
    }

    private IEnumerable<string> CreateArguments(string[]? additionalOptions = null)
    {
      var options = new List<string>()
      { 
        "-w",
        "--encoding UTF-8",
        "--no-continue",
        "--no-mtime",
        "--progress-delta 1"
      };

      options.Insert(0,string.IsNullOrEmpty(JsonText) ? (Url ?? throw new NullReferenceException("URL not null")) : $"--load-info-json \"{JsonFileName}\"");

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
    
    private IEnumerable<string> CreateFormatOptions()
    {
      List<string> options = [];

      if (!Separated && !string.IsNullOrEmpty(MovieFormat))
      {
        options.Add(MovieFormat);
      }
      else if(Separated && (!string.IsNullOrEmpty(VideoFormat) || !string.IsNullOrEmpty(AudioFormat)))
      {
        if (!string.IsNullOrEmpty(VideoFormat))
          options.Add(VideoFormat);
        if (!string.IsNullOrEmpty(AudioFormat))
          options.Add(AudioFormat);
      }

      return options;
    }

    public async void DownloadAsync()
    {
      var sb = CreateFormatOptions();

      string[] formats = sb.Count() > 0 ? [$"-f {string.Join('+', sb.ToArray())}"] : [];

      psi.StandardOutputEncoding = Encoding.UTF8;
      psi.StandardErrorEncoding = Encoding.UTF8;
      psi.StandardInputEncoding = Encoding.UTF8;

      var arguments = string.Join(' ', CreateArguments(formats).ToArray());
      Debug.WriteLine($"{Command} {arguments}");
      
      await StartAsync(arguments);
    }

    public async Task<MediaInformation?> getMediaInformation()
    {
      if (Url == null)
        throw new NullReferenceException("URL not specified");

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
      MediaInformation? info = null;

      Debug.WriteLine($"{Command} {arguments}");

      StdOutReceived += data => log.Add(data);
      ProcessExited += (s, e) =>
      {
        if (Current.ExitCode == 0)
          info = new MediaInformation(string.Join(string.Empty, log.ToArray()));
      };

      await StartAsync(arguments);
      return info;
    }

    public Task GetDownloadFileNames()
    {
      var parser = Clone();

      if (parser.Url == null)
        throw new NullReferenceException("URL not specified");

      var options = new List<string?>()
      {
        "--print filename",
      };
      options.Insert(0,string.IsNullOrEmpty(JsonText) ? parser.Url : $"--load-info-json \"{JsonFileName}\"");
      options.Add($"-f {string.Join('+', CreateFormatOptions().ToArray())}");

      if (!string.IsNullOrEmpty(CookiePath) && File.Exists(CookiePath))
        options.Add($"--cookies \"{CookiePath}\"");
      else if (!string.IsNullOrEmpty(CookieBrowser))
        options.Add($"--cookies-from-browser {CookieBrowser}");

      if (!string.IsNullOrEmpty(OutputFile))
        options.Add($"-o \"{OutputFile}\"");

      var log = new List<string>();
      var arguments = string.Join(' ', options.ToArray());

      string[] filenames = [];

      Debug.WriteLine($"{Command} {arguments}");

      parser.StdOutReceived += data => log.Add(data);
      parser.ProcessExited += (s, e) =>
      {
        if (parser.ExitCode == 0)
        {
          DownloadFiles = string.Join(string.Empty, log.ToArray())
                                .Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                                .Select(filename => Path.Combine(OutputPath ?? string.Empty, filename))
                                .ToArray();
        }
      };

      return parser.StartAsync(arguments);
    }
  }
}
