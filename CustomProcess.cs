using System.Diagnostics;

namespace ffmpeg_command_builder
{
  internal class CustomProcess : Process
  {
    public string CustomFileName { get; set; }
  }
}
