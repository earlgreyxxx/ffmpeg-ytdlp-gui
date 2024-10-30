using ffmpeg_ytdlp_gui.libs;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ffmpeg_ytdlp_gui
{
  partial class Form1 
  {
    /// <summary>
    /// ダウンロードプロセス
    /// </summary>
    object _lock = new object();
    private ytdlp_process? ytdlp = null;
    private StdoutForm? ytdlpfm = null;
    private ObservableQueue<ytdlp_process>? ytdlps = new ObservableQueue<ytdlp_process>();

    private void WriteQueueStatus(int count)
    {
      QueueCount.Text = $"Download Queue: {count}";
    }

    private void InitializeYtdlpQueue()
    {
      // キューにytdlpプロセスを入れた時
      ytdlps!.Enqueued += (s, e) =>
      {
        var q = s as ObservableQueue<ytdlp_process>;
        WriteQueueStatus(q?.Count ?? 0);
      };

      // キューからytdlpプロセスを取り出す時
      Action formAction = () =>
      {
        if (ytdlpfm != null)
        {
          ytdlpfm.Lock();
          ytdlpfm.Pause = false;
        }
      };

      ytdlps.Dequeued += (s, e) =>
      {
        var q = s as ObservableQueue<ytdlp_process>;
        if (q == null)
          return;

        ytdlp = e.data;
        ytdlp.ProcessExited += (sender, e) =>
        {
          lock (_lock)
          {
            if (q.Count > 0)
            {
              q.Dequeue();
            }
            else
            {
              ytdlp = null;
              //if (IsOpenStderr.Checked && ytdlpfm != null)
              //  ytdlpfm = null;

              Invoke(OnDownloaded);
              ToastPush("ダウンロードキューが空になりました。", "PageDownloader");
            }
          }
        };

        if (IsOpenStderr.Checked)
        {
          if (ytdlpfm!.InvokeRequired)
            ytdlpfm.Invoke(formAction);
          else
            formAction();
        }

        ytdlp.DownloadAsync();
        WriteQueueStatus(q.Count);
      };
    }

    /// <summary>
    /// メディアURLのパース情報を取得し、フォームにセット
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<YtdlpItem?> YtdlpParseDownloadUrl(string url,string? confDir = null)
    {
      YtdlpItem? ytdlpItem = null;
      MediaInformation? mediaInfo;

      try
      {
        if (url.Length == 0)
          throw new Exception("URLが入力されていません。");

        var parser = new ytdlp_process(YtdlpPath.Text)
        {
          Url = url,
          ConfigDir = confDir,
        };

        parser.CookieBrowser = UseCookie.SelectedValue?.ToString();
        parser.CookiePath = CookiePath.Text;

        OnPreParseMediaUrl();

        OutputStderr.Text = "ダウンロード先の情報の取得及び解析中...";
        mediaInfo = await parser.GetMediaInformation();
        OutputStderr.Text = "";

        if (mediaInfo == null)
          throw new Exception("解析に失敗しました。");

        var image = await mediaInfo.GetThumbnailImageAsync();

        ytdlpItem = new YtdlpItem(url, mediaInfo, image, null);
      }
      catch (MediaInformationException mediaInfoException) // URLがプレイリストだった場合
      {
        var mediaInformations = mediaInfoException.Data["MediaInformations"] as MediaInformation[];
        OutputStderr.Text = "プレイリストが検出されました。";

        ytdlpItem = new YtdlpItem(url, null, null, mediaInformations);
        foreach (var mediaInformation in mediaInformations!)
          await mediaInformation.LoadThumbnailImageAsync();
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
      finally
      {
        OnPostParseMediaUrl();
        OutputStderr.Text = string.Empty;
      }

      return ytdlpItem;
    }

    private async void YtdlpAddDownloadQueue(YtdlpItem? ytdlpItem,bool separatedDownload = false,bool isDefaultDownload = false)
    {
      var url = ytdlpItem?.Item1;
      var mediaInfo = ytdlpItem?.Item2;
      if (url == null || mediaInfo == null)
        return;

      if(true == (ytdlps?.Any(item => item.Url == url) ?? false))
      {
        var result = MessageBox.Show("同じURLのダウンロードが既にキューに入っています。\nキューに追加しますか？", "確認", MessageBoxButtons.OKCancel);
        if (result == DialogResult.Cancel)
          return;
      }

      try
      {
        var outputdir = string.IsNullOrEmpty(cbOutputDir.Text) ? Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) : cbOutputDir.Text;
        if (!Directory.Exists(outputdir))
          Directory.CreateDirectory(outputdir);

        var downloader = new ytdlp_process(YtdlpPath.Text)
        {
          Url = url,
          OutputPath = outputdir,
          VideoFormat = string.Empty,
          AudioFormat = string.Empty,
          MovieFormat = string.Empty,
          Separated = separatedDownload,
          JsonText = mediaInfo.JsonText
        };
        if (isDefaultDownload)
        {
          var radio = GetCheckedRadioButton(PlaylistGroup);
          downloader.MovieFormat = (radio?.Tag as string) ?? string.Empty;
        }

        if (!isDefaultDownload)
        {
          downloader.VideoFormat = VideoOnlyFormat.SelectedValue?.ToString();
          downloader.AudioFormat = AudioOnlyFormat.SelectedValue?.ToString();
          downloader.MovieFormat = MovieFormat.SelectedValue?.ToString();
        }

        if (!string.IsNullOrEmpty(OutputFileFormat.Text))
          downloader.OutputFile = OutputFileFormat.Text;

        downloader.CookieBrowser = UseCookie.SelectedValue?.ToString();
        downloader.CookiePath = CookiePath.Text;

        downloader.StdOutReceived += data => Invoke(() => OutputStderr.Text = data);

        SubmitDownloadDequeue.Enabled = false;
        OutputStderr.Text = "ダウンロードファイル名を取得しています。";

        await downloader.GetDownloadFileNames();
        if (string.IsNullOrEmpty(downloader.DownloadFile))
          throw new Exception("ダウンロード名が決定できませんでした。");

        OutputStderr.Text = string.Empty;

        downloader.ProcessExited += (s, e) => Invoke(() =>
        {
          Debug.WriteLine($"exitcode = {downloader.ExitCode},DownloadName = {downloader.DownloadFile}");

          if (downloader.ExitCode == 0 && chkAfterDownload.Checked && !string.IsNullOrEmpty(downloader.DownloadFile))
          {
            FileListBindingSource.Add(new StringListItem(downloader.DownloadFile));
            btnSubmitInvoke.Enabled = true;
          }

          if(DeleteUrlAfterDownloaded.Checked)
          {
            var items = UrlBindingSource.DataSource as YtdlpItems;
            var item = items?.FirstOrDefault(item => item?.Item1 == downloader.Url);
            if (item != null)
            {
              UrlBindingSource.Remove(item);
              UrlBindingSource.ResetBindings(false);
            }
          }
        });

        if (IsOpenStderr.Checked)
        {
          if (ytdlpfm == null)
          {
            var form = ytdlpfm;
            ytdlpfm = new StdoutForm();
            ytdlpfm.Load += StdoutFormLoadAction;
            ytdlpfm.FormClosing += StdoutFormClosingAction;
            ytdlpfm.FormClosed += (s, e) => ytdlpfm = null;
            //{
            //  // 新しい StdoutForm が生成されていなければ NULL を代入
            //  bool nextBegan = form != ytdlpfm;
            //  Debug.WriteLine($"Next ytdlp process was began: {nextBegan}");
            //  if (!nextBegan)
            //    ytdlpfm = null;
            //};
          }

          Action<string> receiver = data =>
          {
            if (ytdlpfm == null)
              return;

            if (ytdlpfm.Pause)
              ytdlpfm.LogData.Add(data);
            else
              ytdlpfm.Invoke(ytdlpfm.WriteLine, [data]);
          };

          downloader.StdOutReceived += receiver;
          downloader.StdErrReceived += receiver;
          downloader.ProcessExited += (s,e) =>
          {
            if(ytdlps!.Count <= 0)
              ytdlpfm.Invoke(() =>
              {
                ytdlpfm.Release();
                ytdlpfm.Pause = true;
              });

            ytdlp = null;
          };
        }

        /// todo
        /// キューに追加
        lock (_lock)
        {
          ytdlps?.Enqueue(downloader);
        }
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message,"エラー");
        if (ytdlpfm != null)
        {
          ytdlpfm.Release();
          ytdlpfm.Pause = true;
        }
      }
      finally
      {
        SubmitDownloadDequeue.Enabled = true;
      }
    }

    private void YtdlpBeginDequeue()
    {
      lock (_lock)
      {
        int len = ytdlps?.Count ?? 0;
        if (ytdlp == null && len > 0)
        {
          if(false == (ytdlpfm?.Visible ?? false))
            ytdlpfm?.Show();

          OnDownload();
          ytdlps?.Dequeue();
          SubmitDownloadDequeue.Enabled = false;
          StopDownload.Enabled = true;
        }
      }
    }
  
    private void YtdlpClearDequeue()
    {
      ytdlps?.Clear();
      WriteQueueStatus(0);
    }

    private void YtdlpAbortDownload(bool stopAll = true)
    {
      ytdlps?.Clear();
      ytdlp?.Interrupt();
      WriteQueueStatus(0);
    }

    private void EnablePlaylist(YtdlpItem item)
    {
      PlaylistBindingSource.DataSource = item.Item4;
      PlaylistBindingSource.ResetBindings(false);
      PlaylistGroup.Enabled = true;
      Playlist_SelectedIndexChanged(Playlist, new EventArgs());
    }

    private void DisablePlaylist()
    {
      // clear Playlist listbox control
      if (PlaylistGroup.Enabled)
      {
        PlaylistBindingSource.DataSource = null;
        PlaylistBindingSource.ResetBindings(false);
        PlaylistGroup.Enabled = false;
      }
    }

    private void OnPreParseMediaUrl()
    {
      DummyProgressBar.Visible = true;
    }

    private void OnPostParseMediaUrl()
    {
      DummyProgressBar.Visible = false;
    }
    
    private void FormatSourceChange(ComboBox comboBox,BindingSource bindingSource)
    {
      int idle = 16;
      var list = bindingSource.List;
      if(list.Count == 0)
        return;

      var max = list.Cast<object>()
                    .Select(item => TextRenderer.MeasureText((item.ToString() ?? string.Empty), comboBox.Font).Width + idle)
                    .Max();

      if (max > 0 && max > comboBox.Width)
        comboBox.DropDownWidth = max;
      else
        comboBox.DropDownWidth = comboBox.Width;
    }

    private void OnDownloaded()
    {
      SubmitDownloadDequeue.Enabled = true;
      StopDownload.Enabled = false;
    }

    private void OnDownload()
    {
      SubmitDownloadDequeue.Enabled = false;
      StopDownload.Enabled = true;
    }
  }
}
