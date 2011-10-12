﻿Imports System.Xml.Serialization
Namespace Envio

    <XmlRoot("RetornoEnvioLoteRPS", Namespace:="http://localhost:8080/WsNFe2/lote")> _
    Public Class RetornoEnvioLoteRPS
        Private _schemaLocation As String = "http://localhost:8080/WsNFe2/lote http://localhost:8080/WsNFe2/xsd/RetornoEnvioLoteRPS.xsd"
        Private _cabecalho As Cabecalho
        Private _alertas As List(Of Alerta)
        Private _erros As List(Of Erro)
        Private _chavesNFSeRPS As List(Of Geral.RPS)
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
        <XmlArray("Alertas", Namespace:=""), XmlArrayItem("Alerta", Namespace:="")> _
        Public Property Alertas() As List(Of Alerta)
            Get
                Return Me._alertas
            End Get
            Set(ByVal valor As List(Of Alerta))
                Me._alertas = valor
            End Set
        End Property
        <XmlArray("Erros", Namespace:=""), XmlArrayItem("Erro", Namespace:="")> _
        Public Property Erros() As List(Of Erro)
            Get
                Return Me._erros
            End Get
            Set(ByVal valor As List(Of Erro))
                Me._erros = valor
            End Set
        End Property
        <XmlArray("ChavesNFSeRPS", Namespace:=""), XmlArrayItem("ChavesNFSeRPS", Namespace:="")> _
        Public Property Notas() As List(Of Geral.RPS)
            Get
                Return Me._chavesNFSeRPS
            End Get
            Set(ByVal valor As List(Of Geral.RPS))
                Me._chavesNFSeRPS = valor
            End Set
        End Property
        Public Sub New()
            Cabecalho = New Cabecalho
            Alertas = New List(Of Alerta)
            Erros = New List(Of Erro)
        End Sub
    End Class

End Namespace