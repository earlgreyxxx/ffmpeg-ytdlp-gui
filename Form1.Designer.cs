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
      components = new System.ComponentModel.Container();
      Commandlines = new System.Windows.Forms.TextBox();
      txtSS = new System.Windows.Forms.TextBox();
      FindFolder = new System.Windows.Forms.FolderBrowserDialog();
      label1 = new System.Windows.Forms.Label();
      label2 = new System.Windows.Forms.Label();
      txtTo = new System.Windows.Forms.TextBox();
      groupBox1 = new System.Windows.Forms.GroupBox();
      linkLabel1 = new System.Windows.Forms.LinkLabel();
      btnClearSS = new System.Windows.Forms.Button();
      btnClearTo = new System.Windows.Forms.Button();
      FindSaveBatchFile = new System.Windows.Forms.OpenFileDialog();
      groupBox2 = new System.Windows.Forms.GroupBox();
      chkUseHWDecoder = new System.Windows.Forms.CheckBox();
      HWDecoder = new System.Windows.Forms.ComboBox();
      label5 = new System.Windows.Forms.Label();
      label4 = new System.Windows.Forms.Label();
      UseAudioEncoder = new System.Windows.Forms.ComboBox();
      UseVideoEncoder = new System.Windows.Forms.ComboBox();
      cbDeinterlaceAlg = new System.Windows.Forms.ComboBox();
      chkFilterDeInterlace = new System.Windows.Forms.CheckBox();
      chkAudioOnly = new System.Windows.Forms.CheckBox();
      ResizeBox = new System.Windows.Forms.GroupBox();
      rbResizeNum = new System.Windows.Forms.RadioButton();
      resizeTo = new System.Windows.Forms.NumericUpDown();
      rbResizeHD = new System.Windows.Forms.RadioButton();
      rbResizeFullHD = new System.Windows.Forms.RadioButton();
      rbResizeNone = new System.Windows.Forms.RadioButton();
      RotateBox = new System.Windows.Forms.GroupBox();
      rbRotateNone = new System.Windows.Forms.RadioButton();
      rbRotateLeft = new System.Windows.Forms.RadioButton();
      rbRotateRight = new System.Windows.Forms.RadioButton();
      cbOutputDir = new System.Windows.Forms.ComboBox();
      FileContainer = new System.Windows.Forms.ComboBox();
      label13 = new System.Windows.Forms.Label();
      label10 = new System.Windows.Forms.Label();
      FileName = new System.Windows.Forms.ComboBox();
      OpenLogFile = new System.Windows.Forms.Button();
      OpenFolder = new System.Windows.Forms.Button();
      btnSubmitOpenDlg = new System.Windows.Forms.Button();
      btnSubmitSaveToFile = new System.Windows.Forms.Button();
      btnClearDirs = new System.Windows.Forms.Button();
      btnSubmitInvoke = new System.Windows.Forms.Button();
      LayoutBox = new System.Windows.Forms.GroupBox();
      label7 = new System.Windows.Forms.Label();
      VideoHeight = new System.Windows.Forms.NumericUpDown();
      VideoWidth = new System.Windows.Forms.NumericUpDown();
      rbPortrait = new System.Windows.Forms.RadioButton();
      rbLandscape = new System.Windows.Forms.RadioButton();
      btnClear = new System.Windows.Forms.Button();
      label3 = new System.Windows.Forms.Label();
      BitrateBox = new System.Windows.Forms.GroupBox();
      label12 = new System.Windows.Forms.Label();
      LookAhead = new System.Windows.Forms.NumericUpDown();
      aUnit = new System.Windows.Forms.Label();
      aBitrate = new System.Windows.Forms.NumericUpDown();
      aQualityLabel = new System.Windows.Forms.Label();
      vQualityLabel = new System.Windows.Forms.Label();
      label6 = new System.Windows.Forms.Label();
      cbPreset = new System.Windows.Forms.ComboBox();
      vBitrate = new System.Windows.Forms.NumericUpDown();
      chkEncodeAudio = new System.Windows.Forms.CheckBox();
      chkConstantQuality = new System.Windows.Forms.CheckBox();
      vUnit = new System.Windows.Forms.Label();
      btnApply = new System.Windows.Forms.Button();
      OpenInputFile = new System.Windows.Forms.OpenFileDialog();
      FileList = new System.Windows.Forms.ListBox();
      btnStop = new System.Windows.Forms.Button();
      btnStopAll = new System.Windows.Forms.Button();
      groupBox4 = new System.Windows.Forms.GroupBox();
      cbDevices = new System.Windows.Forms.ComboBox();
      panel1 = new System.Windows.Forms.Panel();
      ClearFileList = new System.Windows.Forms.Button();
      OpenDecoderHelp = new System.Windows.Forms.Button();
      OpenEncoderHelp = new System.Windows.Forms.Button();
      btnFFmpeg = new System.Windows.Forms.Button();
      label8 = new System.Windows.Forms.Label();
      OpenFFMpegFileDlg = new System.Windows.Forms.OpenFileDialog();
      btnFindInPath = new System.Windows.Forms.Button();
      ffmpeg = new System.Windows.Forms.ComboBox();
      StatusBar = new System.Windows.Forms.StatusStrip();
      OutputStderr = new System.Windows.Forms.ToolStripStatusLabel();
      CropBox = new System.Windows.Forms.GroupBox();
      CropLabel4 = new System.Windows.Forms.Label();
      CropLabel3 = new System.Windows.Forms.Label();
      CropLabel2 = new System.Windows.Forms.Label();
      CropLabel1 = new System.Windows.Forms.Label();
      CropY = new System.Windows.Forms.NumericUpDown();
      CropX = new System.Windows.Forms.NumericUpDown();
      CropHeight = new System.Windows.Forms.NumericUpDown();
      CropWidth = new System.Windows.Forms.NumericUpDown();
      chkCrop = new System.Windows.Forms.CheckBox();
      FileListBindingSource = new System.Windows.Forms.BindingSource(components);
      DeInterlaceListBindingSource = new System.Windows.Forms.BindingSource(components);
      Tab = new System.Windows.Forms.TabControl();
      PageConvert = new System.Windows.Forms.TabPage();
      SubmitButtonBox = new System.Windows.Forms.Panel();
      groupBox6 = new System.Windows.Forms.GroupBox();
      FreeOptions = new System.Windows.Forms.TextBox();
      label11 = new System.Windows.Forms.Label();
      groupBox3 = new System.Windows.Forms.GroupBox();
      tabPage1 = new System.Windows.Forms.TabPage();
      CommonButtonBox = new System.Windows.Forms.Panel();
      CommandInvoker = new System.Windows.Forms.Button();
      btnStopUtil = new System.Windows.Forms.Button();
      btnStopAllUtil = new System.Windows.Forms.Button();
      button2 = new System.Windows.Forms.Button();
      groupBox8 = new System.Windows.Forms.GroupBox();
      label22 = new System.Windows.Forms.Label();
      label21 = new System.Windows.Forms.Label();
      SubmitCopy = new System.Windows.Forms.Button();
      SubmitConcat = new System.Windows.Forms.Button();
      Image2Box = new System.Windows.Forms.GroupBox();
      ImageWidth = new System.Windows.Forms.NumericUpDown();
      ImageHeight = new System.Windows.Forms.NumericUpDown();
      label18 = new System.Windows.Forms.Label();
      label20 = new System.Windows.Forms.Label();
      label17 = new System.Windows.Forms.Label();
      FrameRate = new System.Windows.Forms.NumericUpDown();
      ImageType = new System.Windows.Forms.ComboBox();
      linkLabel2 = new System.Windows.Forms.LinkLabel();
      ImageTo = new System.Windows.Forms.TextBox();
      ImageSS = new System.Windows.Forms.TextBox();
      label16 = new System.Windows.Forms.Label();
      label9 = new System.Windows.Forms.Label();
      label19 = new System.Windows.Forms.Label();
      label15 = new System.Windows.Forms.Label();
      label14 = new System.Windows.Forms.Label();
      SubmitThumbnail = new System.Windows.Forms.Button();
      groupBox5 = new System.Windows.Forms.GroupBox();
      FilePrefix = new System.Windows.Forms.ComboBox();
      FileSuffix = new System.Windows.Forms.ComboBox();
      settingsPropertyValueBindingSource = new System.Windows.Forms.BindingSource(components);
      groupBox1.SuspendLayout();
      groupBox2.SuspendLayout();
      ResizeBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)resizeTo).BeginInit();
      RotateBox.SuspendLayout();
      LayoutBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)VideoHeight).BeginInit();
      ((System.ComponentModel.ISupportInitialize)VideoWidth).BeginInit();
      BitrateBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)LookAhead).BeginInit();
      ((System.ComponentModel.ISupportInitialize)aBitrate).BeginInit();
      ((System.ComponentModel.ISupportInitialize)vBitrate).BeginInit();
      groupBox4.SuspendLayout();
      panel1.SuspendLayout();
      StatusBar.SuspendLayout();
      CropBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)CropY).BeginInit();
      ((System.ComponentModel.ISupportInitialize)CropX).BeginInit();
      ((System.ComponentModel.ISupportInitialize)CropHeight).BeginInit();
      ((System.ComponentModel.ISupportInitialize)CropWidth).BeginInit();
      ((System.ComponentModel.ISupportInitialize)FileListBindingSource).BeginInit();
      ((System.ComponentModel.ISupportInitialize)DeInterlaceListBindingSource).BeginInit();
      Tab.SuspendLayout();
      PageConvert.SuspendLayout();
      SubmitButtonBox.SuspendLayout();
      groupBox6.SuspendLayout();
      groupBox3.SuspendLayout();
      tabPage1.SuspendLayout();
      CommonButtonBox.SuspendLayout();
      groupBox8.SuspendLayout();
      Image2Box.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)ImageWidth).BeginInit();
      ((System.ComponentModel.ISupportInitialize)ImageHeight).BeginInit();
      ((System.ComponentModel.ISupportInitialize)FrameRate).BeginInit();
      groupBox5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)settingsPropertyValueBindingSource).BeginInit();
      SuspendLayout();
      // 
      // Commandlines
      // 
      Commandlines.BackColor = System.Drawing.SystemColors.Window;
      Commandlines.Location = new System.Drawing.Point(10, 10);
      Commandlines.Multiline = true;
      Commandlines.Name = "Commandlines";
      Commandlines.ReadOnly = true;
      Commandlines.Size = new System.Drawing.Size(690, 68);
      Commandlines.TabIndex = 0;
      // 
      // txtSS
      // 
      txtSS.Location = new System.Drawing.Point(13, 42);
      txtSS.Name = "txtSS";
      txtSS.Size = new System.Drawing.Size(62, 19);
      txtSS.TabIndex = 4;
      // 
      // FindFolder
      // 
      FindFolder.Description = "出力先フォルダを選択してください。";
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new System.Drawing.Point(10, 27);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(55, 12);
      label1.TabIndex = 2;
      label1.Text = "開始(-ss)";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new System.Drawing.Point(140, 27);
      label2.Name = "label2";
      label2.Size = new System.Drawing.Size(53, 12);
      label2.TabIndex = 3;
      label2.Text = "終了(-to)";
      // 
      // txtTo
      // 
      txtTo.Location = new System.Drawing.Point(142, 42);
      txtTo.Name = "txtTo";
      txtTo.Size = new System.Drawing.Size(63, 19);
      txtTo.TabIndex = 6;
      // 
      // groupBox1
      // 
      groupBox1.Controls.Add(linkLabel1);
      groupBox1.Controls.Add(btnClearSS);
      groupBox1.Controls.Add(btnClearTo);
      groupBox1.Controls.Add(label1);
      groupBox1.Controls.Add(txtTo);
      groupBox1.Controls.Add(txtSS);
      groupBox1.Controls.Add(label2);
      groupBox1.Location = new System.Drawing.Point(10, 85);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new System.Drawing.Size(347, 80);
      groupBox1.TabIndex = 5;
      groupBox1.TabStop = false;
      groupBox1.Text = "切り取り　　　　　　　　　　　　　　　";
      // 
      // linkLabel1
      // 
      linkLabel1.AutoSize = true;
      linkLabel1.Location = new System.Drawing.Point(64, 0);
      linkLabel1.Name = "linkLabel1";
      linkLabel1.Size = new System.Drawing.Size(111, 12);
      linkLabel1.TabIndex = 3;
      linkLabel1.TabStop = true;
      linkLabel1.Text = "時間指定構文の説明";
      linkLabel1.LinkClicked += linkLabel1_LinkClicked;
      // 
      // btnClearSS
      // 
      btnClearSS.Location = new System.Drawing.Point(79, 42);
      btnClearSS.Name = "btnClearSS";
      btnClearSS.Size = new System.Drawing.Size(46, 20);
      btnClearSS.TabIndex = 5;
      btnClearSS.TabStop = false;
      btnClearSS.Text = "クリア";
      btnClearSS.UseVisualStyleBackColor = true;
      btnClearSS.Click += btnClearSS_Click;
      // 
      // btnClearTo
      // 
      btnClearTo.Location = new System.Drawing.Point(208, 42);
      btnClearTo.Name = "btnClearTo";
      btnClearTo.Size = new System.Drawing.Size(46, 20);
      btnClearTo.TabIndex = 7;
      btnClearTo.TabStop = false;
      btnClearTo.Text = "クリア";
      btnClearTo.UseVisualStyleBackColor = true;
      btnClearTo.Click += btnClearTo_Click;
      // 
      // FindSaveBatchFile
      // 
      FindSaveBatchFile.CheckFileExists = false;
      FindSaveBatchFile.CheckPathExists = false;
      FindSaveBatchFile.DefaultExt = "cmd";
      FindSaveBatchFile.FileName = "ffmpeg-batch.cmd";
      // 
      // groupBox2
      // 
      groupBox2.Controls.Add(chkUseHWDecoder);
      groupBox2.Controls.Add(HWDecoder);
      groupBox2.Controls.Add(label5);
      groupBox2.Controls.Add(label4);
      groupBox2.Controls.Add(UseAudioEncoder);
      groupBox2.Controls.Add(UseVideoEncoder);
      groupBox2.Location = new System.Drawing.Point(681, 171);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new System.Drawing.Size(138, 129);
      groupBox2.TabIndex = 7;
      groupBox2.TabStop = false;
      groupBox2.Text = "コーデック";
      // 
      // chkUseHWDecoder
      // 
      chkUseHWDecoder.AutoSize = true;
      chkUseHWDecoder.Location = new System.Drawing.Point(12, 78);
      chkUseHWDecoder.Name = "chkUseHWDecoder";
      chkUseHWDecoder.Size = new System.Drawing.Size(112, 16);
      chkUseHWDecoder.TabIndex = 28;
      chkUseHWDecoder.Text = "HWデコーダー指定";
      chkUseHWDecoder.UseVisualStyleBackColor = true;
      chkUseHWDecoder.CheckedChanged += chkUseHWDecoder_CheckedChanged;
      // 
      // HWDecoder
      // 
      HWDecoder.DisplayMember = "Label";
      HWDecoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      HWDecoder.Enabled = false;
      HWDecoder.FormattingEnabled = true;
      HWDecoder.Location = new System.Drawing.Point(11, 97);
      HWDecoder.Name = "HWDecoder";
      HWDecoder.Size = new System.Drawing.Size(119, 20);
      HWDecoder.TabIndex = 29;
      HWDecoder.ValueMember = "Value";
      // 
      // label5
      // 
      label5.AutoSize = true;
      label5.Location = new System.Drawing.Point(12, 48);
      label5.Name = "label5";
      label5.Size = new System.Drawing.Size(25, 12);
      label5.TabIndex = 26;
      label5.Text = "-c:a";
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Location = new System.Drawing.Point(12, 23);
      label4.Name = "label4";
      label4.Size = new System.Drawing.Size(25, 12);
      label4.TabIndex = 26;
      label4.Text = "-c:v";
      // 
      // UseAudioEncoder
      // 
      UseAudioEncoder.DisplayMember = "Label";
      UseAudioEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      UseAudioEncoder.FormattingEnabled = true;
      UseAudioEncoder.Location = new System.Drawing.Point(43, 44);
      UseAudioEncoder.Name = "UseAudioEncoder";
      UseAudioEncoder.Size = new System.Drawing.Size(86, 20);
      UseAudioEncoder.TabIndex = 25;
      UseAudioEncoder.ValueMember = "Value";
      // 
      // UseVideoEncoder
      // 
      UseVideoEncoder.BackColor = System.Drawing.SystemColors.Window;
      UseVideoEncoder.DisplayMember = "Label";
      UseVideoEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      UseVideoEncoder.FlatStyle = System.Windows.Forms.FlatStyle.System;
      UseVideoEncoder.FormattingEnabled = true;
      UseVideoEncoder.Location = new System.Drawing.Point(43, 18);
      UseVideoEncoder.Name = "UseVideoEncoder";
      UseVideoEncoder.Size = new System.Drawing.Size(86, 20);
      UseVideoEncoder.TabIndex = 24;
      UseVideoEncoder.ValueMember = "Value";
      UseVideoEncoder.SelectedIndexChanged += UseVideoEncoder_SelectedIndexChanged;
      // 
      // cbDeinterlaceAlg
      // 
      cbDeinterlaceAlg.DisplayMember = "Label";
      cbDeinterlaceAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cbDeinterlaceAlg.Enabled = false;
      cbDeinterlaceAlg.FormattingEnabled = true;
      cbDeinterlaceAlg.Location = new System.Drawing.Point(87, 26);
      cbDeinterlaceAlg.Name = "cbDeinterlaceAlg";
      cbDeinterlaceAlg.Size = new System.Drawing.Size(131, 20);
      cbDeinterlaceAlg.TabIndex = 27;
      cbDeinterlaceAlg.ValueMember = "Value";
      // 
      // chkFilterDeInterlace
      // 
      chkFilterDeInterlace.AutoSize = true;
      chkFilterDeInterlace.Location = new System.Drawing.Point(14, 28);
      chkFilterDeInterlace.Name = "chkFilterDeInterlace";
      chkFilterDeInterlace.Size = new System.Drawing.Size(67, 16);
      chkFilterDeInterlace.TabIndex = 26;
      chkFilterDeInterlace.Text = "処理する";
      chkFilterDeInterlace.CheckedChanged += chkFilterDeInterlace_CheckedChanged;
      // 
      // chkAudioOnly
      // 
      chkAudioOnly.AutoSize = true;
      chkAudioOnly.Location = new System.Drawing.Point(15, 21);
      chkAudioOnly.Name = "chkAudioOnly";
      chkAudioOnly.Size = new System.Drawing.Size(93, 16);
      chkAudioOnly.TabIndex = 23;
      chkAudioOnly.Text = "音声のみ出力";
      chkAudioOnly.UseVisualStyleBackColor = true;
      chkAudioOnly.CheckedChanged += chkAudioOnly_CheckedChanged;
      // 
      // ResizeBox
      // 
      ResizeBox.Controls.Add(rbResizeNum);
      ResizeBox.Controls.Add(resizeTo);
      ResizeBox.Controls.Add(rbResizeHD);
      ResizeBox.Controls.Add(rbResizeFullHD);
      ResizeBox.Controls.Add(rbResizeNone);
      ResizeBox.Location = new System.Drawing.Point(364, 171);
      ResizeBox.Name = "ResizeBox";
      ResizeBox.Size = new System.Drawing.Size(311, 62);
      ResizeBox.TabIndex = 8;
      ResizeBox.TabStop = false;
      ResizeBox.Text = "リサイズ：短辺指定";
      // 
      // rbResizeNum
      // 
      rbResizeNum.AutoSize = true;
      rbResizeNum.Location = new System.Drawing.Point(180, 29);
      rbResizeNum.Name = "rbResizeNum";
      rbResizeNum.Size = new System.Drawing.Size(59, 16);
      rbResizeNum.TabIndex = 15;
      rbResizeNum.Tag = "-1";
      rbResizeNum.Text = "指定値";
      rbResizeNum.UseVisualStyleBackColor = true;
      rbResizeNum.CheckedChanged += rbResizeNum_CheckedChanged;
      // 
      // resizeTo
      // 
      resizeTo.Enabled = false;
      resizeTo.Increment = new decimal(new int[] { 8, 0, 0, 0 });
      resizeTo.Location = new System.Drawing.Point(245, 27);
      resizeTo.Maximum = new decimal(new int[] { 4320, 0, 0, 0 });
      resizeTo.Minimum = new decimal(new int[] { 320, 0, 0, 0 });
      resizeTo.Name = "resizeTo";
      resizeTo.Size = new System.Drawing.Size(54, 19);
      resizeTo.TabIndex = 14;
      resizeTo.TabStop = false;
      resizeTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      resizeTo.Value = new decimal(new int[] { 320, 0, 0, 0 });
      // 
      // rbResizeHD
      // 
      rbResizeHD.AutoSize = true;
      rbResizeHD.Location = new System.Drawing.Point(123, 29);
      rbResizeHD.Name = "rbResizeHD";
      rbResizeHD.Size = new System.Drawing.Size(53, 16);
      rbResizeHD.TabIndex = 13;
      rbResizeHD.Tag = "720";
      rbResizeHD.Text = "720px";
      // 
      // rbResizeFullHD
      // 
      rbResizeFullHD.AutoSize = true;
      rbResizeFullHD.Location = new System.Drawing.Point(61, 29);
      rbResizeFullHD.Name = "rbResizeFullHD";
      rbResizeFullHD.Size = new System.Drawing.Size(59, 16);
      rbResizeFullHD.TabIndex = 12;
      rbResizeFullHD.Tag = "1080";
      rbResizeFullHD.Text = "1080px";
      // 
      // rbResizeNone
      // 
      rbResizeNone.AutoSize = true;
      rbResizeNone.Checked = true;
      rbResizeNone.Location = new System.Drawing.Point(14, 29);
      rbResizeNone.Name = "rbResizeNone";
      rbResizeNone.Size = new System.Drawing.Size(42, 16);
      rbResizeNone.TabIndex = 11;
      rbResizeNone.TabStop = true;
      rbResizeNone.Tag = "0";
      rbResizeNone.Text = "なし";
      // 
      // RotateBox
      // 
      RotateBox.Controls.Add(rbRotateNone);
      RotateBox.Controls.Add(rbRotateLeft);
      RotateBox.Controls.Add(rbRotateRight);
      RotateBox.Location = new System.Drawing.Point(364, 242);
      RotateBox.Name = "RotateBox";
      RotateBox.Size = new System.Drawing.Size(311, 58);
      RotateBox.TabIndex = 9;
      RotateBox.TabStop = false;
      RotateBox.Text = "回転：90°";
      // 
      // rbRotateNone
      // 
      rbRotateNone.AutoSize = true;
      rbRotateNone.Checked = true;
      rbRotateNone.Location = new System.Drawing.Point(14, 26);
      rbRotateNone.Name = "rbRotateNone";
      rbRotateNone.Size = new System.Drawing.Size(42, 16);
      rbRotateNone.TabIndex = 16;
      rbRotateNone.TabStop = true;
      rbRotateNone.Tag = "0";
      rbRotateNone.Text = "なし";
      // 
      // rbRotateLeft
      // 
      rbRotateLeft.AutoSize = true;
      rbRotateLeft.Location = new System.Drawing.Point(150, 26);
      rbRotateLeft.Name = "rbRotateLeft";
      rbRotateLeft.Size = new System.Drawing.Size(79, 16);
      rbRotateLeft.TabIndex = 18;
      rbRotateLeft.Tag = "2";
      rbRotateLeft.Text = "半時計周り";
      // 
      // rbRotateRight
      // 
      rbRotateRight.AutoSize = true;
      rbRotateRight.Location = new System.Drawing.Point(72, 26);
      rbRotateRight.Name = "rbRotateRight";
      rbRotateRight.Size = new System.Drawing.Size(67, 16);
      rbRotateRight.TabIndex = 17;
      rbRotateRight.Tag = "1";
      rbRotateRight.Text = "時計周り";
      // 
      // cbOutputDir
      // 
      cbOutputDir.Location = new System.Drawing.Point(11, 30);
      cbOutputDir.Name = "cbOutputDir";
      cbOutputDir.Size = new System.Drawing.Size(288, 20);
      cbOutputDir.Sorted = true;
      cbOutputDir.TabIndex = 16;
      // 
      // FileContainer
      // 
      FileContainer.DisplayMember = "Label";
      FileContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      FileContainer.FormattingEnabled = true;
      FileContainer.Location = new System.Drawing.Point(330, 85);
      FileContainer.Name = "FileContainer";
      FileContainer.Size = new System.Drawing.Size(64, 20);
      FileContainer.TabIndex = 31;
      FileContainer.ValueMember = "Value";
      // 
      // label13
      // 
      label13.AutoSize = true;
      label13.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold);
      label13.Location = new System.Drawing.Point(319, 89);
      label13.Name = "label13";
      label13.Size = new System.Drawing.Size(8, 12);
      label13.TabIndex = 32;
      label13.Text = ".";
      // 
      // label10
      // 
      label10.AutoSize = true;
      label10.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      label10.Location = new System.Drawing.Point(13, 71);
      label10.Name = "label10";
      label10.Size = new System.Drawing.Size(372, 11);
      label10.TabIndex = 30;
      label10.Text = "※複数の入力ファイルがあり、元ファイル名以外を指定すると、連番号が付加されます。";
      // 
      // FileName
      // 
      FileName.FormattingEnabled = true;
      FileName.ItemHeight = 12;
      FileName.Items.AddRange(new object[] { "元ファイル名" });
      FileName.Location = new System.Drawing.Point(73, 85);
      FileName.Name = "FileName";
      FileName.Size = new System.Drawing.Size(182, 20);
      FileName.TabIndex = 29;
      // 
      // OpenLogFile
      // 
      OpenLogFile.Location = new System.Drawing.Point(756, 12);
      OpenLogFile.Name = "OpenLogFile";
      OpenLogFile.Size = new System.Drawing.Size(67, 24);
      OpenLogFile.TabIndex = 24;
      OpenLogFile.TabStop = false;
      OpenLogFile.Text = "ログ表示";
      OpenLogFile.UseVisualStyleBackColor = true;
      OpenLogFile.Click += OpenLogFile_Click;
      // 
      // OpenFolder
      // 
      OpenFolder.Location = new System.Drawing.Point(350, 27);
      OpenFolder.Name = "OpenFolder";
      OpenFolder.Size = new System.Drawing.Size(43, 24);
      OpenFolder.TabIndex = 10;
      OpenFolder.TabStop = false;
      OpenFolder.Text = "開く";
      OpenFolder.UseVisualStyleBackColor = true;
      OpenFolder.Click += OpenFolder_Click;
      // 
      // btnSubmitOpenDlg
      // 
      btnSubmitOpenDlg.Location = new System.Drawing.Point(305, 28);
      btnSubmitOpenDlg.Name = "btnSubmitOpenDlg";
      btnSubmitOpenDlg.Size = new System.Drawing.Size(43, 23);
      btnSubmitOpenDlg.TabIndex = 9;
      btnSubmitOpenDlg.TabStop = false;
      btnSubmitOpenDlg.Text = "参照";
      btnSubmitOpenDlg.UseVisualStyleBackColor = true;
      btnSubmitOpenDlg.Click += btnSubmitOpenDlg_Click;
      // 
      // btnSubmitSaveToFile
      // 
      btnSubmitSaveToFile.Location = new System.Drawing.Point(414, 12);
      btnSubmitSaveToFile.Name = "btnSubmitSaveToFile";
      btnSubmitSaveToFile.Size = new System.Drawing.Size(144, 24);
      btnSubmitSaveToFile.TabIndex = 23;
      btnSubmitSaveToFile.TabStop = false;
      btnSubmitSaveToFile.Text = "バッチファイルとして保存";
      btnSubmitSaveToFile.UseVisualStyleBackColor = true;
      btnSubmitSaveToFile.Click += btnSubmitSaveToFile_Click;
      // 
      // btnClearDirs
      // 
      btnClearDirs.Location = new System.Drawing.Point(334, 12);
      btnClearDirs.Name = "btnClearDirs";
      btnClearDirs.Size = new System.Drawing.Size(74, 24);
      btnClearDirs.TabIndex = 8;
      btnClearDirs.TabStop = false;
      btnClearDirs.Text = "履歴削除";
      btnClearDirs.UseVisualStyleBackColor = true;
      btnClearDirs.Click += btnClearDirs_Click;
      // 
      // btnSubmitInvoke
      // 
      btnSubmitInvoke.Enabled = false;
      btnSubmitInvoke.Location = new System.Drawing.Point(564, 12);
      btnSubmitInvoke.Name = "btnSubmitInvoke";
      btnSubmitInvoke.Size = new System.Drawing.Size(54, 24);
      btnSubmitInvoke.TabIndex = 1;
      btnSubmitInvoke.TabStop = false;
      btnSubmitInvoke.Text = "実行";
      btnSubmitInvoke.UseVisualStyleBackColor = true;
      btnSubmitInvoke.Click += btnSubmitInvoke_Click;
      // 
      // LayoutBox
      // 
      LayoutBox.Controls.Add(label7);
      LayoutBox.Controls.Add(VideoHeight);
      LayoutBox.Controls.Add(VideoWidth);
      LayoutBox.Controls.Add(rbPortrait);
      LayoutBox.Controls.Add(rbLandscape);
      LayoutBox.Location = new System.Drawing.Point(10, 253);
      LayoutBox.Name = "LayoutBox";
      LayoutBox.Size = new System.Drawing.Size(347, 62);
      LayoutBox.TabIndex = 12;
      LayoutBox.TabStop = false;
      LayoutBox.Text = "元動画の情報(リサイズとクロップ時のヒント情報)";
      // 
      // label7
      // 
      label7.AutoSize = true;
      label7.Location = new System.Drawing.Point(227, 32);
      label7.Name = "label7";
      label7.Size = new System.Drawing.Size(17, 12);
      label7.TabIndex = 17;
      label7.Text = "×";
      // 
      // VideoHeight
      // 
      VideoHeight.Location = new System.Drawing.Point(244, 28);
      VideoHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      VideoHeight.Name = "VideoHeight";
      VideoHeight.Size = new System.Drawing.Size(57, 19);
      VideoHeight.TabIndex = 16;
      VideoHeight.TabStop = false;
      VideoHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      VideoHeight.Value = new decimal(new int[] { 2160, 0, 0, 0 });
      // 
      // VideoWidth
      // 
      VideoWidth.Location = new System.Drawing.Point(168, 28);
      VideoWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      VideoWidth.Name = "VideoWidth";
      VideoWidth.Size = new System.Drawing.Size(57, 19);
      VideoWidth.TabIndex = 16;
      VideoWidth.TabStop = false;
      VideoWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      VideoWidth.Value = new decimal(new int[] { 3840, 0, 0, 0 });
      // 
      // rbPortrait
      // 
      rbPortrait.AutoSize = true;
      rbPortrait.Location = new System.Drawing.Point(91, 29);
      rbPortrait.Name = "rbPortrait";
      rbPortrait.Size = new System.Drawing.Size(61, 16);
      rbPortrait.TabIndex = 15;
      rbPortrait.Text = "Portrait";
      rbPortrait.UseVisualStyleBackColor = true;
      // 
      // rbLandscape
      // 
      rbLandscape.AutoSize = true;
      rbLandscape.Checked = true;
      rbLandscape.Location = new System.Drawing.Point(14, 28);
      rbLandscape.Name = "rbLandscape";
      rbLandscape.Size = new System.Drawing.Size(74, 16);
      rbLandscape.TabIndex = 14;
      rbLandscape.TabStop = true;
      rbLandscape.Text = "landscape";
      rbLandscape.UseVisualStyleBackColor = true;
      // 
      // btnClear
      // 
      btnClear.Location = new System.Drawing.Point(706, 55);
      btnClear.Name = "btnClear";
      btnClear.Size = new System.Drawing.Size(113, 23);
      btnClear.TabIndex = 2;
      btnClear.TabStop = false;
      btnClear.Text = "クリア";
      btnClear.UseVisualStyleBackColor = true;
      btnClear.Click += btnClear_Click;
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new System.Drawing.Point(0, 13);
      label3.Name = "label3";
      label3.Size = new System.Drawing.Size(344, 12);
      label3.TabIndex = 0;
      label3.Text = "動画ファイルをドラッグ＆ドロップもしくはダブルクリックして選択してください。";
      label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      label3.MouseDoubleClick += DropArea_MouseDoubleClick;
      // 
      // BitrateBox
      // 
      BitrateBox.Controls.Add(label12);
      BitrateBox.Controls.Add(LookAhead);
      BitrateBox.Controls.Add(aUnit);
      BitrateBox.Controls.Add(aBitrate);
      BitrateBox.Controls.Add(aQualityLabel);
      BitrateBox.Controls.Add(vQualityLabel);
      BitrateBox.Controls.Add(label6);
      BitrateBox.Controls.Add(cbPreset);
      BitrateBox.Controls.Add(vBitrate);
      BitrateBox.Controls.Add(chkEncodeAudio);
      BitrateBox.Controls.Add(chkConstantQuality);
      BitrateBox.Controls.Add(vUnit);
      BitrateBox.Location = new System.Drawing.Point(364, 85);
      BitrateBox.Name = "BitrateBox";
      BitrateBox.Size = new System.Drawing.Size(456, 80);
      BitrateBox.TabIndex = 15;
      BitrateBox.TabStop = false;
      BitrateBox.Text = "出力品質";
      // 
      // label12
      // 
      label12.AutoSize = true;
      label12.Location = new System.Drawing.Point(292, 53);
      label12.Name = "label12";
      label12.Size = new System.Drawing.Size(102, 12);
      label12.TabIndex = 28;
      label12.Text = "先行探索フレーム数";
      // 
      // LookAhead
      // 
      LookAhead.Location = new System.Drawing.Point(395, 49);
      LookAhead.Name = "LookAhead";
      LookAhead.Size = new System.Drawing.Size(51, 19);
      LookAhead.TabIndex = 22;
      LookAhead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      LookAhead.Value = new decimal(new int[] { 15, 0, 0, 0 });
      // 
      // aUnit
      // 
      aUnit.AutoSize = true;
      aUnit.Location = new System.Drawing.Point(219, 50);
      aUnit.Name = "aUnit";
      aUnit.Size = new System.Drawing.Size(30, 12);
      aUnit.TabIndex = 26;
      aUnit.Text = "Kbps";
      // 
      // aBitrate
      // 
      aBitrate.Increment = new decimal(new int[] { 16, 0, 0, 0 });
      aBitrate.Location = new System.Drawing.Point(148, 46);
      aBitrate.Maximum = new decimal(new int[] { 320, 0, 0, 0 });
      aBitrate.Name = "aBitrate";
      aBitrate.Size = new System.Drawing.Size(70, 19);
      aBitrate.TabIndex = 20;
      aBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      aBitrate.Value = new decimal(new int[] { 192, 0, 0, 0 });
      // 
      // aQualityLabel
      // 
      aQualityLabel.AutoSize = true;
      aQualityLabel.Location = new System.Drawing.Point(119, 48);
      aQualityLabel.Name = "aQualityLabel";
      aQualityLabel.Size = new System.Drawing.Size(25, 12);
      aQualityLabel.TabIndex = 24;
      aQualityLabel.Text = "-b:a";
      // 
      // vQualityLabel
      // 
      vQualityLabel.Location = new System.Drawing.Point(94, 26);
      vQualityLabel.Name = "vQualityLabel";
      vQualityLabel.Size = new System.Drawing.Size(50, 12);
      vQualityLabel.TabIndex = 23;
      vQualityLabel.Text = "-b:v";
      vQualityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label6
      // 
      label6.AutoSize = true;
      label6.Location = new System.Drawing.Point(289, 27);
      label6.Name = "label6";
      label6.Size = new System.Drawing.Size(46, 12);
      label6.TabIndex = 22;
      label6.Text = "プリセット";
      // 
      // cbPreset
      // 
      cbPreset.DisplayMember = "Label";
      cbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cbPreset.FormattingEnabled = true;
      cbPreset.Location = new System.Drawing.Point(335, 23);
      cbPreset.Name = "cbPreset";
      cbPreset.Size = new System.Drawing.Size(111, 20);
      cbPreset.TabIndex = 21;
      cbPreset.ValueMember = "Value";
      // 
      // vBitrate
      // 
      vBitrate.Location = new System.Drawing.Point(148, 22);
      vBitrate.Maximum = new decimal(new int[] { 1215752191, 23, 0, 0 });
      vBitrate.Name = "vBitrate";
      vBitrate.Size = new System.Drawing.Size(70, 19);
      vBitrate.TabIndex = 18;
      vBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      vBitrate.Value = new decimal(new int[] { 6000, 0, 0, 0 });
      // 
      // chkEncodeAudio
      // 
      chkEncodeAudio.AutoSize = true;
      chkEncodeAudio.Location = new System.Drawing.Point(14, 47);
      chkEncodeAudio.Name = "chkEncodeAudio";
      chkEncodeAudio.Size = new System.Drawing.Size(93, 16);
      chkEncodeAudio.TabIndex = 19;
      chkEncodeAudio.Text = "音声エンコード";
      chkEncodeAudio.UseVisualStyleBackColor = true;
      chkEncodeAudio.CheckedChanged += chkEncodeAudio_CheckedChanged;
      // 
      // chkConstantQuality
      // 
      chkConstantQuality.AutoSize = true;
      chkConstantQuality.Location = new System.Drawing.Point(14, 24);
      chkConstantQuality.Name = "chkConstantQuality";
      chkConstantQuality.Size = new System.Drawing.Size(72, 16);
      chkConstantQuality.TabIndex = 17;
      chkConstantQuality.TabStop = false;
      chkConstantQuality.Text = "固定品質";
      chkConstantQuality.CheckedChanged += chkConstantQuality_CheckedChanged;
      // 
      // vUnit
      // 
      vUnit.AutoSize = true;
      vUnit.Location = new System.Drawing.Point(219, 26);
      vUnit.Name = "vUnit";
      vUnit.Size = new System.Drawing.Size(30, 12);
      vUnit.TabIndex = 1;
      vUnit.Text = "Kbps";
      // 
      // btnApply
      // 
      btnApply.Location = new System.Drawing.Point(706, 10);
      btnApply.Name = "btnApply";
      btnApply.Size = new System.Drawing.Size(113, 39);
      btnApply.TabIndex = 1;
      btnApply.TabStop = false;
      btnApply.Text = "実行コマンド確認\r\n（表示更新）";
      btnApply.UseVisualStyleBackColor = true;
      btnApply.Click += btnApply_Click;
      // 
      // OpenInputFile
      // 
      OpenInputFile.DefaultExt = "mp4";
      OpenInputFile.Filter = "動画ファイル|*.mpg;*.mp4;*.mkv;*.ts;*.avi;*.webm;*.m4v;*.wmv|すべてのファイル|*.*";
      OpenInputFile.Multiselect = true;
      OpenInputFile.Title = "動画ファイルを選択してください。";
      // 
      // FileList
      // 
      FileList.AllowDrop = true;
      FileList.DisplayMember = "Label";
      FileList.Dock = System.Windows.Forms.DockStyle.Bottom;
      FileList.FormattingEnabled = true;
      FileList.HorizontalScrollbar = true;
      FileList.ItemHeight = 12;
      FileList.Location = new System.Drawing.Point(0, 42);
      FileList.Name = "FileList";
      FileList.Size = new System.Drawing.Size(419, 88);
      FileList.Sorted = true;
      FileList.TabIndex = 29;
      FileList.ValueMember = "Value";
      FileList.DragDrop += DropArea_DragDrop;
      FileList.DragEnter += DropArea_DragEnter;
      FileList.MouseDoubleClick += DropArea_MouseDoubleClick;
      // 
      // btnStop
      // 
      btnStop.Enabled = false;
      btnStop.Location = new System.Drawing.Point(624, 12);
      btnStop.Name = "btnStop";
      btnStop.Size = new System.Drawing.Size(56, 24);
      btnStop.TabIndex = 31;
      btnStop.TabStop = false;
      btnStop.Text = "中止";
      btnStop.UseVisualStyleBackColor = true;
      btnStop.Click += btnStop_Click;
      // 
      // btnStopAll
      // 
      btnStopAll.Enabled = false;
      btnStopAll.Location = new System.Drawing.Point(686, 12);
      btnStopAll.Name = "btnStopAll";
      btnStopAll.Size = new System.Drawing.Size(64, 24);
      btnStopAll.TabIndex = 32;
      btnStopAll.TabStop = false;
      btnStopAll.Text = "全て中止";
      btnStopAll.UseVisualStyleBackColor = true;
      btnStopAll.Click += btnStopAll_Click;
      // 
      // groupBox4
      // 
      groupBox4.Controls.Add(cbDeinterlaceAlg);
      groupBox4.Controls.Add(chkFilterDeInterlace);
      groupBox4.Location = new System.Drawing.Point(364, 306);
      groupBox4.Name = "groupBox4";
      groupBox4.Size = new System.Drawing.Size(227, 60);
      groupBox4.TabIndex = 33;
      groupBox4.TabStop = false;
      groupBox4.Text = "デインターレース";
      // 
      // cbDevices
      // 
      cbDevices.DisplayMember = "Label";
      cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cbDevices.FormattingEnabled = true;
      cbDevices.Location = new System.Drawing.Point(131, 18);
      cbDevices.Name = "cbDevices";
      cbDevices.Size = new System.Drawing.Size(203, 20);
      cbDevices.TabIndex = 0;
      cbDevices.TabStop = false;
      cbDevices.ValueMember = "Value";
      cbDevices.SelectedIndexChanged += cbDevices_SelectedIndexChanged;
      // 
      // panel1
      // 
      panel1.Controls.Add(label3);
      panel1.Controls.Add(FileList);
      panel1.Controls.Add(ClearFileList);
      panel1.Location = new System.Drawing.Point(8, 485);
      panel1.Name = "panel1";
      panel1.Size = new System.Drawing.Size(419, 130);
      panel1.TabIndex = 34;
      // 
      // ClearFileList
      // 
      ClearFileList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      ClearFileList.Location = new System.Drawing.Point(344, 7);
      ClearFileList.Name = "ClearFileList";
      ClearFileList.Size = new System.Drawing.Size(75, 24);
      ClearFileList.TabIndex = 34;
      ClearFileList.TabStop = false;
      ClearFileList.Text = "クリア";
      ClearFileList.UseVisualStyleBackColor = true;
      ClearFileList.Click += ClearFileList_Click;
      // 
      // OpenDecoderHelp
      // 
      OpenDecoderHelp.Enabled = false;
      OpenDecoderHelp.Location = new System.Drawing.Point(174, 12);
      OpenDecoderHelp.Name = "OpenDecoderHelp";
      OpenDecoderHelp.Size = new System.Drawing.Size(154, 24);
      OpenDecoderHelp.TabIndex = 36;
      OpenDecoderHelp.Text = "デコーダーオプションのヘルプ";
      OpenDecoderHelp.UseVisualStyleBackColor = true;
      OpenDecoderHelp.Click += OpenDecoderHelp_Click;
      // 
      // OpenEncoderHelp
      // 
      OpenEncoderHelp.Location = new System.Drawing.Point(14, 12);
      OpenEncoderHelp.Name = "OpenEncoderHelp";
      OpenEncoderHelp.Size = new System.Drawing.Size(154, 24);
      OpenEncoderHelp.TabIndex = 35;
      OpenEncoderHelp.Text = "エンコーダーオプションのヘルプ";
      OpenEncoderHelp.UseVisualStyleBackColor = true;
      OpenEncoderHelp.Click += OpenEncoderHelp_Click;
      // 
      // btnFFmpeg
      // 
      btnFFmpeg.Location = new System.Drawing.Point(640, 9);
      btnFFmpeg.Name = "btnFFmpeg";
      btnFFmpeg.Size = new System.Drawing.Size(49, 23);
      btnFFmpeg.TabIndex = 36;
      btnFFmpeg.Text = "参照";
      btnFFmpeg.UseVisualStyleBackColor = true;
      btnFFmpeg.Click += btnFFmpeg_Click;
      // 
      // label8
      // 
      label8.AutoSize = true;
      label8.Location = new System.Drawing.Point(11, 15);
      label8.Name = "label8";
      label8.Size = new System.Drawing.Size(98, 12);
      label8.TabIndex = 37;
      label8.Text = "ffmpeg実行ファイル";
      // 
      // OpenFFMpegFileDlg
      // 
      OpenFFMpegFileDlg.DefaultExt = "exe";
      OpenFFMpegFileDlg.FileName = "ffmpeg";
      OpenFFMpegFileDlg.Filter = "実行ファイル|*.exe";
      OpenFFMpegFileDlg.Title = "ffmpeg実行ファイルを指定してください。";
      // 
      // btnFindInPath
      // 
      btnFindInPath.Location = new System.Drawing.Point(695, 9);
      btnFindInPath.Name = "btnFindInPath";
      btnFindInPath.Size = new System.Drawing.Size(144, 23);
      btnFindInPath.TabIndex = 38;
      btnFindInPath.Text = "環境変数PATHから探す";
      btnFindInPath.UseVisualStyleBackColor = true;
      btnFindInPath.Click += btnFindInPath_Click;
      // 
      // ffmpeg
      // 
      ffmpeg.FormattingEnabled = true;
      ffmpeg.Location = new System.Drawing.Point(112, 11);
      ffmpeg.Name = "ffmpeg";
      ffmpeg.Size = new System.Drawing.Size(522, 20);
      ffmpeg.TabIndex = 39;
      // 
      // StatusBar
      // 
      StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { OutputStderr });
      StatusBar.Location = new System.Drawing.Point(3, 625);
      StatusBar.Name = "StatusBar";
      StatusBar.Size = new System.Drawing.Size(840, 23);
      StatusBar.SizingGrip = false;
      StatusBar.TabIndex = 40;
      StatusBar.Text = "statusStrip1";
      // 
      // OutputStderr
      // 
      OutputStderr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      OutputStderr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      OutputStderr.Name = "OutputStderr";
      OutputStderr.Size = new System.Drawing.Size(825, 18);
      OutputStderr.Spring = true;
      OutputStderr.Text = "stderr";
      OutputStderr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // CropBox
      // 
      CropBox.Controls.Add(CropLabel4);
      CropBox.Controls.Add(CropLabel3);
      CropBox.Controls.Add(CropLabel2);
      CropBox.Controls.Add(CropLabel1);
      CropBox.Controls.Add(CropY);
      CropBox.Controls.Add(CropX);
      CropBox.Controls.Add(CropHeight);
      CropBox.Controls.Add(CropWidth);
      CropBox.Controls.Add(chkCrop);
      CropBox.Location = new System.Drawing.Point(10, 171);
      CropBox.Name = "CropBox";
      CropBox.Size = new System.Drawing.Size(347, 76);
      CropBox.TabIndex = 41;
      CropBox.TabStop = false;
      CropBox.Text = "クロップ";
      // 
      // CropLabel4
      // 
      CropLabel4.AutoSize = true;
      CropLabel4.Location = new System.Drawing.Point(243, 48);
      CropLabel4.Name = "CropLabel4";
      CropLabel4.Size = new System.Drawing.Size(12, 12);
      CropLabel4.TabIndex = 2;
      CropLabel4.Text = "Y";
      // 
      // CropLabel3
      // 
      CropLabel3.AutoSize = true;
      CropLabel3.Location = new System.Drawing.Point(165, 48);
      CropLabel3.Name = "CropLabel3";
      CropLabel3.Size = new System.Drawing.Size(12, 12);
      CropLabel3.TabIndex = 2;
      CropLabel3.Text = "X";
      // 
      // CropLabel2
      // 
      CropLabel2.AutoSize = true;
      CropLabel2.Location = new System.Drawing.Point(89, 48);
      CropLabel2.Name = "CropLabel2";
      CropLabel2.Size = new System.Drawing.Size(13, 12);
      CropLabel2.TabIndex = 2;
      CropLabel2.Text = "H";
      // 
      // CropLabel1
      // 
      CropLabel1.AutoSize = true;
      CropLabel1.Location = new System.Drawing.Point(13, 48);
      CropLabel1.Name = "CropLabel1";
      CropLabel1.Size = new System.Drawing.Size(14, 12);
      CropLabel1.TabIndex = 2;
      CropLabel1.Text = "W";
      // 
      // CropY
      // 
      CropY.Location = new System.Drawing.Point(257, 45);
      CropY.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
      CropY.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
      CropY.Name = "CropY";
      CropY.Size = new System.Drawing.Size(54, 19);
      CropY.TabIndex = 12;
      CropY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      CropY.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
      // 
      // CropX
      // 
      CropX.Location = new System.Drawing.Point(179, 45);
      CropX.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
      CropX.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
      CropX.Name = "CropX";
      CropX.Size = new System.Drawing.Size(54, 19);
      CropX.TabIndex = 11;
      CropX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      CropX.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
      // 
      // CropHeight
      // 
      CropHeight.Location = new System.Drawing.Point(104, 45);
      CropHeight.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
      CropHeight.Name = "CropHeight";
      CropHeight.Size = new System.Drawing.Size(54, 19);
      CropHeight.TabIndex = 10;
      CropHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // CropWidth
      // 
      CropWidth.Location = new System.Drawing.Point(28, 45);
      CropWidth.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
      CropWidth.Name = "CropWidth";
      CropWidth.Size = new System.Drawing.Size(54, 19);
      CropWidth.TabIndex = 9;
      CropWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // chkCrop
      // 
      chkCrop.AutoSize = true;
      chkCrop.Location = new System.Drawing.Point(16, 22);
      chkCrop.Name = "chkCrop";
      chkCrop.Size = new System.Drawing.Size(67, 16);
      chkCrop.TabIndex = 8;
      chkCrop.Text = "処理する";
      chkCrop.UseVisualStyleBackColor = true;
      chkCrop.CheckedChanged += chkCrop_CheckedChanged;
      // 
      // Tab
      // 
      Tab.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      Tab.Controls.Add(PageConvert);
      Tab.Controls.Add(tabPage1);
      Tab.Location = new System.Drawing.Point(3, 40);
      Tab.Margin = new System.Windows.Forms.Padding(0);
      Tab.Name = "Tab";
      Tab.Padding = new System.Drawing.Point(0, 0);
      Tab.SelectedIndex = 0;
      Tab.Size = new System.Drawing.Size(840, 444);
      Tab.TabIndex = 42;
      // 
      // PageConvert
      // 
      PageConvert.BackColor = System.Drawing.SystemColors.ButtonFace;
      PageConvert.Controls.Add(SubmitButtonBox);
      PageConvert.Controls.Add(groupBox6);
      PageConvert.Controls.Add(groupBox3);
      PageConvert.Controls.Add(groupBox1);
      PageConvert.Controls.Add(CropBox);
      PageConvert.Controls.Add(BitrateBox);
      PageConvert.Controls.Add(ResizeBox);
      PageConvert.Controls.Add(groupBox2);
      PageConvert.Controls.Add(RotateBox);
      PageConvert.Controls.Add(btnClear);
      PageConvert.Controls.Add(groupBox4);
      PageConvert.Controls.Add(btnApply);
      PageConvert.Controls.Add(Commandlines);
      PageConvert.Controls.Add(LayoutBox);
      PageConvert.Location = new System.Drawing.Point(4, 22);
      PageConvert.Margin = new System.Windows.Forms.Padding(0);
      PageConvert.Name = "PageConvert";
      PageConvert.Size = new System.Drawing.Size(832, 418);
      PageConvert.TabIndex = 0;
      PageConvert.Text = "動画変換";
      // 
      // SubmitButtonBox
      // 
      SubmitButtonBox.Controls.Add(OpenEncoderHelp);
      SubmitButtonBox.Controls.Add(btnSubmitSaveToFile);
      SubmitButtonBox.Controls.Add(btnClearDirs);
      SubmitButtonBox.Controls.Add(OpenDecoderHelp);
      SubmitButtonBox.Controls.Add(btnSubmitInvoke);
      SubmitButtonBox.Controls.Add(btnStop);
      SubmitButtonBox.Controls.Add(OpenLogFile);
      SubmitButtonBox.Controls.Add(btnStopAll);
      SubmitButtonBox.Dock = System.Windows.Forms.DockStyle.Bottom;
      SubmitButtonBox.Location = new System.Drawing.Point(0, 372);
      SubmitButtonBox.Name = "SubmitButtonBox";
      SubmitButtonBox.Size = new System.Drawing.Size(832, 46);
      SubmitButtonBox.TabIndex = 44;
      // 
      // groupBox6
      // 
      groupBox6.Controls.Add(FreeOptions);
      groupBox6.Controls.Add(label11);
      groupBox6.Location = new System.Drawing.Point(603, 306);
      groupBox6.Name = "groupBox6";
      groupBox6.Size = new System.Drawing.Size(216, 60);
      groupBox6.TabIndex = 43;
      groupBox6.TabStop = false;
      groupBox6.Text = "追加オプション";
      // 
      // FreeOptions
      // 
      FreeOptions.Location = new System.Drawing.Point(14, 32);
      FreeOptions.Name = "FreeOptions";
      FreeOptions.Size = new System.Drawing.Size(180, 19);
      FreeOptions.TabIndex = 0;
      // 
      // label11
      // 
      label11.AutoSize = true;
      label11.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      label11.Location = new System.Drawing.Point(14, 18);
      label11.Name = "label11";
      label11.Size = new System.Drawing.Size(186, 11);
      label11.TabIndex = 30;
      label11.Text = "カンマ、セミコロン、コロンで区切ってください";
      // 
      // groupBox3
      // 
      groupBox3.Controls.Add(cbDevices);
      groupBox3.Controls.Add(chkAudioOnly);
      groupBox3.Location = new System.Drawing.Point(11, 320);
      groupBox3.Name = "groupBox3";
      groupBox3.Size = new System.Drawing.Size(347, 46);
      groupBox3.TabIndex = 42;
      groupBox3.TabStop = false;
      groupBox3.Text = "その他";
      // 
      // tabPage1
      // 
      tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
      tabPage1.Controls.Add(CommonButtonBox);
      tabPage1.Controls.Add(groupBox8);
      tabPage1.Controls.Add(Image2Box);
      tabPage1.Location = new System.Drawing.Point(4, 22);
      tabPage1.Name = "tabPage1";
      tabPage1.Padding = new System.Windows.Forms.Padding(3);
      tabPage1.Size = new System.Drawing.Size(832, 418);
      tabPage1.TabIndex = 1;
      tabPage1.Text = "ユーティリティ";
      // 
      // CommonButtonBox
      // 
      CommonButtonBox.Controls.Add(CommandInvoker);
      CommonButtonBox.Controls.Add(btnStopUtil);
      CommonButtonBox.Controls.Add(btnStopAllUtil);
      CommonButtonBox.Controls.Add(button2);
      CommonButtonBox.Dock = System.Windows.Forms.DockStyle.Bottom;
      CommonButtonBox.Location = new System.Drawing.Point(3, 385);
      CommonButtonBox.Name = "CommonButtonBox";
      CommonButtonBox.Size = new System.Drawing.Size(826, 30);
      CommonButtonBox.TabIndex = 27;
      // 
      // CommandInvoker
      // 
      CommandInvoker.Location = new System.Drawing.Point(9, 3);
      CommandInvoker.Name = "CommandInvoker";
      CommandInvoker.Size = new System.Drawing.Size(106, 24);
      CommandInvoker.TabIndex = 2;
      CommandInvoker.Text = "コマンド実行";
      CommandInvoker.UseVisualStyleBackColor = true;
      CommandInvoker.Click += CommandInvoker_Click;
      // 
      // btnStopUtil
      // 
      btnStopUtil.Enabled = false;
      btnStopUtil.Location = new System.Drawing.Point(570, 3);
      btnStopUtil.Name = "btnStopUtil";
      btnStopUtil.Size = new System.Drawing.Size(56, 24);
      btnStopUtil.TabIndex = 33;
      btnStopUtil.TabStop = false;
      btnStopUtil.Text = "中止";
      btnStopUtil.UseVisualStyleBackColor = true;
      btnStopUtil.Click += btnStop_Click;
      // 
      // btnStopAllUtil
      // 
      btnStopAllUtil.Enabled = false;
      btnStopAllUtil.Location = new System.Drawing.Point(632, 3);
      btnStopAllUtil.Name = "btnStopAllUtil";
      btnStopAllUtil.Size = new System.Drawing.Size(64, 24);
      btnStopAllUtil.TabIndex = 34;
      btnStopAllUtil.TabStop = false;
      btnStopAllUtil.Text = "全て中止";
      btnStopAllUtil.UseVisualStyleBackColor = true;
      btnStopAllUtil.Click += btnStopAll_Click;
      // 
      // button2
      // 
      button2.Location = new System.Drawing.Point(704, 3);
      button2.Name = "button2";
      button2.Size = new System.Drawing.Size(113, 24);
      button2.TabIndex = 26;
      button2.TabStop = false;
      button2.Text = "ログ表示";
      button2.UseVisualStyleBackColor = true;
      button2.Click += OpenLogFile_Click;
      // 
      // groupBox8
      // 
      groupBox8.Controls.Add(label22);
      groupBox8.Controls.Add(label21);
      groupBox8.Controls.Add(SubmitCopy);
      groupBox8.Controls.Add(SubmitConcat);
      groupBox8.Location = new System.Drawing.Point(12, 136);
      groupBox8.Name = "groupBox8";
      groupBox8.Size = new System.Drawing.Size(805, 124);
      groupBox8.TabIndex = 2;
      groupBox8.TabStop = false;
      groupBox8.Text = "ツール";
      // 
      // label22
      // 
      label22.AutoSize = true;
      label22.Location = new System.Drawing.Point(151, 77);
      label22.Name = "label22";
      label22.Size = new System.Drawing.Size(243, 12);
      label22.TabIndex = 1;
      label22.Text = "エンコーダーをCOPYにしてコンテナを再生成します。";
      // 
      // label21
      // 
      label21.AutoSize = true;
      label21.Location = new System.Drawing.Point(151, 37);
      label21.Name = "label21";
      label21.Size = new System.Drawing.Size(349, 12);
      label21.TabIndex = 1;
      label21.Text = "入力ファイルを順番に連結します。異なるサイズの動画は結合できません。";
      // 
      // SubmitCopy
      // 
      SubmitCopy.Location = new System.Drawing.Point(18, 66);
      SubmitCopy.Name = "SubmitCopy";
      SubmitCopy.Size = new System.Drawing.Size(122, 32);
      SubmitCopy.TabIndex = 0;
      SubmitCopy.Text = "ファイルコンテナ変更";
      SubmitCopy.UseVisualStyleBackColor = true;
      SubmitCopy.Click += SubmitCopy_Click;
      // 
      // SubmitConcat
      // 
      SubmitConcat.Location = new System.Drawing.Point(18, 27);
      SubmitConcat.Name = "SubmitConcat";
      SubmitConcat.Size = new System.Drawing.Size(122, 32);
      SubmitConcat.TabIndex = 0;
      SubmitConcat.Text = "ファイル結合";
      SubmitConcat.UseVisualStyleBackColor = true;
      SubmitConcat.Click += SubmitConcat_Click;
      // 
      // Image2Box
      // 
      Image2Box.Controls.Add(ImageWidth);
      Image2Box.Controls.Add(ImageHeight);
      Image2Box.Controls.Add(label18);
      Image2Box.Controls.Add(label20);
      Image2Box.Controls.Add(label17);
      Image2Box.Controls.Add(FrameRate);
      Image2Box.Controls.Add(ImageType);
      Image2Box.Controls.Add(linkLabel2);
      Image2Box.Controls.Add(ImageTo);
      Image2Box.Controls.Add(ImageSS);
      Image2Box.Controls.Add(label16);
      Image2Box.Controls.Add(label9);
      Image2Box.Controls.Add(label19);
      Image2Box.Controls.Add(label15);
      Image2Box.Controls.Add(label14);
      Image2Box.Controls.Add(SubmitThumbnail);
      Image2Box.Location = new System.Drawing.Point(12, 25);
      Image2Box.Name = "Image2Box";
      Image2Box.Size = new System.Drawing.Size(805, 95);
      Image2Box.TabIndex = 1;
      Image2Box.TabStop = false;
      Image2Box.Text = "画像生成                                   ";
      // 
      // ImageWidth
      // 
      ImageWidth.Location = new System.Drawing.Point(382, 53);
      ImageWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      ImageWidth.Name = "ImageWidth";
      ImageWidth.Size = new System.Drawing.Size(64, 19);
      ImageWidth.TabIndex = 8;
      ImageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // ImageHeight
      // 
      ImageHeight.Location = new System.Drawing.Point(473, 53);
      ImageHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      ImageHeight.Name = "ImageHeight";
      ImageHeight.Size = new System.Drawing.Size(64, 19);
      ImageHeight.TabIndex = 8;
      ImageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label18
      // 
      label18.AutoSize = true;
      label18.Location = new System.Drawing.Point(594, 34);
      label18.Name = "label18";
      label18.Size = new System.Drawing.Size(77, 12);
      label18.TabIndex = 7;
      label18.Text = "出力画像形式";
      // 
      // label20
      // 
      label20.AutoSize = true;
      label20.Location = new System.Drawing.Point(543, 56);
      label20.Name = "label20";
      label20.Size = new System.Drawing.Size(42, 12);
      label20.TabIndex = 7;
      label20.Text = "ピクセル";
      // 
      // label17
      // 
      label17.AutoSize = true;
      label17.Location = new System.Drawing.Point(235, 56);
      label17.Name = "label17";
      label17.Size = new System.Drawing.Size(29, 12);
      label17.TabIndex = 7;
      label17.Text = "秒毎";
      // 
      // FrameRate
      // 
      FrameRate.Location = new System.Drawing.Point(164, 53);
      FrameRate.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
      FrameRate.Name = "FrameRate";
      FrameRate.Size = new System.Drawing.Size(64, 19);
      FrameRate.TabIndex = 6;
      FrameRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      FrameRate.Value = new decimal(new int[] { 1, 0, 0, 0 });
      // 
      // ImageType
      // 
      ImageType.DisplayMember = "Label";
      ImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      ImageType.FormattingEnabled = true;
      ImageType.Location = new System.Drawing.Point(594, 52);
      ImageType.Name = "ImageType";
      ImageType.Size = new System.Drawing.Size(94, 20);
      ImageType.TabIndex = 5;
      ImageType.ValueMember = "Value";
      // 
      // linkLabel2
      // 
      linkLabel2.AutoSize = true;
      linkLabel2.Location = new System.Drawing.Point(67, 0);
      linkLabel2.Name = "linkLabel2";
      linkLabel2.Size = new System.Drawing.Size(111, 12);
      linkLabel2.TabIndex = 4;
      linkLabel2.TabStop = true;
      linkLabel2.Text = "時間指定構文の説明";
      linkLabel2.LinkClicked += linkLabel1_LinkClicked;
      // 
      // ImageTo
      // 
      ImageTo.Location = new System.Drawing.Point(97, 53);
      ImageTo.Name = "ImageTo";
      ImageTo.Size = new System.Drawing.Size(53, 19);
      ImageTo.TabIndex = 2;
      // 
      // ImageSS
      // 
      ImageSS.Location = new System.Drawing.Point(18, 53);
      ImageSS.Name = "ImageSS";
      ImageSS.Size = new System.Drawing.Size(55, 19);
      ImageSS.TabIndex = 2;
      // 
      // label16
      // 
      label16.AutoSize = true;
      label16.Location = new System.Drawing.Point(75, 56);
      label16.Name = "label16";
      label16.Size = new System.Drawing.Size(17, 12);
      label16.TabIndex = 1;
      label16.Text = "〜";
      // 
      // label9
      // 
      label9.AutoSize = true;
      label9.Location = new System.Drawing.Point(453, 56);
      label9.Name = "label9";
      label9.Size = new System.Drawing.Size(17, 12);
      label9.TabIndex = 1;
      label9.Text = "×";
      label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label19
      // 
      label19.AutoSize = true;
      label19.Location = new System.Drawing.Point(379, 34);
      label19.Name = "label19";
      label19.Size = new System.Drawing.Size(65, 12);
      label19.TabIndex = 1;
      label19.Text = "画像リサイズ";
      // 
      // label15
      // 
      label15.AutoSize = true;
      label15.Location = new System.Drawing.Point(97, 34);
      label15.Name = "label15";
      label15.Size = new System.Drawing.Size(53, 12);
      label15.TabIndex = 1;
      label15.Text = "開始位置";
      // 
      // label14
      // 
      label14.AutoSize = true;
      label14.Location = new System.Drawing.Point(20, 34);
      label14.Name = "label14";
      label14.Size = new System.Drawing.Size(53, 12);
      label14.TabIndex = 1;
      label14.Text = "開始位置";
      // 
      // SubmitThumbnail
      // 
      SubmitThumbnail.Location = new System.Drawing.Point(693, 51);
      SubmitThumbnail.Name = "SubmitThumbnail";
      SubmitThumbnail.Size = new System.Drawing.Size(96, 23);
      SubmitThumbnail.TabIndex = 0;
      SubmitThumbnail.Text = "実行";
      SubmitThumbnail.UseVisualStyleBackColor = true;
      SubmitThumbnail.Click += SubmitThumbnail_Click;
      // 
      // groupBox5
      // 
      groupBox5.Controls.Add(FilePrefix);
      groupBox5.Controls.Add(FileSuffix);
      groupBox5.Controls.Add(btnSubmitOpenDlg);
      groupBox5.Controls.Add(FileContainer);
      groupBox5.Controls.Add(OpenFolder);
      groupBox5.Controls.Add(label13);
      groupBox5.Controls.Add(FileName);
      groupBox5.Controls.Add(label10);
      groupBox5.Controls.Add(cbOutputDir);
      groupBox5.Location = new System.Drawing.Point(433, 492);
      groupBox5.Name = "groupBox5";
      groupBox5.Size = new System.Drawing.Size(407, 125);
      groupBox5.TabIndex = 44;
      groupBox5.TabStop = false;
      groupBox5.Text = "出力フォルダとファイル";
      // 
      // FilePrefix
      // 
      FilePrefix.FormattingEnabled = true;
      FilePrefix.Location = new System.Drawing.Point(11, 85);
      FilePrefix.Name = "FilePrefix";
      FilePrefix.Size = new System.Drawing.Size(61, 20);
      FilePrefix.TabIndex = 34;
      // 
      // FileSuffix
      // 
      FileSuffix.FormattingEnabled = true;
      FileSuffix.Location = new System.Drawing.Point(256, 85);
      FileSuffix.Name = "FileSuffix";
      FileSuffix.Size = new System.Drawing.Size(59, 20);
      FileSuffix.TabIndex = 33;
      // 
      // settingsPropertyValueBindingSource
      // 
      settingsPropertyValueBindingSource.DataSource = typeof(System.Configuration.SettingsPropertyValue);
      // 
      // Form1
      // 
      AllowDrop = true;
      AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(846, 651);
      Controls.Add(groupBox5);
      Controls.Add(Tab);
      Controls.Add(StatusBar);
      Controls.Add(ffmpeg);
      Controls.Add(btnFindInPath);
      Controls.Add(label8);
      Controls.Add(btnFFmpeg);
      Controls.Add(panel1);
      Font = new System.Drawing.Font("MS UI Gothic", 9F);
      FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      Name = "Form1";
      Padding = new System.Windows.Forms.Padding(3);
      SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      Text = "ffmpeg command builder for NVENC and QSV : required ffmpeg 6.1 or higher";
      FormClosing += Form1_FormClosing;
      Load += Form1_Load;
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      groupBox2.ResumeLayout(false);
      groupBox2.PerformLayout();
      ResizeBox.ResumeLayout(false);
      ResizeBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)resizeTo).EndInit();
      RotateBox.ResumeLayout(false);
      RotateBox.PerformLayout();
      LayoutBox.ResumeLayout(false);
      LayoutBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)VideoHeight).EndInit();
      ((System.ComponentModel.ISupportInitialize)VideoWidth).EndInit();
      BitrateBox.ResumeLayout(false);
      BitrateBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)LookAhead).EndInit();
      ((System.ComponentModel.ISupportInitialize)aBitrate).EndInit();
      ((System.ComponentModel.ISupportInitialize)vBitrate).EndInit();
      groupBox4.ResumeLayout(false);
      groupBox4.PerformLayout();
      panel1.ResumeLayout(false);
      panel1.PerformLayout();
      StatusBar.ResumeLayout(false);
      StatusBar.PerformLayout();
      CropBox.ResumeLayout(false);
      CropBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)CropY).EndInit();
      ((System.ComponentModel.ISupportInitialize)CropX).EndInit();
      ((System.ComponentModel.ISupportInitialize)CropHeight).EndInit();
      ((System.ComponentModel.ISupportInitialize)CropWidth).EndInit();
      ((System.ComponentModel.ISupportInitialize)FileListBindingSource).EndInit();
      ((System.ComponentModel.ISupportInitialize)DeInterlaceListBindingSource).EndInit();
      Tab.ResumeLayout(false);
      PageConvert.ResumeLayout(false);
      PageConvert.PerformLayout();
      SubmitButtonBox.ResumeLayout(false);
      groupBox6.ResumeLayout(false);
      groupBox6.PerformLayout();
      groupBox3.ResumeLayout(false);
      groupBox3.PerformLayout();
      tabPage1.ResumeLayout(false);
      CommonButtonBox.ResumeLayout(false);
      groupBox8.ResumeLayout(false);
      groupBox8.PerformLayout();
      Image2Box.ResumeLayout(false);
      Image2Box.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)ImageWidth).EndInit();
      ((System.ComponentModel.ISupportInitialize)ImageHeight).EndInit();
      ((System.ComponentModel.ISupportInitialize)FrameRate).EndInit();
      groupBox5.ResumeLayout(false);
      groupBox5.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)settingsPropertyValueBindingSource).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.TextBox Commandlines;
    private System.Windows.Forms.TextBox txtSS;
    private System.Windows.Forms.FolderBrowserDialog FindFolder;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTo;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.OpenFileDialog FindSaveBatchFile;
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
    private System.Windows.Forms.OpenFileDialog OpenInputFile;
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
    private System.Windows.Forms.OpenFileDialog OpenFFMpegFileDlg;
    private System.Windows.Forms.Button btnFindInPath;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.ComboBox ffmpeg;
    private System.Windows.Forms.NumericUpDown resizeTo;
    private System.Windows.Forms.RadioButton rbResizeNum;
    private System.Windows.Forms.Button OpenLogFile;
    private System.Windows.Forms.Button ClearFileList;
    private System.Windows.Forms.ComboBox cbDeinterlaceAlg;
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
    private System.Windows.Forms.BindingSource DeInterlaceListBindingSource;
    private System.Windows.Forms.Button OpenEncoderHelp;
    private System.Windows.Forms.Button OpenDecoderHelp;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.ComboBox FileContainer;
    private System.Windows.Forms.TabControl Tab;
    private System.Windows.Forms.TabPage PageConvert;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.BindingSource settingsPropertyValueBindingSource;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.TextBox FreeOptions;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.Button SubmitConcat;
    private System.Windows.Forms.GroupBox Image2Box;
    private System.Windows.Forms.Button SubmitThumbnail;
    private System.Windows.Forms.LinkLabel linkLabel2;
    private System.Windows.Forms.TextBox ImageSS;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.ComboBox ImageType;
    private System.Windows.Forms.TextBox ImageTo;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.NumericUpDown FrameRate;
    private System.Windows.Forms.GroupBox groupBox8;
    private System.Windows.Forms.Button SubmitCopy;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.NumericUpDown ImageHeight;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.Panel SubmitButtonBox;
    private System.Windows.Forms.Panel CommonButtonBox;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.NumericUpDown ImageWidth;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Button btnStopUtil;
    private System.Windows.Forms.Button btnStopAllUtil;
    private System.Windows.Forms.ComboBox FilePrefix;
    private System.Windows.Forms.ComboBox FileSuffix;
    private System.Windows.Forms.Button CommandInvoker;
  }
}

