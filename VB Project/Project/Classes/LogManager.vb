Imports System.IO

Public Class LogManager
    Private LogFile As FileInfo = Nothing
    Private objWriter As StreamWriter = Nothing

    Public Sub CreateLogFile(Path As String)
        CloseLogFile()
        objWriter = New System.IO.StreamWriter(Path)
    End Sub

    Public Sub CloseLogFile()
        If Not IsNothing(objWriter) Then
            objWriter.Close()
            objWriter.Dispose()
            objWriter = Nothing
        End If
    End Sub

    Public Sub WriteLine(Log As String)
        objWriter.WriteLine(Log)
        objWriter.Flush()
    End Sub
End Class
