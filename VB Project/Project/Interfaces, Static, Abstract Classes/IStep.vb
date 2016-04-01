Imports System.IO
Imports System.Collections

Public Interface IStep

    Delegate Sub LogSubDelegate(Log As String)
    ReadOnly Property Type As StepType
    Sub Run(LogSub As LogSubDelegate)
    Property inputfolder As String
    Property outputfolder As String

End Interface
