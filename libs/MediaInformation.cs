using SixLabors.ImageSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  /// <summary>
  /// MediaInformationクラス
  /// </summary>
  public class MediaInformation
  {
    public string? description { set; get; }
    public decimal? duration { set; get; }
    public string? id { get; set; }
    public string? thumbnail { set; get; }
    public string? title { set; get; }
    public string? webpage_url { set; get; }
    public string? format_id { set; get; }
    public List<MediaFormat>? formats { get; set; } = new List<MediaFormat>();
    public List<MediaFormat>? requested_formats {  set; get; } = new List<MediaFormat>();
    public string? JsonText {  private set; get; }
    public System.Drawing.Image? image { set; get; }
    
    public MediaInformation(string json)
    {
      JsonText = json;
      Initialize();
    }

    ~MediaInformation()
    {
      if (image != null)
        image.Dispose();
    }

    private void Initialize()
    {
      if(JsonText == null)
        throw new ArgumentNullException(nameof(JsonText));

      using (var document = JsonDocument.Parse(JsonText))
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

    public async Task LoadThumbnailImageAsync()
    {
      await GetThumbnailImageAsync();
    }

    public async Task<Stream?> GetThumbnailStream()
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

    public async Task<System.Drawing.Image?> GetThumbnailImageAsync()
    {
      using Stream? stream = await GetThumbnailStream();
      if (stream == null)
        return null;

      if (stream == null)
        return null;

      image = System.Drawing.Image.FromStream(stream);

      return image;
    }

    public string GetDurationTime()
    {
      TimeSpan rv = TimeSpan.Zero;

      if (duration == 0)
        return rv.ToString();

      decimal? ns = duration * 10000000;
      rv = new TimeSpan((long)(ns ?? 0));

      return rv.ToString();
    }

    public override string? ToString()
    {
      return title;
    }
  }

  /// <summary>
  /// MediaFormatクラス
  /// </summary>
  public class MediaFormat : IEnumerable<KeyValuePair<string, string>>
  {
    public string? resolution { set; get; }
    public string? format_id { set; get; }
    public string? format_note { set; get; }
    public string? format { set; get; }
    public string? vcodec { set; get; }
    public string? acodec { set; get; }
    public string? url {set; get; }
    public decimal? rows { set; get; }
    public decimal? cols { set; get; }
    public decimal? fps { set; get; }
    public decimal? width { set; get; }
    public decimal? height { set; get; }
    public string? ext { set; get; }
    public decimal? filesize { set; get; }
    public decimal? filesize_approx { set; get; }
    public decimal? vbr { set; get; }
    public decimal? abr { set; get; }
    public decimal? tbr { set; get; }
    public string? protocol { set; get; }
    public string? container { set; get; }

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
      var sb = new StringBuilder(
        format_id?.Length > 16 ? $"[{format_id.Substring(0, 15) + "…"}] " : $"[{format_id}] "
      );

      if (!string.IsNullOrEmpty(resolution) && resolution != "audio only")
        sb.Append($"{resolution}");

      if (fps != null && fps > 0)
        sb.Append($" ({fps}fps)");
      if (!string.IsNullOrEmpty(format_note))
        sb.Append($" {format_note}");
      if (!string.IsNullOrEmpty(vcodec) && vcodec != "none")
        sb.Append($" {vcodec}");

      if (!string.IsNullOrEmpty(acodec) && acodec != "none")
        sb.Append($" {acodec}");
      if (abr != null && abr > 0)
        sb.Append($"{Math.Floor((decimal)abr)}kbs");

      if (!string.IsNullOrEmpty(ext))
        sb.Append($" {ext}");

      return sb.ToString(); 
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
      foreach (var pi in GetType().GetProperties())
        yield return new KeyValuePair<string, string>(pi.Name, pi.GetValue(this)?.ToString() ?? "");
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }

  internal class MediaInformationException : Exception
  {
    public MediaInformationException(MediaInformation[]? data)
    {
      Data["MediaInformations"] = data;
    }

    public override string ToString()
    {
      return "複数のメディア(プレイリスト）が検出されました。exception.Data[MediaInformations]を参照してください。";
    }
  }
}
