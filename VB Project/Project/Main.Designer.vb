<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.JobPathText = New System.Windows.Forms.TextBox()
        Me.RunButton = New System.Windows.Forms.Button()
        Me.ProcessLogText = New System.Windows.Forms.RichTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Job Folder:"
        '
        'JobPathText
        '
        Me.JobPathText.Location = New System.Drawing.Point(97, 6)
        Me.JobPathText.Name = "JobPathText"
        Me.JobPathText.Size = New System.Drawing.Size(536, 22)
        Me.JobPathText.TabIndex = 2
        '
        'RunButton
        '
        Me.RunButton.Location = New System.Drawing.Point(558, 34)
        Me.RunButton.Name = "RunButton"
        Me.RunButton.Size = New System.Drawing.Size(75, 29)
        Me.RunButton.TabIndex = 4
        Me.RunButton.Text = "Run"
        Me.RunButton.UseVisualStyleBackColor = True
        '
        'ProcessLogText
        '
        Me.ProcessLogText.Location = New System.Drawing.Point(12, 79)
        Me.ProcessLogText.Name = "ProcessLogText"
        Me.ProcessLogText.ReadOnly = True
        Me.ProcessLogText.Size = New System.Drawing.Size(618, 390)
        Me.ProcessLogText.TabIndex = 5
        Me.ProcessLogText.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Process Log:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 481)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ProcessLogText)
        Me.Controls.Add(Me.RunButton)
        Me.Controls.Add(Me.JobPathText)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Process files"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents JobPathText As System.Windows.Forms.TextBox
    Friend WithEvents RunButton As System.Windows.Forms.Button
    Friend WithEvents ProcessLogText As System.Windows.Forms.RichTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
