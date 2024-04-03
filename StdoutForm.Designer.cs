namespace ffmpeg_command_builder
{
  partial class StdoutForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StdoutForm));
      StdOutAndErrorView = new System.Windows.Forms.TextBox();
      BtnClose = new System.Windows.Forms.Button();
      StdoutBindingSource = new System.Windows.Forms.BindingSource(components);
      ((System.ComponentModel.ISupportInitialize)StdoutBindingSource).BeginInit();
      SuspendLayout();
      // 
      // StdOutAndErrorView
      // 
      resources.ApplyResources(StdOutAndErrorView, "StdOutAndErrorView");
      StdOutAndErrorView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      StdOutAndErrorView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      StdOutAndErrorView.Name = "StdOutAndErrorView";
      StdOutAndErrorView.ReadOnly = true;
      StdOutAndErrorView.TabStop = false;
      // 
      // BtnClose
      // 
      resources.ApplyResources(BtnClose, "BtnClose");
      BtnClose.Name = "BtnClose";
      BtnClose.TabStop = false;
      BtnClose.UseVisualStyleBackColor = true;
      BtnClose.Click += BtnClose_Click;
      // 
      // StdoutForm
      // 
      resources.ApplyResources(this, "$this");
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      Controls.Add(BtnClose);
      Controls.Add(StdOutAndErrorView);
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "StdoutForm";
      ShowIcon = false;
      ShowInTaskbar = false;
      ((System.ComponentModel.ISupportInitialize)StdoutBindingSource).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.TextBox StdOutAndErrorView;
    private System.Windows.Forms.Button BtnClose;
    private System.Windows.Forms.BindingSource StdoutBindingSource;
  }
}