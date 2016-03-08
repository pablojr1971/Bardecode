Public Class FrmProcessesSearch
    Private _SelectedId As Integer
    Private _SelectedProcessName As String
    Private ctx As VBProjectContext = New VBProjectContext()

    Public ReadOnly Property SelectedId As Integer
        Get
            Return _SelectedId
        End Get
    End Property

    Public ReadOnly Property SelectedProcessName As String
        Get
            Return _SelectedProcessName
        End Get
    End Property

    Public Sub New(ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()
        Me.ShowDialog(ParentForm)
    End Sub

    Private Sub btFilter_Click(sender As Object, e As EventArgs) Handles btFilter.Click
        RefreshGrid()
    End Sub

    Private Sub btSelect_Click(sender As Object, e As EventArgs) Handles btSelect.Click
        Me._SelectedId = dgProcesses.CurrentRow.Cells("Id").Value
        Me._SelectedProcessName = dgProcesses.CurrentRow.Cells("Description").Value
        Me.Close()
    End Sub

    Private Sub btInsert_Click(sender As Object, e As EventArgs) Handles btInsert.Click
        With New FrmProcesses(Me)
            .Dispose()
        End With
        RefreshGrid()
    End Sub

    Private Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        Me._SelectedId = dgProcesses.CurrentRow.Cells("Id").Value
        With New FrmProcesses(Me._SelectedId, Me)
            .Dispose()
        End With
        RefreshGrid()
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        Me._SelectedId = dgProcesses.CurrentRow.Cells("Id").Value
        If MsgBox("Are you sure?", MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.Yes Then
            ctx.ESteps.RemoveRange(From p In ctx.ESteps Where p.Process = _SelectedId)
            ctx.EProcesses.RemoveRange(From p In ctx.EProcesses Where p.Id = _SelectedId)
            ctx.SaveChanges()
        End If
        RefreshGrid()
    End Sub

    Private Sub RefreshGrid()
        Dim Processes = From p In ctx.EProcesses
                        Where p.Number.Contains(txSearch.Text)
                        Select p.Number, p.Id, p.Description, Steps = p.Steps.Count
                        Order By Number Ascending
        Me.gbProcesses.Text = "Processes: " + CStr(Processes.Count())
        Me.dgProcesses.DataSource = Processes.ToList()
    End Sub

    Private Sub FrmProcessesSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshGrid()
    End Sub
End Class