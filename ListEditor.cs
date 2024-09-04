using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ffmpeg_ytdlp_gui.libs;

namespace ffmpeg_ytdlp_gui
{
  using StringListItem = ListItem<string>;
  using YtdlpItem = Tuple<string, MediaInformation, System.Drawing.Image?>;

  public partial class ListEditor : Form
  {
    ListItemType EditorItemType;

    private ListEditor()
    {
      InitializeComponent();
    }

    public ListEditor(BindingSource bindingSource, ListItemType type) : this()
    {
      if (type == ListItemType.Url)
      {
        ListEditItems.DisplayMember = "Item1";
        ListEditItems.ValueMember = "Item1";
      }

      ListEditItems.DataSource = bindingSource;
      EditorItemType = type;
      switch (type)
      {
        case ListItemType.FileOrDirectory:
          ContextMenuStrip = DirectoryContextMenu;
          break;

        case ListItemType.PlainText:
        case ListItemType.Url:
          ContextMenuStrip = PlainTextContextMenu;
          break;
      }
    }

    private void SubmitClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void SubmitClear_Click(object sender, EventArgs e)
    {
      var items = ListEditItems.DataSource as BindingSource;
      var result = MessageBox.Show("全て削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel);
      if (result == DialogResult.OK)
        items?.Clear();
    }

    private void SubmitDelete_Click(object sender, EventArgs e)
    {
      var items = ListEditItems.DataSource as BindingSource;
      foreach (var item in ListEditItems.SelectedItems.Cast<object>().ToArray())
        items?.Remove(item);
    }

    private void ListEditItems_DragEnter(object sender, DragEventArgs e)
    {
      var DataObject = e.Data;
      e.Effect = DragDropEffects.None;
      if (DataObject == null)
        return;

      switch (EditorItemType)
      {
        case ListItemType.PlainText:
        case ListItemType.Url:
          if (DataObject.GetDataPresent(DataFormats.UnicodeText))
            e.Effect = DragDropEffects.Copy;
          break;

        case ListItemType.FileOrDirectory:
          if (DataObject.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
          break;
      }
    }

    private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      var DataObject = Clipboard.GetDataObject() ?? throw new Exception("GetDataObject() Failed");
      switch (EditorItemType)
      {
        case ListItemType.PlainText:
        case ListItemType.Url:
          MenuItemDirectoryPaste.Enabled = DataObject.GetDataPresent(DataFormats.UnicodeText);
          break;

        case ListItemType.FileOrDirectory:
          MenuItemDirectoryPaste.Enabled = DataObject.GetDataPresent(DataFormats.FileDrop);
          break;
      }
    }

    private Func<string, bool> IsValidDirectory = path => !string.IsNullOrEmpty(path) &&
                                                          File.GetAttributes(path).HasFlag(FileAttributes.Directory) &&
                                                          Directory.Exists(path);

    private async Task AddItems(IDataObject? dataObject)
    {
      try
      {
        var items = ListEditItems.DataSource as BindingSource ?? throw new Exception("DataSource is Null");

        if (dataObject == null)
          throw new ArgumentNullException(nameof(dataObject));

        switch (EditorItemType)
        {
          case ListItemType.PlainText:
            if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
              var lines = dataObject.GetData(DataFormats.UnicodeText) as string;
              if (string.IsNullOrEmpty(lines))
                throw new Exception("Data is empty or null");

              foreach (var _ in lines.Split(Environment.NewLine.ToCharArray()).Where(str => !string.IsNullOrEmpty(str.Trim())))
              {
                string line = _.Trim();
                if (false == items?.Contains(line))
                  items?.Add(line);
              }
            }
            break;

          case ListItemType.Url:
            if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
              var owner = Owner as Form1;
              var lines = dataObject.GetData(DataFormats.UnicodeText) as string;
              if (string.IsNullOrEmpty(lines))
                throw new Exception("Data is empty or null");

              var ite = lines.Split(Environment.NewLine.ToCharArray())
                             .Select(str => str.Trim())
                             .Where(str => !string.IsNullOrEmpty(str));

              Enabled = false;
              foreach (var line in ite)
              {
                if (!Regex.IsMatch(line, "^https?://"))
                {
                  MessageBox.Show("URLのフォーマットが違います。");
                  continue;
                }

                if (true == items!.List.Cast<YtdlpItem>().Any(item => item.Item1 == line))
                  continue;

                var item = await owner!.YtdlpParseDownloadUrl(line);
                if (item == null)
                  continue;

                items?.Add(item);
              }
              Enabled = true;
            }
            break;

          case ListItemType.FileOrDirectory:
            if (dataObject.GetDataPresent(DataFormats.FileDrop))
            {
              var pathes = dataObject.GetData(DataFormats.FileDrop) as string[];
              if (pathes == null || pathes?.Length <= 0)
                throw new Exception("Data length is zero");

              var srclist = items?.List;
              foreach (var path in pathes!.Where(IsValidDirectory))
              {
                if (false == srclist?.Cast<StringListItem>().Any(item => item.Value == path))
                  items?.Add(new StringListItem(path, DateTime.Now));
              }
            }
            break;

          default:
            throw new Exception("Can not detect editor type");
        }
      }
      catch (Exception exception)
      {
        MessageBox.Show(exception.Message);
      }
    }

    private async void ListEditItems_DragDrop(object sender, DragEventArgs e)
    {
      await AddItems(e.Data);
    }

    private async void MenuItemPaste_Click(object sender, EventArgs e)
    {
      await AddItems(Clipboard.GetDataObject());
    }

    public ListBox GetListEditorControl()
    {
      return ListEditItems;
    }

    private void ListEditItems_DoubleClick(object sender, EventArgs e)
    {
      var listbox = sender as ListBox;

      switch (EditorItemType)
      {
        case ListItemType.FileOrDirectory:
          var stritem = listbox?.SelectedItem as StringListItem;
          if (stritem == null)
            return;

          CustomProcess.ShellExecute(stritem.Value);
          break;

        case ListItemType.Url:
          var ytdlpitem = listbox?.SelectedItem as YtdlpItem;
          if (ytdlpitem == null)
            return;

          CustomProcess.ShellExecute(ytdlpitem.Item1);
          break;

        case ListItemType.PlainText:
          break;
      }
    }

    private void MenuItemOpen_Click(object sender, EventArgs e)
    {
      foreach(StringListItem item in ListEditItems.SelectedItems)
        CustomProcess.ShellExecute(item.Value);
    }
  }
}
