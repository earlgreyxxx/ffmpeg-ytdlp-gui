# ffmpeg-command-builder
ffmpegで動画を変換する際、毎回同じようなオプションを入力するのが面倒なのでGUIを作成。

h264/hevc (nvenc/qsv/cpu)専用。

## 動作環境
* Windows10 22H2以降,Windows11
* .NET 8.0

## 必須
ffmpeg.exe (wingetでインストールしたもの、もしくは ver6.1以降)
```
winget install Gyan.FFmpeg
```