using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ffmpeg_command_builder
{
  internal class ffprobe_process : RedirectedProcess
  {
    // Statics
    // ----------------------------------------------------------------------

    public static async Task<MediaStreamProperty> GetStreamProperty(string filepath)
    {
      var process = new ffprobe_process(filepath);
      await process.Initialize();

      var property = process.getStreamProperty();
      return property;
    }

    // Instances
    // ----------------------------------------------------------------------

    public string FileName { get; set; }

    public ffprobe_process(string filepath) : base("ffprobe")
    {
      if (!string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
        throw new Exception("指定されたファイルが存在しません");

      ProcessExited += (s, e) => Debug.WriteLine("ffprobeプロセス終了");
    }

    public MediaStreamProperty getStreamProperty()
    {

    }

    public MediaContainerProperty getContainerProperty()
    {

    }

    public async Task Initialize()
    {
      if (!string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
        throw new Exception("ファイル名が指定されていないか、ファイルが存在しません");

      var options = new string[]
      {
        "-hide_banner",
        "-show_streames",
        "-show_format",
        "-output_format json"
      };
      var log = new List<string>();
      var arguments = string.Join(' ', options);

      Debug.WriteLine($"{Command} {arguments}");

      StdOutReceived += data => log.Add(data);
      ProcessExited += (s, e) =>
      {
        if (Current.ExitCode == 0)
        {
          MediaProperty.Factory(
            string.Join(string.Empty, log.ToArray()),
            out MediaContainerProperty mcp,
            out MediaStreamProperty msp
          );
        }
      };

      await StartAsync(arguments);
    }
  }

  internal class MediaProperty
  {
    public static bool Factory(string jsonText,out MediaContainerProperty mcp,out MediaStreamProperty msp)
    {
      mcp = null;
      msp = null;
      using (var document = JsonDocument.Parse(jsonText))
      {
        var root = document.RootElement;
        if (root.TryGetProperty("streams", out JsonElement format))
          mcp = new MediaContainerProperty(format);

        if (root.TryGetProperty("format", out JsonElement streams))
          msp = new MediaStreamProperty(streams);
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
              pi.SetValue(obj, el.GetString());
              break;
            case JsonValueKind.Number:
              pi.SetValue(obj, el.GetDecimal());
              break;
          }
        }
      }
    }
  }

  internal class MediaContainerProperty : MediaProperty
  {
    public MediaContainerProperty(JsonElement je)
    {
      FillProperty(this, je);
    }
  }

  internal class MediaStreamProperty : MediaProperty
  {
    public MediaStreamProperty(JsonElement je)
    {
      FillProperty(this, je);
    }
  }
}
