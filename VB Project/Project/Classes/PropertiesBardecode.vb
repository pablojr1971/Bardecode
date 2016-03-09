Imports System.IO

Public Structure PropertiesBardecode

    '[Folders and FileName]
    Public InputFolder As String
    Public OutputFolder As String
    Public ExceptionFolder As String
    Public ProcessedFolder As String

    Public CreateOutputSubFolders As Boolean
    Public ProcessSubFolders As Boolean
    Public SubFolderPattern As String
    'This property must contain a regex expression that bardecode will use to find just files that matches with this expression
    Public FileNamePattern As String

    '[output]
    'Must contain the output name in the same format that bardecode use to generate the file names
    Public OutputNameTemplate As String

    '[Barcode recognition]
    Public BarcodeTypes As List(Of BarcodeType)
    'Must contain a regex expression that bardecode will use to pick up just the barcodes that matches with the expression
    Public BarcodePattern As String
    Public MinimumBarcodeSize As Integer
    Public MaximumBarcodeSize As Integer

    Public Sub SetDefaultvalues()
        Me.InputFolder = Directory.GetCurrentDirectory()
        Me.OutputFolder = Directory.GetCurrentDirectory()
        Me.ExceptionFolder = Directory.GetCurrentDirectory()
        Me.ProcessedFolder = Directory.GetCurrentDirectory()
        Me.FileNamePattern = ""
        Me.OutputNameTemplate = "%VALUES_%SEQ3"
        Me.BarcodeTypes = New List(Of BarcodeType) From {BarcodeType.Code_128, BarcodeType.Code_2_of_5, BarcodeType.Code_3_of_9}
        Me.BarcodePattern = ""
        Me.MinimumBarcodeSize = 4
        Me.MaximumBarcodeSize = 99
        Me.CreateOutputSubFolders = True
    End Sub

End Structure
