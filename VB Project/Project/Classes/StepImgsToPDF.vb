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
    Public Property ImgsToPDFProperties As PropertiesImgsToPDF

    Public Sub RunFile(File As FileInfo) Implements IStep.RunFile
        Me.Doc = New PdfDocument()

        If ImgsToPDFProperties.InputFormats.Contains(File.Extension) Then
            Me.Pag = Me.Doc.AddPage
            Me.Gfx = XGraphics.FromPdfPage(Me.Pag)
            Me.GfxPoint = New XPoint()
            Me.XImg = XImage.FromFile(File.FullName)
            Me.Pag.Height = Me.XImg.Size.Height
            Me.Pag.Width = Me.XImg.Size.Width
            Me.Gfx.DrawImage(Me.XImg, Me.GfxPoint)
        End If

        Me.Doc.Save(Replace(File.FullName, File.Extension, ".pdf"))
        Me.Doc.Dispose()
    End Sub

    Public Sub RunFiles(Files As List(Of FileInfo)) Implements IStep.RunFiles
        If Me.ImgsToPDFProperties.MergeOutput = MergeOutputType.FilePerFolder Then
            Me.Doc = New PdfDocument()
        End If

        For Each File In Files
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
                    Me.Doc.Save(Me.ImgsToPDFProperties.Outputfolder.FullName + "\" + Replace(File.Name, File.Extension, ".pdf"))
                    Me.Doc.Dispose()
                End If
            End If
        Next

        If Me.ImgsToPDFProperties.MergeOutput = MergeOutputType.FilePerFolder Then
            Me.Doc.Save(Me.ImgsToPDFProperties.Outputfolder.FullName + "\" + Me.ImgsToPDFProperties.OutputName)
            Me.Doc.Dispose()
        End If
    End Sub

    Public Sub RunFolder(Folder As DirectoryInfo, RunSubFolders As Boolean, SearchPattern As String) Implements IStep.RunFolder
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
                    Me.Doc.Save(ImgsToPDFProperties.Outputfolder.FullName + "\" + Replace(File.Name, File.Extension, ".pdf"))
                    Me.Doc.Dispose()
                End If
            End If
        Next

        If Me.ImgsToPDFProperties.MergeOutput = MergeOutputType.FilePerFolder Then
            Me.Doc.Save(ImgsToPDFProperties.Outputfolder.FullName + "\" + ImgsToPDFProperties.OutputName)
            Me.Doc.Dispose()
        End If
    End Sub

    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.ImgsToPDF
        End Get
    End Property
End Class
