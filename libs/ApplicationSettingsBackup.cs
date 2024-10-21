using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  internal class ApplicationSettingsBackup : IEnumerable<IList<string>?>
  {
    public IList<string>? DownloadFormats { get; set; } = new List<string>();
    public IList<string>? DownloadDirectories { get; set; } = new List<string>();
    public IList<string>? OutputDirectories { get; set; } = new List<string>();

    public IEnumerator<IList<string>?> GetEnumerator()
    {
      yield return DownloadFormats;
      yield return DownloadDirectories;
      yield return OutputDirectories;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator)GetEnumerator();
    }

    public void Save(string jsonFile)
    {

    }

    public void Save(StreamWriter sr)
    {

    }

    public void Load(string jsonFile)
    {

    }

    public void Load(StreamReader sr)
    {

    }
  }
}
