Imports System.IO
Imports System.Collections
Imports Ghostscript.NET.Rasterizer
Imports Clock.Pdf
Imports Clock.Hocr
Imports Clock.Util

Public Class StepOCR
    Implements IStep

    Private tes As Tesseract.TesseractEngine
    Public OCRProperties As PropertiesOCR
    Public ReadOnly Property Type As Project.StepType Implements IStep.Type
        Get
            Return StepType.OCR
        End Get
    End Property

    Sub New()
        Me.OCRProperties = New PropertiesOCR()
        Me.OCRProperties.SetDefaultValues()
        Me.tes = New Tesseract.TesseractEngine(Directory.GetCurrentDirectory + "\tessdata", "eng")
        tes.SetVariable("tessedit_char_whitelist", " $.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz,'-()\/")
    End Sub

    Sub New(Properties As PropertiesOCR)
        Me.OCRProperties = Properties
        Me.tes = New Tesseract.TesseractEngine(Directory.GetCurrentDirectory + "\tessdata", "eng")
        tes.SetVariable("tessedit_char_whitelist", " $.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz,'-()\/")
    End Sub

    Public Sub RunFile(File As FileInfo, OutFolder As String, LogSub As IStep.LogSubDelegate)
        Dim currentPage As Image
        Dim pdfset As PDFSettings = New PDFSettings()
        pdfset.ImageType = PdfImageType.Jpg
        pdfset.ImageQuality = 150
        pdfset.Dpi = 500
        pdfset.PdfOcrMode = Clock.Util.OcrMode.TesseractDigitsOnly
        pdfset.WriteTextMode = WriteTextMode.Line
        pdfset.Language = "eng"

        Dim hdoc As hDocument = New hDocument()
        Dim hpage As hPage = New hPage()
        Dim pdfCreator As PdfCreator = New PdfCreator(pdfset, Replace(OutFolder + "\" + File.Name, ".pdf", Me.OCRProperties.OutputNameTemplate))

        Dim rasterizer = New GhostscriptRasterizer()

        rasterizer.Open(File.FullName)
        For index As Integer = 1 To rasterizer.PageCount
            LogSub(String.Format("Page {0} of {1}", index, rasterizer.PageCount))
            currentPage = rasterizer.GetPage(200, 200, index)
            If {41, 42}.Contains(index) Then
                pdfCreator.AddPage(currentPage)
                Continue For
            End If
            With tes.Process(currentPage)
                OCRParser.ParseHOCR(hdoc, .GetHOCRText(0, True), True)
                pdfCreator.AddPage(hdoc.Pages(hdoc.Pages.Count - 1), currentPage)
                hdoc.Pages.RemoveAt(hdoc.Pages.Count - 1)

                .Dispose()
            End With
        Next
        pdfCreator.SaveAndClose()
        pdfCreator.Dispose()
        rasterizer.Dispose()
        GC.Collect()
    End Sub

    Public Shared Function LoadStep(StepId As Integer, ctx As VBProjectContext) As StepOCR
        With ctx.ESteps.Single(Function(p) p.Id = StepId)
            LoadStep = New StepOCR(Serializer.FromXml(.PropertiesObj, GetType(PropertiesOCR)))
        End With
    End Function

    Public Sub Run(LogSub As IStep.LogSubDelegate) Implements IStep.Run
        Me.RecursiveRun(New DirectoryInfo(OCRProperties.InputFolder), OCRProperties.ProcessSubFolders, LogSub)
    End Sub

    Private Sub RecursiveRun(Folders As DirectoryInfo, RunSubFolders As Boolean, LogSub As IStep.LogSubDelegate)
        If RunSubFolders Then
            For Each subfolder In Folders.GetDirectories()
                RecursiveRun(subfolder, True, LogSub)
            Next
        End If

        For Each File In Folders.GetFiles("*.pdf")
            RunFile(File, OCRProperties.OutputFolder, LogSub)
        Next
    End Sub
End Class
