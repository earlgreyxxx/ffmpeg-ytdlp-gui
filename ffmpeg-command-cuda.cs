using System;
using System.Collections.Generic;
using System.IO;

namespace ffmpeg_command_builder
{
  internal class ffmpeg_command_cuda : ffmpeg_command
  {
    public ffmpeg_command_cuda(string ffmpegPath = "")
    {
      Initialize(ffmpegPath);
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

    public override string GetCommandLineArguments(string strInputPath)
    {
      var args = new List<string> { options["default"] };

      if (!bAudioOnly)
      {
        if (IndexOfGpuDevice > 0)
          args.Add($"-init_hw_device cuda:hw,child_device={IndexOfGpuDevice}");

        args.Add("-hwaccel cuda -hwaccel_output_format cuda");
      }

      if (options.ContainsKey("ss") && !string.IsNullOrEmpty(options["ss"]))
        args.Add(options["ss"]);
      if(options.ContainsKey("to") && !string.IsNullOrEmpty(options["to"]))
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
          if (filters.ContainsKey("bwdif_cuda"))
            strFilters.Add("bwdif_cuda=" + filters["bwdif_cuda"]);
          else if (filters.ContainsKey("yadif_cuda"))
            strFilters.Add("yadif_cuda=" + filters["yadif_cuda"]);

          if (filters.ContainsKey("scale_cuda"))
            strFilters.Add("scale_cuda=" + filters["scale_cuda"]);
          if (filters.ContainsKey("transpose"))
            strFilters.Add($"hwdownload,format=nv12,transpose={filters["transpose"]},hwupload_cuda");

          string strFilter = string.Join(",", strFilters.ToArray());
          args.Add($"-vf \"{strFilter}\"");
        }
        args.Add(options["vcodec"]);
        args.Add(options["preset"]);
        args.Add(options["b:v"]);

        if(options.ContainsKey("tag:v"))
          args.Add(options["tag:v"]);
      }

      args.Add(options["acodec"]);
      if (options.ContainsKey("b:a") && !string.IsNullOrEmpty(options["b:a"]))
        args.Add(options["b:a"]);

      string strOutputFileName = CreateOutputFileName(strInputPath);
      string strOutputFilePath = Path.Combine(OutputPath, strOutputFileName);
      if(strOutputFilePath == strInputPath)
        throw new Exception("入力ファイルと出力ファイルが同じです。");

      args.Add($"\"{strOutputFilePath}\"");

      return string.Join(" ", args.ToArray());
    }
  }
}
