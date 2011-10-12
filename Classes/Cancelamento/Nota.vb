Imports System.Xml.Serialization

Namespace Cancelamento

    Public Class Nota

        Private _id As String
        Private _inscricaoMunicipalPrestador As String
        Private _numeroNota As String
        Private _codigoVerificacao As String
        Private _motivoCancelamento As String

        <XmlAttributeAttribute()> _
        Public Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal valor As String)
                Me._id = String.Format("rps:{0}", valor)
            End Set
        End Property
        Public Property InscricaoMunicipalPrestador() As String
            Get
                Return Me._inscricaoMunicipalPrestador
            End Get
            Set(ByVal valor As String)
                Me._inscricaoMunicipalPrestador = valor
            End Set
        End Property
        Public Property NumeroNota() As String
            Get
                Return Me._numeroNota
            End Get
            Set(ByVal valor As String)
                Me._numeroNota = valor
            End Set
        End Property
        Public Property CodigoVerificacao() As String
            Get
                Return Me._codigoVerificacao
            End Get
            Set(ByVal valor As String)
                Me._codigoVerificacao = valor
            End Set
        End Property
        Public Property MotivoCancelamento() As String
            Get
                Return Me._motivoCancelamento
            End Get
            Set(ByVal valor As String)
                Me._motivoCancelamento = valor
            End Set
        End Property

    End Class

End Namespace