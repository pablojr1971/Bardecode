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
        Me.Entity = ctx.ESteps.Local.Single(Function(p) p.Id = Id)

        Select Case Me.Entity.StepType
            Case StepType.Bardecode
                tcSteps.SelectedTab = PgBardecode

            Case StepType.OCR
                tcSteps.SelectedTab = PgOCR

            Case StepType.ImgsToPDF
                tcSteps.SelectedTab = PgImgsToPdf

        End Select
        Me.ShowDialog(ParentForm)
    End Sub

    Private Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Select Case tcSteps.SelectedIndex
            Case 0 ' Bardecode
                Dim Props As New PropertiesBardecode
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
                For Each item In cx1Barcodes.SelectedIndices
                    Props.BarcodeTypes.Add(item)
                Next

                Entity.RunOrder = CInt(txRunOrder.text)
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
End Class