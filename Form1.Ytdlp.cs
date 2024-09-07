using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ffmpeg_ytdlp_gui.libs;

namespace ffmpeg_ytdlp_gui
{
  using StringListItem = ListItem<string>;
  using YtdlpItem = Tuple<string, MediaInformation, System.Drawing.Image?>;

  partial class Form1 
  {
    /// <summary>
    /// ダウンロードプロセス
    /// </summary>
    object _lock = new object();
    private ytdlp_process? ytdlp = null;
    private StdoutForm? ytdlpfm = null;
    private ObservableQueue<ytdlp_process>? ytdlps = new ObservableQueue<ytdlp_process>();

    private readonly Regex DownloadRegex = new Regex(@"\[download\]\s+Destination:\s+(?<filename>.+)$");
    private readonly Regex MergerRegex = new Regex(@"\[Merger\]\s+Merging formats into ""(?<filename>.+)""$");

    private void InitializeYtdlpQueue()
    {
      Action<int> WriteQueueStatus = count => QueueCount.Text = $"Download Queue: {count}";

      // キューにytdlpプロセスを入れた時
      ytdlps!.Enqueued += (sender, e) =>
      {
        var q = sender as ObservableQueue<ytdlp_process> ?? throw new NullReferenceException("sender is null");
        WriteQueueStatus(q.Count);

        lock (_lock)
        {
          if (ytdlp == null)
            q.Dequeue();
        }
      };

      // キューからytdlpプロセスを取り出す時
      Action formAction = () =>
      {
        var button = ytdlpfm!.Controls["BtnClose"] as Button ?? throw new NullReferenceException("button is null");
        button.Enabled = false;
        ytdlpfm.Pause = false;
      };

      ytdlps.Dequeued += (sender, e) =>
      {
        var q = sender as ObservableQueue<ytdlp_process> ?? throw new NullReferenceException("sender is null");
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
              if (IsOpenStderr.Checked && ytdlpfm != null)
                ytdlpfm = null;
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

    public async Task<YtdlpItem?> YtdlpParseDownloadUrl(string url)
    {
      YtdlpItem? ytdlpItem = null;
      MediaInformation? mediaInfo;
      try
      {
        if (url.Length == 0)
          throw new Exception("URLが入力されていません。");

        var parser = new ytdlp_process()
        {
          Url = url,
          DownloadFile = string.Empty
        };

        var cookieKind = UseCookie.SelectedValue?.ToString();
        if (cookieKind == "file" && !string.IsNullOrEmpty(CookiePath.Text) && File.Exists(CookiePath.Text))
          parser.CookiePath = CookiePath.Text;
        else if (cookieKind != "none" && cookieKind != "file")
          parser.CookieBrowser = cookieKind;

        OutputStderr.Text = "ダウンロード先の情報の取得及び解析中...";
        mediaInfo = await parser.getMediaInformation();
        OutputStderr.Text = "";

        if (mediaInfo == null)
          throw new Exception("解析に失敗しました。");

        Image? image = await mediaInfo.GetThumbnailImage();

        ytdlpItem = new YtdlpItem(url,mediaInfo,image);
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }

      return ytdlpItem;
    }

    private async void YtdlpInvokeDownload(YtdlpItem? ytdlpItem,bool separatedDownload = false)
    {
      if (ytdlpItem == null) throw new NullReferenceException("YtdlpItem is null");
      var mediaInfo = ytdlpItem.Item2;

      if ( mediaInfo == null)
        return;

      try
      {
        var outputdir = string.IsNullOrEmpty(cbOutputDir.Text) ? Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) : cbOutputDir.Text;
        if (!Directory.Exists(outputdir))
          Directory.CreateDirectory(outputdir);

        var downloader = new ytdlp_process()
        {
          Url = mediaInfo.webpage_url,
          OutputPath = outputdir,
          Separated = separatedDownload,
          VideoFormat = VideoOnlyFormat.SelectedValue?.ToString(),
          AudioFormat = AudioOnlyFormat.SelectedValue?.ToString(),
          MovieFormat = MovieFormat.SelectedValue?.ToString()
        };

        if (!string.IsNullOrEmpty(OutputFileFormat.Text))
          downloader.OutputFile = OutputFileFormat.Text;

        var cookieKind = UseCookie.SelectedValue?.ToString();

        if (cookieKind == "file" && !string.IsNullOrEmpty(CookiePath.Text) && File.Exists(CookiePath.Text))
          downloader.CookiePath = CookiePath.Text;
        else if (cookieKind != "none" && cookieKind != "file")
          downloader.CookieBrowser = cookieKind;

        downloader.StdOutReceived += data => Invoke(() => OutputStderr.Text = data);

        OutputStderr.Text = "ダウンロードファイル名を取得しています。";

        var parser = downloader.Clone();
        downloader.DownloadFiles = await parser.GetDownloadFileNames();

        downloader.ProcessExited += (s, e) => Invoke(() =>
        {
          Debug.WriteLine($"exitcode = {downloader.ExitCode},DownloadName = {downloader.DownloadFile}");

          if (downloader.ExitCode == 0 && chkAfterDownload.Checked && !string.IsNullOrEmpty(downloader.DownloadFile))
          {
            FileListBindingSource.Add(new StringListItem(downloader.DownloadFile));
            btnSubmitInvoke.Enabled = true;
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

            ytdlpfm.FormClosed += (sender, e) =>
            {
              // 新しい StdoutForm が生成されていなければ NULL を代入
              bool nextBegan = form != ytdlpfm;
              Debug.WriteLine($"Next ytdlp process was began: {nextBegan}");
              if (!nextBegan)
                ytdlpfm = null;
            };
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
                var button = ytdlpfm.Controls["BtnClose"] as Button ?? throw new NullReferenceException("button is null");
                button.Enabled = true;
                ytdlpfm.Pause = true;
              });

            ytdlp = null;
          };

          if(!ytdlpfm.Visible)
            ytdlpfm.Show();
        }

        /// todo
        /// キューに追加
        lock (_lock)
        {
          ytdlps!.Enqueue(downloader);
        }
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message,"エラー");
        if (ytdlpfm != null)
        {
          var button = ytdlpfm.Controls["BtnClose"] as Button ?? throw new NullReferenceException("button is null");
          button.Enabled = true;
        }
      }
    }

    private void YtdlpAbortDownload()
    {
      if (ytdlp != null)
        ytdlp.Interrupt();
    }
  }
}
