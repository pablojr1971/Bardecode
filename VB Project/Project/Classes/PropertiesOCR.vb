Imports System.IO
Imports System.Collections
Imports System.Drawing.Imaging

Public Class PropertiesOCR
    Implements IProperties

    Property OutputDirectory As DirectoryInfo
    Property SaveHtmlFiles As Boolean
    Property SaveImageFiles As Boolean
    Property OutputNameTemplate As String
    Property ImageDPI As Integer
    Property SaveImageFormat As ImageFormat
    Property TesseractData As String
    Property TesseractLanguage As String

    Sub New()
        Me.ReadProperties()
    End Sub

    Public Sub ReadProperties() Implements IProperties.ReadProperties
        ' Implement read Properties if don't have a place where to read set the standard properties
        ' false because it will be a condition when implement readproperties
        If False Then

        Else
            Me.SaveHtmlFiles = False
            Me.SaveImageFiles = True
            Me.SaveImageFormat = ImageFormat.Tiff
            Me.ImageDPI = 200
            Me.OutputNameTemplate = "_OCR.pdf"
            Me.OutputDirectory = New DirectoryInfo(Application.StartupPath)
            Me.TesseractData = Application.StartupPath + "\tessdata"
            Me.TesseractLanguage = "eng"
        End If
    End Sub

    Public Sub SaveProperties() Implements IProperties.SaveProperties

    End Sub
End Class

