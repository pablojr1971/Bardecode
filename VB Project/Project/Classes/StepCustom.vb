Imports System.IO
Imports System.Reflection
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports Ghostscript.NET.Rasterizer
Imports ImageMagick
Imports System.Drawing.Imaging
Imports ZXing

Public Class StepCustom
    Implements IStep

    Public CustomProperties As PropertiesCustom

    Public Sub New()
        CustomProperties = New PropertiesCustom()
    End Sub

    Public Sub New(Properties As PropertiesCustom)
        Me.CustomProperties = Properties
        Me.CustomProperties.Input1 = Directory.GetCurrentDirectory() + "\Processing\Documents"
        Me.CustomProperties.Input2 = Directory.GetCurrentDirectory() + "\Processing\Drawings"
        Me.CustomProperties.Output = Me.CustomProperties.Input1
    End Sub

    Public Sub Run(LogSub As IStep.LogSubDelegate) Implements IStep.Run
        LogSub("Running " + CustomProperties.CustomRunID)
        Me.GetType.InvokeMember(Me.CustomProperties.CustomRunID,
                                BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance,
                                Nothing,
                                Me,
                                {LogSub})
    End Sub

    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.Custom
        End Get
    End Property

    Public Shared Function LoadStep(StepId As Integer, ctx As VBProjectContext) As StepCustom
        With ctx.ESteps.Single(Function(p) p.Id = StepId)
            LoadStep = New StepCustom(Serializer.FromXml(.PropertiesObj, GetType(PropertiesCustom)))
        End With
    End Function

    Private Sub ConvertJPGTOPDF(LogSub As IStep.LogSubDelegate)
        Dim document As iTextSharp.text.Document = Nothing
        Dim filename As String = Nothing
        Dim fs As FileStream = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        ' Will have a input folder, with subfolders as files
        ' each file will have many images and need to be merged into a single multipage PDF
        Dim img As iTextSharp.text.Image = Nothing
        For Each subFolder In New DirectoryInfo(CustomProperties.Input2).GetDirectories()
            If (subFolder.GetFiles("*.jpg").Length > 0) And (Not subFolder.Name.StartsWith("EDW")) Then
                filename = subFolder.FullName.Replace("SD", "SP") + ".pdf"
                document = New Document()
                fs = New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None)
                writer = PdfWriter.GetInstance(document, fs)
                document.Open()
                For Each File In subFolder.GetFiles("*.jpg")
                    img = Image.GetInstance(File.FullName)
                    img.SetAbsolutePosition(0, 0)
                    document.SetPageSize(New iTextSharp.text.Rectangle(0, 0, img.Width, img.Height, 0))
                    document.NewPage()
                    writer.DirectContent.AddImage(img)
                    img = Nothing
                Next
                document.Close()
                fs = Nothing
                writer = Nothing
                document = Nothing
                LogSub("File created - " + filename)
            End If
            subFolder.Delete(True)
        Next
        LogSub("Files converted" + vbCrLf)
    End Sub

    Private Sub convertTiffTOPDF(LogSub As IStep.LogSubDelegate)
        Dim document As iTextSharp.text.Document = Nothing
        Dim filename As String = Nothing
        Dim fs As FileStream = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        ' Will have a input folder, with subfolders as files
        ' each file will have many images and need to be merged into a single multipage PDF
        Dim img As iTextSharp.text.Image = Nothing
        For Each subFolder In New DirectoryInfo(CustomProperties.Input1).GetDirectories()
            If (subFolder.GetFiles("*.tif").Length > 0) And (Not subFolder.Name.StartsWith("EDW")) Then
                filename = subFolder.FullName + "\" + subFolder.Name.Replace("SD", "SP") + ".pdf"
                document = New Document()
                fs = New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None)
                writer = PdfWriter.GetInstance(document, fs)
                document.Open()
                For Each File In subFolder.GetFiles("*.tif")
                    img = Image.GetInstance(File.FullName)
                    img.SetAbsolutePosition(0, 0)
                    document.SetPageSize(New iTextSharp.text.Rectangle(0, 0, img.Width, img.Height, 0))
                    document.NewPage()
                    writer.DirectContent.AddImage(img)
                    img = Nothing
                    File.Delete()
                Next
                document.Close()
                fs = Nothing
                writer = Nothing
                document = Nothing
                LogSub("File created - " + filename)
            End If
        Next
        LogSub("Files converted" + vbCrLf)
    End Sub

    Private Sub MergeA4AndDrawingsSplit1(LogSub As IStep.LogSubDelegate)
        ' Documents by file will be the input1
        ' Drawings by file will be the input2

        ' I will have to go throg the subfolders of the input1 and then find the files to merge with the drawings
        ' The second folder should have pdfs of each subfolder containing the image files merged into a single file
        Dim dir1 As DirectoryInfo = New DirectoryInfo(CustomProperties.Input1)
        Dim dir2 As DirectoryInfo = New DirectoryInfo(CustomProperties.Input2)

        RecursiveMerge(dir1, dir2, LogSub)

        For Each subfile In dir2.GetFiles("*.PDF")
            LogSub("Drawing " + subfile.Name + " Not used in the process")
        Next

        LogSub("Merge done" + vbCrLf)
    End Sub

    Private Shared Sub RecursiveMerge(directory As DirectoryInfo, DrawingDir As DirectoryInfo, logsub As IStep.LogSubDelegate)
        ' Need to go through each folder and merge with the drawings
        For Each subfolder In directory.GetDirectories()
            RecursiveMerge(subfolder, DrawingDir, logsub)
        Next

        MergeFolder(directory, DrawingDir, logsub)
    End Sub

    Private Shared Sub MergeFolder(directory As DirectoryInfo, DrawingDir As DirectoryInfo, logsub As IStep.LogSubDelegate)
        Try
            Dim document As PdfDocument = Nothing
            Dim output As FileStream = Nothing
            Dim writer As PdfWriter = Nothing

            Dim Directoryfiles As List(Of FileInfo) = directory.GetFiles().ToList()
            Dim FinalFiles As List(Of FileInfo) = Directoryfiles.Where(Function(p) p.FullName.EndsWith("_NOBARCODE.pdf")).ToList()

            Dim outputFile As String = ""
            Directoryfiles.RemoveAll(Function(p) p.FullName.EndsWith("_NOBARCODE.pdf"))

            Dim FilesToMerge As List(Of String) = New List(Of String)

            For Each finalFile In FinalFiles
                FilesToMerge.Clear()
                FilesToMerge.Add(finalFile.FullName)
                Directoryfiles.OrderBy(Function(p) p.CreationTime)
                For Each subfile In Directoryfiles.Where(Function(p) p.FullName.StartsWith(finalFile.FullName.Replace("_NOBARCODE.pdf", "")) And p.FullName <> finalFile.FullName).OrderBy(Function(p) p.CreationTime)
                    If File.Exists(DrawingDir.FullName + "\" + subfile.Name.Replace(finalFile.Name.Replace("_NOBARCODE.pdf", ""), "").Replace("_", "").Replace("SD", "SP")) Then
                        FilesToMerge.Add(DrawingDir.FullName + "\" + subfile.Name.Replace(finalFile.Name.Replace("_NOBARCODE.pdf", ""), "").Replace("_", "").Replace("SD", "SP"))
                    Else
                        logsub(" ---- WARNING ----")
                        logsub("file " + subfile.Name.Replace(finalFile.Name.Replace("_NOBARCODE.pdf", ""), "").Replace("_", "").Replace("SD", "SP") + " Missing, Must be a CD")
                        logsub("refering to file " + finalFile.Name.Replace("_NOBARCODE", ""))
                    End If
                    FilesToMerge.Add(subfile.FullName)
                Next
                outputFile = finalFile.FullName.Replace("_NOBARCODE.pdf", ".pdf")

                For Each line In Utils.MergePdfs(FilesToMerge, outputFile)
                    logsub(line)
                Next
                System.Threading.Thread.Sleep(100)

                For Each subfile In FilesToMerge
                    My.Computer.FileSystem.DeleteFile(subfile)
                Next
            Next
        Catch e As Exception
            logsub(e.Message)
            Throw e
        End Try
    End Sub

    Private Sub CountPages(LogSub As IStep.LogSubDelegate)
        RecursiveCountPages(LogSub, New DirectoryInfo(CustomProperties.Input1))
        LogSub("Count Pages Done" + vbCrLf)
    End Sub

    Private Shared Sub RecursiveCountPages(Logsub As IStep.LogSubDelegate, Dir As DirectoryInfo)
        If File.Exists(Dir.FullName + "\results.csv") Then
            File.Delete(Dir.FullName + "\results.csv")
        End If
        If File.Exists(Dir.FullName + "\bardecodefiler.log") Then
            File.Delete(Dir.FullName + "\bardecodefiler.log")
        End If
        If File.Exists(Dir.FullName + "\_001.pdf") Then
            File.Delete(Dir.FullName + "\_001.pdf")
        End If
        For Each subdir In Dir.GetDirectories()
            RecursiveCountPages(Logsub, subdir)
        Next

        Dim A4 As Integer = 0
        Dim A3 As Integer = 0
        Dim A2 As Integer = 0
        Dim A1 As Integer = 0
        Dim A0 As Integer = 0

        Dim reader As PdfReader = Nothing
        Dim pageSize As iTextSharp.text.Rectangle = Nothing
        Dim StrOut As String = Nothing
        Dim pagecount As Integer = 0
        Dim drawingcount As Integer = 0

        For Each File In Dir.GetFiles("*.pdf")
            reader = New PdfReader(File.FullName)

            For index = 1 To reader.NumberOfPages
                pageSize = reader.GetPageSize(index)

                Select Case Utils.PageSize(pageSize.Height, pageSize.Width, True)
                    Case Project.PageSize.A0 : A0 += 1
                    Case Project.PageSize.A1 : A1 += 1
                    Case Project.PageSize.A2 : A2 += 1
                    Case Project.PageSize.A3 : A3 += 1
                    Case Project.PageSize.A4 : A4 += 1
                    Case Else : A4 += 1
                End Select
            Next
        Next

        StrOut = String.Format("{0}; {1}; {2}; {3}; {4}", A0, A1, A2, A3, A4)
        pagecount = A3 + A4
        drawingcount = A0 + A1 + A2

        If (pagecount + drawingcount) > 0 Then
            Dim connection As SqlConnection = New SqlConnection(FrmMain.ScanDataStringConnection)
            connection.Open()

            Dim command As SqlCommand = New SqlCommand("   UPDATE SCANDATA " + _
                                                       "      SET FIELD10 = '" + StrOut + "'," + _
                                                       "          PAGECOUNT = " + pagecount.ToString() + "," + _
                                                       "          DRAWINGCOUNT = " + drawingcount.ToString() + _
                                                       "   WHERE JOBNO = 2001 " + _
                                                       "     AND BARCODE = '" + Dir.Name + "'", connection)
            Logsub(String.Format("Updated ScanData barcode {0} - Rows Affected = ", Dir.Name) + command.ExecuteNonQuery().ToString())

            connection.Close()
            command.Dispose()
            connection.Dispose()
        End If
    End Sub

    Private Sub ConvertBWPDF(LogSub As IStep.LogSubDelegate)
        Dim img As Bitmap = Nothing
        Dim mimg As MagickImage = Nothing
        Dim rasterizer As GhostscriptRasterizer = New GhostscriptRasterizer()
        Dim folder As DirectoryInfo = New DirectoryInfo(CustomProperties.Input1)

        Dim encoderInfo As ImageCodecInfo = Nothing
        Dim encoderParams As EncoderParameters = New EncoderParameters(2)
        encoderInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/tiff")
        encoderParams.Param(1) = New EncoderParameter(Encoder.Compression, EncoderValue.CompressionCCITT4)
        encoderParams.Param(0) = New EncoderParameter(Encoder.ColorDepth, 8)

        Dim img2 As iTextSharp.text.Image = Nothing
        Try
            For Each subfolder In folder.GetDirectories()
                If subfolder.GetFiles(".pdf").Count > 0 Then
                    My.Computer.FileSystem.CreateDirectory(CustomProperties.Output + "\" + subfolder.Name)
                End If
                For Each File In subfolder.GetFiles("*.pdf")
                    Dim document As Document = New Document()
                    Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(CustomProperties.Output + "\" + subfolder.Name + "\Test_" + File.Name, FileMode.Create))
                    document.Open()
                    rasterizer.Open(File.FullName)
                    For index = 1 To rasterizer.PageCount
                        Dim a As Boolean = True
                        Dim size As Long = 0
                        Test.Convert(rasterizer.GetPage(200, 200, index), Directory.GetCurrentDirectory() + String.Format("\Images\temp{0}.png", index))
                        'With New MagickImage(rasterizer.GetPage(200, 200, index))
                        '    If index = 1 Then
                        '        size = .Height + .Width
                        '    End If
                        '    a = False
                        '    If a Then
                        '        a = True
                        '        Dim quant As QuantizeSettings = New QuantizeSettings()
                        '        quant.Colors = 256
                        '        quant.ColorSpace = ColorSpace.Gray
                        '        .Quantize(quant)
                        '        '.Write(Directory.GetCurrentDirectory() + "\temp.tiff")
                        '        Test.Convert(.ToBitmap, )
                        '        .ToBitmap.Save(Directory.GetCurrentDirectory() + "\temp.jpg", ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/jpeg"), encoderParams)
                        '        .ToBitmap.Save(Directory.GetCurrentDirectory() + String.Format("\Images\temp{0}.jpg", index), ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/jpeg"), encoderParams)
                        '        .Dispose()
                        '    Else
                        '        a = False
                        '        .ToBitmap.Save(Directory.GetCurrentDirectory() + "\temp.tiff", encoderInfo, encoderParams)
                        '        .ToBitmap.Save(Directory.GetCurrentDirectory() + String.Format("\Images\temp{0}.Tiff", index), encoderInfo, encoderParams)
                        '        .Dispose()
                        '    End If
                        'End With
                        'img = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + "\temp.tiff")
                        If a Then
                            img2 = Image.GetInstance(Directory.GetCurrentDirectory() + String.Format("\Images\temp{0}.png", index))
                        Else
                            img2 = Image.GetInstance(Directory.GetCurrentDirectory() + "\temp.tiff")
                        End If
                        img2.SetAbsolutePosition(0, 0)
                        document.SetPageSize(New iTextSharp.text.Rectangle(0, 0, img2.Width, img2.Height, 0))
                        document.NewPage()
                        writer.DirectContent.AddImage(img2)
                        img2 = Nothing
                    Next
                    document.Close()
                    writer.Dispose()
                    writer = Nothing
                    document = Nothing
                    rasterizer.Close()
                    File.Delete()
                Next
            Next
            rasterizer.Dispose()
            rasterizer = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub KetteringRename(LogSub As IStep.LogSubDelegate)
        LogSub("Renaming the files")

        Dim Documents As DirectoryInfo = New DirectoryInfo(CustomProperties.Input1)

        RecursiveKetteringRename(LogSub, Documents)
        For Each subfolder In Documents.GetDirectories()
            For Each subfile In subfolder.GetFiles()
                subfile.Delete()
            Next
            For Each subfolderlv2 In subfolder.GetDirectories()
                If File.Exists(subfolderlv2.FullName + "\" + subfolderlv2.Name + "NOBARCODE.pdf") Then
                    Dim filestomerge As List(Of String) = New List(Of String)
                    filestomerge.Add(subfolderlv2.FullName + "\" + subfolderlv2.Name + "NOBARCODE.pdf")
                    filestomerge.Add(subfolderlv2.FullName + "\" + subfolderlv2.Name + "- 01 - Application_.pdf")
                    Utils.MergePdfs(filestomerge, subfolderlv2.FullName + "\" + subfolderlv2.Name + "- 01 - Application.pdf", True)
                End If
            Next
        Next

        LogSub("Renaming Done")
    End Sub

    Private Shared Sub RecursiveKetteringRename(LogSub As IStep.LogSubDelegate, Dir As DirectoryInfo)
        Try
            Dim description As String = ""
            For Each File In Dir.GetFiles("*.pdf")
                description = ""
                If File.Name.Contains("EDSB") Then
                    description = "- 03 - General"
                End If
                If File.Name.Contains("EDSC") Then
                    description = "- 04 - Drawings-LOC-BP"
                End If
                If File.Name.Contains("EDSD") Then
                    description = "- 05 - Drawings-EL-FP-SEC"
                End If
                If File.Name.Contains("EDSE") Then
                    description = "- 06 - Drawings-SP-SL"
                End If
                If File.Name.Contains("EDSF") Then
                    description = "- 01 - Application_"
                End If
                If File.Name.Contains("EDSA") Then
                    description = "- 02 - Decision"
                End If
                If File.Name.Contains("NOBARCODE") Then
                    description = "NOBARCODE"
                End If
                File.MoveTo(File.Directory.Parent.FullName + "\" + File.Directory.Parent.Name + description + ".pdf")
            Next

            For Each subfile In Dir.GetFiles("*.csv")
                subfile.Delete()
            Next
            For Each subfile In Dir.GetFiles("*.log")
                subfile.Delete()
            Next

            For Each subdir In Dir.GetDirectories()
                RecursiveKetteringRename(LogSub, subdir)
            Next

            If Dir.GetDirectories.Count = 0 And Dir.GetFiles("*.pdf").Count = 0 Then
                Dir.Delete(True)
                Dir.Refresh()
            End If
        Catch
            LogSub("Problem Renaming the files, might be two same sections in one file")
        End Try
    End Sub

    Private Sub KetteringCount(LogSub As IStep.LogSubDelegate)
        RecursiveKetteringCount(LogSub, New DirectoryInfo(CustomProperties.Input1))
        LogSub("Count Pages Done" + vbCrLf)
    End Sub

    Private Shared Sub RecursiveKetteringCount(LogSub As IStep.LogSubDelegate, Dir As DirectoryInfo)
        For Each subdir In Dir.GetDirectories()
            RecursiveKetteringCount(LogSub, subdir)
        Next

        Dim A4 As Integer = 0
        Dim A3 As Integer = 0
        Dim A2 As Integer = 0
        Dim A1 As Integer = 0
        Dim A0 As Integer = 0

        Dim reader As PdfReader = Nothing
        Dim pageSize As iTextSharp.text.Rectangle = Nothing
        Dim StrOut As String = Nothing
        Dim pagecount As Integer = 0
        Dim drawingcount As Integer = 0

        For Each File In Dir.GetFiles("*.pdf")
            reader = New PdfReader(File.FullName)

            For index = 1 To reader.NumberOfPages
                pageSize = reader.GetPageSize(index)

                Select Case Utils.PageSize(pageSize.Height, pageSize.Width, True)
                    Case Project.PageSize.A0 : A0 += 1
                    Case Project.PageSize.A1 : A1 += 1
                    Case Project.PageSize.A2 : A2 += 1
                    Case Project.PageSize.A3 : A3 += 1
                    Case Project.PageSize.A4 : A4 += 1
                    Case Else : A4 += 1
                End Select
            Next
        Next

        StrOut = String.Format("{0}; {1}; {2}; {3}; {4}", A0, A1, A2, A3, A4)
        pagecount = A3 + A4
        drawingcount = A0 + A1 + A2

        If (pagecount + drawingcount) > 0 Then
            Dim connection As SqlConnection = New SqlConnection(FrmMain.ScanDataStringConnection)
            connection.Open()

            Dim command As SqlCommand = New SqlCommand("   UPDATE SCANDATA " + _
                                                       "      SET PAGECOUNT = " + pagecount.ToString() + "," + _
                                                       "          DRAWINGCOUNT = " + drawingcount.ToString() + _
                                                       "   WHERE JOBNO = 2017 " + _
                                                       "     AND BARCODE = '" + Dir.Name + "'", connection)
            LogSub(String.Format("Updated ScanData barcode {0} - Rows Affected = ", Dir.Name) + command.ExecuteNonQuery().ToString())

            connection.Close()
            command.Dispose()
            connection.Dispose()
        End If
    End Sub

    Private Sub MergePdfs(LogSub As IStep.LogSubDelegate)
        Dim Dir As DirectoryInfo = New DirectoryInfo(CustomProperties.Input1)
        Dim Files As List(Of String) = New List(Of String)

        For Each subfolder In Dir.GetDirectories()
            For Each subfile In subfolder.GetFiles("*.pdf")
                Files.Add(subfile.FullName)
            Next

            Utils.MergePdfs(Files, subfolder.FullName + "\" + subfolder.Name + ".pdf", True)
            Files.Clear()
        Next
        LogSub("Done")
    End Sub

    Private Sub ProcessKettering(LogSub As IStep.LogSubDelegate)
        ' This Step will be one step to process Everything

        Dim doc As iTextSharp.text.Document = Nothing
        Dim writer As PdfSmartCopy = Nothing

        Dim barcodeList As Dictionary(Of KeyValuePair(Of Integer, String), Dictionary(Of Integer, String)) = Nothing
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim copiedPage As PdfImportedPage = Nothing
        Dim LastPageToCopy As Integer = 0

        Dim docFolder As DirectoryInfo = New DirectoryInfo(CustomProperties.Input1)
        Dim drwFolder As DirectoryInfo = New DirectoryInfo(CustomProperties.Input2)

        Dim outputFolder As String = Nothing

        Dim DrawingCount As Integer = 0
        Dim PageCount As Integer = 0

        Dim AuditPageCount As Integer = 0
        Dim AuditPageCountOriginal As Integer = 0
        Dim AuditPlaceHolders As Integer = 0
        Dim AuditDrawingCount As Integer = 0
        Dim AuditDrawingAmount As Integer = 0

        Dim UpdateLogList As List(Of String) = New List(Of String)
        Dim RemovedPagesList As List(Of Integer) = New List(Of Integer)

        Dim drawpdfname As String = Nothing

        Dim firstsectionvalue As String = Nothing

        For Each subfolder In docFolder.GetDirectories()
            Try
                AuditDrawingAmount = 0
                AuditDrawingCount = 0
                AuditPageCount = 0
                AuditPageCountOriginal = 0
                AuditPlaceHolders = 0
                LogSub("Separating Drawings...")
                Dim drawingdirInfo As DirectoryInfo = New DirectoryInfo(Path.Combine(drwFolder.FullName, subfolder.Name + "D"))
                AuditDrawingAmount = drawingdirInfo.GetFiles("*.jpg").Count()
                AuditDrawingCount = SeparateImages(drawingdirInfo)
                outputFolder = Path.Combine(CustomProperties.Output, subfolder.Name)
                Dim subfile As FileInfo = New FileInfo(Path.Combine(subfolder.FullName, subfolder.Name + ".pdf"))
                'Dim ImageList As List(Of String) = subfolder.GetFiles("*.tif").ToList().OrderBy(Function(p) p.Name)
                LogSub(subfolder.Name + " Reading Barcodes...")
                barcodeList = getPdfBarcodes(subfile)
                reader = New PdfReader(subfile.FullName)

                LogSub(subfolder.Name + " Processing...")
                For Each element In barcodeList
                    PageCount = 0
                    DrawingCount = 0
                    firstsectionvalue = element.Value.Where(Function(p) p.Value.StartsWith("EDS")).OrderBy(Function(p) p.Value).ToList().First().Value

                    ' check if theres any pages between last file and the file start of this file

                    If (element.Key.Key - LastPageToCopy > 1) Then
                        For index = (LastPageToCopy + 1) To (element.Key.Key - 1)
                            RemovedPagesList.Add(index)
                        Next
                    End If

                    ' get the first barcode in the file after file start
                    LastPageToCopy = element.Value.First().Key
                    ' Check if theres any pages between file start barcode and the first section barcode
                    If (LastPageToCopy > 0) And (LastPageToCopy - element.Key.Key > 1) Then
                        For index = element.Key.Key + 1 To (LastPageToCopy - 1)
                            RemovedPagesList.Add(index)
                        Next
                    End If

                    Dim Placeholders As Integer() = (From placeholder In element.Value
                                                     Where New Regex("^([S][P])(\d{6})$").Match(placeholder.Value).Success
                                                     Select placeholder.Key).ToArray()
                    AuditPlaceHolders += Placeholders.Count()

                    For Each subelement In element.Value.Where(Function(p) p.Value.Contains("EDS")).OrderBy(Function(p) p.Value)
                        If (element.Value.Where(Function(p) p.Value = subelement.Value And p.Key = (subelement.Key + 1)).Count > 0) Then
                            RemovedPagesList.Add(subelement.Key)
                            Continue For
                        End If
                        Dim filename As String = Nothing
                        Select Case subelement.Value
                            Case "EDS1" : filename = "- 01 - Application.pdf"
                            Case "EDS2" : filename = "- 02 - Decision.pdf"
                            Case "EDS3" : filename = "- 03 - General.pdf"
                            Case "EDS4" : filename = "- 04 - Drawings-LOC-BP.pdf"
                            Case "EDS5" : filename = "- 05 - Drawings-EL-FP-SEC.pdf"
                            Case "EDS6" : filename = "- 06 - Drawings-SP-SL.pdf"
                        End Select

                        doc = New iTextSharp.text.Document()
                        If File.Exists(Path.Combine(outputFolder, element.Key.Value + filename)) Then
                            Throw New Exception("Duplicated section in file " + element.Key.Value)
                        End If
                        writer = New PdfSmartCopy(doc, New FileStream(Path.Combine(outputFolder, element.Key.Value + filename), FileMode.Create))
                        doc.Open()

                        If subelement.Value = firstsectionvalue Then
                            copiedPage = writer.GetImportedPage(reader, element.Key.Key)
                            writer.AddPage(copiedPage)
                            copiedPage = Nothing
                            PageCount += 1
                        End If

                        If element.Value.Any(Function(p) p.Value.Contains("EDS") And p.Key > (subelement.Key)) Then
                            LastPageToCopy = (element.Value.First(Function(p) p.Value.Contains("EDS") And p.Key > (subelement.Key)).Key - 1)
                        Else
                            LastPageToCopy = (element.Value.First(Function(p) p.Value = "EDFE").Key - 1)
                        End If
                        For index = subelement.Key To LastPageToCopy
                            If Placeholders.Contains(index) Then
                                Dim localindex As Integer = index
                                drawpdfname = element.Value.Single(Function(p) p.Key = localindex).Value + ".pdf"
                                Dim drawpdfreader As PdfReader = New PdfReader(Path.Combine(Path.Combine(drwFolder.FullName, subfolder.Name + "D"), drawpdfname))
                                DrawingCount += drawpdfreader.NumberOfPages
                                writer.AddDocument(drawpdfreader)
                                LogSub(String.Format("Added {0} to {1} on Page {2}", drawpdfreader.NumberOfPages, (element.Key.Value + filename), (index - subelement.Key + 1).ToString()))
                                drawpdfreader.Close()
                                drawpdfreader.Dispose()
                                My.Computer.FileSystem.DeleteFile(Path.Combine(Path.Combine(drwFolder.FullName, subfolder.Name + "D"), drawpdfname))
                            Else
                                copiedPage = writer.GetImportedPage(reader, index)
                                writer.AddPage(copiedPage)
                                copiedPage = Nothing
                                PageCount += 1
                            End If
                        Next

                        If (element.Value.Where(Function(p) p.Value.StartsWith("EDS")).OrderBy(Function(p) p.Value).ToList.IndexOf(subelement)) = (element.Value.Where(Function(p) p.Value.StartsWith("EDS")).ToList.Count - 1) Then
                            copiedPage = writer.GetImportedPage(reader, element.Value.First(Function(p) p.Value = "EDFE").Key)
                            writer.AddPage(copiedPage)
                            copiedPage = Nothing
                            PageCount += 1
                        End If

                        doc.Close()
                        writer.Close()
                        writer.Dispose()
                        doc.Dispose()
                    Next

                    ' always set last page as the file end page so i can check any blank between the end of this file and the begining of the next one
                    LastPageToCopy = element.Value.First(Function(p) p.Value = "EDFE").Key

                    If (PageCount + DrawingCount) > 0 Then
                        Dim connection As SqlConnection = New SqlConnection(FrmMain.ScanDataStringConnection)
                        connection.Open()

                        Dim command As SqlCommand = New SqlCommand("   UPDATE SCANDATA " + _
                                                                   "      SET PAGECOUNT = " + PageCount.ToString() + "," + _
                                                                   "          DRAWINGCOUNT = " + DrawingCount.ToString() + _
                                                                   "   WHERE JOBNO = 2017 " + _
                                                                   "     AND BARCODE = '" + element.Key.Value + "'", connection)
                        UpdateLogList.Add(String.Format("Updated ScanData barcode {0} - Rows Affected = ", element.Key.Value) + command.ExecuteNonQuery().ToString())

                        connection.Close()
                        command.Dispose()
                        connection.Dispose()
                    End If

                    AuditPageCount += PageCount
                Next

                If reader.NumberOfPages - LastPageToCopy >= 1 Then
                    For index = (LastPageToCopy + 1) To reader.NumberOfPages
                        RemovedPagesList.Add(index)
                    Next
                End If


                LogSub("")

                For Each notusedfile In New DirectoryInfo(Path.Combine(drwFolder.FullName, subfolder.Name + "D")).GetFiles(".pdf")
                    LogSub("Drawing not Used: " + notusedfile.Name)
                Next

                LogSub("")

                AuditPageCountOriginal = reader.NumberOfPages

                For Each line In UpdateLogList
                    LogSub(line)
                Next
                UpdateLogList.Clear()

                Dim blankpages As String = Nothing

                For Each page In RemovedPagesList
                    blankpages += "-" + page.ToString() + "-"
                Next

                blankpages = blankpages.Replace("--", ", ")
                blankpages = blankpages.Replace("-", "")

                LogSub("-- Audit --")

                LogSub("Original File PageCount: " + AuditPageCountOriginal.ToString())
                LogSub("After Process PageCount: " + AuditPageCount.ToString())
                LogSub("Original Amount of Drws: " + AuditDrawingAmount.ToString())
                LogSub("Amount of Drws by Brcd : " + AuditDrawingCount.ToString())
                LogSub("Amount of Placeholders : " + AuditPlaceHolders.ToString())
                LogSub("Pages Removed from file: " + RemovedPagesList.Count().ToString() + " (" + blankpages + ") ")
                LogSub("Amount of files in box : " + barcodeList.Count().ToString())
                LogSub("")


                RemovedPagesList.Clear()
                barcodeList.Clear()
                reader.Close()
                reader.Dispose()

                If subfile.Exists Then
                    subfile.Delete()
                End If
            Catch Ex As Exception
                If TypeOf (Ex) Is System.IO.FileNotFoundException Then
                    LogSub("Drawing " + Path.Combine(subfolder.Name, drawpdfname) + " not found!")
                    Throw Ex
                Else
                    LogSub(Ex.Message)
                    Throw Ex
                End If
            End Try
        Next
    End Sub

    Public Function getPdfBarcodes(pdf As FileInfo) As Dictionary(Of KeyValuePair(Of Integer, String), Dictionary(Of Integer, String))
        getPdfBarcodes = New Dictionary(Of KeyValuePair(Of Integer, String), Dictionary(Of Integer, String))

        Dim rasterizer As GhostscriptRasterizer = New GhostscriptRasterizer()
        Dim Barcode As String = Nothing

        If pdf.Exists Then
            rasterizer.Open(pdf.FullName)
        End If

        If pdf.Exists Then
            For index = 1 To rasterizer.PageCount
                Using Img As System.Drawing.Image = rasterizer.GetPage(200, 200, index)
                    Barcode = getImageBarcode(Img)

                    If Not IsNothing(Barcode) Then
                        If New Regex("^([K][T][T])(\d{6})$").Match(Barcode).Success Then
                            getPdfBarcodes.Add(New KeyValuePair(Of Integer, String)(index, Barcode), New Dictionary(Of Integer, String))
                        Else
                            Select Case Barcode
                                Case "EDSF" : Barcode = "EDS1" ' Application
                                Case "EDSA" : Barcode = "EDS2" ' Decision
                                Case "EDSB" : Barcode = "EDS3" ' General
                                Case "EDSC" : Barcode = "EDS4" ' Drawing LOC BP
                                Case "EDSD" : Barcode = "EDS5" ' Drawing EL FP SEC
                                Case "EDSE" : Barcode = "EDS6" ' Drawing SP SL
                            End Select
                            If index > 1 Then
                                getPdfBarcodes.Last.Value.Add(index, Barcode)
                            End If
                        End If
                    End If
                End Using
                Barcode = Nothing
            Next
            rasterizer.Close()
            rasterizer.Dispose()
        Else
            Dim imagelist As List(Of String) = pdf.Directory.GetFiles("*.tif").ToList().OrderBy(Function(p) p.Name)

            For Each Image In imagelist
                Try
                    Using Img As System.Drawing.Image = New System.Drawing.Bitmap(Image)
                        Barcode = getImageBarcode(Img)

                        If Not IsNothing(Barcode) Then
                            If New Regex("^([K][T][T])(\d{6})$").Match(Barcode).Success Then
                                getPdfBarcodes.Add(New KeyValuePair(Of Integer, String)(imagelist.IndexOf(Image), Barcode), New Dictionary(Of Integer, String))
                            Else
                                Select Case Barcode
                                    Case "EDSF" : Barcode = "EDS1" ' Application
                                    Case "EDSA" : Barcode = "EDS2" ' Decision
                                    Case "EDSB" : Barcode = "EDS3" ' General
                                    Case "EDSC" : Barcode = "EDS4" ' Drawing LOC BP
                                    Case "EDSD" : Barcode = "EDS5" ' Drawing EL FP SEC
                                    Case "EDSE" : Barcode = "EDS6" ' Drawing SP SL
                                End Select
                                If imagelist.IndexOf(Image) > 0 Then
                                    getPdfBarcodes.Last.Value.Add(imagelist.IndexOf(Image), Barcode)
                                End If
                            End If
                        End If
                    End Using
                    Barcode = Nothing
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Next
        End If
    End Function

    Public Function SeparateImages(Folder As DirectoryInfo) As Integer
        SeparateImages = 0
        Dim lastbarcodeFolder As String = Nothing
        Dim barcode As String = Nothing
        Dim FolderFiles As List(Of FileInfo) = (From file In Folder.GetFiles()
                                                Where file.Name.EndsWith(".jpg") Or file.Name.EndsWith(".tif")
                                                Select file).ToList()
        Dim index As Integer = 0

        For Each subfile In FolderFiles
            Using Img As System.Drawing.Image = Bitmap.FromFile(subfile.FullName)
                barcode = getImageBarcode(Img)
                Img.Dispose()
                If Not IsNothing(barcode) Then
                    If New Regex("^([S][D]|[S][P])(\d{6})$").Match(barcode).Success Then
                        SeparateImages += 1
                        lastbarcodeFolder = Path.Combine(Folder.FullName, barcode)
                        My.Computer.FileSystem.CreateDirectory(lastbarcodeFolder)
                    Else
                        subfile.MoveTo(Path.Combine(lastbarcodeFolder, subfile.Name))
                    End If
                End If
                If Not IsNothing(lastbarcodeFolder) Then
                    subfile.MoveTo(Path.Combine(lastbarcodeFolder, subfile.Name))
                Else
                    SeparateImages += 1
                    subfile.MoveTo(subfile.FullName.Replace(subfile.Name, String.Format("NOBARCODE_{0}.jpg", index)))
                    index += 1
                End If
            End Using
        Next

        Folder.Refresh()
        MergeDrawings(Folder)
    End Function

    Public Function getImageBarcode(Img As Bitmap) As String
        getImageBarcode = Nothing

        Dim reader As BarcodeReader = New BarcodeReader()
        Dim result As ZXing.Result = Nothing
        reader.Options.PossibleFormats = {BarcodeFormat.CODE_39}
        reader.Options.TryHarder = True

        result = reader.Decode(Img)

        If Not IsNothing(result) Then
            getImageBarcode = result.Text
        End If
        reader = Nothing
        result = Nothing
    End Function

    Private Sub MergeDrawings(dir As DirectoryInfo)
        Dim document As iTextSharp.text.Document = Nothing
        Dim filename As String = Nothing
        Dim fs As FileStream = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        ' Will have a input folder, with subfolders as files
        ' each file will have many images and need to be merged into a single multipage PDF
        Dim img As iTextSharp.text.Image = Nothing
        For Each subFolder In dir.GetDirectories()
            If (subFolder.GetFiles().Length > 0) And (Not subFolder.Name.StartsWith("EDW")) Then
                filename = subFolder.FullName.Replace("SD", "SP") + ".pdf"
                document = New iTextSharp.text.Document()
                fs = New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None)
                writer = PdfWriter.GetInstance(document, fs)
                document.Open()
                For Each File In subFolder.GetFiles()
                    img = iTextSharp.text.Image.GetInstance(File.FullName)
                    img.SetAbsolutePosition(0, 0)
                    document.SetPageSize(New iTextSharp.text.Rectangle(0, 0, img.Width, img.Height, 0))
                    document.NewPage()
                    writer.DirectContent.AddImage(img)
                    img = Nothing
                Next
                document.Close()
                fs = Nothing
                writer = Nothing
                document = Nothing
            End If
            subFolder.Delete(True)
        Next
    End Sub

    Private Sub ProcessWolverhampton(LogSub As IStep.LogSubDelegate)
        Dim docFolder As DirectoryInfo = New DirectoryInfo(CustomProperties.Input1)
        Dim drwFolder As DirectoryInfo = New DirectoryInfo(CustomProperties.Input2)

        Dim docReader As PdfReader = Nothing
        Dim rasterizer As GhostscriptRasterizer = Nothing

        Dim Barcodelist As List(Of Dictionary(Of Integer, String)) = New List(Of Dictionary(Of Integer, String))

        Dim encoderInfo As ImageCodecInfo = Nothing
        Dim encoderParams As EncoderParameters = New EncoderParameters(2)
        encoderParams.Param(1) = New EncoderParameter(Encoder.SaveFlag, CLng(EncoderValue.MultiFrame))

        Dim tess As ProcessStartInfo = New ProcessStartInfo(Directory.GetCurrentDirectory() + "\tesseract\tesseract.exe")
        tess.WindowStyle = ProcessWindowStyle.Hidden

        Dim pdfs As List(Of String) = New List(Of String)

        Dim format As String = Nothing

        Dim barcode As String = Nothing

        Dim doc As Document = Nothing
        Dim writer As PdfSmartCopy = Nothing

        Dim OCRProcess As Boolean = True
        Dim doOcr As Boolean = True

        Dim drawpdfname As String = Nothing
        Dim CopiedPage As PdfImportedPage = Nothing

        Dim outdir As String = Directory.GetCurrentDirectory() + "\_temp"
        If Directory.Exists(outdir) Then
            Directory.Delete(outdir, True)
        End If
        Directory.CreateDirectory(outdir)

        For Each boxfolder In docFolder.GetDirectories()

            SeparateImages(New DirectoryInfo(Path.Combine(drwFolder.FullName, boxfolder.Name + "D")))

            For Each InDocument In boxfolder.GetFiles("*.pdf")
                If OCRProcess Then
                    doOcr = Not InDocument.Name.EndsWith("_OCRED.pdf")
                Else
                    doOcr = False
                End If

                rasterizer = New GhostscriptRasterizer()
                rasterizer.Open(InDocument.FullName)
                docReader = New PdfReader(InDocument.FullName)

                Try
                    For index = 1 To rasterizer.PageCount

                        Using Img As MagickImage = New MagickImage(rasterizer.GetPage(200, 200, index))
                            ' first start the ocr process and then find the barcode

                            If doOcr Then
                                If Img.TotalColors > 50 Then
                                    encoderInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/jpeg")
                                    encoderParams.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionNone))
                                    format = "jpg"
                                    Img.ToBitmap().Save(String.Format(outdir + "\page{0}.{1}", index, format), encoderInfo, encoderParams)
                                Else
                                    encoderInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/tiff")
                                    encoderParams.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionCCITT4))
                                    format = "tif"
                                    Img.ToBitmap().Save(String.Format(outdir + "\page{0}.{1}", index, format), encoderInfo, encoderParams)
                                End If

                                tess.Arguments = String.Format("""" + outdir + "\page{0}.{1}"" """ + outdir + "\page{0}"" ""pdf""", index, format)
                                With System.Diagnostics.Process.Start(tess)
                                    .PriorityClass = ProcessPriorityClass.AboveNormal
                                    .Dispose()
                                End With
                                pdfs.Add(String.Format(outdir + "\page{0}.pdf", index))
                            End If

                            barcode = getImageBarcode(Img.ToBitmap)

                            If Not IsNothing(barcode) Then
                                If New Regex("^([W][V][C])(\d{7})$").Match(barcode).Success Then
                                    Barcodelist.Add(New Dictionary(Of Integer, String))
                                    Barcodelist.Last().Add(index, barcode)
                                Else
                                    If Barcodelist.Count > 0 Then
                                        Barcodelist.Last().Add(index, barcode)
                                    End If
                                End If
                            End If
                        End Using
                    Next

                    rasterizer.Close()
                    docReader.Close()
                    rasterizer.Dispose()
                    docReader.Dispose()

                    While System.Diagnostics.Process.GetProcessesByName("Tesseract").Count > 0
                        System.Threading.Thread.Sleep(1)
                    End While

                    If doOcr Then
                        Utils.MergePdfs(pdfs, InDocument.FullName.Replace(".pdf", "_OCRED.pdf"))
                    End If

                    Dim currentFolder As String = Nothing
                    For Each subfile In Barcodelist
                        currentFolder = Path.Combine(CustomProperties.Output, boxfolder.Name, subfile.First().Value)
                        My.Computer.FileSystem.CreateDirectory(currentFolder)

                        subfile.Values.First()

                        Dim placeholders As Integer() = (From placeholder In subfile
                                                         Where New Regex("^([S][P]|[S][D])(\d{6})$").Match(placeholder.Value).Success
                                                         Select placeholder.Key).ToArray()

                        Dim reader As PdfReader = Nothing
                        If doOcr Then
                            reader = New PdfReader(InDocument.FullName.Replace(".pdf", "_OCRED.pdf"))
                        Else
                            reader = New PdfReader(InDocument.FullName)
                        End If

                        Dim sections = subfile.Where(Function(p) New Regex("^([R][N][D])|(([W][V][C])(\d{7}))$").Match(p.Value).Success)
                        Dim lastpage As Integer = 0

                        For sectionindex = 0 To (sections.Count - 1)
                            If sections.Count > 1 Then
                                If (sectionindex = 0) And ((sections.ElementAt(0).Key + 1) = (sections.ElementAt(1).Key)) Then
                                    Continue For
                                End If
                            End If
                            doc = New iTextSharp.text.Document()
                            writer = New PdfSmartCopy(doc, New FileStream(Path.Combine(currentFolder, subfile.First().Value) + String.Format("_{0}.pdf", (sectionindex + 1).ToString("000")), FileMode.Create))
                            doc.Open()
                            If sections.Count > 1 Then
                                If (sectionindex = 1) And ((sections.ElementAt(0).Key + 1) = (sections.ElementAt(1).Key)) Then
                                    CopiedPage = writer.GetImportedPage(reader, subfile.First().Key)
                                    writer.AddPage(CopiedPage)
                                    CopiedPage = Nothing
                                End If
                            End If

                            If sectionindex = (sections.Count - 1) Then
                                lastpage = subfile.Single(Function(p) New Regex("^([E][D][F][E])$").Match(p.Value).Success).Key ' file end
                            Else
                                lastpage = sections.ElementAt(sectionindex + 1).Key - 1
                            End If

                            For index = sections.ElementAt(sectionindex).Key To lastpage
                                If placeholders.Contains(index) Then
                                    Dim localindex As Integer = index
                                    drawpdfname = Path.Combine(Path.Combine(drwFolder.FullName, boxfolder.Name + "D"), (subfile.Single(Function(p) p.Key = localindex).Value + ".pdf"))
                                    If File.Exists(drawpdfname) Then
                                        Dim drawpdfreader As PdfReader = New PdfReader(drawpdfname)
                                        writer.AddDocument(drawpdfreader)
                                        LogSub(String.Format("Added {0} to {1} on Page {2}", drawpdfreader.NumberOfPages, (subfile.First().Value + ".pdf"), (localindex).ToString()))
                                        drawpdfreader.Close()
                                        drawpdfreader.Dispose()
                                        My.Computer.FileSystem.DeleteFile(Path.Combine(Path.Combine(drwFolder.FullName, boxfolder.Name + "D"), drawpdfname))
                                    Else
                                        LogSub(" ---- WARNING ----")
                                        LogSub("file " + subfile.Single(Function(p) p.Key = localindex).Value + ".pdf" + " Missing, Must be a CD")
                                        LogSub("")
                                    End If
                                Else
                                    CopiedPage = writer.GetImportedPage(reader, index)
                                    writer.AddPage(CopiedPage)
                                    CopiedPage = Nothing
                                End If
                            Next
                            doc.Close()
                            doc.Dispose()
                            writer.Close()
                        Next
                        reader.Close()
                        reader.Dispose()
                    Next

                Catch ex As Exception
                    ' Log something to show that we got an exception
                    LogSub(ex.Message)
                    Exit For
                End Try
                If doOcr Then
                    My.Computer.FileSystem.DeleteFile(InDocument.FullName.Replace(".pdf", "_OCRED.pdf"))
                End If
                InDocument.Delete()
                InDocument.Refresh()
            Next
        Next
    End Sub

    Private Sub ConvertWolverhamptonDrawings(LogSub As IStep.LogSubDelegate)
        ' so the input should be a box with all the drawings so the input should be a box with all the drawings

        Dim document As iTextSharp.text.Document = Nothing
        Dim filename As String = Nothing
        Dim fs As FileStream = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        ' Will have a input folder, with subfolders as files
        ' each file will have many images and need to be merged into a single multipage PDF
        Dim img As iTextSharp.text.Image = Nothing
        For Each box In New DirectoryInfo(CustomProperties.Input1).GetDirectories()
            For Each subfolder In box.GetDirectories()
                If (subfolder.GetFiles("*.jpg").Length > 0) And (Not subfolder.Name.StartsWith("EDW")) Then
                    filename = subfolder.FullName + ".pdf"
                    document = New Document()
                    fs = New FileStream(filename.Replace("D.pdf", ".pdf"), FileMode.Create, FileAccess.Write, FileShare.None)
                    writer = PdfWriter.GetInstance(document, fs)
                    document.Open()
                    For Each File In subfolder.GetFiles("*.jpg")
                        img = Image.GetInstance(File.FullName)
                        img.SetAbsolutePosition(0, 0)
                        document.SetPageSize(New iTextSharp.text.Rectangle(0, 0, img.Width, img.Height, 0))
                        document.NewPage()
                        writer.DirectContent.AddImage(img)
                        img = Nothing
                    Next
                    document.Close()
                    fs = Nothing
                    writer = Nothing
                    document = Nothing
                    LogSub("File created - " + filename)
                End If
                subfolder.Delete(True)
            Next
        Next
        LogSub("Files converted" + vbCrLf)
    End Sub

    Private Sub ProcessKetteringImages(LogSub As IStep.LogSubDelegate)
        ' This Step will be one step to process Everything

        Dim doc As iTextSharp.text.Document = Nothing
        Dim writer As PdfWriter = Nothing
        Dim drawpdfreader As PdfReader = Nothing

        Dim barcodeList As Dictionary(Of KeyValuePair(Of Integer, String), Dictionary(Of Integer, String)) = Nothing
        Dim LastPageToCopy As Integer = -1

        Dim docFolder As DirectoryInfo = New DirectoryInfo(CustomProperties.Input1)
        Dim drwFolder As DirectoryInfo = New DirectoryInfo(CustomProperties.Input2)

        Dim outputFolder As String = Nothing

        Dim DrawingCount As Integer = 0
        Dim PageCount As Integer = 0

        Dim AuditPageCount As Integer = 0
        Dim AuditPageCountOriginal As Integer = 0
        Dim AuditPlaceHolders As Integer = 0
        Dim AuditDrawingCount As Integer = 0
        Dim AuditDrawingAmount As Integer = 0

        Dim UpdateLogList As List(Of String) = New List(Of String)
        Dim RemovedPagesList As List(Of Integer) = New List(Of Integer)

        Dim drawpdfname As String = Nothing

        Dim firstsectionvalue As String = Nothing
        Dim tempImg As Image = Nothing


        For Each subfolder In docFolder.GetDirectories()
            Try
                AuditDrawingAmount = 0
                AuditDrawingCount = 0
                AuditPageCount = 0
                AuditPageCountOriginal = 0
                AuditPlaceHolders = 0
                LogSub("Separating Drawings...")
                Dim drawingdirInfo As DirectoryInfo = New DirectoryInfo(Path.Combine(drwFolder.FullName, subfolder.Name + "D"))
                AuditDrawingAmount = drawingdirInfo.GetFiles("*.jpg").Count()
                AuditDrawingCount = SeparateImages(drawingdirInfo)
                outputFolder = Path.Combine(CustomProperties.Output, subfolder.Name)

                LogSub(subfolder.Name + " Reading Barcodes...")
                barcodeList = getPdfBarcodesImages(subfolder)
                Dim imagelist As List(Of String) = (From image In subfolder.GetFiles("*.tif").OrderBy(Function(p) p.Name)
                                                    Select image.FullName).ToList()
                LogSub(subfolder.Name + " Processing...")
                For Each element In barcodeList
                    PageCount = 0
                    DrawingCount = 0
                    firstsectionvalue = element.Value.Where(Function(p) p.Value.StartsWith("EDS")).OrderBy(Function(p) p.Value).ToList().First().Value

                    ' check if theres any pages between last file and the file start of this file

                    If (element.Key.Key - LastPageToCopy > 1) Then
                        For index = (LastPageToCopy + 1) To (element.Key.Key - 1)
                            RemovedPagesList.Add(index)
                            My.Computer.FileSystem.DeleteFile(imagelist.ElementAt(index))
                        Next
                    End If

                    ' get the first barcode in the file after file start
                    LastPageToCopy = element.Value.First().Key
                    ' Check if theres any pages between file start barcode and the first section barcode
                    If (LastPageToCopy > 0) And (LastPageToCopy - element.Key.Key > 1) Then
                        For index = element.Key.Key + 1 To (LastPageToCopy - 1)
                            RemovedPagesList.Add(index)
                            My.Computer.FileSystem.DeleteFile(imagelist.ElementAt(index))
                        Next
                    End If

                    Dim Placeholders As Integer() = (From placeholder In element.Value
                                                     Where New Regex("^([S][P])(\d{6})$").Match(placeholder.Value).Success
                                                     Select placeholder.Key).ToArray()
                    AuditPlaceHolders += Placeholders.Count()

                    For Each subelement In element.Value.Where(Function(p) p.Value.Contains("EDS")).OrderBy(Function(p) p.Value)
                        If (element.Value.Where(Function(p) p.Value = subelement.Value And p.Key = (subelement.Key + 1)).Count > 0) Then
                            RemovedPagesList.Add(subelement.Key)
                            Continue For
                        End If
                        Dim filename As String = Nothing
                        Select Case subelement.Value
                            Case "EDS1" : filename = "- 01 - Application.pdf"
                            Case "EDS2" : filename = "- 02 - Decision.pdf"
                            Case "EDS3" : filename = "- 03 - General.pdf"
                            Case "EDS4" : filename = "- 04 - Drawings-LOC-BP.pdf"
                            Case "EDS5" : filename = "- 05 - Drawings-EL-FP-SEC.pdf"
                            Case "EDS6" : filename = "- 06 - Drawings-SP-SL.pdf"
                        End Select

                        doc = New iTextSharp.text.Document()
                        If File.Exists(Path.Combine(outputFolder, element.Key.Value + filename)) Then
                            Throw New Exception("Duplicated section in file " + element.Key.Value)
                        End If
                        writer = PdfWriter.GetInstance(doc, New FileStream(Path.Combine(outputFolder, element.Key.Value + filename), FileMode.Create))
                        doc.Open()

                        If subelement.Value = firstsectionvalue Then
                            tempImg = iTextSharp.text.Image.GetInstance(imagelist.ElementAt(element.Key.Key))
                            tempImg.SetAbsolutePosition(0, 0)
                            doc.SetPageSize(New iTextSharp.text.Rectangle(0, 0, tempImg.Width, tempImg.Height, 0))
                            doc.NewPage()
                            writer.DirectContent.AddImage(tempImg)
                            tempImg = Nothing
                            PageCount += 1
                            My.Computer.FileSystem.DeleteFile(imagelist.ElementAt(element.Key.Key))
                        End If

                        If element.Value.Any(Function(p) p.Value.Contains("EDS") And p.Key > (subelement.Key)) Then
                            LastPageToCopy = (element.Value.First(Function(p) p.Value.Contains("EDS") And p.Key > (subelement.Key)).Key - 1)
                        Else
                            LastPageToCopy = (element.Value.First(Function(p) p.Value = "EDFE").Key - 1)
                        End If
                        For index = subelement.Key To LastPageToCopy
                            If Placeholders.Contains(index) Then
                                Dim localindex As Integer = index
                                Dim drawfolder As DirectoryInfo = New DirectoryInfo(Path.Combine(drwFolder.FullName, Path.Combine(subfolder.Name + "D", element.Value.Single(Function(p) p.Key = localindex).Value.Replace("SP", "SD"))))
                                DrawingCount += drawfolder.GetFiles().Count()
                                For Each subfile In drawfolder.GetFiles()
                                    tempImg = Image.GetInstance(subfile.FullName)
                                    tempImg.SetAbsolutePosition(0, 0)
                                    doc.SetPageSize(New iTextSharp.text.Rectangle(0, 0, tempImg.Width, tempImg.Height, 0))
                                    doc.NewPage()
                                    writer.DirectContent.AddImage(tempImg)
                                    tempImg = Nothing
                                Next
                                LogSub(String.Format("Added {0} to {1} on Page {2}", drawfolder.GetFiles().Count(), (element.Key.Value + filename), (index - subelement.Key + 1).ToString()))
                                drawfolder.Delete(True)
                                drawfolder.Refresh()
                                drawfolder = Nothing
                                My.Computer.FileSystem.DeleteFile(imagelist.ElementAt(index))
                            Else
                                tempImg = iTextSharp.text.Image.GetInstance(imagelist.ElementAt(index))
                                tempImg.SetAbsolutePosition(0, 0)
                                doc.SetPageSize(New iTextSharp.text.Rectangle(0, 0, tempImg.Width, tempImg.Height, 0))
                                doc.NewPage()
                                writer.DirectContent.AddImage(tempImg)
                                tempImg = Nothing
                                PageCount += 1
                                My.Computer.FileSystem.DeleteFile(imagelist.ElementAt(index))
                            End If
                        Next

                        If (element.Value.Where(Function(p) p.Value.StartsWith("EDS")).OrderBy(Function(p) p.Value).ToList.IndexOf(subelement)) = (element.Value.Where(Function(p) p.Value.StartsWith("EDS")).ToList.Count - 1) Then
                            tempImg = iTextSharp.text.Image.GetInstance(imagelist.ElementAt(element.Value.First(Function(p) p.Value = "EDFE").Key))
                            tempImg.SetAbsolutePosition(0, 0)
                            doc.SetPageSize(New iTextSharp.text.Rectangle(0, 0, tempImg.Width, tempImg.Height, 0))
                            doc.NewPage()
                            writer.DirectContent.AddImage(tempImg)
                            tempImg = Nothing
                            PageCount += 1
                            My.Computer.FileSystem.DeleteFile(imagelist.ElementAt(element.Value.First(Function(p) p.Value = "EDFE").Key))
                        End If

                        doc.Close()
                        doc.Dispose()
                        writer.Close()
                        writer.Dispose()
                    Next

                    ' always set last page as the file end page so i can check any blank between the end of this file and the begining of the next one
                    LastPageToCopy = element.Value.First(Function(p) p.Value = "EDFE").Key

                    If (PageCount + DrawingCount) > 0 Then
                        Dim connection As SqlConnection = New SqlConnection(FrmMain.ScanDataStringConnection)
                        connection.Open()

                        Dim command As SqlCommand = New SqlCommand("   UPDATE SCANDATA " + _
                                                                   "      SET PAGECOUNT = " + PageCount.ToString() + "," + _
                                                                   "          DRAWINGCOUNT = " + DrawingCount.ToString() + _
                                                                   "   WHERE JOBNO = 2017 " + _
                                                                   "     AND BARCODE = '" + element.Key.Value + "'", connection)
                        UpdateLogList.Add(String.Format("Updated ScanData barcode {0} - Rows Affected = ", element.Key.Value) + command.ExecuteNonQuery().ToString())

                        connection.Close()
                        command.Dispose()
                        connection.Dispose()
                    End If

                    AuditPageCount += PageCount
                Next

                If (imagelist.Count - 1) - LastPageToCopy >= 1 Then
                    For index = (LastPageToCopy + 1) To (imagelist.Count - 1)
                        RemovedPagesList.Add(index)
                        My.Computer.FileSystem.DeleteFile(imagelist.ElementAt(index))
                    Next
                End If


                LogSub("")

                For Each notusedfile In New DirectoryInfo(Path.Combine(drwFolder.FullName, subfolder.Name + "D")).GetDirectories()
                    LogSub("Drawing not Used: " + notusedfile.Name)
                Next

                LogSub("")

                AuditPageCountOriginal = imagelist.Count

                For Each line In UpdateLogList
                    LogSub(line)
                Next
                UpdateLogList.Clear()

                Dim blankpages As String = Nothing

                For Each page In RemovedPagesList
                    blankpages += "-" + imagelist(page).Replace(subfolder.FullName, "") + "-"
                Next

                blankpages = blankpages.Replace("--", ", ")
                blankpages = blankpages.Replace("-", "")

                LogSub("-- Audit --")

                LogSub("Original File PageCount: " + AuditPageCountOriginal.ToString())
                LogSub("After Process PageCount: " + AuditPageCount.ToString())
                LogSub("Original Amount of Drws: " + AuditDrawingAmount.ToString())
                LogSub("Amount of Drws by Brcd : " + AuditDrawingCount.ToString())
                LogSub("Amount of Placeholders : " + AuditPlaceHolders.ToString())
                LogSub("Pages Removed from file: " + RemovedPagesList.Count().ToString() + " (" + blankpages + ") ")
                LogSub("Amount of files in box : " + barcodeList.Count().ToString())
                LogSub("")


                RemovedPagesList.Clear()
                barcodeList.Clear()

            Catch Ex As Exception
                If TypeOf (Ex) Is System.IO.FileNotFoundException Then
                    LogSub("Drawing " + Path.Combine(subfolder.Name, drawpdfname) + " not found!")
                    Throw Ex
                Else
                    LogSub(Ex.Message)
                    Throw Ex
                End If
            End Try
        Next
    End Sub

    Public Function getPdfBarcodesImages(folder As DirectoryInfo) As Dictionary(Of KeyValuePair(Of Integer, String), Dictionary(Of Integer, String))
        getPdfBarcodesImages = New Dictionary(Of KeyValuePair(Of Integer, String), Dictionary(Of Integer, String))

        Dim rasterizer As GhostscriptRasterizer = New GhostscriptRasterizer()
        Dim Barcode As String = Nothing

        Dim imagelist As List(Of String) = (From image In folder.GetFiles("*.tif").OrderBy(Function(p) p.Name)
                                            Select image.FullName).ToList()

        For Each Image In imagelist
            Using Img As System.Drawing.Image = New System.Drawing.Bitmap(Image)
                Barcode = getImageBarcode(Img)

                If Not IsNothing(Barcode) Then
                    If New Regex("^([K][T][T])(\d{6})$").Match(Barcode).Success Then
                        getPdfBarcodesImages.Add(New KeyValuePair(Of Integer, String)(imagelist.IndexOf(Image), Barcode), New Dictionary(Of Integer, String))
                    Else
                        Select Case Barcode
                            Case "EDSF" : Barcode = "EDS1" ' Application
                            Case "EDSA" : Barcode = "EDS2" ' Decision
                            Case "EDSB" : Barcode = "EDS3" ' General
                            Case "EDSC" : Barcode = "EDS4" ' Drawing LOC BP
                            Case "EDSD" : Barcode = "EDS5" ' Drawing EL FP SEC
                            Case "EDSE" : Barcode = "EDS6" ' Drawing SP SL
                        End Select
                        If imagelist.IndexOf(Image) > 0 Then
                            getPdfBarcodesImages.Last.Value.Add(imagelist.IndexOf(Image), Barcode)
                        End If
                    End If
                End If
            End Using
            Barcode = Nothing
        Next
    End Function
End Class
