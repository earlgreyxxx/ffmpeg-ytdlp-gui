using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ffmpeg_ytdlp_gui
{
  public partial class ListEditor : Form
  {
    private ListEditor()
    {
      InitializeComponent();
    }

    public ListEditor(BindingSource bindingSource) : this()
    {
      ListEditItems.DataSource = bindingSource;
    }

    private void SubmitClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void SubmitClear_Click(object sender, EventArgs e)
    {
      var items = ListEditItems.DataSource as BindingSource;
      items?.Clear();
    }

    private void SubmitDelete_Click(object sender, EventArgs e)
    {
      var items = ListEditItems.DataSource as BindingSource;
      foreach (var item in ListEditItems.SelectedItems.Cast<object>().ToArray())
        items?.Remove(item);
    }
  }
}
