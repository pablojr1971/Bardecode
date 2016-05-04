Imports System.IO
Imports System.Data.SqlClient


Public Class Form1
    Public Const ScanDataStringConnection = "Server=Jerry;Database=other;User Id=sa;Password=569874123;"

    Private Sub Process_Click(sender As Object, e As EventArgs) Handles Process.Click
        Dim Database As SqlConnection = New SqlConnection(ScanDataStringConnection)
        Database.Open()

        Dim List As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim table = New DataTable()
        Using da = New SqlDataAdapter("SELECT C.COLUMN_NAME Colum, C.DATA_TYPE ColumType" + _
                                      "  FROM INFORMATION_SCHEMA.TABLES T " + _
                                      " INNER JOIN INFORMATION_SCHEMA.COLUMNS C ON (C.TABLE_NAME = T.TABLE_NAME) " + _
                                      " WHERE T.TABLE_NAME = 'SCANDATA'", Database)
            da.Fill(table)
        End Using

        Dim data = From row In table
                   Let Colum = row.Field(Of String)("Colum")
                   Let ColumType = row.Field(Of String)("ColumType")
                   Select Colum, ColumType
                   Order By Colum

        For Each line In RichTextBox1.Lines()
            Dim Field As String = line.Split("=")(0)
            Dim value As String = line.Split("=")(1)

            If data.Count(Function(a) a.Colum = Field) > 0 Then
                List.Add(Field, value)
            Else
                Throw New Exception("Problemn")
                Exit Sub
            End If
        Next

        Dim InsertBottom As String = ""
        Dim InsertHeader As String = "Insert into Scandata ("
        For Each field In List
            InsertHeader += field.Key + ","
        Next
        InsertHeader += ")"
        InsertHeader = InsertHeader.Replace(",)", ")")

        Dim csvFile As String() = File.ReadAllLines(TextBox1.Text)
        Dim CsvHeader As String() = csvFile(0).Split(";")
        Dim SLine As String() = Nothing

        Dim transaction = Database.BeginTransaction()
        Dim c As Integer = 0

        Try

            For Index = 1 To csvFile.Count - 1
                SLine = csvFile(Index).Split(";")

                InsertBottom = " VALUES("
                For Each field In List
                    If CsvHeader.Contains(field.Value) Then
                        If data.Single(Function(p) p.Colum = field.Key).ColumType = "nvarchar" Then
                            InsertBottom += "'" + SLine(CsvHeader.ToList().IndexOf(field.Value)) + "',"
                        ElseIf data.Single(Function(p) p.Colum = field.Key).ColumType = "int" Then
                            InsertBottom += SLine(CsvHeader.ToList().IndexOf(field.Value)) + ","
                        ElseIf data.Single(Function(p) p.Colum = field.Key).ColumType = "datetime" Then
                            InsertBottom += "CONVERT(datetime, '" + SLine(CsvHeader.ToList().IndexOf(field.Value)).Replace("/", "-") + "', 105),"
                        End If
                    Else
                        If data.Single(Function(p) p.Colum = field.Key).ColumType = "nvarchar" Then
                            InsertBottom += "'" + field.Value + "',"
                        ElseIf data.Single(Function(p) p.Colum = field.Key).ColumType = "int" Then
                            InsertBottom += field.Value + ","
                        ElseIf data.Single(Function(p) p.Colum = field.Key).ColumType = "datetime" Then
                            InsertBottom += "CONVERT(datetime, '" + field.Value.Replace("/", ",") + "', 105),"
                        End If
                    End If
                Next
                InsertBottom += ")"
                InsertBottom = InsertBottom.Replace(",)", ")")

                Using command As New SqlCommand(InsertHeader + " " + InsertBottom, Database, transaction)
                    command.ExecuteNonQuery()
                End Using
                c += 1
            Next
            transaction.Commit()
            MessageBox.Show("Done")
        Catch ex As Exception
            transaction.Rollback()
            MessageBox.Show(ex.Message + c.ToString + vbCrLf + InsertBottom)
        End Try
    End Sub
End Class