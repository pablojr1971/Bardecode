Imports System.Data.Entity


Public Class FrmProcesses
    Private Entity As EProcess
    Private Inserting As Boolean = True
    Private ctx As VBProjectContext = New VBProjectContext()

    ' If no Id is received Start in Inserting Mode
    Public Sub New(ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()

        Entity = New EProcess
        dgSteps.DataSource = Entity.Steps
        RefreshGrid()

        Me.ShowDialog(ParentForm)
    End Sub

    ' If an Id is received Start in Edit Mode
    Public Sub New(ProcessId As Integer, ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()
        Inserting = False

        ctx.EProcesses.Where(Function(p) p.Id = ProcessId).Load()
        Entity = ctx.EProcesses.Single(Function(p) p.Id = ProcessId)
        ctx.ESteps.Where(Function(p) p.Process = ProcessId).Load()
        Entity.Steps = ctx.ESteps.Where(Function(p) p.Process = ProcessId).ToList()

        txNumber.Text = Entity.Number
        txDescription.Text = Entity.Description
        RefreshGrid()
        Me.ShowDialog(ParentForm)
    End Sub

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Close()
    End Sub

    Private Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Entity.Number = txNumber.Text
        Entity.Description = txDescription.Text

        If Inserting Then
            ctx.EProcesses.Add(Entity)
        End If

        ctx.SaveChanges()
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With New FrmSteps(Me)
            If Not (IsNothing(.EntityStep)) Then
                .EntityStep.Process = Me.Entity.Id
                Me.Entity.Steps.Add(.EntityStep)
            End If
        End With
        RefreshGrid()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click        
        With New FrmSteps(Me.Entity.Steps.Single(Function(p) (p.Id = 0 Or p.Id = dgSteps.CurrentRow.Cells("Id").Value) And p.RunOrder = dgSteps.CurrentRow.Cells("RunOrder").Value), Me)
            .Dispose()
        End With
        RefreshGrid()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("Are you sure?", MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.Yes Then
            If Entity.Id > 0 Then
                If ctx.ESteps.Local.Remove(Me.Entity.Steps.Single(Function(p) (p.Id = 0 Or p.Id = dgSteps.CurrentRow.Cells("Id").Value) And p.RunOrder = dgSteps.CurrentRow.Cells("RunOrder").Value)) Then
                    RefreshGrid()
                End If
            Else
                If Entity.Steps.Remove(Me.Entity.Steps.Single(Function(p) (p.Id = 0 Or p.Id = dgSteps.CurrentRow.Cells("Id").Value) And p.RunOrder = dgSteps.CurrentRow.Cells("RunOrder").Value)) Then
                    RefreshGrid()
                End If
            End If
        End If
    End Sub

    Private Sub RefreshGrid()
        If Not IsNothing(dgSteps.DataSource) Then
            dgSteps.DataSource.Clear()
        End If
        dgSteps.DataSource = (From p In Entity.Steps.Where(Function(p) p.Process = Entity.Id)
                              Select p.Id, StepType = CType(p.StepType, StepType), p.RunOrder).ToList()
    End Sub
End Class