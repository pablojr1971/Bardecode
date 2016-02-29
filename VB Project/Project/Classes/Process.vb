' this class will contain a collection of steps
' and then a method run, who will iterate the steps and  run each of them

Public Class Process
    Property Id As Integer
    Property Name As String
    Property Description As String
    Property Steps As Dictionary(Of IStep, RunMethod)

    Enum RunMethod
        SingleFile = 1
        Folder = 2
        FileCollection = 3
    End Enum

    Public Sub Run()
        'this will be the basic engine of the processes
        For Each StepRun As KeyValuePair(Of IStep, RunMethod) In Steps
            If StepRun.Value = RunMethod.SingleFile Then
                StepRun.Key.RunFile(New System.IO.FileInfo(""))
            End If

            If StepRun.Value = RunMethod.FileCollection Then
                StepRun.Key.RunFiles(New List(Of System.IO.FileInfo))
            End If

            If StepRun.Value = RunMethod.Folder Then
                StepRun.Key.RunFolder(New System.IO.DirectoryInfo(""), True, "")
            End If
        Next
    End Sub
End Class
