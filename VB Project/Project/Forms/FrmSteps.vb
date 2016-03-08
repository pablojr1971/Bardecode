Imports System.Data.Entity

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
        Select Case Me.Entity.StepType
            Case StepType.Bardecode
                tcSteps.SelectedTab = PgBardecode

            Case StepType.OCR
                tcSteps.SelectedTab = PgOCR

            Case StepType.ImgsToPDF
                tcSteps.SelectedTab = PgImgsToPdf
        End Select
        LoadProperties()
        Me.ShowDialog(ParentForm)
    End Sub

    Private Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Select Case tcSteps.SelectedIndex
            Case 0 ' Bardecode
                Dim Props As PropertiesBardecode = New PropertiesBardecode
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
                Props.WhitelistChar = tx1WhitelistChar.Text
                For Each item In cx1Barcodes.CheckedIndices
                    Props.BarcodeTypes.Add(CType(item, BarcodeType))
                Next

                Entity.RunOrder = CInt(txRunOrder.Text)
                Entity.PropertiesObj = Serializer.ToXml(Props, Props.GetType)
                Entity.StepType = StepType.Bardecode
            Case 1 ' OCR


            Case 2 ' ImgsToPDF
        End Select
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
                        tx1WhitelistChar.Text = .WhitelistChar
                        For Each item In .BarcodeTypes
                            cx1Barcodes.SetItemChecked(item, True)
                        Next
                    End With

                Case StepType.OCR
                    With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesOCR)), PropertiesOCR)

                    End With

                Case StepType.ImgsToPDF
                    With CType(Serializer.FromXml(Me.Entity.PropertiesObj, GetType(PropertiesImgsToPDF)), PropertiesImgsToPDF)

                    End With

            End Select
        End If
        txRunOrder.Text = Entity.RunOrder
    End Sub
End Class