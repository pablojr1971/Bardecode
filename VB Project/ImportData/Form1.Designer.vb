<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Process = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 43)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(439, 260)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = "Barcode=Barcode" & Global.Microsoft.VisualBasic.ChrW(10) & "Field1=Name" & Global.Microsoft.VisualBasic.ChrW(10) & "Field2=Registration" & Global.Microsoft.VisualBasic.ChrW(10) & "Field3=Agreement" & Global.Microsoft.VisualBasic.ChrW(10) & "Box=BOX0852652" & Global.Microsoft.VisualBasic.ChrW(10) & "J" & _
    "obNo=2025" & Global.Microsoft.VisualBasic.ChrW(10) & "Date1=Start date" & Global.Microsoft.VisualBasic.ChrW(10) & "Date2=End date"
        '
        'Process
        '
        Me.Process.Location = New System.Drawing.Point(376, 315)
        Me.Process.Name = "Process"
        Me.Process.Size = New System.Drawing.Size(75, 23)
        Me.Process.TabIndex = 1
        Me.Process.Text = "Process"
        Me.Process.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(439, 20)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Text = "C:\Users\User\Desktop\Hitachi_2025-H0852652-660\2025-H0852652-660\2025-H0852652.c" & _
    "sv"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(463, 350)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Process)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Process As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
