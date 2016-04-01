Imports System.IO
Imports System.Reflection
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports iTextSharp.text.pdf
Imports iTextSharp.text
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

    Private Sub ConvertJPGTOPDF(LogSub As IStep.LogSubDelegate)
        Dim document As iTextSharp.text.Document = Nothing
        Dim filename As String = Nothing
        Dim fs As FileStream = Nothing
        Dim writer As iTextSharp.text.pdf.PdfWriter = Nothing
        ' Will have a input folder, with subfolders as files
        ' each file will have many images and need to be merged into a single multipage PDF
        Dim img As iTextSharp.text.Image = Nothing
        For Each subFolder In New DirectoryInfo(CustomPropeties.Input1).GetDirectories()
            filename = subFolder.FullName.Replace("SD", "SP") + ".pdf"
            document = New Document()
            fs = New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None)
            writer = PdfWriter.GetInstance(document, fs)
            document.Open()
            For Each File In subFolder.GetFiles("*.jpg")
                img = Image.GetInstance(File.FullName)
                img.SetAbsolutePosition(0, 0)
                document.SetPageSize(New iTextSharp.text.Rectangle(0, 0, img.Width, img.Height, 0))
                document.NewPage()
                writer.DirectContent.AddImage(img)
                img = Nothing
            Next
            document.Close()
            fs = Nothing
            writer = Nothing
            document = Nothing
            subFolder.Delete(True)
        Next
    End Sub

    Private Sub MergeA4AndDrawingsSplit1(LogSub As IStep.LogSubDelegate)
        ' Documents by file will be the input1
        ' Drawings by file will be the input2

        ' I will have to go throg the subfolders of the input1 and then find the files to merge with the drawings
        ' The second folder should have pdfs of each subfolder containing the image files merged into a single file
        Dim dir1 As DirectoryInfo = New DirectoryInfo(CustomPropeties.Input1)
        Dim dir2 As DirectoryInfo = New DirectoryInfo(CustomPropeties.Input2)

        RecursiveMerge(dir1, dir2, LogSub)
    End Sub

    Private Shared Sub RecursiveMerge(directory As DirectoryInfo, DrawingDir As DirectoryInfo, logsub As IStep.LogSubDelegate)
        ' Need to go through each folder and merge with the drawings
        For Each subfolder In directory.GetDirectories()
            RecursiveMerge(subfolder, DrawingDir, logsub)
        Next

        MergeFolder(directory, DrawingDir, logsub)
    End Sub



    Private Shared Sub MergeFolder(directory As DirectoryInfo, DrawingDir As DirectoryInfo, logsub As IStep.LogSubDelegate)
        Dim document As PdfDocument = Nothing
        Dim output As FileStream = Nothing
        Dim writer As PdfWriter = Nothing

        Dim Directoryfiles As List(Of FileInfo) = directory.GetFiles().ToList()
        Dim FinalFiles As List(Of FileInfo) = Directoryfiles.Where(Function(p) p.FullName.EndsWith("_NOBARCODE.pdf")).ToList()

        Dim outputFile As String = ""
        Directoryfiles.RemoveAll(Function(p) p.FullName.EndsWith("_NOBARCODE.pdf"))

        Dim FilesToMerge As List(Of String) = New List(Of String)

        For Each finalFile In FinalFiles
            FilesToMerge.Clear()
            FilesToMerge.Add(finalFile.FullName)
            For Each subfile In Directoryfiles.Where(Function(p) p.FullName.StartsWith(finalFile.FullName.Replace("_NOBARCODE.pdf", "_")) And p.FullName <> finalFile.FullName)
                FilesToMerge.Add(DrawingDir.FullName + "\" + subfile.FullName.Substring(subfile.FullName.Length - 12, 12))
                FilesToMerge.Add(subfile.FullName)
            Next
            outputFile = finalFile.FullName.Replace("_NOBARCODE.pdf", "_Final.pdf")

            Dim logs As String() = Utils.MergePdfs(FilesToMerge, outputFile)
            For Each line In logs
                logsub(line)
            Next

        Next

        ' Delete the files
        For Each File In Directoryfiles
            My.Computer.FileSystem.DeleteFile(File.FullName)
        Next
        For Each File In FinalFiles
            My.Computer.FileSystem.DeleteFile(File.FullName)
        Next
        If File.Exists(directory.Parent.FullName + "\_001_NOBARCODE.pdf") Then
            My.Computer.FileSystem.DeleteFile(directory.Parent.FullName + "\_001_NOBARCODE.pdf")
        End If
    End Sub

    Private Sub CountPages(LogSub As IStep.LogSubDelegate)
        RecursiveCountPages(LogSub, New DirectoryInfo(CustomPropeties.Input1))
    End Sub

    Private Shared Sub RecursiveCountPages(Logsub As IStep.LogSubDelegate, Dir As DirectoryInfo)
        For Each subdir In Dir.GetDirectories()
            RecursiveCountPages(Logsub, subdir)
        Next

        Dim A4 As Integer = 0
        Dim A3 As Integer = 0
        Dim A2 As Integer = 0
        Dim A1 As Integer = 0
        Dim A0 As Integer = 0

        Dim reader As PdfReader = Nothing
        Dim pageSize As iTextSharp.text.Rectangle = Nothing
        Dim StrOut As String = Nothing
        Dim pagecount As Integer = 0
        Dim drawingcount As Integer = 0

        For Each File In Dir.GetFiles("*.pdf")
            reader = New PdfReader(File.FullName)

            For index = 1 To reader.NumberOfPages
                pageSize = reader.GetPageSize(index)

                Select Case Utils.PageSize(pageSize.Height, pageSize.Width, True)
                    Case Project.PageSize.A0 : A0 += 1
                    Case Project.PageSize.A1 : A1 += 1
                    Case Project.PageSize.A2 : A2 += 1
                    Case Project.PageSize.A3 : A3 += 1
                    Case Project.PageSize.A4 : A4 += 1
                    Case Else : A4 += 1
                End Select
            Next
        Next

        StrOut = String.Format("{0}; {1}; {2}; {3}; {4}", A0, A1, A2, A3, A4)
        pagecount = A3 + A4
        drawingcount = A0 + A1 + A2

        Dim connection As SqlConnection = New SqlConnection(FrmMain.ScanDataStringConnection)
        connection.Open()

        Dim command As SqlCommand = New SqlCommand("   UPDATE SCANDATA " + _
                                                   "      SET FIELD10 = '" + StrOut + "'," + _
                                                   "          PAGECOUNT = " + pagecount.ToString() + "," + _
                                                   "          DRAWINGCOUNT = " + drawingcount.ToString() + _
                                                   "   WHERE JOBNO = 2001 " + _
                                                   "     AND BARCODE = '" + Dir.Name + "'", connection)
        Logsub("Updated ScanData - Rows = " + command.ExecuteNonQuery().ToString())

        connection.Close()
        command.Dispose()
        connection.Dispose()
    End Sub

    Public Property inputfolder As String Implements IStep.inputfolder
        Get
            Return CustomPropeties.Input1
        End Get
        Set(value As String)
            CustomPropeties.Input1 = value
        End Set
    End Property

    Public Property outputfolder As String Implements IStep.outputfolder
        Get
            Return CustomPropeties.Output
        End Get
        Set(value As String)
            CustomPropeties.Output = value
        End Set
    End Property
End Class
