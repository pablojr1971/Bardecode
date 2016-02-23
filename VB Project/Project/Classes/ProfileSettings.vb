Imports System.IO

' This class will be to put together all the profile settings
' The profile setting define how the process will work, based on this settings, the main process routine will do diferent actions
' at the first I will be storing this options in A ini.file but later we can create a place on the main database

Public Class ProfileSettings
    Public Enum A4FormatType
        PDF = 1
    End Enum

    Public Enum LFFormatType
        PDF = 1
        JPG = 2
    End Enum

    ' Paths
    Public Property A4InputFolder As DirectoryInfo
    Public Property A4OutputFolder As DirectoryInfo
    Public Property LFInputFolder As DirectoryInfo
    Public Property LFOutputFolder As DirectoryInfo

    ' File Types
    Public Property A4InputFormat As A4FormatType
    Public Property A4OutPutFormat As A4FormatType
    Public Property LFInputFormat As LFFormatType
    Public Property LFOutputFormat As LFFormatType

    'Bardecode Settings
    Public Property A4BardecodeIni As FileInfo
    Public Property LFBardecodeIni As FileInfo
    Public Property BardecodeExe As FileInfo

    'Ini file
    Public Property Ini As IniFile

    Public Sub New()
        ' this class may can be used just to pass settings through the code, so with jus New without arguments, no ini file is associated with this class
        ' but without the ini file load and save methods can't be invoked
    End Sub

    Public Sub New(Path As FileInfo)
        Me.Ini = New IniFile(Path.FullName)
        Me.LoadSettings(Path)
    End Sub

    Public Sub LoadSettings(Path As FileInfo)
        Me._A4InputFolder = New DirectoryInfo(Ini.ReadValue("Profile", "A4InputFolder"))
        Me._A4OutputFolder = New DirectoryInfo(Ini.ReadValue("Profile", "A4OutputFolder"))
        Me._LFInputFolder = New DirectoryInfo(Ini.ReadValue("Profile", "LFInputFolder"))
        Me._LFOutputFolder = New DirectoryInfo(Ini.ReadValue("Profile", "LFOutputFolder"))

        Me._A4InputFormat = CInt(Ini.ReadValue("Profile", "A4InputFormat"))
        Me._A4OutPutFormat = CInt(Ini.ReadValue("Profile", "A4OutPutFormat"))
        Me._LFInputFormat = CInt(Ini.ReadValue("Profile", "LFInputFormat"))
        Me._LFOutputFormat = CInt(Ini.ReadValue("Profile", "LFOutputFormat"))

        Me._A4BardecodeIni = New FileInfo(Ini.ReadValue("Profile", "A4BardecodeIni"))
        Me._LFBardecodeIni = New FileInfo(Ini.ReadValue("Profile", "LFBardecodeIni"))
        Me._BardecodeExe = New FileInfo(Ini.ReadValue("Profile", "BardecodeExe"))
    End Sub

    Public Sub SaveSettings()
        Ini.WriteValue("Profile", "A4InputFolder", Me._A4InputFolder.FullName)
        Ini.WriteValue("Profile", "A4OutputFolder", Me._A4OutputFolder.FullName)
        Ini.WriteValue("Profile", "LFInputFolder", Me._LFInputFolder.FullName)
        Ini.WriteValue("Profile", "LFOutputFolder", Me._LFOutputFolder.FullName)

        Ini.WriteValue("Profile", "A4InputFormat", CStr(CInt(Me._A4InputFormat)))
        Ini.WriteValue("Profile", "A4OutputFormat", CStr(CInt(Me._A4OutPutFormat)))
        Ini.WriteValue("Profile", "LFInputFormat", CStr(CInt(Me._LFInputFormat)))
        Ini.WriteValue("Profile", "LFOutputFormat", CStr(CInt(Me._LFOutputFormat)))

        Ini.WriteValue("Profile", "A4BardecodeIni", Me._A4BardecodeIni.FullName)
        Ini.WriteValue("Profile", "LFBardecodeIni", Me._LFBardecodeIni.FullName)
        Ini.WriteValue("Profile", "BardecodeExe", Me._BardecodeExe.FullName)
    End Sub
End Class
