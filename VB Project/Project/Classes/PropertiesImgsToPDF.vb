Imports System.IO

Public Structure PropertiesImgsToPDF
    Public Enum MergeOutputType
        FilePerFile = 1
        FilePerFolder = 2
    End Enum

    Property MergeOutput As MergeOutputType
    Property InputFormats As List(Of String)
    Property OutputName As String
End Structure
