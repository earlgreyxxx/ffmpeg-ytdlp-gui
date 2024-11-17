namespace ffmpeg_ytdlp_gui
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
      label1 = new System.Windows.Forms.Label();
      label2 = new System.Windows.Forms.Label();
      txtTo = new System.Windows.Forms.TextBox();
      CuttingBox = new System.Windows.Forms.GroupBox();
      linkLabel1 = new System.Windows.Forms.LinkLabel();
      btnClearSS = new System.Windows.Forms.Button();
      btnClearTo = new System.Windows.Forms.Button();
      CodecBox = new System.Windows.Forms.GroupBox();
      label7 = new System.Windows.Forms.Label();
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
      rbResize900 = new System.Windows.Forms.RadioButton();
      rbResizeSD = new System.Windows.Forms.RadioButton();
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
      btnSubmitAddToBatch = new System.Windows.Forms.Button();
      btnSubmitInvoke = new System.Windows.Forms.Button();
      btnClear = new System.Windows.Forms.Button();
      label3 = new System.Windows.Forms.Label();
      BitrateBox = new System.Windows.Forms.GroupBox();
      VideoFrameRate = new System.Windows.Forms.ComboBox();
      label27 = new System.Windows.Forms.Label();
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
      FileList = new System.Windows.Forms.ListBox();
      FileListMenu = new System.Windows.Forms.ContextMenuStrip(components);
      FileListMenuItemAddFolder = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      FileListMenuItemOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      FileListMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
      FileListMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
      btnStop = new System.Windows.Forms.Button();
      btnStopAll = new System.Windows.Forms.Button();
      DeInterlaceBox = new System.Windows.Forms.GroupBox();
      cbDevices = new System.Windows.Forms.ComboBox();
      InputBox = new System.Windows.Forms.Panel();
      btnFFmpeg = new System.Windows.Forms.Button();
      label8 = new System.Windows.Forms.Label();
      btnFindInPath = new System.Windows.Forms.Button();
      ffmpeg = new System.Windows.Forms.ComboBox();
      StatusBar = new System.Windows.Forms.StatusStrip();
      StatusBarMenu = new System.Windows.Forms.ContextMenuStrip(components);
      StatusBarMenuItemClearQueue = new System.Windows.Forms.ToolStripMenuItem();
      BatListCount = new System.Windows.Forms.ToolStripStatusLabel();
      OutputStderr = new System.Windows.Forms.ToolStripStatusLabel();
      DummyProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      QueueCount = new System.Windows.Forms.ToolStripStatusLabel();
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
      btnSubmitBatExecute = new System.Windows.Forms.Button();
      btnSubmitBatchClear = new System.Windows.Forms.Button();
      btnSubmitSaveToFile = new System.Windows.Forms.Button();
      groupBox6 = new System.Windows.Forms.GroupBox();
      FreeOptions = new System.Windows.Forms.TextBox();
      OthersBox = new System.Windows.Forms.GroupBox();
      PageUtility = new System.Windows.Forms.TabPage();
      groupBox7 = new System.Windows.Forms.GroupBox();
      DecoderHelpList = new System.Windows.Forms.ComboBox();
      EncoderHelpList = new System.Windows.Forms.ComboBox();
      OpenEncoderHelp = new System.Windows.Forms.Button();
      OpenDecoderHelp = new System.Windows.Forms.Button();
      CommonButtonBox = new System.Windows.Forms.Panel();
      btnStopUtil = new System.Windows.Forms.Button();
      btnStopAllUtil = new System.Windows.Forms.Button();
      button2 = new System.Windows.Forms.Button();
      groupBox8 = new System.Windows.Forms.GroupBox();
      label22 = new System.Windows.Forms.Label();
      label21 = new System.Windows.Forms.Label();
      SubmitCopy = new System.Windows.Forms.Button();
      SubmitConcat = new System.Windows.Forms.Button();
      Image2Box = new System.Windows.Forms.GroupBox();
      ImageFreeOptions = new System.Windows.Forms.TextBox();
      label11 = new System.Windows.Forms.Label();
      useTiledImage = new System.Windows.Forms.CheckBox();
      TileRows = new System.Windows.Forms.NumericUpDown();
      CropTB = new System.Windows.Forms.NumericUpDown();
      ImageWidth = new System.Windows.Forms.NumericUpDown();
      TileColumns = new System.Windows.Forms.NumericUpDown();
      CropLR = new System.Windows.Forms.NumericUpDown();
      ImageHeight = new System.Windows.Forms.NumericUpDown();
      label18 = new System.Windows.Forms.Label();
      label31 = new System.Windows.Forms.Label();
      label20 = new System.Windows.Forms.Label();
      label17 = new System.Windows.Forms.Label();
      FrameRate = new System.Windows.Forms.NumericUpDown();
      ImageType = new System.Windows.Forms.ComboBox();
      linkLabel2 = new System.Windows.Forms.LinkLabel();
      ImageTo = new System.Windows.Forms.TextBox();
      ImageSS = new System.Windows.Forms.TextBox();
      label33 = new System.Windows.Forms.Label();
      label26 = new System.Windows.Forms.Label();
      label16 = new System.Windows.Forms.Label();
      label25 = new System.Windows.Forms.Label();
      label9 = new System.Windows.Forms.Label();
      label19 = new System.Windows.Forms.Label();
      label15 = new System.Windows.Forms.Label();
      label14 = new System.Windows.Forms.Label();
      SubmitThumbnail = new System.Windows.Forms.Button();
      PageDownloader = new System.Windows.Forms.TabPage();
      AddDownloadQueue = new System.Windows.Forms.Button();
      SubmitDownloadDequeue = new System.Windows.Forms.Button();
      PlaylistGroup = new System.Windows.Forms.GroupBox();
      DownloadBestQuality = new System.Windows.Forms.RadioButton();
      DownloadWorstQuality = new System.Windows.Forms.RadioButton();
      PlayListDownloadAll = new System.Windows.Forms.Button();
      Playlist = new System.Windows.Forms.ListBox();
      linkLabel5 = new System.Windows.Forms.LinkLabel();
      MediaTitle = new System.Windows.Forms.TextBox();
      linkLabel3 = new System.Windows.Forms.LinkLabel();
      OutputFileFormat = new System.Windows.Forms.ComboBox();
      DownloadUrl = new System.Windows.Forms.ComboBox();
      DurationTime = new System.Windows.Forms.Label();
      chkAfterDownload = new System.Windows.Forms.CheckBox();
      StopDownload = new System.Windows.Forms.Button();
      groupBox1 = new System.Windows.Forms.GroupBox();
      FmtSeparated = new System.Windows.Forms.RadioButton();
      FmtWhole = new System.Windows.Forms.RadioButton();
      MovieFormat = new System.Windows.Forms.ComboBox();
      label23 = new System.Windows.Forms.Label();
      label34 = new System.Windows.Forms.Label();
      label30 = new System.Windows.Forms.Label();
      VideoOnlyFormat = new System.Windows.Forms.ComboBox();
      AudioOnlyFormat = new System.Windows.Forms.ComboBox();
      LinkYdlOutputTemplate = new System.Windows.Forms.LinkLabel();
      ThumbnailBox = new System.Windows.Forms.PictureBox();
      label28 = new System.Windows.Forms.Label();
      SubmitConfirmFormat = new System.Windows.Forms.Button();
      PageSetting = new System.Windows.Forms.TabPage();
      groupBox4 = new System.Windows.Forms.GroupBox();
      LoadFromJson = new System.Windows.Forms.Button();
      SaveToJson = new System.Windows.Forms.Button();
      label35 = new System.Windows.Forms.Label();
      MaxListItems = new System.Windows.Forms.NumericUpDown();
      CommandInvoker = new System.Windows.Forms.Button();
      Overwrite = new System.Windows.Forms.CheckBox();
      ClearLists = new System.Windows.Forms.Button();
      IsOpenStderr = new System.Windows.Forms.CheckBox();
      groupBox3 = new System.Windows.Forms.GroupBox();
      ConfigDirectory = new System.Windows.Forms.Label();
      SubmitConfigDirDlg = new System.Windows.Forms.Button();
      UseCustomConfig = new System.Windows.Forms.CheckBox();
      PrimaryMovieFormatId = new System.Windows.Forms.TextBox();
      PrimaryAudioFormatId = new System.Windows.Forms.TextBox();
      label39 = new System.Windows.Forms.Label();
      label38 = new System.Windows.Forms.Label();
      label37 = new System.Windows.Forms.Label();
      label36 = new System.Windows.Forms.Label();
      PrimaryVideoFormatId = new System.Windows.Forms.TextBox();
      HideThumbnail = new System.Windows.Forms.CheckBox();
      label24 = new System.Windows.Forms.Label();
      label29 = new System.Windows.Forms.Label();
      SubmitOpenCookie = new System.Windows.Forms.Button();
      CookiePath = new System.Windows.Forms.TextBox();
      label0 = new System.Windows.Forms.Label();
      DeleteUrlAfterDownloaded = new System.Windows.Forms.CheckBox();
      label32 = new System.Windows.Forms.Label();
      UseCookie = new System.Windows.Forms.ComboBox();
      YtdlpPath = new System.Windows.Forms.ComboBox();
      ytdlpFindInPath = new System.Windows.Forms.Button();
      btnYtdlp = new System.Windows.Forms.Button();
      groupBox2 = new System.Windows.Forms.GroupBox();
      RemoveBatListAfterDone = new System.Windows.Forms.CheckBox();
      BatExecWithConsole = new System.Windows.Forms.CheckBox();
      ImageContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
      CommandSaveImage = new System.Windows.Forms.ToolStripMenuItem();
      OutputBox = new System.Windows.Forms.GroupBox();
      linkLabel4 = new System.Windows.Forms.LinkLabel();
      FilePrefix = new System.Windows.Forms.ComboBox();
      FileSuffix = new System.Windows.Forms.ComboBox();
      settingsPropertyValueBindingSource = new System.Windows.Forms.BindingSource(components);
      DirectoryListBindingSource = new System.Windows.Forms.BindingSource(components);
      VideoOnlyFormatSource = new System.Windows.Forms.BindingSource(components);
      AudioOnlyFormatSource = new System.Windows.Forms.BindingSource(components);
      MovieFormatSource = new System.Windows.Forms.BindingSource(components);
      UrlBindingSource = new System.Windows.Forms.BindingSource(components);
      OutputFileFormatBindingSource = new System.Windows.Forms.BindingSource(components);
      PlaylistBindingSource = new System.Windows.Forms.BindingSource(components);
      TooltipHintStringInput = new System.Windows.Forms.ToolTip(components);
      CuttingBox.SuspendLayout();
      CodecBox.SuspendLayout();
      ResizeBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)resizeTo).BeginInit();
      RotateBox.SuspendLayout();
      BitrateBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)LookAhead).BeginInit();
      ((System.ComponentModel.ISupportInitialize)aBitrate).BeginInit();
      ((System.ComponentModel.ISupportInitialize)vBitrate).BeginInit();
      FileListMenu.SuspendLayout();
      DeInterlaceBox.SuspendLayout();
      InputBox.SuspendLayout();
      StatusBar.SuspendLayout();
      StatusBarMenu.SuspendLayout();
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
      OthersBox.SuspendLayout();
      PageUtility.SuspendLayout();
      groupBox7.SuspendLayout();
      CommonButtonBox.SuspendLayout();
      groupBox8.SuspendLayout();
      Image2Box.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)TileRows).BeginInit();
      ((System.ComponentModel.ISupportInitialize)CropTB).BeginInit();
      ((System.ComponentModel.ISupportInitialize)ImageWidth).BeginInit();
      ((System.ComponentModel.ISupportInitialize)TileColumns).BeginInit();
      ((System.ComponentModel.ISupportInitialize)CropLR).BeginInit();
      ((System.ComponentModel.ISupportInitialize)ImageHeight).BeginInit();
      ((System.ComponentModel.ISupportInitialize)FrameRate).BeginInit();
      PageDownloader.SuspendLayout();
      PlaylistGroup.SuspendLayout();
      groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)ThumbnailBox).BeginInit();
      PageSetting.SuspendLayout();
      groupBox4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)MaxListItems).BeginInit();
      groupBox3.SuspendLayout();
      groupBox2.SuspendLayout();
      ImageContextMenu.SuspendLayout();
      OutputBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)settingsPropertyValueBindingSource).BeginInit();
      ((System.ComponentModel.ISupportInitialize)DirectoryListBindingSource).BeginInit();
      ((System.ComponentModel.ISupportInitialize)VideoOnlyFormatSource).BeginInit();
      ((System.ComponentModel.ISupportInitialize)AudioOnlyFormatSource).BeginInit();
      ((System.ComponentModel.ISupportInitialize)MovieFormatSource).BeginInit();
      ((System.ComponentModel.ISupportInitialize)UrlBindingSource).BeginInit();
      ((System.ComponentModel.ISupportInitialize)OutputFileFormatBindingSource).BeginInit();
      ((System.ComponentModel.ISupportInitialize)PlaylistBindingSource).BeginInit();
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
      Commandlines.TabStop = false;
      // 
      // txtSS
      // 
      txtSS.Location = new System.Drawing.Point(13, 42);
      txtSS.Name = "txtSS";
      txtSS.Size = new System.Drawing.Size(62, 19);
      txtSS.TabIndex = 4;
      txtSS.TabStop = false;
      txtSS.Validating += TimeFormatValidating;
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
      txtTo.TabStop = false;
      txtTo.Validating += TimeFormatValidating;
      // 
      // CuttingBox
      // 
      CuttingBox.Controls.Add(linkLabel1);
      CuttingBox.Controls.Add(btnClearSS);
      CuttingBox.Controls.Add(btnClearTo);
      CuttingBox.Controls.Add(label1);
      CuttingBox.Controls.Add(txtTo);
      CuttingBox.Controls.Add(txtSS);
      CuttingBox.Controls.Add(label2);
      CuttingBox.Location = new System.Drawing.Point(10, 85);
      CuttingBox.Name = "CuttingBox";
      CuttingBox.Size = new System.Drawing.Size(275, 80);
      CuttingBox.TabIndex = 5;
      CuttingBox.TabStop = false;
      CuttingBox.Text = "切り取り　　　　　　　　　　　　　　　";
      // 
      // linkLabel1
      // 
      linkLabel1.AutoSize = true;
      linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
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
      // CodecBox
      // 
      CodecBox.Controls.Add(label7);
      CodecBox.Controls.Add(chkUseHWDecoder);
      CodecBox.Controls.Add(HWDecoder);
      CodecBox.Controls.Add(label5);
      CodecBox.Controls.Add(label4);
      CodecBox.Controls.Add(UseAudioEncoder);
      CodecBox.Controls.Add(UseVideoEncoder);
      CodecBox.Location = new System.Drawing.Point(651, 171);
      CodecBox.Name = "CodecBox";
      CodecBox.Size = new System.Drawing.Size(168, 195);
      CodecBox.TabIndex = 7;
      CodecBox.TabStop = false;
      CodecBox.Text = "コーデック";
      // 
      // label7
      // 
      label7.AutoEllipsis = true;
      label7.Location = new System.Drawing.Point(8, 76);
      label7.Name = "label7";
      label7.Size = new System.Drawing.Size(154, 46);
      label7.TabIndex = 30;
      label7.Text = "コーデックの種類により無視される設定があります。";
      // 
      // chkUseHWDecoder
      // 
      chkUseHWDecoder.AutoSize = true;
      chkUseHWDecoder.Location = new System.Drawing.Point(12, 131);
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
      HWDecoder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      HWDecoder.FormattingEnabled = true;
      HWDecoder.Location = new System.Drawing.Point(11, 150);
      HWDecoder.Name = "HWDecoder";
      HWDecoder.Size = new System.Drawing.Size(148, 20);
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
      UseAudioEncoder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      UseAudioEncoder.FormattingEnabled = true;
      UseAudioEncoder.Location = new System.Drawing.Point(43, 44);
      UseAudioEncoder.Name = "UseAudioEncoder";
      UseAudioEncoder.Size = new System.Drawing.Size(116, 20);
      UseAudioEncoder.TabIndex = 25;
      UseAudioEncoder.TabStop = false;
      UseAudioEncoder.ValueMember = "Value";
      // 
      // UseVideoEncoder
      // 
      UseVideoEncoder.BackColor = System.Drawing.SystemColors.Window;
      UseVideoEncoder.DisplayMember = "Label";
      UseVideoEncoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      UseVideoEncoder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      UseVideoEncoder.Location = new System.Drawing.Point(43, 18);
      UseVideoEncoder.Name = "UseVideoEncoder";
      UseVideoEncoder.Size = new System.Drawing.Size(116, 20);
      UseVideoEncoder.TabIndex = 24;
      UseVideoEncoder.TabStop = false;
      UseVideoEncoder.ValueMember = "Value";
      UseVideoEncoder.SelectedIndexChanged += UseVideoEncoder_SelectedIndexChanged;
      // 
      // cbDeinterlaceAlg
      // 
      cbDeinterlaceAlg.DisplayMember = "Label";
      cbDeinterlaceAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cbDeinterlaceAlg.Enabled = false;
      cbDeinterlaceAlg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      cbDeinterlaceAlg.FormattingEnabled = true;
      cbDeinterlaceAlg.Location = new System.Drawing.Point(89, 19);
      cbDeinterlaceAlg.Name = "cbDeinterlaceAlg";
      cbDeinterlaceAlg.Size = new System.Drawing.Size(131, 20);
      cbDeinterlaceAlg.TabIndex = 27;
      cbDeinterlaceAlg.ValueMember = "Value";
      // 
      // chkFilterDeInterlace
      // 
      chkFilterDeInterlace.AutoSize = true;
      chkFilterDeInterlace.Location = new System.Drawing.Point(16, 21);
      chkFilterDeInterlace.Name = "chkFilterDeInterlace";
      chkFilterDeInterlace.Size = new System.Drawing.Size(67, 16);
      chkFilterDeInterlace.TabIndex = 26;
      chkFilterDeInterlace.Text = "処理する";
      chkFilterDeInterlace.CheckedChanged += chkFilterDeInterlace_CheckedChanged;
      // 
      // chkAudioOnly
      // 
      chkAudioOnly.AutoSize = true;
      chkAudioOnly.Location = new System.Drawing.Point(264, 52);
      chkAudioOnly.Name = "chkAudioOnly";
      chkAudioOnly.Size = new System.Drawing.Size(93, 16);
      chkAudioOnly.TabIndex = 23;
      chkAudioOnly.TabStop = false;
      chkAudioOnly.Text = "音声のみ出力";
      chkAudioOnly.UseVisualStyleBackColor = true;
      chkAudioOnly.CheckedChanged += chkAudioOnly_CheckedChanged;
      // 
      // ResizeBox
      // 
      ResizeBox.Controls.Add(rbResizeNum);
      ResizeBox.Controls.Add(resizeTo);
      ResizeBox.Controls.Add(rbResize900);
      ResizeBox.Controls.Add(rbResizeSD);
      ResizeBox.Controls.Add(rbResizeHD);
      ResizeBox.Controls.Add(rbResizeFullHD);
      ResizeBox.Controls.Add(rbResizeNone);
      ResizeBox.Location = new System.Drawing.Point(334, 171);
      ResizeBox.Name = "ResizeBox";
      ResizeBox.Size = new System.Drawing.Size(311, 76);
      ResizeBox.TabIndex = 8;
      ResizeBox.TabStop = false;
      ResizeBox.Text = "リサイズ：短辺指定";
      // 
      // rbResizeNum
      // 
      rbResizeNum.AutoSize = true;
      rbResizeNum.Location = new System.Drawing.Point(15, 53);
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
      resizeTo.Location = new System.Drawing.Point(80, 51);
      resizeTo.Maximum = new decimal(new int[] { 4320, 0, 0, 0 });
      resizeTo.Minimum = new decimal(new int[] { 32, 0, 0, 0 });
      resizeTo.Name = "resizeTo";
      resizeTo.Size = new System.Drawing.Size(54, 19);
      resizeTo.TabIndex = 14;
      resizeTo.TabStop = false;
      resizeTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      resizeTo.Value = new decimal(new int[] { 320, 0, 0, 0 });
      // 
      // rbResize900
      // 
      rbResize900.AutoSize = true;
      rbResize900.Location = new System.Drawing.Point(129, 24);
      rbResize900.Name = "rbResize900";
      rbResize900.Size = new System.Drawing.Size(53, 16);
      rbResize900.TabIndex = 13;
      rbResize900.Tag = "900";
      rbResize900.Text = "900px";
      // 
      // rbResizeSD
      // 
      rbResizeSD.AutoSize = true;
      rbResizeSD.Location = new System.Drawing.Point(244, 24);
      rbResizeSD.Name = "rbResizeSD";
      rbResizeSD.Size = new System.Drawing.Size(53, 16);
      rbResizeSD.TabIndex = 13;
      rbResizeSD.Tag = "480";
      rbResizeSD.Text = "480px";
      // 
      // rbResizeHD
      // 
      rbResizeHD.AutoSize = true;
      rbResizeHD.Location = new System.Drawing.Point(185, 24);
      rbResizeHD.Name = "rbResizeHD";
      rbResizeHD.Size = new System.Drawing.Size(53, 16);
      rbResizeHD.TabIndex = 13;
      rbResizeHD.Tag = "720";
      rbResizeHD.Text = "720px";
      // 
      // rbResizeFullHD
      // 
      rbResizeFullHD.AutoSize = true;
      rbResizeFullHD.Location = new System.Drawing.Point(64, 24);
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
      rbResizeNone.Location = new System.Drawing.Point(15, 24);
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
      RotateBox.Location = new System.Drawing.Point(10, 256);
      RotateBox.Name = "RotateBox";
      RotateBox.Size = new System.Drawing.Size(318, 50);
      RotateBox.TabIndex = 9;
      RotateBox.TabStop = false;
      RotateBox.Text = "回転：90°";
      // 
      // rbRotateNone
      // 
      rbRotateNone.AutoSize = true;
      rbRotateNone.Checked = true;
      rbRotateNone.Location = new System.Drawing.Point(54, 22);
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
      rbRotateLeft.Location = new System.Drawing.Point(190, 22);
      rbRotateLeft.Name = "rbRotateLeft";
      rbRotateLeft.Size = new System.Drawing.Size(79, 16);
      rbRotateLeft.TabIndex = 18;
      rbRotateLeft.Tag = "2";
      rbRotateLeft.Text = "反時計周り";
      // 
      // rbRotateRight
      // 
      rbRotateRight.AutoSize = true;
      rbRotateRight.Location = new System.Drawing.Point(112, 22);
      rbRotateRight.Name = "rbRotateRight";
      rbRotateRight.Size = new System.Drawing.Size(67, 16);
      rbRotateRight.TabIndex = 17;
      rbRotateRight.Tag = "1";
      rbRotateRight.Text = "時計周り";
      // 
      // cbOutputDir
      // 
      cbOutputDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      cbOutputDir.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      cbOutputDir.Location = new System.Drawing.Point(11, 30);
      cbOutputDir.Name = "cbOutputDir";
      cbOutputDir.Size = new System.Drawing.Size(285, 25);
      cbOutputDir.TabIndex = 16;
      cbOutputDir.TabStop = false;
      cbOutputDir.SelectedIndexChanged += cbOutputDir_SelectedIndexChanged;
      // 
      // FileContainer
      // 
      FileContainer.DisplayMember = "Label";
      FileContainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      FileContainer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      FileContainer.Font = new System.Drawing.Font("Meiryo UI", 9F);
      FileContainer.FormattingEnabled = true;
      FileContainer.Location = new System.Drawing.Point(334, 65);
      FileContainer.Name = "FileContainer";
      FileContainer.Size = new System.Drawing.Size(64, 23);
      FileContainer.TabIndex = 31;
      FileContainer.TabStop = false;
      FileContainer.ValueMember = "Value";
      // 
      // label13
      // 
      label13.AutoSize = true;
      label13.Font = new System.Drawing.Font("Meiryo UI", 9F);
      label13.Location = new System.Drawing.Point(320, 74);
      label13.Name = "label13";
      label13.Size = new System.Drawing.Size(11, 15);
      label13.TabIndex = 32;
      label13.Text = ".";
      // 
      // label10
      // 
      label10.AutoSize = true;
      label10.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      label10.Location = new System.Drawing.Point(10, 95);
      label10.Name = "label10";
      label10.Size = new System.Drawing.Size(374, 14);
      label10.TabIndex = 30;
      label10.Text = "※複数の入力ファイルがあり、元ファイル名以外を指定すると、連番号が付加されます。";
      // 
      // FileName
      // 
      FileName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      FileName.Font = new System.Drawing.Font("Meiryo UI", 9F);
      FileName.FormattingEnabled = true;
      FileName.ItemHeight = 15;
      FileName.Items.AddRange(new object[] { "元ファイル名" });
      FileName.Location = new System.Drawing.Point(73, 65);
      FileName.Name = "FileName";
      FileName.Size = new System.Drawing.Size(182, 23);
      FileName.TabIndex = 29;
      FileName.TabStop = false;
      // 
      // OpenLogFile
      // 
      OpenLogFile.Location = new System.Drawing.Point(528, 12);
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
      OpenFolder.Font = new System.Drawing.Font("Meiryo UI", 9F);
      OpenFolder.Location = new System.Drawing.Point(351, 28);
      OpenFolder.Name = "OpenFolder";
      OpenFolder.Size = new System.Drawing.Size(47, 28);
      OpenFolder.TabIndex = 10;
      OpenFolder.TabStop = false;
      OpenFolder.Text = "開く";
      OpenFolder.UseVisualStyleBackColor = true;
      OpenFolder.Click += OpenFolder_Click;
      // 
      // btnSubmitOpenDlg
      // 
      btnSubmitOpenDlg.Font = new System.Drawing.Font("Meiryo UI", 9F);
      btnSubmitOpenDlg.Location = new System.Drawing.Point(302, 28);
      btnSubmitOpenDlg.Name = "btnSubmitOpenDlg";
      btnSubmitOpenDlg.Size = new System.Drawing.Size(48, 28);
      btnSubmitOpenDlg.TabIndex = 9;
      btnSubmitOpenDlg.TabStop = false;
      btnSubmitOpenDlg.Text = "参照";
      btnSubmitOpenDlg.UseVisualStyleBackColor = true;
      btnSubmitOpenDlg.Click += btnSubmitOpenDlg_Click;
      // 
      // btnSubmitAddToBatch
      // 
      btnSubmitAddToBatch.Location = new System.Drawing.Point(204, 12);
      btnSubmitAddToBatch.Name = "btnSubmitAddToBatch";
      btnSubmitAddToBatch.Size = new System.Drawing.Size(93, 24);
      btnSubmitAddToBatch.TabIndex = 23;
      btnSubmitAddToBatch.TabStop = false;
      btnSubmitAddToBatch.Text = "バッチに追加";
      btnSubmitAddToBatch.UseVisualStyleBackColor = true;
      btnSubmitAddToBatch.Click += btnSubmitAddToFile_Click;
      // 
      // btnSubmitInvoke
      // 
      btnSubmitInvoke.Enabled = false;
      btnSubmitInvoke.Location = new System.Drawing.Point(733, 12);
      btnSubmitInvoke.Name = "btnSubmitInvoke";
      btnSubmitInvoke.Size = new System.Drawing.Size(90, 24);
      btnSubmitInvoke.TabIndex = 1;
      btnSubmitInvoke.TabStop = false;
      btnSubmitInvoke.Text = "実行";
      btnSubmitInvoke.UseVisualStyleBackColor = true;
      btnSubmitInvoke.Click += btnSubmitInvoke_Click;
      // 
      // btnClear
      // 
      btnClear.Location = new System.Drawing.Point(706, 56);
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
      label3.Size = new System.Drawing.Size(344, 15);
      label3.TabIndex = 0;
      label3.Text = "動画ファイルをドラッグ＆ドロップもしくはダブルクリックして選択してください。";
      label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      label3.MouseDoubleClick += DropArea_MouseDoubleClick;
      // 
      // BitrateBox
      // 
      BitrateBox.Controls.Add(VideoFrameRate);
      BitrateBox.Controls.Add(chkAudioOnly);
      BitrateBox.Controls.Add(label27);
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
      BitrateBox.Location = new System.Drawing.Point(295, 85);
      BitrateBox.Name = "BitrateBox";
      BitrateBox.Size = new System.Drawing.Size(524, 80);
      BitrateBox.TabIndex = 15;
      BitrateBox.TabStop = false;
      BitrateBox.Text = "出力品質";
      // 
      // VideoFrameRate
      // 
      VideoFrameRate.DisplayMember = "Label";
      VideoFrameRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      VideoFrameRate.FormattingEnabled = true;
      VideoFrameRate.Location = new System.Drawing.Point(295, 21);
      VideoFrameRate.Name = "VideoFrameRate";
      VideoFrameRate.Size = new System.Drawing.Size(96, 20);
      VideoFrameRate.TabIndex = 30;
      VideoFrameRate.ValueMember = "Value";
      // 
      // label27
      // 
      label27.AutoSize = true;
      label27.Location = new System.Drawing.Point(268, 25);
      label27.Name = "label27";
      label27.Size = new System.Drawing.Size(23, 12);
      label27.TabIndex = 29;
      label27.Text = "-r:v";
      // 
      // label12
      // 
      label12.AutoSize = true;
      label12.Location = new System.Drawing.Point(406, 26);
      label12.Name = "label12";
      label12.Size = new System.Drawing.Size(61, 12);
      label12.TabIndex = 28;
      label12.Text = "LookAhead";
      // 
      // LookAhead
      // 
      LookAhead.Location = new System.Drawing.Point(469, 22);
      LookAhead.Name = "LookAhead";
      LookAhead.Size = new System.Drawing.Size(46, 19);
      LookAhead.TabIndex = 22;
      LookAhead.TabStop = false;
      LookAhead.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      LookAhead.Value = new decimal(new int[] { 15, 0, 0, 0 });
      // 
      // aUnit
      // 
      aUnit.AutoSize = true;
      aUnit.Location = new System.Drawing.Point(219, 52);
      aUnit.Name = "aUnit";
      aUnit.Size = new System.Drawing.Size(30, 12);
      aUnit.TabIndex = 26;
      aUnit.Text = "Kbps";
      // 
      // aBitrate
      // 
      aBitrate.Increment = new decimal(new int[] { 16, 0, 0, 0 });
      aBitrate.Location = new System.Drawing.Point(148, 49);
      aBitrate.Maximum = new decimal(new int[] { 320, 0, 0, 0 });
      aBitrate.Name = "aBitrate";
      aBitrate.Size = new System.Drawing.Size(70, 19);
      aBitrate.TabIndex = 20;
      aBitrate.TabStop = false;
      aBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      aBitrate.Value = new decimal(new int[] { 192, 0, 0, 0 });
      // 
      // aQualityLabel
      // 
      aQualityLabel.AutoSize = true;
      aQualityLabel.Location = new System.Drawing.Point(119, 52);
      aQualityLabel.Name = "aQualityLabel";
      aQualityLabel.Size = new System.Drawing.Size(25, 12);
      aQualityLabel.TabIndex = 24;
      aQualityLabel.Text = "-b:a";
      // 
      // vQualityLabel
      // 
      vQualityLabel.Location = new System.Drawing.Point(94, 25);
      vQualityLabel.Name = "vQualityLabel";
      vQualityLabel.Size = new System.Drawing.Size(50, 12);
      vQualityLabel.TabIndex = 23;
      vQualityLabel.Text = "-b:v";
      vQualityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label6
      // 
      label6.AutoSize = true;
      label6.Location = new System.Drawing.Point(364, 52);
      label6.Name = "label6";
      label6.Size = new System.Drawing.Size(38, 12);
      label6.TabIndex = 22;
      label6.Text = "Preset";
      // 
      // cbPreset
      // 
      cbPreset.DisplayMember = "Label";
      cbPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cbPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      cbPreset.FormattingEnabled = true;
      cbPreset.Location = new System.Drawing.Point(411, 48);
      cbPreset.Name = "cbPreset";
      cbPreset.Size = new System.Drawing.Size(104, 20);
      cbPreset.TabIndex = 21;
      cbPreset.TabStop = false;
      cbPreset.ValueMember = "Value";
      // 
      // vBitrate
      // 
      vBitrate.Location = new System.Drawing.Point(148, 22);
      vBitrate.Maximum = new decimal(new int[] { 1215752191, 23, 0, 0 });
      vBitrate.Name = "vBitrate";
      vBitrate.Size = new System.Drawing.Size(70, 19);
      vBitrate.TabIndex = 18;
      vBitrate.TabStop = false;
      vBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      vBitrate.Value = new decimal(new int[] { 6000, 0, 0, 0 });
      // 
      // chkEncodeAudio
      // 
      chkEncodeAudio.AutoSize = true;
      chkEncodeAudio.Location = new System.Drawing.Point(14, 50);
      chkEncodeAudio.Name = "chkEncodeAudio";
      chkEncodeAudio.Size = new System.Drawing.Size(93, 16);
      chkEncodeAudio.TabIndex = 19;
      chkEncodeAudio.TabStop = false;
      chkEncodeAudio.Text = "音声エンコード";
      chkEncodeAudio.UseVisualStyleBackColor = true;
      chkEncodeAudio.CheckedChanged += chkEncodeAudio_CheckedChanged;
      // 
      // chkConstantQuality
      // 
      chkConstantQuality.AutoSize = true;
      chkConstantQuality.Location = new System.Drawing.Point(14, 23);
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
      vUnit.Location = new System.Drawing.Point(219, 25);
      vUnit.Name = "vUnit";
      vUnit.Size = new System.Drawing.Size(30, 12);
      vUnit.TabIndex = 1;
      vUnit.Text = "Kbps";
      // 
      // btnApply
      // 
      btnApply.Location = new System.Drawing.Point(706, 10);
      btnApply.Name = "btnApply";
      btnApply.Size = new System.Drawing.Size(113, 40);
      btnApply.TabIndex = 1;
      btnApply.TabStop = false;
      btnApply.Text = "実行コマンド確認\r\n（表示更新）";
      btnApply.UseVisualStyleBackColor = true;
      btnApply.Click += btnApply_Click;
      // 
      // FileList
      // 
      FileList.AllowDrop = true;
      FileList.ContextMenuStrip = FileListMenu;
      FileList.DisplayMember = "Label";
      FileList.Dock = System.Windows.Forms.DockStyle.Bottom;
      FileList.FormattingEnabled = true;
      FileList.HorizontalScrollbar = true;
      FileList.ItemHeight = 15;
      FileList.Location = new System.Drawing.Point(0, 36);
      FileList.Name = "FileList";
      FileList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      FileList.Size = new System.Drawing.Size(419, 94);
      FileList.TabIndex = 29;
      FileList.TabStop = false;
      FileList.ValueMember = "Value";
      FileList.DragDrop += DropArea_DragDrop;
      FileList.DragEnter += DropArea_DragEnter;
      FileList.MouseDoubleClick += DropArea_MouseDoubleClick;
      // 
      // FileListMenu
      // 
      FileListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { FileListMenuItemAddFolder, toolStripSeparator2, FileListMenuItemOpenFolder, toolStripSeparator1, FileListMenuItemDelete, FileListMenuItemClear });
      FileListMenu.Name = "FileListMenu";
      FileListMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      FileListMenu.Size = new System.Drawing.Size(269, 104);
      // 
      // FileListMenuItemAddFolder
      // 
      FileListMenuItemAddFolder.Name = "FileListMenuItemAddFolder";
      FileListMenuItemAddFolder.Size = new System.Drawing.Size(268, 22);
      FileListMenuItemAddFolder.Text = "フォルダを選択";
      FileListMenuItemAddFolder.Click += FileListMenuItemAddFolder_Click;
      // 
      // toolStripSeparator2
      // 
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new System.Drawing.Size(265, 6);
      // 
      // FileListMenuItemOpenFolder
      // 
      FileListMenuItemOpenFolder.Name = "FileListMenuItemOpenFolder";
      FileListMenuItemOpenFolder.Size = new System.Drawing.Size(268, 22);
      FileListMenuItemOpenFolder.Text = "選択済みファイルのフォルダを開く";
      FileListMenuItemOpenFolder.Click += FileListMenuItemOpenFolder_Click;
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new System.Drawing.Size(265, 6);
      // 
      // FileListMenuItemDelete
      // 
      FileListMenuItemDelete.Name = "FileListMenuItemDelete";
      FileListMenuItemDelete.Size = new System.Drawing.Size(268, 22);
      FileListMenuItemDelete.Text = "選択済みのファイルをクリア";
      FileListMenuItemDelete.Click += FileListMenuItemDelete_Click;
      // 
      // FileListMenuItemClear
      // 
      FileListMenuItemClear.Name = "FileListMenuItemClear";
      FileListMenuItemClear.Size = new System.Drawing.Size(268, 22);
      FileListMenuItemClear.Text = "全てのファイルをクリア";
      FileListMenuItemClear.Click += FileListMenuItemClear_Click;
      // 
      // btnStop
      // 
      btnStop.Enabled = false;
      btnStop.Location = new System.Drawing.Point(601, 12);
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
      btnStopAll.Location = new System.Drawing.Point(663, 12);
      btnStopAll.Name = "btnStopAll";
      btnStopAll.Size = new System.Drawing.Size(64, 24);
      btnStopAll.TabIndex = 32;
      btnStopAll.TabStop = false;
      btnStopAll.Text = "全て中止";
      btnStopAll.UseVisualStyleBackColor = true;
      btnStopAll.Click += btnStopAll_Click;
      // 
      // DeInterlaceBox
      // 
      DeInterlaceBox.Controls.Add(cbDeinterlaceAlg);
      DeInterlaceBox.Controls.Add(chkFilterDeInterlace);
      DeInterlaceBox.Location = new System.Drawing.Point(334, 256);
      DeInterlaceBox.Name = "DeInterlaceBox";
      DeInterlaceBox.Size = new System.Drawing.Size(311, 50);
      DeInterlaceBox.TabIndex = 33;
      DeInterlaceBox.TabStop = false;
      DeInterlaceBox.Text = "デインターレース";
      // 
      // cbDevices
      // 
      cbDevices.DisplayMember = "Label";
      cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      cbDevices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      cbDevices.FormattingEnabled = true;
      cbDevices.Location = new System.Drawing.Point(17, 21);
      cbDevices.Name = "cbDevices";
      cbDevices.Size = new System.Drawing.Size(284, 20);
      cbDevices.TabIndex = 0;
      cbDevices.TabStop = false;
      cbDevices.ValueMember = "Value";
      cbDevices.SelectedIndexChanged += cbDevices_SelectedIndexChanged;
      // 
      // InputBox
      // 
      InputBox.Controls.Add(label3);
      InputBox.Controls.Add(FileList);
      InputBox.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      InputBox.Location = new System.Drawing.Point(8, 461);
      InputBox.Name = "InputBox";
      InputBox.Size = new System.Drawing.Size(419, 130);
      InputBox.TabIndex = 34;
      // 
      // btnFFmpeg
      // 
      btnFFmpeg.Font = new System.Drawing.Font("Meiryo UI", 9F);
      btnFFmpeg.Location = new System.Drawing.Point(605, 21);
      btnFFmpeg.Name = "btnFFmpeg";
      btnFFmpeg.Size = new System.Drawing.Size(49, 28);
      btnFFmpeg.TabIndex = 36;
      btnFFmpeg.TabStop = false;
      btnFFmpeg.Text = "参照";
      btnFFmpeg.UseVisualStyleBackColor = true;
      btnFFmpeg.Click += btnFFmpeg_Click;
      // 
      // label8
      // 
      label8.AutoSize = true;
      label8.Location = new System.Drawing.Point(18, 28);
      label8.Name = "label8";
      label8.Size = new System.Drawing.Size(106, 15);
      label8.TabIndex = 37;
      label8.Text = "ffmpeg実行ファイル";
      // 
      // btnFindInPath
      // 
      btnFindInPath.Font = new System.Drawing.Font("Meiryo UI", 9F);
      btnFindInPath.Location = new System.Drawing.Point(655, 21);
      btnFindInPath.Name = "btnFindInPath";
      btnFindInPath.Size = new System.Drawing.Size(144, 28);
      btnFindInPath.TabIndex = 38;
      btnFindInPath.TabStop = false;
      btnFindInPath.Tag = "ffmpeg";
      btnFindInPath.Text = "環境変数PATHから検索";
      btnFindInPath.UseVisualStyleBackColor = true;
      btnFindInPath.Click += FindInPath_Click;
      // 
      // ffmpeg
      // 
      ffmpeg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      ffmpeg.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      ffmpeg.FormattingEnabled = true;
      ffmpeg.Location = new System.Drawing.Point(122, 23);
      ffmpeg.Name = "ffmpeg";
      ffmpeg.Size = new System.Drawing.Size(477, 23);
      ffmpeg.TabIndex = 39;
      ffmpeg.TabStop = false;
      // 
      // StatusBar
      // 
      StatusBar.ContextMenuStrip = StatusBarMenu;
      StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { BatListCount, OutputStderr, DummyProgressBar, QueueCount });
      StatusBar.Location = new System.Drawing.Point(3, 598);
      StatusBar.Name = "StatusBar";
      StatusBar.Size = new System.Drawing.Size(840, 23);
      StatusBar.SizingGrip = false;
      StatusBar.TabIndex = 40;
      StatusBar.Text = "statusStrip1";
      // 
      // StatusBarMenu
      // 
      StatusBarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { StatusBarMenuItemClearQueue });
      StatusBarMenu.Name = "StatusBarMenu";
      StatusBarMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      StatusBarMenu.Size = new System.Drawing.Size(161, 26);
      // 
      // StatusBarMenuItemClearQueue
      // 
      StatusBarMenuItemClearQueue.Name = "StatusBarMenuItemClearQueue";
      StatusBarMenuItemClearQueue.Size = new System.Drawing.Size(160, 22);
      StatusBarMenuItemClearQueue.Text = "キューのクリア";
      StatusBarMenuItemClearQueue.Click += StatusBarMenuItemClearQueue_Click;
      // 
      // BatListCount
      // 
      BatListCount.Name = "BatListCount";
      BatListCount.Size = new System.Drawing.Size(69, 18);
      BatListCount.Text = "Bat List: 0";
      // 
      // OutputStderr
      // 
      OutputStderr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      OutputStderr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      OutputStderr.Name = "OutputStderr";
      OutputStderr.Size = new System.Drawing.Size(634, 18);
      OutputStderr.Spring = true;
      OutputStderr.Text = "stderr";
      OutputStderr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // DummyProgressBar
      // 
      DummyProgressBar.AutoSize = false;
      DummyProgressBar.Name = "DummyProgressBar";
      DummyProgressBar.Size = new System.Drawing.Size(0, 17);
      DummyProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      DummyProgressBar.Visible = false;
      DummyProgressBar.VisibleChanged += DummyProgressBar_VisibleChanged;
      // 
      // QueueCount
      // 
      QueueCount.Name = "QueueCount";
      QueueCount.Size = new System.Drawing.Size(122, 18);
      QueueCount.Text = "Download Queue: 0";
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
      CropBox.Size = new System.Drawing.Size(318, 76);
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
      CropY.TabStop = false;
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
      CropX.TabStop = false;
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
      CropHeight.TabStop = false;
      CropHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // CropWidth
      // 
      CropWidth.Location = new System.Drawing.Point(28, 45);
      CropWidth.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
      CropWidth.Name = "CropWidth";
      CropWidth.Size = new System.Drawing.Size(54, 19);
      CropWidth.TabIndex = 9;
      CropWidth.TabStop = false;
      CropWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // chkCrop
      // 
      chkCrop.AutoSize = true;
      chkCrop.Location = new System.Drawing.Point(14, 22);
      chkCrop.Name = "chkCrop";
      chkCrop.Size = new System.Drawing.Size(67, 16);
      chkCrop.TabIndex = 8;
      chkCrop.TabStop = false;
      chkCrop.Text = "処理する";
      chkCrop.UseVisualStyleBackColor = true;
      chkCrop.CheckedChanged += chkCrop_CheckedChanged;
      // 
      // FileListBindingSource
      // 
      FileListBindingSource.ListChanged += FileListBindingSource_ListChanged;
      // 
      // Tab
      // 
      Tab.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
      Tab.Controls.Add(PageConvert);
      Tab.Controls.Add(PageUtility);
      Tab.Controls.Add(PageDownloader);
      Tab.Controls.Add(PageSetting);
      Tab.Location = new System.Drawing.Point(3, 14);
      Tab.Margin = new System.Windows.Forms.Padding(0);
      Tab.Name = "Tab";
      Tab.Padding = new System.Drawing.Point(0, 0);
      Tab.SelectedIndex = 0;
      Tab.Size = new System.Drawing.Size(840, 450);
      Tab.TabIndex = 42;
      Tab.SelectedIndexChanged += Tab_SelectedIndexChanged;
      // 
      // PageConvert
      // 
      PageConvert.BackColor = System.Drawing.SystemColors.ButtonFace;
      PageConvert.Controls.Add(SubmitButtonBox);
      PageConvert.Controls.Add(groupBox6);
      PageConvert.Controls.Add(OthersBox);
      PageConvert.Controls.Add(CuttingBox);
      PageConvert.Controls.Add(CropBox);
      PageConvert.Controls.Add(BitrateBox);
      PageConvert.Controls.Add(ResizeBox);
      PageConvert.Controls.Add(CodecBox);
      PageConvert.Controls.Add(RotateBox);
      PageConvert.Controls.Add(btnClear);
      PageConvert.Controls.Add(DeInterlaceBox);
      PageConvert.Controls.Add(btnApply);
      PageConvert.Controls.Add(Commandlines);
      PageConvert.Location = new System.Drawing.Point(4, 22);
      PageConvert.Margin = new System.Windows.Forms.Padding(0);
      PageConvert.Name = "PageConvert";
      PageConvert.Size = new System.Drawing.Size(832, 424);
      PageConvert.TabIndex = 0;
      PageConvert.Text = "動画変換";
      // 
      // SubmitButtonBox
      // 
      SubmitButtonBox.Controls.Add(btnSubmitBatExecute);
      SubmitButtonBox.Controls.Add(btnSubmitBatchClear);
      SubmitButtonBox.Controls.Add(btnSubmitSaveToFile);
      SubmitButtonBox.Controls.Add(btnSubmitAddToBatch);
      SubmitButtonBox.Controls.Add(btnSubmitInvoke);
      SubmitButtonBox.Controls.Add(btnStop);
      SubmitButtonBox.Controls.Add(OpenLogFile);
      SubmitButtonBox.Controls.Add(btnStopAll);
      SubmitButtonBox.Dock = System.Windows.Forms.DockStyle.Bottom;
      SubmitButtonBox.Location = new System.Drawing.Point(0, 378);
      SubmitButtonBox.Name = "SubmitButtonBox";
      SubmitButtonBox.Size = new System.Drawing.Size(832, 46);
      SubmitButtonBox.TabIndex = 44;
      // 
      // btnSubmitBatExecute
      // 
      btnSubmitBatExecute.Enabled = false;
      btnSubmitBatExecute.Location = new System.Drawing.Point(11, 12);
      btnSubmitBatExecute.Name = "btnSubmitBatExecute";
      btnSubmitBatExecute.Size = new System.Drawing.Size(75, 24);
      btnSubmitBatExecute.TabIndex = 35;
      btnSubmitBatExecute.Text = "バッチ実行";
      btnSubmitBatExecute.UseVisualStyleBackColor = true;
      btnSubmitBatExecute.Click += btnSubmitBatExecute_Click;
      // 
      // btnSubmitBatchClear
      // 
      btnSubmitBatchClear.Enabled = false;
      btnSubmitBatchClear.Location = new System.Drawing.Point(309, 12);
      btnSubmitBatchClear.Name = "btnSubmitBatchClear";
      btnSubmitBatchClear.Size = new System.Drawing.Size(84, 24);
      btnSubmitBatchClear.TabIndex = 34;
      btnSubmitBatchClear.Text = "バッチクリア";
      btnSubmitBatchClear.UseVisualStyleBackColor = true;
      btnSubmitBatchClear.Click += btnSubmitBatchClear_Click;
      // 
      // btnSubmitSaveToFile
      // 
      btnSubmitSaveToFile.Enabled = false;
      btnSubmitSaveToFile.Location = new System.Drawing.Point(92, 12);
      btnSubmitSaveToFile.Name = "btnSubmitSaveToFile";
      btnSubmitSaveToFile.Size = new System.Drawing.Size(112, 24);
      btnSubmitSaveToFile.TabIndex = 33;
      btnSubmitSaveToFile.Text = "バッチファイル保存";
      btnSubmitSaveToFile.UseVisualStyleBackColor = true;
      btnSubmitSaveToFile.Click += btnSubmitSaveToFile_Click;
      // 
      // groupBox6
      // 
      groupBox6.Controls.Add(FreeOptions);
      groupBox6.Location = new System.Drawing.Point(334, 312);
      groupBox6.Name = "groupBox6";
      groupBox6.Size = new System.Drawing.Size(311, 54);
      groupBox6.TabIndex = 43;
      groupBox6.TabStop = false;
      groupBox6.Text = "追加オプション";
      // 
      // FreeOptions
      // 
      FreeOptions.HideSelection = false;
      FreeOptions.Location = new System.Drawing.Point(16, 22);
      FreeOptions.Name = "FreeOptions";
      FreeOptions.PlaceholderText = "カンマ、セミコロン、コロンで区切ってください";
      FreeOptions.Size = new System.Drawing.Size(281, 19);
      FreeOptions.TabIndex = 0;
      // 
      // OthersBox
      // 
      OthersBox.Controls.Add(cbDevices);
      OthersBox.Location = new System.Drawing.Point(11, 312);
      OthersBox.Name = "OthersBox";
      OthersBox.Size = new System.Drawing.Size(317, 54);
      OthersBox.TabIndex = 42;
      OthersBox.TabStop = false;
      OthersBox.Text = "GPU";
      // 
      // PageUtility
      // 
      PageUtility.BackColor = System.Drawing.SystemColors.ButtonFace;
      PageUtility.Controls.Add(groupBox7);
      PageUtility.Controls.Add(CommonButtonBox);
      PageUtility.Controls.Add(groupBox8);
      PageUtility.Controls.Add(Image2Box);
      PageUtility.Location = new System.Drawing.Point(4, 27);
      PageUtility.Name = "PageUtility";
      PageUtility.Padding = new System.Windows.Forms.Padding(3);
      PageUtility.Size = new System.Drawing.Size(832, 419);
      PageUtility.TabIndex = 1;
      PageUtility.Text = "ユーティリティ";
      // 
      // groupBox7
      // 
      groupBox7.Controls.Add(DecoderHelpList);
      groupBox7.Controls.Add(EncoderHelpList);
      groupBox7.Controls.Add(OpenEncoderHelp);
      groupBox7.Controls.Add(OpenDecoderHelp);
      groupBox7.Location = new System.Drawing.Point(12, 291);
      groupBox7.Name = "groupBox7";
      groupBox7.Size = new System.Drawing.Size(805, 70);
      groupBox7.TabIndex = 28;
      groupBox7.TabStop = false;
      groupBox7.Text = "その他";
      // 
      // DecoderHelpList
      // 
      DecoderHelpList.DisplayMember = "Label";
      DecoderHelpList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      DecoderHelpList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      DecoderHelpList.FormattingEnabled = true;
      DecoderHelpList.Location = new System.Drawing.Point(533, 34);
      DecoderHelpList.Name = "DecoderHelpList";
      DecoderHelpList.Size = new System.Drawing.Size(145, 20);
      DecoderHelpList.TabIndex = 39;
      DecoderHelpList.ValueMember = "Value";
      // 
      // EncoderHelpList
      // 
      EncoderHelpList.DisplayMember = "Label";
      EncoderHelpList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      EncoderHelpList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      EncoderHelpList.FormattingEnabled = true;
      EncoderHelpList.Location = new System.Drawing.Point(178, 34);
      EncoderHelpList.Name = "EncoderHelpList";
      EncoderHelpList.Size = new System.Drawing.Size(145, 20);
      EncoderHelpList.TabIndex = 39;
      EncoderHelpList.ValueMember = "Value";
      // 
      // OpenEncoderHelp
      // 
      OpenEncoderHelp.Location = new System.Drawing.Point(20, 33);
      OpenEncoderHelp.Name = "OpenEncoderHelp";
      OpenEncoderHelp.Size = new System.Drawing.Size(152, 23);
      OpenEncoderHelp.TabIndex = 37;
      OpenEncoderHelp.TabStop = false;
      OpenEncoderHelp.Text = "エンコーダーオプションのヘルプ";
      OpenEncoderHelp.UseVisualStyleBackColor = true;
      OpenEncoderHelp.Click += OpenEncoderHelp_Click;
      // 
      // OpenDecoderHelp
      // 
      OpenDecoderHelp.Location = new System.Drawing.Point(373, 33);
      OpenDecoderHelp.Name = "OpenDecoderHelp";
      OpenDecoderHelp.Size = new System.Drawing.Size(154, 23);
      OpenDecoderHelp.TabIndex = 38;
      OpenDecoderHelp.TabStop = false;
      OpenDecoderHelp.Text = "デコーダーオプションのヘルプ";
      OpenDecoderHelp.UseVisualStyleBackColor = true;
      OpenDecoderHelp.Click += OpenDecoderHelp_Click;
      // 
      // CommonButtonBox
      // 
      CommonButtonBox.Controls.Add(btnStopUtil);
      CommonButtonBox.Controls.Add(btnStopAllUtil);
      CommonButtonBox.Controls.Add(button2);
      CommonButtonBox.Dock = System.Windows.Forms.DockStyle.Bottom;
      CommonButtonBox.Location = new System.Drawing.Point(3, 386);
      CommonButtonBox.Name = "CommonButtonBox";
      CommonButtonBox.Size = new System.Drawing.Size(826, 30);
      CommonButtonBox.TabIndex = 27;
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
      groupBox8.Location = new System.Drawing.Point(12, 165);
      groupBox8.Name = "groupBox8";
      groupBox8.Size = new System.Drawing.Size(805, 120);
      groupBox8.TabIndex = 2;
      groupBox8.TabStop = false;
      groupBox8.Text = "ツール";
      // 
      // label22
      // 
      label22.AutoSize = true;
      label22.Location = new System.Drawing.Point(183, 78);
      label22.Name = "label22";
      label22.Size = new System.Drawing.Size(243, 12);
      label22.TabIndex = 1;
      label22.Text = "エンコーダーをCOPYにしてコンテナを再生成します。";
      // 
      // label21
      // 
      label21.AutoSize = true;
      label21.Location = new System.Drawing.Point(183, 38);
      label21.Name = "label21";
      label21.Size = new System.Drawing.Size(349, 12);
      label21.TabIndex = 1;
      label21.Text = "入力ファイルを順番に連結します。異なるサイズの動画は結合できません。";
      // 
      // SubmitCopy
      // 
      SubmitCopy.Location = new System.Drawing.Point(13, 67);
      SubmitCopy.Name = "SubmitCopy";
      SubmitCopy.Size = new System.Drawing.Size(154, 32);
      SubmitCopy.TabIndex = 0;
      SubmitCopy.Text = "ファイルコンテナ変更";
      SubmitCopy.UseVisualStyleBackColor = true;
      SubmitCopy.Click += SubmitCopy_Click;
      // 
      // SubmitConcat
      // 
      SubmitConcat.Location = new System.Drawing.Point(13, 28);
      SubmitConcat.Name = "SubmitConcat";
      SubmitConcat.Size = new System.Drawing.Size(154, 32);
      SubmitConcat.TabIndex = 0;
      SubmitConcat.Text = "ファイル結合";
      SubmitConcat.UseVisualStyleBackColor = true;
      SubmitConcat.Click += SubmitConcat_Click;
      // 
      // Image2Box
      // 
      Image2Box.Controls.Add(ImageFreeOptions);
      Image2Box.Controls.Add(label11);
      Image2Box.Controls.Add(useTiledImage);
      Image2Box.Controls.Add(TileRows);
      Image2Box.Controls.Add(CropTB);
      Image2Box.Controls.Add(ImageWidth);
      Image2Box.Controls.Add(TileColumns);
      Image2Box.Controls.Add(CropLR);
      Image2Box.Controls.Add(ImageHeight);
      Image2Box.Controls.Add(label18);
      Image2Box.Controls.Add(label31);
      Image2Box.Controls.Add(label20);
      Image2Box.Controls.Add(label17);
      Image2Box.Controls.Add(FrameRate);
      Image2Box.Controls.Add(ImageType);
      Image2Box.Controls.Add(linkLabel2);
      Image2Box.Controls.Add(ImageTo);
      Image2Box.Controls.Add(ImageSS);
      Image2Box.Controls.Add(label33);
      Image2Box.Controls.Add(label26);
      Image2Box.Controls.Add(label16);
      Image2Box.Controls.Add(label25);
      Image2Box.Controls.Add(label9);
      Image2Box.Controls.Add(label19);
      Image2Box.Controls.Add(label15);
      Image2Box.Controls.Add(label14);
      Image2Box.Controls.Add(SubmitThumbnail);
      Image2Box.Location = new System.Drawing.Point(12, 25);
      Image2Box.Name = "Image2Box";
      Image2Box.Size = new System.Drawing.Size(805, 134);
      Image2Box.TabIndex = 1;
      Image2Box.TabStop = false;
      Image2Box.Text = "画像生成                                   ";
      // 
      // ImageFreeOptions
      // 
      ImageFreeOptions.Location = new System.Drawing.Point(90, 91);
      ImageFreeOptions.Name = "ImageFreeOptions";
      ImageFreeOptions.PlaceholderText = "カンマ、セミコロン、コロンで区切ってください";
      ImageFreeOptions.Size = new System.Drawing.Size(268, 19);
      ImageFreeOptions.TabIndex = 11;
      // 
      // label11
      // 
      label11.AutoSize = true;
      label11.Location = new System.Drawing.Point(13, 94);
      label11.Name = "label11";
      label11.Size = new System.Drawing.Size(72, 12);
      label11.TabIndex = 10;
      label11.Text = "追加オプション";
      // 
      // useTiledImage
      // 
      useTiledImage.AutoSize = true;
      useTiledImage.Location = new System.Drawing.Point(382, 92);
      useTiledImage.Name = "useTiledImage";
      useTiledImage.Size = new System.Drawing.Size(97, 16);
      useTiledImage.TabIndex = 9;
      useTiledImage.Text = "タイル(列 x 行)";
      useTiledImage.UseVisualStyleBackColor = true;
      useTiledImage.CheckedChanged += useTiledImage_CheckedChanged;
      // 
      // TileRows
      // 
      TileRows.Enabled = false;
      TileRows.Location = new System.Drawing.Point(567, 91);
      TileRows.Name = "TileRows";
      TileRows.Size = new System.Drawing.Size(64, 19);
      TileRows.TabIndex = 8;
      TileRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // CropTB
      // 
      CropTB.Location = new System.Drawing.Point(357, 52);
      CropTB.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      CropTB.Name = "CropTB";
      CropTB.Size = new System.Drawing.Size(64, 19);
      CropTB.TabIndex = 8;
      CropTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // ImageWidth
      // 
      ImageWidth.Location = new System.Drawing.Point(486, 53);
      ImageWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      ImageWidth.Name = "ImageWidth";
      ImageWidth.Size = new System.Drawing.Size(64, 19);
      ImageWidth.TabIndex = 8;
      ImageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // TileColumns
      // 
      TileColumns.Enabled = false;
      TileColumns.Location = new System.Drawing.Point(481, 91);
      TileColumns.Name = "TileColumns";
      TileColumns.Size = new System.Drawing.Size(64, 19);
      TileColumns.TabIndex = 8;
      TileColumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      TileColumns.Value = new decimal(new int[] { 6, 0, 0, 0 });
      // 
      // CropLR
      // 
      CropLR.Location = new System.Drawing.Point(271, 52);
      CropLR.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      CropLR.Name = "CropLR";
      CropLR.Size = new System.Drawing.Size(64, 19);
      CropLR.TabIndex = 8;
      CropLR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // ImageHeight
      // 
      ImageHeight.Location = new System.Drawing.Point(567, 53);
      ImageHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      ImageHeight.Name = "ImageHeight";
      ImageHeight.Size = new System.Drawing.Size(64, 19);
      ImageHeight.TabIndex = 8;
      ImageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label18
      // 
      label18.AutoSize = true;
      label18.Location = new System.Drawing.Point(695, 34);
      label18.Name = "label18";
      label18.Size = new System.Drawing.Size(77, 12);
      label18.TabIndex = 7;
      label18.Text = "出力画像形式";
      // 
      // label31
      // 
      label31.AutoSize = true;
      label31.Location = new System.Drawing.Point(427, 56);
      label31.Name = "label31";
      label31.Size = new System.Drawing.Size(42, 12);
      label31.TabIndex = 7;
      label31.Text = "ピクセル";
      // 
      // label20
      // 
      label20.AutoSize = true;
      label20.Location = new System.Drawing.Point(637, 56);
      label20.Name = "label20";
      label20.Size = new System.Drawing.Size(42, 12);
      label20.TabIndex = 7;
      label20.Text = "ピクセル";
      // 
      // label17
      // 
      label17.AutoSize = true;
      label17.Location = new System.Drawing.Point(228, 56);
      label17.Name = "label17";
      label17.Size = new System.Drawing.Size(29, 12);
      label17.TabIndex = 7;
      label17.Text = "秒毎";
      // 
      // FrameRate
      // 
      FrameRate.Location = new System.Drawing.Point(157, 53);
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
      ImageType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      ImageType.FormattingEnabled = true;
      ImageType.Location = new System.Drawing.Point(695, 52);
      ImageType.Name = "ImageType";
      ImageType.Size = new System.Drawing.Size(96, 20);
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
      ImageTo.Location = new System.Drawing.Point(90, 53);
      ImageTo.Name = "ImageTo";
      ImageTo.Size = new System.Drawing.Size(53, 19);
      ImageTo.TabIndex = 2;
      ImageTo.Validating += TimeFormatValidating;
      // 
      // ImageSS
      // 
      ImageSS.Location = new System.Drawing.Point(11, 53);
      ImageSS.Name = "ImageSS";
      ImageSS.Size = new System.Drawing.Size(55, 19);
      ImageSS.TabIndex = 2;
      ImageSS.Validating += TimeFormatValidating;
      // 
      // label33
      // 
      label33.AutoSize = true;
      label33.Location = new System.Drawing.Point(551, 94);
      label33.Name = "label33";
      label33.Size = new System.Drawing.Size(17, 12);
      label33.TabIndex = 1;
      label33.Text = "×";
      label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label26
      // 
      label26.AutoSize = true;
      label26.Location = new System.Drawing.Point(341, 56);
      label26.Name = "label26";
      label26.Size = new System.Drawing.Size(17, 12);
      label26.TabIndex = 1;
      label26.Text = "×";
      label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label16
      // 
      label16.AutoSize = true;
      label16.Location = new System.Drawing.Point(68, 56);
      label16.Name = "label16";
      label16.Size = new System.Drawing.Size(17, 12);
      label16.TabIndex = 1;
      label16.Text = "〜";
      // 
      // label25
      // 
      label25.AutoSize = true;
      label25.Location = new System.Drawing.Point(271, 34);
      label25.Name = "label25";
      label25.Size = new System.Drawing.Size(89, 12);
      label25.TabIndex = 1;
      label25.Text = "クロップ(L&R x T&B)";
      // 
      // label9
      // 
      label9.AutoSize = true;
      label9.Location = new System.Drawing.Point(551, 56);
      label9.Name = "label9";
      label9.Size = new System.Drawing.Size(17, 12);
      label9.TabIndex = 1;
      label9.Text = "×";
      label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label19
      // 
      label19.AutoSize = true;
      label19.Location = new System.Drawing.Point(483, 34);
      label19.Name = "label19";
      label19.Size = new System.Drawing.Size(96, 12);
      label19.TabIndex = 1;
      label19.Text = "画像リサイズ(WxH)";
      // 
      // label15
      // 
      label15.AutoSize = true;
      label15.Location = new System.Drawing.Point(90, 34);
      label15.Name = "label15";
      label15.Size = new System.Drawing.Size(53, 12);
      label15.TabIndex = 1;
      label15.Text = "開始位置";
      // 
      // label14
      // 
      label14.AutoSize = true;
      label14.Location = new System.Drawing.Point(13, 34);
      label14.Name = "label14";
      label14.Size = new System.Drawing.Size(53, 12);
      label14.TabIndex = 1;
      label14.Text = "開始位置";
      // 
      // SubmitThumbnail
      // 
      SubmitThumbnail.Location = new System.Drawing.Point(644, 89);
      SubmitThumbnail.Name = "SubmitThumbnail";
      SubmitThumbnail.Size = new System.Drawing.Size(149, 23);
      SubmitThumbnail.TabIndex = 0;
      SubmitThumbnail.Text = "画像生成";
      SubmitThumbnail.UseVisualStyleBackColor = true;
      SubmitThumbnail.Click += SubmitThumbnail_Click;
      // 
      // PageDownloader
      // 
      PageDownloader.BackColor = System.Drawing.SystemColors.ButtonFace;
      PageDownloader.Controls.Add(AddDownloadQueue);
      PageDownloader.Controls.Add(SubmitDownloadDequeue);
      PageDownloader.Controls.Add(PlaylistGroup);
      PageDownloader.Controls.Add(linkLabel5);
      PageDownloader.Controls.Add(MediaTitle);
      PageDownloader.Controls.Add(linkLabel3);
      PageDownloader.Controls.Add(OutputFileFormat);
      PageDownloader.Controls.Add(DownloadUrl);
      PageDownloader.Controls.Add(DurationTime);
      PageDownloader.Controls.Add(chkAfterDownload);
      PageDownloader.Controls.Add(StopDownload);
      PageDownloader.Controls.Add(groupBox1);
      PageDownloader.Controls.Add(LinkYdlOutputTemplate);
      PageDownloader.Controls.Add(ThumbnailBox);
      PageDownloader.Controls.Add(label28);
      PageDownloader.Controls.Add(SubmitConfirmFormat);
      PageDownloader.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      PageDownloader.Location = new System.Drawing.Point(4, 27);
      PageDownloader.Name = "PageDownloader";
      PageDownloader.Padding = new System.Windows.Forms.Padding(3);
      PageDownloader.Size = new System.Drawing.Size(832, 419);
      PageDownloader.TabIndex = 2;
      PageDownloader.Text = "ダウンロード";
      // 
      // AddDownloadQueue
      // 
      AddDownloadQueue.Enabled = false;
      AddDownloadQueue.Font = new System.Drawing.Font("Meiryo UI", 9F);
      AddDownloadQueue.Location = new System.Drawing.Point(696, 332);
      AddDownloadQueue.Name = "AddDownloadQueue";
      AddDownloadQueue.Size = new System.Drawing.Size(130, 26);
      AddDownloadQueue.TabIndex = 6;
      AddDownloadQueue.Tag = false;
      AddDownloadQueue.Text = "ダウンロードキューに追加";
      AddDownloadQueue.UseVisualStyleBackColor = true;
      AddDownloadQueue.Click += AddDownloadQueue_Click;
      // 
      // SubmitDownloadDequeue
      // 
      SubmitDownloadDequeue.Enabled = false;
      SubmitDownloadDequeue.Font = new System.Drawing.Font("Meiryo UI", 9F);
      SubmitDownloadDequeue.Location = new System.Drawing.Point(609, 333);
      SubmitDownloadDequeue.Name = "SubmitDownloadDequeue";
      SubmitDownloadDequeue.Size = new System.Drawing.Size(75, 26);
      SubmitDownloadDequeue.TabIndex = 6;
      SubmitDownloadDequeue.Tag = true;
      SubmitDownloadDequeue.Text = "キュー開始";
      SubmitDownloadDequeue.UseVisualStyleBackColor = true;
      SubmitDownloadDequeue.Click += BeginDequeue_Click;
      // 
      // PlaylistGroup
      // 
      PlaylistGroup.Controls.Add(DownloadBestQuality);
      PlaylistGroup.Controls.Add(DownloadWorstQuality);
      PlaylistGroup.Controls.Add(PlayListDownloadAll);
      PlaylistGroup.Controls.Add(Playlist);
      PlaylistGroup.Enabled = false;
      PlaylistGroup.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      PlaylistGroup.Location = new System.Drawing.Point(441, 54);
      PlaylistGroup.Name = "PlaylistGroup";
      PlaylistGroup.Size = new System.Drawing.Size(388, 157);
      PlaylistGroup.TabIndex = 48;
      PlaylistGroup.TabStop = false;
      PlaylistGroup.Text = "プレイリスト検出";
      // 
      // DownloadBestQuality
      // 
      DownloadBestQuality.AutoSize = true;
      DownloadBestQuality.Checked = true;
      DownloadBestQuality.Location = new System.Drawing.Point(14, 123);
      DownloadBestQuality.Name = "DownloadBestQuality";
      DownloadBestQuality.Size = new System.Drawing.Size(99, 19);
      DownloadBestQuality.TabIndex = 50;
      DownloadBestQuality.TabStop = true;
      DownloadBestQuality.Tag = "bv*+ba/b";
      DownloadBestQuality.Text = "最高品質でDL";
      DownloadBestQuality.UseVisualStyleBackColor = true;
      // 
      // DownloadWorstQuality
      // 
      DownloadWorstQuality.AutoSize = true;
      DownloadWorstQuality.Location = new System.Drawing.Point(119, 123);
      DownloadWorstQuality.Name = "DownloadWorstQuality";
      DownloadWorstQuality.Size = new System.Drawing.Size(99, 19);
      DownloadWorstQuality.TabIndex = 49;
      DownloadWorstQuality.Tag = "wv*+wa/w";
      DownloadWorstQuality.Text = "最低品質でDL";
      DownloadWorstQuality.UseVisualStyleBackColor = true;
      // 
      // PlayListDownloadAll
      // 
      PlayListDownloadAll.Location = new System.Drawing.Point(223, 120);
      PlayListDownloadAll.Name = "PlayListDownloadAll";
      PlayListDownloadAll.Size = new System.Drawing.Size(154, 26);
      PlayListDownloadAll.TabIndex = 48;
      PlayListDownloadAll.Text = "全てダウンロードキューに追加";
      PlayListDownloadAll.UseVisualStyleBackColor = true;
      PlayListDownloadAll.Click += PlayListDownloadAll_Click;
      // 
      // Playlist
      // 
      Playlist.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      Playlist.HorizontalScrollbar = true;
      Playlist.ItemHeight = 14;
      Playlist.Location = new System.Drawing.Point(14, 23);
      Playlist.Name = "Playlist";
      Playlist.Size = new System.Drawing.Size(363, 88);
      Playlist.TabIndex = 47;
      Playlist.SelectedIndexChanged += Playlist_SelectedIndexChanged;
      // 
      // linkLabel5
      // 
      linkLabel5.AutoSize = true;
      linkLabel5.Location = new System.Drawing.Point(12, 22);
      linkLabel5.Name = "linkLabel5";
      linkLabel5.Size = new System.Drawing.Size(79, 15);
      linkLabel5.TabIndex = 45;
      linkLabel5.TabStop = true;
      linkLabel5.Tag = "DownloadUrl";
      linkLabel5.Text = "URLリスト編集";
      linkLabel5.LinkClicked += EditListItems;
      // 
      // MediaTitle
      // 
      MediaTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
      MediaTitle.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      MediaTitle.ImeMode = System.Windows.Forms.ImeMode.Disable;
      MediaTitle.Location = new System.Drawing.Point(4, 67);
      MediaTitle.Multiline = true;
      MediaTitle.Name = "MediaTitle";
      MediaTitle.PlaceholderText = "メディアタイトル";
      MediaTitle.ReadOnly = true;
      MediaTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      MediaTitle.Size = new System.Drawing.Size(426, 95);
      MediaTitle.TabIndex = 0;
      MediaTitle.TabStop = false;
      // 
      // linkLabel3
      // 
      linkLabel3.AutoSize = true;
      linkLabel3.Location = new System.Drawing.Point(576, 367);
      linkLabel3.Name = "linkLabel3";
      linkLabel3.Size = new System.Drawing.Size(55, 15);
      linkLabel3.TabIndex = 44;
      linkLabel3.TabStop = true;
      linkLabel3.Tag = "OutputFileFormat";
      linkLabel3.Text = "リスト編集";
      linkLabel3.LinkClicked += EditListItems;
      // 
      // OutputFileFormat
      // 
      OutputFileFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      OutputFileFormat.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      OutputFileFormat.FormattingEnabled = true;
      OutputFileFormat.Items.AddRange(new object[] { "%(title)s-%(id)s.%(ext)s" });
      OutputFileFormat.Location = new System.Drawing.Point(441, 387);
      OutputFileFormat.Name = "OutputFileFormat";
      OutputFileFormat.Size = new System.Drawing.Size(385, 28);
      OutputFileFormat.TabIndex = 43;
      // 
      // DownloadUrl
      // 
      DownloadUrl.DisplayMember = "Item1";
      DownloadUrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      DownloadUrl.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      DownloadUrl.FormattingEnabled = true;
      DownloadUrl.Location = new System.Drawing.Point(97, 16);
      DownloadUrl.Name = "DownloadUrl";
      DownloadUrl.Size = new System.Drawing.Size(619, 28);
      DownloadUrl.TabIndex = 42;
      DownloadUrl.ValueMember = "Item2";
      DownloadUrl.SelectedIndexChanged += DownloadUrl_SelectedIndexChanged;
      DownloadUrl.Leave += DownloadUrl_Leave;
      // 
      // DurationTime
      // 
      DurationTime.BackColor = System.Drawing.Color.White;
      DurationTime.Location = new System.Drawing.Point(323, 386);
      DurationTime.Name = "DurationTime";
      DurationTime.Padding = new System.Windows.Forms.Padding(5);
      DurationTime.Size = new System.Drawing.Size(107, 30);
      DurationTime.TabIndex = 41;
      DurationTime.Text = "00:00:00.0000";
      DurationTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      DurationTime.Visible = false;
      // 
      // chkAfterDownload
      // 
      chkAfterDownload.AutoSize = true;
      chkAfterDownload.Location = new System.Drawing.Point(441, 337);
      chkAfterDownload.Name = "chkAfterDownload";
      chkAfterDownload.Size = new System.Drawing.Size(107, 19);
      chkAfterDownload.TabIndex = 40;
      chkAfterDownload.Text = "入力リストに追加";
      chkAfterDownload.UseVisualStyleBackColor = true;
      // 
      // StopDownload
      // 
      StopDownload.Enabled = false;
      StopDownload.Font = new System.Drawing.Font("Meiryo UI", 9F);
      StopDownload.Location = new System.Drawing.Point(560, 333);
      StopDownload.Name = "StopDownload";
      StopDownload.Size = new System.Drawing.Size(48, 26);
      StopDownload.TabIndex = 11;
      StopDownload.Text = "中止";
      StopDownload.UseVisualStyleBackColor = true;
      StopDownload.Click += StopDownload_Click;
      // 
      // groupBox1
      // 
      groupBox1.Controls.Add(FmtSeparated);
      groupBox1.Controls.Add(FmtWhole);
      groupBox1.Controls.Add(MovieFormat);
      groupBox1.Controls.Add(label23);
      groupBox1.Controls.Add(label34);
      groupBox1.Controls.Add(label30);
      groupBox1.Controls.Add(VideoOnlyFormat);
      groupBox1.Controls.Add(AudioOnlyFormat);
      groupBox1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      groupBox1.Location = new System.Drawing.Point(441, 217);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new System.Drawing.Size(385, 110);
      groupBox1.TabIndex = 37;
      groupBox1.TabStop = false;
      groupBox1.Text = "メディア別";
      // 
      // FmtSeparated
      // 
      FmtSeparated.AutoSize = true;
      FmtSeparated.Checked = true;
      FmtSeparated.Location = new System.Drawing.Point(14, 35);
      FmtSeparated.Name = "FmtSeparated";
      FmtSeparated.Size = new System.Drawing.Size(14, 13);
      FmtSeparated.TabIndex = 11;
      FmtSeparated.TabStop = true;
      FmtSeparated.UseVisualStyleBackColor = true;
      // 
      // FmtWhole
      // 
      FmtWhole.AutoSize = true;
      FmtWhole.Location = new System.Drawing.Point(14, 82);
      FmtWhole.Name = "FmtWhole";
      FmtWhole.Size = new System.Drawing.Size(14, 13);
      FmtWhole.TabIndex = 11;
      FmtWhole.TabStop = true;
      FmtWhole.UseVisualStyleBackColor = true;
      // 
      // MovieFormat
      // 
      MovieFormat.DisplayMember = "Label";
      MovieFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      MovieFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      MovieFormat.FormattingEnabled = true;
      MovieFormat.Location = new System.Drawing.Point(73, 77);
      MovieFormat.Name = "MovieFormat";
      MovieFormat.Size = new System.Drawing.Size(304, 23);
      MovieFormat.TabIndex = 10;
      MovieFormat.ValueMember = "Value";
      MovieFormat.Click += Format_Click;
      // 
      // label23
      // 
      label23.AutoSize = true;
      label23.Location = new System.Drawing.Point(40, 23);
      label23.Name = "label23";
      label23.Size = new System.Drawing.Size(31, 15);
      label23.TabIndex = 5;
      label23.Text = "動画";
      // 
      // label34
      // 
      label34.AutoSize = true;
      label34.Location = new System.Drawing.Point(40, 81);
      label34.Name = "label34";
      label34.Size = new System.Drawing.Size(31, 15);
      label34.TabIndex = 5;
      label34.Text = "完全";
      // 
      // label30
      // 
      label30.AutoSize = true;
      label30.Location = new System.Drawing.Point(40, 49);
      label30.Name = "label30";
      label30.Size = new System.Drawing.Size(31, 15);
      label30.TabIndex = 5;
      label30.Text = "音声";
      // 
      // VideoOnlyFormat
      // 
      VideoOnlyFormat.DisplayMember = "Label";
      VideoOnlyFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      VideoOnlyFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      VideoOnlyFormat.FormattingEnabled = true;
      VideoOnlyFormat.Location = new System.Drawing.Point(73, 19);
      VideoOnlyFormat.Name = "VideoOnlyFormat";
      VideoOnlyFormat.Size = new System.Drawing.Size(304, 23);
      VideoOnlyFormat.TabIndex = 10;
      VideoOnlyFormat.ValueMember = "Value";
      VideoOnlyFormat.Click += Format_Click;
      // 
      // AudioOnlyFormat
      // 
      AudioOnlyFormat.DisplayMember = "Label";
      AudioOnlyFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      AudioOnlyFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      AudioOnlyFormat.FormattingEnabled = true;
      AudioOnlyFormat.Location = new System.Drawing.Point(73, 45);
      AudioOnlyFormat.Name = "AudioOnlyFormat";
      AudioOnlyFormat.Size = new System.Drawing.Size(304, 23);
      AudioOnlyFormat.TabIndex = 10;
      AudioOnlyFormat.ValueMember = "Value";
      AudioOnlyFormat.Click += Format_Click;
      // 
      // LinkYdlOutputTemplate
      // 
      LinkYdlOutputTemplate.AutoSize = true;
      LinkYdlOutputTemplate.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
      LinkYdlOutputTemplate.Location = new System.Drawing.Point(664, 367);
      LinkYdlOutputTemplate.Name = "LinkYdlOutputTemplate";
      LinkYdlOutputTemplate.Size = new System.Drawing.Size(134, 15);
      LinkYdlOutputTemplate.TabIndex = 14;
      LinkYdlOutputTemplate.TabStop = true;
      LinkYdlOutputTemplate.Text = "※OUTPUT TEMPLATE";
      LinkYdlOutputTemplate.LinkClicked += LinkYdlOutputTemplate_LinkClicked;
      // 
      // ThumbnailBox
      // 
      ThumbnailBox.BackColor = System.Drawing.Color.Black;
      ThumbnailBox.InitialImage = null;
      ThumbnailBox.Location = new System.Drawing.Point(2, 175);
      ThumbnailBox.Name = "ThumbnailBox";
      ThumbnailBox.Size = new System.Drawing.Size(428, 241);
      ThumbnailBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      ThumbnailBox.TabIndex = 9;
      ThumbnailBox.TabStop = false;
      // 
      // label28
      // 
      label28.AutoSize = true;
      label28.Location = new System.Drawing.Point(441, 367);
      label28.Name = "label28";
      label28.Size = new System.Drawing.Size(131, 15);
      label28.TabIndex = 5;
      label28.Text = "出力ファイル名テンプレート";
      // 
      // SubmitConfirmFormat
      // 
      SubmitConfirmFormat.Location = new System.Drawing.Point(727, 13);
      SubmitConfirmFormat.Name = "SubmitConfirmFormat";
      SubmitConfirmFormat.Size = new System.Drawing.Size(92, 31);
      SubmitConfirmFormat.TabIndex = 6;
      SubmitConfirmFormat.Text = "URL解析";
      SubmitConfirmFormat.UseVisualStyleBackColor = true;
      SubmitConfirmFormat.Click += SubmitParseUrl_Click;
      // 
      // PageSetting
      // 
      PageSetting.BackColor = System.Drawing.SystemColors.ButtonFace;
      PageSetting.Controls.Add(groupBox4);
      PageSetting.Controls.Add(groupBox3);
      PageSetting.Controls.Add(groupBox2);
      PageSetting.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      PageSetting.Location = new System.Drawing.Point(4, 27);
      PageSetting.Name = "PageSetting";
      PageSetting.Padding = new System.Windows.Forms.Padding(3);
      PageSetting.Size = new System.Drawing.Size(832, 419);
      PageSetting.TabIndex = 3;
      PageSetting.Text = "設定";
      // 
      // groupBox4
      // 
      groupBox4.Controls.Add(LoadFromJson);
      groupBox4.Controls.Add(SaveToJson);
      groupBox4.Controls.Add(label35);
      groupBox4.Controls.Add(MaxListItems);
      groupBox4.Controls.Add(CommandInvoker);
      groupBox4.Controls.Add(Overwrite);
      groupBox4.Controls.Add(ClearLists);
      groupBox4.Controls.Add(IsOpenStderr);
      groupBox4.Location = new System.Drawing.Point(11, 319);
      groupBox4.Name = "groupBox4";
      groupBox4.Size = new System.Drawing.Size(812, 99);
      groupBox4.TabIndex = 43;
      groupBox4.TabStop = false;
      groupBox4.Text = "共通・全般";
      // 
      // LoadFromJson
      // 
      LoadFromJson.Location = new System.Drawing.Point(436, 23);
      LoadFromJson.Name = "LoadFromJson";
      LoadFromJson.Size = new System.Drawing.Size(156, 27);
      LoadFromJson.TabIndex = 46;
      LoadFromJson.Text = "JSONファイルからロード";
      LoadFromJson.UseVisualStyleBackColor = true;
      LoadFromJson.Click += LoadFromJson_Click;
      // 
      // SaveToJson
      // 
      SaveToJson.Location = new System.Drawing.Point(598, 23);
      SaveToJson.Name = "SaveToJson";
      SaveToJson.Size = new System.Drawing.Size(201, 27);
      SaveToJson.TabIndex = 45;
      SaveToJson.Text = "出力フォルダとフォーマット名の保存";
      SaveToJson.UseVisualStyleBackColor = true;
      SaveToJson.Click += SaveToJson_Click;
      // 
      // label35
      // 
      label35.AutoSize = true;
      label35.Location = new System.Drawing.Point(18, 62);
      label35.Name = "label35";
      label35.Size = new System.Drawing.Size(125, 15);
      label35.TabIndex = 44;
      label35.Text = "リストの最大保存項目数";
      // 
      // MaxListItems
      // 
      MaxListItems.Location = new System.Drawing.Point(150, 58);
      MaxListItems.Name = "MaxListItems";
      MaxListItems.Size = new System.Drawing.Size(48, 23);
      MaxListItems.TabIndex = 43;
      MaxListItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      MaxListItems.Value = new decimal(new int[] { 20, 0, 0, 0 });
      // 
      // CommandInvoker
      // 
      CommandInvoker.Location = new System.Drawing.Point(436, 56);
      CommandInvoker.Name = "CommandInvoker";
      CommandInvoker.Size = new System.Drawing.Size(110, 26);
      CommandInvoker.TabIndex = 42;
      CommandInvoker.Text = "コマンド実行";
      CommandInvoker.UseVisualStyleBackColor = true;
      CommandInvoker.Visible = false;
      CommandInvoker.Click += CommandInvoker_Click;
      // 
      // Overwrite
      // 
      Overwrite.AutoSize = true;
      Overwrite.Location = new System.Drawing.Point(207, 27);
      Overwrite.Name = "Overwrite";
      Overwrite.Size = new System.Drawing.Size(187, 19);
      Overwrite.TabIndex = 36;
      Overwrite.Text = "既存ファイルの上書きを許可する。";
      Overwrite.UseVisualStyleBackColor = true;
      // 
      // ClearLists
      // 
      ClearLists.Location = new System.Drawing.Point(555, 56);
      ClearLists.Name = "ClearLists";
      ClearLists.Size = new System.Drawing.Size(244, 27);
      ClearLists.TabIndex = 41;
      ClearLists.TabStop = false;
      ClearLists.Text = "保存されているリストのクリア及び設定のリセット";
      ClearLists.UseVisualStyleBackColor = true;
      ClearLists.Click += ClearListItem_Click;
      // 
      // IsOpenStderr
      // 
      IsOpenStderr.AutoSize = true;
      IsOpenStderr.Location = new System.Drawing.Point(18, 27);
      IsOpenStderr.Name = "IsOpenStderr";
      IsOpenStderr.Size = new System.Drawing.Size(173, 19);
      IsOpenStderr.TabIndex = 35;
      IsOpenStderr.TabStop = false;
      IsOpenStderr.Text = "実行中の出力ウィンドウを開く。";
      IsOpenStderr.UseVisualStyleBackColor = true;
      // 
      // groupBox3
      // 
      groupBox3.Controls.Add(ConfigDirectory);
      groupBox3.Controls.Add(SubmitConfigDirDlg);
      groupBox3.Controls.Add(UseCustomConfig);
      groupBox3.Controls.Add(PrimaryMovieFormatId);
      groupBox3.Controls.Add(PrimaryAudioFormatId);
      groupBox3.Controls.Add(label39);
      groupBox3.Controls.Add(label38);
      groupBox3.Controls.Add(label37);
      groupBox3.Controls.Add(label36);
      groupBox3.Controls.Add(PrimaryVideoFormatId);
      groupBox3.Controls.Add(HideThumbnail);
      groupBox3.Controls.Add(label24);
      groupBox3.Controls.Add(label29);
      groupBox3.Controls.Add(SubmitOpenCookie);
      groupBox3.Controls.Add(CookiePath);
      groupBox3.Controls.Add(label0);
      groupBox3.Controls.Add(DeleteUrlAfterDownloaded);
      groupBox3.Controls.Add(label32);
      groupBox3.Controls.Add(UseCookie);
      groupBox3.Controls.Add(YtdlpPath);
      groupBox3.Controls.Add(ytdlpFindInPath);
      groupBox3.Controls.Add(btnYtdlp);
      groupBox3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      groupBox3.Location = new System.Drawing.Point(11, 125);
      groupBox3.Name = "groupBox3";
      groupBox3.Size = new System.Drawing.Size(812, 188);
      groupBox3.TabIndex = 40;
      groupBox3.TabStop = false;
      groupBox3.Text = "ダウンロード";
      // 
      // ConfigDirectory
      // 
      ConfigDirectory.AutoEllipsis = true;
      ConfigDirectory.Location = new System.Drawing.Point(565, 68);
      ConfigDirectory.Name = "ConfigDirectory";
      ConfigDirectory.Size = new System.Drawing.Size(234, 15);
      ConfigDirectory.TabIndex = 49;
      // 
      // SubmitConfigDirDlg
      // 
      SubmitConfigDirDlg.Location = new System.Drawing.Point(380, 61);
      SubmitConfigDirDlg.Name = "SubmitConfigDirDlg";
      SubmitConfigDirDlg.Size = new System.Drawing.Size(179, 28);
      SubmitConfigDirDlg.TabIndex = 48;
      SubmitConfigDirDlg.Text = "設定ファイルのディレクトリを指定";
      SubmitConfigDirDlg.UseVisualStyleBackColor = true;
      SubmitConfigDirDlg.Click += SubmitConfigDirDlg_Click;
      // 
      // UseCustomConfig
      // 
      UseCustomConfig.AutoSize = true;
      UseCustomConfig.Location = new System.Drawing.Point(356, 68);
      UseCustomConfig.Name = "UseCustomConfig";
      UseCustomConfig.Size = new System.Drawing.Size(15, 14);
      UseCustomConfig.TabIndex = 47;
      UseCustomConfig.UseVisualStyleBackColor = true;
      // 
      // PrimaryMovieFormatId
      // 
      PrimaryMovieFormatId.Location = new System.Drawing.Point(563, 96);
      PrimaryMovieFormatId.Name = "PrimaryMovieFormatId";
      PrimaryMovieFormatId.Size = new System.Drawing.Size(160, 23);
      PrimaryMovieFormatId.TabIndex = 46;
      PrimaryMovieFormatId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // PrimaryAudioFormatId
      // 
      PrimaryAudioFormatId.Location = new System.Drawing.Point(354, 96);
      PrimaryAudioFormatId.Name = "PrimaryAudioFormatId";
      PrimaryAudioFormatId.Size = new System.Drawing.Size(160, 23);
      PrimaryAudioFormatId.TabIndex = 45;
      PrimaryAudioFormatId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label39
      // 
      label39.AutoSize = true;
      label39.Location = new System.Drawing.Point(542, 100);
      label39.Name = "label39";
      label39.Size = new System.Drawing.Size(17, 15);
      label39.TabIndex = 44;
      label39.Text = "M";
      // 
      // label38
      // 
      label38.AutoSize = true;
      label38.Location = new System.Drawing.Point(336, 100);
      label38.Name = "label38";
      label38.Size = new System.Drawing.Size(15, 15);
      label38.TabIndex = 44;
      label38.Text = "A";
      // 
      // label37
      // 
      label37.AutoSize = true;
      label37.Location = new System.Drawing.Point(138, 100);
      label37.Name = "label37";
      label37.Size = new System.Drawing.Size(15, 15);
      label37.TabIndex = 44;
      label37.Text = "V";
      // 
      // label36
      // 
      label36.AutoSize = true;
      label36.Location = new System.Drawing.Point(18, 100);
      label36.Name = "label36";
      label36.Size = new System.Drawing.Size(114, 15);
      label36.TabIndex = 44;
      label36.Text = "優先するフォーマットID";
      // 
      // PrimaryVideoFormatId
      // 
      PrimaryVideoFormatId.Location = new System.Drawing.Point(155, 96);
      PrimaryVideoFormatId.Name = "PrimaryVideoFormatId";
      PrimaryVideoFormatId.Size = new System.Drawing.Size(160, 23);
      PrimaryVideoFormatId.TabIndex = 43;
      PrimaryVideoFormatId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // HideThumbnail
      // 
      HideThumbnail.AutoSize = true;
      HideThumbnail.Location = new System.Drawing.Point(189, 66);
      HideThumbnail.Name = "HideThumbnail";
      HideThumbnail.Size = new System.Drawing.Size(145, 19);
      HideThumbnail.TabIndex = 42;
      HideThumbnail.Text = "サムネール画像を非表示";
      HideThumbnail.UseVisualStyleBackColor = true;
      // 
      // label24
      // 
      label24.AutoSize = true;
      label24.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      label24.ForeColor = System.Drawing.Color.Red;
      label24.Location = new System.Drawing.Point(17, 162);
      label24.Name = "label24";
      label24.Size = new System.Drawing.Size(236, 15);
      label24.TabIndex = 41;
      label24.Text = "※現在chrome系ブラウザではエラーになります。";
      // 
      // label29
      // 
      label29.AutoSize = true;
      label29.Font = new System.Drawing.Font("Meiryo UI", 9F);
      label29.Location = new System.Drawing.Point(336, 135);
      label29.Name = "label29";
      label29.Size = new System.Drawing.Size(99, 15);
      label29.TabIndex = 5;
      label29.Text = "Cookieファイルパス";
      // 
      // SubmitOpenCookie
      // 
      SubmitOpenCookie.Font = new System.Drawing.Font("Meiryo UI", 9F);
      SubmitOpenCookie.Location = new System.Drawing.Point(732, 129);
      SubmitOpenCookie.Name = "SubmitOpenCookie";
      SubmitOpenCookie.Size = new System.Drawing.Size(51, 28);
      SubmitOpenCookie.TabIndex = 36;
      SubmitOpenCookie.TabStop = false;
      SubmitOpenCookie.Text = "参照";
      SubmitOpenCookie.UseVisualStyleBackColor = true;
      SubmitOpenCookie.Click += SubmitOpenCookie_Click;
      // 
      // CookiePath
      // 
      CookiePath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      CookiePath.Font = new System.Drawing.Font("Meiryo UI", 9F);
      CookiePath.Location = new System.Drawing.Point(441, 132);
      CookiePath.Name = "CookiePath";
      CookiePath.PlaceholderText = "Netscape形式のcookieファイル";
      CookiePath.Size = new System.Drawing.Size(289, 23);
      CookiePath.TabIndex = 15;
      // 
      // label0
      // 
      label0.AutoSize = true;
      label0.Font = new System.Drawing.Font("Meiryo UI", 9F);
      label0.Location = new System.Drawing.Point(18, 136);
      label0.Name = "label0";
      label0.Size = new System.Drawing.Size(125, 15);
      label0.TabIndex = 5;
      label0.Text = "ブラウザのCookieを使用";
      // 
      // DeleteUrlAfterDownloaded
      // 
      DeleteUrlAfterDownloaded.AutoSize = true;
      DeleteUrlAfterDownloaded.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      DeleteUrlAfterDownloaded.Location = new System.Drawing.Point(21, 66);
      DeleteUrlAfterDownloaded.Name = "DeleteUrlAfterDownloaded";
      DeleteUrlAfterDownloaded.Size = new System.Drawing.Size(149, 19);
      DeleteUrlAfterDownloaded.TabIndex = 40;
      DeleteUrlAfterDownloaded.Text = "ダウンロード後URLを削除";
      DeleteUrlAfterDownloaded.UseVisualStyleBackColor = true;
      // 
      // label32
      // 
      label32.AutoSize = true;
      label32.Location = new System.Drawing.Point(18, 32);
      label32.Name = "label32";
      label32.Size = new System.Drawing.Size(103, 15);
      label32.TabIndex = 37;
      label32.Text = "yt-dlp 実行ファイル";
      // 
      // UseCookie
      // 
      UseCookie.DisplayMember = "Label";
      UseCookie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      UseCookie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      UseCookie.Font = new System.Drawing.Font("Meiryo UI", 9F);
      UseCookie.FormattingEnabled = true;
      UseCookie.Location = new System.Drawing.Point(155, 132);
      UseCookie.Name = "UseCookie";
      UseCookie.Size = new System.Drawing.Size(159, 23);
      UseCookie.TabIndex = 13;
      UseCookie.ValueMember = "Value";
      // 
      // YtdlpPath
      // 
      YtdlpPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      YtdlpPath.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      YtdlpPath.FormattingEnabled = true;
      YtdlpPath.Location = new System.Drawing.Point(125, 28);
      YtdlpPath.Name = "YtdlpPath";
      YtdlpPath.Size = new System.Drawing.Size(474, 23);
      YtdlpPath.TabIndex = 39;
      YtdlpPath.TabStop = false;
      // 
      // ytdlpFindInPath
      // 
      ytdlpFindInPath.Font = new System.Drawing.Font("Meiryo UI", 9F);
      ytdlpFindInPath.Location = new System.Drawing.Point(655, 25);
      ytdlpFindInPath.Name = "ytdlpFindInPath";
      ytdlpFindInPath.Size = new System.Drawing.Size(144, 28);
      ytdlpFindInPath.TabIndex = 38;
      ytdlpFindInPath.TabStop = false;
      ytdlpFindInPath.Tag = "yt-dlp";
      ytdlpFindInPath.Text = "環境変数PATHから検索";
      ytdlpFindInPath.UseVisualStyleBackColor = true;
      ytdlpFindInPath.Click += FindInPath_Click;
      // 
      // btnYtdlp
      // 
      btnYtdlp.Font = new System.Drawing.Font("Meiryo UI", 9F);
      btnYtdlp.Location = new System.Drawing.Point(605, 25);
      btnYtdlp.Name = "btnYtdlp";
      btnYtdlp.Size = new System.Drawing.Size(49, 28);
      btnYtdlp.TabIndex = 36;
      btnYtdlp.TabStop = false;
      btnYtdlp.Text = "参照";
      btnYtdlp.UseVisualStyleBackColor = true;
      btnYtdlp.Click += btnYtdlp_Click;
      // 
      // groupBox2
      // 
      groupBox2.Controls.Add(RemoveBatListAfterDone);
      groupBox2.Controls.Add(BatExecWithConsole);
      groupBox2.Controls.Add(label8);
      groupBox2.Controls.Add(ffmpeg);
      groupBox2.Controls.Add(btnFFmpeg);
      groupBox2.Controls.Add(btnFindInPath);
      groupBox2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      groupBox2.Location = new System.Drawing.Point(11, 18);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new System.Drawing.Size(812, 101);
      groupBox2.TabIndex = 40;
      groupBox2.TabStop = false;
      groupBox2.Text = "動画変換";
      // 
      // RemoveBatListAfterDone
      // 
      RemoveBatListAfterDone.AutoSize = true;
      RemoveBatListAfterDone.Checked = true;
      RemoveBatListAfterDone.CheckState = System.Windows.Forms.CheckState.Checked;
      RemoveBatListAfterDone.Location = new System.Drawing.Point(274, 64);
      RemoveBatListAfterDone.Name = "RemoveBatListAfterDone";
      RemoveBatListAfterDone.Size = new System.Drawing.Size(222, 19);
      RemoveBatListAfterDone.TabIndex = 41;
      RemoveBatListAfterDone.Text = "バッチ実行完了後バッチリストを削除する。";
      RemoveBatListAfterDone.UseVisualStyleBackColor = true;
      // 
      // BatExecWithConsole
      // 
      BatExecWithConsole.AutoSize = true;
      BatExecWithConsole.Location = new System.Drawing.Point(20, 64);
      BatExecWithConsole.Name = "BatExecWithConsole";
      BatExecWithConsole.Size = new System.Drawing.Size(235, 19);
      BatExecWithConsole.TabIndex = 40;
      BatExecWithConsole.Text = "バッチ実行はコンソールウィンドウで実行する。";
      BatExecWithConsole.UseVisualStyleBackColor = true;
      // 
      // ImageContextMenu
      // 
      ImageContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { CommandSaveImage });
      ImageContextMenu.Name = "ImageContextMenu";
      ImageContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      ImageContextMenu.Size = new System.Drawing.Size(125, 26);
      // 
      // CommandSaveImage
      // 
      CommandSaveImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      CommandSaveImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      CommandSaveImage.Name = "CommandSaveImage";
      CommandSaveImage.Size = new System.Drawing.Size(124, 22);
      CommandSaveImage.Text = "画像保存";
      CommandSaveImage.Click += CommandSaveImage_Click;
      // 
      // OutputBox
      // 
      OutputBox.Controls.Add(linkLabel4);
      OutputBox.Controls.Add(FilePrefix);
      OutputBox.Controls.Add(FileSuffix);
      OutputBox.Controls.Add(btnSubmitOpenDlg);
      OutputBox.Controls.Add(FileContainer);
      OutputBox.Controls.Add(OpenFolder);
      OutputBox.Controls.Add(label13);
      OutputBox.Controls.Add(FileName);
      OutputBox.Controls.Add(label10);
      OutputBox.Controls.Add(cbOutputDir);
      OutputBox.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
      OutputBox.Location = new System.Drawing.Point(433, 468);
      OutputBox.Name = "OutputBox";
      OutputBox.Size = new System.Drawing.Size(407, 125);
      OutputBox.TabIndex = 44;
      OutputBox.TabStop = false;
      OutputBox.Text = "出力フォルダとファイル　　　　　　　　　";
      // 
      // linkLabel4
      // 
      linkLabel4.AutoSize = true;
      linkLabel4.Location = new System.Drawing.Point(117, 1);
      linkLabel4.Name = "linkLabel4";
      linkLabel4.Size = new System.Drawing.Size(55, 15);
      linkLabel4.TabIndex = 44;
      linkLabel4.TabStop = true;
      linkLabel4.Tag = "cbOutputDir";
      linkLabel4.Text = "リスト編集";
      linkLabel4.LinkClicked += EditListItems;
      // 
      // FilePrefix
      // 
      FilePrefix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      FilePrefix.Font = new System.Drawing.Font("Meiryo UI", 9F);
      FilePrefix.FormattingEnabled = true;
      FilePrefix.Location = new System.Drawing.Point(11, 65);
      FilePrefix.Name = "FilePrefix";
      FilePrefix.Size = new System.Drawing.Size(61, 23);
      FilePrefix.TabIndex = 34;
      FilePrefix.TabStop = false;
      // 
      // FileSuffix
      // 
      FileSuffix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      FileSuffix.Font = new System.Drawing.Font("Meiryo UI", 9F);
      FileSuffix.FormattingEnabled = true;
      FileSuffix.Location = new System.Drawing.Point(256, 65);
      FileSuffix.Name = "FileSuffix";
      FileSuffix.Size = new System.Drawing.Size(59, 23);
      FileSuffix.TabIndex = 33;
      FileSuffix.TabStop = false;
      // 
      // settingsPropertyValueBindingSource
      // 
      settingsPropertyValueBindingSource.DataSource = typeof(System.Configuration.SettingsPropertyValue);
      // 
      // DirectoryListBindingSource
      // 
      DirectoryListBindingSource.Sort = "";
      DirectoryListBindingSource.DataSourceChanged += DirectoryListBindingSource_DataSourceChanged;
      DirectoryListBindingSource.DataMemberChanged += DirectoryListBindingSource_DataMemberChanged;
      DirectoryListBindingSource.ListChanged += DirectoryListBindingSource_ListChanged;
      // 
      // VideoOnlyFormatSource
      // 
      VideoOnlyFormatSource.DataSourceChanged += VideoOnlyFormatSource_DataSourceChanged;
      VideoOnlyFormatSource.ListChanged += VideoOnlyFormatSource_ListChanged;
      // 
      // AudioOnlyFormatSource
      // 
      AudioOnlyFormatSource.DataSourceChanged += AudioOnlyFormatSource_DataSourceChanged;
      AudioOnlyFormatSource.ListChanged += AudioOnlyFormatSource_ListChanged;
      // 
      // MovieFormatSource
      // 
      MovieFormatSource.DataSourceChanged += MovieFormatSource_DataSourceChanged;
      MovieFormatSource.ListChanged += MovieFormatSource_ListChanged;
      // 
      // UrlBindingSource
      // 
      UrlBindingSource.ListChanged += UrlBindingSource_ListChanged;
      // 
      // OutputFileFormatBindingSource
      // 
      OutputFileFormatBindingSource.DataSourceChanged += OutputFileFormatBindingSource_DataSourceChanged;
      OutputFileFormatBindingSource.ListChanged += OutputFileFormatBindingSource_ListChanged;
      // 
      // PlaylistBindingSource
      // 
      PlaylistBindingSource.DataMember = "Item4";
      // 
      // Form1
      // 
      AllowDrop = true;
      AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(846, 624);
      Controls.Add(OutputBox);
      Controls.Add(Tab);
      Controls.Add(StatusBar);
      Controls.Add(InputBox);
      Font = new System.Drawing.Font("MS UI Gothic", 9F);
      FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      Name = "Form1";
      Padding = new System.Windows.Forms.Padding(3);
      SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      Text = "ffmpeg & yt-dlp GUI";
      FormClosing += Form1_FormClosing;
      Load += Form1_Load;
      CuttingBox.ResumeLayout(false);
      CuttingBox.PerformLayout();
      CodecBox.ResumeLayout(false);
      CodecBox.PerformLayout();
      ResizeBox.ResumeLayout(false);
      ResizeBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)resizeTo).EndInit();
      RotateBox.ResumeLayout(false);
      RotateBox.PerformLayout();
      BitrateBox.ResumeLayout(false);
      BitrateBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)LookAhead).EndInit();
      ((System.ComponentModel.ISupportInitialize)aBitrate).EndInit();
      ((System.ComponentModel.ISupportInitialize)vBitrate).EndInit();
      FileListMenu.ResumeLayout(false);
      DeInterlaceBox.ResumeLayout(false);
      DeInterlaceBox.PerformLayout();
      InputBox.ResumeLayout(false);
      InputBox.PerformLayout();
      StatusBar.ResumeLayout(false);
      StatusBar.PerformLayout();
      StatusBarMenu.ResumeLayout(false);
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
      OthersBox.ResumeLayout(false);
      PageUtility.ResumeLayout(false);
      groupBox7.ResumeLayout(false);
      CommonButtonBox.ResumeLayout(false);
      groupBox8.ResumeLayout(false);
      groupBox8.PerformLayout();
      Image2Box.ResumeLayout(false);
      Image2Box.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)TileRows).EndInit();
      ((System.ComponentModel.ISupportInitialize)CropTB).EndInit();
      ((System.ComponentModel.ISupportInitialize)ImageWidth).EndInit();
      ((System.ComponentModel.ISupportInitialize)TileColumns).EndInit();
      ((System.ComponentModel.ISupportInitialize)CropLR).EndInit();
      ((System.ComponentModel.ISupportInitialize)ImageHeight).EndInit();
      ((System.ComponentModel.ISupportInitialize)FrameRate).EndInit();
      PageDownloader.ResumeLayout(false);
      PageDownloader.PerformLayout();
      PlaylistGroup.ResumeLayout(false);
      PlaylistGroup.PerformLayout();
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)ThumbnailBox).EndInit();
      PageSetting.ResumeLayout(false);
      groupBox4.ResumeLayout(false);
      groupBox4.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)MaxListItems).EndInit();
      groupBox3.ResumeLayout(false);
      groupBox3.PerformLayout();
      groupBox2.ResumeLayout(false);
      groupBox2.PerformLayout();
      ImageContextMenu.ResumeLayout(false);
      OutputBox.ResumeLayout(false);
      OutputBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)settingsPropertyValueBindingSource).EndInit();
      ((System.ComponentModel.ISupportInitialize)DirectoryListBindingSource).EndInit();
      ((System.ComponentModel.ISupportInitialize)VideoOnlyFormatSource).EndInit();
      ((System.ComponentModel.ISupportInitialize)AudioOnlyFormatSource).EndInit();
      ((System.ComponentModel.ISupportInitialize)MovieFormatSource).EndInit();
      ((System.ComponentModel.ISupportInitialize)UrlBindingSource).EndInit();
      ((System.ComponentModel.ISupportInitialize)OutputFileFormatBindingSource).EndInit();
      ((System.ComponentModel.ISupportInitialize)PlaylistBindingSource).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.TextBox Commandlines;
    private System.Windows.Forms.TextBox txtSS;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTo;
    private System.Windows.Forms.GroupBox CuttingBox;
    private System.Windows.Forms.GroupBox CodecBox;
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
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnSubmitInvoke;
    private System.Windows.Forms.Button btnSubmitAddToBatch;
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
    private System.Windows.Forms.ListBox FileList;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnStopAll;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cbPreset;
    private System.Windows.Forms.GroupBox DeInterlaceBox;
    private System.Windows.Forms.ComboBox cbDevices;
    private System.Windows.Forms.Panel InputBox;
    private System.Windows.Forms.Button btnFFmpeg;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Button btnFindInPath;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.ComboBox ffmpeg;
    private System.Windows.Forms.NumericUpDown resizeTo;
    private System.Windows.Forms.RadioButton rbResizeNum;
    private System.Windows.Forms.Button OpenLogFile;
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
    private System.Windows.Forms.ComboBox FileName;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.BindingSource FileListBindingSource;
    private System.Windows.Forms.BindingSource DeInterlaceListBindingSource;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.ComboBox FileContainer;
    private System.Windows.Forms.TabControl Tab;
    private System.Windows.Forms.TabPage PageConvert;
    private System.Windows.Forms.GroupBox OthersBox;
    private System.Windows.Forms.GroupBox OutputBox;
    private System.Windows.Forms.BindingSource settingsPropertyValueBindingSource;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.TextBox FreeOptions;
    private System.Windows.Forms.TabPage PageUtility;
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
    private System.Windows.Forms.CheckBox IsOpenStderr;
    private System.Windows.Forms.BindingSource DirectoryListBindingSource;
    private System.Windows.Forms.Button OpenEncoderHelp;
    private System.Windows.Forms.Button OpenDecoderHelp;
    private System.Windows.Forms.Button btnSubmitSaveToFile;
    private System.Windows.Forms.Button btnSubmitBatchClear;
    private System.Windows.Forms.ComboBox DecoderHelpList;
    private System.Windows.Forms.ComboBox EncoderHelpList;
    private System.Windows.Forms.GroupBox groupBox7;
    private System.Windows.Forms.CheckBox Overwrite;
    private System.Windows.Forms.TabPage PageDownloader;
    private System.Windows.Forms.Button AddDownloadQueue;
    private System.Windows.Forms.Button SubmitConfirmFormat;
    private System.Windows.Forms.PictureBox ThumbnailBox;
    private System.Windows.Forms.ComboBox VideoOnlyFormat;
    private System.Windows.Forms.ComboBox AudioOnlyFormat;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.ComboBox MovieFormat;
    private System.Windows.Forms.Label label28;
    private System.Windows.Forms.LinkLabel LinkYdlOutputTemplate;
    private System.Windows.Forms.ComboBox VideoFrameRate;
    private System.Windows.Forms.Label label27;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.BindingSource VideoOnlyFormatSource;
    private System.Windows.Forms.BindingSource AudioOnlyFormatSource;
    private System.Windows.Forms.BindingSource MovieFormatSource;
    private System.Windows.Forms.Button SubmitDownloadDequeue;
    private System.Windows.Forms.Button StopDownload;
    private System.Windows.Forms.NumericUpDown CropTB;
    private System.Windows.Forms.NumericUpDown CropLR;
    private System.Windows.Forms.Label label31;
    private System.Windows.Forms.Label label26;
    private System.Windows.Forms.Label label25;
    private System.Windows.Forms.NumericUpDown TileRows;
    private System.Windows.Forms.NumericUpDown TileColumns;
    private System.Windows.Forms.Label label33;
    private System.Windows.Forms.CheckBox useTiledImage;
    private System.Windows.Forms.RadioButton rbResize900;
    private System.Windows.Forms.ContextMenuStrip ImageContextMenu;
    private System.Windows.Forms.ToolStripMenuItem CommandSaveImage;
    private System.Windows.Forms.RadioButton rbResizeSD;
    private System.Windows.Forms.CheckBox chkAfterDownload;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox ImageFreeOptions;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label DurationTime;
    private System.Windows.Forms.ComboBox DownloadUrl;
    private System.Windows.Forms.BindingSource UrlBindingSource;
    private System.Windows.Forms.ComboBox OutputFileFormat;
    private System.Windows.Forms.BindingSource OutputFileFormatBindingSource;
    private System.Windows.Forms.ToolStripStatusLabel QueueCount;
    private System.Windows.Forms.ContextMenuStrip FileListMenu;
    private System.Windows.Forms.ToolStripMenuItem FileListMenuItemDelete;
    private System.Windows.Forms.ToolStripMenuItem FileListMenuItemClear;
    private System.Windows.Forms.LinkLabel linkLabel3;
    private System.Windows.Forms.LinkLabel linkLabel4;
    private System.Windows.Forms.TextBox MediaTitle;
    private System.Windows.Forms.LinkLabel linkLabel5;
    private System.Windows.Forms.ListBox Playlist;
    private System.Windows.Forms.BindingSource PlaylistBindingSource;
    private System.Windows.Forms.GroupBox PlaylistGroup;
    private System.Windows.Forms.Button PlayListDownloadAll;
    private System.Windows.Forms.ToolStripProgressBar DummyProgressBar;
    private System.Windows.Forms.RadioButton DownloadBestQuality;
    private System.Windows.Forms.RadioButton DownloadWorstQuality;
    private System.Windows.Forms.TabPage PageSetting;
    private System.Windows.Forms.Button ytdlpFindInPath;
    private System.Windows.Forms.Button btnYtdlp;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.ComboBox YtdlpPath;
    private System.Windows.Forms.Label label34;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton FmtSeparated;
    private System.Windows.Forms.RadioButton FmtWhole;
    private System.Windows.Forms.ContextMenuStrip StatusBarMenu;
    private System.Windows.Forms.ToolStripMenuItem StatusBarMenuItemClearQueue;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox DeleteUrlAfterDownloaded;
    private System.Windows.Forms.Label label29;
    private System.Windows.Forms.Button SubmitOpenCookie;
    private System.Windows.Forms.TextBox CookiePath;
    private System.Windows.Forms.Label label0;
    private System.Windows.Forms.ComboBox UseCookie;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.CheckBox HideThumbnail;
    private System.Windows.Forms.Button CommandInvoker;
    private System.Windows.Forms.Button ClearLists;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button btnSubmitBatExecute;
    private System.Windows.Forms.NumericUpDown MaxListItems;
    private System.Windows.Forms.Label label35;
    private System.Windows.Forms.CheckBox BatExecWithConsole;
    private System.Windows.Forms.ToolStripStatusLabel BatListCount;
    private System.Windows.Forms.ToolStripMenuItem FileListMenuItemOpenFolder;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.Button SaveToJson;
    private System.Windows.Forms.Button LoadFromJson;
    private System.Windows.Forms.TextBox PrimaryVideoFormatId;
    private System.Windows.Forms.TextBox PrimaryAudioFormatId;
    private System.Windows.Forms.Label label38;
    private System.Windows.Forms.Label label37;
    private System.Windows.Forms.Label label36;
    private System.Windows.Forms.TextBox PrimaryMovieFormatId;
    private System.Windows.Forms.Label label39;
    private System.Windows.Forms.ToolTip TooltipHintStringInput;
    private System.Windows.Forms.CheckBox UseCustomConfig;
    private System.Windows.Forms.Label ConfigDirectory;
    private System.Windows.Forms.Button SubmitConfigDirDlg;
    private System.Windows.Forms.ToolStripMenuItem FileListMenuItemAddFolder;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.CheckBox RemoveBatListAfterDone;
  }
}

