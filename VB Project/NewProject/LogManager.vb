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
        If Not IsNothing(objWriter) Then
            objWriter.WriteLine(Log)
            objWriter.Flush()
        End If
        FrmMain.txProcessLog.AppendText(Log)
    End Sub
End Class
