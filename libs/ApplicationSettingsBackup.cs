using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ffmpeg_ytdlp_gui.libs
{
  internal class ApplicationSettingsBackup
  {
    public StringCollection DownloadFormats { get; set; } = [];
    public StringCollection DownloadDirectories { get; set; } = [];
    public StringCollection OutputDirectories { get; set; } = [];
  }
}
