Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.Advanced
Imports PdfSharp.Pdf.IO
Imports PdfSharp.Drawing
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

    Enum FolderType
        A4Doc
        Drawing
    End Enum

    Sub test(path As String)
        Dim teste As StepOCR = New StepOCR()
        teste.RunFile(New FileInfo(path))
        teste.dispose()
    End Sub

    Private Sub RunButton_Click(sender As Object, e As EventArgs) Handles RunButton.Click

        Me.test(ProfileText.Text)

        Exit Sub

        Me.Log(LogType.Begin, "")

        ' Get the A4 folders
        Me.GetFolders(FolderType.A4Doc)
        Me.Log(LogType.ProcessingA4, CStr(Folders.Count))

        ' Start to Split the files in the boxes of A4
        Me.SplitBoxesFiles(FolderType.A4Doc)

        ' Get the Drawings folders
        Me.GetFolders(FolderType.Drawing)
        Me.Log(LogType.ProcessingDrawing, CStr(Folders.Count))

        ' Start to Split the files in the boxes of Drawings
        Me.SplitBoxesFiles(FolderType.Drawing)

        Me.Log(LogType.Done, "")
    End Sub

    Private Sub ChangeBardecodeProcessingFolder(Folder As DirectoryInfo, FolderType As FolderType)
        ' check the type of the folder that are being processed and create the IniFile object according with it
        Dim BardecodeIni As IniFile = New IniFile(IIf(FolderType = (MainForm.FolderType.A4Doc), Me.Profile.A4BardecodeIni.FullName, Me.Profile.LFBardecodeIni.FullName))
        Dim outputFolder As String = IIf(FolderType = MainForm.FolderType.A4Doc, Me.Profile.A4OutputFolder.FullName, Me.Profile.LFOutputFolder.FullName)

        BardecodeIni.WriteValue("options", "inputFolder", "System.String," + Folder.FullName)
        BardecodeIni.WriteValue("options", "outputFolder", "System.String," + outputFolder)
        BardecodeIni.WriteValue("options", "outputTemplate", "System.String," + Folder.Name + "\%VALUES")
        BardecodeIni.WriteValue("options", "outputTemplate", "System.String," + Folder.Name + "\%VALUES")
    End Sub

    Private Sub SplitBoxesFiles(FolderType As FolderType)

        ' Going through each sub folder on the AsScanned folder and running bardecode for each folder
        For Each Folder As DirectoryInfo In Folders

            ' change the Bardecode's ini files according with the folder that are being processed
            ChangeBardecodeProcessingFolder(Folder, FolderType)

            Me.Log(LogType.Folder, Folder.FullName)

            ' start a windows process, passing the filename and the arguments
            Dim pHelp As New ProcessStartInfo
            pHelp.FileName = Me.Profile.BardecodeExe.FullName
            pHelp.Arguments = IIf(FolderType = MainForm.FolderType.A4Doc, Me.Profile.A4BardecodeIni.FullName, Me.Profile.LFBardecodeIni.FullName)
            pHelp.UseShellExecute = True
            pHelp.WindowStyle = ProcessWindowStyle.Normal
            Dim proc As Process = Process.Start(pHelp)

            ' wait for the bardecode to finish and then go to the next one
            proc.WaitForExit()
            proc.Close()

            ' if the output format is Image we can convert is to pdf
            If (Me.Profile.LFOutputFormat = ProfileSettings.LFFormatType.JPG) Then
                Me.ConvertImagesToPDF(Folder)
            End If
        Next
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

    Private Sub GetFolders(FolderType As FolderType)
        ' FolderType to know if we are getting the subfolders of A4 or of the drawings
        ' basically in this sub we pick each subfolder of the A4 or Drawings and put into a collection
        Folders.Clear()

        Select Case FolderType

            Case MainForm.FolderType.A4Doc
                ' the name of the folder DocAsScanned is a constant in the source by now, but we will change when we have a settings form
                For Each Folder In Directory.GetDirectories(Me.Profile.A4InputFolder.FullName)
                    Dim FolderInfo As DirectoryInfo = New DirectoryInfo(Folder)

                    ' check if not have a correspongind folder in the output folder
                    If Not Directory.Exists(Me.Profile.A4OutputFolder.FullName + FolderInfo.Name) Then
                        Me.Folders.Add(FolderInfo)
                    End If
                Next


            Case MainForm.FolderType.Drawing
                For Each Folder In Directory.GetDirectories(Me.Profile.LFInputFolder.FullName)
                    Dim FolderInfo As DirectoryInfo = New DirectoryInfo(Folder)

                    ' check if not have a correspongind folder in ByFiles 
                    If Not Directory.Exists(Me.Profile.LFOutputFolder.FullName + FolderInfo.Name) Then
                        Me.Folders.Add(FolderInfo)
                    End If
                Next
        End Select
    End Sub

    Private Sub ConvertImagesToPDF(Folder As DirectoryInfo)
        ' using PDFSharp library to convert images to PDF and to merge PDFs. Is an openSource library
        ' pick all image files on Drawing output folder and put together in one PDF

        Dim Doc As PdfDocument = New PdfDocument()

        For Each file As FileInfo In Folder.GetFiles()
            ' if the file are an image
            If {".jpg", ".png", ".bmp"}.Contains(file.Extension) Then

                ' add a page to the document
                Dim Pag As PdfPage = Doc.AddPage
                ' create the graphics object and the point to draw the image
                Dim Gfx As XGraphics = XGraphics.FromPdfPage(Pag)
                Dim GfxPoint As XPoint = New XPoint()

                ' load the image 
                Dim XImg As XImage = XImage.FromFile(file.FullName)

                ' set the page size according with the image size
                Pag.Height = XImg.Size.Height
                Pag.Width = XImg.Size.Width

                ' draw the image in the page
                Gfx.DrawImage(XImg, GfxPoint)
            End If
        Next

        ' I put the name of the pdf file the same as the name of the folder but we can change it later
        ' save the document 
        Doc.Save(Folder.FullName + "\" + Folder.Name + ".pdf")
        Doc.Dispose()
    End Sub

End Class
