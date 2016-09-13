Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports ZXing

Public Class Process
    Private Connection As SqlConnection = New SqlConnection("Server=Jerry;Database=other;User Id=sa;Password=569874123;")
    Private Config As ProcessSetup = Nothing
    Private A4Dir As DirectoryInfo = Nothing
    Private LFDir As DirectoryInfo = Nothing
    Private outDir As DirectoryInfo = Nothing
    Private BarcodeList As List(Of List(Of Barcode)) = Nothing
    Private LogDelegate As MainForm.LogDelegate
    Private LogObj As LogManager = Nothing
    Private Exceptions As List(Of String) = Nothing
    Private BoxInfo As BoxInfo = Nothing

    Public Sub New(ProcessConfig As ProcessSetup, LogDelegate As MainForm.LogDelegate)
        Me.Config = ProcessConfig
        Me.Connection.Open()

        If My.Computer.FileSystem.DirectoryExists(Path.Combine(Directory.GetCurrentDirectory(), "Processing")) Then
            My.Computer.FileSystem.DeleteDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Processing"), FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If

        My.Computer.FileSystem.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Processing"))
        A4Dir = New DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Processing", "A4"))
        LFDir = New DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Processing", "LF"))
        outDir = New DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Processing", "Output"))
        BarcodeList = New List(Of List(Of Barcode))
        Me.LogDelegate = LogDelegate
        Me.LogObj = New LogManager()
        Me.Exceptions = New List(Of String)
        Me.BoxInfo = New BoxInfo()
    End Sub

    Public Sub Run()
        Dim BoxesToProcess As List(Of String) = getToProcessBoxes()
        Dim SubDirectories As List(Of DirectoryInfo) = Config.A4InputFolder.GetDirectories().ToList()
        Dim currentDir As DirectoryInfo = Nothing

        Dim expression As String = Nothing

        For Each box In BoxesToProcess
            expression = String.Format("^({0})(.{{0,}})({1})$", Config.JobNo, box)
            currentDir = Nothing

            currentDir = SubDirectories.FirstOrDefault(Function(p) New Regex(expression).Match(p.Name).Success)

            If Not IsNothing(currentDir) Then

                A4Dir.Create()
                LFDir.Create()
                outDir.Create()
                If Directory.Exists(Path.Combine(Config.LFInputFolder.FullName, currentDir.Name + "D")) Then
                    Try
                        LogObj.CreateLogFile(Config.LogFolder.FullName + "\" + Config.JobNo.ToString() + " - " + box + ".txt")
                        WriteLog("Copying box " + box)
                        My.Computer.FileSystem.CopyDirectory(currentDir.FullName, A4Dir.FullName)
                        My.Computer.FileSystem.CopyDirectory(Path.Combine(Config.LFInputFolder.FullName, currentDir.Name + "D"), LFDir.FullName)

                        WriteLog("Processing box" + box)
                        WriteLog("")

                        BoxInfo.OriginalPagecount = A4Dir.GetFiles("*.tif").Count()
                        BoxInfo.DrawingPages = LFDir.GetFiles("*.jpg").Count()

                        WriteLog("Separating Drawings")
                        SeparateDrawings()
                        BoxInfo.Drawings = LFDir.GetDirectories().Count()

                        WriteLog("Reading Barcodes")
                        ReadBarcodes()

                        BoxInfo.Placeholders = BarcodeList.Sum(Function(p) p.Where(Function(q) q.SectionName = "PH").Count())
                        WriteLog("Validating Files")
                        If ValidateFiles() Then
                            WriteLog("Validation Success")
                            CheckAndDoBW()
                            CheckAndDoOCR()
                            WriteLog("Creating PDFs")
                            CreateOutput()
                            WriteLog("Updating Scandata")
                            UpdateScandata()
                            If Config.FSplitSizeMB > 0 Then
                                WriteLog("Checking size and Splitting files")
                                SplitFiles()
                            End If
                            WriteLog("Moving to Output Folder")
                            My.Computer.FileSystem.CopyDirectory(outDir.FullName, Path.Combine(Config.OutputFolder.FullName, Config.JobNo.ToString() + " - " + box))

                            WriteLog("Original File PageCount: " + BoxInfo.OriginalPagecount.ToString())
                            WriteLog("After Process PageCount: " + BoxInfo.Files.Sum(Function(p) p.Pagecount).ToString())
                            WriteLog("Original Amount of Drws: " + BoxInfo.DrawingPages.ToString())
                            WriteLog("Amount of Drws by Brcd : " + BoxInfo.Drawings.ToString())
                            WriteLog("Amount of Placeholders : " + BoxInfo.Placeholders.ToString())
                            WriteLog("Amount of files in box : " + BoxInfo.Files.Count.ToString())

                            WriteLog("Box " + box + " Done")
                        Else
                            WriteLog("Problems validating the box: " + box)
                            For Each ex In Exceptions
                                WriteLog(" - " + ex)
                            Next
                        End If
                        LogObj.CloseLogFile()
                    Catch ex As Exception
                        WriteLog("Problem processing the file")
                        WriteLog("Exception: " + ex.Message)
                        LogObj.CloseLogFile()
                    End Try
                Else
                    WriteLog("Missing Drawing folder for the box: " + box)
                    Continue For
                End If

                A4Dir.Delete(True)
                LFDir.Delete(True)
                outDir.Delete(True)
                BoxInfo.Clear()
                BarcodeList.Clear()
            End If
        Next

        WriteLog("PROCESS DONE.")

        Connection.Close()
    End Sub

    Private Function ValidateFiles() As Boolean
        ValidateFiles = True
        For Each File In BarcodeList
            If Not File.First.SectionName = "FS" Then
                Exceptions.Add("Missed file Start")
                ValidateFiles = False
            End If

            If Not File.Last.SectionName = "FE" Then
                Exceptions.Add("File end missing in file " + File.First().BarcodeValue)
                ValidateFiles = False
            End If

            For Each requiredSection In Config.Sections.Where(Function(p) p.Required = True)
                If File.Where(Function(p) p.SectionName = requiredSection.Name).Count <= 0 Then
                    Exceptions.Add("Required section " + requiredSection.Name + " not found in file " + File.First().BarcodeValue)
                    ValidateFiles = False
                End If
            Next

            For Each oncePerFileSection In Config.Sections.Where(Function(p) p.OncePerFile = True)
                If File.Where(Function(p) p.SectionName = oncePerFileSection.Name).Count > 1 Then
                    Exceptions.Add("Section " + oncePerFileSection.Name + " appear more than once in file " + File.First().BarcodeValue)
                    ValidateFiles = False
                End If
            Next

            For Each Placeholder In File.Where(Function(p) p.SectionName = "PH")
                If Not Directory.Exists(LFDir.FullName + "\" + Placeholder.BarcodeValue) Then
                    Exceptions.Add("Drawing " + Placeholder.BarcodeValue + " not found ")
                    ValidateFiles = False
                End If
            Next
        Next
    End Function

    Private Function getToProcessBoxes() As List(Of String)
        getToProcessBoxes = New List(Of String)

        Using Command As SqlCommand = New SqlCommand("SELECT DISTINCT BOX " + _
                                                    "  FROM SCANDATA A " + _
                                                    " WHERE A.JOBNO = " + Config.JobNo.ToString() + _
                                                    "   AND A.PAGECOUNT IS NULL " + _
                                                    "   AND A.DRAWINGCOUNT IS NULL " + _
                                                    "   AND A.BOX IS NOT NULL " + _
                                                    "   AND NOT EXISTS(SELECT 1 " + _
                                                    "                    FROM SCANDATA  " + _
                                                    "                   WHERE JOBNO = A.JOBNO " + _
                                                    "                     AND BOX = A.BOX " + _
                                                    "                     AND PAGECOUNT IS NOT NULL) ", Connection)
            With Command.ExecuteReader()
                While .Read()
                    getToProcessBoxes.Add(.GetFieldValue(Of String)(0).Replace("BOX", ""))
                End While
            End With
        End Using
    End Function

    Private Sub ReadBarcodes()
        Dim BarcodeReader As BarcodeReader = New BarcodeReader()
        Dim ListOfImages As List(Of FileInfo) = A4Dir.GetFiles("*.tif").OrderBy(Function(p) p.Name).ToList()
        Dim result As ZXing.Result = Nothing

        BarcodeReader.Options.PossibleFormats = {BarcodeFormat.CODE_39}
        BarcodeReader.Options.TryHarder = True
        For Index = 0 To ListOfImages.Count - 1
            Using tempBitmap As Bitmap = Bitmap.FromFile(ListOfImages.ElementAt(Index).FullName)
                result = BarcodeReader.Decode(tempBitmap)

                If Not IsNothing(result) Then
                    If Config.FStartRegex.Match(result.Text).Success Then
                        BarcodeList.Add(New List(Of Barcode))
                        BarcodeList.Last.Add(New Barcode(Index, _
                                                         "FS", _
                                                         result.Text))
                        If Config.Sections.Exists(Function(p) p.Regex.ToString() = "^DEFAULT$") Then
                            BarcodeList.Last.Add(New Barcode(Index + 1, _
                                                             Config.Sections.Single(Function(p) p.Regex.ToString() = "^DEFAULT$").Name, _
                                                             "DEFAULT"))
                        End If
                        Continue For
                    End If

                    If New Regex("^([S][P]|[S][D])(\d{6})$").Match(result.Text).Success Then
                        BarcodeList.Last.Add(New Barcode(Index, _
                                                         "PH", _
                                                         result.Text.Replace("SP", "SD")))
                        Continue For
                    End If

                    If New Regex("^([E][D][F][E])$").Match(result.Text).Success Then
                        BarcodeList.Last.Add(New Barcode(Index, _
                                                         "FE", _
                                                         result.Text))
                        Continue For
                    End If

                    For Each section In Config.Sections
                        If section.Regex.Match(result.Text).Success Then
                            If (BarcodeList.Last.Last.SectionName = "DEFAULT" And BarcodeList.Last.Last.Index = Index - 1) Then
                                BarcodeList.Last.RemoveAt(BarcodeList.Last.Count - 1)
                            End If

                            BarcodeList.Last.Add(New Barcode(Index, _
                                                             section.Name, _
                                                             result.Text))
                            Exit For
                        End If
                    Next
                End If
                tempBitmap.Dispose()
            End Using
        Next
    End Sub

    Private Sub SaveImage(Image As Bitmap, BlackAndWhite As Boolean, PageNumber As Integer)
        Dim encoderInfo As System.Drawing.Imaging.ImageCodecInfo = Nothing
        Dim encoderParams As System.Drawing.Imaging.EncoderParameters = New System.Drawing.Imaging.EncoderParameters(2)
        encoderParams.Param(1) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, CLng(System.Drawing.Imaging.EncoderValue.MultiFrame))

        If BlackAndWhite Then
            encoderInfo = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/tiff")
            encoderParams.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, CLng(System.Drawing.Imaging.EncoderValue.CompressionCCITT4))
        Else
            encoderInfo = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/jpeg")
            encoderParams.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, CLng(System.Drawing.Imaging.EncoderValue.CompressionLZW))
        End If

        Image.SetResolution(200, 200)
        Image.Save(A4Dir.FullName + String.Format("\Page{0}.tif", PageNumber.ToString("0000")), encoderInfo, encoderParams)
        Image.Dispose()
    End Sub

    Private Sub SeparateDrawings()
        Dim BarcodeReader As BarcodeReader = New BarcodeReader()
        Dim result As ZXing.Result = Nothing
        Dim LastDirectoryPath As String = Nothing
        Dim ListOfImages As List(Of FileInfo) = LFDir.GetFiles("*.jpg").OrderBy(Function(p) p.Name).ToList()
        Dim encoderInfo As System.Drawing.Imaging.ImageCodecInfo = Nothing
        Dim encoderParams As System.Drawing.Imaging.EncoderParameters = New System.Drawing.Imaging.EncoderParameters(1)

        encoderParams.Param(0) = New System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, CLng(System.Drawing.Imaging.EncoderValue.CompressionLZW))
        encoderInfo = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/jpeg")


        BarcodeReader.Options.PossibleFormats = {BarcodeFormat.CODE_39}
        BarcodeReader.Options.TryHarder = True

        For Each Img In ListOfImages
            Using tempImg As ImageMagick.MagickImage = New ImageMagick.MagickImage(Img.FullName)
                result = BarcodeReader.Decode(tempImg.ToBitmap())

                If Not IsNothing(result) Then
                    If New Regex("^([S][D]|[S][P])(\d{6})$").Match(result.Text).Success Then
                        LastDirectoryPath = Path.Combine(LFDir.FullName, result.Text.Replace("SP", "SD"))
                        Directory.CreateDirectory(LastDirectoryPath)
                    End If
                End If

                If Not IsNothing(LastDirectoryPath) Then
                    tempImg.ToBitmap().Save(LastDirectoryPath + "\" + Img.Name, encoderInfo, encoderParams)
                End If
            End Using
            Img.Delete()
        Next
    End Sub

    Private Sub WriteLog(Text As String)
        LogDelegate(Text)
        LogObj.WriteLine(Date.Now.ToShortTimeString + " - " + Text)
    End Sub

    Private Sub CreateOutput()
        Dim ImageList As List(Of FileInfo) = Nothing
        Dim FileName As String = Nothing
        Dim fileFolder As String = Nothing

        Dim LastSection As String = Nothing
        Dim SectionIndex As Integer = Nothing

        Dim CurrentDocument As iTextSharp.text.Document = Nothing
        Dim PdfWriter As iTextSharp.text.pdf.PdfWriter = Nothing
        Dim PdfCopyWriter As iTextSharp.text.pdf.PdfSmartCopy = Nothing
        Dim PdfReader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim tempImg As iTextSharp.text.Image = Nothing
        Dim DocPerPage As Boolean = False
        Dim PageIndex As Integer = 0 ' only used when its docperpage
        Dim CurrentPageCount As Integer = 0 ' only used when its docperpage
        Dim page As iTextSharp.text.pdf.PdfImportedPage = Nothing

        If Config.OCRA4 Then
            ImageList = A4Dir.GetFiles("*.pdf").OrderBy(Function(p) p.Name).ToList()

            For Each File In BarcodeList
                BoxInfo.Files.Add(New FileCount(File.First().BarcodeValue))
                LastSection = Nothing
                SectionIndex = 0
                If Not String.IsNullOrWhiteSpace(Config.FFolder) Then
                    fileFolder = outDir.FullName + "\" + Config.FFolder
                Else
                    fileFolder = outDir.FullName
                End If

                For Index = File.First().Index + 1 To File.Last().Index - 1
                    Dim localIndex As Integer = Index
                    If File.Exists(Function(p) p.Index = localIndex) Then
                        If File.Single(Function(p) p.Index = localIndex).SectionName = "PH" Then
                            If System.IO.File.Exists(Path.Combine(LFDir.FullName, File.Single(Function(p) p.Index = localIndex).BarcodeValue + ".pdf")) Then
                                PdfReader = New iTextSharp.text.pdf.PdfReader(Path.Combine(LFDir.FullName, File.Single(Function(p) p.Index = localIndex).BarcodeValue + ".pdf"))
                                If DocPerPage Then
                                    WriteLog(String.Format("Added {0} Drawings to {1} on single page pdfs", PdfReader.NumberOfPages, Path.GetFileName(FileName)))

                                    Dim index2 As Integer = 0

                                    For index2 = 1 To PdfReader.NumberOfPages
                                        If CurrentPageCount > 0 Then
                                            CurrentDocument.Close()
                                            PdfCopyWriter.Close()
                                            CurrentDocument.Dispose()
                                            PdfCopyWriter.Dispose()
                                            CurrentDocument = Nothing
                                            PdfCopyWriter = Nothing
                                            page = Nothing

                                            CurrentDocument = New iTextSharp.text.Document()
                                            PdfCopyWriter = New iTextSharp.text.pdf.PdfSmartCopy(CurrentDocument, New FileStream(FileName.Replace("[PAGECOUNT]", PageIndex.ToString("0000")), FileMode.Create))
                                            CurrentDocument.Open()
                                            CurrentPageCount = 0
                                        End If
                                        page = PdfCopyWriter.GetImportedPage(PdfReader, index2)

                                        PdfCopyWriter.AddPage(page)
                                        BoxInfo.Files.Last().DrawingCount += 1
                                        PageIndex += 1
                                        CurrentPageCount = 1
                                    Next

                                    If DocPerPage Then
                                        CurrentDocument.Close()
                                        PdfCopyWriter.Close()
                                        CurrentDocument.Dispose()
                                        PdfCopyWriter.Dispose()
                                        CurrentDocument = Nothing
                                        PdfCopyWriter = Nothing
                                        page = Nothing
                                    End If
                                Else
                                    WriteLog(String.Format("Added {0} Drawings to {1} on Page {2}", PdfReader.NumberOfPages, Path.GetFileName(FileName), PdfCopyWriter.CurrentPageNumber))
                                    PdfCopyWriter.AddDocument(PdfReader)
                                    BoxInfo.Files.Last().DrawingCount += PdfReader.NumberOfPages
                                End If
                                PdfReader.Close()
                                PdfReader.Dispose()
                                PdfReader = Nothing
                                System.IO.File.Delete(Path.Combine(LFDir.FullName, File.Single(Function(p) p.Index = localIndex).BarcodeValue + ".pdf"))
                            End If
                            Continue For
                        Else
                            If Not IsNothing(CurrentDocument) Then
                                CurrentDocument.Close()
                                PdfCopyWriter.Close()
                                CurrentDocument.Dispose()
                                PdfCopyWriter.Dispose()
                                CurrentDocument = Nothing
                                PdfCopyWriter = Nothing

                                If Not IsNothing(PdfReader) Then
                                    PdfReader.Close()
                                    PdfReader.Dispose()
                                    PdfReader = Nothing
                                End If
                            End If

                            With Config.Sections.Single(Function(p) p.Name = File.Single(Function(q) q.Index = localIndex).SectionName)
                                If LastSection = .Name Then
                                    SectionIndex += 1
                                Else
                                    LastSection = .Name
                                    SectionIndex = 1
                                End If
                                If .FolderName.Length > 0 Then
                                    FileName = Path.Combine(fileFolder, .FolderName, Config.FOutputName + .OutputName)
                                Else
                                    FileName = Path.Combine(fileFolder, Config.FOutputName + .OutputName)
                                End If

                                DocPerPage = .DocPerPage
                            End With

                            If DocPerPage Then
                                FileName = ReplaceFileNameParameters(FileName, File.First().BarcodeValue, File.Single(Function(q) q.Index = localIndex).SectionName, File.Single(Function(q) q.Index = localIndex).BarcodeValue, SectionIndex) + "_[PAGECOUNT].pdf"
                                PageIndex = 1
                                CurrentPageCount = 0
                            Else
                                FileName = ReplaceFileNameParameters(FileName, File.First().BarcodeValue, File.Single(Function(q) q.Index = localIndex).SectionName, File.Single(Function(q) q.Index = localIndex).BarcodeValue, SectionIndex) + ".pdf"
                            End If

                            If Not Directory.Exists(Path.GetDirectoryName(FileName)) Then
                                Directory.CreateDirectory(Path.GetDirectoryName(FileName))
                            End If

                            CurrentDocument = New iTextSharp.text.Document()
                            If DocPerPage Then
                                PdfCopyWriter = New iTextSharp.text.pdf.PdfSmartCopy(CurrentDocument, New FileStream(FileName.Replace("[PAGECOUNT]", PageIndex.ToString("0000")), FileMode.Create))
                            Else
                                PdfCopyWriter = New iTextSharp.text.pdf.PdfSmartCopy(CurrentDocument, New FileStream(FileName, FileMode.Create))
                            End If
                            CurrentDocument.Open()

                            ' When the first section in the XML and first time of the section in the file, add the file Start
                            If (Config.Sections.First().Name = File.Single(Function(p) p.Index = localIndex).SectionName) And (SectionIndex = 1) Then
                                PdfReader = New iTextSharp.text.pdf.PdfReader(ImageList.ElementAt(File.First().Index).FullName)
                                PdfCopyWriter.AddDocument(PdfReader)
                                PdfReader.Close()
                                PdfReader.Dispose()
                                PdfReader = Nothing
                                BoxInfo.Files.Last().Pagecount += 1
                                CurrentPageCount += 1
                            End If
                        End If
                    End If

                    If DocPerPage Then
                        ' close and open if it already has a page
                        ' if it doesn't then add the page, close and open 

                        If CurrentPageCount > 0 Then
                            If Not IsNothing(CurrentDocument) Then
                                CurrentDocument.Close()
                                PdfCopyWriter.Close()
                                CurrentDocument.Dispose()
                                PdfCopyWriter.Dispose()
                                CurrentDocument = Nothing
                                PdfCopyWriter = Nothing
                            End If

                            CurrentDocument = New iTextSharp.text.Document()
                            PdfCopyWriter = New iTextSharp.text.pdf.PdfSmartCopy(CurrentDocument, New FileStream(FileName.Replace("[PAGECOUNT]", PageIndex.ToString("0000")), FileMode.Create))
                            PageIndex += 1
                            CurrentPageCount = 0

                            CurrentDocument.Open()
                        End If

                        PdfReader = New iTextSharp.text.pdf.PdfReader(ImageList.ElementAt(Index).FullName)
                        PdfCopyWriter.AddDocument(PdfReader)
                        PdfReader.Close()
                        PdfReader.Dispose()
                        PdfReader = Nothing
                        BoxInfo.Files.Last().Pagecount += 1
                        CurrentPageCount += 1
                    Else
                        ' Add Pdf to the current document
                        PdfReader = New iTextSharp.text.pdf.PdfReader(ImageList.ElementAt(Index).FullName)
                        PdfCopyWriter.AddDocument(PdfReader)
                        PdfReader.Close()
                        PdfReader.Dispose()
                        PdfReader = Nothing
                        BoxInfo.Files.Last().Pagecount += 1
                    End If
                Next
                If Not IsNothing(CurrentDocument) Then
                    ' Add the file end
                    If DocPerPage Then
                        CurrentDocument.Close()
                        PdfCopyWriter.Close()
                        CurrentDocument.Dispose()
                        PdfCopyWriter.Dispose()
                        CurrentDocument = Nothing
                        PdfCopyWriter = Nothing

                        CurrentDocument = New iTextSharp.text.Document()
                        PdfCopyWriter = New iTextSharp.text.pdf.PdfSmartCopy(CurrentDocument, New FileStream(FileName.Replace("[PAGECOUNT]", PageIndex.ToString("0000")), FileMode.Create))
                        PageIndex += 1
                        CurrentDocument.Open()
                    End If
                    PdfReader = New iTextSharp.text.pdf.PdfReader(ImageList.ElementAt(File.Last().Index).FullName)
                    PdfCopyWriter.AddDocument(PdfReader)
                    PdfReader.Close()
                    PdfReader.Dispose()
                    PdfReader = Nothing
                    BoxInfo.Files.Last().Pagecount += 1

                    CurrentDocument.Close()
                    PdfCopyWriter.Close()
                    CurrentDocument.Dispose()
                    PdfCopyWriter.Dispose()
                    CurrentDocument = Nothing
                    PdfCopyWriter = Nothing
                End If
            Next

        Else
            ImageList = A4Dir.GetFiles("*.tif").OrderBy(Function(p) p.Name).ToList()
            Dim localIndex As Integer = 0

            For Each File In BarcodeList
                BoxInfo.Files.Add(New FileCount(File.First().BarcodeValue))
                LastSection = Nothing
                SectionIndex = 0
                If Not String.IsNullOrWhiteSpace(Config.FFolder) Then
                    fileFolder = outDir.FullName + "\" + Config.FFolder
                Else
                    fileFolder = outDir.FullName
                End If

                For Index = File.First().Index + 1 To File.Last().Index - 1
                    localIndex = Index
                    If File.Exists(Function(p) p.Index = localIndex) Then
                        If File.Single(Function(p) p.Index = localIndex).SectionName = "PH" Then
                            If Directory.Exists(Path.Combine(LFDir.FullName, File.Single(Function(p) p.Index = localIndex).BarcodeValue)) Then
                                If DocPerPage Then
                                    WriteLog(String.Format("Added {0} Drawings to {1} in single page pdfs", Directory.GetFiles(Path.Combine(LFDir.FullName, File.Single(Function(p) p.Index = localIndex).BarcodeValue), "*.jpg").Count, Path.GetFileName(FileName)))
                                    For Each Img In Directory.GetFiles(Path.Combine(LFDir.FullName, File.Single(Function(p) p.Index = localIndex).BarcodeValue), "*.jpg")
                                        If CurrentPageCount > 0 Then
                                            CurrentDocument.Close()
                                            PdfWriter.Close()
                                            CurrentDocument.Dispose()
                                            PdfWriter.Dispose()
                                            CurrentDocument = Nothing
                                            PdfWriter = Nothing

                                            CurrentDocument = New iTextSharp.text.Document()
                                            PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(CurrentDocument, New FileStream(FileName.Replace("[PAGECOUNT]", PageIndex.ToString("0000")), FileMode.Create))
                                            CurrentDocument.Open()
                                        End If

                                        BoxInfo.Files.Last().DrawingCount += 1
                                        tempImg = iTextSharp.text.Image.GetInstance(Img)
                                        tempImg.SetAbsolutePosition(0, 0)
                                        tempImg.ScaleToFit(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                                        CurrentDocument.SetPageSize(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                                        CurrentDocument.NewPage()
                                        PdfWriter.DirectContent.AddImage(tempImg)
                                        tempImg = Nothing
                                        PageIndex += 1
                                        CurrentPageCount = 1
                                    Next
                                Else
                                    WriteLog(String.Format("Added {0} Drawings to {1} on Page {2}", Directory.GetFiles(Path.Combine(LFDir.FullName, File.Single(Function(p) p.Index = localIndex).BarcodeValue), "*.jpg").Count, Path.GetFileName(FileName), PdfWriter.CurrentPageNumber))
                                    For Each Img In Directory.GetFiles(Path.Combine(LFDir.FullName, File.Single(Function(p) p.Index = localIndex).BarcodeValue), "*.jpg")
                                        ' Add code for split each img in one document
                                        BoxInfo.Files.Last().DrawingCount += 1
                                        tempImg = iTextSharp.text.Image.GetInstance(Img)
                                        tempImg.SetAbsolutePosition(0, 0)
                                        tempImg.ScaleToFit(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                                        CurrentDocument.SetPageSize(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                                        CurrentDocument.NewPage()
                                        PdfWriter.DirectContent.AddImage(tempImg)
                                        tempImg = Nothing
                                    Next
                                End If
                            End If

                            Continue For
                        Else
                            If Not IsNothing(CurrentDocument) Then
                                CurrentDocument.Close()
                                PdfWriter.Close()
                                CurrentDocument.Dispose()
                                PdfWriter.Dispose()
                                CurrentDocument = Nothing
                                PdfWriter = Nothing
                            End If

                            With Config.Sections.Single(Function(p) p.Name = File.Single(Function(q) q.Index = localIndex).SectionName)
                                If LastSection = .Name Then
                                    SectionIndex += 1
                                Else
                                    LastSection = .Name
                                    SectionIndex = 1
                                End If
                                If .FolderName.Length > 0 Then
                                    FileName = Path.Combine(fileFolder, .FolderName, Config.FOutputName + .OutputName)
                                Else
                                    FileName = Path.Combine(fileFolder, Config.FOutputName + .OutputName)
                                End If

                                DocPerPage = .DocPerPage
                            End With

                            If DocPerPage Then
                                FileName = ReplaceFileNameParameters(FileName, File.First().BarcodeValue, File.Single(Function(q) q.Index = localIndex).SectionName, File.Single(Function(q) q.Index = localIndex).BarcodeValue, SectionIndex) + "_[PAGECOUNT].pdf"
                                PageIndex = 1
                                CurrentPageCount = 0
                            Else
                                FileName = ReplaceFileNameParameters(FileName, File.First().BarcodeValue, File.Single(Function(q) q.Index = localIndex).SectionName, File.Single(Function(q) q.Index = localIndex).BarcodeValue, SectionIndex) + ".pdf"
                            End If

                            If Not Directory.Exists(Path.GetDirectoryName(FileName)) Then
                                Directory.CreateDirectory(Path.GetDirectoryName(FileName))
                            End If

                            CurrentDocument = New iTextSharp.text.Document()
                            If DocPerPage Then
                                PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(CurrentDocument, New FileStream(FileName.Replace("[PAGECOUNT]", PageIndex.ToString("0000")), FileMode.Create))
                            Else
                                PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(CurrentDocument, New FileStream(FileName, FileMode.Create))
                            End If
                            CurrentDocument.Open()

                            ' When the first section in the XML and first time of the section in the file, add the file Start
                            If (Config.Sections.First().Name = File.Single(Function(p) p.Index = localIndex).SectionName) And (SectionIndex = 1) Then
                                tempImg = iTextSharp.text.Image.GetInstance(ImageList.ElementAt(File.First().Index).FullName)
                                tempImg.SetAbsolutePosition(0, 0)
                                tempImg.ScaleToFit(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                                CurrentDocument.SetPageSize(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                                CurrentDocument.NewPage()
                                PdfWriter.DirectContent.AddImage(tempImg)
                                tempImg = Nothing
                                BoxInfo.Files.Last().Pagecount += 1
                                CurrentPageCount += 1
                            End If

                        End If
                    End If

                    If DocPerPage Then
                        If CurrentPageCount > 0 Then
                            CurrentDocument.Close()
                            PdfWriter.Close()
                            CurrentDocument.Dispose()
                            PdfWriter.Dispose()
                            CurrentDocument = Nothing
                            PdfWriter = Nothing

                            CurrentDocument = New iTextSharp.text.Document()
                            PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(CurrentDocument, New FileStream(FileName.Replace("[PAGECOUNT]", PageIndex.ToString("0000")), FileMode.Create))
                            PageIndex += 1
                            CurrentPageCount = 0

                            CurrentDocument.Open()
                        End If

                        tempImg = iTextSharp.text.Image.GetInstance(ImageList(Index).FullName)
                        tempImg.SetAbsolutePosition(0, 0)
                        tempImg.ScaleToFit(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                        CurrentDocument.SetPageSize(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                        CurrentDocument.NewPage()
                        PdfWriter.DirectContent.AddImage(tempImg)
                        tempImg = Nothing
                        BoxInfo.Files.Last().Pagecount += 1
                        CurrentPageCount += 1
                    Else
                        tempImg = iTextSharp.text.Image.GetInstance(ImageList(Index).FullName)
                        tempImg.SetAbsolutePosition(0, 0)
                        tempImg.ScaleToFit(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                        CurrentDocument.SetPageSize(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                        CurrentDocument.NewPage()
                        PdfWriter.DirectContent.AddImage(tempImg)
                        tempImg = Nothing
                        BoxInfo.Files.Last().Pagecount += 1
                    End If
                Next
                If Not IsNothing(CurrentDocument) Then
                    ' Add the file end
                    If DocPerPage Then
                        If CurrentPageCount >= 1 Then
                            CurrentDocument.Close()
                            PdfWriter.Close()
                            CurrentDocument.Dispose()
                            PdfWriter.Dispose()
                            CurrentDocument = Nothing
                            PdfWriter = Nothing


                            CurrentDocument = New iTextSharp.text.Document()
                            PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(CurrentDocument, New FileStream(FileName.Replace("[PAGECOUNT]", PageIndex.ToString("0000")), FileMode.Create))
                            PageIndex += 1

                            CurrentDocument.Open()
                        End If
                    End If

                    tempImg = iTextSharp.text.Image.GetInstance(ImageList.ElementAt(File.Last().Index).FullName)
                    tempImg.SetAbsolutePosition(0, 0)
                    tempImg.ScaleToFit(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                    CurrentDocument.SetPageSize(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                    CurrentDocument.NewPage()
                    PdfWriter.DirectContent.AddImage(tempImg)
                    tempImg = Nothing
                    BoxInfo.Files.Last().Pagecount += 1

                    CurrentDocument.Close()
                    PdfWriter.Close()
                    CurrentDocument.Dispose()
                    PdfWriter.Dispose()
                    CurrentDocument = Nothing
                    PdfWriter = Nothing
                End If
            Next
        End If
    End Sub

    Private Sub CheckAndDoOCR()
        If Config.OCRA4 Then

            WriteLog("Re-saving images and doing OCR (this will take a while)")
            Dim ImgList As List(Of FileInfo) = A4Dir.GetFiles("*.tif").ToList()

            For index = 0 To ImgList.Count - 1
                Using mimg As ImageMagick.MagickImage = New ImageMagick.MagickImage(ImgList.ElementAt(index).FullName)
                    SaveImage(mimg.ToBitmap, mimg.TotalColors < 64, index)
                    mimg.Dispose()
                End Using

                ImgList.ElementAt(index).Delete()
            Next

            ImgList.Clear()
            ImgList = A4Dir.GetFiles("*.tif").ToList()

            Dim tess As ProcessStartInfo = New ProcessStartInfo(Directory.GetCurrentDirectory() + "\tesseract\tesseract.exe")
            tess.WindowStyle = ProcessWindowStyle.Hidden

            For Each img In ImgList

                tess.Arguments = String.Format("""" + A4Dir.FullName + "\{0}.tif"" """ + A4Dir.FullName + "\{0}"" ""pdf""", img.Name.Replace(".tif", ""))
                With System.Diagnostics.Process.Start(tess)
                    .PriorityClass = ProcessPriorityClass.Normal
                    .Dispose()
                End With

            Next

            While System.Diagnostics.Process.GetProcessesByName("tesseract").Count > 0
                System.Threading.Thread.Sleep(10)
            End While

            ImgList.Clear()
            A4Dir.Refresh()
            ImgList = A4Dir.GetFiles("*.tif").ToList()
            For Each Img In ImgList
                Img.Delete()
            Next

            If Config.OCRLF Then
                WriteLog("OCRing Drawings")
                For Each subdir In LFDir.GetDirectories()
                    ImgList.Clear()
                    ImgList = subdir.GetFiles("*.jpg").ToList()
                    For index = 0 To ImgList.Count - 1
                        Using mimg As ImageMagick.MagickImage = New ImageMagick.MagickImage(ImgList.ElementAt(index).FullName)
                            SaveImage(mimg.ToBitmap, mimg.TotalColors < 64, index)
                        End Using

                        ImgList.ElementAt(index).Delete()
                    Next
                Next

                For Each subdir In LFDir.GetDirectories()
                    ImgList.Clear()
                    ImgList = subdir.GetFiles("*.tif").ToList()
                    For Each img In ImgList
                        tess.Arguments = String.Format("""" + subdir.FullName + "\{0}.tif"" """ + subdir.FullName + "\{0}"" ""pdf""", img.Name.Replace(".tif", ""))
                        With System.Diagnostics.Process.Start(tess)
                            .PriorityClass = ProcessPriorityClass.Normal
                            .Dispose()
                        End With
                    Next
                Next

                While System.Diagnostics.Process.GetProcessesByName("tesseract").Count > 0
                    System.Threading.Thread.Sleep(1)
                End While

                ImgList.ForEach(Sub(p)
                                    p.Delete()
                                End Sub)

                MergeLFFilesPDF()

            Else
                WriteLog("Converting Drawings to PDF")
                ConvertLFtoPDF()
            End If

        End If
    End Sub

    Private Sub CheckAndDoBW()
        ' Do the BW part in the documents
        Beep()
        ' this will be added at the end.
    End Sub

    Private Function ReplaceFileNameParameters(FileName As String, FileBarcode As String, SectionName As String, SectionBarcode As String, SectionIndex As Integer) As String
        Dim FileReturn As String = FileName
        Dim field As String = Nothing

        FileReturn = FileReturn.Replace("[FBARCODE]", FileBarcode)
        FileReturn = FileReturn.Replace("[SNAME]", SectionName)
        FileReturn = FileReturn.Replace("[SBARCODE]", SectionBarcode)
        FileReturn = FileReturn.Replace("[SINDEX]", SectionIndex.ToString("000"))

        If FileReturn.Contains("[D") Then

            Using Command As New SqlCommand("SELECT ISNULL(FIELD1, ''), ISNULL(FIELD2, ''), ISNULL(FIELD3, ''), ISNULL(FIELD4, ''), ISNULL(FIELD5, ''), ISNULL(FIELD6, ''), ISNULL(FIELD7, ''), ISNULL(FIELD8, ''), ISNULL(FIELD9, ''), ISNULL(FIELD10, ''), ISNULL(FIELD11, ''), ISNULL(FIELD12, '') " + _
                                            "   FROM SCANDATA " + _
                                            "  WHERE JOBNO = " + Config.JobNo.ToString() + _
                                            "    AND BARCODE = '" + FileBarcode + "'", Connection)
                With Command.ExecuteReader()
                    If .Read() Then
                        For index = 1 To 12
                            FileReturn = FileReturn.Replace("[DFIELD" + index.ToString() + "]", .GetString(index - 1))
                        Next
                    End If
                    .Close()
                End With
            End Using
        End If

        Return FileReturn
    End Function

    Private Sub MergeLFFilesPDF()
        Dim doc As iTextSharp.text.Document = Nothing
        Dim pdfCopyWriter As iTextSharp.text.pdf.PdfSmartCopy = Nothing
        Dim pdfReader As iTextSharp.text.pdf.PdfReader = Nothing

        For Each subdir In LFDir.GetDirectories()
            doc = New iTextSharp.text.Document()
            pdfCopyWriter = iTextSharp.text.pdf.PdfSmartCopy.GetInstance(doc, New FileStream(subdir.FullName + ".pdf", FileMode.Create))
            doc.Open()

            For Each File In subdir.GetFiles("*.pdf")
                pdfReader = New iTextSharp.text.pdf.PdfReader(File.FullName)
                pdfCopyWriter.AddDocument(pdfReader)
                pdfReader.Close()
                pdfReader.Dispose()
                pdfReader = Nothing
            Next
            doc.Close()
            pdfCopyWriter.Close()
            doc.Dispose()
            pdfCopyWriter.Dispose()
            doc = Nothing
            pdfCopyWriter = Nothing

            subdir.Delete(True)
        Next
    End Sub

    Private Sub ConvertLFtoPDF()
        Dim doc As iTextSharp.text.Document = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        Dim tempImg As iTextSharp.text.Image = Nothing

        For Each subdir In LFDir.GetDirectories()
            doc = New iTextSharp.text.Document()
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, New FileStream(subdir.FullName + ".pdf", FileMode.Create))
            doc.Open()

            For Each img In subdir.GetFiles("*.jpg")
                tempImg = iTextSharp.text.Image.GetInstance(img.FullName)
                tempImg.SetAbsolutePosition(0, 0)
                tempImg.ScaleToFit(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                doc.SetPageSize(New iTextSharp.text.Rectangle(0, 0, ((tempImg.Width / 200) * 72), ((tempImg.Height / 200) * 72), 0))
                doc.NewPage()
                writer.DirectContent.AddImage(tempImg)
                tempImg = Nothing
            Next

            doc.Close()
            writer.Close()

            doc.Dispose()
            writer.Dispose()

            doc = Nothing
            writer = Nothing

            subdir.Delete(True)
        Next
    End Sub

    Private Sub UpdateScandata()
        For Each NewFile In BoxInfo.Files
            Using Command As SqlCommand = New SqlCommand("UPDATE SCANDATA " + _
                                                         "   SET PAGECOUNT = " + NewFile.Pagecount.ToString() + ", " + _
                                                         "       DRAWINGCOUNT = " + NewFile.DrawingCount.ToString() + _
                                                         " WHERE JOBNO = " + Config.JobNo.ToString() + _
                                                         "   AND BARCODE = '" + NewFile.Barcode + "'", Connection)
                WriteLog(String.Format("Updated ScanData {0} - Rows Affected = ", NewFile.Barcode) + Command.ExecuteNonQuery().ToString())
            End Using
        Next
    End Sub

    Private Sub SplitFiles()
        SplitFilesRecursive(outDir)
    End Sub

    Private Sub SplitFilesRecursive(CurrentDir As DirectoryInfo)
        For Each subdir In CurrentDir.GetDirectories()
            SplitFilesRecursive(subdir)
        Next

        For Each File In CurrentDir.GetFiles("*.pdf")
            SplitFileSize(File.FullName, Config.FSplitSizeMB)
        Next
    End Sub

    Public Shared Sub SplitFileSize(file As String, MBFileSize As Integer)
        Dim reader As iTextSharp.text.pdf.PdfReader = New iTextSharp.text.pdf.PdfReader(file)
        Dim documentcount As Integer = 1
        Dim page As iTextSharp.text.pdf.PdfImportedPage = Nothing
        Dim NewFiles As List(Of String) = New List(Of String)
        ' times 1024 to convert to kBytes and times 1024 to conver to bytes
        Dim LimitBytes As Long = ((MBFileSize * 1024) * 1024)

        If (reader.FileLength() > LimitBytes) And (reader.NumberOfPages > 1) Then
            Dim document As iTextSharp.text.Document = New iTextSharp.text.Document()
            Dim pdfwriter As iTextSharp.text.pdf.PdfCopy = New iTextSharp.text.pdf.PdfSmartCopy(document, New FileStream(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)), FileMode.Create))
            NewFiles.Add(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)))
            document.Open()
            For index = 1 To reader.NumberOfPages
                page = pdfwriter.GetImportedPage(reader, index)

                If (pdfwriter.CurrentDocumentSize + getPageFileSize(page)) > LimitBytes Then
                    pdfwriter.Close()
                    document.Close()
                    pdfwriter.Dispose()
                    document.Dispose()
                    documentcount += 1

                    document = New iTextSharp.text.Document()
                    pdfwriter = New iTextSharp.text.pdf.PdfCopy(document, New FileStream(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)), FileMode.Create))
                    NewFiles.Add(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)))

                    document.Open()
                    pdfwriter.AddPage(page)
                Else
                    pdfwriter.AddPage(page)
                End If
            Next
            document.Close()
            pdfwriter.Close()
            pdfwriter.Dispose()
            document.Dispose()

            reader.Close()
            reader = Nothing

            'Renaming the files
            For Each NewFile In NewFiles
                My.Computer.FileSystem.RenameFile(NewFile, Path.GetFileName(NewFile).Replace(".pdf", String.Format("-{0}.pdf", documentcount)))
            Next

            My.Computer.FileSystem.DeleteFile(file)
        Else
            reader.Close()
            reader = Nothing
        End If
    End Sub

    Private Shared Function getPageFileSize(Page As iTextSharp.text.pdf.PdfImportedPage) As Long
        Dim document As iTextSharp.text.Document = New iTextSharp.text.Document()
        Dim writer As iTextSharp.text.pdf.PdfCopy = New iTextSharp.text.pdf.PdfCopy(document, New MemoryStream())
        Dim size As Long = 0

        document.Open()
        size = writer.CurrentDocumentSize
        writer.AddPage(Page)
        size = writer.CurrentDocumentSize - size

        writer.Close()
        document.Close()

        writer.Dispose()
        document.Dispose()

        Return size
    End Function

End Class
