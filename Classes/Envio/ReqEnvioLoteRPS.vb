Imports System.Xml.Serialization

Namespace Envio

    <XmlRoot("ReqEnvioLoteRPS", Namespace:="http://localhost:8080/WsNFe2/lote")> _
    Public Class ReqEnvioLoteRPS
        Private _cabecalho As Cabecalho
        Private _lote As Envio.Lote
        Private _schemaLocation As String = "http://localhost:8080/WsNFe2/lote http://localhost:8080/WsNFe2/xsd/ReqEnvioLoteRPS.xsd"
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
        <XmlElement("Lote", Namespace:="")> _
        Public Property Lote() As Envio.Lote
            Get
                Return Me._lote
            End Get
            Set(ByVal valor As Envio.Lote)
                Me._lote = valor
            End Set
        End Property

        Public Sub New()
            Cabecalho = New Cabecalho
            Lote = New Envio.Lote
        End Sub
    End Class

End Namespace