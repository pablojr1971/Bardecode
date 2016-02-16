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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProfileText = New System.Windows.Forms.TextBox()
        Me.RunButton = New System.Windows.Forms.Button()
        Me.ProcessLogText = New System.Windows.Forms.RichTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 8)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Profile:"
        '
        'ProfileText
        '
        Me.ProfileText.Location = New System.Drawing.Point(50, 5)
        Me.ProfileText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ProfileText.Name = "ProfileText"
        Me.ProfileText.Size = New System.Drawing.Size(422, 20)
        Me.ProfileText.TabIndex = 2
        '
        'RunButton
        '
        Me.RunButton.Location = New System.Drawing.Point(417, 29)
        Me.RunButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RunButton.Name = "RunButton"
        Me.RunButton.Size = New System.Drawing.Size(56, 24)
        Me.RunButton.TabIndex = 4
        Me.RunButton.Text = "Run"
        Me.RunButton.UseVisualStyleBackColor = True
        '
        'ProcessLogText
        '
        Me.ProcessLogText.Location = New System.Drawing.Point(9, 57)
        Me.ProcessLogText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ProcessLogText.Name = "ProcessLogText"
        Me.ProcessLogText.ReadOnly = True
        Me.ProcessLogText.Size = New System.Drawing.Size(464, 325)
        Me.ProcessLogText.TabIndex = 5
        Me.ProcessLogText.Text = ""
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
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 391)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ProcessLogText)
        Me.Controls.Add(Me.RunButton)
        Me.Controls.Add(Me.ProfileText)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "MainForm"
        Me.Text = "Process files"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ProfileText As System.Windows.Forms.TextBox
    Friend WithEvents RunButton As System.Windows.Forms.Button
    Friend WithEvents ProcessLogText As System.Windows.Forms.RichTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
