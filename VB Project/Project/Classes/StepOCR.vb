Imports System.IO
Imports System.Collections
Imports System.Drawing.Imaging
Imports Ghostscript.NET.Rasterizer
Imports ImageMagick


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
    End Sub

    Sub New(Properties As PropertiesOCR)
        Me.OCRProperties = Properties
    End Sub

    Public Sub RunFile(File As FileInfo, OutFolder As String, LogSub As IStep.LogSubDelegate)
        LogSub("OCR Start - File:" + File.Name)
        Dim rasterizer As GhostscriptRasterizer = New GhostscriptRasterizer()
        Dim outdir As String = Directory.GetCurrentDirectory() + "\_temp"

        Dim tess As System.Diagnostics.ProcessStartInfo = Nothing
        Dim pdfs As List(Of String) = New List(Of String)()

        tess = New ProcessStartInfo(Directory.GetCurrentDirectory() + "\tesseract\tesseract.exe")
        tess.WindowStyle = ProcessWindowStyle.Hidden

        Dim parameters As New EncoderParameters(1)
        parameters.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 25)

        If Not Directory.Exists(outdir) Then
            Directory.CreateDirectory(outdir)
        End If

        Dim img As Image = Nothing
        Dim mimg As MagickImage = Nothing

        Dim encoderInfo As ImageCodecInfo = Nothing
        Dim encoderParams As EncoderParameters = New EncoderParameters(2)
        encoderParams.Param(1) = New EncoderParameter(Encoder.SaveFlag, CLng(EncoderValue.MultiFrame))

        Dim format As String = Nothing

        rasterizer.Open(File.FullName)
        For index = 1 To rasterizer.PageCount
            Try
                img = rasterizer.GetPage(200, 200, index)
                mimg = New MagickImage(img)

                ' Check total color in the image, if less than 50 is greyscale of B&W and can be saved as compressed tiff
                ' if the total colors is greater than 50 the image is colored and we will save as a compressed JPG
                If mimg.TotalColors > 50 Then
                    encoderInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/jpeg")
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionNone))
                    format = "jpg"
                    img.Save(String.Format(outdir + "\page{0}.{1}", index, format), encoderInfo, encoderParams)
                Else
                    encoderInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/tiff")
                    encoderParams.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionCCITT4))
                    format = "tif"
                    img.Save(String.Format(outdir + "\page{0}.{1}", index, format), encoderInfo, encoderParams)
                End If

                mimg.Dispose()
                mimg = Nothing
                img.Dispose()
                img = Nothing

                tess.Arguments = String.Format("""" + outdir + "\page{0}.{1}"" """ + outdir + "\page{0}"" ""pdf""", index, format)
                LogSub(String.Format("Processing Page {0}", index))
                With System.Diagnostics.Process.Start(tess)
                    .Dispose()
                End With
                pdfs.Add(String.Format(outdir + "\page{0}.pdf", index))
            Catch e As Exception
                LogSub(String.Format("Error OCRing page {0} of the Document {1}", index, File.Name) + vbCrLf + e.Message)            
            End Try
        Next

        While System.Diagnostics.Process.GetProcessesByName("Tesseract").Count > 0
            ' Wait till all OCR processes started on the loop finish
            ' We need to wait because if the we don't when merge the pdfs the file will be locked to the tesseract process
            ' and we will get an error saying that the tile is being used in another process
            Threading.Thread.Sleep(100)
        End While

        rasterizer.Dispose()

        If OCRProperties.CreateOutputSubFolders Then
            Utils.MergePdfs(pdfs, OCRProperties.OutputFolder + Utils.GetOutputSubFolder(OCRProperties.InputFolder, File.FullName.Replace(".pdf", OCRProperties.OutputNameTemplate) + ".pdf"))
        Else
            Utils.MergePdfs(pdfs, OCRProperties.OutputFolder + "\" + File.Name.Replace(".pdf", OCRProperties.OutputNameTemplate) + ".pdf")
        End If
        My.Computer.FileSystem.DeleteDirectory(outdir, FileIO.DeleteDirectoryOption.DeleteAllContents)
        Threading.Thread.Sleep(500)
        If OCRProperties.DeleteInputFile Then
            File.Delete()
        End If
        LogSub("OCR Done - File:" + File.Name + vbCrLf)
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
            If Not File.Name.EndsWith("_OCRED.pdf") Then
                RunFile(File, OCRProperties.OutputFolder, LogSub)
            End If
        Next
    End Sub
End Class
