Imports System.Xml.Serialization

Namespace ConsultaNFSeRPS

    Public Class RPS

        Private _id As String
        Private _inscricaoMunicipalPrestador As String
        Private _numeroRPS As String
        Private _seriePrestacao As String

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
        Public Property NumeroRPS() As String
            Get
                Return Me._numeroRPS
            End Get
            Set(ByVal valor As String)
                Me._numeroRPS = valor
            End Set
        End Property
        Public Property SeriePrestacao() As String
            Get
                Return Me._seriePrestacao
            End Get
            Set(ByVal valor As String)
                Me._seriePrestacao = valor
            End Set
        End Property

    End Class

End Namespace