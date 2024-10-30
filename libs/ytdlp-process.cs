using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  public class ytdlp_process : RedirectedProcess
  {
    private static readonly Encoding UTF8N = new UTF8Encoding(false);

    const string YTDLPNAME = "yt-dlp";

    public string? Url { get; set; }
    public string? CookieBrowser { get; set; } = "none";
    public string? CookiePath { get; set; }
    public string? OutputPath { get; set; }
    public string? OutputFile { get; set; }
    public bool Separated { get; set; }
    public string? AudioFormat { private get; set; }
    public string? VideoFormat { private get; set; }
    public string? MovieFormat { private get; set; }
    public string? JsonText {  get; set; }

    private string? _config_dir;
    public string? ConfigDir
    {
      get => _config_dir;
      set {
        string v = value ?? string.Empty;
        if (string.IsNullOrEmpty(v))
          throw new Exception("argument is empty");
          
        if(v.Any(c => c == '%'))
          v = Environment.ExpandEnvironmentVariables(v);

        if (!Directory.Exists(v))
          throw new Exception("Directory is not exists");

        _config_dir = v;
      }
    }

    private string[]? _downloadfiles;
    public string? DownloadFile
    {
      get => _downloadfiles != null && _downloadfiles.Length > 0 ? _downloadfiles[0] : null;
    }

    public string[]? DownloadFiles
    {
      get => _downloadfiles;
      set => _downloadfiles = value;
    }

    public ytdlp_process(string filename = "")
    {
      var command = !string.IsNullOrEmpty(filename) ? filename : YTDLPNAME;

      // ファイルパスではなくコマンド名だけの場合は環境変数PATHから存在チェックを行う
      if (Regex.IsMatch(command, @"^[\w\-]+$"))
      {
        if (CustomProcess.FindInPath(command).Length <= 0)
          throw new Exception($"{command} not found in PATH envrionment.");
      }
      Command = command;
      InitializeInstance();

#if DEBUG
      ProcessExited += (s, e) => Debug.WriteLine($"Exit code of yt-dlp is {(s as Process)?.ExitCode}");
#endif
    }

    private ytdlp_process Clone()
    {
      return new ytdlp_process()
      {
        Command = Command,
        Url = Url,
        CookieBrowser = CookieBrowser,
        CookiePath = CookiePath,
        OutputPath = OutputPath,
        OutputFile = OutputFile,
        AudioFormat = AudioFormat,
        VideoFormat = VideoFormat,
        MovieFormat = MovieFormat,
        Separated = Separated,
        ConfigDir = ConfigDir,
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

      options.Insert(0,string.IsNullOrEmpty(JsonText) ? ($"\"{Url}\"" ?? throw new NullReferenceException("URL not null")) : "--load-info-json -");

      if (!string.IsNullOrEmpty(CookiePath) && File.Exists(CookiePath))
        options.Add($"--cookies \"{CookiePath}\"");
      else if (!string.IsNullOrEmpty(CookieBrowser) && CookieBrowser != "none" && CookieBrowser != "file")
        options.Add($"--cookies-from-browser {CookieBrowser}");

      if (!string.IsNullOrEmpty(OutputPath))
        options.Add($"-P \"{OutputPath}\"");

      if(!string.IsNullOrEmpty(OutputFile))
        options.Add($"-o \"{OutputFile}\"");

      if(additionalOptions != null && additionalOptions.Length > 0)
        options.AddRange(additionalOptions);

      if (!string.IsNullOrEmpty(ConfigDir) && Directory.Exists(ConfigDir))
        options.Add($"--config-locations \"{ConfigDir}\"");

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

      string[] formats = sb.Count() > 0 ? [$"-f \"{string.Join('+', sb.ToArray())}\""] : [];

      psi.StandardOutputEncoding = UTF8N;
      psi.StandardErrorEncoding = UTF8N;
      psi.StandardInputEncoding = UTF8N;

      var arguments = string.Join(' ', CreateArguments(formats).ToArray());
      Debug.WriteLine($"{Command} {arguments}");
      Start(arguments);

      StdInWriter?.WriteLine(JsonText);
      StdInWriter?.Dispose();

      await WaitForExitAsync();
    }

    public async Task<IEnumerable<MediaInformation>?> GetMediaInformations()
    {
      if (Url == null)
        throw new NullReferenceException("URL not specified");

      var options = new List<string>()
      {
        $"\"{Url}\"",
        "-j"
      };

      if (CookieBrowser == "file" && !string.IsNullOrEmpty(CookiePath) && File.Exists(CookiePath))
        options.Add($"--cookies \"{CookiePath}\"");
      else if (!string.IsNullOrEmpty(CookieBrowser) && CookieBrowser != "none" && CookieBrowser != "file")
        options.Add($"--cookies-from-browser {CookieBrowser}");

      var log = new List<string>();
      var arguments = string.Join(' ', options.ToArray());
      IEnumerable<MediaInformation>? infoes = null;

      Debug.WriteLine($"{Command} {arguments}");

      StdOutReceived += data => log.Add(data);
      ProcessExited += (s, e) =>
      {
        if (Current.ExitCode == 0)
        {
          infoes = log.Where(line => !string.IsNullOrEmpty(line?.Trim()))
                      .Select(line => new MediaInformation(line));
        }
        else
        {
          infoes = null;
        }
      };

      await StartAsync(arguments);
      return infoes;
    }

    public async Task<MediaInformation?> GetMediaInformation()
    {
      var infoes = await GetMediaInformations();

      if (infoes == null || infoes.Count() == 0)
        throw new Exception("YT-DLPがエラーを返し正常に終了できませんでした。");

      if (infoes.Count() > 1)
        throw new MediaInformationException(infoes.ToArray());

      return infoes.FirstOrDefault();
    }

    public Task GetDownloadFileNames()
    {
      var parser = Clone();

      if (parser.Url == null)
        throw new NullReferenceException("URL not specified");

      var options = new List<string?>()
      {
        "--encoding UTF-8",
        "--print filename"
      };
      options.Insert(0,string.IsNullOrEmpty(JsonText) ? $"\"{parser.Url}\"" : "--load-info-json -");

      var formats = CreateFormatOptions().ToArray();
      if(formats.Length > 0)
        options.Add($"-f \"{string.Join('+', formats)}\"");

      if (CookieBrowser == "file" && !string.IsNullOrEmpty(CookiePath) && File.Exists(CookiePath))
        options.Add($"--cookies \"{CookiePath}\"");
      else if (!string.IsNullOrEmpty(CookieBrowser) && CookieBrowser != "none" && CookieBrowser != "file")
        options.Add($"--cookies-from-browser {CookieBrowser}");

      if (!string.IsNullOrEmpty(OutputFile))
        options.Add($"-o \"{OutputFile}\"");

      var log = new List<string>();
      var arguments = string.Join(' ', options.ToArray());

      Debug.WriteLine($"{Command} {arguments}");

      parser.psi.StandardOutputEncoding = UTF8N;
      parser.psi.StandardErrorEncoding = UTF8N;
      parser.psi.StandardInputEncoding = UTF8N;

      parser.StdOutReceived += data => log.Add(data);
      parser.ProcessExited += (s, e) =>
      {
        if (parser.ExitCode == 0)
        {
          DownloadFiles = log.Where(line => !string.IsNullOrEmpty(line.Trim()))
                             .Select(filename => Path.Combine(OutputPath ?? string.Empty, filename))
                             .ToArray();
        }
      };

      parser.Start(arguments);
      parser.StdInWriter?.WriteLine(JsonText);
      parser.StdInWriter?.Dispose();

      return parser.WaitForExitAsync();
    }
  }
}
