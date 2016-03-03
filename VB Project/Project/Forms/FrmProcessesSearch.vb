Public Class FrmProcessesSearch
    Private _SelectedId As Integer
    Private ctx As VBProjectContext = New VBProjectContext()

    Public ReadOnly Property SelectedId As Integer
        Get
            Return _SelectedId
        End Get
    End Property


    Private Sub btFilter_Click(sender As Object, e As EventArgs) Handles btFilter.Click
        RefreshGrid()
    End Sub

    Private Sub btSelect_Click(sender As Object, e As EventArgs) Handles btSelect.Click
        Me._SelectedId = dgProcesses.CurrentRow.Cells("Id").Value
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
End Class