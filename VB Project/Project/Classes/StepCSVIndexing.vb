Public Class StepCSVIndexing
    Implements IStep

    Public CSVIndexingProperties As PropertiesCSVIndexing
    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.CSVIndexingProperties
        End Get
    End Property

    Public Sub RunFile(File As IO.FileInfo) Implements IStep.RunFile

    End Sub

    Public Sub RunFiles(Files As List(Of IO.FileInfo)) Implements IStep.RunFiles

    End Sub

    Public Sub RunFolder(Folder As IO.DirectoryInfo, RunSubFolders As Boolean, SearchPattern As String) Implements IStep.RunFolder

    End Sub
End Class
