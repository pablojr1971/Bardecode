Imports System.Threading
Imports System.IO
Imports System.Data.SqlClient
' this class will contain a collection of steps
' and then a method run, who will iterate the steps and  run each of them

Public Class Process
    Private ctx As VBProjectContext = New VBProjectContext()
    Private log As IStep.LogSubDelegate = Nothing
    Property Status As Integer = 0
    Property Id As Integer
    Property Steps As List(Of IStep)
    Public Delegate Sub EnableRunDelegate()
    Public EnableRun As EnableRunDelegate = Nothing

    Private InputPath As String = "Input path"
    Private OutputPath As String = "Output path"
    Private ProcessingPath As String = Directory.GetCurrentDirectory() + "\Processing\"

    Public Sub New(ProcessId As Integer)
        Id = ProcessId
        Steps = (From a In ctx.ESteps
                 Where a.Process = ProcessId And a.RunOrder < 90
                 Select a.Id, a.RunOrder
                 Order By RunOrder).AsEnumerable().Select(Function(p) CreateStepObj(p.Id)).ToList()
    End Sub

    Private Function CreateStepObj(StepId As Integer) As IStep
        CreateStepObj = Nothing
        With ctx.ESteps.Single(Function(p) p.Id = StepId)
            Select Case .StepType
                Case StepType.Bardecode
                    CreateStepObj = StepBardecode.LoadStep(.Id, ctx)

                Case StepType.OCR
                    CreateStepObj = StepOCR.LoadStep(.Id, ctx)

                Case StepType.ImgsToPDF
                    CreateStepObj = StepImgsToPDF.LoadStep(.Id, ctx)

                Case StepType.Custom
                    CreateStepObj = StepCustom.LoadStep(.Id, ctx)

                Case StepType.CSVIndexingProperties

                Case StepType.SplitPDFSize
                    CreateStepObj = StepSplitPDFSize.LoadStep(.Id, ctx)
            End Select
        End With
    End Function

    Public Sub Run(LogSub As IStep.LogSubDelegate)
        Me.log = LogSub
        Me.Status = 1

        If Not Directory.Exists(ProcessingPath) Then
            Directory.CreateDirectory(ProcessingPath)
        End If

        CopyBoxes(LogSub)

        EnableRun = AddressOf FrmMain.EnableRun
        Dim thread As New Thread(AddressOf ThreadTask)
        thread.IsBackground = True
        thread.Start()
    End Sub

    Private Sub ThreadTask()
        Dim index As Integer = 1
        For Each StepRun In Steps
            If index = 0 Then
                StepRun.Run()
            End If

            log(String.Format("Step {0}", index))
            StepRun.Run(log)
            index += 1
        Next

        Me.Status = 0
        Me.log = Nothing

        EnableRun()
    End Sub

    Private Sub CopyBoxes(LogSub As IStep.LogSubDelegate)
        Dim boxes As List(Of String) = New List(Of String)
        Dim connection As SqlConnection = New SqlConnection(FrmMain.ScanDataStringConnection)
        Dim command As SqlCommand = New SqlCommand("SELECT SUM(PAGECOUNT) PC, SUM(DRAWINGCOUNT) DC, BOX " + _
                                                  "  FROM SCANDATA " + _
                                                  " WHERE JOBNO = " + FrmMain.TextBox1.Text + _
                                                  "   AND BOX IS NOT NULL", connection)
        connection.Open()
        Dim reader As SqlDataReader = command.ExecuteReader()
        If reader.HasRows Then
            While reader.Read()
                If (reader.GetFieldValue(Of Integer)(0) = 0) And (reader.GetFieldValue(Of Integer)(1) = 0) Then
                    boxes.Add(reader.GetFieldValue(Of String)(2))
                End If
            End While
        End If

        Dim dir As DirectoryInfo = New DirectoryInfo(InputPath)

        For Each folder In dir.GetDirectories()
            If boxes.Contains(folder.Name.Substring(folder.Name.Length - 6, 6)) Then
                Directory.CreateDirectory(ProcessingPath + folder.Name)
                For Each File In folder.GetFiles()
                    File.CopyTo(ProcessingPath + folder.Name + "\" + File.Name)
                Next
            End If
        Next
    End Sub
End Class
