Imports System.Threading
Imports System.IO
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Process
    Private ctx As VBProjectContext = New VBProjectContext()
    Private JobNo As Integer
    Private Id As Integer
    Private DocInputFolder As DirectoryInfo = Nothing
    Private DrwInputFolder As DirectoryInfo = Nothing
    Private Outfolder As DirectoryInfo = Nothing
    Private Steps As List(Of IStep)
    Private LogObj As LogManager = New LogManager()
    Private LogSub As IStep.LogSubDelegate
    Public Delegate Sub FinishProcessDelegate(Path As String)

    Public Sub New(ProcessId As Integer, JobNumber As Integer)
        Me.Id = ProcessId
        Me.Steps = (From a In ctx.ESteps
                    Where a.Process = ProcessId And a.RunOrder < 90
                    Select a.Id, a.RunOrder
                    Order By RunOrder).AsEnumerable().Select(Function(p) CreateStepObj(p.Id)).ToList()
        Me.JobNo = JobNumber

        With ctx.EProcesses.Single(Function(p) p.Id = ProcessId)
            Me.DocInputFolder = New DirectoryInfo(.docInput)
            If Directory.Exists(.drwInput) Then
                Me.DrwInputFolder = New DirectoryInfo(.drwInput)
            End If
            Me.Outfolder = New DirectoryInfo(.outFolder)
        End With

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

    Public Sub WriteLog(Log As String)
        LogSub(Log)
        LogObj.WriteLine(Date.Now.ToShortTimeString + " - " + Log)
    End Sub

    Public Sub Run(LogSub As IStep.LogSubDelegate)
        Me.LogSub = LogSub
        Dim parameters(4) As Object
        parameters(0) = getToProcessBoxes()
        parameters(1) = CType(AddressOf FrmMain.FinishProcess, FinishProcessDelegate)
        parameters(2) = New IStep.LogSubDelegate(AddressOf WriteLog)
        parameters(3) = Me.JobNo
        parameters(4) = Me.LogObj

        Dim thread As New Thread(AddressOf ThreadTask)
        thread.IsBackground = True
        thread.Start(parameters)
    End Sub

    Private Sub ThreadTask(ByVal parameters As Object)
        Dim Boxes As List(Of String) = parameters(0)
        Dim FinishProcess As FinishProcessDelegate = parameters(1)
        Dim log As IStep.LogSubDelegate = parameters(2)
        Dim Job As Integer = parameters(3)
        Dim LogManager As LogManager = parameters(4)

        Dim index As Integer = 1
        Dim ProcessingFolder As DirectoryInfo = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing")
        Dim DocFolder As DirectoryInfo = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing\Documents")
        Dim DrwFolder As DirectoryInfo = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing\Drawings")
        Dim Ocrfolder As DirectoryInfo = New DirectoryInfo(Directory.GetCurrentDirectory() + "\Processing\OCR")

        Dim expression As String = Nothing
        Try
            If ProcessingFolder.Exists Then
                ProcessingFolder.Delete(True)
                ProcessingFolder.Refresh()
            End If

            For Each box In Boxes
                Try
                    expression = String.Format("({0})(.{{0,}})({1})", JobNo, box)
                    If DocInputFolder.GetDirectories().Where(Function(p) New Regex(expression).Match(p.Name).Success).ToList.Count > 0 Then
                        LogManager.CreateLogFile(Outfolder.FullName + "\Log - " + box + ".txt")
                        If Not ProcessingFolder.Exists() Then
                            ProcessingFolder.Create()
                            DocFolder.Create()
                            DrwFolder.Create()
                            Ocrfolder.Create()
                        End If


                        log("Copying box " + box)
                        For Each folder In DocInputFolder.GetDirectories().Where(Function(p) New Regex(expression).Match(p.Name).Success)
                            My.Computer.FileSystem.CopyDirectory(folder.FullName, DocFolder.FullName + "\" + folder.Name)
                            If Directory.Exists(DrwInputFolder.FullName + "\" + folder.Name + "D") Then
                                My.Computer.FileSystem.CopyDirectory(DrwInputFolder.FullName + "\" + folder.Name + "D", DrwFolder.FullName + "\" + folder.Name + "D")
                            Else
                                log(String.Format("Drawing Folder Missing - Box No. {0}", box))
                            End If
                        Next
                        Thread.Sleep(2000)

                        log("Processing box " + box + vbCrLf)

                        For Each StepRun In Steps
                            log(String.Format("Step {0}", index))
                            StepRun.Run(log)
                            index += 1
                            Thread.Sleep(500)
                        Next

                        log("box " + box + " Done - Moving to output Folder" + vbCrLf + vbCrLf)

                        ' Force collect to remove any objects that could be blocking files
                        GC.Collect()

                        For Each folder In DocFolder.GetDirectories()
                            My.Computer.FileSystem.MoveDirectory(folder.FullName, Outfolder.FullName + "\" + folder.Name, True)
                            While folder.Exists()
                                folder.Refresh()
                                Thread.Sleep(1)
                            End While
                        Next

                        ProcessingFolder.Delete(True)
                        ProcessingFolder.Refresh()
                        If Directory.Exists(Directory.GetCurrentDirectory() + "\Exception") Then
                            My.Computer.FileSystem.DeleteDirectory(Directory.GetCurrentDirectory() + "\Exception", FileIO.DeleteDirectoryOption.DeleteAllContents)
                        End If
                        LogManager.CloseLogFile()
                    End If
                    index = 1
                Catch e As Exception
                    log(e.Message + vbCrLf)
                    log("Errors Running this box. Process Stopped." + vbCrLf)
                    log(e.Message + vbCrLf)

                    ProcessingFolder.Delete(True)
                    ProcessingFolder.Refresh()
                    If Directory.Exists(Directory.GetCurrentDirectory() + "\Exception") Then
                        My.Computer.FileSystem.DeleteDirectory(Directory.GetCurrentDirectory() + "\Exception", FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                    log("Skip to next box.")
                    index = 1
                    LogManager.CloseLogFile()
                    Continue For
                End Try
            Next

            If Job = 0 Then
                For Each folder In DocInputFolder.GetDirectories()
                    Try
                        LogManager.CreateLogFile(Outfolder.FullName + "\" + folder.Name + ".txt")
                        log("Copying folder " + folder.Name)
                        My.Computer.FileSystem.CopyDirectory(folder.FullName, DocFolder.FullName + "\" + folder.Name)
                        If Not IsNothing(DrwInputFolder) Then
                            If Directory.Exists(DrwInputFolder.FullName + "\" + folder.Name + "D") Then
                                My.Computer.FileSystem.CopyDirectory(DrwInputFolder.FullName + "\" + folder.Name + "D", DrwFolder.FullName + "\" + folder.Name + "D")
                            Else
                                log(String.Format("Drawing Folder Missing - Doc Folder {0}", folder.Name))
                            End If
                        End If

                        log("Processing folder " + folder.Name + vbCrLf)

                        For Each StepRun In Steps
                            log(String.Format("Step {0}", index))
                            StepRun.Run(log)
                            index += 1
                            Thread.Sleep(500)
                        Next

                        log("folder " + folder.Name + " Done - Moving to output Folder" + vbCrLf + vbCrLf)

                        ' Force collect to remove any objects that could be blocking files when trying to move
                        GC.Collect()

                        For Each subfolder In DocFolder.GetDirectories()
                            My.Computer.FileSystem.MoveDirectory(subfolder.FullName, Outfolder.FullName + "\" + subfolder.Name, True)
                            While subfolder.Exists()
                                subfolder.Refresh()
                                Thread.Sleep(1)
                            End While
                        Next

                        ProcessingFolder.Delete(True)
                        ProcessingFolder.Refresh()
                        If Directory.Exists(Directory.GetCurrentDirectory() + "\Exception") Then
                            My.Computer.FileSystem.DeleteDirectory(Directory.GetCurrentDirectory() + "\Exception", FileIO.DeleteDirectoryOption.DeleteAllContents)
                        End If
                        LogManager.CloseLogFile()
                    Catch e As Exception
                        log(e.Message + vbCrLf)
                        log("Errors Running this box. Process Stopped.")
                        log("Moving current data to outputFolder.")

                        My.Computer.FileSystem.MoveDirectory(Ocrfolder.FullName, Outfolder.FullName + "\" + folder.Name, True)
                        Thread.Sleep(2000)

                        ProcessingFolder.Delete(True)
                        ProcessingFolder.Refresh()
                        If Directory.Exists(Directory.GetCurrentDirectory() + "\Exception") Then
                            My.Computer.FileSystem.DeleteDirectory(Directory.GetCurrentDirectory() + "\Exception", FileIO.DeleteDirectoryOption.DeleteAllContents)
                        End If
                        LogManager.CloseLogFile()
                        Continue For
                    End Try
                Next
            End If

            FinishProcess(Outfolder.FullName)
        Catch e As Exception
            log(e.Message + vbCrLf + "Process end with errors")
        End Try
    End Sub

    Private Function getToProcessBoxes() As List(Of String)
        ' with this sub we will find all the boxes that we need to process
        ' return pass them to the thread 
        Dim boxes As List(Of String) = New List(Of String)
        Dim connection As SqlConnection = New SqlConnection(FrmMain.ScanDataStringConnection)
        Dim command As SqlCommand = New SqlCommand("SELECT ISNULL(SUM(PAGECOUNT),0) PC, ISNULL(SUM(DRAWINGCOUNT),0) DC, BOX " + _
                                                  "  FROM SCANDATA " + _
                                                  " WHERE JOBNO = " + JobNo.ToString + _
                                                  "   AND BOX IS NOT NULL" + _
                                                  " GROUP BY BOX ", connection)
        connection.Open()
        Dim reader As SqlDataReader = command.ExecuteReader()
        If reader.HasRows Then
            While reader.Read()
                If (reader.GetFieldValue(Of Integer)(0) = 0) And (reader.GetFieldValue(Of Integer)(1) = 0) Then
                    boxes.Add(reader.GetFieldValue(Of String)(2).Replace("BOX", ""))
                End If
            End While
        End If
        Return boxes
    End Function
End Class
