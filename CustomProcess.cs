using System.Diagnostics;

namespace ffmpeg_command_builder
{
  internal class CustomProcess : Process
  {
    public static Process ShellExecute(string fileName)
    {
      return Process.Start(new ProcessStartInfo() { FileName = fileName, UseShellExecute = true });
    }

    public string CustomFileName { get; set; }
  }
}
