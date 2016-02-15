Imports System.IO

Public Class MainForm
    Private Const Bardecode = "C:\Program Files (x86)\Softek Software\BardecodeFiler\BardecodeFiler.exe"
    Private JobPath As String
    Private Ini As IniFile
    Private Folders As Collection = New Collection()

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles RunButton.Click
        JobPath = JobPathText.Text
        If Directory.Exists(JobPath) Then

            If File.Exists(JobPath + "\INIFILE.ini") Then
                Ini = New IniFile(JobPath + "\INIFILE.ini")
            End If

            Me.CheckFolders(JobPath)
            Me.LogFolders()
            Me.ProcessLogText.AppendText(vbCrLf + "RUNNING")
            Me.ProcessBoxes()
            Me.ProcessLogText.AppendText(vbCrLf + "DONE")
        End If
    End Sub

    Private Sub ProcessBoxes()
        For Each Folder As DirectoryInfo In Folders
            Ini.WriteValue("options", "inputFolder", "System.String," + Folder.FullName)
            Ini.WriteValue("options", "outputTemplate", "System.String," + Folder.Name + "\%VALUES")

            MessageBox.Show(Ini.ReadValue("options", "inputFolder"))
            MessageBox.Show(Ini.Path)

            Dim pHelp As New ProcessStartInfo
            pHelp.FileName = Bardecode
            pHelp.Arguments = Ini.Path
            pHelp.UseShellExecute = True
            pHelp.WindowStyle = ProcessWindowStyle.Normal
            Dim proc As Process = Process.Start(pHelp)

            proc.WaitForExit()
            proc.Close()

        Next
    End Sub

    Private Sub SplitFiles()

    End Sub

    Private Sub LogFolders()
        Me.ProcessLogText.AppendText("Processing folders" + vbCrLf)
        For Each Folder As DirectoryInfo In Me.Folders
            Me.ProcessLogText.AppendText(" - " + Folder.FullName + vbCrLf)
        Next
        Me.ProcessLogText.AppendText(vbCrLf)
    End Sub

    Private Function CheckFolders(Path As String) As Boolean
        Folders.Clear()

        For Each Folder In Directory.GetDirectories(Path + "\AsScanned")
            Dim FolderInfo As DirectoryInfo = New DirectoryInfo(Folder)
            If Not Directory.Exists(Path + "\ByFile\" + FolderInfo.Name) Then
                Me.Folders.Add(FolderInfo)
            End If
        Next
    End Function

    Private Sub ListFoldersAndFiles(Path As String)
        For Each File In Directory.GetFiles(Path)
            Dim FileInfo As FileInfo = New FileInfo(File)
            If Not (FileInfo.Attributes.HasFlag(System.IO.FileAttributes.Hidden)) Then
                Me.ProcessLogText.AppendText("    " + File + vbCrLf)
            End If
        Next

        For Each Folder In Directory.GetDirectories(Path)
            Dim FolderInfo As DirectoryInfo = New DirectoryInfo(Folder)
            If Not (FolderInfo.Attributes.HasFlag(System.IO.FileAttributes.Hidden)) Then
                Me.ProcessLogText.AppendText(Folder + vbCrLf)
                Me.ListFoldersAndFiles(Folder)
            End If
        Next
    End Sub
End Class
