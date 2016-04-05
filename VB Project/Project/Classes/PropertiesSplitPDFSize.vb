Imports System.IO

Public Structure PropertiesSplitPDFSize
    Public Property InputFolder As String
    Public Property FilePattern As String
    Public Property Size As Long
    Public Property ProcessSubFolders As Boolean

    Public Sub SetDefaultValues()
        FilePattern = ""
        Size = 10
        ProcessSubFolders = True
    End Sub

End Structure
