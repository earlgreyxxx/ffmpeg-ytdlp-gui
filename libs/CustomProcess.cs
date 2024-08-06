using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ffmpeg_ytdlp_gui.libs
{
  internal class CustomProcess : Process
  {
    public static string[] FindInPath(string CommandName)
    {
      //環境変数%PATH%取得し、カレントディレクトリを連結。配列への格納
      IEnumerable<string> dirPathList =
        Environment
          .ExpandEnvironmentVariables(Environment.GetEnvironmentVariable("PATH")!)
          .Split([';'])
          .Prepend(Directory.GetCurrentDirectory());

      //正規表現に使用するため、%PATHEXT%の取得・ピリオド文字の変換及び配列への格納
      string[] pathext = Environment.GetEnvironmentVariable("PATHEXT")!.Replace(".", @"\.").Split([';']);

      //検索するファイル名の正規表現
      var regex = new Regex(
        $"^{CommandName}(?:{String.Join("|", pathext)})?$",
        RegexOptions.IgnoreCase
      );

      return
        dirPathList
          .Where(dirPath => Directory.Exists(dirPath))
          .SelectMany(dirPath => Directory.GetFiles(dirPath).Where(file => regex.IsMatch(Path.GetFileName(file))))
          .ToArray();
    }

    public static Process? ShellExecute(string fileName)
    {
      return Process.Start(new ProcessStartInfo() { FileName = fileName, UseShellExecute = true });
    }

    public string? CustomFileName { get; set; }
  }
}
