Imports System.IO
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text
Imports System
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

' Static Class
Public NotInheritable Class Serializer

    Private Sub New()
        ' to prevent this classs to be instanciated
    End Sub

    Public Shared Function GetNamespaces() As XmlSerializerNamespaces
        Dim ns As XmlSerializerNamespaces
        ns = New XmlSerializerNamespaces()
        ns.Add("xs", "http://www.w3.org/2001/XMLSchema")
        ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Return ns
    End Function

    Public Shared ReadOnly Property TargetNamespace() As String
        Get
            Return "http://www.w3.org/2001/XMLSchema"
        End Get
    End Property

    Public Shared Function ToXml(ByVal Obj As Object, ByVal ObjType As System.Type) As String
        Dim xml As New System.Xml.Serialization.XmlSerializer(ObjType)
        Dim sw As New IO.StringWriter()
        xml.Serialize(sw, Obj)

        If sw IsNot Nothing Then
            Return sw.ToString()
        Else
            Return ""
        End If
    End Function

    Public Shared Function FromXml(ByVal Xml As String, ByVal ObjType As System.Type) As Object
        Dim ser As New System.Xml.Serialization.XmlSerializer(ObjType)
        Dim sr As New IO.StringReader(Xml)
        Dim obj As Object = ser.Deserialize(sr)
        Return obj
    End Function
End Class
