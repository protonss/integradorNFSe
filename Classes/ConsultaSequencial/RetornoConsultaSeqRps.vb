Imports System.Xml.Serialization

Namespace ConsultaSequencial

    <XmlRoot("RetornoConsultaSeqRps", Namespace:="http://localhost:8080/WsNFe2/lote")> _
    Public Class RetornoConsultaSeqRps
        Private _schemaLocation As String = "http://localhost:8080/WsNFe2/lote http://localhost:8080/WsNFe2/xsd/RetornoConsultaSeqRps.xsd"
        Private _cabecalho As Cabecalho
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
        Public Sub New()
            Cabecalho = New Cabecalho
        End Sub

    End Class

End Namespace