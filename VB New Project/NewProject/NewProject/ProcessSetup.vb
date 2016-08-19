Imports System.IO
Imports System.Text.RegularExpressions

Public Class ProcessSetup
    Public Property JobNo As Integer = 0
    Public Property Name As String = Nothing
    Public Property A4InputFolder As DirectoryInfo = Nothing
    Public Property LFInputFolder As DirectoryInfo = Nothing
    Public Property OutputFolder As DirectoryInfo = Nothing
    Public Property LogFolder As DirectoryInfo = Nothing
    Public Property BWA4 As Boolean = False
    Public Property BWLF As Boolean = False
    Public Property OCRA4 As Boolean = False
    Public Property OCRLF As Boolean = False
    Public Property FStartRegex As Regex = Nothing
    Public Property FOutputName As String = Nothing
    Public Property FFolder As String = Nothing
    Public Property FSplitSizeMB As Integer = 0
    Public Property Sections As List(Of SectionSetup) = New List(Of SectionSetup)
End Class
