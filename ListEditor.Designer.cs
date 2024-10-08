﻿namespace ffmpeg_ytdlp_gui
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
      components = new System.ComponentModel.Container();
      SubmitClose = new System.Windows.Forms.Button();
      ListEditItems = new System.Windows.Forms.ListBox();
      DirectoryContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
      MenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
      MenuItemDirectoryPaste = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      MenuItemDirectoryDelete = new System.Windows.Forms.ToolStripMenuItem();
      SubmitDelete = new System.Windows.Forms.Button();
      SubmitClear = new System.Windows.Forms.Button();
      PlainTextContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
      MenuItemTextPaste = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      MenuItemTextDelete = new System.Windows.Forms.ToolStripMenuItem();
      DirectoryContextMenu.SuspendLayout();
      PlainTextContextMenu.SuspendLayout();
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
      ListEditItems.AllowDrop = true;
      ListEditItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      ListEditItems.ImeMode = System.Windows.Forms.ImeMode.Disable;
      ListEditItems.Location = new System.Drawing.Point(12, 12);
      ListEditItems.Name = "ListEditItems";
      ListEditItems.Size = new System.Drawing.Size(484, 184);
      ListEditItems.TabIndex = 1;
      ListEditItems.DragDrop += ListEditItems_DragDrop;
      ListEditItems.DragEnter += ListEditItems_DragEnter;
      ListEditItems.DoubleClick += ListEditItems_DoubleClick;
      // 
      // DirectoryContextMenu
      // 
      DirectoryContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { MenuItemOpen, MenuItemDirectoryPaste, toolStripSeparator1, MenuItemDirectoryDelete });
      DirectoryContextMenu.Name = "ContextMenu";
      DirectoryContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      DirectoryContextMenu.ShowImageMargin = false;
      DirectoryContextMenu.Size = new System.Drawing.Size(117, 76);
      DirectoryContextMenu.Opening += ContextMenu_Opening;
      // 
      // MenuItemOpen
      // 
      MenuItemOpen.Name = "MenuItemOpen";
      MenuItemOpen.Size = new System.Drawing.Size(116, 22);
      MenuItemOpen.Text = "開く";
      MenuItemOpen.Click += MenuItemOpen_Click;
      // 
      // MenuItemDirectoryPaste
      // 
      MenuItemDirectoryPaste.Name = "MenuItemDirectoryPaste";
      MenuItemDirectoryPaste.Size = new System.Drawing.Size(116, 22);
      MenuItemDirectoryPaste.Text = "ペースト(&p)";
      MenuItemDirectoryPaste.Click += MenuItemPaste_Click;
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
      // 
      // MenuItemDirectoryDelete
      // 
      MenuItemDirectoryDelete.Name = "MenuItemDirectoryDelete";
      MenuItemDirectoryDelete.Size = new System.Drawing.Size(116, 22);
      MenuItemDirectoryDelete.Text = "削除";
      MenuItemDirectoryDelete.Click += SubmitDelete_Click;
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
      // PlainTextContextMenu
      // 
      PlainTextContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { MenuItemTextPaste, toolStripSeparator2, MenuItemTextDelete });
      PlainTextContextMenu.Name = "PlainTextcontextMenu";
      PlainTextContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      PlainTextContextMenu.ShowImageMargin = false;
      PlainTextContextMenu.Size = new System.Drawing.Size(100, 54);
      PlainTextContextMenu.Opening += ContextMenu_Opening;
      // 
      // MenuItemTextPaste
      // 
      MenuItemTextPaste.Name = "MenuItemTextPaste";
      MenuItemTextPaste.Size = new System.Drawing.Size(99, 22);
      MenuItemTextPaste.Text = "ペースト";
      MenuItemTextPaste.Click += MenuItemPaste_Click;
      // 
      // toolStripSeparator2
      // 
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new System.Drawing.Size(96, 6);
      // 
      // MenuItemTextDelete
      // 
      MenuItemTextDelete.Name = "MenuItemTextDelete";
      MenuItemTextDelete.Size = new System.Drawing.Size(99, 22);
      MenuItemTextDelete.Text = "削除";
      MenuItemTextDelete.Click += SubmitDelete_Click;
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
      DirectoryContextMenu.ResumeLayout(false);
      PlainTextContextMenu.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Button SubmitClose;
    private System.Windows.Forms.ListBox ListEditItems;
    private System.Windows.Forms.Button SubmitDelete;
    private System.Windows.Forms.Button SubmitClear;
    private System.Windows.Forms.ContextMenuStrip DirectoryContextMenu;
    private System.Windows.Forms.ToolStripMenuItem MenuItemDirectoryPaste;
    private System.Windows.Forms.ToolStripMenuItem MenuItemDirectoryDelete;
    private System.Windows.Forms.ToolStripMenuItem MenuItemOpen;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ContextMenuStrip PlainTextContextMenu;
    private System.Windows.Forms.ToolStripMenuItem MenuItemTextPaste;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem MenuItemTextDelete;
  }
}