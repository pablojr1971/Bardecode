Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Drawing.Imaging

Public NotInheritable Class Utils
    Public Delegate Function AfterMergeDelegate(doc As Document)

    Public Shared Function MergePdfs(InputFiles As List(Of String), outputFile As String, Optional AfterMergeFunction As AfterMergeDelegate = Nothing) As List(Of String)
        Dim a As Integer = InputFiles.Count
        Dim ret(a) As String
        Dim index As Integer = 0

        Dim document As Document = New Document()
        Dim writer As PdfCopy = New PdfCopy(document, New FileStream(outputFile, FileMode.Create))
        Dim outinfo As FileInfo = New FileInfo(outputFile)
        Dim finfo As FileInfo = Nothing
        Dim reader As PdfReader = Nothing

        Try
            MergePdfs = New List(Of String)
            If IsNothing(writer) Then
                Exit Function
            End If

            document.Open()
            For Each File In InputFiles
                finfo = New FileInfo(File)
                reader = New PdfReader(File)
                If (Not File.EndsWith("_NOBARCODE.pdf") And New FileInfo(File).Name.StartsWith("SP")) Then
                    MergePdfs.Add(String.Format("Add {3} Drawings of {0} to {1} at page {2}", finfo.Name, outinfo.Name, (writer.CurrentPageNumber), reader.NumberOfPages))
                End If

                writer.AddDocument(reader)
                reader.Close()
                finfo = Nothing
                index += 1
            Next

            writer.Close()
            If Not IsNothing(AfterMergeFunction) Then
                AfterMergeFunction(document)
            End If
        Catch e As Exception
            Throw e
        Finally
            reader.Close()
            document.Close()
            reader.Dispose()
            writer.Close()
            writer.Dispose()
        End Try
    End Function

    Public Shared Sub SplitFileSize(file As String, MBFileSize As Integer)
        Dim reader As PdfReader = New PdfReader(file)
        Dim documentcount As Integer = 1
        Dim page As PdfImportedPage = Nothing
        Dim NewFiles As List(Of String) = New List(Of String)
        ' times 1024 to convert to kBytes and times 1024 to conver to bytes
        Dim LimitBytes As Long = ((MBFileSize * 1024) * 1024)

        If reader.FileLength() > LimitBytes Then
            Dim document As Document = New Document()
            Dim pdfwriter As PdfCopy = New PdfSmartCopy(document, New FileStream(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)), FileMode.Create))
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

                    document = New Document()
                    pdfwriter = New PdfCopy(document, New FileStream(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)), FileMode.Create))
                    NewFiles.Add(file.Replace(".pdf", String.Format("_{0}.pdf", documentcount)))

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

    Public Shared Function PageSize(Height As Integer, Width As Integer, Optional IsPDF As Boolean = False) As PageSize
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
            If (Width > 5651) Or (Height > 7993) Then
                Return PageSize.A0
            ElseIf (InRange(Width, 5650, 3994) Or InRange(Height, 7992, 5651)) Then
                Return PageSize.A1
            ElseIf (InRange(Width, 3993, 2823) Or InRange(Height, 5650, 3994)) Then
                Return PageSize.A2
            ElseIf (InRange(Width, 2822, 1996) Or InRange(Height, 3993, 2823)) Then
                Return PageSize.A3
            ElseIf (InRange(Width, 1995) Or InRange(Height, 2822)) Then
                Return PageSize.A4
            Else
                Return PageSize.Undefined
            End If
        Else
            ' Landscape/Horizontal
            If (Height > 5651) Or (Width > 7993) Then
                Return PageSize.A0
            ElseIf (InRange(Height, 5650, 3994) Or InRange(Width, 7992, 5651)) Then
                Return PageSize.A1
            ElseIf (InRange(Height, 3993, 2823) Or InRange(Width, 5650, 3994)) Then
                Return PageSize.A2
            ElseIf (InRange(Height, 2822, 1996) Or InRange(Width, 3993, 2823)) Then
                Return PageSize.A3
            ElseIf (InRange(Height, 1995) Or InRange(Width, 2822)) Then
                Return PageSize.A4
            Else
                Return PageSize.Undefined
            End If
        End If
    End Function

    Public Shared Function InRange(ByVal value As Integer, ByVal max As Integer, Optional ByVal min As Integer = 0) As Boolean
        Return (value >= min AndAlso value <= max)
    End Function

    Public Shared Function GetOutputSubFolder(InputFolder As String, CurrentFolder As String) As String
        ' this will return all folders above the input folder, so we can just concat into the outputfolder
        Return CurrentFolder.Replace(InputFolder, "")
    End Function
End Class