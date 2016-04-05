Imports System.IO

Public Structure PropertiesOCR
    Public InputFolder As String
    Public OutputFolder As String
    Public CreateOutputSubFolders As Boolean
    Public ProcessSubFolders As Boolean
    Public OutputNameTemplate As String
    Public DeleteInputFile As Boolean

    Public Sub SetDefaultValues()
        Me.OutputNameTemplate = "_OCR.pdf"
    End Sub
End Structure

