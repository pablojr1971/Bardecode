Imports System.Data.Entity
Imports System.Reflection

Public Class FrmSteps

    Private Entity As EStep
    Private Inserting As Boolean
    Private ctx As VBProjectContext = New VBProjectContext()
    Public ReadOnly Property EntityStep As EStep
        Get
            Return Me.Entity
        End Get
    End Property

    Public Sub New(ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()
        Me.Entity = New EStep
        Me.ShowDialog(ParentForm)
    End Sub

    Public Sub New(Id As Integer, ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()
        ctx.ESteps.Where(Function(p) p.Id = Id).Load()
        Me.Entity = ctx.ESteps.Single(Function(p) p.Id = Id)
        ShowForm()
    End Sub

    Public Sub New(ByVal StepEntity As EStep, ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()
        Me.Entity = StepEntity
        ShowForm()
    End Sub

    Private Sub ShowForm()
        tcSteps.SelectedIndex = Me.Entity.StepType
        LoadProperties()
        Me.ShowDialog(ParentForm)
    End Sub

    Private Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Select Case tcSteps.SelectedIndex
            Case 0 : SaveBardecode()
            Case 1 : SaveOCR()
            Case 2 'SaveImgsToPDF
            Case 3 : SaveCustom()
        End Select
        Entity.RunOrder = txRunOrder.Text
        Close()
    End Sub

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Me.Entity = Nothing
        Close()
    End Sub

    Private Sub LoadProperties()
        If Not String.IsNullOrEmpty(Me.Entity.PropertiesObj) Then            
            Select Case EntityStep.StepType
                Case StepType.Bardecode
                    With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesBardecode)), PropertiesBardecode)
                        tx1BarcodeRegex.Text = .BarcodePattern
                        tx1ExceptionFolder.Text = .ExceptionFolder
                        tx1FileInRegex.Text = .FileNamePattern
                        tx1InputFolder.Text = .InputFolder
                        tx1OutputFolder.Text = .OutputFolder
                        tx1FileOutTemplate.Text = .OutputNameTemplate
                        tx1ProcessedFolder.Text = .ProcessedFolder
                        cx1SubFolders.Checked = .ProcessSubFolders
                        tx1SubFolderRegex.Text = .SubFolderPattern
                        cx1CreateOutSubFolders.Checked = .CreateOutputSubFolders
                        For Each item In .BarcodeTypes
                            cx1Barcodes.SetItemChecked(item, True)
                        Next
                    End With

                Case StepType.OCR
                    With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesOCR)), PropertiesOCR)
                        tx2InputFolder.Text = .InputFolder
                        tx2OutputFolder.Text = .OutputFolder
                        tx2FileOutTemplate.Text = .OutputNameTemplate
                        cx2CreateOutSubFolders.Checked = .CreateOutputSubFolders
                        cx2ProcessSubFolders.Checked = .ProcessSubFolders
                    End With

                Case StepType.ImgsToPDF
                    With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesImgsToPDF)), PropertiesImgsToPDF)

                    End With

                Case StepType.Custom
                    With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesCustom)), PropertiesCustom)
                        tx4InputFolder.Text = .InputDirectory
                        tx4OutputFolder.Text = .OutputDirectory
                        cb4CustomProcess.Text = .CustomRunID
                    End With
                    cb4CustomProcess.Items.Clear()
                    For Each method In GetType(StepCustom).GetMethods(BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
                        cb4CustomProcess.Items.Add(method.Name)
                    Next

            End Select
        End If
        txRunOrder.Text = Entity.RunOrder
    End Sub

    Private Sub tcSteps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcSteps.SelectedIndexChanged
        Select Case tcSteps.SelectedIndex
            Case 3
                cb4CustomProcess.Items.Clear()
                For Each method In GetType(StepCustom).GetMethods(BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
                    cb4CustomProcess.Items.Add(method.Name)
                Next
        End Select
    End Sub

    Private Sub SaveBardecode()
        Dim Props As PropertiesBardecode = New PropertiesBardecode()
        Props.BarcodeTypes = New List(Of BarcodeType)
        Props.BarcodePattern = tx1BarcodeRegex.Text
        Props.ExceptionFolder = tx1ExceptionFolder.Text
        Props.FileNamePattern = tx1FileInRegex.Text
        Props.InputFolder = tx1InputFolder.Text
        Props.OutputFolder = tx1OutputFolder.Text
        Props.OutputNameTemplate = tx1FileOutTemplate.Text
        Props.ProcessedFolder = tx1ProcessedFolder.Text
        Props.ProcessSubFolders = cx1SubFolders.Checked
        Props.SubFolderPattern = tx1SubFolderRegex.Text
        Props.CreateOutputSubFolders = cx1CreateOutSubFolders.Checked
        For Each item In cx1Barcodes.CheckedIndices
            Props.BarcodeTypes.Add(CType(item, BarcodeType))
        Next
        Entity.PropertiesObj = Serializer.ToXml(Props, Props.GetType)
        Entity.StepType = StepType.Bardecode
    End Sub

    Private Sub SaveCustom()
        Dim Props As PropertiesCustom = New PropertiesCustom()
        Props.InputDirectory = tx4InputFolder.Text
        Props.OutputDirectory = tx4OutputFolder.Text
        Props.CustomRunID = cb4CustomProcess.Text
        Entity.PropertiesObj = Serializer.ToXml(Props, Props.GetType)
        Entity.StepType = StepType.Custom
    End Sub

    Private Sub SaveOCR()
        Dim Props As PropertiesOCR = New PropertiesOCR()
        Props.InputFolder = tx2InputFolder.Text
        Props.OutputFolder = tx2OutputFolder.Text
        Props.OutputNameTemplate = tx2FileOutTemplate.Text
        Props.ProcessSubFolders = cx2ProcessSubFolders.Checked
        Props.CreateOutputSubFolders = cx2CreateOutSubFolders.Checked
        Entity.PropertiesObj = Serializer.ToXml(Props, Props.GetType)
        Entity.StepType = StepType.OCR
    End Sub
End Class