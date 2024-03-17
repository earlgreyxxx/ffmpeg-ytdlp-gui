using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffmpeg_command_builder
{
  internal class ffmpeg_command_copy : ffmpeg_command
  {
    public ffmpeg_command_copy(string ffmpegPath = "") : base(ffmpegPath)
    {
      options["vcodec"] = "-c:v copy";
    }

    public override ffmpeg_command vcodec(string strCodec, int indexOfGpuDevice = 0)
    {
      // copy nothing to do...
      return this;
    }

    public override ffmpeg_command vBitrate(int value, bool bCQ = false)
    {
      // copy nothing to do...
      return this;
    }

    public override ffmpeg_command crop(bool hw,decimal width, decimal height, decimal x = -1, decimal y = -1)
    {
      // copy nothing to do...
      return this;
    }

    public override ffmpeg_command crop(decimal width, decimal height, decimal x = -1, decimal y = -1)
    {
      // copy nothing to do...
      return this;
    }

    public override IEnumerator<string> GetEnumerator()
    {
      yield return "-hide_banner";
      yield return "-y";

      if (options.ContainsKey("ss") && !string.IsNullOrEmpty(options["ss"]))
        yield return $"-ss {options["ss"]}";
      if(options.ContainsKey("to") && !string.IsNullOrEmpty(options["to"]))
        yield return $"-to {options["to"]}";

      yield return $"-i \"{InputPath}\"";

      if (bAudioOnly)
      {
        yield return "-vn";
      }
      else
      {
        yield return options["vcodec"];
      }

      yield return options["acodec"];
      if (options.ContainsKey("b:a") && !string.IsNullOrEmpty(options["b:a"]))
        yield return options["b:a"];
    }
  }
}
