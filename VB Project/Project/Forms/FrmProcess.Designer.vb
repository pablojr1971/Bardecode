<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcess
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
        Me.gbProcess = New System.Windows.Forms.GroupBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.bt6NewStep = New System.Windows.Forms.Button()
        Me.bt6EditStep = New System.Windows.Forms.Button()
        Me.bt6DeleteStep = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.SectionName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Regex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Required = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Multiple = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btOk = New System.Windows.Forms.Button()
        Me.btCancel = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.gbProcess.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbProcess
        '
        Me.gbProcess.Controls.Add(Me.TextBox8)
        Me.gbProcess.Controls.Add(Me.Label9)
        Me.gbProcess.Controls.Add(Me.CheckBox5)
        Me.gbProcess.Controls.Add(Me.CheckBox4)
        Me.gbProcess.Controls.Add(Me.CheckBox3)
        Me.gbProcess.Controls.Add(Me.CheckBox2)
        Me.gbProcess.Controls.Add(Me.CheckBox1)
        Me.gbProcess.Controls.Add(Me.Label4)
        Me.gbProcess.Controls.Add(Me.Label3)
        Me.gbProcess.Controls.Add(Me.Label2)
        Me.gbProcess.Controls.Add(Me.Label1)
        Me.gbProcess.Controls.Add(Me.TextBox4)
        Me.gbProcess.Controls.Add(Me.TextBox3)
        Me.gbProcess.Controls.Add(Me.TextBox2)
        Me.gbProcess.Controls.Add(Me.TextBox1)
        Me.gbProcess.Location = New System.Drawing.Point(12, 12)
        Me.gbProcess.Name = "gbProcess"
        Me.gbProcess.Size = New System.Drawing.Size(625, 175)
        Me.gbProcess.TabIndex = 0
        Me.gbProcess.TabStop = False
        Me.gbProcess.Text = "Process"
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(358, 123)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(261, 17)
        Me.CheckBox5.TabIndex = 12
        Me.CheckBox5.Text = "Ask to fix Missing Drawings (Will stop the process)"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(223, 146)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(96, 17)
        Me.CheckBox4.TabIndex = 11
        Me.CheckBox4.Text = "OCR Drawings"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(223, 123)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(83, 17)
        Me.CheckBox3.TabIndex = 10
        Me.CheckBox3.Text = "OCR A4/A3"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(98, 146)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(91, 17)
        Me.CheckBox2.TabIndex = 9
        Me.CheckBox2.Text = "B&W Drawings"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(98, 123)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(78, 17)
        Me.CheckBox1.TabIndex = 8
        Me.CheckBox1.Text = "B&W A4/A3"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Output Folder:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Drawings Folder:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Input Folder:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Name:"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(98, 97)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(521, 20)
        Me.TextBox4.TabIndex = 3
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(98, 71)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(521, 20)
        Me.TextBox3.TabIndex = 2
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(98, 45)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(521, 20)
        Me.TextBox2.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(98, 19)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(360, 20)
        Me.TextBox1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GroupBox1.Controls.Add(Me.bt6NewStep)
        Me.GroupBox1.Controls.Add(Me.bt6EditStep)
        Me.GroupBox1.Controls.Add(Me.bt6DeleteStep)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 336)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(625, 216)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sections"
        '
        'bt6NewStep
        '
        Me.bt6NewStep.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bt6NewStep.Location = New System.Drawing.Point(382, 181)
        Me.bt6NewStep.Name = "bt6NewStep"
        Me.bt6NewStep.Size = New System.Drawing.Size(75, 23)
        Me.bt6NewStep.TabIndex = 17
        Me.bt6NewStep.Text = "New"
        Me.bt6NewStep.UseVisualStyleBackColor = True
        '
        'bt6EditStep
        '
        Me.bt6EditStep.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bt6EditStep.Location = New System.Drawing.Point(463, 181)
        Me.bt6EditStep.Name = "bt6EditStep"
        Me.bt6EditStep.Size = New System.Drawing.Size(75, 23)
        Me.bt6EditStep.TabIndex = 16
        Me.bt6EditStep.Text = "Edit"
        Me.bt6EditStep.UseVisualStyleBackColor = True
        '
        'bt6DeleteStep
        '
        Me.bt6DeleteStep.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bt6DeleteStep.Location = New System.Drawing.Point(544, 181)
        Me.bt6DeleteStep.Name = "bt6DeleteStep"
        Me.bt6DeleteStep.Size = New System.Drawing.Size(75, 23)
        Me.bt6DeleteStep.TabIndex = 15
        Me.bt6DeleteStep.Text = "Delete"
        Me.bt6DeleteStep.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SectionName, Me.Regex, Me.Id, Me.Required, Me.Multiple})
        Me.DataGridView1.Location = New System.Drawing.Point(6, 21)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(613, 154)
        Me.DataGridView1.TabIndex = 0
        '
        'SectionName
        '
        Me.SectionName.DataPropertyName = "SectionName"
        Me.SectionName.HeaderText = "Section Name"
        Me.SectionName.Name = "SectionName"
        Me.SectionName.ReadOnly = True
        Me.SectionName.Width = 240
        '
        'Regex
        '
        Me.Regex.DataPropertyName = "Regex"
        Me.Regex.HeaderText = "Regex"
        Me.Regex.Name = "Regex"
        Me.Regex.ReadOnly = True
        Me.Regex.Width = 150
        '
        'Id
        '
        Me.Id.HeaderText = "Section Id"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        Me.Id.Width = 5
        '
        'Required
        '
        Me.Required.HeaderText = "Required"
        Me.Required.Name = "Required"
        Me.Required.ReadOnly = True
        '
        'Multiple
        '
        Me.Multiple.HeaderText = "Multiple"
        Me.Multiple.Name = "Multiple"
        Me.Multiple.ReadOnly = True
        '
        'btOk
        '
        Me.btOk.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btOk.Location = New System.Drawing.Point(481, 559)
        Me.btOk.Name = "btOk"
        Me.btOk.Size = New System.Drawing.Size(75, 23)
        Me.btOk.TabIndex = 5
        Me.btOk.Text = "Ok"
        Me.btOk.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btCancel.Location = New System.Drawing.Point(562, 559)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 6
        Me.btCancel.Text = "Cancel"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.TextBox9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.TextBox7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.TextBox6)
        Me.GroupBox2.Controls.Add(Me.TextBox5)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 193)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(625, 134)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "File"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(381, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(228, 78)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "For the output name/folder you can use these " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "options:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[FNAME] = File original " & _
    "Name" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[FBARCODE] = File Barcode" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[DFIELD1] = Database Field1 (Scandata fields)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
    "Can be used with all scandata fields"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Folder Name:"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(98, 71)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(277, 20)
        Me.TextBox7.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Output Name:"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(98, 45)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(277, 20)
        Me.TextBox6.TabIndex = 6
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(98, 19)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(277, 20)
        Me.TextBox5.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "File Start Regex:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(464, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "JobNo:"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(511, 19)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(108, 20)
        Me.TextBox8.TabIndex = 14
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(98, 97)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(100, 20)
        Me.TextBox9.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "File Size(Split):"
        '
        'FrmProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 595)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btOk)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.gbProcess)
        Me.Name = "FrmProcess"
        Me.Text = "Process"
        Me.gbProcess.ResumeLayout(False)
        Me.gbProcess.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbProcess As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents bt6NewStep As System.Windows.Forms.Button
    Friend WithEvents bt6EditStep As System.Windows.Forms.Button
    Friend WithEvents bt6DeleteStep As System.Windows.Forms.Button
    Friend WithEvents btOk As System.Windows.Forms.Button
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Private WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents SectionName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Regex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Required As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Multiple As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
End Class
