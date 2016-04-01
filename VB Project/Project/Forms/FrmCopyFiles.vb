Imports System.IO
Public Class FrmCopyFiles

    Public Sub New(ParentForm As System.Windows.Forms.IWin32Window)
        InitializeComponent()
        Me.ShowDialog(ParentForm)
    End Sub

    Private Sub btCopy_Click(sender As Object, e As EventArgs) Handles btCopy.Click
        ' go through the folder on the from path
        ' see if we have the same file in the processed path, if we have don't copy
        ' if we don't have should copy...

        Dim fromDir As DirectoryInfo = New DirectoryInfo(txFrom.Text)
        Dim toDocs As DirectoryInfo = New DirectoryInfo(txToDocs.Text)
        Dim toDrawings As DirectoryInfo = New DirectoryInfo(txToDrawings.Text)
        Dim ProcessedDir As DirectoryInfo = New DirectoryInfo(txProcessed.Text)

        txLog.Clear()

        For Each subdir In fromDir.GetDirectories()
            For Each subfile In subdir.GetFiles("*.pdf")
                ' if the file don't exits in the processed folder I should copy.
                If Not (File.Exists(ProcessedDir.FullName + "\" + subfile.Name.Replace(".pdf", "_OCRED.pdf"))) Then
                    CopyFile(subfile, toDocs, toDrawings)
                End If
            Next
        Next

    End Sub

    Private Sub CopyFile(FromFile As FileInfo, ToDocsFolder As DirectoryInfo, ToDrawingsFolder As DirectoryInfo)
        Dim FromBoxfolder As String = ToDocsFolder.FullName + "\" + FromFile.Directory.Name

        If Not Directory.Exists("") Then
            Directory.CreateDirectory("")
        End If
        File.Copy(FromFile.FullName, "")

        txLog.AppendText("Copying File: " + FromFile.FullName + vbCrLf)
        txLog.AppendText("      To File: " + "" + vbCrLf)

    End Sub
End Class