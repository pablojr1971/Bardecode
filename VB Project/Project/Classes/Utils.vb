Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Drawing.Imaging
Imports System.Windows.Media.Imaging

Public NotInheritable Class Utils
    Public Delegate Function AfterMergeDelegate(doc As Document)

    Public Shared Function MergePdfs(InputFiles As List(Of String), outputFile As String, Optional DeleteFiles As Boolean = False, Optional AfterMergeFunction As AfterMergeDelegate = Nothing) As List(Of String)
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

            If DeleteFiles Then
                For Each File In InputFiles
                    My.Computer.FileSystem.DeleteFile(File)
                Next
            End If

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
            Height = ((Height / 72) * 200)
            Width = ((Width / 72) * 200)
        End If


        If Height > Width Then
            ' Vertical
            If (Width > 5651) Or (Height > 7993) Then
                Return PageSize.A0
            ElseIf (InRange(Width, 5650, 3994) Or InRange(Height, 7992, 5651)) Then
                Return PageSize.A1
            ElseIf (InRange(Width, 3993, 2823) Or InRange(Height, 5650, 3994)) Then
                Return PageSize.A2
            ElseIf (InRange(Width, 2822, 1700) Or InRange(Height, 3993, 2600)) Then
                Return PageSize.A3
            ElseIf (InRange(Width, 1699) Or InRange(Height, 2599)) Then
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
            ElseIf (InRange(Height, 3993, 3000) Or InRange(Width, 5650, 4200)) Then
                Return PageSize.A2
            ElseIf (InRange(Height, 2999, 1700) Or InRange(Width, 4199, 2600)) Then
                Return PageSize.A3
            ElseIf (InRange(Height, 1699) Or InRange(Width, 2599)) Then
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

    Public Shared Sub Convert(Image As Bitmap, Path As String)

        Dim image2 As Bitmap = MakeGrayscale(Image)

        Dim fcb As System.Windows.Media.Imaging.FormatConvertedBitmap = New FormatConvertedBitmap(System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image2.GetHbitmap(System.Drawing.Color.Transparent), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(image2.Width, image2.Height)), System.Windows.Media.PixelFormats.Gray8, BitmapPalettes.Gray256, 0.5)
        Dim pngBitmapEncoder As PngBitmapEncoder = New PngBitmapEncoder()
        pngBitmapEncoder.Interlace = PngInterlaceOption.Off
        pngBitmapEncoder.Frames.Add(BitmapFrame.Create(fcb))
        Dim fileStream As Stream = File.Open(Path, FileMode.Create)
        pngBitmapEncoder.Save(fileStream)
        fileStream.Close()
    End Sub

    Public Shared Function MakeGrayscale(ByVal original As Bitmap) As Bitmap
        ' This function creates a greyscale copy of the original Image by changing the colormatrix attribute of the Image.

        'create a blank bitmap the same size as original
        Dim newBitmap As Bitmap = New Bitmap(original.Width, original.Height)
        'get a graphics object from the new image
        Dim g As Graphics = Graphics.FromImage(newBitmap)
        'create the grayscale ColorMatrix

        Dim colorMatrix As ColorMatrix = New ColorMatrix(New Single()() { _
          New Single() {0.3F, 0.3F, 0.3F, 0, 0}, _
          New Single() {0.59F, 0.59F, 0.59F, 0, 0}, _
          New Single() {0.11F, 0.11F, 0.11F, 0, 0}, _
          New Single() {0, 0, 0, 1, 0}, _
          New Single() {0, 0, 0, 0, 1}})

        'create some image attributes
        Dim attributes As ImageAttributes = New ImageAttributes()
        'set the color matrix attribute
        attributes.SetColorMatrix(colorMatrix)
        'draw the original image on the new image using the grayscale color matrix
        g.DrawImage(original, New System.Drawing.Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes)
        'dispose the Graphics object
        g.Dispose()
        Return newBitmap
    End Function
End Class