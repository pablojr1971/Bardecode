Imports System.Reflection
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports Ghostscript.NET.Rasterizer

Public Class StepCustom
    Implements IStep

    Public CustomPropeties As PropertiesCustom

    Public Sub New()
        CustomPropeties = New PropertiesCustom()
    End Sub

    Public Sub New(Properties As PropertiesCustom)
        Me.CustomPropeties = Properties
    End Sub

    Public Sub Run(LogSub As IStep.LogSubDelegate) Implements IStep.Run
        LogSub("Running " + CustomPropeties.CustomRunID + vbCrLf)
        Me.GetType.InvokeMember(Me.CustomPropeties.CustomRunID,
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

    Private Sub TestePdf(LogSub As IStep.LogSubDelegate)
        LogSub("Loading the PDF")

        Dim rasterizer As GhostscriptRasterizer = New GhostscriptRasterizer()
        rasterizer.Open(CustomPropeties.Input1)

        Dim xydif As Integer = 0
        Dim xyratio As Double = 0

        For index = 1 To rasterizer.PageCount
            With rasterizer.GetPage(300, 300, index)
                xydif = .Height - .Width
                xydif = IIf(xydif < 0, (xydif * -1), xydif)
                xyratio = IIf(.Height > .Width, (.Height / .Width), (.Width / .Height))

                If (xyratio <= 1.42 And xyratio >= 1.32) Then
                    LogSub(String.Format("Page {0} is A4 - Heigh = {1} and Width = {2}", index, .Height, .Width))
                Else
                    LogSub(String.Format("Page {0} is Custom Size - Heigh = {1} and Width = {2}", index, .Height, .Width))
                End If
                .Save(String.Format(CustomPropeties.Output + "\Image{0}.Tiff", index), System.Drawing.Imaging.ImageFormat.Tiff)
                .Dispose()
            End With
        Next

        rasterizer.Dispose()
        rasterizer = Nothing
    End Sub

    Private Sub TesteImageSize(LogSub As IStep.LogSubDelegate)
        LogSub("Start")

        For Each file In New DirectoryInfo(CustomPropeties.Input1).GetFiles()
            With New Bitmap(file.FullName)
                LogSub(.Height.ToString + " x " + .Width.ToString + " = " + (.Height * 2).ToString + " x " + (.Width * 2).ToString + "  =  " + (.Height / .Width).ToString)
            End With
        Next
    End Sub

    Private Sub MergeA4andDrawings(LogSub As IStep.LogSubDelegate)
        Dim document As PdfDocument = Nothing
        Dim output As FileStream = Nothing
        Dim writer As PdfWriter = Nothing

        Dim Directoryfiles As List(Of String) = Directory.GetFiles(CustomPropeties.Input1).ToList()
        Dim FinalFiles As List(Of String) = Directoryfiles.Where(Function(p) p.EndsWith("_NOBARCODE.pdf")).ToList()

        Dim outputFile As String = ""
        Directoryfiles.RemoveAll(Function(p) p.EndsWith("_NOBARCODE.pdf"))

        Dim FilesToMerge As List(Of String) = New List(Of String)

        For Each finalFile In FinalFiles
            FilesToMerge.Clear()
            FilesToMerge.Add(finalFile)
            For Each subfile In Directoryfiles.Where(Function(p) p.StartsWith(finalFile.Replace("_NOBARCODE.pdf", "")))
                FilesToMerge.Add(CustomPropeties.Input2 + "\" + subfile.Substring(subfile.Length - 11, 11))
                FilesToMerge.Add(subfile)
            Next
            outputFile = CustomPropeties.Output + "\" + (New FileInfo(finalFile).Name.Replace("_NOBARCODE", ""))
            LogSub(outputFile)
            PDFMerge.MergePdfs(FilesToMerge.ToArray, outputFile)
        Next
    End Sub

    Private Sub ConvertJPGTOPDF(LogSub As IStep.LogSubDelegate)
        Dim document As iTextSharp.text.Document = Nothing
        Dim filename As String = Nothing
        Dim fs As FileStream = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        ' Will have a input folder, with subfolders as files
        ' each file will have many images and need to be merged into a single multipage PDF
        Dim img As iTextSharp.text.Image = Nothing
        For Each subFolder In New DirectoryInfo(CustomPropeties.Input1).GetDirectories()
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
        Next
    End Sub

    Private Sub MergeA4AndDrawingsSplit1(LogSub As IStep.LogSubDelegate)
        ' Documents by file will be the input1
        ' Drawings by file will be the input2

        ' I will have to go throg the subfolders of the input1 and then find the files to merge with the drawings
        ' The second folder should have pdfs of each subfolder containing the image files merged into a single file

        RecursiveMerge(New DirectoryInfo(CustomPropeties.Input1), New DirectoryInfo(CustomPropeties.Input2))
    End Sub

    Private Shared Sub RecursiveMerge(directory As DirectoryInfo, DrawingDir As DirectoryInfo)
        ' Need to go through each folder and merge with the drawings
        For Each subfolder In directory.GetDirectories()
            RecursiveMerge(subfolder, DrawingDir)
        Next

        MergeFolder(directory, DrawingDir)
    End Sub



    Private Shared Sub MergeFolder(directory As DirectoryInfo, DrawingDir As DirectoryInfo)
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
                FilesToMerge.Add(DrawingDir.FullName + "\" + subfile.FullName.Substring(subfile.FullName.Length - 11, 11))
                FilesToMerge.Add(subfile.FullName)
            Next
            outputFile = finalFile.FullName.Replace("_NOBARCODE.pdf", "_Final.pdf")
            PDFMerge.MergePdfs(FilesToMerge.ToArray, outputFile)
        Next

        ' Delete the files
        For Each File In Directoryfiles
            My.Computer.FileSystem.DeleteFile(File.FullName)
        Next
        For Each File In FinalFiles
            My.Computer.FileSystem.DeleteFile(File.FullName)
        Next
    End Sub
End Class
