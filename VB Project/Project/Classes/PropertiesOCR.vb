Imports System.IO
Imports System.Collections
Imports System.Drawing.Imaging

Public Structure PropertiesOCR
    Public InputFolder As String
    Public OutputFolder As String
    Public CreateOutputSubFolders As Boolean
    Public ProcessSubFolders As Boolean
    Public OutputNameTemplate As String

    Public Sub SetDefaultValues()
        Me.InputFolder = Directory.GetCurrentDirectory()
        Me.ProcessSubFolders = True
        Me.CreateOutputSubFolders = True
        Me.OutputNameTemplate = "_OCR.pdf"
        Me.OutputFolder = Directory.GetCurrentDirectory()
    End Sub
End Structure

