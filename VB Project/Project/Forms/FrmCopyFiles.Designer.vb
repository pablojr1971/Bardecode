<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCopyFiles
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
        Me.txLog = New System.Windows.Forms.RichTextBox()
        Me.txFrom = New System.Windows.Forms.TextBox()
        Me.txToDocs = New System.Windows.Forms.TextBox()
        Me.txToDrawings = New System.Windows.Forms.TextBox()
        Me.btCopy = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txProcessed = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txLog
        '
        Me.txLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txLog.Location = New System.Drawing.Point(12, 167)
        Me.txLog.Name = "txLog"
        Me.txLog.ReadOnly = True
        Me.txLog.Size = New System.Drawing.Size(558, 199)
        Me.txLog.TabIndex = 0
        Me.txLog.Text = ""
        '
        'txFrom
        '
        Me.txFrom.Location = New System.Drawing.Point(127, 12)
        Me.txFrom.Name = "txFrom"
        Me.txFrom.Size = New System.Drawing.Size(375, 20)
        Me.txFrom.TabIndex = 1
        Me.txFrom.Text = "\\jerry\d\2001-Wolverhampton Comissions\Docs as Scanned"
        '
        'txToDocs
        '
        Me.txToDocs.Location = New System.Drawing.Point(127, 38)
        Me.txToDocs.Name = "txToDocs"
        Me.txToDocs.Size = New System.Drawing.Size(375, 20)
        Me.txToDocs.TabIndex = 2
        Me.txToDocs.Text = "\\geoff\Processing VB\Docs as Scanned"
        '
        'txToDrawings
        '
        Me.txToDrawings.Location = New System.Drawing.Point(127, 64)
        Me.txToDrawings.Name = "txToDrawings"
        Me.txToDrawings.Size = New System.Drawing.Size(375, 20)
        Me.txToDrawings.TabIndex = 3
        Me.txToDrawings.Text = "\\geoff\Processing VB\Drawings as Scanned"
        '
        'btCopy
        '
        Me.btCopy.Location = New System.Drawing.Point(251, 120)
        Me.btCopy.Name = "btCopy"
        Me.btCopy.Size = New System.Drawing.Size(75, 23)
        Me.btCopy.TabIndex = 4
        Me.btCopy.Text = "Copy"
        Me.btCopy.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "To Documents"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "To Drawings"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Processed"
        '
        'txProcessed
        '
        Me.txProcessed.Location = New System.Drawing.Point(127, 90)
        Me.txProcessed.Name = "txProcessed"
        Me.txProcessed.Size = New System.Drawing.Size(375, 20)
        Me.txProcessed.TabIndex = 8
        Me.txProcessed.Text = "\\geoff\Processing VB\Processed"
        '
        'FrmCopyFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(582, 378)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txProcessed)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btCopy)
        Me.Controls.Add(Me.txToDrawings)
        Me.Controls.Add(Me.txToDocs)
        Me.Controls.Add(Me.txFrom)
        Me.Controls.Add(Me.txLog)
        Me.Name = "FrmCopyFiles"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FrmCopyFiles"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txLog As System.Windows.Forms.RichTextBox
    Friend WithEvents txFrom As System.Windows.Forms.TextBox
    Friend WithEvents txToDocs As System.Windows.Forms.TextBox
    Friend WithEvents txToDrawings As System.Windows.Forms.TextBox
    Friend WithEvents btCopy As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txProcessed As System.Windows.Forms.TextBox
End Class
