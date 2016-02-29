Imports System.IO

Public Class StepBardecode
    Implements IStep

    Private BardecodeProcessInfo As ProcessStartInfo
    Public BardecodeProperties As PropertiesBardecode

    Public ReadOnly Property Type As Project.StepType Implements IStep.Type
        Get
            Return Project.StepType.Bardecode
        End Get
    End Property

    Sub New()
        Me.BardecodeProperties = New PropertiesBardecode
        Me.BardecodeProperties.SetDefaultvalues()
    End Sub

    Public Sub RunFile(File As FileInfo) Implements IStep.RunFile
        ' for this method we can pickup the file name and set up as a filename pattern on the regex expression
        ' so when bardecode will process the hole folder it will only pick up that file that we passing through this method
    End Sub

    Public Sub RunFiles(Files As List(Of FileInfo)) Implements IStep.RunFiles
        For Each File In Files
            RunFile(File)
        Next
    End Sub

    Public Sub RunFolder(Folder As DirectoryInfo, RunSubFolders As Boolean, SearchPattern As String) Implements IStep.RunFolder
        SetBardecodeProperties()
        If RunSubFolders Then
            For Each Subfolder In IIf(SearchPattern = "", Folder.GetDirectories(), Folder.GetDirectories(SearchPattern))
                Me.ChangeInputPath(Subfolder.FullName)
                Me.StartBardecodeProcess()
            Next
        Else
            Me.ChangeInputPath(Folder.FullName)
            Me.StartBardecodeProcess()
        End If
    End Sub

    Public Sub StartBardecodeProcess()
        Me.BardecodeProcessInfo = New ProcessStartInfo()
        Me.BardecodeProcessInfo.FileName = Me.BardecodeProperties.BardecodeExe
        Me.BardecodeProcessInfo.Arguments = "BardecodeIni.ini"
        Me.BardecodeProcessInfo.UseShellExecute = True
        Me.BardecodeProcessInfo.WindowStyle = ProcessWindowStyle.Normal

        With System.Diagnostics.Process.Start(Me.BardecodeProcessInfo)
            .WaitForExit()
            .Close()
            .Dispose()
        End With
    End Sub

    Public Sub SetBardecodeProperties()
        ' on this sub i will have to pick everything that i have on that class of properties
        ' and pass it to the inifile on the same directory were bardecode is placed
        ' will have to modify the inifiles each time that i run bardecode passing just the subfolders and not the whole input folder of the boxes

        ' check the type of the folder that are being processed and create the IniFile object according with it
        Dim BardecodeIni As IniFile = New IniFile(Directory.GetCurrentDirectory() + "\BardecodeIni.ini")

        'Folders and files
        BardecodeIni.WriteValue("options", "inputFolder", "System.String," + Me.BardecodeProperties.InputFolder)
        BardecodeIni.WriteValue("options", "outputFolder", "System.String," + Me.BardecodeProperties.OutputFolder)
        BardecodeIni.WriteValue("options", "exceptionFolder", "System.String," + Me.BardecodeProperties.ExceptionFolder)
        BardecodeIni.WriteValue("options", "outputTemplate", "System.String," + Me.BardecodeProperties.OutputNameTemplate)
        BardecodeIni.WriteValue("options", "FilePattern", "System.String," + Me.BardecodeProperties.FileNamePattern)
        BardecodeIni.WriteValue("options", "ProcessSubFolders", "System.Boolean," + Me.BardecodeProperties.ProcessSubFolders.ToString())
        BardecodeIni.WriteValue("options", "SubFolderPattern", "System.String," + Me.BardecodeProperties.SubFolderPattern)

        'Barcode Types
        BardecodeIni.WriteValue("options", "Codabar", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.Codabar).ToString())
        BardecodeIni.WriteValue("options", "Code 128", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.Code_128).ToString())
        BardecodeIni.WriteValue("options", "Code 2 of 5 (interleaved)", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.Code_2_of_5).ToString())
        BardecodeIni.WriteValue("options", "Code 2 of 5 (other)", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.Code_2_of_5_other).ToString())
        BardecodeIni.WriteValue("options", "Code 3 of 9", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.Code_3_of_9).ToString())
        BardecodeIni.WriteValue("options", "PDF-417", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.PDF_417).ToString())
        BardecodeIni.WriteValue("options", "EAN-13 and UPC-A", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.EAN_13_UPC_A).ToString())
        BardecodeIni.WriteValue("options", "EAN-8", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.EAN_8).ToString())
        BardecodeIni.WriteValue("options", "UPC-E", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.UPC_E).ToString())
        BardecodeIni.WriteValue("options", "Datamatrix", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.Datamatrix).ToString())
        BardecodeIni.WriteValue("options", "QR-Code", "System.Boolean," + Me.BardecodeProperties.BarcodeTypes.Contains(BarcodeType.QR_Code).ToString())

        'Barcode Pattern, Size e WhiteChars
        BardecodeIni.WriteValue("options", "Pattern", "System.String," + Me.BardecodeProperties.BarcodePattern)
        BardecodeIni.WriteValue("options", "MinLength", "System.Int32," + Me.BardecodeProperties.MinimumBarcodeSize.ToString())
        BardecodeIni.WriteValue("options", "MaxLength", "System.Int32," + Me.BardecodeProperties.MaximumBarcodeSize.ToString())
        BardecodeIni.WriteValue("options", "FilterChars", "System.String," + Me.BardecodeProperties.WhitelistChar)
    End Sub

    Public Sub ChangeInputPath(Path As String)
        ' this method will be invoked just through the loop to change the input folder of the bardecode inifile configuration
        ' we need to process just one folder at time so thats the reason why we need to change the input folder on the inifile on the go

        Dim BardecodeIni As IniFile = New IniFile(Directory.GetCurrentDirectory() + "\BardecodeIni.ini")
        BardecodeIni.WriteValue("options", "inputFolder", "System.String," + Path)
    End Sub
End Class
