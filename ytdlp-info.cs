using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace ffmpeg_command_builder
{
  /// <summary>
  /// MediaInformationクラス
  /// </summary>
  internal class MediaInformation
  {
    public string description { set; get; }
    public decimal duration { set; get; }
    public string id { get; set; }
    public string thumbnail { set; get; }
    public string title { set; get; }
    public string webpage_url { set; get; }
    public string format_id { set; get; }
    public List<MediaFormat> formats { get; set; } = new List<MediaFormat>();
    public List<MediaFormat> requested_formats {  set; get; } = new List<MediaFormat>();

    private string _json_text;
    public MediaInformation(string json)
    {
      _json_text = json;
      Initialize();
    }

    private void Initialize()
    {
      using (var document = JsonDocument.Parse(_json_text))
      {
        var root = document.RootElement;
        foreach (var pi in GetType().GetProperties())
        {
          if (root.TryGetProperty(pi.Name, out JsonElement je))
          {
            switch(je.ValueKind)
            {
              case JsonValueKind.String:
                pi.SetValue(this, je.GetString());
                break;
              case JsonValueKind.Number:
                pi.SetValue(this, je.GetDecimal());
                break;
              case JsonValueKind.Array:
                if (pi.Name == "formats" || pi.Name == "requested_formats")
                {
                  foreach (var el in je.EnumerateArray())
                  {
                    var mfs = pi.GetValue(this) as List<MediaFormat>;
                    mfs?.Add(new MediaFormat(el));
                  }
                }
                break;
            }
          }
        }
      }
    }

    public async Task<Stream> GetThumbnailStream()
    {
      if (thumbnail != null)
      {
        using (var agent = new HttpClient())
        {
          using (var s = await agent.GetStreamAsync(thumbnail))
          {
            var stream = new MemoryStream();
            using (var image = SixLabors.ImageSharp.Image.Load(s))
            {
              image.SaveAsPng(stream);
            }
            return stream;
          }
        }
      }
      return null;
    }

    public async Task<System.Drawing.Image> GetThumbnailImage()
    {
      Stream stream = await GetThumbnailStream();
      if (stream == null)
        return null;

      System.Drawing.Image rv;
      using (stream)
      {
        if (stream == null)
          return null;

        rv = System.Drawing.Image.FromStream(stream);
      }

      return rv;
    }

    public string GetDurationTime()
    {
      TimeSpan rv = TimeSpan.Zero;

      if (duration == 0)
        return rv.ToString();

      decimal ns = duration * 10000000;
      rv = new TimeSpan((long)ns);

      return rv.ToString();
    }
  }

  /// <summary>
  /// MediaFormatクラス
  /// </summary>
  internal class MediaFormat : IEnumerable<KeyValuePair<string, string>>
  {
    public string resolution { set; get; }
    public string format_id { set; get; }
    public string format_note { set; get; }
    public string format { set; get; }
    public string vcodec { set; get; }
    public string acodec { set; get; }
    public string url {set; get; }
    public decimal? rows { set; get; }
    public decimal? cols { set; get; }
    public decimal? fps { set; get; }
    public decimal? width { set; get; }
    public decimal? height { set; get; }
    public string ext { set; get; }
    public decimal? filesize { set; get; }
    public decimal? filesize_approx { set; get; }
    public decimal? vbr { set; get; }
    public decimal? abr { set; get; }
    public decimal? tbr { set; get; }
    public string protocol { set; get; }

    public MediaFormat(JsonElement el)
    {
      initialize(el);
    }

    private void initialize(JsonElement format)
    {
      foreach (var pi in GetType().GetProperties())
      {
        if (format.TryGetProperty(pi.Name, out JsonElement el))
        {
          switch (el.ValueKind)
          {
            case JsonValueKind.String:
              pi.SetValue(this, el.GetString());
              break;
            case JsonValueKind.Number:
              pi.SetValue(this, el.GetDecimal());
              break;
          }
        }
      }
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      if (format_id.Length > 16)
      {
        var str = format_id.Substring(0, 15) + "…";
        sb.Append($"[{str}]");
      }
      else
      {
        sb.Append($"[{format_id}]");
      }

      if (!string.IsNullOrEmpty(resolution) && resolution != "audio only")
        sb.Append($" - {resolution}");
      if (!string.IsNullOrEmpty(ext))
        sb.Append($" - {ext}");
      if (!string.IsNullOrEmpty(format_note))
        sb.Append($"({format_note})");
      if (!string.IsNullOrEmpty(vcodec) && vcodec != "none")
        sb.Append($" - {vcodec}");
      if (!string.IsNullOrEmpty(acodec) && acodec != "none")
        sb.Append($" - {acodec}");

      return sb.ToString(); 
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
      foreach (var pi in GetType().GetProperties())
        yield return new KeyValuePair<string, string>(pi.Name, pi.GetValue(this)?.ToString());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
}
