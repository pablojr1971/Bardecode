Imports System.Reflection
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports Ghostscript.NET.Rasterizer

Public Class StepCustom
    Implements IStep

    Public CustomPropeties As PropertiesCustom

    Public Sub New()
        CustomPropeties = New PropertiesCustom()
    End Sub

    Public Sub New(Properties As PropertiesCustom)
        Me.CustomPropeties = Properties
    End Sub

    Public Sub Run(LogSub As IStep.LogSubDelegate) Implements IStep.Run
        LogSub("Running " + CustomPropeties.CustomRunID + vbCrLf)
        Me.GetType.InvokeMember(Me.CustomPropeties.CustomRunID,
                                BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Instance,
                                Nothing,
                                Me,
                                {LogSub})
    End Sub

    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.Custom
        End Get
    End Property

    Public Shared Function LoadStep(StepId As Integer, ctx As VBProjectContext) As StepCustom
        With ctx.ESteps.Single(Function(p) p.Id = StepId)
            LoadStep = New StepCustom(Serializer.FromXml(.PropertiesObj, GetType(PropertiesCustom)))
        End With
    End Function

    Private Sub TestePdf(LogSub As IStep.LogSubDelegate)
        LogSub("Loading the PDF")

        Dim rasterizer As GhostscriptRasterizer = New GhostscriptRasterizer()
        rasterizer.Open(CustomPropeties.Input1)

        Dim xydif As Integer = 0
        Dim xyratio As Double = 0

        For index = 1 To rasterizer.PageCount
            With rasterizer.GetPage(300, 300, index)
                xydif = .Height - .Width
                xydif = IIf(xydif < 0, (xydif * -1), xydif)
                xyratio = IIf(.Height > .Width, (.Height / .Width), (.Width / .Height))

                If (xyratio <= 1.42 And xyratio >= 1.32) Then
                    LogSub(String.Format("Page {0} is A4 - Heigh = {1} and Width = {2}", index, .Height, .Width))
                Else
                    LogSub(String.Format("Page {0} is Custom Size - Heigh = {1} and Width = {2}", index, .Height, .Width))
                End If
                .Save(String.Format(CustomPropeties.Output + "\Image{0}.Tiff", index), System.Drawing.Imaging.ImageFormat.Tiff)
                .Dispose()
            End With
        Next

        rasterizer.Dispose()
        rasterizer = Nothing
    End Sub

    Private Sub TesteImageSize(LogSub As IStep.LogSubDelegate)
        LogSub("Start")

        For Each file In New DirectoryInfo(CustomPropeties.Input1).GetFiles()
            With New Bitmap(file.FullName)
                LogSub(.Height.ToString + " x " + .Width.ToString + " = " + (.Height * 2).ToString + " x " + (.Width * 2).ToString + "  =  " + (.Height / .Width).ToString)
            End With
        Next
    End Sub

    Private Sub MergeA4andDrawings(LogSub As IStep.LogSubDelegate)
        Dim document As PdfDocument = Nothing
        Dim output As FileStream = Nothing
        Dim writer As PdfWriter = Nothing

        Dim Directoryfiles As List(Of String) = Directory.GetFiles(CustomPropeties.Input1).ToList()
        Dim FinalFiles As List(Of String) = Directoryfiles.Where(Function(p) p.EndsWith("_NOBARCODE.pdf")).ToList()

        Dim outputFile As String = ""
        Directoryfiles.RemoveAll(Function(p) p.EndsWith("_NOBARCODE.pdf"))

        Dim FilesToMerge As List(Of String) = New List(Of String)

        For Each finalFile In FinalFiles
            FilesToMerge.Clear()
            FilesToMerge.Add(finalFile)
            For Each subfile In Directoryfiles.Where(Function(p) p.StartsWith(finalFile.Replace("_NOBARCODE.pdf", "")))
                FilesToMerge.Add(CustomPropeties.Input2 + "\" + subfile.Substring(subfile.Length - 11, 11))
                FilesToMerge.Add(subfile)
            Next
            outputFile = CustomPropeties.Output + "\" + (New FileInfo(finalFile).Name.Replace("_NOBARCODE", ""))
            LogSub(outputFile)
            PDFMerge.MergePdfs(FilesToMerge.ToArray, outputFile)
        Next
    End Sub
End Class
