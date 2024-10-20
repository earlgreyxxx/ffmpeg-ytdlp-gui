using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  internal class ApplicationSettingsBackup
  {
    public IList<string>? DownloadFormats { get; set; }
    public IList<string>? DownloadDirectories { get; set; }
    public IList<string>? OutputDirectories { get; set; }

    public IEnumerator<IList<string>?> GetEnumerator()
    {
      yield return DownloadFormats;
      yield return DownloadDirectories;
      yield return OutputDirectories;
    }

    public async Task Save(string jsonFile)
    {
      var options = new JsonSerializerOptions()
      {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true
      };

      using var sw = File.Create(jsonFile);
      await JsonSerializer.SerializeAsync<ApplicationSettingsBackup>(sw, this, options);
    }

    public static async Task<ApplicationSettingsBackup?> Load(string jsonFile)
    {
      using var sr = File.OpenRead(jsonFile);
      var backup = await JsonSerializer.DeserializeAsync<ApplicationSettingsBackup>(sr);

      return backup;
    }
  }
}
