Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class MainForm
    Public Property ProcessConfig As ProcessSetup
    Delegate Sub LogDelegate(Log As String)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles OpenButton.Click
        If XMLDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = XMLDialog.FileName
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles PlayButton.Click
        Dim XMLFile As FileInfo = Nothing
        If TextBox1.Text.Length > 0 Then
            XMLFile = New FileInfo(TextBox1.Text)
        End If

        Dim XMLExample As String = Nothing
        XMLExample = _
            "<?xml version=""1.0"" encoding=""UTF-8""?>  " + vbCrLf + _
            "<Process>                                   " + vbCrLf + _
            "   <JobNo></JobNo>                          " + vbCrLf + _
            "   <Name></Name>                            " + vbCrLf + _
            "   <A4InputFolder></A4InputFolder>          " + vbCrLf + _
            "   <LFInputFolder></LFInputFolder>          " + vbCrLf + _
            "   <OutputFolder></OutputFolder>            " + vbCrLf + _
            "   <LogFolder></LogFolder>                  " + vbCrLf + _
            "   <BWA4></BWA4>                            " + vbCrLf + _
            "   <BWLF></BWLF>                            " + vbCrLf + _
            "   <OCRA4></OCRA4>                          " + vbCrLf + _
            "   <OCRLF></OCRLF>                          " + vbCrLf + _
            "   <FStartRegex></FStartRegex>              " + vbCrLf + _
            "   <FOutputName></FOutputName>              " + vbCrLf + _
            "   <FFolder></FFolder>                      " + vbCrLf + _
            "   <FSplitSizeMB></FSplitSizeMB>            " + vbCrLf + _
            "   <SectionList>                            " + vbCrLf + _
            "       <Section>                            " + vbCrLf + _
            "           <SName></SName>                  " + vbCrLf + _
            "           <SRegex></SRegex>                " + vbCrLf + _
            "           <SOutputName></SOutputName>      " + vbCrLf + _
            "           <SFolderName></SFolderName>      " + vbCrLf + _
            "           <SOncePerFile></SOncePerFile>    " + vbCrLf + _
            "           <SRequired></SRequired>          " + vbCrLf + _
            "           <SDocPerPage></SDocPerPage>      " + vbCrLf + _
            "       </Section>	                         " + vbCrLf + _
            "   </SectionList>                           " + vbCrLf + _
            "</Process>                                  "

        If Not (Not IsNothing(XMLFile) AndAlso XMLFile.Extension = ".xml" AndAlso XMLFile.Exists) Then
            MessageBox.Show("The XML Setup file should be as following" + vbCrLf + vbCrLf + XMLExample)
        Else
            Run(XMLFile.FullName)
        End If
    End Sub

    Private Sub Run(XMLFilePath As String)
        ProcessConfig = New ProcessSetup()
        Dim XML As XmlDocument = New XmlDocument()
        XML.Load(XMLFilePath)

        ProcessConfig.JobNo = CInt(XML.SelectSingleNode("Process/JobNo").InnerText)
        ProcessConfig.Name = XML.SelectSingleNode("Process/Name").InnerText
        ProcessConfig.A4InputFolder = New DirectoryInfo(XML.SelectSingleNode("Process/A4InputFolder").InnerText)
        ProcessConfig.LFInputFolder = New DirectoryInfo(XML.SelectSingleNode("Process/LFInputFolder").InnerText)
        ProcessConfig.OutputFolder = New DirectoryInfo(XML.SelectSingleNode("Process/OutputFolder").InnerText)
        ProcessConfig.LogFolder = New DirectoryInfo(XML.SelectSingleNode("Process/LogFolder").InnerText)
        ProcessConfig.BWA4 = CBool(XML.SelectSingleNode("Process/BWA4").InnerText)
        ProcessConfig.BWLF = CBool(XML.SelectSingleNode("Process/BWLF").InnerText)
        ProcessConfig.OCRA4 = CBool(XML.SelectSingleNode("Process/OCRA4").InnerText)
        ProcessConfig.OCRLF = CBool(XML.SelectSingleNode("Process/OCRLF").InnerText)
        ProcessConfig.FStartRegex = New Regex(XML.SelectSingleNode("Process/FStartRegex").InnerText)
        ProcessConfig.FOutputName = XML.SelectSingleNode("Process/FOutputName").InnerText
        ProcessConfig.FFolder = XML.SelectSingleNode("Process/FFolder").InnerText
        ProcessConfig.FSplitSizeMB = CInt(XML.SelectSingleNode("Process/FSplitSizeMB").InnerText)

        For Each section As XmlNode In XML.SelectNodes("Process/SectionList/Section")
            ProcessConfig.Sections.Add(New SectionSetup)
            With ProcessConfig.Sections.Last
                .Name = section.SelectSingleNode("SName").InnerText
                .Regex = New Regex(section.SelectSingleNode("SRegex").InnerText)
                .OutputName = section.SelectSingleNode("SOutputName").InnerText
                .FolderName = section.SelectSingleNode("SFolderName").InnerText
                .OncePerFile = CBool(section.SelectSingleNode("SOncePerFile").InnerText)
                .Required = CBool(section.SelectSingleNode("SRequired").InnerText)
                .DocPerPage = CBool(section.SelectSingleNode("SDocPerPage").InnerText)
            End With
        Next

        Dim parameters(2) As Object
        parameters(0) = ProcessConfig
        parameters(1) = New LogDelegate(AddressOf writeLog)

        Dim thread As New Threading.Thread(AddressOf ThreadTask)
        thread.IsBackground = True
        thread.Start(parameters)
    End Sub

    Private Sub writeLog(Log As String)
        txProcessLog.AppendText(Date.Now.ToShortTimeString + " - " + Log + vbCrLf)
    End Sub

    Private Sub ThreadTask(ByVal parameters As Object)
        Dim Process As Process = New Process(parameters(0), parameters(1))
        Process.Run()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MainForm.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
