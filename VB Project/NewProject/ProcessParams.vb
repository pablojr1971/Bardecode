Public NotInheritable Class ProcessParams
    Property JobNumber As String
    Property InpFolder As String
    Property DrwFolder As String
    Property OutFolder As String
    Property BWA4 As Boolean
    Property BWDrw As Boolean
    Property OCRA4 As Boolean
    Property OCRDrw As Boolean
    Property StopToFix As Boolean

    Property FStartRegex As String
    Property OutName As String
    Property FolderName As String
    Property FileSize As Integer
    Property Sections As List(Of SectionParams)
End Class
