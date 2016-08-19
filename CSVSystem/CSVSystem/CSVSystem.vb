Imports System.IO

Module Module1
    ' Creating the object that will get the files and subdirectories from the main directory
    Dim MainDirectory As DirectoryInfo = New DirectoryInfo("C:\Processing VB\Wolverhampton\Out")
    ' Creating the Object that will create the outputCSV file
    Dim outputCSV As StreamWriter = Nothing
    ' Creating the Database connection using a ConnectionString
    Dim DatabaseConnection As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection("Server=jerry;Database=other;User Id=sa;Password=569874123")

    Sub Main()
        ' Opening the database connection
        DatabaseConnection.Open()

        outputCSV = New StreamWriter(Path.Combine(MainDirectory.FullName, "Index.csv"))
        outputCSV.WriteLine("""FilePath"",""DatabaseField1"",""DatabaseField5""")

        CheckFolderRecursive(MainDirectory)

        outputCSV.Flush()
        outputCSV.Close()
        outputCSV.Dispose()

        DatabaseConnection.Close()
    End Sub

    Public Sub CheckFolderRecursive(CurrentFolder As DirectoryInfo)
        ' Will check if this folder has subdirectories and if it has will call this Sub again with the subdirectory as a parameter
        For Each subfolder In CurrentFolder.GetDirectories()
            CheckFolderRecursive(subfolder)
        Next

        ' Will check if this folder has files and Add these files to the document (.csv)
        For Each subfile In CurrentFolder.GetFiles("*.pdf")
            AddToDocument(subfile)
        Next

    End Sub

    Public Sub AddToDocument(subfile As FileInfo)

        ' Creating the commmand object that will retrieve information from the database
        Dim Command As System.Data.SqlClient.SqlCommand = New SqlClient.SqlCommand()
        Command.Connection = DatabaseConnection
        Command.CommandText = String.Format("SELECT ISNULL(FIELD1, ''), ISNULL(FIELD5, '') FROM SCANDATA WHERE BARCODE = '{0}'", getFileBarcode(subfile))

        With Command.ExecuteReader()
            If .Read() Then
                outputCSV.WriteLine(String.Format("""{0}"",""{1}"",""{2}""", subfile.FullName.Replace(MainDirectory.FullName, ""), .GetString(0), .GetString(1)))
            End If

            .Close()
        End With

        Command.Dispose()
    End Sub

    Function getFileBarcode(subfile As FileInfo) As String
        ' Play with the file name to get the barcode 
        Return subfile.Name.Substring(0, 10)
        'subfile.Name.IndexOf("-")
        'subfile.Name.Replace("-", "")
    End Function

End Module
