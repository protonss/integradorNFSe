Imports System.Xml.Serialization

Namespace ConsultaLote

    '<?xml version="1.0" encoding="UTF-8"?>
    '<ns1:ReqConsultaLote xmlns:ns1="http://localhost:8080/WsNFe2/lote" xmlns:tipos="http://localhost:8080/WsNFe2/tp" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://localhost:8080/WsNFe2/lote  http://localhost:8080/WsNFe2/xsd/ReqConsultaLote.xsd">
    '	<Cabecalho>
    '		<CodCidade>7145</CodCidade>
    '		<CPFCNPJRemetente>02852715000106</CPFCNPJRemetente>
    '		<Versao>1</Versao>
    '		<NumeroLote>190619</NumeroLote>
    '	</Cabecalho>
    '</ns1:ReqConsultaLote>

    <XmlRoot("ReqConsultaLote", Namespace:="http://localhost:8080/WsNFe2/lote")> _
    Public Class ReqConsultaLote
        Private _cabecalho As Cabecalho
        Private _schemaLocation As String = "http://localhost:8080/WsNFe2/lote http://localhost:8080/WsNFe2/xsd/ReqConsultaLote.xsd"
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