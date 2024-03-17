using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ffmpeg_command_builder
{
  internal class ffmpeg_command_cuda : ffmpeg_command,IEnumerable<string>
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
      return crop(false,width, height, x, y);
    }

    public override IEnumerator<string> GetEnumerator()
    {
      yield return "-hide_banner";
      yield return "-y";

      if (!bAudioOnly)
      {
        if (IndexOfGpuDevice > 0)
          yield return $"-init_hw_device cuda:hw,child_device={IndexOfGpuDevice}";

        yield return "-hwaccel cuda";
        yield return "-hwaccel_output_format cuda";
      }

      if (options.ContainsKey("hwdecoder") && !string.IsNullOrEmpty(options["hwdecoder"]))
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

        if (filters.ContainsKey("bwdif_cuda") || filters.ContainsKey("yadif_cuda"))
        {
          if (sw)
          {
            strFilters.Add("hwupload_cuda");
            sw = false;
          }

          if (filters.ContainsKey("bwdif_cuda"))
            strFilters.Add("bwdif_cuda=" + filters["bwdif_cuda"]);
          else if (filters.ContainsKey("yadif_cuda"))
            strFilters.Add("yadif_cuda=" + filters["yadif_cuda"]);
        }

        if (filters.ContainsKey("scale_cuda"))
        {
          if (sw)
          {
            strFilters.Add("hwupload_cuda");
            sw = false;
          }

          strFilters.Add("scale_cuda=" + filters["scale_cuda"]);
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

        yield return options["vcodec"];
        yield return options["preset"];
        yield return options["b:v"];

        if(options.ContainsKey("lookahead") && !string.IsNullOrEmpty(options["lookahead"]))
          yield return $"-rc-lookahead {options["lookahead"]}";

        if (options.ContainsKey("tag:v"))
          yield return options["tag:v"];
      }

      yield return options["acodec"];
      if (options.ContainsKey("b:a") && !string.IsNullOrEmpty(options["b:a"]))
        yield return options["b:a"];
    }
  }
}
