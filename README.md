# ffmpeg-command-builder
ffmpegで動画を変換する際、毎回同じようなオプションを入力するのが面倒なのでGUIを作成。

nvenc(h264_nvenc,hevc_nvenc)専用。

## 動作環境
* Windows10 22H2以降 ／ .NET Framework 4.8.1
* Windows11

## 必須
ffmpeg.exe (wingetでインストールしたもの)
```
winget install Gyan.FFmpeg
```