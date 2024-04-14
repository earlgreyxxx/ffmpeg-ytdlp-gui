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
      StdOutAndErrorView = new System.Windows.Forms.TextBox();
      BtnClose = new System.Windows.Forms.Button();
      StdoutBindingSource = new System.Windows.Forms.BindingSource(components);
      ((System.ComponentModel.ISupportInitialize)StdoutBindingSource).BeginInit();
      SuspendLayout();
      // 
      // StdOutAndErrorView
      // 
      StdOutAndErrorView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      StdOutAndErrorView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      StdOutAndErrorView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      StdOutAndErrorView.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      StdOutAndErrorView.Location = new System.Drawing.Point(0, 0);
      StdOutAndErrorView.Multiline = true;
      StdOutAndErrorView.Name = "StdOutAndErrorView";
      StdOutAndErrorView.ReadOnly = true;
      StdOutAndErrorView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      StdOutAndErrorView.Size = new System.Drawing.Size(856, 420);
      StdOutAndErrorView.TabIndex = 0;
      StdOutAndErrorView.TabStop = false;
      // 
      // BtnClose
      // 
      BtnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      BtnClose.Location = new System.Drawing.Point(780, 436);
      BtnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      BtnClose.Name = "BtnClose";
      BtnClose.Size = new System.Drawing.Size(64, 29);
      BtnClose.TabIndex = 1;
      BtnClose.TabStop = false;
      BtnClose.Text = " 閉じる";
      BtnClose.UseVisualStyleBackColor = true;
      BtnClose.Click += BtnClose_Click;
      // 
      // StdoutForm
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(856, 476);
      Controls.Add(BtnClose);
      Controls.Add(StdOutAndErrorView);
      Font = new System.Drawing.Font("MS UI Gothic", 9F);
      Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "StdoutForm";
      ShowIcon = false;
      ShowInTaskbar = false;
      Text = "ffmpeg outputs";
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