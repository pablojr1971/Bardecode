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
    Public Const sRMLL As String = "%RMLL%"

    Private Process As Process
    Private _ProcessId As Integer

    Private Folders As List(Of DirectoryInfo) = New List(Of DirectoryInfo)()
    Private Files As List(Of FileInfo) = New List(Of FileInfo)()

    Private Sub RunButton_Click(sender As Object, e As EventArgs) Handles btRun.Click
        ' need to create a process object 
        ' create a instance of the process class
        ' load the steps of the process 
        ' invoke the method run for each step in the step list
        ' generate a log file registering everything that the process are doing
        ' This method should handle exceptions with a try catch because it will be the main engine
        txProcessLog.Clear()
        RunProcess()
    End Sub

    Private Sub btSelect_Click(sender As Object, e As EventArgs) Handles btSelect.Click
        With New FrmProcessesSearch(Me)
            txProcess.Text = .SelectedProcessName
            _ProcessId = .SelectedId
            .Dispose()
        End With
    End Sub

    Private Sub RunProcess()
        Dim ProcessObj As Process = New Process(_ProcessId)

        ' If we want to write the log in a file, or in another place 
        ' we just need to change this delegate function and pass one that 
        ' do what we want
        btRun.Enabled = False
        ProcessObj.Run(AddressOf writeLog)
    End Sub

    Private Sub writeLog(Log As String)
        ' This sub should just write a string into the log edit
        ' this sub will be passed as a delegate to the step objects to log the process    
        ' We could ReWrite this function to Write the logs on a text file,
        ' so if we need to run the process in a assync mode or in a separated thred without visual elements, we can!        
        UpdateText(Log)
    End Sub

    Private Sub UpdateText(text As String)
        If text.Contains(sRMLL) Then
            txProcessLog.Lines(txProcessLog.Lines.Count()) = Date.Now.ToShortTimeString + " - " + text.Replace(sRMLL, "")
        Else
            txProcessLog.AppendText(Date.Now.ToShortTimeString + " - " + text + vbCrLf)
        End If
    End Sub

    Public Sub EnableRun()
        btRun.Enabled = True
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FrmMain.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
