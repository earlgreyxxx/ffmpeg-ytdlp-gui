using System;
using System.Collections.Generic;
using System.Linq;

namespace ffmpeg_command_builder
{
  internal class ffmpeg_command_cpu : ffmpeg_command, IEnumerable<string>
  {
    private bool HardwareCrop = false;

    public ffmpeg_command_cpu(string ffmpegpath) : base(ffmpegpath)
    {
      options["vcodec"] = "-c:v libx264";
    }

    public override ffmpeg_command vcodec(string strCodec, int indexOfGpuDevice = 0)
    {
      EncoderType = strCodec;

      options["vcodec"] = $"-c:v {strCodec}";
      if (strCodec != "libx265" && options.ContainsKey("tag:v"))
        options.Remove("tag:v");

      return this;
    }

    public override ffmpeg_command vBitrate(int value, bool bCQ = false)
    {
      if (value <= 0)
        options["b:v"] = "-crf 25";
      else
        options["b:v"] = bCQ ? $"-crf {value}" : $"-b:v {value}K";

      return this;
    }

    public override ffmpeg_command crop(bool hw, decimal width, decimal height, decimal x = -1, decimal y = -1)
    {
      decimal top, bottom, left, right;
      decimal xx = x, yy = y;

      HardwareCrop = hw;

      if (hw)
      {
        if (Width <= 0 || Height <= 0)
          throw new Exception("Movie size require");

        if (x < 0 || y < 0)
        {
          xx = 0;
          yy = 0;
        }
        top = yy;
        bottom = Height - (yy + height);
        left = xx;
        right = Width - (xx + width);

        options["crop"] = $"{top}x{bottom}x{left}x{right}";
      }
      else
      {
        options["crop"] = x < 0 || y < 0 ? $"{width}:{height}" : $"{width}:{height}:{x}:{y}";
      }

      return this;
    }

    public override ffmpeg_command crop(decimal width, decimal height, decimal x = -1, decimal y = -1)
    {
      return crop(false, width, height, x, y);
    }

    public override IEnumerator<string> GetEnumerator()
    {
      yield return "-hide_banner";
      yield return "-y";

      if (options.ContainsKey("ss") && !string.IsNullOrEmpty(options["ss"]))
        yield return $"-ss {options["ss"]}";
      if (options.ContainsKey("to") && !string.IsNullOrEmpty(options["to"]))
        yield return $"-to {options["to"]}";

      if (AdditionalPreOptions.Count() > 0)
        foreach (var option in AdditionalPreOptions)
          yield return option.Trim();

      yield return $"-i \"{InputPath}\"";

      if (bAudioOnly)
      {
        yield return "-vn";
      }
      else
      {
        // ここにビデオフィルター
        var strFilters = new List<string>();

        if (options.ContainsKey("crop") && !string.IsNullOrEmpty(options["crop"]) && !HardwareCrop)
        {
          strFilters.Add($"crop={options["crop"]}");
        }

        if (filters.ContainsKey("bwdif"))
          strFilters.Add("bwdif=" + filters["bwdif"]);
        else if (filters.ContainsKey("yadif"))
          strFilters.Add("yadif=" + filters["yadif"]);

        if (filters.ContainsKey("scale"))
        {
          string size = filters["scale"];
          string filter = IsLandscape ? $"-2:{size}" : $"{size}:-2";
          strFilters.Add($"scale={filter}");
        }

        if (filters.ContainsKey("transpose"))
          strFilters.Add($"transpose={filters["transpose"]}");

        if (strFilters.Count > 0)
        {
          string strFilter = string.Join(",", strFilters.ToArray());
          yield return $"-filter:v \"{strFilter}\"";
        }

        yield return options["vcodec"];
        yield return options["preset"];
        yield return options["b:v"];
        yield return options["r:v"];

        if (options.ContainsKey("lookahead") && !string.IsNullOrEmpty(options["lookahead"]))
          yield return $"-rc-lookahead {options["lookahead"]}";

        if (options.ContainsKey("tag:v"))
          yield return options["tag:v"];
      }

      if (AdditionalOptions.Count() > 0)
        foreach (var option in AdditionalOptions)
          yield return option.Trim();

      yield return options["acodec"];
      if (options.ContainsKey("b:a") && !string.IsNullOrEmpty(options["b:a"]))
        yield return options["b:a"];
    }
  }
}
