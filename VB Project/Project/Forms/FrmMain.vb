Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Ghostscript.NET.Rasterizer
Imports Clock.Pdf
Imports Clock.Hocr

Public Class FrmMain
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
        ' test the bardecode step class

        Dim bardecode As New StepBardecode()
        With bardecode.BardecodeProperties
            .BarcodeTypes.AddRange({BarcodeType.Code_128, BarcodeType.Code_2_of_5, BarcodeType.Code_3_of_9})
            .InputFolder = "c:\vb_projects\box19268\docasscanned"
            .MinimumBarcodeSize = 4
            .MaximumBarcodeSize = 99
            .OutputFolder = "c:\vb_projects\box19268\test"
            .BarcodePattern = "^(sut)[0-9]{6}$"
        End With

        bardecode.RunFolder(New DirectoryInfo("c:\vb_projects\box19268\docasscanned"), True, "")
        MessageBox.Show("done bardecode")

        Dim OCR As StepOCR = New StepOCR()
        OCR.RunFolder(New DirectoryInfo("C:\VB_Projects\Box19268\test"), True, "")

        MessageBox.Show("OCR Done")

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
            Case FrmMain.LogType.Begin
                Me.ProcessLogText.AppendText("----PROCESS BEGIN----")

            Case FrmMain.LogType.ProcessingA4
                Me.ProcessLogText.AppendText(vbCrLf + vbCrLf + "Spliting A4 files, " + data + " boxes found" + vbCrLf + vbCrLf)

            Case FrmMain.LogType.ProcessingDrawing
                Me.ProcessLogText.AppendText(vbCrLf + vbCrLf + "Spliting Drawing files, " + data + " boxes found" + vbCrLf + vbCrLf)

                ' log which folder is being processed at the moment, the data will be the folder
            Case FrmMain.LogType.Folder
                Me.ProcessLogText.AppendText("Processing folder: " + vbCrLf + _
                                             "    " + data + vbCrLf)

                ' end of process, just to say done and that the process went well
            Case FrmMain.LogType.Done
                Me.ProcessLogText.AppendText(vbCrLf + vbCrLf + "----PROCESS DONE----")

        End Select
    End Sub
End Class
