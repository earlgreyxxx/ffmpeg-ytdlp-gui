using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ffmpeg_command_builder
{
  internal class ffmpeg_command_qsv : ffmpeg_command
  {
    public ffmpeg_command_qsv(string ffmpegPath = "") : base(ffmpegPath)
    {
      options["vcodec"] = "-c:v hevc_qsv";
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
        options["b:v"] = "-global_quality 23";
      else
        options["b:v"] = bCQ ? $"-global_quality {value}" : $"-b:v {value}K";

      return this;
    }

    // use -filter:v crop=....
    public override ffmpeg_command crop(decimal width,decimal height,decimal x = -1,decimal y = -1)
    {
      crop(false, width, height, x, y);
      return this;
    }

    public override ffmpeg_command crop(bool hw,decimal width,decimal height,decimal x = -1,decimal y = -1)
    {
      options["crop"] = x < 0 || y < 0 ? $"{width}:{height}" : $"{width}:{height}:{x}:{y}";
      return this;
    }

    public override IEnumerator<string> GetEnumerator()
    {
      yield return "-hide_banner";
      yield return "-y";

      if (!bAudioOnly)
      {
        if (IndexOfGpuDevice > 0)
          yield return $"-init_hw_device qsv:hw,child_device={IndexOfGpuDevice}";

        yield return "-hwaccel qsv";
        yield return "-hwaccel_output_format qsv";
      }

      if (options.ContainsKey("hwdecoder") && !string.IsNullOrEmpty(options["hwdecoder"]))
        yield return $"-c:v {options["hwdecoder"]}";

      if (options.ContainsKey("ss") && !string.IsNullOrEmpty(options["ss"]))
        yield return $"-ss {options["ss"]}";
      if (options.ContainsKey("to") && !string.IsNullOrEmpty(options["to"]))
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
        if (options.ContainsKey("crop") && !string.IsNullOrEmpty(options["crop"]))
        {
          sw = true;
          strFilters.Add("hwdownload,format=nv12");
          strFilters.Add($"crop={options["crop"]}");
        }

        if (filters.ContainsKey("bwdif"))
        {
          if (!sw)
          {
            strFilters.Add("hwdownload,format=nv12");
            sw = true;
          }

          strFilters.Add("bwdif=" + filters["bwdif"]);
        }
        else if (filters.ContainsKey("yadif"))
        {
          if (!sw)
          {
            strFilters.Add("hwdownload,format=nv12");
            sw = true;
          }

          strFilters.Add("yadif=" + filters["yadif"]);
        }

        if (filters.ContainsKey("scale_qsv"))
        {
          if (sw)
          {
            strFilters.Add("hwupload=extra_hw_frames=64");
            sw = false;
          }

          strFilters.Add("scale_qsv=" + filters["scale_qsv"]);
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
        {
          strFilters.Add("hwupload=extra_hw_frames=64");
        }

        if (strFilters.Count > 0)
        {
          string strFilter = string.Join(",", strFilters.ToArray());
          yield return $"-filter:v \"{strFilter}\"";
        }

        yield return options["vcodec"];
        yield return options["preset"];
        yield return options["b:v"];
        
        if(options.ContainsKey("lookahead") && !string.IsNullOrEmpty(options["lookahead"]))
        {
          yield return "-look_ahead 1";
          yield return "-extbrc 1";
          yield return $"-look_ahead_depth {options["lookahead"]}";
        }

        if (options.ContainsKey("tag:v"))
          yield return options["tag:v"];
      }

      yield return options["acodec"];
      if (options.ContainsKey("b:a") && !string.IsNullOrEmpty(options["b:a"]))
        yield return options["b:a"];
    }
  }
}
