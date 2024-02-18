using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ffmpeg_command_builder
{
  internal class ffmpeg_command
  {
    // instances
    protected Dictionary<string,string> filters;
    protected Dictionary<string, string> options;
    protected string OutputPath;
    protected string ffmpegPath = "ffmpeg";
    protected bool bAudioOnly = false;

    public ffmpeg_command(string ffmpegPath = "")
    {
      filters = new Dictionary<string,string>();
      options = new Dictionary<string, string>
      {
        ["default"] = "-hide_banner -y",
        ["tag:v"] = "-tag:v hvc1",
        ["acodec"] = "-c:a copy",
        ["vcodec"] = "-c:v hevc_nvenc",
        ["b:v"] = "-b:v 0 -cq 25",
        ["preset"] = "-preset p6",
      };
      OutputPath = ".";

      if (!String.IsNullOrEmpty(ffmpegPath) && File.Exists(ffmpegPath))
      {
        this.ffmpegPath = ffmpegPath;
      }
      else
      {
        string[] ffmpegPathes = FindInPath("ffmpeg");
        if (ffmpegPathes.Length == 0)
          throw new Exception("ffmpegコマンドが環境変数PATHから見つかりません。");

        this.ffmpegPath = ffmpegPathes[0];
      }
    }

    private string[] FindInPath(string CommandName)
    {
      List<string> PathList = new List<string>();

      //環境変数%PATH%取得し、カレントディレクトリを連結。配列への格納
      string strPathEnv = Directory.GetCurrentDirectory() + ";" + Environment.ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("PATH"));
      string[] dirPathes = strPathEnv.Split(new Char[] { ';' });

      //正規表現に使用するため、%PATHEXT%の取得・ピリオド文字の変換及び配列への格納
      string[] pathext = Environment.GetEnvironmentVariable("PATHEXT").Replace(".", @"\.").Split(new Char[] { ';' });

      //検索するファイル名の正規表現
      Regex regex = new Regex(
        $"^{CommandName}({String.Join("|", pathext)})?$",
        RegexOptions.IgnoreCase
      );

      foreach (string dir in dirPathes)
      {
        if (!Directory.Exists(dir))
          continue;

        foreach (string file in Directory.GetFiles(dir))
        {
          if (regex.Match(Path.GetFileName(file)).Success == true)
          {
            PathList.Add(file);
          }
        }
      }

      return PathList.ToArray();
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
          this.options["to"] = $"-to {strTime}";
      }
      else
      {
        options["to"] = null;
      }

      return this;
    }

    public ffmpeg_command vcodec(string strCodec)
    {
      if (!String.IsNullOrEmpty(strCodec))
      {
        options["vcodec"] = $"-c:v {strCodec}";
        if (strCodec != "hevc_nvenc" && options.ContainsKey("tag:v"))
          options.Remove("tag:v");
      }
      return this;
    }

    public ffmpeg_command acodec(string strCodec)
    {
      if (!String.IsNullOrEmpty(strCodec))
        this.options["acodec"] = $"-c:a {strCodec}";

      return this;
    }

    public ffmpeg_command vBitrate(int value,bool bCQ = false)
    {
      if (value <= 0)
      {
        options["b:v"] = "-b:v 0 -cq 25";
      }
      else
      {
        options["b:v"] = bCQ ? $"-b:v 0 -cq {value}" : $"-b:v {value}K";
      }
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

    public ffmpeg_command audioOnly(bool b)
    {
      bAudioOnly = b;
      return this;
    }

    public string GetCommandLineArguments(string strInputPath)
    {
      var args = new List<string> { options["default"] };

      if(!bAudioOnly)
        args.Add("-hwaccel cuda -hwaccel_output_format cuda");

      if (options.ContainsKey("ss") && !string.IsNullOrEmpty(options["ss"]))
        args.Add(options["ss"]);
      if(options.ContainsKey("to") && !string.IsNullOrEmpty(options["to"]))
        args.Add(options["to"]);

      args.Add($"-i \"{strInputPath}\"");

      if (bAudioOnly)
      {
        args.Add("-vn");
      }
      else
      {
        // ここにビデオフィルター
        if (filters.Count > 0)
        {
          var strFilters = new List<string>();
          if (filters.ContainsKey("bwdif_cuda"))
            strFilters.Add("bwdif_cuda=" + filters["bwdif_cuda"]);
          if (filters.ContainsKey("scale_cuda"))
            strFilters.Add("scale_cuda=" + filters["scale_cuda"]);
          if (filters.ContainsKey("transpose"))
            strFilters.Add($"hwdownload,format=nv12,transpose={filters["transpose"]},hwupload_cuda");

          string strFilter = string.Join(",", strFilters.ToArray());
          args.Add($"-vf \"{strFilter}\"");
        }
        args.Add(options["vcodec"]);
        args.Add(options["preset"]);
        args.Add(options["b:v"]);

        if(options.ContainsKey("tag:v"))
          args.Add(options["tag:v"]);
      }

      args.Add(options["acodec"]);
      if (options.ContainsKey("b:a") && !string.IsNullOrEmpty(options["b:a"]))
        args.Add(options["b:a"]);

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
              strOutputFileName = basename + ".aac";
              break;
            case "libmp3lame":
              strOutputFileName = basename + ".mp3";
              break;
          }
        }
      }
      else
      {
        strOutputFileName = basename + ".mp4";
      }

      string strOutputFilePath = Path.Combine(OutputPath, strOutputFileName);
      if(strOutputFilePath == strInputPath)
        throw new Exception("入力ファイルと出力ファイルが同じです。");

      args.Add($"\"{strOutputFilePath}\"");

      return string.Join(" ", args.ToArray());
    }

    public string GetCommandLine(string strInputPath)
    {
      return $"{this.ffmpegPath} {this.GetCommandLineArguments(strInputPath)}";
    }

    public Process InvokeCommand(string strInputPath,bool suspend = false)
    {
      var psi = new ProcessStartInfo()
      {
        FileName = this.ffmpegPath,
        Arguments = this.GetCommandLineArguments(strInputPath),
        UseShellExecute = false,
        RedirectStandardError = true,
        RedirectStandardInput = true,
        CreateNoWindow = true
      };

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
