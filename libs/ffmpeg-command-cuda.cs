using System;
using System.Collections.Generic;
using System.Linq;

namespace ffmpeg_ytdlp_gui.libs
{
  public class ffmpeg_command_cuda : ffmpeg_command,IEnumerable<string>
  {
    private bool HardwareCrop = false;

    public ffmpeg_command_cuda(string ffmpegPath = "") : base(ffmpegPath)
    {
      options["vcodec"] = "-c:v hevc_cuda";
    }

    public override ffmpeg_command vcodec(string strCodec, int indexOfGpuDevice = 0)
    {
      EncoderType = strCodec;

      options["vcodec"] = $"-c:v {strCodec}";
      if (!strCodec.StartsWith("hevc") && options.ContainsKey("tag:v"))
        options.Remove("tag:v");

      if (indexOfGpuDevice > 0)
        IndexOfGpuDevice = indexOfGpuDevice;

      return this;
    }

    public override ffmpeg_command vBitrate(int value, bool bCQ = false)
    {
      if (value <= 0)
        options["b:v"] = "-b:v 0 -cq 25";
      else
        options["b:v"] = bCQ ? $"-b:v 0 -cq {value}" : $"-b:v {value}K";

      return this;
    }

    public override ffmpeg_command crop(bool hw,decimal width, decimal height, decimal x = -1, decimal y = -1)
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
        bottom = (Height ?? 0) - (yy + height);
        left = xx;
        right = (Width ?? 0) - (xx + width);

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
      return crop(false,width, height, x, y);
    }

    public override IEnumerator<string> GetEnumerator()
    {
      yield return "-hide_banner";
      yield return "-y";
      yield return "-loglevel info";

      if (!bAudioOnly)
      {
        if (IndexOfGpuDevice > 0)
          yield return $"-init_hw_device cuda:hw,child_device={IndexOfGpuDevice}";

        yield return "-hwaccel cuda";
        yield return "-hwaccel_output_format cuda";
      }

      if (options.ContainsKey("hwdecoder") && !string.IsNullOrEmpty(options["hwdecoder"]) && options["hwdecoder"] != "none")
      {
        yield return $"-c:v {options["hwdecoder"]}";

        if (filters.ContainsKey("bob"))
        {
          yield return "-deint bob";
          yield return "-drop_second_field 1";
        }

        else if (filters.ContainsKey("adaptive"))
        {
          yield return "-deint adaptive";
          yield return "-drop_second_field 1";
        }

        if (HardwareCrop && options.ContainsKey("crop") && !string.IsNullOrEmpty(options["crop"]))
          yield return $"-crop {options["crop"]}";
      }

      if (options.ContainsKey("ss") && !string.IsNullOrEmpty(options["ss"]))
        yield return $"-ss {options["ss"]}";
      if(options.ContainsKey("to") && !string.IsNullOrEmpty(options["to"]))
        yield return $"-to {options["to"]}";

      if (AdditionalPreOptions!.Count() > 0)
        foreach (var option in AdditionalPreOptions!)
          yield return option.Trim();

      yield return $"-i \"{InputPath}\"";

      if (bAudioOnly)
      {
        yield return "-vn";
      }
      else
      {
        // ここにビデオフィルター
        bool sw = false;
        var strFilters = new List<string>();

        if (options.ContainsKey("crop") && !string.IsNullOrEmpty(options["crop"]) && !HardwareCrop)
        {
          strFilters.Add("hwdownload,format=nv12");
          sw = true;

          strFilters.Add($"crop={options["crop"]}");
        }

        if (filters.ContainsKey("bwdif") || filters.ContainsKey("yadif"))
        {
          if (sw)
          {
            strFilters.Add("hwupload_cuda");
            sw = false;
          }

          if (filters.ContainsKey("bwdif"))
            strFilters.Add("bwdif_cuda=" + filters["bwdif"]);
          else if (filters.ContainsKey("yadif"))
            strFilters.Add("yadif_cuda=" + filters["yadif"]);
        }

        if (filters.ContainsKey("scale"))
        {
          if (sw)
          {
            strFilters.Add("hwupload_cuda");
            sw = false;
          }
          string? size = filters["scale"];
          string filter = IsLandscape ? $"-2:{size}" : $"{size}:-2";
          strFilters.Add($"scale_cuda={filter}");
        }


        if (filters.ContainsKey("transpose"))
        {
          if (!sw)
          {
            strFilters.Add("hwdownload,format=nv12");
            sw = true;
          }

          strFilters.Add($"transpose={filters["transpose"]}");
        }
        if (sw)
          strFilters.Add("hwupload_cuda");


        if (strFilters.Count > 0)
        {
          string strFilter = string.Join(",", strFilters.ToArray());
          yield return $"-filter:v \"{strFilter}\"";
        }

        yield return options["vcodec"]!;
        yield return options["preset"]!;
        yield return options["b:v"]!;
        yield return options["r:v"]!;

        if (options.ContainsKey("lookahead") && !string.IsNullOrEmpty(options["lookahead"]))
          yield return $"-rc-lookahead {options["lookahead"]}";

        if (options.ContainsKey("tag:v"))
          yield return options["tag:v"]!;
      }

      if (AdditionalOptions!.Count() > 0)
        foreach (var option in AdditionalOptions!)
          yield return option.Trim();

      yield return options["acodec"]!;
      if (options.ContainsKey("b:a") && !string.IsNullOrEmpty(options["b:a"]))
        yield return options["b:a"]!;
    }
  }
}
