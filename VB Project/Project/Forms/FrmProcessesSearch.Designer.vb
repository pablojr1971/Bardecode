<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcessesSearch
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
        Me.btSelect = New System.Windows.Forms.Button()
        Me.btInsert = New System.Windows.Forms.Button()
        Me.btUpdate = New System.Windows.Forms.Button()
        Me.btDelete = New System.Windows.Forms.Button()
        Me.gbProcesses = New System.Windows.Forms.GroupBox()
        Me.dgProcesses = New System.Windows.Forms.DataGridView()
        Me.SearchGroupBox = New System.Windows.Forms.GroupBox()
        Me.btFilter = New System.Windows.Forms.Button()
        Me.txSearch = New System.Windows.Forms.TextBox()
        Me.lbNumber = New System.Windows.Forms.Label()
        Me.Number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Steps = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MainPanel.SuspendLayout()
        Me.gbProcesses.SuspendLayout()
        CType(Me.dgProcesses, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SearchGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainPanel
        '
        Me.MainPanel.AutoSize = True
        Me.MainPanel.Controls.Add(Me.btSelect)
        Me.MainPanel.Controls.Add(Me.btInsert)
        Me.MainPanel.Controls.Add(Me.btUpdate)
        Me.MainPanel.Controls.Add(Me.btDelete)
        Me.MainPanel.Controls.Add(Me.gbProcesses)
        Me.MainPanel.Controls.Add(Me.SearchGroupBox)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(490, 366)
        Me.MainPanel.TabIndex = 0
        '
        'btSelect
        '
        Me.btSelect.Location = New System.Drawing.Point(161, 331)
        Me.btSelect.Name = "btSelect"
        Me.btSelect.Size = New System.Drawing.Size(75, 23)
        Me.btSelect.TabIndex = 5
        Me.btSelect.Text = "Select"
        Me.btSelect.UseVisualStyleBackColor = True
        '
        'btInsert
        '
        Me.btInsert.Location = New System.Drawing.Point(242, 331)
        Me.btInsert.Name = "btInsert"
        Me.btInsert.Size = New System.Drawing.Size(75, 23)
        Me.btInsert.TabIndex = 4
        Me.btInsert.Text = "New"
        Me.btInsert.UseVisualStyleBackColor = True
        '
        'btUpdate
        '
        Me.btUpdate.Location = New System.Drawing.Point(323, 331)
        Me.btUpdate.Name = "btUpdate"
        Me.btUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btUpdate.TabIndex = 3
        Me.btUpdate.Text = "Edit"
        Me.btUpdate.UseVisualStyleBackColor = True
        '
        'btDelete
        '
        Me.btDelete.Location = New System.Drawing.Point(404, 331)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(75, 23)
        Me.btDelete.TabIndex = 2
        Me.btDelete.Text = "Delete"
        Me.btDelete.UseVisualStyleBackColor = True
        '
        'gbProcesses
        '
        Me.gbProcesses.Controls.Add(Me.dgProcesses)
        Me.gbProcesses.Location = New System.Drawing.Point(12, 64)
        Me.gbProcesses.Name = "gbProcesses"
        Me.gbProcesses.Size = New System.Drawing.Size(467, 261)
        Me.gbProcesses.TabIndex = 1
        Me.gbProcesses.TabStop = False
        Me.gbProcesses.Text = "Processes"
        '
        'dgProcesses
        '
        Me.dgProcesses.AllowUserToAddRows = False
        Me.dgProcesses.AllowUserToDeleteRows = False
        Me.dgProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgProcesses.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Number, Me.Steps, Me.Description, Me.Id})
        Me.dgProcesses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgProcesses.Location = New System.Drawing.Point(3, 16)
        Me.dgProcesses.Name = "dgProcesses"
        Me.dgProcesses.ReadOnly = True
        Me.dgProcesses.RowHeadersWidth = 20
        Me.dgProcesses.RowTemplate.ReadOnly = True
        Me.dgProcesses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgProcesses.ShowRowErrors = False
        Me.dgProcesses.Size = New System.Drawing.Size(461, 242)
        Me.dgProcesses.TabIndex = 0
        '
        'SearchGroupBox
        '
        Me.SearchGroupBox.Controls.Add(Me.btFilter)
        Me.SearchGroupBox.Controls.Add(Me.txSearch)
        Me.SearchGroupBox.Controls.Add(Me.lbNumber)
        Me.SearchGroupBox.Location = New System.Drawing.Point(12, 12)
        Me.SearchGroupBox.Name = "SearchGroupBox"
        Me.SearchGroupBox.Size = New System.Drawing.Size(467, 46)
        Me.SearchGroupBox.TabIndex = 0
        Me.SearchGroupBox.TabStop = False
        Me.SearchGroupBox.Text = "Search"
        '
        'btFilter
        '
        Me.btFilter.Location = New System.Drawing.Point(386, 17)
        Me.btFilter.Name = "btFilter"
        Me.btFilter.Size = New System.Drawing.Size(75, 23)
        Me.btFilter.TabIndex = 2
        Me.btFilter.Text = "Filter"
        Me.btFilter.UseVisualStyleBackColor = True
        '
        'txSearch
        '
        Me.txSearch.Location = New System.Drawing.Point(59, 19)
        Me.txSearch.Name = "txSearch"
        Me.txSearch.Size = New System.Drawing.Size(321, 20)
        Me.txSearch.TabIndex = 1
        '
        'lbNumber
        '
        Me.lbNumber.AutoSize = True
        Me.lbNumber.Location = New System.Drawing.Point(6, 22)
        Me.lbNumber.Name = "lbNumber"
        Me.lbNumber.Size = New System.Drawing.Size(47, 13)
        Me.lbNumber.TabIndex = 0
        Me.lbNumber.Text = "Number:"
        '
        'Number
        '
        Me.Number.DataPropertyName = "Number"
        Me.Number.HeaderText = "Number"
        Me.Number.Name = "Number"
        Me.Number.ReadOnly = True
        Me.Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.Number.Width = 50
        '
        'Steps
        '
        Me.Steps.DataPropertyName = "Steps"
        Me.Steps.HeaderText = "Steps"
        Me.Steps.Name = "Steps"
        Me.Steps.ReadOnly = True
        Me.Steps.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Steps.Width = 50
        '
        'Description
        '
        Me.Description.DataPropertyName = "Description"
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        Me.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Description.Width = 338
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Id.Visible = False
        '
        'SearchProcesses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 366)
        Me.Controls.Add(Me.MainPanel)
        Me.Name = "SearchProcesses"
        Me.Text = "Processes"
        Me.MainPanel.ResumeLayout(False)
        Me.gbProcesses.ResumeLayout(False)
        CType(Me.dgProcesses, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SearchGroupBox.ResumeLayout(False)
        Me.SearchGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainPanel As System.Windows.Forms.Panel
    Friend WithEvents SearchGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents btSelect As System.Windows.Forms.Button
    Friend WithEvents btInsert As System.Windows.Forms.Button
    Friend WithEvents btUpdate As System.Windows.Forms.Button
    Friend WithEvents btDelete As System.Windows.Forms.Button
    Friend WithEvents gbProcesses As System.Windows.Forms.GroupBox
    Friend WithEvents btFilter As System.Windows.Forms.Button
    Friend WithEvents txSearch As System.Windows.Forms.TextBox
    Friend WithEvents lbNumber As System.Windows.Forms.Label
    Friend WithEvents dgProcesses As System.Windows.Forms.DataGridView
    Friend WithEvents Number As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Steps As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
