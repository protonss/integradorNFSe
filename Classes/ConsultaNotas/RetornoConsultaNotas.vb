Imports System.Xml.Serialization

Namespace ConsultaNotas

    <XmlRoot("RetornoConsultaNotas", Namespace:="http://localhost:8080/WsNFe2/lote")> _
    Public Class RetornoConsultaNotas
        Private _schemaLocation As String = "http://localhost:8080/WsNFe2/lote http://localhost:8080/WsNFe2/xsd/RetornoConsultaNotas.xsd"
        Private _cabecalho As Cabecalho
        Private _notas As List(Of Geral.RPS)
        <XmlAttributeAttribute(AttributeName:="schemaLocation", Namespace:="http://www.w3.org/2001/XMLSchema-instance")> _
        Public Property schemaLocation() As String
            Get
                Return Me._schemaLocation
            End Get
            Set(ByVal valor As String)
                Me._schemaLocation = valor
            End Set
        End Property
        <XmlElement("Cabecalho", Namespace:="")> _
        Public Property Cabecalho() As Cabecalho
            Get
                Return Me._cabecalho
            End Get
            Set(ByVal valor As Cabecalho)
                Me._cabecalho = valor
            End Set
        End Property
        <XmlArray("Notas", Namespace:=""), XmlArrayItem("Nota", Namespace:="")> _
        Public Property Notas() As List(Of Geral.RPS)
            Get
                Return Me._notas
            End Get
            Set(ByVal valor As List(Of Geral.RPS))
                Me._notas = valor
            End Set
        End Property
        Public Sub New()
            Cabecalho = New Cabecalho
            Notas = New List(Of Geral.RPS)
        End Sub

    End Class

End Namespace