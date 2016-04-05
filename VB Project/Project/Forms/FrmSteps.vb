Imports System.Data.Entity
Imports System.Reflection
Imports System.IO

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

    Public Sub New(ByVal StepEntity As EStep, ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()
        Me.Entity = StepEntity
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
            Case 4 : SaveSplitPDF()
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
                Case StepType.Bardecode : LoadBardecode()
                Case StepType.OCR : LoadOCR()
                Case StepType.ImgsToPDF ' LoadImgsToPDF
                Case StepType.Custom : LoadCustom()
                Case StepType.SplitPDFSize : LoadSplitPDF()
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
        Props.ExceptionFolder = Directory.GetCurrentDirectory() + "\Exception"
        Props.FileNamePattern = tx1FileInRegex.Text
        Props.OutputNameTemplate = tx1FileOutTemplate.Text
        Props.SubFolderPattern = tx1SubFolderRegex.Text
        Props.SplitMode = cb1SplitMode.SelectedIndex
        For Each item In cx1Barcodes.CheckedIndices
            Props.BarcodeTypes.Add(CType(item, BarcodeType))
        Next
        Props.FolderType = IIf(rbDocs.Checked, 0, 1)

        Entity.PropertiesObj = Serializer.ToXml(Props, Props.GetType())
        Entity.StepType = StepType.Bardecode
    End Sub

    Private Sub SaveCustom()
        Dim Props As PropertiesCustom = New PropertiesCustom()
        Props.Input1 = Directory.GetCurrentDirectory() + "Processing\Documents"
        Props.Input2 = Directory.GetCurrentDirectory() + "Processing\Drawings"
        Props.Output = Directory.GetCurrentDirectory() + "Processing\Documents"
        Props.CustomRunID = cb4CustomProcess.Text
        Entity.PropertiesObj = Serializer.ToXml(Props, Props.GetType())
        Entity.StepType = StepType.Custom
    End Sub

    Private Sub SaveOCR()
        Dim Props As PropertiesOCR = New PropertiesOCR()
        Props.InputFolder = Directory.GetCurrentDirectory() + "Processing\Documents"
        Props.OutputFolder = Directory.GetCurrentDirectory() + "Processing\Documents"
        Props.OutputNameTemplate = tx2FileOutTemplate.Text
        Props.ProcessSubFolders = True
        Props.CreateOutputSubFolders = True
        Props.DeleteInputFile = True
        Entity.PropertiesObj = Serializer.ToXml(Props, Props.GetType())
        Entity.StepType = StepType.OCR
    End Sub

    Private Sub SaveSplitPDF()
        Dim Props As PropertiesSplitPDFSize = New PropertiesSplitPDFSize()
        Props.InputFolder = Directory.GetCurrentDirectory() + "Processing\Documents"
        Props.FilePattern = tx5FileTemplate.Text
        Props.Size = CLng(tx5Size.Text)
        Props.ProcessSubFolders = True
        Entity.PropertiesObj = Serializer.ToXml(Props, Props.GetType())
        Entity.StepType = StepType.SplitPDFSize
    End Sub

    Private Sub LoadBardecode()
        With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesBardecode)), PropertiesBardecode)
            tx1BarcodeRegex.Text = .BarcodePattern
            tx1FileInRegex.Text = .FileNamePattern
            tx1FileOutTemplate.Text = .OutputNameTemplate
            tx1SubFolderRegex.Text = .SubFolderPattern
            cb1SplitMode.SelectedIndex = .SplitMode
            For Each item In .BarcodeTypes
                cx1Barcodes.SetItemChecked(item, True)
            Next
            rbDocs.Checked = .FolderType = 0
            rbDrws.Checked = Not rbDocs.Checked
        End With
    End Sub

    Private Sub LoadCustom()
        With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesCustom)), PropertiesCustom)
            cb4CustomProcess.Text = .CustomRunID
        End With
        cb4CustomProcess.Items.Clear()
        For Each method In GetType(StepCustom).GetMethods(BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
            cb4CustomProcess.Items.Add(method.Name)
        Next
    End Sub

    Private Sub LoadOCR()
        With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesOCR)), PropertiesOCR)
            tx2FileOutTemplate.Text = .OutputNameTemplate
        End With
    End Sub

    Private Sub LoadSplitPDF()
        With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesSplitPDFSize)), PropertiesSplitPDFSize)
            tx5FileTemplate.Text = .FilePattern
            tx5Size.Text = .Size.ToString()
        End With
    End Sub
End Class