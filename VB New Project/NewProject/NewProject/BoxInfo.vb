Public Class BoxInfo
    Public Property OriginalPagecount As Integer
    Public Property FinalPagecount As Integer
    Public Property DrawingPages As Integer
    Public Property Placeholders As Integer
    Public Property Drawings As Integer
    Public Property RemovedPages As List(Of String)
    Public Property Files As List(Of FileCount)

    Public Sub New()
        RemovedPages = New List(Of String)
        Files = New List(Of FileCount)
    End Sub

    Public Sub Clear()
        OriginalPagecount = 0
        FinalPagecount = 0
        DrawingPages = 0
        Placeholders = 0
        Drawings = 0
        RemovedPages.Clear()
        Files.Clear()
    End Sub
End Class
