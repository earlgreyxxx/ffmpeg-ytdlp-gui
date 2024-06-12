# ffmpeg-ytdlp-gui
ffmpegで動画を変換する際、毎回同じようなオプションを入力するのが面倒なのでGUIを作成。  
また、yt-dlpの簡易GUIも機能として追加。

ffmpegでの変換はh264/hevc (nvenc/qsv/cpu)のみ。

## 動作環境
* Windows10 22H2以降,Windows11
* .NET 8 デスクトップ ランタイム

## 必須
ffmpeg.exe／yt-dlp.exe (いずれも winget でインストールしたもの）
```
winget install Gyan.FFmpeg
winget install yt-dlp.yt-dlp
```
