Imports System.Threading
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

        EnableRun = AddressOf FrmMain.EnableRun
        Dim thread As New Thread(AddressOf ThreadTask)        
        thread.IsBackground = True
        thread.Start()
    End Sub

    Private Sub ThreadTask()
        Dim index As Integer = 1
        For Each StepRun In Steps
            log(String.Format("Step {0}", index))
            StepRun.Run(log)
            index += 1
        Next

        Me.Status = 0
        Me.log = Nothing

        EnableRun()
    End Sub
End Class
