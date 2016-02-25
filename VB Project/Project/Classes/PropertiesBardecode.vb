Imports System.IO

Public Structure PropertiesBardecode

    '[Bardecode]
    Dim IniFilePath As String
    Dim BardecodeExe As String

    '[Folders and FileName]
    Dim InputFolder As String
    Dim OutputFolder As String
    Dim ExceptionFolder As String
    Dim ProcessedFolder As String

    Dim ProcessSubFolders As Boolean
    Dim SubFolderPattern As String
    'This property must contain a regex expression that bardecode will use to find just files that matches with this expression
    Dim FileNamePattern As String

    '[output]
    'Must contain the output name in the same format that bardecode use to generate the file names
    Dim OutputNameTemplate As String

    '[Barcode recognition]
    Dim BarcodeTypes As List(Of BarcodeType)
    'Must contain a regex expression that bardecode will use to pick up just the barcodes that matches with the expression
    Dim BarcodePattern As String
    Dim MinimumBarcodeSize As Integer
    Dim MaximumBarcodeSize As Integer
    'This propertie must contain all chars that bardecode can recognize in barcode values
    Dim WhitelistChar As String

    Sub SetDefaultvalues()
        Me.InputFolder = Application.ExecutablePath()
        Me.OutputFolder = Application.ExecutablePath()
        Me.ExceptionFolder = Application.ExecutablePath()
        Me.ProcessedFolder = Application.ExecutablePath()
        Me.FileNamePattern = ""
        Me.OutputNameTemplate = "%VALUES_%SEQ3"
        Me.BarcodeTypes = New List(Of BarcodeType) From {BarcodeType.Code_128, BarcodeType.Code_2_of_5, BarcodeType.Code_3_of_9}
        Me.BarcodePattern = ""
        Me.MinimumBarcodeSize = 4
        Me.MaximumBarcodeSize = 99
        Me.WhitelistChar = "123456789_+-:.$ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz "
    End Sub

End Structure
