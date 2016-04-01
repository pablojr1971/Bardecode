Imports System.IO

Public Structure PropertiesImgsToPDF
    Public Enum MergeOutputType
        FilePerFile = 1
        FilePerFolder = 2
    End Enum

    Property MergeOutput As MergeOutputType
    Property InputFormats As List(Of String)
    Property Outputfolder As String
    Property OutputName As String
    Property InputFolder As String
End Structure
