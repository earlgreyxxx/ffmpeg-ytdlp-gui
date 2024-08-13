namespace ffmpeg_ytdlp_gui
{
  partial class ListEditor
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      SubmitClose = new System.Windows.Forms.Button();
      ListEditItems = new System.Windows.Forms.ListBox();
      SubmitDelete = new System.Windows.Forms.Button();
      SubmitClear = new System.Windows.Forms.Button();
      SuspendLayout();
      // 
      // SubmitClose
      // 
      SubmitClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      SubmitClose.Location = new System.Drawing.Point(406, 215);
      SubmitClose.Name = "SubmitClose";
      SubmitClose.Size = new System.Drawing.Size(90, 26);
      SubmitClose.TabIndex = 0;
      SubmitClose.Text = "閉じる";
      SubmitClose.UseVisualStyleBackColor = true;
      SubmitClose.Click += SubmitClose_Click;
      // 
      // ListEditItems
      // 
      ListEditItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      ListEditItems.DisplayMember = "Label";
      ListEditItems.ImeMode = System.Windows.Forms.ImeMode.Disable;
      ListEditItems.Location = new System.Drawing.Point(12, 12);
      ListEditItems.Name = "ListEditItems";
      ListEditItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      ListEditItems.Size = new System.Drawing.Size(484, 184);
      ListEditItems.TabIndex = 1;
      ListEditItems.ValueMember = "Value";
      // 
      // SubmitDelete
      // 
      SubmitDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      SubmitDelete.Location = new System.Drawing.Point(245, 215);
      SubmitDelete.Name = "SubmitDelete";
      SubmitDelete.Size = new System.Drawing.Size(75, 26);
      SubmitDelete.TabIndex = 2;
      SubmitDelete.Text = "削除";
      SubmitDelete.UseVisualStyleBackColor = true;
      SubmitDelete.Click += SubmitDelete_Click;
      // 
      // SubmitClear
      // 
      SubmitClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      SubmitClear.Location = new System.Drawing.Point(326, 215);
      SubmitClear.Name = "SubmitClear";
      SubmitClear.Size = new System.Drawing.Size(75, 26);
      SubmitClear.TabIndex = 3;
      SubmitClear.Text = "クリア";
      SubmitClear.UseVisualStyleBackColor = true;
      SubmitClear.Click += SubmitClear_Click;
      // 
      // ListEditor
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(508, 253);
      Controls.Add(SubmitClear);
      Controls.Add(SubmitDelete);
      Controls.Add(ListEditItems);
      Controls.Add(SubmitClose);
      FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "ListEditor";
      ShowIcon = false;
      ShowInTaskbar = false;
      Text = "リスト編集";
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Button SubmitClose;
    private System.Windows.Forms.ListBox ListEditItems;
    private System.Windows.Forms.Button SubmitDelete;
    private System.Windows.Forms.Button SubmitClear;
  }
}