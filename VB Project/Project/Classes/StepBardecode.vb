Imports System.IO

Public Class StepBardecode
    Implements IStep

    Private BardecodeProperties As PropertiesBardecode
    Public ReadOnly Property Properties As IProperties Implements IStep.Properties
        Get
            Return Me.BardecodeProperties
        End Get
    End Property
    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.Bardecode
        End Get
    End Property

    Sub New()
        Me.BardecodeProperties = New PropertiesBardecode()
    End Sub

    Public Sub RunFile(File As FileInfo) Implements IStep.RunFile

    End Sub

    Public Sub RunFiles(Files As List(Of FileInfo)) Implements IStep.RunFiles

    End Sub

    Public Sub RunFolder(Folder As DirectoryInfo, SearchPattern As String) Implements IStep.RunFolder

    End Sub
End Class
