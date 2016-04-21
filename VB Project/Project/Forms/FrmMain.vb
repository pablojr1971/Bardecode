Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Ghostscript.NET.Rasterizer
Imports Clock.Pdf
Imports Clock.Hocr
Imports System.Text.RegularExpressions

Public Class FrmMain
    ' Variable to tell the log String to Remove the Last Line 
    ' if the String Starts with this the log function must threat the operation to remove the last line
    Public Const ScanDataStringConnection = "Server=Jerry;Database=other;User Id=sa;Password=569874123;"

    Private Process As Process
    Private _ProcessId As Integer

    Private Folders As List(Of DirectoryInfo) = New List(Of DirectoryInfo)()
    Private Files As List(Of FileInfo) = New List(Of FileInfo)()

    Private Sub btSelect_Click(sender As Object, e As EventArgs) Handles btSelect.Click
        With New FrmProcessesSearch(Me)
            txProcess.Text = .SelectedProcessName
            _ProcessId = .SelectedId
            .Dispose()
        End With
    End Sub

    Private Sub RunProcess() Handles btRun.Click
        If _ProcessId <= 0 Then
            MessageBox.Show("Select the process to run", "Process Missing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If (Trim(txJobNumber.Text) = "") Then
            If MessageBox.Show("Without Job Number, The process will run with the whole Input folder and not filter boxes that are already done, " + _
                               "Are you sure?", "Missing Job Number", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If
        End If

        Dim job As Integer = 0
        Integer.TryParse(txJobNumber.Text, job)
        Dim ProcessObj As Process = New Process(_ProcessId, job)

        btRun.Enabled = False
        txProcessLog.Clear()
        ProcessObj.Run(AddressOf writeLog)
    End Sub

    Private Sub writeLog(Log As String)
        txProcessLog.AppendText(Date.Now.ToShortTimeString + " - " + Log + vbCrLf)
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FrmMain.CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub CopyFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyFilesToolStripMenuItem.Click
        With New FrmCopyFiles(Me)
            .Dispose()
        End With
    End Sub

    Public Sub FinishProcess()
        btRun.Enabled = True
        txProcessLog.AppendText("PROCESS DONE")
    End Sub
End Class
