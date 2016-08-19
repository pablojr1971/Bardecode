Imports System.IO
Imports System.Collections
Imports ImageMagick
Imports System.Drawing.Imaging
Imports ZXing
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions



Public Class BarcodeProcessor
    Dim reader As BarcodeReader
    Dim A4Dir As DirectoryInfo
    Dim LFDir As DirectoryInfo

    Public Sub New()
        reader = New BarcodeReader()

        A4Dir = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing\Documents")
        LFDir = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing\Drawings")
    End Sub

    Public Function getImagesAndBarcodes(dirToSearch As DirectoryInfo, FStart As Regex, Sections As List(Of Section)) As List(Of DocumentNode)
        Dim result As ZXing.Result
        Dim Index = 0
        Dim TempImgPath As String = Nothing
        Dim PlaceholderRegex = New Regex("([S][P]|[S][D])(\d{6})")
        Dim FEnd = New Regex("EDFE")
        getImagesAndBarcodes = New List(Of DocumentNode)

        For Each page In dirToSearch.GetFiles("*.tif")
            TempImgPath = dirToSearch.FullName + String.Format("\Page{0}.Tif", Index)

            Using TempImg As MagickImage = New MagickImage(page.FullName)
                SaveImageAccordingColors(TempImg, TempImgPath)

                result = reader.Decode(TempImg.ToBitmap())
                If Not IsNothing(result) Then
                    If FStart.Match(result.Text).Success Then
                        getImagesAndBarcodes.Add(New DocumentNode(Index, _
                                                                  New FileInfo(TempImgPath), _
                                                                  result.Text, _
                                                                  "FStart"))
                    End If

                    If FEnd.Match(result.Text).Success Then
                        getImagesAndBarcodes.Add(New DocumentNode(Index, _
                                                                  New FileInfo(TempImgPath), _
                                                                  result.Text, _
                                                                  "FEnd"))                                                                        ))
                    End If


                    If PlaceholderRegex.Match(result.Text).Success Then
                        getImagesAndBarcodes.Add(New DocumentNode(Index, _
                                                                  New FileInfo(TempImgPath), _
                                                                  result.Text, _
                                                                  "Placeholder"))
                    End If

                    For Each Section In Sections
                        If Section.Regex.Match(result.Text).Success Then
                            getImagesAndBarcodes.Add(New DocumentNode(Index, _
                                                                      New FileInfo(TempImgPath), _
                                                                      result.Text, _
                                                                      Section.Name))
                            Exit For
                        End If
                    Next
                End If
            End Using
            page.Delete()
            page.Refresh()
            Index += 1
        Next
    End Function

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

    Public Function SeparateLFImages() As Integer
        SeparateLFImages = 0
        Dim result As ZXing.Result = Nothing
        Dim PlaceholderRegex As Regex = New Regex("([S][P]|[S][D])(/d{6})")
        Dim LastFileFolder As String = Nothing
        For Each drw In LFDir.GetFiles("*.jpg")
            Using Bitmap As Bitmap = New Bitmap(drw.FullName)
                result = reader.Decode(Bitmap)
                If Not IsNothing(result) Then
                    If New Regex("^([S][D]|[S][P])(\d{6})$").Match(result.Text).Success Then
                        LastFileFolder = Path.Combine(LFDir.FullName, result.Text)
                        My.Computer.FileSystem.CreateDirectory(LastFileFolder)
                        SeparateLFImages += 1
                    Else
                        drw.MoveTo(Path.Combine(LastFileFolder, drw.Name))
                    End If
                End If

                If Not IsNothing(LastFileFolder) Then
                    drw.MoveTo(Path.Combine(LastFileFolder, drw.Name))
                Else
                    SeparateLFImages += 1
                    If Not Directory.Exists(Path.Combine(LFDir.FullName, "NOBARCODE")) Then
                        My.Computer.FileSystem.CreateDirectory(Path.Combine(LFDir.FullName, "NOBARCODE"))
                    End If
                    drw.MoveTo(Path.Combine(Path.Combine(LFDir.FullName, "NOBARCODE"), drw.Name))
                End If
            End Using
        Next
    End Function

    Public Function ConsistBarcodes() As String
        ' This function Should return yes or no and if we got any problem with the processing
        ' if a problem is found

        ' Check if Start with a File Start
        ' If theres no file Start without File End and/or File end without file Start
        ' Check the required Sections
        ' Check the Number of files in the box and compare with Scandata
        ' if there's any difference, or things are not working
        ' then I should return false and throw a message saying that there's a problem

        Return Nothing
    End Function

End Class
