Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Ghostscript.NET.Rasterizer
Imports Clock.Pdf
Imports Clock.Hocr

Public Class MainForm
    Private Profile As ProfileSettings

    Private Folders As List(Of DirectoryInfo) = New List(Of DirectoryInfo)()
    Private Files As List(Of FileInfo) = New List(Of FileInfo)()

    Enum LogType
        Begin
        ProcessingA4
        ProcessingDrawing
        Folder
        Done
    End Enum

    Private Sub RunButton_Click(sender As Object, e As EventArgs) Handles RunButton.Click

        Exit Sub

        Me.Log(LogType.Begin, "")
        ' Get the A4 folders
        Me.Log(LogType.ProcessingA4, CStr(Folders.Count))
        ' Get the Drawings folders
        Me.Log(LogType.ProcessingDrawing, CStr(Folders.Count))
        Me.Log(LogType.Done, "")
    End Sub

    Private Sub Log(LogType As LogType, data As String)

        ' this is a sub just to have a log on the main form of what the code is doing
        ' about the arguments of the sub, the index define the kind of log that we will write, and the data is what we will log
        ' I`ve made this sub to concentrate logs in just one place of the source

        Select Case LogType

            ' begining the process, the data will be the number of subfolders found
            Case MainForm.LogType.Begin
                Me.ProcessLogText.AppendText("----PROCESS BEGIN----")

            Case MainForm.LogType.ProcessingA4
                Me.ProcessLogText.AppendText(vbCrLf + vbCrLf + "Spliting A4 files, " + data + " boxes found" + vbCrLf + vbCrLf)

            Case MainForm.LogType.ProcessingDrawing
                Me.ProcessLogText.AppendText(vbCrLf + vbCrLf + "Spliting Drawing files, " + data + " boxes found" + vbCrLf + vbCrLf)

                ' log which folder is being processed at the moment, the data will be the folder
            Case MainForm.LogType.Folder
                Me.ProcessLogText.AppendText("Processing folder: " + vbCrLf + _
                                             "    " + data + vbCrLf)

                ' end of process, just to say done and that the process went well
            Case MainForm.LogType.Done
                Me.ProcessLogText.AppendText(vbCrLf + vbCrLf + "----PROCESS DONE----")

        End Select
    End Sub
End Class
