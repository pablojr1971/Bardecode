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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txProcess = New System.Windows.Forms.TextBox()
        Me.btRun = New System.Windows.Forms.Button()
        Me.txProcessLog = New System.Windows.Forms.RichTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btSelect = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Process:"
        '
        'txProcess
        '
        Me.txProcess.Location = New System.Drawing.Point(59, 11)
        Me.txProcess.Margin = New System.Windows.Forms.Padding(2)
        Me.txProcess.Name = "txProcess"
        Me.txProcess.ReadOnly = True
        Me.txProcess.Size = New System.Drawing.Size(291, 20)
        Me.txProcess.TabIndex = 2
        '
        'btRun
        '
        Me.btRun.Location = New System.Drawing.Point(417, 10)
        Me.btRun.Margin = New System.Windows.Forms.Padding(2)
        Me.btRun.Name = "btRun"
        Me.btRun.Size = New System.Drawing.Size(56, 24)
        Me.btRun.TabIndex = 4
        Me.btRun.Text = "Run"
        Me.btRun.UseVisualStyleBackColor = True
        '
        'txProcessLog
        '
        Me.txProcessLog.Location = New System.Drawing.Point(9, 57)
        Me.txProcessLog.Margin = New System.Windows.Forms.Padding(2)
        Me.txProcessLog.Name = "txProcessLog"
        Me.txProcessLog.ReadOnly = True
        Me.txProcessLog.Size = New System.Drawing.Size(464, 325)
        Me.txProcessLog.TabIndex = 5
        Me.txProcessLog.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 40)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Process Log:"
        '
        'btSelect
        '
        Me.btSelect.Location = New System.Drawing.Point(355, 10)
        Me.btSelect.Name = "btSelect"
        Me.btSelect.Size = New System.Drawing.Size(57, 23)
        Me.btSelect.TabIndex = 7
        Me.btSelect.Text = "Select"
        Me.btSelect.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 391)
        Me.Controls.Add(Me.btSelect)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txProcessLog)
        Me.Controls.Add(Me.btRun)
        Me.Controls.Add(Me.txProcess)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Process files"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txProcess As System.Windows.Forms.TextBox
    Friend WithEvents btRun As System.Windows.Forms.Button
    Friend WithEvents txProcessLog As System.Windows.Forms.RichTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btSelect As System.Windows.Forms.Button

End Class
