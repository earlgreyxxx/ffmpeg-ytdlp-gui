namespace ffmpeg_command_builder
{
  partial class Form1
  {
    /// <summary>
    /// 必要なデザイナー変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナーで生成されたコード

    /// <summary>
    /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.Commandlines = new System.Windows.Forms.TextBox();
      this.txtSS = new System.Windows.Forms.TextBox();
      this.findFolder = new System.Windows.Forms.FolderBrowserDialog();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtTo = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.btnClearSS = new System.Windows.Forms.Button();
      this.btnClearTo = new System.Windows.Forms.Button();
      this.findSaveBatchFile = new System.Windows.Forms.OpenFileDialog();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.chkUseHWDecoder = new System.Windows.Forms.CheckBox();
      this.HWDecoder = new System.Windows.Forms.ComboBox();
      this.cbDeinterlaceAlg = new System.Windows.Forms.ComboBox();
      this.chkFilterDeInterlace = new System.Windows.Forms.CheckBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.UseAudioEncoder = new System.Windows.Forms.ComboBox();
      this.UseVideoEncoder = new System.Windows.Forms.ComboBox();
      this.chkAudioOnly = new System.Windows.Forms.CheckBox();
      this.ResizeBox = new System.Windows.Forms.GroupBox();
      this.rbResizeNum = new System.Windows.Forms.RadioButton();
      this.resizeTo = new System.Windows.Forms.NumericUpDown();
      this.rbResizeHD = new System.Windows.Forms.RadioButton();
      this.rbResizeFullHD = new System.Windows.Forms.RadioButton();
      this.rbResizeNone = new System.Windows.Forms.RadioButton();
      this.RotateBox = new System.Windows.Forms.GroupBox();
      this.rbRotateNone = new System.Windows.Forms.RadioButton();
      this.rbRotateLeft = new System.Windows.Forms.RadioButton();
      this.rbRotateRight = new System.Windows.Forms.RadioButton();
      this.cbOutputDir = new System.Windows.Forms.ComboBox();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.label10 = new System.Windows.Forms.Label();
      this.FileName = new System.Windows.Forms.ComboBox();
      this.label11 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.FilePrefix = new System.Windows.Forms.TextBox();
      this.FileSuffix = new System.Windows.Forms.TextBox();
      this.OpenLogFile = new System.Windows.Forms.Button();
      this.btnClearDirs = new System.Windows.Forms.Button();
      this.OpenFolder = new System.Windows.Forms.Button();
      this.btnSubmitOpenDlg = new System.Windows.Forms.Button();
      this.btnSubmitSaveToFile = new System.Windows.Forms.Button();
      this.btnSubmitInvoke = new System.Windows.Forms.Button();
      this.LayoutBox = new System.Windows.Forms.GroupBox();
      this.label7 = new System.Windows.Forms.Label();
      this.VideoHeight = new System.Windows.Forms.NumericUpDown();
      this.VideoWidth = new System.Windows.Forms.NumericUpDown();
      this.rbPortrait = new System.Windows.Forms.RadioButton();
      this.rbLandscape = new System.Windows.Forms.RadioButton();
      this.btnClear = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.BitrateBox = new System.Windows.Forms.GroupBox();
      this.label12 = new System.Windows.Forms.Label();
      this.LookAhead = new System.Windows.Forms.NumericUpDown();
      this.aUnit = new System.Windows.Forms.Label();
      this.aBitrate = new System.Windows.Forms.NumericUpDown();
      this.aQualityLabel = new System.Windows.Forms.Label();
      this.vQualityLabel = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.cbPreset = new System.Windows.Forms.ComboBox();
      this.vBitrate = new System.Windows.Forms.NumericUpDown();
      this.chkEncodeAudio = new System.Windows.Forms.CheckBox();
      this.chkConstantQuality = new System.Windows.Forms.CheckBox();
      this.vUnit = new System.Windows.Forms.Label();
      this.btnApply = new System.Windows.Forms.Button();
      this.openInputFile = new System.Windows.Forms.OpenFileDialog();
      this.FileList = new System.Windows.Forms.ListBox();
      this.btnStop = new System.Windows.Forms.Button();
      this.btnStopAll = new System.Windows.Forms.Button();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.cbDevices = new System.Windows.Forms.ComboBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.ClearFileList = new System.Windows.Forms.Button();
      this.btnFFmpeg = new System.Windows.Forms.Button();
      this.label8 = new System.Windows.Forms.Label();
      this.openFFMpegFileDlg = new System.Windows.Forms.OpenFileDialog();
      this.btnFindInPath = new System.Windows.Forms.Button();
      this.ffmpeg = new System.Windows.Forms.ComboBox();
      this.StatusBar = new System.Windows.Forms.StatusStrip();
      this.OutputStderr = new System.Windows.Forms.ToolStripStatusLabel();
      this.CropBox = new System.Windows.Forms.GroupBox();
      this.CropLabel4 = new System.Windows.Forms.Label();
      this.CropLabel3 = new System.Windows.Forms.Label();
      this.CropLabel2 = new System.Windows.Forms.Label();
      this.CropLabel1 = new System.Windows.Forms.Label();
      this.CropY = new System.Windows.Forms.NumericUpDown();
      this.CropX = new System.Windows.Forms.NumericUpDown();
      this.CropHeight = new System.Windows.Forms.NumericUpDown();
      this.CropWidth = new System.Windows.Forms.NumericUpDown();
      this.chkCrop = new System.Windows.Forms.CheckBox();
      this.FileListBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.ResizeBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.resizeTo)).BeginInit();
      this.RotateBox.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.LayoutBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.VideoHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.VideoWidth)).BeginInit();
      this.BitrateBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.LookAhead)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.aBitrate)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).BeginInit();
      this.groupBox4.SuspendLayout();
      this.panel1.SuspendLayout();
      this.StatusBar.SuspendLayout();
      this.CropBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.CropY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CropX)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CropHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CropWidth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileListBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // Commandlines
      // 
      this.Commandlines.BackColor = System.Drawing.SystemColors.Window;
      this.Commandlines.Cursor = System.Windows.Forms.Cursors.Default;
      this.Commandlines.Enabled = false;
      this.Commandlines.Location = new System.Drawing.Point(8, 47);
      this.Commandlines.Multiline = true;
      this.Commandlines.Name = "Commandlines";
      this.Commandlines.ReadOnly = true;
      this.Commandlines.Size = new System.Drawing.Size(672, 68);
      this.Commandlines.TabIndex = 0;
      // 
      // txtSS
      // 
      this.txtSS.Location = new System.Drawing.Point(13, 42);
      this.txtSS.Name = "txtSS";
      this.txtSS.Size = new System.Drawing.Size(100, 19);
      this.txtSS.TabIndex = 4;
      // 
      // findFolder
      // 
      this.findFolder.Description = "出力先フォルダを選択してください。";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 12);
      this.label1.TabIndex = 2;
      this.label1.Text = "開始(-ss)";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(183, 27);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(53, 12);
      this.label2.TabIndex = 3;
      this.label2.Text = "終了(-to)";
      // 
      // txtTo
      // 
      this.txtTo.Location = new System.Drawing.Point(185, 42);
      this.txtTo.Name = "txtTo";
      this.txtTo.Size = new System.Drawing.Size(100, 19);
      this.txtTo.TabIndex = 6;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.linkLabel1);
      this.groupBox1.Controls.Add(this.btnClearSS);
      this.groupBox1.Controls.Add(this.btnClearTo);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.txtTo);
      this.groupBox1.Controls.Add(this.txtSS);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Location = new System.Drawing.Point(8, 127);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(351, 80);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "切り取り　　　　　　　　　　　　　　　";
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(58, 0);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(111, 12);
      this.linkLabel1.TabIndex = 3;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "時間指定構文の説明";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // btnClearSS
      // 
      this.btnClearSS.Location = new System.Drawing.Point(115, 41);
      this.btnClearSS.Name = "btnClearSS";
      this.btnClearSS.Size = new System.Drawing.Size(46, 20);
      this.btnClearSS.TabIndex = 5;
      this.btnClearSS.TabStop = false;
      this.btnClearSS.Text = "クリア";
      this.btnClearSS.UseVisualStyleBackColor = true;
      this.btnClearSS.Click += new System.EventHandler(this.btnClearSS_Click);
      // 
      // btnClearTo
      // 
      this.btnClearTo.Location = new System.Drawing.Point(287, 42);
      this.btnClearTo.Name = "btnClearTo";
      this.btnClearTo.Size = new System.Drawing.Size(46, 20);
      this.btnClearTo.TabIndex = 7;
      this.btnClearTo.TabStop = false;
      this.btnClearTo.Text = "クリア";
      this.btnClearTo.UseVisualStyleBackColor = true;
      this.btnClearTo.Click += new System.EventHandler(this.btnClearTo_Click);
      // 
      // findSaveBatchFile
      // 
      this.findSaveBatchFile.CheckFileExists = false;
      this.findSaveBatchFile.CheckPathExists = false;
      this.findSaveBatchFile.DefaultExt = "cmd";
      this.findSaveBatchFile.FileName = "ffmpeg-batch.cmd";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.chkUseHWDecoder);
      this.groupBox2.Controls.Add(this.HWDecoder);
      this.groupBox2.Controls.Add(this.cbDeinterlaceAlg);
      this.groupBox2.Controls.Add(this.chkFilterDeInterlace);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.UseAudioEncoder);
      this.groupBox2.Controls.Add(this.UseVideoEncoder);
      this.groupBox2.Controls.Add(this.chkAudioOnly);
      this.groupBox2.Location = new System.Drawing.Point(653, 213);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(145, 228);
      this.groupBox2.TabIndex = 7;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "その他";
      // 
      // chkUseHWDecoder
      // 
      this.chkUseHWDecoder.AutoSize = true;
      this.chkUseHWDecoder.Location = new System.Drawing.Point(12, 157);
      this.chkUseHWDecoder.Name = "chkUseHWDecoder";
      this.chkUseHWDecoder.Size = new System.Drawing.Size(112, 16);
      this.chkUseHWDecoder.TabIndex = 28;
      this.chkUseHWDecoder.Text = "HWデコーダー指定";
      this.chkUseHWDecoder.UseVisualStyleBackColor = true;
      this.chkUseHWDecoder.CheckedChanged += new System.EventHandler(this.chkUseHWDecoder_CheckedChanged);
      // 
      // HWDecoder
      // 
      this.HWDecoder.DisplayMember = "Label";
      this.HWDecoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.HWDecoder.Enabled = false;
      this.HWDecoder.FormattingEnabled = true;
      this.HWDecoder.Location = new System.Drawing.Point(11, 176);
      this.HWDecoder.Name = "HWDecoder";
      this.HWDecoder.Size = new System.Drawing.Size(119, 20);
      this.HWDecoder.TabIndex = 29;
      this.HWDecoder.ValueMember = "Value";
      // 
      // cbDeinterlaceAlg
      // 
      this.cbDeinterlaceAlg.DisplayMember = "Label";
      this.cbDeinterlaceAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDeinterlaceAlg.Enabled = false;
      this.cbDeinterlaceAlg.FormattingEnabled = true;
      this.cbDeinterlaceAlg.Location = new System.Drawing.Point(11, 125);
      this.cbDeinterlaceAlg.Name = "cbDeinterlaceAlg";
      this.cbDeinterlaceAlg.Size = new System.Drawing.Size(119, 20);
      this.cbDeinterlaceAlg.TabIndex = 27;
      this.cbDeinterlaceAlg.ValueMember = "Value";
      // 
      // chkFilterDeInterlace
      // 
      this.chkFilterDeInterlace.AutoSize = true;
      this.chkFilterDeInterlace.Location = new System.Drawing.Point(11, 105);
      this.chkFilterDeInterlace.Name = "chkFilterDeInterlace";
      this.chkFilterDeInterlace.Size = new System.Drawing.Size(122, 16);
      this.chkFilterDeInterlace.TabIndex = 26;
      this.chkFilterDeInterlace.Text = "デインターレース処理";
      this.chkFilterDeInterlace.CheckedChanged += new System.EventHandler(this.chkFilterDeInterlace_CheckedChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(13, 76);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(25, 12);
      this.label5.TabIndex = 26;
      this.label5.Text = "-c:a";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(13, 51);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(25, 12);
      this.label4.TabIndex = 26;
      this.label4.Text = "-c:v";
      // 
      // UseAudioEncoder
      // 
      this.UseAudioEncoder.DisplayMember = "Label";
      this.UseAudioEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.UseAudioEncoder.FormattingEnabled = true;
      this.UseAudioEncoder.Location = new System.Drawing.Point(44, 72);
      this.UseAudioEncoder.Name = "UseAudioEncoder";
      this.UseAudioEncoder.Size = new System.Drawing.Size(86, 20);
      this.UseAudioEncoder.TabIndex = 25;
      this.UseAudioEncoder.ValueMember = "Value";
      // 
      // UseVideoEncoder
      // 
      this.UseVideoEncoder.BackColor = System.Drawing.SystemColors.Window;
      this.UseVideoEncoder.DisplayMember = "Label";
      this.UseVideoEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.UseVideoEncoder.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.UseVideoEncoder.FormattingEnabled = true;
      this.UseVideoEncoder.Location = new System.Drawing.Point(44, 46);
      this.UseVideoEncoder.Name = "UseVideoEncoder";
      this.UseVideoEncoder.Size = new System.Drawing.Size(86, 20);
      this.UseVideoEncoder.TabIndex = 24;
      this.UseVideoEncoder.ValueMember = "Value";
      this.UseVideoEncoder.SelectedIndexChanged += new System.EventHandler(this.UseVideoEncoder_SelectedIndexChanged);
      // 
      // chkAudioOnly
      // 
      this.chkAudioOnly.AutoSize = true;
      this.chkAudioOnly.Location = new System.Drawing.Point(11, 23);
      this.chkAudioOnly.Name = "chkAudioOnly";
      this.chkAudioOnly.Size = new System.Drawing.Size(93, 16);
      this.chkAudioOnly.TabIndex = 23;
      this.chkAudioOnly.Text = "音声のみ出力";
      this.chkAudioOnly.UseVisualStyleBackColor = true;
      this.chkAudioOnly.CheckedChanged += new System.EventHandler(this.chkAudioOnly_CheckedChanged);
      // 
      // ResizeBox
      // 
      this.ResizeBox.Controls.Add(this.rbResizeNum);
      this.ResizeBox.Controls.Add(this.resizeTo);
      this.ResizeBox.Controls.Add(this.rbResizeHD);
      this.ResizeBox.Controls.Add(this.rbResizeFullHD);
      this.ResizeBox.Controls.Add(this.rbResizeNone);
      this.ResizeBox.Location = new System.Drawing.Point(335, 213);
      this.ResizeBox.Name = "ResizeBox";
      this.ResizeBox.Size = new System.Drawing.Size(312, 49);
      this.ResizeBox.TabIndex = 8;
      this.ResizeBox.TabStop = false;
      this.ResizeBox.Text = "リサイズ：短辺指定";
      // 
      // rbResizeNum
      // 
      this.rbResizeNum.AutoSize = true;
      this.rbResizeNum.Location = new System.Drawing.Point(187, 21);
      this.rbResizeNum.Name = "rbResizeNum";
      this.rbResizeNum.Size = new System.Drawing.Size(59, 16);
      this.rbResizeNum.TabIndex = 15;
      this.rbResizeNum.Tag = "-1";
      this.rbResizeNum.Text = "指定値";
      this.rbResizeNum.UseVisualStyleBackColor = true;
      this.rbResizeNum.CheckedChanged += new System.EventHandler(this.rbResizeNum_CheckedChanged);
      // 
      // resizeTo
      // 
      this.resizeTo.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ffmpeg_command_builder.Properties.Settings.Default, "resize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.resizeTo.Enabled = false;
      this.resizeTo.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.resizeTo.Location = new System.Drawing.Point(250, 20);
      this.resizeTo.Maximum = new decimal(new int[] {
            4320,
            0,
            0,
            0});
      this.resizeTo.Minimum = new decimal(new int[] {
            320,
            0,
            0,
            0});
      this.resizeTo.Name = "resizeTo";
      this.resizeTo.Size = new System.Drawing.Size(54, 19);
      this.resizeTo.TabIndex = 14;
      this.resizeTo.TabStop = false;
      this.resizeTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.resizeTo.Value = global::ffmpeg_command_builder.Properties.Settings.Default.resize;
      // 
      // rbResizeHD
      // 
      this.rbResizeHD.AutoSize = true;
      this.rbResizeHD.Location = new System.Drawing.Point(128, 21);
      this.rbResizeHD.Name = "rbResizeHD";
      this.rbResizeHD.Size = new System.Drawing.Size(53, 16);
      this.rbResizeHD.TabIndex = 13;
      this.rbResizeHD.Tag = "720";
      this.rbResizeHD.Text = "720px";
      // 
      // rbResizeFullHD
      // 
      this.rbResizeFullHD.AutoSize = true;
      this.rbResizeFullHD.Location = new System.Drawing.Point(63, 21);
      this.rbResizeFullHD.Name = "rbResizeFullHD";
      this.rbResizeFullHD.Size = new System.Drawing.Size(59, 16);
      this.rbResizeFullHD.TabIndex = 12;
      this.rbResizeFullHD.Tag = "1080";
      this.rbResizeFullHD.Text = "1080px";
      // 
      // rbResizeNone
      // 
      this.rbResizeNone.AutoSize = true;
      this.rbResizeNone.Checked = true;
      this.rbResizeNone.Location = new System.Drawing.Point(14, 21);
      this.rbResizeNone.Name = "rbResizeNone";
      this.rbResizeNone.Size = new System.Drawing.Size(42, 16);
      this.rbResizeNone.TabIndex = 11;
      this.rbResizeNone.TabStop = true;
      this.rbResizeNone.Tag = "0";
      this.rbResizeNone.Text = "なし";
      // 
      // RotateBox
      // 
      this.RotateBox.Controls.Add(this.rbRotateNone);
      this.RotateBox.Controls.Add(this.rbRotateLeft);
      this.RotateBox.Controls.Add(this.rbRotateRight);
      this.RotateBox.Location = new System.Drawing.Point(336, 268);
      this.RotateBox.Name = "RotateBox";
      this.RotateBox.Size = new System.Drawing.Size(311, 47);
      this.RotateBox.TabIndex = 9;
      this.RotateBox.TabStop = false;
      this.RotateBox.Text = "回転：90°";
      // 
      // rbRotateNone
      // 
      this.rbRotateNone.AutoSize = true;
      this.rbRotateNone.Checked = true;
      this.rbRotateNone.Location = new System.Drawing.Point(12, 21);
      this.rbRotateNone.Name = "rbRotateNone";
      this.rbRotateNone.Size = new System.Drawing.Size(42, 16);
      this.rbRotateNone.TabIndex = 16;
      this.rbRotateNone.TabStop = true;
      this.rbRotateNone.Tag = "0";
      this.rbRotateNone.Text = "なし";
      // 
      // rbRotateLeft
      // 
      this.rbRotateLeft.AutoSize = true;
      this.rbRotateLeft.Location = new System.Drawing.Point(131, 21);
      this.rbRotateLeft.Name = "rbRotateLeft";
      this.rbRotateLeft.Size = new System.Drawing.Size(79, 16);
      this.rbRotateLeft.TabIndex = 18;
      this.rbRotateLeft.Tag = "2";
      this.rbRotateLeft.Text = "半時計周り";
      // 
      // rbRotateRight
      // 
      this.rbRotateRight.AutoSize = true;
      this.rbRotateRight.Location = new System.Drawing.Point(60, 21);
      this.rbRotateRight.Name = "rbRotateRight";
      this.rbRotateRight.Size = new System.Drawing.Size(67, 16);
      this.rbRotateRight.TabIndex = 17;
      this.rbRotateRight.Tag = "1";
      this.rbRotateRight.Text = "時計周り";
      // 
      // cbOutputDir
      // 
      this.cbOutputDir.Location = new System.Drawing.Point(11, 83);
      this.cbOutputDir.Name = "cbOutputDir";
      this.cbOutputDir.Size = new System.Drawing.Size(225, 20);
      this.cbOutputDir.Sorted = true;
      this.cbOutputDir.TabIndex = 16;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.label10);
      this.groupBox5.Controls.Add(this.FileName);
      this.groupBox5.Controls.Add(this.label11);
      this.groupBox5.Controls.Add(this.label9);
      this.groupBox5.Controls.Add(this.FilePrefix);
      this.groupBox5.Controls.Add(this.FileSuffix);
      this.groupBox5.Controls.Add(this.OpenLogFile);
      this.groupBox5.Controls.Add(this.btnClearDirs);
      this.groupBox5.Controls.Add(this.OpenFolder);
      this.groupBox5.Controls.Add(this.btnSubmitOpenDlg);
      this.groupBox5.Controls.Add(this.cbOutputDir);
      this.groupBox5.Controls.Add(this.btnSubmitSaveToFile);
      this.groupBox5.Location = new System.Drawing.Point(8, 295);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(321, 146);
      this.groupBox5.TabIndex = 11;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "出力";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.label10.Location = new System.Drawing.Point(11, 47);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(257, 11);
      this.label10.TabIndex = 30;
      this.label10.Text = "※元ファイル名以外を指定すると、連番号が付加されます。";
      // 
      // FileName
      // 
      this.FileName.FormattingEnabled = true;
      this.FileName.ItemHeight = 12;
      this.FileName.Items.AddRange(new object[] {
            "元ファイル名"});
      this.FileName.Location = new System.Drawing.Point(142, 22);
      this.FileName.Name = "FileName";
      this.FileName.Size = new System.Drawing.Size(104, 20);
      this.FileName.TabIndex = 29;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(11, 68);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(74, 12);
      this.label11.TabIndex = 28;
      this.label11.Text = "保存フォルダー";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(11, 27);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(51, 12);
      this.label9.TabIndex = 26;
      this.label9.Text = "ファイル名";
      // 
      // FilePrefix
      // 
      this.FilePrefix.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ffmpeg_command_builder.Properties.Settings.Default, "prefix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.FilePrefix.Location = new System.Drawing.Point(78, 23);
      this.FilePrefix.Name = "FilePrefix";
      this.FilePrefix.Size = new System.Drawing.Size(62, 19);
      this.FilePrefix.TabIndex = 13;
      this.FilePrefix.Text = global::ffmpeg_command_builder.Properties.Settings.Default.prefix;
      this.FilePrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // FileSuffix
      // 
      this.FileSuffix.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ffmpeg_command_builder.Properties.Settings.Default, "suffix", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.FileSuffix.Location = new System.Drawing.Point(248, 23);
      this.FileSuffix.Name = "FileSuffix";
      this.FileSuffix.Size = new System.Drawing.Size(62, 19);
      this.FileSuffix.TabIndex = 15;
      this.FileSuffix.Text = global::ffmpeg_command_builder.Properties.Settings.Default.suffix;
      this.FileSuffix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // OpenLogFile
      // 
      this.OpenLogFile.Location = new System.Drawing.Point(12, 109);
      this.OpenLogFile.Name = "OpenLogFile";
      this.OpenLogFile.Size = new System.Drawing.Size(62, 23);
      this.OpenLogFile.TabIndex = 24;
      this.OpenLogFile.TabStop = false;
      this.OpenLogFile.Text = "ログ表示";
      this.OpenLogFile.UseVisualStyleBackColor = true;
      this.OpenLogFile.Click += new System.EventHandler(this.OpenLogFile_Click);
      // 
      // btnClearDirs
      // 
      this.btnClearDirs.Location = new System.Drawing.Point(280, 83);
      this.btnClearDirs.Name = "btnClearDirs";
      this.btnClearDirs.Size = new System.Drawing.Size(29, 20);
      this.btnClearDirs.TabIndex = 8;
      this.btnClearDirs.TabStop = false;
      this.btnClearDirs.Text = "✖";
      this.btnClearDirs.UseVisualStyleBackColor = true;
      this.btnClearDirs.Click += new System.EventHandler(this.btnClearDirs_Click);
      // 
      // OpenFolder
      // 
      this.OpenFolder.Location = new System.Drawing.Point(75, 109);
      this.OpenFolder.Name = "OpenFolder";
      this.OpenFolder.Size = new System.Drawing.Size(109, 23);
      this.OpenFolder.TabIndex = 10;
      this.OpenFolder.TabStop = false;
      this.OpenFolder.Text = "保存フォルダーを開く";
      this.OpenFolder.UseVisualStyleBackColor = true;
      this.OpenFolder.Click += new System.EventHandler(this.OpenFolder_Click);
      // 
      // btnSubmitOpenDlg
      // 
      this.btnSubmitOpenDlg.Location = new System.Drawing.Point(242, 83);
      this.btnSubmitOpenDlg.Name = "btnSubmitOpenDlg";
      this.btnSubmitOpenDlg.Size = new System.Drawing.Size(37, 20);
      this.btnSubmitOpenDlg.TabIndex = 9;
      this.btnSubmitOpenDlg.TabStop = false;
      this.btnSubmitOpenDlg.Text = "参照";
      this.btnSubmitOpenDlg.UseVisualStyleBackColor = true;
      this.btnSubmitOpenDlg.Click += new System.EventHandler(this.btnSubmitOpenDlg_Click);
      // 
      // btnSubmitSaveToFile
      // 
      this.btnSubmitSaveToFile.Location = new System.Drawing.Point(185, 109);
      this.btnSubmitSaveToFile.Name = "btnSubmitSaveToFile";
      this.btnSubmitSaveToFile.Size = new System.Drawing.Size(124, 23);
      this.btnSubmitSaveToFile.TabIndex = 23;
      this.btnSubmitSaveToFile.TabStop = false;
      this.btnSubmitSaveToFile.Text = "バッチファイルとして保存";
      this.btnSubmitSaveToFile.UseVisualStyleBackColor = true;
      this.btnSubmitSaveToFile.Click += new System.EventHandler(this.btnSubmitSaveToFile_Click);
      // 
      // btnSubmitInvoke
      // 
      this.btnSubmitInvoke.Enabled = false;
      this.btnSubmitInvoke.Location = new System.Drawing.Point(612, 113);
      this.btnSubmitInvoke.Name = "btnSubmitInvoke";
      this.btnSubmitInvoke.Size = new System.Drawing.Size(54, 23);
      this.btnSubmitInvoke.TabIndex = 1;
      this.btnSubmitInvoke.TabStop = false;
      this.btnSubmitInvoke.Text = "実行";
      this.btnSubmitInvoke.UseVisualStyleBackColor = true;
      this.btnSubmitInvoke.Click += new System.EventHandler(this.btnSubmitInvoke_Click);
      // 
      // LayoutBox
      // 
      this.LayoutBox.Controls.Add(this.label7);
      this.LayoutBox.Controls.Add(this.VideoHeight);
      this.LayoutBox.Controls.Add(this.VideoWidth);
      this.LayoutBox.Controls.Add(this.rbPortrait);
      this.LayoutBox.Controls.Add(this.rbLandscape);
      this.LayoutBox.Location = new System.Drawing.Point(335, 321);
      this.LayoutBox.Name = "LayoutBox";
      this.LayoutBox.Size = new System.Drawing.Size(311, 58);
      this.LayoutBox.TabIndex = 12;
      this.LayoutBox.TabStop = false;
      this.LayoutBox.Text = "元動画の情報(リサイズとクロップ時のヒント情報)";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(227, 32);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(17, 12);
      this.label7.TabIndex = 17;
      this.label7.Text = "×";
      // 
      // VideoHeight
      // 
      this.VideoHeight.Location = new System.Drawing.Point(244, 28);
      this.VideoHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.VideoHeight.Name = "VideoHeight";
      this.VideoHeight.Size = new System.Drawing.Size(57, 19);
      this.VideoHeight.TabIndex = 16;
      this.VideoHeight.TabStop = false;
      this.VideoHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.VideoHeight.Value = new decimal(new int[] {
            2160,
            0,
            0,
            0});
      // 
      // VideoWidth
      // 
      this.VideoWidth.Location = new System.Drawing.Point(168, 28);
      this.VideoWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.VideoWidth.Name = "VideoWidth";
      this.VideoWidth.Size = new System.Drawing.Size(57, 19);
      this.VideoWidth.TabIndex = 16;
      this.VideoWidth.TabStop = false;
      this.VideoWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.VideoWidth.Value = new decimal(new int[] {
            3840,
            0,
            0,
            0});
      // 
      // rbPortrait
      // 
      this.rbPortrait.AutoSize = true;
      this.rbPortrait.Location = new System.Drawing.Point(91, 29);
      this.rbPortrait.Name = "rbPortrait";
      this.rbPortrait.Size = new System.Drawing.Size(61, 16);
      this.rbPortrait.TabIndex = 15;
      this.rbPortrait.Text = "Portrait";
      this.rbPortrait.UseVisualStyleBackColor = true;
      // 
      // rbLandscape
      // 
      this.rbLandscape.AutoSize = true;
      this.rbLandscape.Checked = true;
      this.rbLandscape.Location = new System.Drawing.Point(14, 28);
      this.rbLandscape.Name = "rbLandscape";
      this.rbLandscape.Size = new System.Drawing.Size(74, 16);
      this.rbLandscape.TabIndex = 14;
      this.rbLandscape.TabStop = true;
      this.rbLandscape.Text = "landscape";
      this.rbLandscape.UseVisualStyleBackColor = true;
      // 
      // btnClear
      // 
      this.btnClear.Location = new System.Drawing.Point(685, 92);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(113, 23);
      this.btnClear.TabIndex = 2;
      this.btnClear.TabStop = false;
      this.btnClear.Text = "クリア";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 4);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(344, 12);
      this.label3.TabIndex = 0;
      this.label3.Text = "動画ファイルをドラッグ＆ドロップするか、ダブルクリックして選択して下さい。";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.label3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DropArea_MouseDoubleClick);
      // 
      // BitrateBox
      // 
      this.BitrateBox.Controls.Add(this.label12);
      this.BitrateBox.Controls.Add(this.LookAhead);
      this.BitrateBox.Controls.Add(this.aUnit);
      this.BitrateBox.Controls.Add(this.aBitrate);
      this.BitrateBox.Controls.Add(this.aQualityLabel);
      this.BitrateBox.Controls.Add(this.vQualityLabel);
      this.BitrateBox.Controls.Add(this.label6);
      this.BitrateBox.Controls.Add(this.cbPreset);
      this.BitrateBox.Controls.Add(this.vBitrate);
      this.BitrateBox.Controls.Add(this.chkEncodeAudio);
      this.BitrateBox.Controls.Add(this.chkConstantQuality);
      this.BitrateBox.Controls.Add(this.vUnit);
      this.BitrateBox.Location = new System.Drawing.Point(366, 127);
      this.BitrateBox.Name = "BitrateBox";
      this.BitrateBox.Size = new System.Drawing.Size(432, 80);
      this.BitrateBox.TabIndex = 15;
      this.BitrateBox.TabStop = false;
      this.BitrateBox.Text = "出力品質";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(268, 53);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(102, 12);
      this.label12.TabIndex = 28;
      this.label12.Text = "先行探索フレーム数";
      // 
      // LookAhead
      // 
      this.LookAhead.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ffmpeg_command_builder.Properties.Settings.Default, "lookAhead", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.LookAhead.Location = new System.Drawing.Point(371, 49);
      this.LookAhead.Name = "LookAhead";
      this.LookAhead.Size = new System.Drawing.Size(51, 19);
      this.LookAhead.TabIndex = 22;
      this.LookAhead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.LookAhead.Value = global::ffmpeg_command_builder.Properties.Settings.Default.lookAhead;
      // 
      // aUnit
      // 
      this.aUnit.AutoSize = true;
      this.aUnit.Location = new System.Drawing.Point(219, 50);
      this.aUnit.Name = "aUnit";
      this.aUnit.Size = new System.Drawing.Size(30, 12);
      this.aUnit.TabIndex = 26;
      this.aUnit.Text = "Kbps";
      // 
      // aBitrate
      // 
      this.aBitrate.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ffmpeg_command_builder.Properties.Settings.Default, "ab", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.aBitrate.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.aBitrate.Location = new System.Drawing.Point(148, 46);
      this.aBitrate.Maximum = new decimal(new int[] {
            320,
            0,
            0,
            0});
      this.aBitrate.Name = "aBitrate";
      this.aBitrate.Size = new System.Drawing.Size(70, 19);
      this.aBitrate.TabIndex = 20;
      this.aBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.aBitrate.Value = global::ffmpeg_command_builder.Properties.Settings.Default.ab;
      // 
      // aQualityLabel
      // 
      this.aQualityLabel.AutoSize = true;
      this.aQualityLabel.Location = new System.Drawing.Point(119, 48);
      this.aQualityLabel.Name = "aQualityLabel";
      this.aQualityLabel.Size = new System.Drawing.Size(25, 12);
      this.aQualityLabel.TabIndex = 24;
      this.aQualityLabel.Text = "-b:a";
      // 
      // vQualityLabel
      // 
      this.vQualityLabel.Location = new System.Drawing.Point(94, 26);
      this.vQualityLabel.Name = "vQualityLabel";
      this.vQualityLabel.Size = new System.Drawing.Size(50, 12);
      this.vQualityLabel.TabIndex = 23;
      this.vQualityLabel.Text = "-b:v";
      this.vQualityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(265, 27);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(43, 12);
      this.label6.TabIndex = 22;
      this.label6.Text = "-preset";
      // 
      // cbPreset
      // 
      this.cbPreset.DisplayMember = "Label";
      this.cbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbPreset.FormattingEnabled = true;
      this.cbPreset.Location = new System.Drawing.Point(311, 23);
      this.cbPreset.Name = "cbPreset";
      this.cbPreset.Size = new System.Drawing.Size(109, 20);
      this.cbPreset.TabIndex = 21;
      this.cbPreset.ValueMember = "Value";
      // 
      // vBitrate
      // 
      this.vBitrate.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::ffmpeg_command_builder.Properties.Settings.Default, "bitrate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.vBitrate.Location = new System.Drawing.Point(148, 22);
      this.vBitrate.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
      this.vBitrate.Name = "vBitrate";
      this.vBitrate.Size = new System.Drawing.Size(70, 19);
      this.vBitrate.TabIndex = 18;
      this.vBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.vBitrate.Value = global::ffmpeg_command_builder.Properties.Settings.Default.bitrate;
      // 
      // chkEncodeAudio
      // 
      this.chkEncodeAudio.AutoSize = true;
      this.chkEncodeAudio.Checked = global::ffmpeg_command_builder.Properties.Settings.Default.acodec;
      this.chkEncodeAudio.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ffmpeg_command_builder.Properties.Settings.Default, "acodec", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.chkEncodeAudio.Location = new System.Drawing.Point(14, 47);
      this.chkEncodeAudio.Name = "chkEncodeAudio";
      this.chkEncodeAudio.Size = new System.Drawing.Size(93, 16);
      this.chkEncodeAudio.TabIndex = 19;
      this.chkEncodeAudio.Text = "音声エンコード";
      this.chkEncodeAudio.UseVisualStyleBackColor = true;
      this.chkEncodeAudio.CheckedChanged += new System.EventHandler(this.chkEncodeAudio_CheckedChanged);
      // 
      // chkConstantQuality
      // 
      this.chkConstantQuality.AutoSize = true;
      this.chkConstantQuality.Location = new System.Drawing.Point(14, 24);
      this.chkConstantQuality.Name = "chkConstantQuality";
      this.chkConstantQuality.Size = new System.Drawing.Size(72, 16);
      this.chkConstantQuality.TabIndex = 17;
      this.chkConstantQuality.TabStop = false;
      this.chkConstantQuality.Text = "固定品質";
      this.chkConstantQuality.CheckedChanged += new System.EventHandler(this.chkConstantQuality_CheckedChanged);
      // 
      // vUnit
      // 
      this.vUnit.AutoSize = true;
      this.vUnit.Location = new System.Drawing.Point(219, 26);
      this.vUnit.Name = "vUnit";
      this.vUnit.Size = new System.Drawing.Size(30, 12);
      this.vUnit.TabIndex = 1;
      this.vUnit.Text = "Kbps";
      // 
      // btnApply
      // 
      this.btnApply.Location = new System.Drawing.Point(685, 47);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new System.Drawing.Size(113, 39);
      this.btnApply.TabIndex = 1;
      this.btnApply.TabStop = false;
      this.btnApply.Text = "実行コマンド確認\r\n（表示更新）";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      // 
      // openInputFile
      // 
      this.openInputFile.DefaultExt = "mp4";
      this.openInputFile.Filter = "動画ファイル|*.mpg;*.mp4;*.mkv;*.ts;*.avi;*.webm;*.m4v;*.wmv|すべてのファイル|*.*";
      this.openInputFile.Multiselect = true;
      this.openInputFile.Title = "動画ファイルを選択してください。";
      // 
      // FileList
      // 
      this.FileList.AllowDrop = true;
      this.FileList.DisplayMember = "Label";
      this.FileList.FormattingEnabled = true;
      this.FileList.ItemHeight = 12;
      this.FileList.Location = new System.Drawing.Point(0, 19);
      this.FileList.Name = "FileList";
      this.FileList.Size = new System.Drawing.Size(790, 88);
      this.FileList.TabIndex = 29;
      this.FileList.ValueMember = "Value";
      this.FileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.DropArea_DragDrop);
      this.FileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.DropArea_DragEnter);
      this.FileList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DropArea_MouseDoubleClick);
      // 
      // btnStop
      // 
      this.btnStop.Enabled = false;
      this.btnStop.Location = new System.Drawing.Point(668, 113);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(56, 23);
      this.btnStop.TabIndex = 31;
      this.btnStop.TabStop = false;
      this.btnStop.Text = "中止";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // btnStopAll
      // 
      this.btnStopAll.Enabled = false;
      this.btnStopAll.Location = new System.Drawing.Point(726, 113);
      this.btnStopAll.Name = "btnStopAll";
      this.btnStopAll.Size = new System.Drawing.Size(64, 23);
      this.btnStopAll.TabIndex = 32;
      this.btnStopAll.TabStop = false;
      this.btnStopAll.Text = "全て中止";
      this.btnStopAll.UseVisualStyleBackColor = true;
      this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.cbDevices);
      this.groupBox4.Location = new System.Drawing.Point(335, 385);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(311, 56);
      this.groupBox4.TabIndex = 33;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "GPUデバイス";
      // 
      // cbDevices
      // 
      this.cbDevices.DisplayMember = "Label";
      this.cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDevices.FormattingEnabled = true;
      this.cbDevices.Location = new System.Drawing.Point(14, 21);
      this.cbDevices.Name = "cbDevices";
      this.cbDevices.Size = new System.Drawing.Size(290, 20);
      this.cbDevices.TabIndex = 0;
      this.cbDevices.TabStop = false;
      this.cbDevices.ValueMember = "Value";
      this.cbDevices.SelectedIndexChanged += new System.EventHandler(this.cbDevices_SelectedIndexChanged);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.ClearFileList);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.FileList);
      this.panel1.Controls.Add(this.btnStopAll);
      this.panel1.Controls.Add(this.btnStop);
      this.panel1.Controls.Add(this.btnSubmitInvoke);
      this.panel1.Location = new System.Drawing.Point(8, 454);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(790, 140);
      this.panel1.TabIndex = 34;
      // 
      // ClearFileList
      // 
      this.ClearFileList.Location = new System.Drawing.Point(0, 113);
      this.ClearFileList.Name = "ClearFileList";
      this.ClearFileList.Size = new System.Drawing.Size(75, 23);
      this.ClearFileList.TabIndex = 34;
      this.ClearFileList.TabStop = false;
      this.ClearFileList.Text = "クリア";
      this.ClearFileList.UseVisualStyleBackColor = true;
      this.ClearFileList.Click += new System.EventHandler(this.ClearFileList_Click);
      // 
      // btnFFmpeg
      // 
      this.btnFFmpeg.Location = new System.Drawing.Point(598, 9);
      this.btnFFmpeg.Name = "btnFFmpeg";
      this.btnFFmpeg.Size = new System.Drawing.Size(49, 23);
      this.btnFFmpeg.TabIndex = 36;
      this.btnFFmpeg.Text = "参照";
      this.btnFFmpeg.UseVisualStyleBackColor = true;
      this.btnFFmpeg.Click += new System.EventHandler(this.btnFFmpeg_Click);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(8, 15);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(98, 12);
      this.label8.TabIndex = 37;
      this.label8.Text = "ffmpeg実行ファイル";
      // 
      // openFFMpegFileDlg
      // 
      this.openFFMpegFileDlg.DefaultExt = "exe";
      this.openFFMpegFileDlg.FileName = "openFileDialog1";
      this.openFFMpegFileDlg.Filter = "実行ファイル|*.exe";
      this.openFFMpegFileDlg.Title = "ffmpeg実行ファイルを指定してください。";
      // 
      // btnFindInPath
      // 
      this.btnFindInPath.Location = new System.Drawing.Point(653, 9);
      this.btnFindInPath.Name = "btnFindInPath";
      this.btnFindInPath.Size = new System.Drawing.Size(144, 23);
      this.btnFindInPath.TabIndex = 38;
      this.btnFindInPath.Text = "環境変数PATHから探す";
      this.btnFindInPath.UseVisualStyleBackColor = true;
      this.btnFindInPath.Click += new System.EventHandler(this.btnFindInPath_Click);
      // 
      // ffmpeg
      // 
      this.ffmpeg.FormattingEnabled = true;
      this.ffmpeg.Location = new System.Drawing.Point(112, 11);
      this.ffmpeg.Name = "ffmpeg";
      this.ffmpeg.Size = new System.Drawing.Size(480, 20);
      this.ffmpeg.TabIndex = 39;
      // 
      // StatusBar
      // 
      this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OutputStderr});
      this.StatusBar.Location = new System.Drawing.Point(0, 596);
      this.StatusBar.Name = "StatusBar";
      this.StatusBar.Size = new System.Drawing.Size(806, 23);
      this.StatusBar.SizingGrip = false;
      this.StatusBar.TabIndex = 40;
      this.StatusBar.Text = "statusStrip1";
      // 
      // OutputStderr
      // 
      this.OutputStderr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.OutputStderr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.OutputStderr.Name = "OutputStderr";
      this.OutputStderr.Size = new System.Drawing.Size(791, 18);
      this.OutputStderr.Spring = true;
      this.OutputStderr.Text = "stderr";
      this.OutputStderr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // CropBox
      // 
      this.CropBox.Controls.Add(this.CropLabel4);
      this.CropBox.Controls.Add(this.CropLabel3);
      this.CropBox.Controls.Add(this.CropLabel2);
      this.CropBox.Controls.Add(this.CropLabel1);
      this.CropBox.Controls.Add(this.CropY);
      this.CropBox.Controls.Add(this.CropX);
      this.CropBox.Controls.Add(this.CropHeight);
      this.CropBox.Controls.Add(this.CropWidth);
      this.CropBox.Controls.Add(this.chkCrop);
      this.CropBox.Location = new System.Drawing.Point(9, 213);
      this.CropBox.Name = "CropBox";
      this.CropBox.Size = new System.Drawing.Size(319, 76);
      this.CropBox.TabIndex = 41;
      this.CropBox.TabStop = false;
      this.CropBox.Text = "クロップ";
      // 
      // CropLabel4
      // 
      this.CropLabel4.AutoSize = true;
      this.CropLabel4.Location = new System.Drawing.Point(243, 48);
      this.CropLabel4.Name = "CropLabel4";
      this.CropLabel4.Size = new System.Drawing.Size(12, 12);
      this.CropLabel4.TabIndex = 2;
      this.CropLabel4.Text = "Y";
      // 
      // CropLabel3
      // 
      this.CropLabel3.AutoSize = true;
      this.CropLabel3.Location = new System.Drawing.Point(165, 48);
      this.CropLabel3.Name = "CropLabel3";
      this.CropLabel3.Size = new System.Drawing.Size(12, 12);
      this.CropLabel3.TabIndex = 2;
      this.CropLabel3.Text = "X";
      // 
      // CropLabel2
      // 
      this.CropLabel2.AutoSize = true;
      this.CropLabel2.Location = new System.Drawing.Point(89, 48);
      this.CropLabel2.Name = "CropLabel2";
      this.CropLabel2.Size = new System.Drawing.Size(13, 12);
      this.CropLabel2.TabIndex = 2;
      this.CropLabel2.Text = "H";
      // 
      // CropLabel1
      // 
      this.CropLabel1.AutoSize = true;
      this.CropLabel1.Location = new System.Drawing.Point(13, 48);
      this.CropLabel1.Name = "CropLabel1";
      this.CropLabel1.Size = new System.Drawing.Size(14, 12);
      this.CropLabel1.TabIndex = 2;
      this.CropLabel1.Text = "W";
      // 
      // CropY
      // 
      this.CropY.Location = new System.Drawing.Point(257, 45);
      this.CropY.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
      this.CropY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
      this.CropY.Name = "CropY";
      this.CropY.Size = new System.Drawing.Size(54, 19);
      this.CropY.TabIndex = 12;
      this.CropY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.CropY.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
      // 
      // CropX
      // 
      this.CropX.Location = new System.Drawing.Point(179, 45);
      this.CropX.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
      this.CropX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
      this.CropX.Name = "CropX";
      this.CropX.Size = new System.Drawing.Size(54, 19);
      this.CropX.TabIndex = 11;
      this.CropX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.CropX.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
      // 
      // CropHeight
      // 
      this.CropHeight.Location = new System.Drawing.Point(104, 45);
      this.CropHeight.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
      this.CropHeight.Name = "CropHeight";
      this.CropHeight.Size = new System.Drawing.Size(54, 19);
      this.CropHeight.TabIndex = 10;
      this.CropHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // CropWidth
      // 
      this.CropWidth.Location = new System.Drawing.Point(28, 45);
      this.CropWidth.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
      this.CropWidth.Name = "CropWidth";
      this.CropWidth.Size = new System.Drawing.Size(54, 19);
      this.CropWidth.TabIndex = 9;
      this.CropWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // chkCrop
      // 
      this.chkCrop.AutoSize = true;
      this.chkCrop.Location = new System.Drawing.Point(16, 22);
      this.chkCrop.Name = "chkCrop";
      this.chkCrop.Size = new System.Drawing.Size(85, 16);
      this.chkCrop.TabIndex = 8;
      this.chkCrop.Text = "クロップを行う";
      this.chkCrop.UseVisualStyleBackColor = true;
      this.chkCrop.CheckedChanged += new System.EventHandler(this.chkCrop_CheckedChanged);
      // 
      // Form1
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(806, 619);
      this.Controls.Add(this.CropBox);
      this.Controls.Add(this.StatusBar);
      this.Controls.Add(this.ffmpeg);
      this.Controls.Add(this.btnFindInPath);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.btnFFmpeg);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.BitrateBox);
      this.Controls.Add(this.btnClear);
      this.Controls.Add(this.btnApply);
      this.Controls.Add(this.LayoutBox);
      this.Controls.Add(this.groupBox5);
      this.Controls.Add(this.RotateBox);
      this.Controls.Add(this.ResizeBox);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.Commandlines);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "ffmpeg command builder for NVENC and QSV : required ffmpeg 6.1 or higher";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResizeBox.ResumeLayout(false);
      this.ResizeBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.resizeTo)).EndInit();
      this.RotateBox.ResumeLayout(false);
      this.RotateBox.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.LayoutBox.ResumeLayout(false);
      this.LayoutBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.VideoHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.VideoWidth)).EndInit();
      this.BitrateBox.ResumeLayout(false);
      this.BitrateBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.LookAhead)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.aBitrate)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.vBitrate)).EndInit();
      this.groupBox4.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.StatusBar.ResumeLayout(false);
      this.StatusBar.PerformLayout();
      this.CropBox.ResumeLayout(false);
      this.CropBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.CropY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CropX)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CropHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CropWidth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.FileListBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox Commandlines;
    private System.Windows.Forms.TextBox txtSS;
    private System.Windows.Forms.FolderBrowserDialog findFolder;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTo;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.OpenFileDialog findSaveBatchFile;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox chkFilterDeInterlace;
    private System.Windows.Forms.RadioButton rbResizeHD;
    private System.Windows.Forms.RadioButton rbResizeFullHD;
    private System.Windows.Forms.RadioButton rbResizeNone;
    private System.Windows.Forms.GroupBox ResizeBox;
    private System.Windows.Forms.GroupBox RotateBox;
    private System.Windows.Forms.RadioButton rbRotateNone;
    private System.Windows.Forms.RadioButton rbRotateLeft;
    private System.Windows.Forms.RadioButton rbRotateRight;
    private System.Windows.Forms.ComboBox cbOutputDir;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.Button btnSubmitOpenDlg;
    private System.Windows.Forms.CheckBox chkEncodeAudio;
    private System.Windows.Forms.GroupBox LayoutBox;
    private System.Windows.Forms.RadioButton rbPortrait;
    private System.Windows.Forms.RadioButton rbLandscape;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnSubmitInvoke;
    private System.Windows.Forms.Button btnSubmitSaveToFile;
    private System.Windows.Forms.Button btnClearDirs;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox BitrateBox;
    private System.Windows.Forms.CheckBox chkConstantQuality;
    private System.Windows.Forms.Label vUnit;
    private System.Windows.Forms.NumericUpDown vBitrate;
    private System.Windows.Forms.Button btnApply;
    private System.Windows.Forms.Button btnClearSS;
    private System.Windows.Forms.Button btnClearTo;
    private System.Windows.Forms.CheckBox chkAudioOnly;
    private System.Windows.Forms.ComboBox UseVideoEncoder;
    private System.Windows.Forms.ComboBox UseAudioEncoder;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button OpenFolder;
    private System.Windows.Forms.OpenFileDialog openInputFile;
    private System.Windows.Forms.ListBox FileList;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnStopAll;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cbPreset;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.ComboBox cbDevices;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnFFmpeg;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.OpenFileDialog openFFMpegFileDlg;
    private System.Windows.Forms.Button btnFindInPath;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.ComboBox ffmpeg;
    private System.Windows.Forms.NumericUpDown resizeTo;
    private System.Windows.Forms.RadioButton rbResizeNum;
    private System.Windows.Forms.Button OpenLogFile;
    private System.Windows.Forms.Button ClearFileList;
    private System.Windows.Forms.ComboBox cbDeinterlaceAlg;
    private System.Windows.Forms.TextBox FilePrefix;
    private System.Windows.Forms.TextBox FileSuffix;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label vQualityLabel;
    private System.Windows.Forms.Label aQualityLabel;
    private System.Windows.Forms.Label aUnit;
    private System.Windows.Forms.NumericUpDown aBitrate;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.NumericUpDown LookAhead;
    private System.Windows.Forms.StatusStrip StatusBar;
    private System.Windows.Forms.ToolStripStatusLabel OutputStderr;
    private System.Windows.Forms.GroupBox CropBox;
    private System.Windows.Forms.CheckBox chkCrop;
    private System.Windows.Forms.Label CropLabel4;
    private System.Windows.Forms.Label CropLabel3;
    private System.Windows.Forms.Label CropLabel2;
    private System.Windows.Forms.Label CropLabel1;
    private System.Windows.Forms.NumericUpDown CropY;
    private System.Windows.Forms.NumericUpDown CropX;
    private System.Windows.Forms.NumericUpDown CropHeight;
    private System.Windows.Forms.NumericUpDown CropWidth;
    private System.Windows.Forms.ComboBox HWDecoder;
    private System.Windows.Forms.CheckBox chkUseHWDecoder;
    private System.Windows.Forms.NumericUpDown VideoHeight;
    private System.Windows.Forms.NumericUpDown VideoWidth;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox FileName;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.BindingSource FileListBindingSource;
  }
}

