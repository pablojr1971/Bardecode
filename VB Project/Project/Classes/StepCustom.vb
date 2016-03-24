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

    Private Sub TestePdf(LogSub As IStep.LogSubDelegate)
        Dim A5 As Integer = 0
        Dim A4 As Integer = 0
        Dim A3 As Integer = 0
        Dim A2 As Integer = 0
        Dim A1 As Integer = 0
        Dim A0 As Integer = 0

        Dim rasterizer As GhostscriptRasterizer = New GhostscriptRasterizer()
        rasterizer.Open(CustomPropeties.Input1)

        For index = 1 To rasterizer.PageCount
            With rasterizer.GetPage(200, 200, index)
                Select Case Utils.GetPageSize(.Height, .Width)
                    Case PageSize.A0 : A0 += 1
                    Case PageSize.A1 : A1 += 1
                    Case PageSize.A2 : A2 += 1
                    Case PageSize.A3 : A3 += 1
                    Case PageSize.A4 : A4 += 1
                    Case PageSize.A5 : A5 += 1
                End Select
                .Dispose()
            End With
        Next
        LogSub(String.Format("A0 = {0}", A0))
        LogSub(String.Format("A1 = {0}", A1))
        LogSub(String.Format("A2 = {0}", A2))
        LogSub(String.Format("A3 = {0}", A3))
        LogSub(String.Format("A4 = {0}", A4))
        LogSub(String.Format("A5 = {0}", A5))

        rasterizer.Dispose()
        rasterizer = Nothing
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

            ' This sub mergePDFS can receive a delegate function 
            ' to run after merge the PDFS and then do whatever we want, like pagecount or anything like that.
            Utils.MergePdfs(FilesToMerge, outputFile)
        Next
    End Sub

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
        Next
    End Sub

    Private Sub MergeA4AndDrawingsSplit1(LogSub As IStep.LogSubDelegate)
        ' Documents by file will be the input1
        ' Drawings by file will be the input2

        ' I will have to go throg the subfolders of the input1 and then find the files to merge with the drawings
        ' The second folder should have pdfs of each subfolder containing the image files merged into a single file

        RecursiveMerge(New DirectoryInfo(CustomPropeties.Input1), New DirectoryInfo(CustomPropeties.Input2))
    End Sub

    Private Shared Sub RecursiveMerge(directory As DirectoryInfo, DrawingDir As DirectoryInfo)
        ' Need to go through each folder and merge with the drawings
        For Each subfolder In directory.GetDirectories()
            RecursiveMerge(subfolder, DrawingDir)
        Next

        MergeFolder(directory, DrawingDir)
    End Sub



    Private Shared Sub MergeFolder(directory As DirectoryInfo, DrawingDir As DirectoryInfo)
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
                FilesToMerge.Add(DrawingDir.FullName + "\" + subfile.FullName.Substring(subfile.FullName.Length - 11, 11))
                FilesToMerge.Add(subfile.FullName)
            Next
            outputFile = finalFile.FullName.Replace("_NOBARCODE.pdf", "_Final.pdf")
            Utils.MergePdfs(FilesToMerge, outputFile)
        Next

        ' Delete the files
        For Each File In Directoryfiles
            My.Computer.FileSystem.DeleteFile(File.FullName)
        Next
        For Each File In FinalFiles
            My.Computer.FileSystem.DeleteFile(File.FullName)
        Next
    End Sub

    Private Sub Wolverhampton2001UpdatePageCount(LogSub As IStep.LogSubDelegate)
        Dim csb As New SqlConnectionStringBuilder
        csb.DataSource = "localhost\SQLEXPRESS"
        csb.IntegratedSecurity = True
        csb.InitialCatalog = "VBProject"

        Dim connection As SqlConnection = New SqlConnection(csb.ToString())
        connection.Open()

        ' This is the way to update the database, we can just set the connection String to the server(Jerry)
        ' and then write the command to update the pagecount of the records by barcode
        ' need to write a method to be passed as a delegate to the merge function.
        ' the delegate functino will run at the end of the merge process.
        ' by doing that, everytime we merge a pdf we could pick the pagecount and update the database
        Dim query As SqlCommand = New SqlCommand("UPDATE PROCESSES SET DESCRIPTION = 'TESTE' WHERE ID = 28", connection)
        Dim reader As SqlDataReader = Nothing
        LogSub(String.Format("Rows affected {0}", query.ExecuteNonQuery))
    End Sub

    Private Sub CountPages_Old(Logsub As IStep.LogSubDelegate)
        Dim A5 As Integer = 0
        Dim A4 As Integer = 0
        Dim A3 As Integer = 0
        Dim A2 As Integer = 0
        Dim A1 As Integer = 0
        Dim A0 As Integer = 0


        For Each file In New DirectoryInfo(CustomPropeties.Input1).GetFiles("*.tif")
            With New Bitmap(file.FullName)
                Select Case Utils.GetPageSize(.Height, .Width)
                    Case PageSize.A0 : A0 += 1
                    Case PageSize.A1 : A1 += 1
                    Case PageSize.A2 : A2 += 1
                    Case PageSize.A3 : A3 += 1
                    Case PageSize.A4 : A4 += 1
                    Case PageSize.A5 : A5 += 1
                End Select
            End With
        Next

        Logsub(String.Format("A0 = {0}", A0))
        Logsub(String.Format("A1 = {0}", A1))
        Logsub(String.Format("A2 = {0}", A2))
        Logsub(String.Format("A3 = {0}", A3))
        Logsub(String.Format("A4 = {0}", A4))
        Logsub(String.Format("A5 = {0}", A5))
    End Sub

    Private Sub OcrTest(LogSub As IStep.LogSubDelegate)
        For Each File In New DirectoryInfo(CustomPropeties.Input1).GetFiles("*.tif")
            Utils.DoOCR(File.FullName, File.FullName.Replace(".tif", ""))
            LogSub(File.Name)
        Next
    End Sub

    Private Sub CountPages(LogSub As IStep.LogSubDelegate)
        Dim A4 As Integer = 0
        Dim A3 As Integer = 0
        Dim A2 As Integer = 0
        Dim A1 As Integer = 0
        Dim A0 As Integer = 0

        Dim reader As PdfReader = Nothing
        Dim pageSize As iTextSharp.text.Rectangle = Nothing

        For Each File In New DirectoryInfo(CustomPropeties.Input1).GetFiles("*.pdf")
            reader = New PdfReader(File.FullName)

            For index = 0 To reader.NumberOfPages
                Beep()
            Next
        Next
    End Sub
End Class
