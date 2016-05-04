Imports System.IO
Imports System.Reflection
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports Ghostscript.NET.Rasterizer
Imports ImageMagick
Imports System.Drawing.Imaging

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
                For Each subfile In Directoryfiles.Where(Function(p) p.FullName.StartsWith(finalFile.FullName.Replace("_NOBARCODE.pdf", "_")) And p.FullName <> finalFile.FullName)
                    If File.Exists(DrawingDir.FullName + "\" + subfile.Name.Replace(finalFile.Name.Replace("NOBARCODE.pdf", ""), "").Replace("SD", "SP")) Then
                        FilesToMerge.Add(DrawingDir.FullName + "\" + subfile.Name.Replace(finalFile.Name.Replace("NOBARCODE.pdf", ""), "").Replace("SD", "SP"))
                    Else
                        logsub(" ---- WARNING ----")
                        logsub("file " + subfile.Name.Replace(finalFile.Name.Replace("NOBARCODE.pdf", ""), "").Replace("SD", "SP") + " Missing, Must be a CD")
                        logsub("refering to file " + finalFile.Name.Replace("_NOBARCODE", ""))
                    End If
                    FilesToMerge.Add(subfile.FullName)
                Next
                outputFile = finalFile.FullName.Replace("_NOBARCODE.pdf", ".pdf")

                For Each line In Utils.MergePdfs(FilesToMerge, outputFile)
                    logsub(line)
                Next
                Threading.Thread.Sleep(100)

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
        Dim encoderParams As EncoderParameters = New EncoderParameters(1)
        encoderInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/tiff")
        encoderParams.Param(0) = New EncoderParameter(Encoder.Compression, EncoderValue.CompressionCCITT3)

        Dim img2 As iTextSharp.text.Image = Nothing
        Try
            For Each subfolder In folder.GetDirectories()
                For Each File In subfolder.GetFiles("*.pdf")
                    Dim document As Document = New Document()
                    Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(CustomProperties.Output + "\Test_" + File.Name, FileMode.Create))
                    document.Open()
                    rasterizer.Open(File.FullName)
                    For index = 1 To rasterizer.PageCount
                        With New MagickImage(rasterizer.GetPage(200, 200, index))
                            .ColorType = ColorType.Bilevel
                            .DetermineColorType()
                            '.Write(Directory.GetCurrentDirectory() + "\temp.tiff")
                            .ToBitmap.Save(Directory.GetCurrentDirectory() + "\temp.tiff", encoderInfo, encoderParams)
                            .Dispose()
                        End With
                        'img = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + "\temp.tiff")
                        img2 = Image.GetInstance(Directory.GetCurrentDirectory() + "\temp.tiff")
                        img2.SetAbsolutePosition(0, 0)
                        document.SetPageSize(New iTextSharp.text.Rectangle(0, 0, img2.Width, img2.Height, 0))
                        document.NewPage()
                        writer.DirectContent.AddImage(img2)
                        img.Dispose()
                        img = Nothing
                        img2 = Nothing
                        My.Computer.FileSystem.DeleteFile(Directory.GetCurrentDirectory() + "\temp.tiff")
                    Next
                    document.Close()
                    writer.Dispose()
                    writer = Nothing
                    document = Nothing
                    rasterizer.Close()
                    rasterizer.Dispose()
                    rasterizer = Nothing
                    File.Delete()
                Next
            Next
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
End Class
