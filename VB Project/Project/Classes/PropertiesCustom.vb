Imports System.IO
Imports System.Collections
Imports System.Drawing.Imaging

Public Structure PropertiesCustom
    Public OutputDirectory As String
    Public InputDirectory As String
    Public CustomRunID As String

    Public Sub SetDefaultvalues()
        Me.OutputDirectory = Directory.GetCurrentDirectory()
        Me.InputDirectory = Directory.GetCurrentDirectory()
        Me.CustomRunID = ""
    End Sub

End Structure
