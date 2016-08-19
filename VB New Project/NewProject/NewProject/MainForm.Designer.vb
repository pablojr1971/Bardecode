<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.txProcessLog = New System.Windows.Forms.RichTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PlayButton = New System.Windows.Forms.Button()
        Me.OpenButton = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.XMLDialog = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txProcessLog
        '
        Me.txProcessLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txProcessLog.Location = New System.Drawing.Point(12, 70)
        Me.txProcessLog.Name = "txProcessLog"
        Me.txProcessLog.ReadOnly = True
        Me.txProcessLog.Size = New System.Drawing.Size(542, 491)
        Me.txProcessLog.TabIndex = 0
        Me.txProcessLog.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PlayButton)
        Me.GroupBox1.Controls.Add(Me.OpenButton)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(542, 52)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select the XML Setup File"
        '
        'PlayButton
        '
        Me.PlayButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PlayButton.Location = New System.Drawing.Point(508, 24)
        Me.PlayButton.Name = "PlayButton"
        Me.PlayButton.Size = New System.Drawing.Size(28, 23)
        Me.PlayButton.TabIndex = 2
        Me.PlayButton.Text = ">"
        Me.PlayButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.PlayButton.UseVisualStyleBackColor = True
        '
        'OpenButton
        '
        Me.OpenButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpenButton.Location = New System.Drawing.Point(475, 24)
        Me.OpenButton.Name = "OpenButton"
        Me.OpenButton.Size = New System.Drawing.Size(28, 23)
        Me.OpenButton.TabIndex = 1
        Me.OpenButton.Text = "..."
        Me.OpenButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.OpenButton.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 26)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(463, 20)
        Me.TextBox1.TabIndex = 0
        '
        'XMLDialog
        '
        Me.XMLDialog.FileName = "XMLDialog"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(566, 573)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txProcessLog)
        Me.Name = "MainForm"
        Me.Text = "Main Form"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txProcessLog As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents PlayButton As System.Windows.Forms.Button
    Friend WithEvents OpenButton As System.Windows.Forms.Button
    Friend WithEvents XMLDialog As System.Windows.Forms.OpenFileDialog

End Class
