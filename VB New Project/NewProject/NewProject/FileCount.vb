Public Class FileCount
    Public Property Barcode As String
    Public Property Pagecount As Integer
    Public Property DrawingCount As Integer

    Public Sub New(Barcode As String)
        Me.Barcode = Barcode
        Me.Pagecount = 0
        Me.DrawingCount = 0
    End Sub
End Class
