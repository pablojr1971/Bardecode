Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public NotInheritable Class PDFMerge
    Public Shared Sub MergePdfs(InputFiles As String(), outputFile As String)

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
        document.Close()
    End Sub
End Class
