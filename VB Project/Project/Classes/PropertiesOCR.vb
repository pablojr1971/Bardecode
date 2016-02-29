Imports System.IO
Imports System.Collections
Imports System.Drawing.Imaging

Public Structure PropertiesOCR
    Property OutputDirectory As DirectoryInfo
    Property SaveHtmlFiles As Boolean
    Property SaveImageFiles As Boolean
    Property OutputNameTemplate As String
    Property ImageDPI As Integer
    Property SaveImageFormat As ImageFormat
    Property TesseractData As String
    Property TesseractLanguage As String

    Public Sub SetDefaultValues()
        If False Then

        Else
            Me.SaveHtmlFiles = False
            Me.SaveImageFiles = False
            Me.SaveImageFormat = ImageFormat.Tiff
            Me.ImageDPI = 200
            Me.OutputNameTemplate = "_OCR.pdf"
            Me.OutputDirectory = New DirectoryInfo(Directory.GetCurrentDirectory())
            Me.TesseractData = Directory.GetCurrentDirectory() + "\tessdata"
            Me.TesseractLanguage = "eng"
        End If
    End Sub
End Structure

