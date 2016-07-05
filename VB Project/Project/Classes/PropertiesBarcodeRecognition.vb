Imports System.IO
Imports System.Collections
Imports System.Drawing.Imaging

Public Structure PropertiesBarcodeRecognition
    Public InputFolder As String
    Public OutputFolder As String
    Public CustomRunID As String

    Public Sub SetDefaultvalues()
        Me.CustomRunID = ""
    End Sub

End Structure