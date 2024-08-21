using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ffmpeg_ytdlp_gui.libs;

namespace ffmpeg_ytdlp_gui
{
  using StringListItem = ListItem<string>;

  public partial class ListEditor : Form
  {
    ListItemType EditorItemType;

    private ListEditor()
    {
      InitializeComponent();
    }

    public ListEditor(BindingSource bindingSource,ListItemType type) : this()
    {
      ListEditItems.DataSource = bindingSource;
      EditorItemType = type;
    }

    private void SubmitClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void SubmitClear_Click(object sender, EventArgs e)
    {
      var items = ListEditItems.DataSource as BindingSource;
      var result = MessageBox.Show("全て削除します。よろしいですか？", "確認", MessageBoxButtons.OKCancel);
      if(result == DialogResult.OK)
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
          MenuItemPaste.Enabled = DataObject.GetDataPresent(DataFormats.UnicodeText);
          break;

        case ListItemType.FileOrDirectory:
          MenuItemPaste.Enabled = DataObject.GetDataPresent(DataFormats.FileDrop);
          break;
      }
    }

    private Func<string, bool> IsValidDirectory = path => !string.IsNullOrEmpty(path) && 
                                                          File.GetAttributes(path).HasFlag(FileAttributes.Directory) &&
                                                          Directory.Exists(path);

    private void AddItems(IDataObject? dataObject)
    {
      var items = ListEditItems.DataSource as BindingSource;
      try
      {
        if(dataObject == null)
          throw new ArgumentNullException(nameof(dataObject));

        switch (EditorItemType)
        {
          case ListItemType.PlainText:
            if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
              var lines = dataObject.GetData(DataFormats.UnicodeText) as string;
              if (string.IsNullOrEmpty(lines))
                throw new Exception("Data is empty or null");

              foreach (var line in lines.Split(Environment.NewLine.ToCharArray()).Where(str => !string.IsNullOrEmpty(str.Trim())))
              {
                items?.Add(line.Trim());
              }
            }
            break;

          case ListItemType.FileOrDirectory:
            if (dataObject.GetDataPresent(DataFormats.FileDrop))
            {
              var pathes = dataObject.GetData(DataFormats.FileDrop) as string[];
              if (pathes == null || pathes?.Length <= 0)
                throw new Exception("Data length is zero");

              foreach (var path in pathes!.Where(IsValidDirectory))
                items?.Add(new StringListItem(path, DateTime.Now));
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

    private void ListEditItems_DragDrop(object sender, DragEventArgs e)
    {
      AddItems(e.Data);
    }

    private void MenuItemPaste_Click(object sender, EventArgs e)
    {
      AddItems(Clipboard.GetDataObject());
    }
  }
}
