Imports System.IO
Imports System.Collections

Public Interface IStep

    ReadOnly Property Type As StepType

    Sub RunFile(File As FileInfo)
    Sub RunFolder(Folder As DirectoryInfo, RunSubFolders As Boolean, SearchPattern As String)
    Sub RunFiles(Files As List(Of FileInfo))

End Interface
