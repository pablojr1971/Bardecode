Imports System.IO
Imports System.Text.RegularExpressions
Imports iTextSharp.text.pdf
Imports ImageMagick
Imports ZXing

Public Class Processing
    Dim LogObj As LogManager
    Dim ParamsObj As ProcessParams
    Dim boxFolder As String
    Dim boxNumber As String

    Dim DocFolder As DirectoryInfo = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing\Documents")
    Dim DrwFolder As DirectoryInfo = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing\Drawings")
    Dim Ocrfolder As DirectoryInfo = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing\OCR")

    Dim Barcodelist As List(Of Dictionary(Of Integer, String)) = New List(Of Dictionary(Of Integer, String))
    Dim ImageList As List(Of String) = New List(Of String)
    Dim tess As ProcessStartInfo = New ProcessStartInfo(Directory.GetCurrentDirectory() + "\tesseract\tesseract.exe")

    Dim barcodeReader As ZXing.BarcodeReader = New ZXing.BarcodeReader()
    Dim ImageList As List(Of String)

    Public Sub New(boxName As String, logObj As LogManager, Parameters As ProcessParams)
        Me.LogObj = logObj
        Me.ParamsObj = Parameters
        Me.boxFolder = boxName
        Me.boxNumber = boxName.Substring(boxName.Length - 6)
    End Sub

    Public Sub Run()
        Dim CurrentBarcode = Nothing
        Dim result As ZXing.Result = Nothing
        If Directory.Exists(Directory.GetCurrentDirectory() + "\Processing") Then
            Directory.Delete(Directory.GetCurrentDirectory() + "\Processing")
        End If

        DocFolder.Create()
        DrwFolder.Create()
        Ocrfolder.Create()

        barcodeReader.Options.PossibleFormats = {ZXing.BarcodeFormat.CODE_39}
        barcodeReader.Options.TryHarder = True

        LogObj.CreateLogFile(Path.Combine(ParamsObj.OutFolder, boxFolder))
        LogObj.WriteLine("Copying box " + boxNumber)
        CopyBoxToProcessingFolder()

        ImageList = (From image In DocFolder.GetFiles("*.tif").OrderBy(Function(p) p.Name)
                     Select image.FullName).ToList()

        If ParamsObj.OCRA4 Then
            LogObj.WriteLine("Reading Barcodes/OCRing")
        Else
            LogObj.WriteLine("Reading Barcodes")
        End If

        For index = 0 To ImageList.Count - 1
            Using Img As MagickImage = New MagickImage(ImageList(index))
                SaveTempImage(Img, index)
                OCRImage(index)

                result = barcodeReader.Decode(Img.ToBitmap)
                If Not IsNothing(result) Then
                    CurrentBarcode = result.Text
                    prepareBarcodeList(CurrentBarcode)
                End If

                File.Delete(ImageList(index))
            End Using
        Next

        While System.Diagnostics.Process.GetProcessesByName("Tesseract").Count > 0
            System.Threading.Thread.Sleep(1)
        End While

        ConsistSectionsAndFiles()

        MountPdfFiles()

        LogObj.CloseLogFile()
    End Sub

    Public Sub SaveTempImage(Img As MagickImage, Index As Integer)
        If ParamsObj.BWA4 Then
            '   

            '

            '
        End If

        ' check the parameters and save the image according BW or not
    End Sub

    Public Sub OCRImage(Index As Integer)
        ' check the parameter and do OCR with the documents
    End Sub

    Public Sub prepareBarcodeList(barcode As String)
        ' check if the barcode match the file start regex and them create a new file in the list
        ' if it doesn't match the file start regex, see if it match any of the section barcodes 
        ' if it does, place it as the lastElement on the sections property of the current file
        ' match the every section property should contain a fileEnd as the last parameter
        ' after have read all the barcodes, should put everything in a variable and return a list

    End Sub

    Public Sub ConsistSectionsAndFiles()
        ' in this sub we should consist everything about the file
        ' FileStart, FileEnd, sections
        ' Verify the required sections, if they don't appear in the file should throw and exception 
        ' if they don't appear in the file should throw and exception
    End Sub

    Public Sub MountPdfFiles()
        ' mount the pdffiles
    End Sub

    Private Sub CopyBoxToProcessingFolder()
        Dim boxPath As String = Path.Combine(ParamsObj.InpFolder, boxFolder)
        Dim drwPath As String = boxPath + "D"

        My.Computer.FileSystem.CopyDirectory(boxPath, DocFolder.FullName)
        My.Computer.FileSystem.CopyDirectory(drwPath, DrwFolder.FullName)
    End Sub

    Public Function SeparateImages() As Integer
        SeparateImages = 0
        Dim lastbarcodeFolder As String = Nothing
        Dim barcode As String = Nothing

        Dim FolderFiles As List(Of FileInfo) = (From file In DrwFolder.GetFiles()
                                                Where file.Name.EndsWith(".jpg") Or file.Name.EndsWith(".tif")
                                                Select file).ToList()
        Dim index As Integer = 0

        For Each subfile In FolderFiles
            Using Img As System.Drawing.Image = Bitmap.FromFile(subfile.FullName)
                barcode = getImageBarcode(Img)
                Img.Dispose()
                If Not IsNothing(barcode) Then
                    If New Regex("^([S][D]|[S][P])(\d{6})$").Match(barcode).Success Then
                        SeparateImages += 1
                        lastbarcodeFolder = Path.Combine(DrwFolder.FullName, barcode)
                        My.Computer.FileSystem.CreateDirectory(lastbarcodeFolder)
                    Else
                        subfile.MoveTo(Path.Combine(lastbarcodeFolder, subfile.Name))
                    End If
                End If
                If Not IsNothing(lastbarcodeFolder) Then
                    subfile.MoveTo(Path.Combine(lastbarcodeFolder, subfile.Name))
                Else
                    SeparateImages += 1
                    subfile.MoveTo(subfile.FullName.Replace(subfile.Name, String.Format("NOBARCODE_{0}.jpg", index)))
                    index += 1
                End If
            End Using
        Next

        DrwFolder.Refresh()
        MergeDrawings(DrwFolder)
    End Function

    Private Sub MergeDrawings(dir As DirectoryInfo)
        Dim document As iTextSharp.text.Document = Nothing
        Dim filename As String = Nothing
        Dim fs As FileStream = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        ' Will have a input folder, with subfolders as files
        ' each file will have many images and need to be merged into a single multipage PDF
        Dim img As iTextSharp.text.Image = Nothing
        For Each subFolder In dir.GetDirectories()
            If (subFolder.GetFiles().Length > 0) And (Not subFolder.Name.StartsWith("EDW")) Then
                filename = subFolder.FullName.Replace("SD", "SP") + ".pdf"
                document = New iTextSharp.text.Document()
                fs = New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None)
                writer = PdfWriter.GetInstance(document, fs)
                document.Open()
                For Each File In subFolder.GetFiles()
                    img = iTextSharp.text.Image.GetInstance(File.FullName)
                    img.SetAbsolutePosition(0, 0)
                    img.ScaleToFit(New iTextSharp.text.Rectangle(0, 0, ((img.Width / 200) * 72), ((img.Height / 200) * 72), 0))
                    document.SetPageSize(New iTextSharp.text.Rectangle(0, 0, ((img.Width / 200) * 72), ((img.Height / 200) * 72), 0))
                    document.NewPage()
                    writer.DirectContent.AddImage(img)
                    img = Nothing
                Next
                document.Close()
                writer.Close()
                fs = Nothing
                writer = Nothing
                document = Nothing
            End If
            subFolder.Delete(True)
        Next
    End Sub
End Class
