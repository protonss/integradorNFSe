Imports System.Xml.Serialization

Namespace Cancelamento

    '<?xml version="1.0" encoding="UTF-8"?>
    '<ns1:ReqCancelamentoNFSe xmlns:ns1="http://localhost:8080/WsNFe2/lote" xmlns:tipos="http://localhost:8080/WsNFe2/tp" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://localhost:8080/WsNFe2/lote http://localhost:8080/WsNFe2/xsd/ReqCancelamentoNFSe.xsd">
    '	<Cabecalho>
    '		<CodCidade>7145</CodCidade>
    '		<CPFCNPJRemetente>02852715000106</CPFCNPJRemetente>
    '		<transacao>true</transacao>
    '		<Versao>1</Versao>
    '	</Cabecalho>
    '	<Lote Id="lote:1ABCDZ">
    '		<Nota Id="nota:3">
    '			<InscricaoMunicipalPrestador>000132683</InscricaoMunicipalPrestador>
    '			<NumeroNota>3</NumeroNota>
    '			<CodigoVerificacao>98d0e61d9396b60e5a847a361cb9264e4b6d0c9f</CodigoVerificacao>
    '			<MotivoCancelamento>teste de cancelamento</MotivoCancelamento>
    '		</Nota>
    '	</Lote>
    '</ns1:ReqCancelamentoNFSe>

    <XmlRoot("ReqCancelamentoNFSe", Namespace:="http://localhost:8080/WsNFe2/lote")> _
    Public Class ReqCancelamentoNFSe
        Private _cabecalho As Cabecalho
        Private _lote As Cancelamento.Lote
        Private _schemaLocation As String = "http://localhost:8080/WsNFe2/lote http://localhost:8080/WsNFe2/xsd/ReqCancelamentoNFSe.xsd"
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
        Public Property Lote() As Cancelamento.Lote
            Get
                Return Me._lote
            End Get
            Set(ByVal valor As Cancelamento.Lote)
                Me._lote = valor
            End Set
        End Property

        Public Sub New()
            Cabecalho = New Cabecalho
            Lote = New Cancelamento.Lote
        End Sub
    End Class

End Namespace