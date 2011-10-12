Imports System.Xml.Serialization

Public Class Cabecalho

    Private _id As String
    Private _codCidade As String
    Private _sucesso As String
    Private _numeroLote As String
    Private _cpfCNPJRemetente As String
    Private _dataEnvioLote As String
    Private _qtdNotasProcessadas As String
    Private _tempoProcessamento As String
    Private _valorTotalServicos As String
    Private _valorTotalDeducoes As String
    Private _versao As String
    Private _assincrono As String

    'RetornoConsultaLote
    Private _razaoSocialRemetente As String

    'RetornoConsultaNotas
    Private _inscricaoMunicipalPrestador As String
    Private _dtInicio As String
    Private _dtFim As String
    Private _notaInicial As String

    'RetornoConsultaNFSeRPS
    Private _transacao As String

    'ReqEnvioLoteRPS
    Private _qtdRPS As String
    Private _metodoEnvio As String
    Private _versaoComponente As String
    <XmlAttributeAttribute()> _
       Public Property Id() As String
        Get
            Return Me._id
        End Get
        Set(ByVal valor As String)
            Me._id = valor
        End Set
    End Property
    Public Property CodCidade() As String
        Get
            Return Me._codCidade
        End Get
        Set(ByVal valor As String)
            Me._codCidade = valor
        End Set
    End Property
    Public Property Sucesso() As String
        Get
            Return Me._sucesso
        End Get
        Set(ByVal valor As String)
            Me._sucesso = valor
        End Set
    End Property
    Public Property DataEnvioLote() As String
        Get
            Return Me._dataEnvioLote
        End Get
        Set(ByVal valor As String)
            Me._dataEnvioLote = valor
        End Set
    End Property
    Public Property QtdNotasProcessadas() As String
        Get
            Return Me._qtdNotasProcessadas
        End Get
        Set(ByVal valor As String)
            Me._qtdNotasProcessadas = valor
        End Set
    End Property
    Public Property TempoProcessamento() As String
        Get
            Return Me._tempoProcessamento
        End Get
        Set(ByVal valor As String)
            Me._tempoProcessamento = valor
        End Set
    End Property
    Public Property CPFCNPJRemetente() As String
        Get
            Return Me._cpfCNPJRemetente
        End Get
        Set(ByVal valor As String)
            Me._cpfCNPJRemetente = valor
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
    Public Property Assincrono() As String
        Get
            Return Me._assincrono
        End Get
        Set(ByVal valor As String)
            Me._assincrono = valor
        End Set
    End Property
    Public Property RazaoSocialRemetente() As String
        Get
            Return Me._razaoSocialRemetente
        End Get
        Set(ByVal valor As String)
            Me._razaoSocialRemetente = valor
        End Set
    End Property
    <XmlElement("transacao")> _
    Public Property Transacao() As String
        Get
            Return Me._transacao
        End Get
        Set(ByVal valor As String)
            Me._transacao = valor
        End Set
    End Property
    Public Property dtInicio() As String
        Get
            Return Me._dtInicio
        End Get
        Set(ByVal valor As String)
            Me._dtInicio = valor
        End Set
    End Property
    Public Property dtFim() As String
        Get
            Return Me._dtFim
        End Get
        Set(ByVal valor As String)
            Me._dtFim = valor
        End Set
    End Property
    Public Property NotaInicial() As String
        Get
            Return Me._notaInicial
        End Get
        Set(ByVal valor As String)
            Me._notaInicial = valor
        End Set
    End Property
    Public Property QtdRPS() As String
        Get
            Return Me._QtdRPS
        End Get
        Set(ByVal valor As String)
            Me._QtdRPS = valor
        End Set
    End Property
    Public Property ValorTotalServicos() As String
        Get
            Return Me._valorTotalServicos
        End Get
        Set(ByVal valor As String)
            Me._valorTotalServicos = valor
        End Set
    End Property
    Public Property ValorTotalDeducoes() As String
        Get
            Return Me._valorTotalDeducoes
        End Get
        Set(ByVal valor As String)
            Me._valorTotalDeducoes = valor
        End Set
    End Property
    Public Property Versao() As String
        Get
            Return Me._versao
        End Get
        Set(ByVal valor As String)
            Me._versao = valor
        End Set
    End Property
    Public Property NumeroLote() As String
        Get
            Return Me._numeroLote
        End Get
        Set(ByVal valor As String)
            Me._numeroLote = valor
        End Set
    End Property
    Public Property MetodoEnvio() As String
        Get
            Return Me._metodoEnvio
        End Get
        Set(ByVal valor As String)
            Me._metodoEnvio = valor
        End Set
    End Property
    Public Property VersaoComponente() As String
        Get
            Return Me._versaoComponente
        End Get
        Set(ByVal valor As String)
            Me._versaoComponente = valor
        End Set
    End Property  
End Class