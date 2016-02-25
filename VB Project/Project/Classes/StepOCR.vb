Imports System.IO
Imports System.Collections
Imports Ghostscript.NET.Rasterizer
Imports Clock.Pdf
Imports Clock.Hocr

Public Class StepOCR
    Implements IStep

    Public OCRProperties As PropertiesOCR
    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.OCR
        End Get
    End Property

    Sub New()
        Me.OCRProperties = New PropertiesOCR()
    End Sub

    Public Sub RunFile(File As FileInfo) Implements IStep.RunFile
        Dim htmlfile As FileStream
        Dim hdoc As hDocument = New hDocument
        Dim index As Integer = 0

        Dim pdfset As PDFSettings = New PDFSettings()
        pdfset.ImageType = PdfImageType.Tif
        pdfset.ImageQuality = 100
        pdfset.Dpi = 200
        pdfset.PdfOcrMode = Clock.Util.OcrMode.Tesseract
        pdfset.WriteTextMode = WriteTextMode.Word

        Dim pdfCreator As PdfCreator = New PdfCreator(pdfset, Me.OCRProperties.OutputDirectory.FullName + "\" + Replace(File.Name, ".pdf", Me.OCRProperties.OutputNameTemplate))

        For Each page As KeyValuePair(Of Bitmap, String) In Me.GetOCRedPages(File)
            htmlfile = New FileStream(String.Format(Me.OCRProperties.OutputDirectory.FullName + "\page{0}.html", index), FileMode.Create, FileAccess.Write)
            With New StreamWriter(htmlfile)
                .Write(page.Value)
                .Close()
                .Dispose()
            End With
            hdoc.AddFile(htmlfile.Name)
            pdfCreator.AddPage(hdoc.Pages(hdoc.Pages.Count - 1), page.Key)
            hdoc.Pages.RemoveAt(hdoc.Pages.Count - 1)
            htmlfile.Dispose()
            If Not Me.OCRProperties.SaveHtmlFiles Then
                My.Computer.FileSystem.DeleteFile(String.Format(Me.OCRProperties.OutputDirectory.FullName + "\page{0}.html", index))
            End If
            index = index + 1
        Next
        pdfCreator.SaveAndClose()
        pdfCreator.Dispose()
        GC.Collect()
    End Sub

    Private Function GetOCRedPages(File As FileInfo) As Dictionary(Of Bitmap, String)
        GetOCRedPages = New Dictionary(Of Bitmap, String)
        Dim index As Integer

        Dim tes As Tesseract.TesseractEngine = New Tesseract.TesseractEngine(Me.OCRProperties.TesseractData, Me.OCRProperties.TesseractLanguage)
        ' seting whitelist to avoid weird characters on final text
        tes.SetVariable("tessedit_char_whitelist", " $.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz,'-()\/")
        For Each item As Bitmap In Me.ExtractImagesFromPdf(File, File.Directory)
            With tes.Process(item)
                GetOCRedPages.Add(item, .GetHOCRText(0))
                .Dispose()
            End With
            If Me.OCRProperties.SaveImageFiles Then
                item.Save(String.Format(Me.OCRProperties.OutputDirectory.FullName + "\page{0}." + Me.OCRProperties.SaveImageFormat.ToString, index), Me.OCRProperties.SaveImageFormat)
                index = index + 1
            End If
        Next
        tes.Dispose()
        GC.Collect()
    End Function

    Private Function ExtractImagesFromPdf(Pdf As FileInfo, OutputFolder As DirectoryInfo) As List(Of Bitmap)
        ExtractImagesFromPdf = New List(Of Bitmap)

        Dim rasterizer = New GhostscriptRasterizer()
        rasterizer.Open(Pdf.FullName)
        For index As Integer = 1 To rasterizer.PageCount
            ExtractImagesFromPdf.Add(rasterizer.GetPage(200, 200, index))
        Next
        rasterizer.Dispose()
    End Function

    Public Sub RunFiles(Files As List(Of FileInfo)) Implements IStep.RunFiles

    End Sub

    Public Sub RunFolder(Folder As DirectoryInfo, RunSubFolders As Boolean, SearchPattern As String) Implements IStep.RunFolder

    End Sub
End Class
