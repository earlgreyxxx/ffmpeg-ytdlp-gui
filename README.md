# ffmpeg-ytdlp-gui
ffmpegで動画を変換する際、毎回同じようなオプションを入力するのが面倒なのでGUIを作成。  
また、yt-dlpの簡易GUIも機能として追加。

ffmpegでの変換はh264/hevc (nvenc/qsv/cpu)のみ。

## 動作環境
* Windows10 22H2以降,Windows11
* .NET 8 デスクトップ ランタイム

## 必須
ffmpeg.exe／ffprobe.exe／yt-dlp.exe (いずれも winget でインストールしたもの）
```
winget install Gyan.FFmpeg
winget install yt-dlp.yt-dlp
```

## 利用しているライブラリなど
* 画像変換処理に <b>[ImageSharp](https://github.com/SixLabors/ImageSharp)</b> を使用しています。

## スクリーンショット

[<img width="47%" src="https://ptsv.jp/code/ffmpeg-ytdlp-gui/movie-convert.png">](https://ptsv.jp/code/ffmpeg-ytdlp-gui/movie-convert.png)
[<img width="47%" src="https://ptsv.jp/code/ffmpeg-ytdlp-gui/movie-utils.png">](https://ptsv.jp/code/ffmpeg-ytdlp-gui/movie-utils.png)
[<img width="47%" src="https://ptsv.jp/code/ffmpeg-ytdlp-gui/movie-download.png">](https://ptsv.jp/code/ffmpeg-ytdlp-gui/movie-download.png)
[<img width="47%" src="https://ptsv.jp/code/ffmpeg-ytdlp-gui/movie-setting.png">](https://ptsv.jp/code/ffmpeg-ytdlp-gui/movie-setting.png)

