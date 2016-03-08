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
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tx1WhitelistChar = New System.Windows.Forms.TextBox()
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
        Me.ch2SaveHTML = New System.Windows.Forms.CheckBox()
        Me.ch2SaveImage = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.gb2Paths = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tx2OutputFolder = New System.Windows.Forms.TextBox()
        Me.tx2InputFolder = New System.Windows.Forms.TextBox()
        Me.PgImgsToPdf = New System.Windows.Forms.TabPage()
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
        Me.MainPanel.Size = New System.Drawing.Size(506, 483)
        Me.MainPanel.TabIndex = 0
        '
        'txRunOrder
        '
        Me.txRunOrder.Location = New System.Drawing.Point(77, 448)
        Me.txRunOrder.Name = "txRunOrder"
        Me.txRunOrder.Size = New System.Drawing.Size(46, 20)
        Me.txRunOrder.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 453)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Run Order:"
        '
        'btOk
        '
        Me.btOk.Location = New System.Drawing.Point(338, 448)
        Me.btOk.Name = "btOk"
        Me.btOk.Size = New System.Drawing.Size(75, 23)
        Me.btOk.TabIndex = 3
        Me.btOk.Text = "Ok"
        Me.btOk.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(419, 448)
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
        Me.gbStepSettings.Size = New System.Drawing.Size(482, 430)
        Me.gbStepSettings.TabIndex = 1
        Me.gbStepSettings.TabStop = False
        Me.gbStepSettings.Text = "Step Settings"
        '
        'tcSteps
        '
        Me.tcSteps.Controls.Add(Me.PgBardecode)
        Me.tcSteps.Controls.Add(Me.PgOCR)
        Me.tcSteps.Controls.Add(Me.PgImgsToPdf)
        Me.tcSteps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcSteps.Location = New System.Drawing.Point(3, 16)
        Me.tcSteps.Name = "tcSteps"
        Me.tcSteps.SelectedIndex = 0
        Me.tcSteps.Size = New System.Drawing.Size(476, 411)
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
        Me.PgBardecode.Size = New System.Drawing.Size(468, 385)
        Me.PgBardecode.TabIndex = 0
        Me.PgBardecode.Text = "Bardecode"
        Me.PgBardecode.UseVisualStyleBackColor = True
        '
        'gb1Barcodes
        '
        Me.gb1Barcodes.Controls.Add(Me.cx1Barcodes)
        Me.gb1Barcodes.Location = New System.Drawing.Point(6, 296)
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
        Me.gb1Patterns.Controls.Add(Me.Label10)
        Me.gb1Patterns.Controls.Add(Me.tx1WhitelistChar)
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
        Me.gb1Patterns.Size = New System.Drawing.Size(456, 153)
        Me.gb1Patterns.TabIndex = 1
        Me.gb1Patterns.TabStop = False
        Me.gb1Patterns.Text = "Patterns"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Whitelist Chars:"
        '
        'tx1WhitelistChar
        '
        Me.tx1WhitelistChar.Location = New System.Drawing.Point(104, 123)
        Me.tx1WhitelistChar.Name = "tx1WhitelistChar"
        Me.tx1WhitelistChar.Size = New System.Drawing.Size(346, 20)
        Me.tx1WhitelistChar.TabIndex = 9
        '
        'cx1SubFolders
        '
        Me.cx1SubFolders.AutoSize = True
        Me.cx1SubFolders.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cx1SubFolders.Checked = True
        Me.cx1SubFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cx1SubFolders.Location = New System.Drawing.Point(327, 100)
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
        Me.tx1SubFolderRegex.Size = New System.Drawing.Size(216, 20)
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
        Me.PgOCR.Controls.Add(Me.ch2SaveHTML)
        Me.PgOCR.Controls.Add(Me.ch2SaveImage)
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
        'ch2SaveHTML
        '
        Me.ch2SaveHTML.AutoSize = True
        Me.ch2SaveHTML.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ch2SaveHTML.Location = New System.Drawing.Point(219, 147)
        Me.ch2SaveHTML.Name = "ch2SaveHTML"
        Me.ch2SaveHTML.Size = New System.Drawing.Size(162, 17)
        Me.ch2SaveHTML.TabIndex = 4
        Me.ch2SaveHTML.Text = "Save each page HTML Files"
        Me.ch2SaveHTML.UseVisualStyleBackColor = True
        '
        'ch2SaveImage
        '
        Me.ch2SaveImage.AutoSize = True
        Me.ch2SaveImage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ch2SaveImage.Location = New System.Drawing.Point(6, 147)
        Me.ch2SaveImage.Name = "ch2SaveImage"
        Me.ch2SaveImage.Size = New System.Drawing.Size(163, 17)
        Me.ch2SaveImage.TabIndex = 3
        Me.ch2SaveImage.Text = "Save Each Page Image Files"
        Me.ch2SaveImage.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.TextBox5)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 89)
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
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(104, 19)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(346, 20)
        Me.TextBox5.TabIndex = 0
        '
        'gb2Paths
        '
        Me.gb2Paths.Controls.Add(Me.Label11)
        Me.gb2Paths.Controls.Add(Me.Label12)
        Me.gb2Paths.Controls.Add(Me.tx2OutputFolder)
        Me.gb2Paths.Controls.Add(Me.tx2InputFolder)
        Me.gb2Paths.Location = New System.Drawing.Point(6, 6)
        Me.gb2Paths.Name = "gb2Paths"
        Me.gb2Paths.Size = New System.Drawing.Size(456, 77)
        Me.gb2Paths.TabIndex = 0
        Me.gb2Paths.TabStop = False
        Me.gb2Paths.Text = "Process Paths"
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
        Me.PgImgsToPdf.Location = New System.Drawing.Point(4, 22)
        Me.PgImgsToPdf.Name = "PgImgsToPdf"
        Me.PgImgsToPdf.Padding = New System.Windows.Forms.Padding(3)
        Me.PgImgsToPdf.Size = New System.Drawing.Size(468, 385)
        Me.PgImgsToPdf.TabIndex = 2
        Me.PgImgsToPdf.Text = "ImgsToPdf"
        Me.PgImgsToPdf.UseVisualStyleBackColor = True
        '
        'FrmSteps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 483)
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
        Me.PgOCR.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gb2Paths.ResumeLayout(False)
        Me.gb2Paths.PerformLayout()
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
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tx1WhitelistChar As System.Windows.Forms.TextBox
    Friend WithEvents txRunOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gb2Paths As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tx2OutputFolder As System.Windows.Forms.TextBox
    Friend WithEvents tx2InputFolder As System.Windows.Forms.TextBox
    Friend WithEvents ch2SaveHTML As System.Windows.Forms.CheckBox
    Friend WithEvents ch2SaveImage As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
End Class
