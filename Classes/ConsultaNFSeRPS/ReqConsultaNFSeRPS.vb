Imports System.Xml.Serialization

Namespace ConsultaNFSeRPS

    <XmlRoot("ReqConsultaNFSeRPS", Namespace:="http://localhost:8080/WsNFe2/lote")> _
    Public Class ReqConsultaNFSeRPS
        Private _cabecalho As Cabecalho
        Private _lote As ConsultaNFSeRPS.Lote
        Private _schemaLocation As String = "http://localhost:8080/WsNFe2/lote http://localhost:8080/WsNFe2/xsd/ReqConsultaNFSeRPS.xsd"
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
        Public Property Lote() As ConsultaNFSeRPS.Lote
            Get
                Return Me._lote
            End Get
            Set(ByVal valor As ConsultaNFSeRPS.Lote)
                Me._lote = valor
            End Set
        End Property
        Public Sub New()
            Cabecalho = New Cabecalho
            Lote = New Lote
        End Sub
    End Class

End Namespace