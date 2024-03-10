using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ffmpeg_command_builder
{
  internal abstract class ffmpeg_command
  {
    public abstract string GetCommandLineArguments(string strInputPath);
    public abstract ffmpeg_command vcodec(string strCodec, int indexOfGpuDevice = 0);
    public abstract ffmpeg_command vBitrate(int value, bool bCQ = false);

    // instances
    protected Dictionary<string,string> filters;
    protected Dictionary<string, string> options;
    protected string OutputPath;
    protected string ffmpegPath = "ffmpeg";
    protected bool bAudioOnly = false;
    protected string EncoderType = "hevc";
    protected int IndexOfGpuDevice = 0;
    protected string FilePrefix = "";
    protected string FileSuffix = "";

    protected void Initialize(string ffmpegPath)
    {
      filters = new Dictionary<string, string>();
      options = new Dictionary<string, string>
      {
        ["default"] = "-hide_banner -y",
        ["tag:v"] = "-tag:v hvc1",
        ["acodec"] = "-c:a copy",
        ["b:v"] = string.Empty,
        ["preset"] = string.Empty
      };
      OutputPath = ".";

      if (!String.IsNullOrEmpty(ffmpegPath) && File.Exists(ffmpegPath))
        this.ffmpegPath = ffmpegPath;
    }

    protected string CreateOutputFileName(string strInputPath)
    {
      string strOutputFileName = Path.GetFileName(strInputPath);
      string basename = Path.GetFileNameWithoutExtension(strInputPath);
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

    public ffmpeg_command Starts(string strTime)
    {
      if (!string.IsNullOrEmpty(strTime))
      {
        var m1 = Regex.Match(strTime, @"^(?:\d{2}:)?\d{2}:\d{2}(?:\.\d+)?$");
        var m2 = Regex.Match(strTime, @"^\d+(?:\.\d+)?(?:s|ms|us)?$",RegexOptions.IgnoreCase);

        options["ss"] = null;
        if (m1.Success || m2.Success)
          this.options["ss"] = $"-ss {strTime}";
      }
      else
      {
        options["ss"] = null;
      }

      return this;
    }

    public ffmpeg_command To(string strTime)
    {
      if (!string.IsNullOrEmpty(strTime))
      {
        var m1 = Regex.Match(strTime, @"^(?:\d{2}:)?\d{2}:\d{2}(?:\.\d+)?$");
        var m2 = Regex.Match(strTime, @"^\d+(?:\.\d+)?(?:s|ms|us)?$",RegexOptions.IgnoreCase);

        options["to"] = null;
        if (m1.Success || m2.Success)
          options["to"] = $"-to {strTime}";
      }
      else
      {
        options["to"] = null;
      }

      return this;
    }

    public ffmpeg_command acodec(string strCodec)
    {
      if (!String.IsNullOrEmpty(strCodec))
        this.options["acodec"] = $"-c:a {strCodec}";

      return this;
    }

    public ffmpeg_command aBitrate(int bitrate)
    {
      if (bitrate > 0)
        this.options["b:a"] = $"-b:a {bitrate}K";
      else
        this.options.Remove("b:a");
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
      options["preset"] = $"-preset {str}";
      return this;
    }

    public ffmpeg_command outputPath(string path)
    {
      OutputPath = string.IsNullOrEmpty(path) ? "." : path;
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

    public ffmpeg_command audioOnly(bool b)
    {
      bAudioOnly = b;
      return this;
    }

    public ffmpeg_command lookAhead(int frames)
    {
      if (frames <= 0 && options.ContainsKey("lookahead"))
        options.Remove("lookahead");

      if (frames > 0)
        options["lookahead"] = frames.ToString();

      return this;
    }

    public string GetCommandLine(string strInputPath)
    {
      string command = ffmpegPath;
      if (Regex.IsMatch(ffmpegPath, @"\s"))
        command = $"\"{ffmpegPath}\"";

      return $"{command} {GetCommandLineArguments(strInputPath)}";
    }

    public Process InvokeCommand(string strInputPath,bool suspend = false)
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

      var process = new Process()
      {
        StartInfo = psi,
        EnableRaisingEvents = true,
      };

      if(!suspend)
        process.Start();

      return process;
    }
  }
}
