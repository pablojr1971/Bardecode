Imports System.IO
Imports PdfSharp.Pdf
Imports PdfSharp.Drawing
Imports Project.PropertiesImgsToPDF

Public Class StepImgsToPDF
    Implements IStep

    Dim Doc As PdfDocument = Nothing
    Dim Pag As PdfPage = Nothing
    Dim Gfx As XGraphics = Nothing
    Dim GfxPoint As XPoint = Nothing
    Dim XImg As XImage = Nothing
    Public ImgsToPDFProperties As PropertiesImgsToPDF

    Public Sub New()
        Me.ImgsToPDFProperties = New PropertiesImgsToPDF()
    End Sub

    Public Sub New(Properties As PropertiesImgsToPDF)
        Me.ImgsToPDFProperties = Properties
    End Sub

    Public Sub RunFolder(Folder As DirectoryInfo, RunSubFolders As Boolean, SearchPattern As String)
        If Me.ImgsToPDFProperties.MergeOutput = MergeOutputType.FilePerFolder Then
            Me.Doc = New PdfDocument()
        End If

        For Each File In Folder.GetFiles(SearchPattern)
            If Me.ImgsToPDFProperties.InputFormats.Contains(File.Extension) Then

                If Me.ImgsToPDFProperties.MergeOutput = MergeOutputType.FilePerFile Then
                    Me.Doc = New PdfDocument()
                End If

                Me.Pag = Me.Doc.AddPage
                Me.Gfx = XGraphics.FromPdfPage(Me.Pag)
                Me.GfxPoint = New XPoint()
                Me.XImg = XImage.FromFile(File.FullName)

                Me.Pag.Height = Me.XImg.Size.Height
                Me.Pag.Width = Me.XImg.Size.Width
                Me.Gfx.DrawImage(Me.XImg, Me.GfxPoint)

                If Me.ImgsToPDFProperties.MergeOutput = MergeOutputType.FilePerFile Then
                    Me.Doc.Save(Directory.GetCurrentDirectory + "\Processing\Drawings\" + Replace(File.Name, File.Extension, ".pdf"))
                    Me.Doc.Dispose()
                End If
            End If
        Next

        If Me.ImgsToPDFProperties.MergeOutput = MergeOutputType.FilePerFolder Then
            Me.Doc.Save(Directory.GetCurrentDirectory + "\Processing\Drawings\" + Me.ImgsToPDFProperties.OutputName)
            Me.Doc.Dispose()
        End If
    End Sub

    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.ImgsToPDF
        End Get
    End Property

    Public Shared Function LoadStep(StepId As Integer, ctx As VBProjectContext) As StepImgsToPDF
        With ctx.ESteps.Single(Function(p) p.Id = StepId)
            LoadStep = New StepImgsToPDF(Serializer.FromXml(.PropertiesObj, GetType(PropertiesImgsToPDF)))
        End With
    End Function

    Public Sub Run(LogSub As IStep.LogSubDelegate) Implements IStep.Run

    End Sub
End Class
