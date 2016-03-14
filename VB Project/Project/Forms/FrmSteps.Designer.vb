<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSteps
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.txRunOrder = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btOk = New System.Windows.Forms.Button()
        Me.btCancel = New System.Windows.Forms.Button()
        Me.gbStepSettings = New System.Windows.Forms.GroupBox()
        Me.tcSteps = New System.Windows.Forms.TabControl()
        Me.PgBardecode = New System.Windows.Forms.TabPage()
        Me.gb1Barcodes = New System.Windows.Forms.GroupBox()
        Me.cx1Barcodes = New System.Windows.Forms.CheckedListBox()
        Me.gb1Patterns = New System.Windows.Forms.GroupBox()
        Me.cx1CreateOutSubFolders = New System.Windows.Forms.CheckBox()
        Me.cx1SubFolders = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tx1SubFolderRegex = New System.Windows.Forms.TextBox()
        Me.tx1BarcodeRegex = New System.Windows.Forms.TextBox()
        Me.tx1FileInRegex = New System.Windows.Forms.TextBox()
        Me.tx1FileOutTemplate = New System.Windows.Forms.TextBox()
        Me.gb1Paths = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tx1OutputFolder = New System.Windows.Forms.TextBox()
        Me.tx1ProcessedFolder = New System.Windows.Forms.TextBox()
        Me.tx1ExceptionFolder = New System.Windows.Forms.TextBox()
        Me.tx1InputFolder = New System.Windows.Forms.TextBox()
        Me.PgOCR = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tx2FileOutTemplate = New System.Windows.Forms.TextBox()
        Me.gb2Paths = New System.Windows.Forms.GroupBox()
        Me.cx2CreateOutSubFolders = New System.Windows.Forms.CheckBox()
        Me.cx2ProcessSubFolders = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tx2OutputFolder = New System.Windows.Forms.TextBox()
        Me.tx2InputFolder = New System.Windows.Forms.TextBox()
        Me.PgImgsToPdf = New System.Windows.Forms.TabPage()
        Me.pgCustom = New System.Windows.Forms.TabPage()
        Me.gb4CustomProcess = New System.Windows.Forms.GroupBox()
        Me.cb4CustomProcess = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.gb4ProcessPath = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tx4Input2 = New System.Windows.Forms.TextBox()
        Me.tx4Input1 = New System.Windows.Forms.TextBox()
        Me.gb3Paths = New System.Windows.Forms.GroupBox()
        Me.cx3CreateOutputSubFolder = New System.Windows.Forms.CheckBox()
        Me.cx3ProcessSubFolders = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tx3OutputFolder = New System.Windows.Forms.TextBox()
        Me.tx3InputFolder = New System.Windows.Forms.TextBox()
        Me.gb3Patterns = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.tx3FileOutTemplate = New System.Windows.Forms.TextBox()
        Me.cx3OCR = New System.Windows.Forms.CheckBox()
        Me.cx1DeleteInputFiles = New System.Windows.Forms.CheckBox()
        Me.cb1SplitMode = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.tx4Output = New System.Windows.Forms.TextBox()
        Me.MainPanel.SuspendLayout()
        Me.gbStepSettings.SuspendLayout()
        Me.tcSteps.SuspendLayout()
        Me.PgBardecode.SuspendLayout()
        Me.gb1Barcodes.SuspendLayout()
        Me.gb1Patterns.SuspendLayout()
        Me.gb1Paths.SuspendLayout()
        Me.PgOCR.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gb2Paths.SuspendLayout()
        Me.PgImgsToPdf.SuspendLayout()
        Me.pgCustom.SuspendLayout()
        Me.gb4CustomProcess.SuspendLayout()
        Me.gb4ProcessPath.SuspendLayout()
        Me.gb3Paths.SuspendLayout()
        Me.gb3Patterns.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.txRunOrder)
        Me.MainPanel.Controls.Add(Me.Label1)
        Me.MainPanel.Controls.Add(Me.btOk)
        Me.MainPanel.Controls.Add(Me.btCancel)
        Me.MainPanel.Controls.Add(Me.gbStepSettings)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(506, 539)
        Me.MainPanel.TabIndex = 0
        '
        'txRunOrder
        '
        Me.txRunOrder.Location = New System.Drawing.Point(77, 504)
        Me.txRunOrder.Name = "txRunOrder"
        Me.txRunOrder.Size = New System.Drawing.Size(46, 20)
        Me.txRunOrder.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 509)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Run Order:"
        '
        'btOk
        '
        Me.btOk.Location = New System.Drawing.Point(338, 504)
        Me.btOk.Name = "btOk"
        Me.btOk.Size = New System.Drawing.Size(75, 23)
        Me.btOk.TabIndex = 3
        Me.btOk.Text = "Ok"
        Me.btOk.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(419, 504)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 4
        Me.btCancel.Text = "Cancel"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'gbStepSettings
        '
        Me.gbStepSettings.Controls.Add(Me.tcSteps)
        Me.gbStepSettings.Location = New System.Drawing.Point(12, 12)
        Me.gbStepSettings.Name = "gbStepSettings"
        Me.gbStepSettings.Size = New System.Drawing.Size(482, 486)
        Me.gbStepSettings.TabIndex = 1
        Me.gbStepSettings.TabStop = False
        Me.gbStepSettings.Text = "Step Settings"
        '
        'tcSteps
        '
        Me.tcSteps.Controls.Add(Me.PgBardecode)
        Me.tcSteps.Controls.Add(Me.PgOCR)
        Me.tcSteps.Controls.Add(Me.PgImgsToPdf)
        Me.tcSteps.Controls.Add(Me.pgCustom)
        Me.tcSteps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcSteps.Location = New System.Drawing.Point(3, 16)
        Me.tcSteps.Name = "tcSteps"
        Me.tcSteps.SelectedIndex = 0
        Me.tcSteps.Size = New System.Drawing.Size(476, 467)
        Me.tcSteps.TabIndex = 0
        '
        'PgBardecode
        '
        Me.PgBardecode.Controls.Add(Me.gb1Barcodes)
        Me.PgBardecode.Controls.Add(Me.gb1Patterns)
        Me.PgBardecode.Controls.Add(Me.gb1Paths)
        Me.PgBardecode.Location = New System.Drawing.Point(4, 22)
        Me.PgBardecode.Name = "PgBardecode"
        Me.PgBardecode.Padding = New System.Windows.Forms.Padding(3)
        Me.PgBardecode.Size = New System.Drawing.Size(468, 441)
        Me.PgBardecode.TabIndex = 0
        Me.PgBardecode.Text = "Bardecode"
        Me.PgBardecode.UseVisualStyleBackColor = True
        '
        'gb1Barcodes
        '
        Me.gb1Barcodes.Controls.Add(Me.cx1Barcodes)
        Me.gb1Barcodes.Location = New System.Drawing.Point(6, 334)
        Me.gb1Barcodes.Name = "gb1Barcodes"
        Me.gb1Barcodes.Size = New System.Drawing.Size(456, 83)
        Me.gb1Barcodes.TabIndex = 2
        Me.gb1Barcodes.TabStop = False
        Me.gb1Barcodes.Text = "Barcode Types"
        '
        'cx1Barcodes
        '
        Me.cx1Barcodes.ColumnWidth = 140
        Me.cx1Barcodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cx1Barcodes.FormattingEnabled = True
        Me.cx1Barcodes.Items.AddRange(New Object() {"Codabar", "Code 128", "Code 2 of 5 (interleaved)", "Code 2 of 5 (other)", "Code 3 of 9", "PDF-417", "EAN-13 and UPC-A", "EAN-8", "UPC-E", "Datamatrix", "QR-Code"})
        Me.cx1Barcodes.Location = New System.Drawing.Point(3, 16)
        Me.cx1Barcodes.MultiColumn = True
        Me.cx1Barcodes.Name = "cx1Barcodes"
        Me.cx1Barcodes.Size = New System.Drawing.Size(450, 64)
        Me.cx1Barcodes.TabIndex = 0
        '
        'gb1Patterns
        '
        Me.gb1Patterns.Controls.Add(Me.Label19)
        Me.gb1Patterns.Controls.Add(Me.cb1SplitMode)
        Me.gb1Patterns.Controls.Add(Me.cx1DeleteInputFiles)
        Me.gb1Patterns.Controls.Add(Me.cx1CreateOutSubFolders)
        Me.gb1Patterns.Controls.Add(Me.cx1SubFolders)
        Me.gb1Patterns.Controls.Add(Me.Label9)
        Me.gb1Patterns.Controls.Add(Me.Label8)
        Me.gb1Patterns.Controls.Add(Me.Label7)
        Me.gb1Patterns.Controls.Add(Me.Label6)
        Me.gb1Patterns.Controls.Add(Me.tx1SubFolderRegex)
        Me.gb1Patterns.Controls.Add(Me.tx1BarcodeRegex)
        Me.gb1Patterns.Controls.Add(Me.tx1FileInRegex)
        Me.gb1Patterns.Controls.Add(Me.tx1FileOutTemplate)
        Me.gb1Patterns.Location = New System.Drawing.Point(6, 137)
        Me.gb1Patterns.Name = "gb1Patterns"
        Me.gb1Patterns.Size = New System.Drawing.Size(456, 191)
        Me.gb1Patterns.TabIndex = 1
        Me.gb1Patterns.TabStop = False
        Me.gb1Patterns.Text = "Patterns"
        '
        'cx1CreateOutSubFolders
        '
        Me.cx1CreateOutSubFolders.AutoSize = True
        Me.cx1CreateOutSubFolders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx1CreateOutSubFolders.Checked = True
        Me.cx1CreateOutSubFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cx1CreateOutSubFolders.Location = New System.Drawing.Point(162, 168)
        Me.cx1CreateOutSubFolders.Name = "cx1CreateOutSubFolders"
        Me.cx1CreateOutSubFolders.Size = New System.Drawing.Size(151, 17)
        Me.cx1CreateOutSubFolders.TabIndex = 9
        Me.cx1CreateOutSubFolders.Text = "Create Output Sub Folders"
        Me.cx1CreateOutSubFolders.UseVisualStyleBackColor = True
        '
        'cx1SubFolders
        '
        Me.cx1SubFolders.AutoSize = True
        Me.cx1SubFolders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx1SubFolders.Checked = True
        Me.cx1SubFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cx1SubFolders.Location = New System.Drawing.Point(6, 168)
        Me.cx1SubFolders.Name = "cx1SubFolders"
        Me.cx1SubFolders.Size = New System.Drawing.Size(123, 17)
        Me.cx1SubFolders.TabIndex = 8
        Me.cx1SubFolders.Text = "Process Sub Folders"
        Me.cx1SubFolders.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(95, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Sub Folder Regex:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 74)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Barcode Regex:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "File In Regex:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "File Out Template:"
        '
        'tx1SubFolderRegex
        '
        Me.tx1SubFolderRegex.Location = New System.Drawing.Point(104, 97)
        Me.tx1SubFolderRegex.Name = "tx1SubFolderRegex"
        Me.tx1SubFolderRegex.Size = New System.Drawing.Size(346, 20)
        Me.tx1SubFolderRegex.TabIndex = 3
        '
        'tx1BarcodeRegex
        '
        Me.tx1BarcodeRegex.Location = New System.Drawing.Point(104, 71)
        Me.tx1BarcodeRegex.Name = "tx1BarcodeRegex"
        Me.tx1BarcodeRegex.Size = New System.Drawing.Size(346, 20)
        Me.tx1BarcodeRegex.TabIndex = 2
        '
        'tx1FileInRegex
        '
        Me.tx1FileInRegex.Location = New System.Drawing.Point(104, 45)
        Me.tx1FileInRegex.Name = "tx1FileInRegex"
        Me.tx1FileInRegex.Size = New System.Drawing.Size(346, 20)
        Me.tx1FileInRegex.TabIndex = 1
        '
        'tx1FileOutTemplate
        '
        Me.tx1FileOutTemplate.Location = New System.Drawing.Point(104, 19)
        Me.tx1FileOutTemplate.Name = "tx1FileOutTemplate"
        Me.tx1FileOutTemplate.Size = New System.Drawing.Size(346, 20)
        Me.tx1FileOutTemplate.TabIndex = 0
        '
        'gb1Paths
        '
        Me.gb1Paths.Controls.Add(Me.Label5)
        Me.gb1Paths.Controls.Add(Me.Label4)
        Me.gb1Paths.Controls.Add(Me.Label3)
        Me.gb1Paths.Controls.Add(Me.Label2)
        Me.gb1Paths.Controls.Add(Me.tx1OutputFolder)
        Me.gb1Paths.Controls.Add(Me.tx1ProcessedFolder)
        Me.gb1Paths.Controls.Add(Me.tx1ExceptionFolder)
        Me.gb1Paths.Controls.Add(Me.tx1InputFolder)
        Me.gb1Paths.Location = New System.Drawing.Point(6, 6)
        Me.gb1Paths.Name = "gb1Paths"
        Me.gb1Paths.Size = New System.Drawing.Size(456, 125)
        Me.gb1Paths.TabIndex = 0
        Me.gb1Paths.TabStop = False
        Me.gb1Paths.Text = "Process Paths"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Output Folder:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Processed Folder:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Exception Folder:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Input Folder:"
        '
        'tx1OutputFolder
        '
        Me.tx1OutputFolder.Location = New System.Drawing.Point(104, 97)
        Me.tx1OutputFolder.Name = "tx1OutputFolder"
        Me.tx1OutputFolder.Size = New System.Drawing.Size(346, 20)
        Me.tx1OutputFolder.TabIndex = 3
        '
        'tx1ProcessedFolder
        '
        Me.tx1ProcessedFolder.Location = New System.Drawing.Point(104, 71)
        Me.tx1ProcessedFolder.Name = "tx1ProcessedFolder"
        Me.tx1ProcessedFolder.Size = New System.Drawing.Size(346, 20)
        Me.tx1ProcessedFolder.TabIndex = 2
        '
        'tx1ExceptionFolder
        '
        Me.tx1ExceptionFolder.Location = New System.Drawing.Point(104, 45)
        Me.tx1ExceptionFolder.Name = "tx1ExceptionFolder"
        Me.tx1ExceptionFolder.Size = New System.Drawing.Size(346, 20)
        Me.tx1ExceptionFolder.TabIndex = 1
        '
        'tx1InputFolder
        '
        Me.tx1InputFolder.Location = New System.Drawing.Point(104, 19)
        Me.tx1InputFolder.Name = "tx1InputFolder"
        Me.tx1InputFolder.Size = New System.Drawing.Size(346, 20)
        Me.tx1InputFolder.TabIndex = 0
        '
        'PgOCR
        '
        Me.PgOCR.Controls.Add(Me.GroupBox1)
        Me.PgOCR.Controls.Add(Me.gb2Paths)
        Me.PgOCR.Location = New System.Drawing.Point(4, 22)
        Me.PgOCR.Name = "PgOCR"
        Me.PgOCR.Padding = New System.Windows.Forms.Padding(3)
        Me.PgOCR.Size = New System.Drawing.Size(468, 385)
        Me.PgOCR.TabIndex = 1
        Me.PgOCR.Text = "OCR"
        Me.PgOCR.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.tx2FileOutTemplate)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 115)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(456, 52)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Patterns"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 22)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(93, 13)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "File Out Template:"
        '
        'tx2FileOutTemplate
        '
        Me.tx2FileOutTemplate.Location = New System.Drawing.Point(104, 19)
        Me.tx2FileOutTemplate.Name = "tx2FileOutTemplate"
        Me.tx2FileOutTemplate.Size = New System.Drawing.Size(346, 20)
        Me.tx2FileOutTemplate.TabIndex = 0
        '
        'gb2Paths
        '
        Me.gb2Paths.Controls.Add(Me.cx2CreateOutSubFolders)
        Me.gb2Paths.Controls.Add(Me.cx2ProcessSubFolders)
        Me.gb2Paths.Controls.Add(Me.Label11)
        Me.gb2Paths.Controls.Add(Me.Label12)
        Me.gb2Paths.Controls.Add(Me.tx2OutputFolder)
        Me.gb2Paths.Controls.Add(Me.tx2InputFolder)
        Me.gb2Paths.Location = New System.Drawing.Point(6, 6)
        Me.gb2Paths.Name = "gb2Paths"
        Me.gb2Paths.Size = New System.Drawing.Size(456, 103)
        Me.gb2Paths.TabIndex = 0
        Me.gb2Paths.TabStop = False
        Me.gb2Paths.Text = "Process Paths"
        '
        'cx2CreateOutSubFolders
        '
        Me.cx2CreateOutSubFolders.AutoSize = True
        Me.cx2CreateOutSubFolders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx2CreateOutSubFolders.Checked = True
        Me.cx2CreateOutSubFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cx2CreateOutSubFolders.Location = New System.Drawing.Point(150, 80)
        Me.cx2CreateOutSubFolders.Name = "cx2CreateOutSubFolders"
        Me.cx2CreateOutSubFolders.Size = New System.Drawing.Size(151, 17)
        Me.cx2CreateOutSubFolders.TabIndex = 13
        Me.cx2CreateOutSubFolders.Text = "Create Output Sub Folders"
        Me.cx2CreateOutSubFolders.UseVisualStyleBackColor = True
        '
        'cx2ProcessSubFolders
        '
        Me.cx2ProcessSubFolders.AutoSize = True
        Me.cx2ProcessSubFolders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx2ProcessSubFolders.Checked = True
        Me.cx2ProcessSubFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cx2ProcessSubFolders.Location = New System.Drawing.Point(6, 80)
        Me.cx2ProcessSubFolders.Name = "cx2ProcessSubFolders"
        Me.cx2ProcessSubFolders.Size = New System.Drawing.Size(123, 17)
        Me.cx2ProcessSubFolders.TabIndex = 12
        Me.cx2ProcessSubFolders.Text = "Process Sub Folders"
        Me.cx2ProcessSubFolders.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 13)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Output Folder:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(66, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Input Folder:"
        '
        'tx2OutputFolder
        '
        Me.tx2OutputFolder.Location = New System.Drawing.Point(104, 45)
        Me.tx2OutputFolder.Name = "tx2OutputFolder"
        Me.tx2OutputFolder.Size = New System.Drawing.Size(346, 20)
        Me.tx2OutputFolder.TabIndex = 9
        '
        'tx2InputFolder
        '
        Me.tx2InputFolder.Location = New System.Drawing.Point(104, 19)
        Me.tx2InputFolder.Name = "tx2InputFolder"
        Me.tx2InputFolder.Size = New System.Drawing.Size(346, 20)
        Me.tx2InputFolder.TabIndex = 8
        '
        'PgImgsToPdf
        '
        Me.PgImgsToPdf.Controls.Add(Me.gb3Patterns)
        Me.PgImgsToPdf.Controls.Add(Me.gb3Paths)
        Me.PgImgsToPdf.Location = New System.Drawing.Point(4, 22)
        Me.PgImgsToPdf.Name = "PgImgsToPdf"
        Me.PgImgsToPdf.Padding = New System.Windows.Forms.Padding(3)
        Me.PgImgsToPdf.Size = New System.Drawing.Size(468, 385)
        Me.PgImgsToPdf.TabIndex = 2
        Me.PgImgsToPdf.Text = "ImgsToPdf"
        Me.PgImgsToPdf.UseVisualStyleBackColor = True
        '
        'pgCustom
        '
        Me.pgCustom.Controls.Add(Me.gb4CustomProcess)
        Me.pgCustom.Controls.Add(Me.gb4ProcessPath)
        Me.pgCustom.Location = New System.Drawing.Point(4, 22)
        Me.pgCustom.Name = "pgCustom"
        Me.pgCustom.Padding = New System.Windows.Forms.Padding(3)
        Me.pgCustom.Size = New System.Drawing.Size(468, 441)
        Me.pgCustom.TabIndex = 3
        Me.pgCustom.Text = "Custom"
        Me.pgCustom.UseVisualStyleBackColor = True
        '
        'gb4CustomProcess
        '
        Me.gb4CustomProcess.Controls.Add(Me.cb4CustomProcess)
        Me.gb4CustomProcess.Controls.Add(Me.Label14)
        Me.gb4CustomProcess.Location = New System.Drawing.Point(6, 115)
        Me.gb4CustomProcess.Name = "gb4CustomProcess"
        Me.gb4CustomProcess.Size = New System.Drawing.Size(456, 53)
        Me.gb4CustomProcess.TabIndex = 2
        Me.gb4CustomProcess.TabStop = False
        Me.gb4CustomProcess.Text = "Custom Process"
        '
        'cb4CustomProcess
        '
        Me.cb4CustomProcess.FormattingEnabled = True
        Me.cb4CustomProcess.Location = New System.Drawing.Point(104, 19)
        Me.cb4CustomProcess.Name = "cb4CustomProcess"
        Me.cb4CustomProcess.Size = New System.Drawing.Size(346, 21)
        Me.cb4CustomProcess.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 22)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 13)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Custom Process Id:"
        '
        'gb4ProcessPath
        '
        Me.gb4ProcessPath.Controls.Add(Me.Label20)
        Me.gb4ProcessPath.Controls.Add(Me.tx4Output)
        Me.gb4ProcessPath.Controls.Add(Me.Label10)
        Me.gb4ProcessPath.Controls.Add(Me.Label13)
        Me.gb4ProcessPath.Controls.Add(Me.tx4Input2)
        Me.gb4ProcessPath.Controls.Add(Me.tx4Input1)
        Me.gb4ProcessPath.Location = New System.Drawing.Point(6, 6)
        Me.gb4ProcessPath.Name = "gb4ProcessPath"
        Me.gb4ProcessPath.Size = New System.Drawing.Size(456, 103)
        Me.gb4ProcessPath.TabIndex = 1
        Me.gb4ProcessPath.TabStop = False
        Me.gb4ProcessPath.Text = "Process Paths"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 13)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Input 2:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 22)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Input 1:"
        '
        'tx4Input2
        '
        Me.tx4Input2.Location = New System.Drawing.Point(104, 45)
        Me.tx4Input2.Name = "tx4Input2"
        Me.tx4Input2.Size = New System.Drawing.Size(346, 20)
        Me.tx4Input2.TabIndex = 9
        '
        'tx4Input1
        '
        Me.tx4Input1.Location = New System.Drawing.Point(104, 19)
        Me.tx4Input1.Name = "tx4Input1"
        Me.tx4Input1.Size = New System.Drawing.Size(346, 20)
        Me.tx4Input1.TabIndex = 8
        '
        'gb3Paths
        '
        Me.gb3Paths.Controls.Add(Me.cx3CreateOutputSubFolder)
        Me.gb3Paths.Controls.Add(Me.cx3ProcessSubFolders)
        Me.gb3Paths.Controls.Add(Me.Label15)
        Me.gb3Paths.Controls.Add(Me.Label16)
        Me.gb3Paths.Controls.Add(Me.tx3OutputFolder)
        Me.gb3Paths.Controls.Add(Me.tx3InputFolder)
        Me.gb3Paths.Location = New System.Drawing.Point(6, 6)
        Me.gb3Paths.Name = "gb3Paths"
        Me.gb3Paths.Size = New System.Drawing.Size(456, 103)
        Me.gb3Paths.TabIndex = 1
        Me.gb3Paths.TabStop = False
        Me.gb3Paths.Text = "Process Paths"
        '
        'cx3CreateOutputSubFolder
        '
        Me.cx3CreateOutputSubFolder.AutoSize = True
        Me.cx3CreateOutputSubFolder.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx3CreateOutputSubFolder.Checked = True
        Me.cx3CreateOutputSubFolder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cx3CreateOutputSubFolder.Location = New System.Drawing.Point(150, 80)
        Me.cx3CreateOutputSubFolder.Name = "cx3CreateOutputSubFolder"
        Me.cx3CreateOutputSubFolder.Size = New System.Drawing.Size(151, 17)
        Me.cx3CreateOutputSubFolder.TabIndex = 13
        Me.cx3CreateOutputSubFolder.Text = "Create Output Sub Folders"
        Me.cx3CreateOutputSubFolder.UseVisualStyleBackColor = True
        '
        'cx3ProcessSubFolders
        '
        Me.cx3ProcessSubFolders.AutoSize = True
        Me.cx3ProcessSubFolders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx3ProcessSubFolders.Checked = True
        Me.cx3ProcessSubFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cx3ProcessSubFolders.Location = New System.Drawing.Point(6, 80)
        Me.cx3ProcessSubFolders.Name = "cx3ProcessSubFolders"
        Me.cx3ProcessSubFolders.Size = New System.Drawing.Size(123, 17)
        Me.cx3ProcessSubFolders.TabIndex = 12
        Me.cx3ProcessSubFolders.Text = "Process Sub Folders"
        Me.cx3ProcessSubFolders.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 48)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 13)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "Output Folder:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 22)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(66, 13)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "Input Folder:"
        '
        'tx3OutputFolder
        '
        Me.tx3OutputFolder.Location = New System.Drawing.Point(104, 45)
        Me.tx3OutputFolder.Name = "tx3OutputFolder"
        Me.tx3OutputFolder.Size = New System.Drawing.Size(346, 20)
        Me.tx3OutputFolder.TabIndex = 9
        '
        'tx3InputFolder
        '
        Me.tx3InputFolder.Location = New System.Drawing.Point(104, 19)
        Me.tx3InputFolder.Name = "tx3InputFolder"
        Me.tx3InputFolder.Size = New System.Drawing.Size(346, 20)
        Me.tx3InputFolder.TabIndex = 8
        '
        'gb3Patterns
        '
        Me.gb3Patterns.Controls.Add(Me.cx3OCR)
        Me.gb3Patterns.Controls.Add(Me.Label18)
        Me.gb3Patterns.Controls.Add(Me.tx3FileOutTemplate)
        Me.gb3Patterns.Location = New System.Drawing.Point(6, 115)
        Me.gb3Patterns.Name = "gb3Patterns"
        Me.gb3Patterns.Size = New System.Drawing.Size(456, 74)
        Me.gb3Patterns.TabIndex = 3
        Me.gb3Patterns.TabStop = False
        Me.gb3Patterns.Text = "Patterns"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 22)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(93, 13)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "File Out Template:"
        '
        'tx3FileOutTemplate
        '
        Me.tx3FileOutTemplate.Location = New System.Drawing.Point(104, 19)
        Me.tx3FileOutTemplate.Name = "tx3FileOutTemplate"
        Me.tx3FileOutTemplate.Size = New System.Drawing.Size(346, 20)
        Me.tx3FileOutTemplate.TabIndex = 0
        '
        'cx3OCR
        '
        Me.cx3OCR.AutoSize = True
        Me.cx3OCR.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx3OCR.Location = New System.Drawing.Point(6, 51)
        Me.cx3OCR.Name = "cx3OCR"
        Me.cx3OCR.Size = New System.Drawing.Size(204, 17)
        Me.cx3OCR.TabIndex = 5
        Me.cx3OCR.Text = "OCR the output PDFs (Takes Longer)"
        Me.cx3OCR.UseVisualStyleBackColor = True
        '
        'cx1DeleteInputFiles
        '
        Me.cx1DeleteInputFiles.AutoSize = True
        Me.cx1DeleteInputFiles.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx1DeleteInputFiles.Location = New System.Drawing.Point(345, 168)
        Me.cx1DeleteInputFiles.Name = "cx1DeleteInputFiles"
        Me.cx1DeleteInputFiles.Size = New System.Drawing.Size(105, 17)
        Me.cx1DeleteInputFiles.TabIndex = 10
        Me.cx1DeleteInputFiles.Text = "Delete Input files"
        Me.cx1DeleteInputFiles.UseVisualStyleBackColor = True
        '
        'cb1SplitMode
        '
        Me.cb1SplitMode.FormattingEnabled = True
        Me.cb1SplitMode.Items.AddRange(New Object() {"Files Start with barcode", "Files End with barcode", "Only content between barcodes", "Only barcode pages", "Process JPG Images", "Just Rename according barcode", "Files Start with Barcode (Only Different Values)", "Fixed Intervals", "Just Rename according last barcode page"})
        Me.cb1SplitMode.Location = New System.Drawing.Point(104, 123)
        Me.cb1SplitMode.Name = "cb1SplitMode"
        Me.cb1SplitMode.Size = New System.Drawing.Size(346, 21)
        Me.cb1SplitMode.TabIndex = 11
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 126)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(60, 13)
        Me.Label19.TabIndex = 12
        Me.Label19.Text = "Split Mode:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 74)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(42, 13)
        Me.Label20.TabIndex = 13
        Me.Label20.Text = "Output:"
        '
        'tx4Output
        '
        Me.tx4Output.Location = New System.Drawing.Point(104, 71)
        Me.tx4Output.Name = "tx4Output"
        Me.tx4Output.Size = New System.Drawing.Size(346, 20)
        Me.tx4Output.TabIndex = 12
        '
        'FrmSteps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 539)
        Me.Controls.Add(Me.MainPanel)
        Me.Name = "FrmSteps"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Steps"
        Me.MainPanel.ResumeLayout(False)
        Me.MainPanel.PerformLayout()
        Me.gbStepSettings.ResumeLayout(False)
        Me.tcSteps.ResumeLayout(False)
        Me.PgBardecode.ResumeLayout(False)
        Me.gb1Barcodes.ResumeLayout(False)
        Me.gb1Patterns.ResumeLayout(False)
        Me.gb1Patterns.PerformLayout()
        Me.gb1Paths.ResumeLayout(False)
        Me.gb1Paths.PerformLayout()
        Me.PgOCR.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gb2Paths.ResumeLayout(False)
        Me.gb2Paths.PerformLayout()
        Me.PgImgsToPdf.ResumeLayout(False)
        Me.pgCustom.ResumeLayout(False)
        Me.gb4CustomProcess.ResumeLayout(False)
        Me.gb4CustomProcess.PerformLayout()
        Me.gb4ProcessPath.ResumeLayout(False)
        Me.gb4ProcessPath.PerformLayout()
        Me.gb3Paths.ResumeLayout(False)
        Me.gb3Paths.PerformLayout()
        Me.gb3Patterns.ResumeLayout(False)
        Me.gb3Patterns.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainPanel As System.Windows.Forms.Panel
    Friend WithEvents gbStepSettings As System.Windows.Forms.GroupBox
    Friend WithEvents tcSteps As System.Windows.Forms.TabControl
    Friend WithEvents PgBardecode As System.Windows.Forms.TabPage
    Friend WithEvents PgOCR As System.Windows.Forms.TabPage
    Friend WithEvents btOk As System.Windows.Forms.Button
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents PgImgsToPdf As System.Windows.Forms.TabPage
    Friend WithEvents gb1Paths As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tx1OutputFolder As System.Windows.Forms.TextBox
    Friend WithEvents tx1ProcessedFolder As System.Windows.Forms.TextBox
    Friend WithEvents tx1ExceptionFolder As System.Windows.Forms.TextBox
    Friend WithEvents tx1InputFolder As System.Windows.Forms.TextBox
    Friend WithEvents gb1Patterns As System.Windows.Forms.GroupBox
    Friend WithEvents tx1SubFolderRegex As System.Windows.Forms.TextBox
    Friend WithEvents tx1BarcodeRegex As System.Windows.Forms.TextBox
    Friend WithEvents tx1FileInRegex As System.Windows.Forms.TextBox
    Friend WithEvents tx1FileOutTemplate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cx1SubFolders As System.Windows.Forms.CheckBox
    Friend WithEvents gb1Barcodes As System.Windows.Forms.GroupBox
    Friend WithEvents cx1Barcodes As System.Windows.Forms.CheckedListBox
    Friend WithEvents txRunOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gb2Paths As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tx2OutputFolder As System.Windows.Forms.TextBox
    Friend WithEvents tx2InputFolder As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tx2FileOutTemplate As System.Windows.Forms.TextBox
    Friend WithEvents cx1CreateOutSubFolders As System.Windows.Forms.CheckBox
    Friend WithEvents pgCustom As System.Windows.Forms.TabPage
    Friend WithEvents gb4CustomProcess As System.Windows.Forms.GroupBox
    Friend WithEvents cb4CustomProcess As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents gb4ProcessPath As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tx4Input2 As System.Windows.Forms.TextBox
    Friend WithEvents tx4Input1 As System.Windows.Forms.TextBox
    Friend WithEvents cx2CreateOutSubFolders As System.Windows.Forms.CheckBox
    Friend WithEvents cx2ProcessSubFolders As System.Windows.Forms.CheckBox
    Friend WithEvents gb3Patterns As System.Windows.Forms.GroupBox
    Friend WithEvents cx3OCR As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents tx3FileOutTemplate As System.Windows.Forms.TextBox
    Friend WithEvents gb3Paths As System.Windows.Forms.GroupBox
    Friend WithEvents cx3CreateOutputSubFolder As System.Windows.Forms.CheckBox
    Friend WithEvents cx3ProcessSubFolders As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tx3OutputFolder As System.Windows.Forms.TextBox
    Friend WithEvents tx3InputFolder As System.Windows.Forms.TextBox
    Friend WithEvents cx1DeleteInputFiles As System.Windows.Forms.CheckBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cb1SplitMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents tx4Output As System.Windows.Forms.TextBox
End Class
