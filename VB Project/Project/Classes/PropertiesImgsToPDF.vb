Imports System.IO

Public Structure PropertiesImgsToPDF
    Public Enum MergeOutputType
        FilePerFile = 1
        FilePerFolder = 2
    End Enum

    Dim MergeOutput As MergeOutputType
    Dim InputFormats As List(Of String)
    Dim Outputfolder As DirectoryInfo
    Dim OutputName As String 
End Structure
