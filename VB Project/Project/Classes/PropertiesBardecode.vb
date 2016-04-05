Imports System.IO

Public Structure PropertiesBardecode

    '[Folders and FileName]
    Public InputFolder As String
    Public OutputFolder As String
    Public ExceptionFolder As String
    Public ProcessedFolder As String

    Public CreateOutputSubFolders As Boolean
    Public ProcessSubFolders As Boolean
    Public DeleteInputFiles As Boolean
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
    'Split Mode
    Public SplitMode As Integer

    'Folder to process, 0 to documents, 1 to drawings
    Public FolderType As Integer

    Public Sub SetDefaultvalues()
        Me.FileNamePattern = ""
        Me.OutputNameTemplate = "%VALUES_%SEQ3"
        Me.BarcodeTypes = New List(Of BarcodeType) From {BarcodeType.Code_128, BarcodeType.Code_2_of_5, BarcodeType.Code_3_of_9}
        Me.BarcodePattern = ""
        Me.CreateOutputSubFolders = False
        Me.ProcessSubFolders = False
        Me.DeleteInputFiles = False
        Me.SplitMode = Project.SplitMode.BarcodeStart
        Me.FolderType = 0
    End Sub

End Structure
