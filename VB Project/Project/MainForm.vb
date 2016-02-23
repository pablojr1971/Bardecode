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
        'Me.Profile = New ProfileSettings(New FileInfo(ProfileText.Text))

        Me.CreateOCRedDocument(New FileInfo(ProfileText.Text))

        ' exiting because I'm just testing the OCR
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

    Private Function ColourAvg(ByVal szAvgSize As Size, ByVal szfImageSize As SizeF, ByVal intX As Integer, ByVal intY As Integer, imag As Bitmap) As Color

        Dim arrlPixels As New ArrayList 'Host All Pixels

        Dim x As Integer 'X Location
        Dim y As Integer 'Y Location

        'Find Each Pixel's Colour And Add To ArrayList
        For x = intX - CInt(szAvgSize.Width / 2) To intX + CInt(szAvgSize.Width / 2) 'Left To Right

            For y = intY - CInt(szAvgSize.Height / 2) To intY + CInt(szAvgSize.Height / 2) 'Up To Down

                If (x > 0 And x < szfImageSize.Width) And (y > 0 And y < szfImageSize.Height) Then 'If Not Out Of Bounds
                    arrlPixels.Add(imag.GetPixel(x, y)) 'Add To ArrayList

                End If

            Next

        Next

        Dim clrCurrColour As Color 'Current Colour

        Dim intAlpha As Integer = 0 'Alpha Channel
        Dim intRed As Integer = 0 'Red Channel
        Dim intGreen As Integer = 0 'Green Channel
        Dim intBlue As Integer = 0 'Blue Channel

        For Each clrCurrColour In arrlPixels 'Loop Through Each Colour

            'Store Each Colour
            intAlpha += clrCurrColour.A
            intRed += clrCurrColour.R
            intGreen += clrCurrColour.G
            intBlue += clrCurrColour.B

        Next

        ' Return Average A, R, G, B  
        Return Color.FromArgb(intAlpha / arrlPixels.Count, intRed / arrlPixels.Count, intGreen / arrlPixels.Count, intBlue / arrlPixels.Count)

    End Function

    Private Sub CreateOCRedDocument(file As FileInfo)
        Dim htmlfile As FileStream
        Dim hdoc As hDocument = New hDocument
        Dim Pages As Dictionary(Of Bitmap, String) = Me.GetOCRedPages(file)

        Dim pdfset As PDFSettings = New PDFSettings()
        pdfset.ImageType = PdfImageType.Tif
        pdfset.ImageQuality = 100
        pdfset.Dpi = 200
        pdfset.PdfOcrMode = Clock.Util.OcrMode.Tesseract
        pdfset.WriteTextMode = WriteTextMode.Word

        Dim pdfCreator As PdfCreator = New PdfCreator(pdfset, Replace(file.FullName, ".pdf", "_OCR.pdf"))

        For Each page As KeyValuePair(Of Bitmap, String) In Pages
            htmlfile = New FileStream(file.Directory.FullName + "\_temp.html", FileMode.Create, FileAccess.Write)
            With New StreamWriter(htmlfile)
                .Write(page.Value)
                .Close()
                .Dispose()
            End With
            hdoc.AddFile(htmlfile.Name)
            pdfCreator.AddPage(hdoc.Pages(hdoc.Pages.Count - 1), page.Key)
            htmlfile.Dispose()
            My.Computer.FileSystem.DeleteFile(file.Directory.FullName + "\_temp.html")
        Next

        pdfCreator.SaveAndClose()
        pdfCreator.Dispose()
        MessageBox.Show("Done")
    End Sub


    Private Function GetOCRedPages(File As FileInfo) As Dictionary(Of Bitmap, String)
        GetOCRedPages = New Dictionary(Of Bitmap, String)

        Dim a As String = ""
        Dim tes As Tesseract.TesseractEngine = New Tesseract.TesseractEngine(Application.StartupPath + "\tessdata", "eng")
        ' seting whitelist to avoid weird characters on final text
        tes.SetVariable("tessedit_char_whitelist", " $.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz,'-()\/")
        For Each item As Bitmap In Me.ExtractImagesFromPdf(File, File.Directory)
            a = a + item.Size.ToString + vbCrLf
            With tes.Process(item)
                GetOCRedPages.Add(item, .GetHOCRText(0))
                .Dispose()
            End With
        Next
        MessageBox.Show(a)
        tes.Dispose()
    End Function

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

    Public Function ExtractImagesFromPdf(Pdf As FileInfo, OutputFolder As DirectoryInfo) As List(Of Bitmap)
        ExtractImagesFromPdf = New List(Of Bitmap)

        Dim rasterizer = New GhostscriptRasterizer()
        rasterizer.Open(Pdf.FullName)
        For index As Integer = 1 To rasterizer.PageCount
            ExtractImagesFromPdf.Add(rasterizer.GetPage(200, 200, index))
        Next()
    End Function
End Class
