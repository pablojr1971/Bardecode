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
        Dim ser As XMLSerializer = New XMLSerializer(ObjType, TargetNamespace)
        Dim memStream As MemoryStream = New MemoryStream()
        Dim xmlWriter As XmlTextWriter = New XmlTextWriter(memStream, Encoding.UTF8)
        xmlWriter.Namespaces = True
        ser.Serialize(xmlWriter, Obj, GetNamespaces())
        xmlWriter.Close()
        memStream.Close()
        Dim xml As String
        xml = Encoding.UTF8.GetString(memStream.GetBuffer())
        xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)))
        xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1))
        Return xml
    End Function


    Public Shared Function FromXml(ByVal Xml As String, ByVal ObjType As System.Type) As Object
        Dim ser As XMLSerializer = New XMLSerializer(ObjType)
        Dim stringReader As StringReader = New StringReader(Xml)
        Dim xmlReader As XmlTextReader = New XmlTextReader(stringReader)
        Dim obj As Object
        obj = ser.Deserialize(xmlReader)
        xmlReader.Close()
        stringReader.Close()
        Return obj
    End Function
End Class
