namespace ffmpeg_ytdlp_gui
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
      StdOutAndErrorView = new System.Windows.Forms.TextBox();
      BtnClose = new System.Windows.Forms.Button();
      BtnToggleReader = new System.Windows.Forms.Button();
      BtnSubmitSaveFile = new System.Windows.Forms.Button();
      _CustomButton = new System.Windows.Forms.Button();
      SuspendLayout();
      // 
      // StdOutAndErrorView
      // 
      StdOutAndErrorView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      StdOutAndErrorView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      StdOutAndErrorView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      StdOutAndErrorView.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      StdOutAndErrorView.Location = new System.Drawing.Point(0, 0);
      StdOutAndErrorView.MaxLength = 0;
      StdOutAndErrorView.Multiline = true;
      StdOutAndErrorView.Name = "StdOutAndErrorView";
      StdOutAndErrorView.ReadOnly = true;
      StdOutAndErrorView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      StdOutAndErrorView.Size = new System.Drawing.Size(873, 437);
      StdOutAndErrorView.TabIndex = 0;
      StdOutAndErrorView.TabStop = false;
      // 
      // BtnClose
      // 
      BtnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      BtnClose.Enabled = false;
      BtnClose.Location = new System.Drawing.Point(781, 453);
      BtnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      BtnClose.Name = "BtnClose";
      BtnClose.Size = new System.Drawing.Size(80, 30);
      BtnClose.TabIndex = 1;
      BtnClose.TabStop = false;
      BtnClose.Text = " 閉じる";
      BtnClose.UseVisualStyleBackColor = true;
      BtnClose.Click += BtnClose_Click;
      // 
      // BtnToggleReader
      // 
      BtnToggleReader.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      BtnToggleReader.Location = new System.Drawing.Point(698, 453);
      BtnToggleReader.Name = "BtnToggleReader";
      BtnToggleReader.Size = new System.Drawing.Size(80, 30);
      BtnToggleReader.TabIndex = 2;
      BtnToggleReader.Text = "読込中断";
      BtnToggleReader.UseVisualStyleBackColor = true;
      BtnToggleReader.Click += ToggleReader_Click;
      // 
      // BtnSubmitSaveFile
      // 
      BtnSubmitSaveFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
      BtnSubmitSaveFile.Enabled = false;
      BtnSubmitSaveFile.Location = new System.Drawing.Point(12, 453);
      BtnSubmitSaveFile.Name = "BtnSubmitSaveFile";
      BtnSubmitSaveFile.Size = new System.Drawing.Size(80, 30);
      BtnSubmitSaveFile.TabIndex = 3;
      BtnSubmitSaveFile.Text = "保存";
      BtnSubmitSaveFile.UseVisualStyleBackColor = true;
      BtnSubmitSaveFile.Click += BtnSubmitSaveFile_Click;
      // 
      // _CustomButton
      // 
      _CustomButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      _CustomButton.AutoSize = true;
      _CustomButton.Location = new System.Drawing.Point(592, 453);
      _CustomButton.Name = "_CustomButton";
      _CustomButton.Padding = new System.Windows.Forms.Padding(4);
      _CustomButton.Size = new System.Drawing.Size(86, 30);
      _CustomButton.TabIndex = 4;
      _CustomButton.Text = "カスタムボタン";
      _CustomButton.UseVisualStyleBackColor = true;
      _CustomButton.Visible = false;
      _CustomButton.Click += OnCustomButtonClick;
      // 
      // StdoutForm
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(873, 493);
      Controls.Add(_CustomButton);
      Controls.Add(BtnSubmitSaveFile);
      Controls.Add(BtnToggleReader);
      Controls.Add(BtnClose);
      Controls.Add(StdOutAndErrorView);
      Font = new System.Drawing.Font("MS UI Gothic", 9F);
      Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "StdoutForm";
      ShowIcon = false;
      ShowInTaskbar = false;
      Text = "View of process stdout,stderr or both.";
      FormClosing += StdoutForm_FormClosing;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.TextBox StdOutAndErrorView;
    private System.Windows.Forms.Button BtnClose;
    private System.Windows.Forms.Button BtnToggleReader;
    private System.Windows.Forms.Button BtnSubmitSaveFile;
    private System.Windows.Forms.Button _CustomButton;
  }
}