﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ffmpeg_command_builder
{
  internal class ffmpeg_command_qsv : ffmpeg_command
  {
    public ffmpeg_command_qsv(string ffmpegPath = "")
    {
      Initialize(ffmpegPath);
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

    public override string GetCommandLineArguments(string strInputPath)
    {
      var args = new List<string> { options["default"] };

      if (!bAudioOnly)
      {
        if (IndexOfGpuDevice > 0)
          args.Add($"-init_hw_device qsv:hw,child_device={IndexOfGpuDevice}");

        args.Add($"-hwaccel qsv -hwaccel_output_format qsv");
      }

      if (options.ContainsKey("ss") && !string.IsNullOrEmpty(options["ss"]))
        args.Add(options["ss"]);
      if (options.ContainsKey("to") && !string.IsNullOrEmpty(options["to"]))
        args.Add(options["to"]);

      args.Add($"-i \"{strInputPath}\"");

      if (bAudioOnly)
      {
        args.Add("-vn");
      }
      else
      {
        // ここにビデオフィルター
        if (filters.Count > 0)
        {
          var strFilters = new List<string>();
          if (filters.ContainsKey("bwdif"))
          {
            strFilters.Add("hwdownload,format=nv12");
            strFilters.Add("bwdif=" + filters["bwdif"]);
          }

          if (filters.ContainsKey("scale_qsv"))
          {
            if (filters.ContainsKey("bwdif"))
              strFilters.Add("hwupload=extra_hw_frames=64");
            strFilters.Add("scale_qsv=" + filters["scale_qsv"]);
          }
          if (filters.ContainsKey("transpose"))
            strFilters.Add($"hwdownload,format=nv12,transpose={filters["transpose"]},hwupload=extra_hw_frames=64");

          string strFilter = string.Join(",", strFilters.ToArray());
          args.Add($"-vf \"{strFilter}\"");
        }
        args.Add(options["vcodec"]);
        args.Add(options["preset"]);
        args.Add(options["b:v"]);

        if (options.ContainsKey("tag:v"))
          args.Add(options["tag:v"]);
      }

      args.Add(options["acodec"]);
      if (options.ContainsKey("b:a") && !string.IsNullOrEmpty(options["b:a"]))
        args.Add(options["b:a"]);

      string strOutputFileName = Path.GetFileName(strInputPath);
      string basename = Path.GetFileNameWithoutExtension(strInputPath);
      if (bAudioOnly)
      {
        var m = Regex.Match(options["acodec"], @"(\w+)$");
        if (m.Success)
        {
          switch (m.Captures[0].Value)
          {
            case "aac":
              strOutputFileName = basename + ".aac";
              break;
            case "libmp3lame":
              strOutputFileName = basename + ".mp3";
              break;
          }
        }
      }
      else
      {
        strOutputFileName = basename + ".mp4";
      }

      string strOutputFilePath = Path.Combine(OutputPath, strOutputFileName);
      if (strOutputFilePath == strInputPath)
        throw new Exception("入力ファイルと出力ファイルが同じです。");

      args.Add($"\"{strOutputFilePath}\"");

      return string.Join(" ", args.ToArray());
    }
  }
}
