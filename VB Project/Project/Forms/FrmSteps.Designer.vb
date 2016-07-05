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
        Me.rbDrws = New System.Windows.Forms.RadioButton()
        Me.rbDocs = New System.Windows.Forms.RadioButton()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cb1SplitMode = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tx1SubFolderRegex = New System.Windows.Forms.TextBox()
        Me.tx1BarcodeRegex = New System.Windows.Forms.TextBox()
        Me.tx1FileInRegex = New System.Windows.Forms.TextBox()
        Me.tx1FileOutTemplate = New System.Windows.Forms.TextBox()
        Me.PgOCR = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tx2FileOutTemplate = New System.Windows.Forms.TextBox()
        Me.PgImgsToPdf = New System.Windows.Forms.TabPage()
        Me.gb3Patterns = New System.Windows.Forms.GroupBox()
        Me.cx3OCR = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.tx3FileOutTemplate = New System.Windows.Forms.TextBox()
        Me.pgCustom = New System.Windows.Forms.TabPage()
        Me.gb4CustomProcess = New System.Windows.Forms.GroupBox()
        Me.cb4CustomProcess = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.pgSplitPDFSize = New System.Windows.Forms.TabPage()
        Me.gb5Pattern = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.tx5Size = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.tx5FileTemplate = New System.Windows.Forms.TextBox()
        Me.pgBarcodeRecognition = New System.Windows.Forms.TabPage()
        Me.gb6Sections = New System.Windows.Forms.GroupBox()
        Me.dg6Sections = New System.Windows.Forms.DataGridView()
        Me.bt6NewStep = New System.Windows.Forms.Button()
        Me.bt6EditStep = New System.Windows.Forms.Button()
        Me.bt6DeleteStep = New System.Windows.Forms.Button()
        Me.MainPanel.SuspendLayout()
        Me.gbStepSettings.SuspendLayout()
        Me.tcSteps.SuspendLayout()
        Me.PgBardecode.SuspendLayout()
        Me.gb1Barcodes.SuspendLayout()
        Me.gb1Patterns.SuspendLayout()
        Me.PgOCR.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.PgImgsToPdf.SuspendLayout()
        Me.gb3Patterns.SuspendLayout()
        Me.pgCustom.SuspendLayout()
        Me.gb4CustomProcess.SuspendLayout()
        Me.pgSplitPDFSize.SuspendLayout()
        Me.gb5Pattern.SuspendLayout()
        Me.pgBarcodeRecognition.SuspendLayout()
        Me.gb6Sections.SuspendLayout()
        CType(Me.dg6Sections, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.MainPanel.Size = New System.Drawing.Size(506, 402)
        Me.MainPanel.TabIndex = 0
        '
        'txRunOrder
        '
        Me.txRunOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txRunOrder.Location = New System.Drawing.Point(77, 367)
        Me.txRunOrder.Name = "txRunOrder"
        Me.txRunOrder.Size = New System.Drawing.Size(46, 20)
        Me.txRunOrder.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 372)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Run Order:"
        '
        'btOk
        '
        Me.btOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btOk.Location = New System.Drawing.Point(338, 367)
        Me.btOk.Name = "btOk"
        Me.btOk.Size = New System.Drawing.Size(75, 23)
        Me.btOk.TabIndex = 3
        Me.btOk.Text = "Ok"
        Me.btOk.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btCancel.Location = New System.Drawing.Point(419, 367)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 4
        Me.btCancel.Text = "Cancel"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'gbStepSettings
        '
        Me.gbStepSettings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbStepSettings.Controls.Add(Me.tcSteps)
        Me.gbStepSettings.Location = New System.Drawing.Point(12, 12)
        Me.gbStepSettings.Name = "gbStepSettings"
        Me.gbStepSettings.Size = New System.Drawing.Size(482, 347)
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
        Me.tcSteps.Controls.Add(Me.pgSplitPDFSize)
        Me.tcSteps.Controls.Add(Me.pgBarcodeRecognition)
        Me.tcSteps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcSteps.Location = New System.Drawing.Point(3, 16)
        Me.tcSteps.Name = "tcSteps"
        Me.tcSteps.SelectedIndex = 0
        Me.tcSteps.Size = New System.Drawing.Size(476, 328)
        Me.tcSteps.TabIndex = 0
        '
        'PgBardecode
        '
        Me.PgBardecode.Controls.Add(Me.gb1Barcodes)
        Me.PgBardecode.Controls.Add(Me.gb1Patterns)
        Me.PgBardecode.Location = New System.Drawing.Point(4, 22)
        Me.PgBardecode.Name = "PgBardecode"
        Me.PgBardecode.Padding = New System.Windows.Forms.Padding(3)
        Me.PgBardecode.Size = New System.Drawing.Size(468, 302)
        Me.PgBardecode.TabIndex = 0
        Me.PgBardecode.Text = "Bardecode"
        Me.PgBardecode.UseVisualStyleBackColor = True
        '
        'gb1Barcodes
        '
        Me.gb1Barcodes.Controls.Add(Me.cx1Barcodes)
        Me.gb1Barcodes.Location = New System.Drawing.Point(6, 213)
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
        Me.gb1Patterns.Controls.Add(Me.rbDrws)
        Me.gb1Patterns.Controls.Add(Me.rbDocs)
        Me.gb1Patterns.Controls.Add(Me.Label19)
        Me.gb1Patterns.Controls.Add(Me.cb1SplitMode)
        Me.gb1Patterns.Controls.Add(Me.Label9)
        Me.gb1Patterns.Controls.Add(Me.Label8)
        Me.gb1Patterns.Controls.Add(Me.Label7)
        Me.gb1Patterns.Controls.Add(Me.Label6)
        Me.gb1Patterns.Controls.Add(Me.tx1SubFolderRegex)
        Me.gb1Patterns.Controls.Add(Me.tx1BarcodeRegex)
        Me.gb1Patterns.Controls.Add(Me.tx1FileInRegex)
        Me.gb1Patterns.Controls.Add(Me.tx1FileOutTemplate)
        Me.gb1Patterns.Location = New System.Drawing.Point(6, 6)
        Me.gb1Patterns.Name = "gb1Patterns"
        Me.gb1Patterns.Size = New System.Drawing.Size(456, 201)
        Me.gb1Patterns.TabIndex = 1
        Me.gb1Patterns.TabStop = False
        Me.gb1Patterns.Text = "Patterns"
        '
        'rbDrws
        '
        Me.rbDrws.AutoSize = True
        Me.rbDrws.Location = New System.Drawing.Point(11, 177)
        Me.rbDrws.Name = "rbDrws"
        Me.rbDrws.Size = New System.Drawing.Size(110, 17)
        Me.rbDrws.TabIndex = 14
        Me.rbDrws.Text = "Process Drawings"
        Me.rbDrws.UseVisualStyleBackColor = True
        '
        'rbDocs
        '
        Me.rbDocs.AutoSize = True
        Me.rbDocs.Checked = True
        Me.rbDocs.Location = New System.Drawing.Point(11, 154)
        Me.rbDocs.Name = "rbDocs"
        Me.rbDocs.Size = New System.Drawing.Size(120, 17)
        Me.rbDocs.TabIndex = 13
        Me.rbDocs.TabStop = True
        Me.rbDocs.Text = "Process Documents"
        Me.rbDocs.UseVisualStyleBackColor = True
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
        'cb1SplitMode
        '
        Me.cb1SplitMode.FormattingEnabled = True
        Me.cb1SplitMode.Items.AddRange(New Object() {"Files Start with barcode", "Files End with barcode", "Only content between barcodes", "Only barcode pages", "Process JPG Images", "Just Rename according barcode", "Files Start with Barcode (Only Different Values)", "Fixed Intervals", "Just Rename according last barcode page"})
        Me.cb1SplitMode.Location = New System.Drawing.Point(104, 123)
        Me.cb1SplitMode.Name = "cb1SplitMode"
        Me.cb1SplitMode.Size = New System.Drawing.Size(346, 21)
        Me.cb1SplitMode.TabIndex = 11
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
        'PgOCR
        '
        Me.PgOCR.Controls.Add(Me.GroupBox1)
        Me.PgOCR.Location = New System.Drawing.Point(4, 22)
        Me.PgOCR.Name = "PgOCR"
        Me.PgOCR.Padding = New System.Windows.Forms.Padding(3)
        Me.PgOCR.Size = New System.Drawing.Size(468, 302)
        Me.PgOCR.TabIndex = 1
        Me.PgOCR.Text = "OCR"
        Me.PgOCR.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.tx2FileOutTemplate)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
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
        'PgImgsToPdf
        '
        Me.PgImgsToPdf.Controls.Add(Me.gb3Patterns)
        Me.PgImgsToPdf.Location = New System.Drawing.Point(4, 22)
        Me.PgImgsToPdf.Name = "PgImgsToPdf"
        Me.PgImgsToPdf.Padding = New System.Windows.Forms.Padding(3)
        Me.PgImgsToPdf.Size = New System.Drawing.Size(468, 302)
        Me.PgImgsToPdf.TabIndex = 2
        Me.PgImgsToPdf.Text = "ImgsToPdf"
        Me.PgImgsToPdf.UseVisualStyleBackColor = True
        '
        'gb3Patterns
        '
        Me.gb3Patterns.Controls.Add(Me.cx3OCR)
        Me.gb3Patterns.Controls.Add(Me.Label18)
        Me.gb3Patterns.Controls.Add(Me.tx3FileOutTemplate)
        Me.gb3Patterns.Location = New System.Drawing.Point(6, 6)
        Me.gb3Patterns.Name = "gb3Patterns"
        Me.gb3Patterns.Size = New System.Drawing.Size(456, 74)
        Me.gb3Patterns.TabIndex = 3
        Me.gb3Patterns.TabStop = False
        Me.gb3Patterns.Text = "Patterns"
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
        'pgCustom
        '
        Me.pgCustom.Controls.Add(Me.gb4CustomProcess)
        Me.pgCustom.Location = New System.Drawing.Point(4, 22)
        Me.pgCustom.Name = "pgCustom"
        Me.pgCustom.Padding = New System.Windows.Forms.Padding(3)
        Me.pgCustom.Size = New System.Drawing.Size(468, 302)
        Me.pgCustom.TabIndex = 3
        Me.pgCustom.Text = "Custom"
        Me.pgCustom.UseVisualStyleBackColor = True
        '
        'gb4CustomProcess
        '
        Me.gb4CustomProcess.Controls.Add(Me.cb4CustomProcess)
        Me.gb4CustomProcess.Controls.Add(Me.Label14)
        Me.gb4CustomProcess.Location = New System.Drawing.Point(6, 6)
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
        'pgSplitPDFSize
        '
        Me.pgSplitPDFSize.Controls.Add(Me.gb5Pattern)
        Me.pgSplitPDFSize.Location = New System.Drawing.Point(4, 22)
        Me.pgSplitPDFSize.Name = "pgSplitPDFSize"
        Me.pgSplitPDFSize.Padding = New System.Windows.Forms.Padding(3)
        Me.pgSplitPDFSize.Size = New System.Drawing.Size(468, 302)
        Me.pgSplitPDFSize.TabIndex = 4
        Me.pgSplitPDFSize.Text = "Split PDF by Size"
        Me.pgSplitPDFSize.UseVisualStyleBackColor = True
        '
        'gb5Pattern
        '
        Me.gb5Pattern.Controls.Add(Me.Label22)
        Me.gb5Pattern.Controls.Add(Me.tx5Size)
        Me.gb5Pattern.Controls.Add(Me.Label21)
        Me.gb5Pattern.Controls.Add(Me.tx5FileTemplate)
        Me.gb5Pattern.Location = New System.Drawing.Point(6, 6)
        Me.gb5Pattern.Name = "gb5Pattern"
        Me.gb5Pattern.Size = New System.Drawing.Size(456, 75)
        Me.gb5Pattern.TabIndex = 13
        Me.gb5Pattern.TabStop = False
        Me.gb5Pattern.Text = "Patterns"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 48)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(53, 13)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "Split Size:"
        '
        'tx5Size
        '
        Me.tx5Size.Location = New System.Drawing.Point(104, 45)
        Me.tx5Size.Name = "tx5Size"
        Me.tx5Size.Size = New System.Drawing.Size(346, 20)
        Me.tx5Size.TabIndex = 5
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 22)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(73, 13)
        Me.Label21.TabIndex = 4
        Me.Label21.Text = "File Template:"
        '
        'tx5FileTemplate
        '
        Me.tx5FileTemplate.Location = New System.Drawing.Point(104, 19)
        Me.tx5FileTemplate.Name = "tx5FileTemplate"
        Me.tx5FileTemplate.Size = New System.Drawing.Size(346, 20)
        Me.tx5FileTemplate.TabIndex = 0
        '
        'pgBarcodeRecognition
        '
        Me.pgBarcodeRecognition.Controls.Add(Me.gb6Sections)
        Me.pgBarcodeRecognition.Location = New System.Drawing.Point(4, 22)
        Me.pgBarcodeRecognition.Name = "pgBarcodeRecognition"
        Me.pgBarcodeRecognition.Padding = New System.Windows.Forms.Padding(3)
        Me.pgBarcodeRecognition.Size = New System.Drawing.Size(468, 302)
        Me.pgBarcodeRecognition.TabIndex = 5
        Me.pgBarcodeRecognition.Text = "Barcode Recognition"
        Me.pgBarcodeRecognition.UseVisualStyleBackColor = True
        '
        'gb6Sections
        '
        Me.gb6Sections.Controls.Add(Me.bt6NewStep)
        Me.gb6Sections.Controls.Add(Me.bt6EditStep)
        Me.gb6Sections.Controls.Add(Me.bt6DeleteStep)
        Me.gb6Sections.Controls.Add(Me.dg6Sections)
        Me.gb6Sections.Location = New System.Drawing.Point(6, 6)
        Me.gb6Sections.Name = "gb6Sections"
        Me.gb6Sections.Size = New System.Drawing.Size(456, 290)
        Me.gb6Sections.TabIndex = 0
        Me.gb6Sections.TabStop = False
        Me.gb6Sections.Text = "File/Sections"
        '
        'dg6Sections
        '
        Me.dg6Sections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg6Sections.Location = New System.Drawing.Point(6, 19)
        Me.dg6Sections.Name = "dg6Sections"
        Me.dg6Sections.Size = New System.Drawing.Size(444, 236)
        Me.dg6Sections.TabIndex = 0
        '
        'bt6NewStep
        '
        Me.bt6NewStep.Location = New System.Drawing.Point(213, 261)
        Me.bt6NewStep.Name = "bt6NewStep"
        Me.bt6NewStep.Size = New System.Drawing.Size(75, 23)
        Me.bt6NewStep.TabIndex = 14
        Me.bt6NewStep.Text = "New"
        Me.bt6NewStep.UseVisualStyleBackColor = True
        '
        'bt6EditStep
        '
        Me.bt6EditStep.Location = New System.Drawing.Point(294, 261)
        Me.bt6EditStep.Name = "bt6EditStep"
        Me.bt6EditStep.Size = New System.Drawing.Size(75, 23)
        Me.bt6EditStep.TabIndex = 13
        Me.bt6EditStep.Text = "Edit"
        Me.bt6EditStep.UseVisualStyleBackColor = True
        '
        'bt6DeleteStep
        '
        Me.bt6DeleteStep.Location = New System.Drawing.Point(375, 261)
        Me.bt6DeleteStep.Name = "bt6DeleteStep"
        Me.bt6DeleteStep.Size = New System.Drawing.Size(75, 23)
        Me.bt6DeleteStep.TabIndex = 12
        Me.bt6DeleteStep.Text = "Delete"
        Me.bt6DeleteStep.UseVisualStyleBackColor = True
        '
        'FrmSteps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 402)
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
        Me.PgOCR.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PgImgsToPdf.ResumeLayout(False)
        Me.gb3Patterns.ResumeLayout(False)
        Me.gb3Patterns.PerformLayout()
        Me.pgCustom.ResumeLayout(False)
        Me.gb4CustomProcess.ResumeLayout(False)
        Me.gb4CustomProcess.PerformLayout()
        Me.pgSplitPDFSize.ResumeLayout(False)
        Me.gb5Pattern.ResumeLayout(False)
        Me.gb5Pattern.PerformLayout()
        Me.pgBarcodeRecognition.ResumeLayout(False)
        Me.gb6Sections.ResumeLayout(False)
        CType(Me.dg6Sections, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents gb1Patterns As System.Windows.Forms.GroupBox
    Friend WithEvents tx1SubFolderRegex As System.Windows.Forms.TextBox
    Friend WithEvents tx1BarcodeRegex As System.Windows.Forms.TextBox
    Friend WithEvents tx1FileInRegex As System.Windows.Forms.TextBox
    Friend WithEvents tx1FileOutTemplate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents gb1Barcodes As System.Windows.Forms.GroupBox
    Friend WithEvents cx1Barcodes As System.Windows.Forms.CheckedListBox
    Friend WithEvents txRunOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tx2FileOutTemplate As System.Windows.Forms.TextBox
    Friend WithEvents pgCustom As System.Windows.Forms.TabPage
    Friend WithEvents gb4CustomProcess As System.Windows.Forms.GroupBox
    Friend WithEvents cb4CustomProcess As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents gb3Patterns As System.Windows.Forms.GroupBox
    Friend WithEvents cx3OCR As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents tx3FileOutTemplate As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cb1SplitMode As System.Windows.Forms.ComboBox
    Friend WithEvents pgSplitPDFSize As System.Windows.Forms.TabPage
    Friend WithEvents gb5Pattern As System.Windows.Forms.GroupBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents tx5FileTemplate As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents tx5Size As System.Windows.Forms.TextBox
    Friend WithEvents rbDrws As System.Windows.Forms.RadioButton
    Friend WithEvents rbDocs As System.Windows.Forms.RadioButton
    Friend WithEvents pgBarcodeRecognition As System.Windows.Forms.TabPage
    Friend WithEvents gb6Sections As System.Windows.Forms.GroupBox
    Friend WithEvents dg6Sections As System.Windows.Forms.DataGridView
    Friend WithEvents bt6NewStep As System.Windows.Forms.Button
    Friend WithEvents bt6EditStep As System.Windows.Forms.Button
    Friend WithEvents bt6DeleteStep As System.Windows.Forms.Button
End Class
