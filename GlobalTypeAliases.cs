﻿global using CodecListItem = ffmpeg_ytdlp_gui.libs.ListItem<ffmpeg_ytdlp_gui.libs.Codec>;
global using CodecListItems = System.Collections.Generic.List<ffmpeg_ytdlp_gui.libs.ListItem<ffmpeg_ytdlp_gui.libs.Codec>>;
global using StringListItem = ffmpeg_ytdlp_gui.libs.ListItem<string>;
global using StringListItems = System.Collections.Generic.List<ffmpeg_ytdlp_gui.libs.ListItem<string>>;
global using DecimalListItem = ffmpeg_ytdlp_gui.libs.ListItem<decimal>;
global using DecimalListItems = System.Collections.Generic.List<ffmpeg_ytdlp_gui.libs.ListItem<decimal>>;
global using StringListItemsSet = System.Tuple<System.Collections.Generic.List<ffmpeg_ytdlp_gui.libs.ListItem<string>>, System.Collections.Generic.List<ffmpeg_ytdlp_gui.libs.ListItem<string>>, System.Collections.Generic.List<ffmpeg_ytdlp_gui.libs.ListItem<string>>,int[]>;
global using YtdlpItem = System.Tuple<string, ffmpeg_ytdlp_gui.libs.MediaInformation?, System.Drawing.Image?,ffmpeg_ytdlp_gui.libs.MediaInformation[]?>;
global using YtdlpItems = System.Collections.Generic.List<System.Tuple<string, ffmpeg_ytdlp_gui.libs.MediaInformation?, System.Drawing.Image?,ffmpeg_ytdlp_gui.libs.MediaInformation[]?>?>;
global using FFmpegBatchList = System.Collections.Generic.Dictionary<ffmpeg_ytdlp_gui.libs.ffmpeg_command, System.Collections.Generic.IEnumerable<string>>;
