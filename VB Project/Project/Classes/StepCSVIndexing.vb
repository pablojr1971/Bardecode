Public Class StepCSVIndexing
    Implements IStep

    Public CSVIndexingProperties As PropertiesCSVIndexing
    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.CSVIndexingProperties
        End Get
    End Property

    Public Sub RunFolder(Folder As IO.DirectoryInfo, RunSubFolders As Boolean, SearchPattern As String)

    End Sub

    Public Sub Run(LogSub As IStep.LogSubDelegate) Implements IStep.Run

    End Sub
End Class
