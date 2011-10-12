Imports System
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Namespace Serializacao

    Public Class MemorySerializer

#Region "Membros Privados Estaticos"
        Private Shared _encoding As Encoding = Encoding.UTF8
#End Region

#Region "Membros Publicos"

        Public Property Encoding() As Encoding
            Get
                Return _encoding
            End Get
            Set(ByVal value As Encoding)
                _encoding = value
            End Set
        End Property

#End Region

#Region "Metodos Publicos"

        Public Shared Function Serialize(ByVal source As Object) As String

            Dim content As String
            Dim namespaces As XmlSerializerNamespaces = New XmlSerializerNamespaces()
            namespaces.Add("ns1", "http://localhost:8080/WsNFe2/lote")
            namespaces.Add("tipos", "http://localhost:8080/WsNFe2/tp")
            namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")

            Using stream As New MemoryStream()
                Dim xmlSerializer As New XmlSerializer(source.GetType())
                Using writer As New XmlTextWriter(stream, _encoding)
                    xmlSerializer.Serialize(writer, source, namespaces)
                    writer.Flush()
                    Using reader As New StreamReader(stream, _encoding)
                        stream.Position = 0
                        content = reader.ReadToEnd()
                    End Using
                End Using
            End Using

            Return content

        End Function

        Public Shared Function Deserialize(ByVal content As String, ByVal type As Type) As Object

            Dim source As Object

            Using stream As New MemoryStream()
                Using writer As New StreamWriter(stream, _encoding)
                    writer.Write(content)
                    writer.Flush()
                    Dim xmlSerializer As New XmlSerializer(type)
                    stream.Position = 0
                    source = xmlSerializer.Deserialize(stream)
                End Using
            End Using

            Return source

        End Function

#End Region

    End Class
End Namespace
