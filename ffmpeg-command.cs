using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ffmpeg_command_builder
{
  internal abstract class ffmpeg_command
  {
    public abstract IEnumerable<string> GetArguments(string strInputPath);
    public abstract ffmpeg_command vcodec(string strCodec, int indexOfGpuDevice = 0);
    public abstract ffmpeg_command vBitrate(int value, bool bCQ = false);
    public abstract ffmpeg_command crop(decimal width, decimal height, decimal x, decimal y);
    public abstract ffmpeg_command crop(bool hw,decimal width, decimal height, decimal x, decimal y);

    // instances
    protected int FileIndex = 1;
    private string _file_prefix;
    private string _file_suffix;
    private string _file_base;

    protected Dictionary<string,string> filters;
    protected Dictionary<string, string> options;
    protected string InputPath;

    public string OutputPath { get; set; }
    public string ffmpegPath {  get; set; }
    public bool bAudioOnly {  get; set; }
    public string EncoderType {  get; set; }
    public int IndexOfGpuDevice {  get; set; }
    public int Width { get; set; }
    public int Height {  get; set; }

    public string FilePrefix
    {
      get => _file_prefix;
      set => _file_prefix = value.Trim();
    }
    public string FileSuffix
    {
      get => _file_suffix;
      set => _file_suffix = value.Trim();
    }
    public string FileBase
    {
      get => _file_base;
      set
      {
        _file_base = value.Trim();
        FileIndex = 1;
      }
    }
    public int LookAhead
    {
      set
      {
        if (value <= 0 && options.ContainsKey("lookahead"))
          options.Remove("lookahead");

        if (value > 0)
          options["lookahead"] = value.ToString();
      }
      protected get
      {
        int rv = 0;
        if (options.ContainsKey("lookahead"))
          rv = int.Parse(options["lookahead"]);

        return rv;
      }
    }
    public string Begin
    {
      get => options["ss"];
      set => options["ss"] = EvalTimeString(value);
    }
    public string End
    {
      get => options["to"];
      set => options["to"] = EvalTimeString(value);
    }

    public string ACodec
    {
      set
      {
        if (String.IsNullOrEmpty(value))
          throw new ArgumentNullException(nameof(value));

        options["acodec"] = $"-c:a {value}";
      }
    }
    public int ABitrate
    {
      set
      {
        if (value > 0)
          this.options["b:a"] = $"-b:a {value}K";
        else
          this.options.Remove("b:a");
      }
    }
    public string Preset
    {
      set => options["preset"] = $"-preset {value}";
    }

    public ffmpeg_command(string ffmpegpath)
    {
      filters = new Dictionary<string, string>();
      options = new Dictionary<string, string>
      {
        ["tag:v"] = "-tag:v hvc1",
        ["acodec"] = "-c:a copy",
        ["b:v"] = string.Empty,
        ["preset"] = string.Empty
      };
      OutputPath = ".";
      ffmpegPath = "ffmpeg";
      bAudioOnly = false;
      EncoderType = "hevc";
      IndexOfGpuDevice = 0;
      FileBase = string.Empty;
      FilePrefix = string.Empty;
      FileSuffix = string.Empty;
      Width = -1;
      Height = -1;

      if (!String.IsNullOrEmpty(ffmpegPath) && File.Exists(ffmpegPath))
        ffmpegPath = ffmpegpath;
    }

    public ffmpeg_command Starts(string strTime)
    {
      Begin = strTime;
      return this;
    }

    public ffmpeg_command To(string strTime)
    {
      End = strTime;
      return this;
    }

    public ffmpeg_command acodec(string strCodec)
    {
      ACodec = strCodec;
      return this;
    }

    public ffmpeg_command aBitrate(int bitrate)
    {
      ABitrate = bitrate;
      return this;
    }

    public ffmpeg_command setFilter(string name,string value)
    {
      filters[name] = value;
      return this;
    }
    public ffmpeg_command removeFilter(string name)
    {
      if(filters.ContainsKey(name))
        filters.Remove(name);
      return this;
    }

    public ffmpeg_command preset(string str)
    {
      Preset = str;
      return this;
    }

    public ffmpeg_command OutputPrefix(string prefix = "")
    {
      FilePrefix = prefix;
      return this;
    }

    public ffmpeg_command OutputSuffix(string suffix = "")
    {
      FileSuffix = suffix;
      return this;
    }

    public ffmpeg_command OutputBaseName(string basename = "")
    {
      FileBase = basename;
      return this;
    }

    public ffmpeg_command audioOnly(bool b)
    {
      bAudioOnly = b;
      return this;
    }

    public ffmpeg_command lookAhead(int frames)
    {
      LookAhead = frames;
      return this;
    }

    public ffmpeg_command hwdecoder(string decoder)
    {
      options["hwdecoder"] = decoder;
      return this;
    }

    public ffmpeg_command size(int w,int h)
    {
      Width = w;
      Height = h;
      return this;
    }

    public string GetCommandLineArguments(string strInputPath)
    {
      var args = GetArguments(strInputPath);
      return string.Join(" ", args.ToArray());
    }

    public string GetCommandLine(string strInputPath)
    {
      string command = ffmpegPath;
      if (Regex.IsMatch(ffmpegPath, @"\s"))
        command = $"\"{ffmpegPath}\"";

      return $"{command} {GetCommandLineArguments(strInputPath)}";
    }

    public CustomProcess InvokeCommand(string strInputPath,bool suspend = false)
    {
      var psi = new ProcessStartInfo()
      {
        FileName = ffmpegPath,
        Arguments = GetCommandLineArguments(strInputPath),
        UseShellExecute = false,
        RedirectStandardError = true,
        RedirectStandardInput = true,
        CreateNoWindow = true
      };

      psi.Environment.Add("AV_LOG_FORCE_NOCOLOR", "1");

      var process = new CustomProcess()
      {
        StartInfo = psi,
        EnableRaisingEvents = true,
        CustomFileName = strInputPath
      };

      if(!suspend)
        process.Start();

      return process;
    }

    public async void ToBatchFile(string filename,IEnumerable<string> files)
    {
      var commandlines = files.Select(file => GetCommandLine(file));

      using (var sw = new StreamWriter(filename, false, Encoding.GetEncoding(932)))
      {
        await sw.WriteLineAsync("@ECHO OFF");
        foreach (var commandline in commandlines)
          await sw.WriteLineAsync(commandline);

        await sw.WriteLineAsync("PAUSE");
      }
    }

    protected string EvalTimeString(string strTime)
    {
      string rv = null;
      if (!string.IsNullOrEmpty(strTime))
      {
        var m1 = Regex.Match(strTime, @"^(?:\d{2}:)?\d{2}:\d{2}(?:\.\d+)?$");
        var m2 = Regex.Match(strTime, @"^\d+(?:\.\d+)?(?:s|ms|us)?$", RegexOptions.IgnoreCase);

        rv = null;
        if (m1.Success || m2.Success)
          rv = strTime;
      }
      return rv;
    }

    protected string CreateOutputFileName(string strInputPath)
    {
      string strOutputFileName = Path.GetFileName(strInputPath);
      string basename = string.IsNullOrEmpty(FileBase) ? Path.GetFileNameWithoutExtension(strInputPath) : string.Format("{0}{1:D2}",FileBase,FileIndex++);

      if (bAudioOnly)
      {
        var m = Regex.Match(options["acodec"], @"(\w+)$");
        if (m.Success)
        {
          switch (m.Captures[0].Value)
          {
            case "aac":
              strOutputFileName = $"{FilePrefix}{basename}{FileSuffix}.aac";
              break;
            case "libmp3lame":
              strOutputFileName = $"{FilePrefix}{basename}{FileSuffix}.mp3";
              break;
          }
        }
      }
      else
      {
        strOutputFileName = $"{FilePrefix}{basename}{FileSuffix}.mp4";
      }

      return strOutputFileName;
    }
  }
}
