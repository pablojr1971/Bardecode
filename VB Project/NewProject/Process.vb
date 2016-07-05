Imports System.IO

Public Class Process
    Private Parameters As ProcessParams

    Public Sub New(ProcessId As Integer)
        'Get the parameters object from the database
        'leave the whole class ready to process
        Parameters = New ProcessParams()
        Parameters.Sections = New List(Of SectionParams)

        Using connection As SqlClient.SqlConnection = New SqlClient.SqlConnection(FrmMain.ScanDataStringConnection)
            connection.Open()

            Dim sql = "SELECT * FROM PROCESS " + _
                      " WHERE ID = " + ProcessId.ToString()

            Using command As SqlClient.SqlCommand = New SqlClient.SqlCommand(sql, connection)
                With command.ExecuteReader()
                    Parameters.JobNumber = .GetInt32(1)
                    Parameters.InpFolder = .GetString(3)
                    Parameters.DrwFolder = .GetString(4)
                    Parameters.OutFolder = .GetString(5)
                    Parameters.BWA4 = .GetBoolean(8)
                    Parameters.BWDrw = .GetBoolean(9)
                    Parameters.OCRA4 = .GetBoolean(6)
                    Parameters.OCRDrw = .GetBoolean(7)
                    Parameters.StopToFix = .GetBoolean(10)
                    Parameters.FStartRegex = .GetString(11)
                    Parameters.OutName = .GetString(12)
                    Parameters.FolderName = .GetString(13)
                End With
                command.Dispose()
            End Using

            sql = "SELECT * FROM SECTION " + _
                  " WHERE PROCESS = " + ProcessId.ToString()

            Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand(sql, connection)
                With Command.ExecuteReader()
                    While .Read()
                        Parameters.Sections.Add(New SectionParams())
                        Parameters.Sections.Last().SectionRegex = .GetString(3)
                        Parameters.Sections.Last().outName = .GetString(4)
                        Parameters.Sections.Last().FolderName = .GetString(5)
                        Parameters.Sections.Last().required = .GetBoolean(6)
                        Parameters.Sections.Last().oncePerFile = .GetBoolean(7)
                    End While
                End With
            End Using

            connection.Close()
        End Using
    End Sub

    Public Sub Run()
        Dim mainDir As DirectoryInfo = New DirectoryInfo(Parameters.InpFolder)
        For Each box In mainDir.GetDirectories()
            If IsProcessed(box.Name) Then
                Continue For
            End If

            Dim LogManagerObj As LogManager = New LogManager()
            Dim Params As Object() = {Parameters, box.Name, LogManagerObj}
            With New Threading.Thread(AddressOf threadTask)
                .Start(Params)
            End With
        Next
    End Sub

    Private Function IsProcessed(BoxFolderName As String) As Boolean
        Dim boxNumber As String = BoxFolderName.Substring(BoxFolderName.Length - 6)
        Using connection As SqlClient.SqlConnection = New SqlClient.SqlConnection(FrmMain.ScanDataStringConnection)
            connection.Open()

            Dim cmd As String = " SELECT COUNT(DISTINCT BOX) " + _
                                "   FROM SCANDATA " + _
                                "  WHERE JOBNO = " + Parameters.JobNumber.ToString() + _
                                "    AND (BOX = 'BOX" + boxNumber + "' OR BOX = '" + boxNumber + "') " + _
                                "    AND PAGECOUNT IS NULL AND DRAWINGCOUNT IS NULL"

            Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand(cmd, connection)
                Dim count As Integer = CInt(Command.ExecuteScalar())
                IsProcessed = (count = 0)
                Command.Dispose()
            End Using
            connection.Close()
        End Using
    End Function

    ' --------------- this will be the part of the file that will run the process actually -------------- '

    Private Shared Sub threadTask(ByVal parameters As Object)
        Dim ParamsObj As ProcessParams = parameters(0)
        Dim boxObj As String = parameters(1)
        Dim LogObj As LogManager = parameters(2)

        Try
            Dim Processing As Processing = New Processing(parameters(1), parameters(2), parameters(0))
            Processing.Run()
        Catch ex As Exception
            LogObj.WriteLine(ex.Message)
        End Try
    End Sub

End Class
