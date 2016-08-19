Imports System.Text.RegularExpressions

Public Class SectionSetup
    Public Property Name As String = Nothing
    Public Property Regex As Regex = Nothing
    Public Property OutputName As String = Nothing
    Public Property FolderName As String = Nothing
    Public Property OncePerFile As Boolean = False
    Public Property Required As Boolean = False
End Class
