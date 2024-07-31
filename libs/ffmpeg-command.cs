using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ffmpeg_ytdlp_gui.libs
{
  using FFmpegBatchList = Dictionary<ffmpeg_command, IEnumerable<string>>;

  public partial class ffmpeg_command : IEnumerable<string>
  {
    public static string CreateBatch(FFmpegBatchList list,Action<ffmpeg_command,string> callback = null)
    {
      var sb = new StringBuilder();
      var commandlines = list.SelectMany(
        pair => pair.Value.Select(
          path =>
          {
            var command = pair.Key;
            callback?.Invoke(command,path);
            return command.GetCommandLine(path);
          }
        )
      );

      sb.AppendLine("@ECHO OFF");
      foreach (var commandline in commandlines)
        sb.AppendLine(commandline);
      sb.AppendLine("PAUSE");

      return sb.ToString();
    }

    // instances
    protected int FileIndex = 1;
    private string _file_prefix;
    private string _file_suffix;
    private string _file_base;
    private Encoding CP932;

    [GeneratedRegex(@"^(?:\d{2}:)?\d{2}:\d{2}(?:\.\d+)?$")]
    private static partial Regex IsDateTime();

    [GeneratedRegex(@"^\d+(?:\.\d+)?(?:s|ms|us)?$", RegexOptions.IgnoreCase, "ja-JP")]
    private static partial Regex IsSecondTime();

    [GeneratedRegex(@"^(\w+)$")]
    private static partial Regex IsWord();

    [GeneratedRegex(@"\s")]
    private static partial Regex HasSpace();

    [GeneratedRegex(@"[;,]+")]
    protected static partial Regex SplitCommaColon();

    protected Dictionary<string, string> filters;
    protected Dictionary<string, string> options;
    protected string InputPath;

    public string OutputPath { get; set; }
    public string OutputExtension { get; set; }
    public string ffmpegPath { get; set; }
    public bool bAudioOnly { get; set; }
    public string EncoderType { get; set; }
    public int IndexOfGpuDevice { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public IEnumerable<string> AdditionalOptions { get; } = new List<string>();
    public IEnumerable<string> AdditionalPreOptions { get; set; } = new List<string>();
    public bool MultiFileProcess { get; set; } = false;
    public bool IsLandscape { get; set; } = false;
    public bool Overwrite { get; set; } = false;

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
        if (options.TryGetValue("lookahead",out string value))
          rv = int.Parse(value);

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
        options["acodec"] = String.IsNullOrEmpty(value) ? "-an" : $"-c:a {value}";
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

    public decimal VFrameRate
    {
      set => options["r:v"] = value > 0 ? $"-r:v {value}" : string.Empty;
    }

    public ffmpeg_command(string ffmpegpath)
    {
      try
      {
        CP932 = Encoding.GetEncoding(932);
      }
      catch
      {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        CP932 = Encoding.GetEncoding(932);
      }

      filters = [];
      options = new Dictionary<string, string>
      {
        ["tag:v"] = "-tag:v hvc1",
        ["vcodec"] = "-c:v copy",
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

      if (!String.IsNullOrEmpty(ffmpegpath) && File.Exists(ffmpegpath))
        ffmpegPath = ffmpegpath;
    }

    public virtual IEnumerator<string> GetEnumerator()
    {
      yield return "-hide_banner";
      yield return "-y";
      yield return "-loglevel info";

      if (options.TryGetValue("ss",out string ss) && !string.IsNullOrEmpty(ss))
        yield return $"-ss {ss}";
      if (options.TryGetValue("to",out string to) && !string.IsNullOrEmpty(to))
        yield return $"-to {to}";

      if(AdditionalPreOptions.Count() > 0)
        foreach (var option in AdditionalPreOptions)
          yield return option.Trim();

      if(!string.IsNullOrEmpty(InputPath))
        yield return $"-i \"{InputPath}\"";

      if (bAudioOnly)
      {
        yield return "-vn";
      }
      else
      {
        yield return options["vcodec"];
      }

      if(AdditionalOptions.Count() > 0)
        foreach(var option in AdditionalOptions)
          yield return option.Trim();

      yield return options["acodec"];
      if (options.TryGetValue("b:a", out string ba) && !string.IsNullOrEmpty(ba))
        yield return ba;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator)GetEnumerator();
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

    public ffmpeg_command vFrameRate(decimal fps)
    {
      VFrameRate = fps;
      return this;
    }

    public virtual ffmpeg_command vcodec(string strCodec, int indexOfGpuDevice = 0)
    {
      options["vcodec"] = string.IsNullOrEmpty(strCodec) ? null : $"-c:v {strCodec}";
      return this;
    }

    public virtual ffmpeg_command vBitrate(int value, bool bCQ = false)
    {
      // copy nothing to do...
      return this;
    }

    public virtual ffmpeg_command crop(bool remove = true)
    {
      if(remove)
        options.Remove("crop");

      return this;
    }

    public virtual ffmpeg_command crop(bool hw,decimal width, decimal height, decimal x = -1, decimal y = -1)
    {
      // copy nothing to do...
      return this;
    }

    public virtual ffmpeg_command crop(decimal width, decimal height, decimal x = -1, decimal y = -1)
    {
      // copy nothing to do...
      return this;
    }

    public ffmpeg_command setFilter(string name,string value)
    {
      filters[name] = value;
      return this;
    }
    public ffmpeg_command removeFilter(string name)
    {
      filters.Remove(name);
      return this;
    }
    public string getFilter(string name)
    {
      return filters.ContainsKey(name) && filters[name] != null ? filters[name] : null;
    }

    public ffmpeg_command setOptions(string options)
    {
      var list = AdditionalOptions as List<string>;
      foreach (var option in SplitCommaColon().Split(options))
      {
        string str = option.Trim();
        if (!list.Contains(str))
          list.Add(str);
      }
      return this;
    }

    public ffmpeg_command setOptions(IEnumerable<string> options)
    {
      var list = AdditionalOptions as List<string>;
      foreach (var option in options)
        if (!list.Contains(option))
          list.Add(option);

      return this;
    }

    public ffmpeg_command clearOptions()
    {
      var list = AdditionalOptions as List<string>;
      list.Clear();

      return this;
    }

    public ffmpeg_command setPreOptions(string options)
    {
      var list = AdditionalPreOptions as List<string>;
      foreach (var option in SplitCommaColon().Split(options))
      {
        string str = option.Trim();
        if (!list.Contains(str))
          list.Add(str);
      }
      return this;
    }
    public ffmpeg_command setPreOptions(IEnumerable<string> options)
    {
      var list = AdditionalPreOptions as List<string>;
      foreach (var option in options)
        if (!list.Contains(option))
          list.Add(option);

      return this;
    }

    public ffmpeg_command preset(string str)
    {
      Preset = str;
      return this;
    }

    public ffmpeg_command OutputDirectory(string dir = ".")
    {
      OutputPath = string.IsNullOrEmpty(dir) ? "." : dir.Trim();
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
      FileBase = basename.Trim();
      return this;
    }

    public ffmpeg_command OutputContainer(string extension)
    {
      OutputExtension = extension;
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
      if (HasSpace().IsMatch(ffmpegPath))
        command = $"\"{ffmpegPath}\"";

      return $"{command} {GetCommandLineArguments(strInputPath)}";
    }

    public IEnumerable<string> GetArguments(string strInputPath)
    {
      InputPath = strInputPath;
      var args = this.ToList();

      string strOutputFileName = CreateOutputFileName(strInputPath);
      string strOutputFilePath = Path.Combine(OutputPath, strOutputFileName);
      if (strOutputFilePath == strInputPath)
        throw new Exception("入力ファイルと出力ファイルが同じです。");

      args.Add($"\"{strOutputFilePath}\"");

      return args;
    }

    public string CreateBatch(string filename,IEnumerable<string> files)
    {
      var commandlines = files.Select(file => GetCommandLine(file));
      var sb = new StringBuilder();
      sb.AppendLine("@ECHO OFF");
      foreach (var commandline in commandlines)
        sb.AppendLine(commandline);

      sb.AppendLine("PAUSE");

      return sb.ToString();
    }

    protected static string EvalTimeString(string strTime)
    {
      string rv = null;
      if (!string.IsNullOrEmpty(strTime))
      {
        var m1 = IsDateTime().Match(strTime);
        var m2 = IsSecondTime().Match(strTime);

        rv = null;
        if (m1.Success || m2.Success)
          rv = strTime;
      }
      return rv;
    }

    protected string CreateOutputFileName(string strInputPath)
    {
      string strOutputFileName = Path.GetFileName(strInputPath);
      string basename = string.IsNullOrEmpty(FileBase) ? Path.GetFileNameWithoutExtension(strInputPath) : string.Format("{0}{1}",FileBase,MultiFileProcess ? FileIndex++ : string.Empty);

      if (string.IsNullOrEmpty(OutputExtension))
      {
        if (bAudioOnly)
        {
          var m = IsWord().Match(options["acodec"]);
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
      }
      else
      {
        strOutputFileName = $"{FilePrefix}{basename}{FileSuffix}{OutputExtension}";
      }

      // ファイルが存在する場合は変更
      string fullpath = Path.Combine(OutputPath,strOutputFileName);
      if(File.Exists(fullpath) && !Overwrite)
      {
        string filename = Path.GetFileNameWithoutExtension(strOutputFileName);
        string extension = Path.GetExtension(strOutputFileName);
        string newname;
        int num = 1;
        do
        {
          newname = $"{filename}.{num++}{extension}";
          fullpath = Path.Combine(OutputPath, newname);
        } while (File.Exists(fullpath));

        strOutputFileName = newname;
      }

      return strOutputFileName;
    }

  }
}
