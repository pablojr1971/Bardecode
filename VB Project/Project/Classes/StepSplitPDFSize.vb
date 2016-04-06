Imports System.IO
Imports System.Text.RegularExpressions

Public Class StepSplitPDFSize
    Implements IStep

    Public SplitPDFSizeProperties As PropertiesSplitPDFSize

    Public Sub Run(LogSub As IStep.LogSubDelegate) Implements IStep.Run
        LogSub("Start Splitting PDFs")
        Me.RecursiveRun(New DirectoryInfo(SplitPDFSizeProperties.InputFolder), SplitPDFSizeProperties.ProcessSubFolders, LogSub)
        LogSub("Splitting Done" + vbCrLf)
    End Sub

    Private Sub RecursiveRun(Folders As DirectoryInfo, RunSubFolders As Boolean, LogSub As IStep.LogSubDelegate)
        If RunSubFolders Then
            For Each subfolder In Folders.GetDirectories()
                RecursiveRun(subfolder, True, LogSub)
            Next
        End If

        For Each File In Folders.GetFiles("*.pdf")
            If SplitPDFSizeProperties.FilePattern.Length > 0 Then
                If Not Regex.Match(File.Name, SplitPDFSizeProperties.FilePattern).Success Then
                    Continue For
                End If
            End If
            LogSub("Processing file " + File.Name)

            Utils.SplitFileSize(File.FullName, SplitPDFSizeProperties.Size)
        Next
    End Sub

    Public Sub New()
        SplitPDFSizeProperties = New PropertiesSplitPDFSize()
        SplitPDFSizeProperties.SetDefaultValues()
    End Sub

    Public Sub New(Properties As PropertiesSplitPDFSize)
        Me.SplitPDFSizeProperties = Properties
        Me.SplitPDFSizeProperties.InputFolder = Directory.GetCurrentDirectory() + "\Processing\Documents"
    End Sub

    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.SplitPDFSize
        End Get
    End Property

    Public Shared Function LoadStep(StepId As Integer, ctx As VBProjectContext) As StepSplitPDFSize
        With ctx.ESteps.Single(Function(p) p.Id = StepId)
            LoadStep = New StepSplitPDFSize(Serializer.FromXml(.PropertiesObj, GetType(PropertiesSplitPDFSize)))
        End With
    End Function
End Class
