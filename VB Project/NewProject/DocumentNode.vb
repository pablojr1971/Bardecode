Imports System.IO
Public Class DocumentNode
    Property Index As Integer
    Property ImageFile As FileInfo
    Property Barcode As String
    Property Type As String

    Public Sub New(Index As Integer, ImageFile As FileInfo, Barcode As String, Type As String)
        Me.Index = Index
        Me.ImageFile = ImageFile
        Me.Barcode = Barcode
        Me.Type = Type
    End Sub
End Class
