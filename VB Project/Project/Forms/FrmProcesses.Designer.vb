<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcesses
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
        Me.btOk = New System.Windows.Forms.Button()
        Me.btCancel = New System.Windows.Forms.Button()
        Me.gbSteps = New System.Windows.Forms.GroupBox()
        Me.btNewStep = New System.Windows.Forms.Button()
        Me.btEditStep = New System.Windows.Forms.Button()
        Me.btDeleteStep = New System.Windows.Forms.Button()
        Me.dgSteps = New System.Windows.Forms.DataGridView()
        Me.RunOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbProcessInfo = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txDescription = New System.Windows.Forms.TextBox()
        Me.txNumber = New System.Windows.Forms.TextBox()
        Me.MainPanel.SuspendLayout()
        Me.gbSteps.SuspendLayout()
        CType(Me.dgSteps, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbProcessInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.btOk)
        Me.MainPanel.Controls.Add(Me.btCancel)
        Me.MainPanel.Controls.Add(Me.gbSteps)
        Me.MainPanel.Controls.Add(Me.gbProcessInfo)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(506, 448)
        Me.MainPanel.TabIndex = 0
        '
        'btOk
        '
        Me.btOk.Location = New System.Drawing.Point(338, 413)
        Me.btOk.Name = "btOk"
        Me.btOk.Size = New System.Drawing.Size(75, 23)
        Me.btOk.TabIndex = 7
        Me.btOk.Text = "Ok"
        Me.btOk.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(419, 413)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 6
        Me.btCancel.Text = "Cancel"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'gbSteps
        '
        Me.gbSteps.Controls.Add(Me.btNewStep)
        Me.gbSteps.Controls.Add(Me.btEditStep)
        Me.gbSteps.Controls.Add(Me.btDeleteStep)
        Me.gbSteps.Controls.Add(Me.dgSteps)
        Me.gbSteps.Location = New System.Drawing.Point(12, 89)
        Me.gbSteps.Name = "gbSteps"
        Me.gbSteps.Size = New System.Drawing.Size(482, 318)
        Me.gbSteps.TabIndex = 1
        Me.gbSteps.TabStop = False
        Me.gbSteps.Text = "Steps"
        '
        'btNewStep
        '
        Me.btNewStep.Location = New System.Drawing.Point(238, 289)
        Me.btNewStep.Name = "btNewStep"
        Me.btNewStep.Size = New System.Drawing.Size(75, 23)
        Me.btNewStep.TabIndex = 11
        Me.btNewStep.Text = "New"
        Me.btNewStep.UseVisualStyleBackColor = True
        '
        'btEditStep
        '
        Me.btEditStep.Location = New System.Drawing.Point(319, 289)
        Me.btEditStep.Name = "btEditStep"
        Me.btEditStep.Size = New System.Drawing.Size(75, 23)
        Me.btEditStep.TabIndex = 10
        Me.btEditStep.Text = "Edit"
        Me.btEditStep.UseVisualStyleBackColor = True
        '
        'btDeleteStep
        '
        Me.btDeleteStep.Location = New System.Drawing.Point(400, 289)
        Me.btDeleteStep.Name = "btDeleteStep"
        Me.btDeleteStep.Size = New System.Drawing.Size(75, 23)
        Me.btDeleteStep.TabIndex = 9
        Me.btDeleteStep.Text = "Delete"
        Me.btDeleteStep.UseVisualStyleBackColor = True
        '
        'dgSteps
        '
        Me.dgSteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSteps.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RunOrder, Me.Type, Me.Id})
        Me.dgSteps.Location = New System.Drawing.Point(6, 19)
        Me.dgSteps.Name = "dgSteps"
        Me.dgSteps.RowHeadersWidth = 20
        Me.dgSteps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgSteps.Size = New System.Drawing.Size(469, 264)
        Me.dgSteps.TabIndex = 0
        '
        'RunOrder
        '
        Me.RunOrder.DataPropertyName = "RunOrder"
        Me.RunOrder.HeaderText = "Order"
        Me.RunOrder.Name = "RunOrder"
        Me.RunOrder.ReadOnly = True
        '
        'Type
        '
        Me.Type.DataPropertyName = "StepType"
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.ReadOnly = True
        Me.Type.Width = 347
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        '
        'gbProcessInfo
        '
        Me.gbProcessInfo.Controls.Add(Me.Label2)
        Me.gbProcessInfo.Controls.Add(Me.Label1)
        Me.gbProcessInfo.Controls.Add(Me.txDescription)
        Me.gbProcessInfo.Controls.Add(Me.txNumber)
        Me.gbProcessInfo.Location = New System.Drawing.Point(12, 12)
        Me.gbProcessInfo.Name = "gbProcessInfo"
        Me.gbProcessInfo.Size = New System.Drawing.Size(482, 71)
        Me.gbProcessInfo.TabIndex = 0
        Me.gbProcessInfo.TabStop = False
        Me.gbProcessInfo.Text = "Process Info"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Description:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Number:"
        '
        'txDescription
        '
        Me.txDescription.Location = New System.Drawing.Point(75, 45)
        Me.txDescription.Name = "txDescription"
        Me.txDescription.Size = New System.Drawing.Size(400, 20)
        Me.txDescription.TabIndex = 1
        '
        'txNumber
        '
        Me.txNumber.Location = New System.Drawing.Point(75, 19)
        Me.txNumber.Name = "txNumber"
        Me.txNumber.Size = New System.Drawing.Size(100, 20)
        Me.txNumber.TabIndex = 0
        '
        'FrmProcesses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 448)
        Me.Controls.Add(Me.MainPanel)
        Me.Name = "FrmProcesses"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Process"
        Me.MainPanel.ResumeLayout(False)
        Me.gbSteps.ResumeLayout(False)
        CType(Me.dgSteps, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbProcessInfo.ResumeLayout(False)
        Me.gbProcessInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainPanel As System.Windows.Forms.Panel
    Friend WithEvents gbProcessInfo As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txDescription As System.Windows.Forms.TextBox
    Friend WithEvents txNumber As System.Windows.Forms.TextBox
    Friend WithEvents gbSteps As System.Windows.Forms.GroupBox
    Friend WithEvents btOk As System.Windows.Forms.Button
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents dgSteps As System.Windows.Forms.DataGridView
    Friend WithEvents btNewStep As System.Windows.Forms.Button
    Friend WithEvents btEditStep As System.Windows.Forms.Button
    Friend WithEvents btDeleteStep As System.Windows.Forms.Button
    Friend WithEvents RunOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
