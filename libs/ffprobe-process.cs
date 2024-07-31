using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  public class ffprobe_process : RedirectedProcess
  {
    // Statics
    // ----------------------------------------------------------------------

    public static ffprobe_process CreateInstance(string filepath)
    {
      var process = new ffprobe_process(filepath);
      process.Initialize();

      return process;
    }

    public static async Task<ffprobe_process> CreateInstanceAsync(string filepath)
    {
      var process = new ffprobe_process(filepath);
      await process.InitializeAsync();

      return process;
    }

    // Instances
    // ----------------------------------------------------------------------
    private MediaContainerProperty containerProperty;
    private List<MediaStreamProperty> streamProperties;

    public string FileName { get; private set; }

    private ffprobe_process(string filepath) : base("ffprobe")
    {
      if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
        throw new Exception("指定されたファイルが存在しません");

      FileName = filepath;
      psi.StandardErrorEncoding = Encoding.UTF8;
      psi.StandardOutputEncoding = Encoding.UTF8;
      psi.StandardInputEncoding = Encoding.UTF8;

      ProcessExited += (s, e) => Debug.WriteLine("ffprobeプロセス終了");
    }

    public IEnumerable<MediaStreamProperty> getStreamProperties()
    {
      return streamProperties ?? null;
    }

    public MediaContainerProperty getContainerProperty()
    {
      return containerProperty ?? null;
    }

    public bool Initialize()
    {
      if (string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
        throw new Exception("ファイル名が指定されていないか、ファイルが存在しません");

      var options = new string[]
      {
        "-hide_banner",
        "-show_streams",
        "-show_format",
        "-output_format json",
        $"\"{FileName}\""
      };
      var log = new List<string>();
      var arguments = string.Join(' ', options);

      Debug.WriteLine($"{Command} {arguments}");
      bool rv = false;

      StdOutReceived += data => log.Add(data);

      // 終了を待つ
      rv = Start(arguments);
      Current.WaitForExit();

      if (Current.ExitCode == 0)
      {
        rv = MediaProperty.Factory(
          string.Join(string.Empty, log.ToArray()),
          out containerProperty,
          out streamProperties
        );
      }
      else
      {
        throw new Exception("ffprobeが失敗しました。");
      }
      return rv;
    }

    public async Task<bool> InitializeAsync()
    {
      if (string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
        throw new Exception("ファイル名が指定されていないか、ファイルが存在しません");

      var options = new string[]
      {
        "-hide_banner",
        "-show_streams",
        "-show_format",
        "-output_format json",
        $"\"{FileName}\""
      };
      var log = new List<string>();
      var arguments = string.Join(' ', options);

      Debug.WriteLine($"{Command} {arguments}");
      bool rv = false;

      StdOutReceived += data => log.Add(data);
      ProcessExited += (s, e) =>
      {
        if (Current.ExitCode == 0)
        {
          rv = MediaProperty.Factory(
            string.Join(string.Empty, log.ToArray()),
            out containerProperty,
            out streamProperties
          );
        }
        else
        {
          throw new Exception("ffprobeが失敗しました。");
        }
      };

      // 終了を待つ
      await StartAsync(arguments);

      return rv;
    }
  }

  public class MediaProperty
  {
    // Statics

    /// <summary>
    /// 時間フォーマットの変換
    /// [-][HH:]MM:SS[.m...] | [-]S+[.m...][s|ms|us] => decimal?
    /// </summary>
    /// <param name="value">string value</param>
    /// <returns>decimal seconds</returns>
    public static decimal? ConvertDuration(string duration)
    {
      decimal? rv = null;
      Match match = Regex.Match(duration.Trim(), @"^((?<HH>\d{1,2}):)?(?<MM>\d{1,2}):(?<SS>\d{1,2})(\.(?<m>\d+))?$");
      if (match.Success)
      {
        decimal h = 0, m = 0, s = 0, ms = 0.0m;
        if (!string.IsNullOrEmpty(match.Groups["HH"].Value))
          h = decimal.Parse(match.Groups["HH"].Value);
        if (!string.IsNullOrEmpty(match.Groups["MM"].Value))
          m = decimal.Parse(match.Groups["MM"].Value);
        if (!string.IsNullOrEmpty(match.Groups["SS"].Value))
          s = decimal.Parse(match.Groups["SS"].Value);
        if (!string.IsNullOrEmpty(match.Groups["m"].Value))
          ms = decimal.Parse("0." + (match.Groups["m"].Value ?? "0"));

        rv = h * 3600 + m * 60 + s + ms;
      }
      else
      {
        match = Regex.Match(duration.Trim(), @"^(?<S>\d+)(\.(?<m>\d+))?(?<unit>s|(?:ms)|(?:us))?$");
        if (match.Success)
        {
          decimal s = decimal.Parse(match.Groups["S"].Value ?? "0");
          decimal ms = decimal.Parse("0." + (match.Groups["m"].Value ?? "0")) ;
          rv = s + ms;
          switch (match.Groups["unit"].Value)
          {
            case "ms":
              rv /= 1000;
              break;
            case "us":
              rv /= 1000000;
              break;
          }
        }
      }
      return rv;
    }

    /// <summary>
    /// インスタンス生成 
    /// </summary>
    /// <param name="jsonText"></param>
    /// <param name="mcp"></param>
    /// <param name="msp"></param>
    /// <returns></returns>
    public static bool Factory(string jsonText,out MediaContainerProperty mcp,out List<MediaStreamProperty> msp)
    {
      mcp = null;
      msp = new List<MediaStreamProperty>();

      using (var document = JsonDocument.Parse(jsonText))
      {
        var root = document.RootElement;
        if (root.TryGetProperty("format", out JsonElement format))
          mcp = new MediaContainerProperty(format);

        if (root.TryGetProperty("streams", out JsonElement streams) && streams.ValueKind == JsonValueKind.Array)
        {
          foreach (var stream in streams.EnumerateArray())
          {
            if (stream.ValueKind == JsonValueKind.Object)
              msp.Add(new MediaStreamProperty(stream));
          }
        }
      }

      return mcp != null && msp != null;
    }

    protected static void FillProperty<T>(T obj,JsonElement element)
    {
      foreach (var pi in obj.GetType().GetProperties())
      {
        if (element.TryGetProperty(pi.Name, out JsonElement el))
        {
          switch (el.ValueKind)
          {
            case JsonValueKind.String:
              pi.SetValue(
                obj,
                pi.PropertyType.FullName != "System.String" ? decimal.Parse(el.GetString()) : el.GetString()
              );
              break;
            case JsonValueKind.Number:
              pi.SetValue(obj, el.GetDecimal());
              break;
          }
        }
      }
    }
  }

  public class MediaContainerProperty : MediaProperty
  {
    // Instances
    public decimal? bit_rate { get; protected set; }
    public decimal? duration { get; protected set; }
    public decimal? nb_programs { get; protected set; }
    public decimal? nb_stream_groups { get; protected set; }
    public decimal? nb_streams { get; protected set; }
    public decimal? probe_score { get; protected set; }
    public decimal? size { get; protected set; }
    public string filename { get; protected set; }
    public string format_long_name { get; protected set; }
    public string format_name { get; protected set; }
    public string start_time { get; protected set; }
    public MediaContainerProperty(JsonElement je)
    {
      FillProperty(this, je);
    }
  }

  public class MediaStreamProperty : MediaProperty
  {
    public decimal? bits_per_sample { get; protected set; }
    public decimal? channels { get; protected set; }
    public decimal? closed_captions { get; protected set; }
    public decimal? coded_height { get; protected set; }
    public decimal? coded_width { get; protected set; }
    public decimal? extradata_size { get; protected set; }
    public decimal? film_grain { get; protected set; }
    public decimal? has_b_frames { get; protected set; }
    public decimal? height { get; protected set; }
    public decimal? index { get; protected set; }
    public decimal? initial_padding { get; protected set; }
    public decimal? level { get; protected set; }
    public decimal? refs { get; protected set; }
    public decimal? start_pts { get; protected set; }
    public decimal? width { get; protected set; }
    public string avg_frame_rate { get; protected set; }
    public string channel_layout { get; protected set; }
    public string codec_long_name { get; protected set; }
    public string codec_name { get; protected set; }
    public string codec_tag { get; protected set; }
    public string codec_tag_string { get; protected set; }
    public string codec_type { get; protected set; }
    public string color_primaries { get; protected set; }
    public string color_range { get; protected set; }
    public string color_space { get; protected set; }
    public string color_transfer { get; protected set; }
    public string display_aspect_ratio { get; protected set; }
    public string pix_fmt { get; protected set; }
    public string profile { get; protected set; }
    public string r_frame_rate { get; protected set; }
    public string sample_aspect_ratio { get; protected set; }
    public string sample_fmt { get; protected set; }
    public string sample_rate { get; protected set; }
    public string start_time { get; protected set; }
    public string time_base { get; protected set; }

    public MediaStreamProperty(JsonElement je)
    {
      FillProperty(this, je);
    }
  }
}
