Imports System.Drawing.Imaging
Imports System.Windows.Media.Imaging
Imports System.IO
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class StepBarcodeRecognition
    Implements IStep

    Public BarcodeRecognitionProperties As PropertiesBarcodeRecognition

    Public ReadOnly Property Type As StepType Implements IStep.Type
        Get
            Return StepType.BardecodeRecognition
        End Get
    End Property

    Public Sub Run(LogSub As IStep.LogSubDelegate) Implements IStep.Run
        ' pegar todas as sections da tabela que se relaciona com este step
        ' e dai precisa montar as listas baseadas nos registros que vem do banco dessa tabela nova que precisa criar

        ' entao vamos ter uma tabela para guardar  as informacoes
        ' do step que serao cadastradas na hora de montar o processo a ser utilizado 
        ' ai quando for na hora de rodar o metodo run

        ' devemos colocar no metodo run um algoritimo que busque
        ' estas informacoes, monte uma strutura de lista com os dados do banco
        ' e depois percorra o documento procurando os barcodes de acordo com as expressoes 
        ' regex que vieram nas linhas trazidas do banco

        ' lembrar de criar um campo na tabela para guardar a opcao de fazer ocr nos documentos ou nao
        ' se for necessario fazer ocr, o mesmo deve ser feito junto com o reconhecimento dos codigos de barra

        'Dim connection As SqlConnection = New SqlConnection(FrmMain.ScanDataStringConnection)
        'connection.Open()

        'Dim command As SqlCommand = New SqlCommand("   UPDATE SCANDATA " + _
        '                                           "      SET PAGECOUNT = " + PageCount.ToString() + "," + _
        '                                           "          DRAWINGCOUNT = " + DrawingCount.ToString() + _
        '                                           "   WHERE JOBNO = 2017 " + _
        '                                           "     AND BARCODE = '" + element.Key.Value + "'", connection)
        'UpdateLogList.Add(String.Format("Updated ScanData {0} - Rows Affected = ", element.Key.Value) + command.ExecuteNonQuery().ToString())

        'connection.Close()
        'command.Dispose()
        'connection.Dispose()

        ' Things that this process will concern from the changes

        ' It will have to work with Drawings Only

    End Sub

    Public Sub New()
        BarcodeRecognitionProperties = New PropertiesBarcodeRecognition()
        BarcodeRecognitionProperties.SetDefaultvalues()
    End Sub

    Public Sub New(Properties As PropertiesBarcodeRecognition)
        Me.BarcodeRecognitionProperties = Properties
        Me.BarcodeRecognitionProperties.InputFolder = Directory.GetCurrentDirectory() + "\Processing\Documents"
    End Sub

    Public Shared Function LoadStep(StepId As Integer, ctx As VBProjectContext) As StepBarcodeRecognition
        With ctx.ESteps.Single(Function(p) p.Id = StepId)
            LoadStep = New StepBarcodeRecognition(Serializer.FromXml(.PropertiesObj, GetType(PropertiesBarcodeRecognition)))
        End With
    End Function

End Class