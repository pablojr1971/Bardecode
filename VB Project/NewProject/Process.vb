Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports ImageMagick
Imports ZXing

Public Class Process
    Property Id As Integer
    Property JobNumber As String
    Property Name As String
    Property InpFolder As String
    Property DrwFolder As String
    Property OutFolder As String
    Property BWA4 As Boolean
    Property BWDrw As Boolean
    Property OCRA4 As Boolean
    Property OCRDrw As Boolean
    Property StopToFix As Boolean

    Property FStartRegex As String
    Property OutName As String
    Property FolderName As String
    Property FileSize As Integer
    Property Sections As List(Of Section)

    Public Sub New(ProcessId As Integer)
        Dim Database = New SqlConnection(FrmMain.VBProjectStringConnection)

        Database.Open()

        Using Command As SqlCommand = New SqlCommand("SELECT * FROM PROCESS WHERE ID = " + ProcessId)
            With Command.ExecuteReader()
                If .Read() Then
                    Me.Id = .GetInt32("Id")
                    Me.JobNumber = .GetString("JobNo")
                    Me.Name = .GetString("Name")
                    Me.InpFolder = .GetString("docInput")
                    Me.DrwFolder = .GetString("drwInput")
                    Me.OutFolder = .GetString("outFolder")
                    Me.OCRA4 = .GetBoolean("docOCR")
                    Me.OCRDrw = .GetBoolean("drwOCR")
                    Me.BWA4 = .GetBoolean("docBW")
                    Me.BWDrw = .GetBoolean("drwBW")
                    Me.StopToFix = .GetBoolean("drwFix")
                    Me.FStartRegex = .GetString("regexFileStart")
                    Me.OutName = .GetString("outName")
                    Me.FileSize = .GetInt32("FileSize")
                End If
                .Close()
            End With
        End Using

        Me.Sections = New List(Of Section)
        Using Command As SqlCommand = New SqlCommand("SELECT * FROM SECTION WHERE PROCESS = " + ProcessId)
            With Command.ExecuteReader()
                While .Read()
                    Me.Sections.Add(New Section())
                    Me.Sections.Last().Id = .GetInt32("Id")
                    Me.Sections.Last().Process = .GetInt32("Process")
                    Me.Sections.Last().Name = .GetString("Name")
                    Me.Sections.Last().Regex = New Regex(.GetString("Regex"))
                    Me.Sections.Last().outName = .GetString("outName")
                    Me.Sections.Last().FolderName = .GetString("FolderName")
                    Me.Sections.Last().required = .GetBoolean("Required")
                    Me.Sections.Last().oncePerFile = .GetBoolean("oncePerFile")
                End While
                .Close()
            End With
        End Using

        Database.Close()
    End Sub

    Public Sub Execute()
        Dim BarcodeObj As BarcodeProcessor = New BarcodeProcessor()
        Dim BarcodeList As List(Of DocumentNode) = New List(Of DocumentNode)
        Dim Images As List(Of FileInfo) = New DirectoryInfo(Me.InpFolder).GetFiles("*.tif").ToList()
        Dim RemovedPages As List(Of FileInfo) = New List(Of FileInfo)
        Dim LastFile As String = Nothing

        Dim document As Document = Nothing
        Dim writer As PdfWriter = Nothing

        Dim tempImage As Bitmap = Nothing

        BarcodeList = BarcodeObj.getImagesAndBarcodes(New DirectoryInfo(Me.InpFolder), New Regex(Me.FStartRegex), Me.Sections)


        For Index = 0 To (Images.Count - 1)
            tempImage = New Bitmap(Images.ElementAt(Index).FullName)

            If IsNothing(LastFile) Then
                RemovedPages.Add(Images.ElementAt(Index))
            End If

            Dim tempIndex = Index
            If BarcodeList.Exists(Function(p) p.Index = tempIndex) Then
                With BarcodeList.Single(Function(p) p.Index = tempIndex)
                    If .Type = "FStart" Then
                        document = New Document()
                        writer = PdfWriter.GetInstance(document, New FileStream(Path.Combine("", ""), FileMode.Create))
                        document.SetPageSize(New Rectangle(0, 0, ((New Bitmap("").Width / 200) * 72), ((New Bitmap("").Height / 200) * 72), 0))
                        document.NewPage()

                        writer.DirectContent.AddImage(iTextSharp.text.Image.GetInstance(""))

                    End If

                    If .Type = "FEnd" Then
                        ' File End 
                    End If

                    If .Type = "Placeholder" Then
                        ' Placeholder 
                        ' Insert the Images from the LF folder.

                    End If

                    If Sections.Exists(Function(p) p.Name = .Type) Then
                        Dim tempSection As Section = Sections.Single(Function(p) p.Name = .Type)
                        ' Sections 
                    End If

                End With
            End If
        Next
    End Sub

    Public Sub Run()
        Dim BarcodeList As List(Of DocumentNode) = New List(Of DocumentNode)
        Dim InpFolder As DirectoryInfo = New DirectoryInfo(Me.InpFolder)
        Dim DrwFolder As DirectoryInfo = New DirectoryInfo(Me.DrwFolder)
        Dim ImgList As List(Of FileInfo) = Nothing
        Dim Reader As BarcodeReader = New BarcodeReader()
        Dim result As Result = Nothing
        Dim barcode As String = Nothing


        Dim RegxStart As Regex = New Regex(Me.FStartRegex)
        Dim RegxEnd As Regex = New Regex("^()$")
        Dim RegxPlaceholder As Regex = New Regex("^([S][D]|[S][P])(/d{6})$")
        Dim RegxSectionList As List(Of Regex) = New List(Of Regex)

        Dim index As Integer = 0
        ImgList = InpFolder.GetFiles("*.tif").ToList()

        Dim CurrentFile As String = Nothing
        Dim CurrentFileFolder As String = Nothing
        Dim CurrentSection As String = Nothing
        Dim CurrentSectionFolder As String = Nothing

        Dim doc As iTextSharp.text.Document = Nothing
        Dim writer As PdfWriter = Nothing
        Dim drawpdfreader As PdfReader = Nothing
        Dim currentImage As iTextSharp.text.Image = Nothing

        For index = 0 To ImgList.Count - 1
            Using currentImg As MagickImage = New MagickImage(ImgList.ElementAt(index).FullName)
                result = Reader.Decode(currentImg.ToBitmap())

                If Not IsNothing(result) Then
                    barcode = result.Text

                    If RegxStart.Match(barcode).Success Then
                        BarcodeList.Add(New DocumentNode(index, _
                                                         ImgList.ElementAt(index), _
                                                         barcode, _
                                                         "FStart"))
                    End If

                    If RegxEnd.Match(barcode).Success Then
                        BarcodeList.Add(New DocumentNode(index, _
                                                         ImgList.ElementAt(index), _
                                                         barcode, _
                                                         "FEnd"))
                    End If

                    If RegxPlaceholder.Match(barcode).Success Then
                        BarcodeList.Add(New DocumentNode(index, _
                                                         ImgList.ElementAt(index), _
                                                         barcode, _
                                                         "Placeholder"))
                    End If

                    For Each section In Me.Sections
                        If section.Regex.Match(barcode).Success Then
                            BarcodeList.Add(New DocumentNode(index, _
                                                             ImgList.ElementAt(index), _
                                                             barcode, _
                                                             section.Name))
                            Exit For
                        End If
                    Next
                End If

                SaveImageAccordingColors(currentImg, Path.Combine(ImgList.ElementAt(index).Directory.FullName, "Page" + index.ToString()))
            End Using

            ImgList.ElementAt(index).Delete()
            ImgList.RemoveAt(index)
        Next

        If ImgList.Count > 0 Then
            ' Should warn that is not empty and some image was forgoten for some reason.
        End If

        ' At this point we should have only the ''Page'' Images

        ImgList = InpFolder.GetFiles("*.tif").ToList()

        For index = 0 To ImgList.Count - 1

            ' if it is a section change the current variables
            ' if it is a file end clear all variables
            ' if it is a placeholder just change it for the images

            ' if the variables are empty them add page to removed pages

            ' if the variables are filled them just add the image in the current document

            If BarcodeList.Exists(Function(p) p.Index = index) Then
                With BarcodeList.ElementAt(index)
                    If .Type = "FStart" Then
                        CurrentFile = .Barcode
                        doc = New Document()
                        writer = PdfWriter.GetInstance(doc, New FileStream(Path.Combine("", ""), FileMode.Create))

                        currentImage = iTextSharp.text.Image.GetInstance(ImgList(index).FullName)
                        writer.DirectContent.AddImage(currentImage)
                        currentImage.SetAbsolutePosition(0, 0)
                        currentImage.ScaleToFit(New Rectangle(0, 0, ((currentImage.Width / 200) * 72), ((currentImage.Height / 200) * 72), 0))
                        doc.SetPageSize(New Rectangle(0, 0, ((currentImage.Width / 200) * 72), ((currentImage.Height / 200) * 72), 0))
                        doc.NewPage()
                        writer.DirectContent.AddImage(currentImage)
                        currentImage = Nothing
                        My.Computer.FileSystem.DeleteFile(ImgList.ElementAt(index).FullName)

                    End If

                    If .Type = "FEnd" Then
                        doc.Close()
                        doc.Dispose()
                        writer.Close()
                        writer.Dispose()
                        CurrentFile = Nothing
                        CurrentSection = Nothing
                        ' nao sei o que fazer ainda (decidir)
                    End If

                    If .Type = "Placeholder" Then
                        ' nao sei o que fazer ainda (decidir)

                        If Directory.Exists(DrwFolder.FullName + "\" + .Barcode.Replace("SP", "SD")) Then

                        End If
                    End If

                    For Each section In Me.Sections
                        If .Type = section.Name Then
                            CurrentSection = section.Name
                            ' nao sei o que fazer ainda (decidir)
                        End If
                    Next

                End With
            End If

            currentImage = iTextSharp.text.Image.GetInstance(ImgList(index).FullName)
            writer.DirectContent.AddImage(currentImage)
            currentImage.SetAbsolutePosition(0, 0)
            currentImage.ScaleToFit(New Rectangle(0, 0, ((currentImage.Width / 200) * 72), ((currentImage.Height / 200) * 72), 0))
            doc.SetPageSize(New Rectangle(0, 0, ((currentImage.Width / 200) * 72), ((currentImage.Height / 200) * 72), 0))
            doc.NewPage()
            writer.DirectContent.AddImage(currentImage)
            currentImage = Nothing
            My.Computer.FileSystem.DeleteFile(ImgList.ElementAt(index).FullName)

            ' so this is what I should do with a normal image, without document nodes in the list
        Next
    End Sub

    Private Sub SaveImageAccordingColors(Img As MagickImage, NewImgPath As String)
        Dim encoderInfo As ImageCodecInfo = Nothing
        Dim encoderParams As EncoderParameters = New EncoderParameters(2)
        encoderParams.Param(1) = New EncoderParameter(Encoder.SaveFlag, CLng(EncoderValue.MultiFrame))

        If Img.TotalColors > 50 Then
            encoderInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/jpeg")
            encoderParams.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionLZW))
        Else
            encoderInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/tiff")
            encoderParams.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionCCITT4))
        End If

        Img.ToBitmap().Save(NewImgPath, encoderInfo, encoderParams)
    End Sub

    Private Function CheckListIntegrity(ImgList As List(Of DocumentNode)) As Boolean

    End Function

End Class
