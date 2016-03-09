Imports System.Reflection
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
                                Nothing)
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

    Private Sub CustomMethod1()
        MessageBox.Show("This is the custom Method 1")
    End Sub

    Private Sub CustomMethod2()
        MessageBox.Show("This is the custom Method 2")
    End Sub

    Private Sub CustomMethod3()
        MessageBox.Show("This is the custom Method 3")
    End Sub
End Class
