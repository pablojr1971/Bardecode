Public Class Barcode
    Property Index As Integer = 0
    Property SectionName As String = Nothing
    Property BarcodeValue As String = Nothing

    Public Sub New(Index As Integer, SectionName As String, BarcodeValue As String)
        Me.Index = Index
        Me.SectionName = SectionName
        Me.BarcodeValue = BarcodeValue
    End Sub
End Class
