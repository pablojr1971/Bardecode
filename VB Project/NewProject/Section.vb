Imports System.Text.RegularExpressions

Public Class Section
    Property Id As Integer
    Property Process As Integer
    Property Name As String
    Property Regex As Regex
    Property outName As String
    Property FolderName As String
    Property required As Boolean
    Property oncePerFile As Boolean
End Class
