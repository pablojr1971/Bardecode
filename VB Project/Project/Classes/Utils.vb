Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Drawing.Imaging

Public NotInheritable Class Utils
    Public Delegate Function AfterMergeDelegate(doc As Document)

    Public Shared Sub MergePdfs(InputFiles As List(Of String), outputFile As String, Optional AfterMergeFunction As AfterMergeDelegate = Nothing)

        Dim document As Document = New Document()
        Dim writer As PdfCopy = New PdfCopy(document, New FileStream(outputFile, FileMode.Create))
        If IsNothing(writer) Then
            Exit Sub
        End If

        document.Open()
        For Each File In InputFiles
            Dim reader As PdfReader = New PdfReader(File)
            writer.AddDocument(reader)
            reader.Close()
        Next

        writer.Close()
        If Not IsNothing(AfterMergeFunction) Then
            AfterMergeFunction(document)
        End If
        document.Close()

        writer.Dispose()
        document.Dispose()
    End Sub

    Public Shared Sub SplitFileSize(file As String, MBFileSize As Integer)
        Dim reader As PdfReader = New PdfReader(file)
        Dim documentcount As Integer = 1
        Dim page As PdfImportedPage = Nothing
        ' times 1024 to convert to kBytes and times 1024 to conver to bytes
        Dim LimitBytes As Long = ((MBFileSize * 1024) * 1024)

        If reader.FileLength() > LimitBytes Then
            Dim document As Document = New Document()
            Dim pdfwriter As PdfCopy = New PdfSmartCopy(document, New FileStream(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)), FileMode.Create))
            document.Open()
            For index = 1 To reader.NumberOfPages
                page = pdfwriter.GetImportedPage(reader, index)

                If (pdfwriter.CurrentDocumentSize + getPageFileSize(page)) > LimitBytes Then
                    pdfwriter.Close()
                    document.Close()
                    pdfwriter.Dispose()
                    document.Dispose()
                    documentcount += 1

                    document = New Document()
                    pdfwriter = New PdfCopy(document, New FileStream(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)), FileMode.Create))

                    document.Open()
                    pdfwriter.AddPage(page)
                Else
                    pdfwriter.AddPage(page)
                End If
            Next
            pdfwriter.Close()
            document.Close()
            pdfwriter.Dispose()
            document.Dispose()
        End If

        reader.Close()
        reader = Nothing
    End Sub

    Private Shared Function getPageFileSize(Page As PdfImportedPage) As Long
        Dim document As Document = New Document()
        Dim writer As PdfCopy = New PdfCopy(document, New MemoryStream())
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

    Public Shared Function GetPageSize(Height As Integer, Width As Integer, Optional IsPDF As Boolean = False) As PageSize
        ' this should work only for images at 200dpi less or greater than that the results will be wrong
        ' We have this parameter to know if it is pdf or not because when the images are placed in the pdf 
        ' they change a bit the sizes.

        ' Multiply the input sizes by 1.2 should solve the problem
        If IsPDF Then
            Height *= 1.2
            Width *= 1.2
        End If


        If Height > Width Then
            ' Vertical
            If (Width > 5651) And (Height > 7993) Then
                Return PageSize.A0
            ElseIf (InRange(Width, 5650, 3994) And InRange(Height, 7992, 5651)) Then
                Return PageSize.A1
            ElseIf (InRange(Width, 3993, 2823) And InRange(Height, 5650, 3994)) Then
                Return PageSize.A2
            ElseIf (InRange(Width, 2822, 1996) And InRange(Height, 3993, 2823)) Then
                Return PageSize.A3
            ElseIf (InRange(Width, 1995) And InRange(Height, 2822)) Then
                Return PageSize.A4
            Else
                Return PageSize.Undefined
            End If
        Else
            ' Landscape/Horizontal
            If (Height > 5651) And (Width > 7993) Then
                Return PageSize.A0
            ElseIf (InRange(Height, 5650, 3994) And InRange(Width, 7992, 5651)) Then
                Return PageSize.A1
            ElseIf (InRange(Height, 3993, 2823) And InRange(Width, 5650, 3994)) Then
                Return PageSize.A2
            ElseIf (InRange(Height, 2822, 1996) And InRange(Width, 3993, 2823)) Then
                Return PageSize.A3
            ElseIf (InRange(Height, 1995) And InRange(Width, 2822)) Then
                Return PageSize.A4
            Else
                Return PageSize.Undefined
            End If
        End If
    End Function

    Public Shared Function InRange(ByVal value As Integer, ByVal max As Integer, Optional ByVal min As Integer = 0) As Boolean
        Return (value >= min AndAlso value <= max)
    End Function

    Public Shared Sub DoOCR(input As String, output As String)
        ' just call the tesseract with the image input
        With System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\Tesseract\tesseract.exe",
                                              """" + input + """ " + """" + output + """ pdf")
            .WaitForExit()
            .Close()
            .Dispose()
        End With

    End Sub

    Private Shared Sub CompressAndSaveImage(ByVal img As System.Drawing.Image, ByVal fileName As String, ByVal quality As Long)
        Dim parameters As New EncoderParameters(1)
        parameters.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality)
        img.Save(fileName, GetCodecInfo("image/tiff"), parameters)
    End Sub

    Public Shared Function GetCodecInfo(ByVal mimeType As String) As ImageCodecInfo
        For Each encoder As ImageCodecInfo In ImageCodecInfo.GetImageEncoders()
            If encoder.MimeType = mimeType Then
                Return encoder
            End If
        Next encoder
        Throw New ArgumentOutOfRangeException(String.Format("'{0}' not supported", mimeType))
    End Function


    Public Shared Function GetOutputSubFolder(InputFolder As String, CurrentFolder As String) As String
        ' this will return all folders above the input folder, so we can just concat into the outputfolder
        Return CurrentFolder.Replace(InputFolder, "")
    End Function
End Class