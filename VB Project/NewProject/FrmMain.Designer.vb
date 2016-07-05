<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txJobNumber = New System.Windows.Forms.TextBox()
        Me.btSelect = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txProcessLog = New System.Windows.Forms.RichTextBox()
        Me.btRun = New System.Windows.Forms.Button()
        Me.txProcess = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txJobNumber)
        Me.Panel1.Controls.Add(Me.btSelect)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txProcessLog)
        Me.Panel1.Controls.Add(Me.btRun)
        Me.Panel1.Controls.Add(Me.txProcess)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(502, 548)
        Me.Panel1.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Job No.:"
        '
        'txJobNumber
        '
        Me.txJobNumber.Location = New System.Drawing.Point(63, 37)
        Me.txJobNumber.Name = "txJobNumber"
        Me.txJobNumber.Size = New System.Drawing.Size(75, 20)
        Me.txJobNumber.TabIndex = 14
        '
        'btSelect
        '
        Me.btSelect.Location = New System.Drawing.Point(432, 10)
        Me.btSelect.Name = "btSelect"
        Me.btSelect.Size = New System.Drawing.Size(57, 23)
        Me.btSelect.TabIndex = 13
        Me.btSelect.Text = "Select"
        Me.btSelect.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 82)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Process Log:"
        '
        'txProcessLog
        '
        Me.txProcessLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txProcessLog.Location = New System.Drawing.Point(13, 97)
        Me.txProcessLog.Margin = New System.Windows.Forms.Padding(2)
        Me.txProcessLog.Name = "txProcessLog"
        Me.txProcessLog.ReadOnly = True
        Me.txProcessLog.Size = New System.Drawing.Size(478, 440)
        Me.txProcessLog.TabIndex = 11
        Me.txProcessLog.Text = ""
        '
        'btRun
        '
        Me.btRun.Location = New System.Drawing.Point(433, 37)
        Me.btRun.Margin = New System.Windows.Forms.Padding(2)
        Me.btRun.Name = "btRun"
        Me.btRun.Size = New System.Drawing.Size(56, 24)
        Me.btRun.TabIndex = 10
        Me.btRun.Text = "Run"
        Me.btRun.UseVisualStyleBackColor = True
        '
        'txProcess
        '
        Me.txProcess.Location = New System.Drawing.Point(63, 12)
        Me.txProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.txProcess.Name = "txProcess"
        Me.txProcess.ReadOnly = True
        Me.txProcess.Size = New System.Drawing.Size(350, 20)
        Me.txProcess.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Process:"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 548)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmMain"
        Me.Text = "Processing"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txJobNumber As System.Windows.Forms.TextBox
    Friend WithEvents btSelect As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txProcessLog As System.Windows.Forms.RichTextBox
    Friend WithEvents btRun As System.Windows.Forms.Button
    Friend WithEvents txProcess As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
