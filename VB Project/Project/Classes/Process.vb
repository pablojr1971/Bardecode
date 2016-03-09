' this class will contain a collection of steps
' and then a method run, who will iterate the steps and  run each of them

Public Class Process
    Private ctx As VBProjectContext = New VBProjectContext()
    Property Id As Integer
    Property Steps As List(Of IStep)

    Public Sub New(ProcessId As Integer)
        Id = ProcessId
        Steps = (From a In ctx.ESteps
                 Where a.Process = ProcessId
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
            End Select
        End With
    End Function

    Public Sub Run(LogSub As IStep.LogSubDelegate)
        For Each StepRun In Steps
            StepRun.Run(LogSub)
        Next
    End Sub
End Class
