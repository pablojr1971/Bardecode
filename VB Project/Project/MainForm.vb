Imports System.IO
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports PdfSharp.Drawing
Imports System.Drawing

Public Class MainForm
    ' By now the path of the Bardecode application is a constant in the source but we will change when we have a settings form on our application
    Private Const Bardecode = "C:\Program Files (x86)\Softek Software\BardecodeFiler\BardecodeFiler.exe"
    Private JobPath As String
    Private Ini As IniFile
    Private Folders As Collection = New Collection()
    Private Files As Collection = New Collection()

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

    Private Sub RunButton_Click(sender As Object, e As EventArgs) Handles RunButton.Click
        JobPath = JobPathText.Text

        If Directory.Exists(JobPath) Then
            If File.Exists(JobPath + "\INIFILE.ini") Then
                ' if the ini file exists we can create the object to manage it
                Ini = New IniFile(JobPath + "\INIFILE.ini")
            End If

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
        End If
    End Sub

    Private Sub SplitBoxesFiles(FolderType As FolderType)

        ' Going through each sub folder on the AsScanned folder and running bardecode for each folder
        For Each Folder As DirectoryInfo In Folders

            ' change the ini files according with the folder that we are running
            Ini.WriteValue("options", "inputFolder", "System.String," + Folder.FullName)
            Ini.WriteValue("options", "outputTemplate", "System.String," + Folder.Name + "\%VALUES")

            Me.Log(LogType.Folder, Folder.FullName)

            ' start a windows process, passing the filename and the arguments
            Dim pHelp As New ProcessStartInfo
            pHelp.FileName = Bardecode
            pHelp.Arguments = Ini.Path
            pHelp.UseShellExecute = True
            pHelp.WindowStyle = ProcessWindowStyle.Normal
            Dim proc As Process = Process.Start(pHelp)

            ' wait for the bardecode to finish and then go to the next one
            proc.WaitForExit()
            proc.Close()

            ' finish processing we need convert images to pdf files
            ' Only for drawings
            If FolderType = MainForm.FolderType.Drawing Then
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
                Me.ProcessLogText.AppendText("----PROCESS BEGIN----" + vbCrLf + vbCrLf)

            Case MainForm.LogType.ProcessingA4
                Me.ProcessLogText.AppendText("Spliting A4 files, " + data + " boxes found" + vbCrLf + vbCrLf)

            Case MainForm.LogType.ProcessingDrawing
                Me.ProcessLogText.AppendText("Spliting Drawing files, " + data + " boxes found" + vbCrLf + vbCrLf)

                ' log which folder is being processed at the moment, the data will be the folder
            Case MainForm.LogType.Folder
                Me.ProcessLogText.AppendText("Processing folder: " + vbCrLf + _
                                             "    " + data + vbCrLf)

                ' end of process, just to say done and that the process went well
            Case MainForm.LogType.Done
                Me.ProcessLogText.AppendText("----PROCESS DONE----")

        End Select
    End Sub

    Private Sub GetFolders(FolderType As FolderType)
        ' FolderType to know if we are getting the subfolders of A4 or of the drawings
        ' basically in this sub we pick each subfolder of the A4 or Drawings and put into a collection
        Folders.Clear()

        Select Case FolderType

            Case MainForm.FolderType.A4Doc
                ' the name of the folder DocAsScanned is a constant in the source by now, but we will change when we have a settings form
                For Each Folder In Directory.GetDirectories(JobPath + "\DocAsScanned")
                    Dim FolderInfo As DirectoryInfo = New DirectoryInfo(Folder)

                    ' check if not have a correspongind folder in ByFiles 
                    If Not Directory.Exists(JobPath + "\DocByFile\" + FolderInfo.Name) Then
                        Me.Folders.Add(FolderInfo)
                    End If
                Next


            Case MainForm.FolderType.Drawing
                For Each Folder In Directory.GetDirectories(JobPath + "\DrawingAsScanned")
                    Dim FolderInfo As DirectoryInfo = New DirectoryInfo(Folder)

                    ' check if not have a correspongind folder in ByFiles 
                    If Not Directory.Exists(JobPath + "\DrawingByFile\" + FolderInfo.Name) Then
                        Me.Folders.Add(FolderInfo)
                    End If
                Next
        End Select
    End Sub

    Private Sub ConvertImagesToPDF(Folder As DirectoryInfo)
        ' using PDFSharp library to convert images to PDF and to merge PDFs. Is an openSource library
        ' pick all image files on DrawingbyFile folder and put together in one PDF

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

    Private Sub ConvertPdfToTiff(File As FileInfo)
        Dim Doc As PdfDocument = PdfReader.Open(File.FullName)

        For Each page As PdfPage In Doc.Pages
            ' TODO read the pages pick the images and export them as TIFF
        Next

    End Sub

End Class
