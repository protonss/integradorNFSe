Imports System.Xml.Serialization

Namespace Geral

    Public Class RPS

        Private _id As String

        Private _numeroNota As String
        Private _dataProcessamento As String
        Private _numeroLote As String
        Private _codigoVerificacao As String
        Private _inscricaoMunicipalPrestador As String
        Private _motivoCancelamento As String
        Private _InscricaoPrestador As String
        Private _numeroNFe As String

        Private _assinatura As String
        Private _razaoSocialPrestador As String
        Private _tipoRPS As String
        Private _serieRPS As String
        Private _numeroRPS As String
        Private _dataEmissaoRPS As String
        Private _situacaoRPS As String
        Private _serieRPSSubstituido As String
        Private _numeroRPSSubstituido As String
        Private _numeroNFSeSubstituida As String
        Private _dataEmissaoNFSeSubstituida As String
        Private _seriePrestacao As String
        Private _inscricaoMunicipalTomador As String
        Private _cpfCNPJTomador As String
        Private _razaoSocialTomador As String
        Private _docTomadorEstrangeiro As String
        Private _tipoLogradouroTomador As String
        Private _logradouroTomador As String
        Private _numeroEnderecoTomador As String
        Private _complementoEnderecoTomador As String
        Private _tipoBairroTomador As String
        Private _bairroTomador As String
        Private _cidadeTomador As String
        Private _cidadeTomadorDescricao As String
        Private _cepTomador As String
        Private _emailTomador As String
        Private _codigoAtividade As String
        Private _aliquotaAtividade As String
        Private _tipoRecolhimento As String
        Private _municipioPrestacao As String
        Private _municipioPrestacaoDescricao As String
        Private _operacao As String
        Private _tributacao As String
        Private _valorPIS As String
        Private _valorCOFINS As String
        Private _valorINSS As String
        Private _valorIR As String
        Private _valorCSLL As String
        Private _aliquotaPIS As String
        Private _aliquotaCOFINS As String
        Private _aliquotaINSS As String
        Private _aliquotaIR As String
        Private _aliquotaCSLL As String
        Private _descricaoRPS As String
        Private _dddPrestador As String
        Private _telefonePrestador As String
        Private _dddTomador As String
        Private _telefoneTomador As String
        Private _motCancelamento As String
        Private _cpfCnpjIntermediario As String
        Private _listaItemServicoRPS As List(Of Envio.ItemServicoRPS)
        Private _listaDeducaoRPS As List(Of Envio.DeducaoRPS)
        <XmlAttributeAttribute()> _
        Public Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal valor As String)
                Me._id = valor
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
        Public Property DataProcessamento() As String
            Get
                Return Me._dataProcessamento
            End Get
            Set(ByVal valor As String)
                Me._dataProcessamento = valor
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
        Public Property Assinatura() As String
            Get
                Return Me._assinatura
            End Get
            Set(ByVal valor As String)
                Me._assinatura = valor
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
        Public Property RazaoSocialPrestador() As String
            Get
                Return Me._razaoSocialPrestador
            End Get
            Set(ByVal valor As String)
                Me._razaoSocialPrestador = valor
            End Set
        End Property
        Public Property TipoRPS() As String
            Get
                Return Me._tipoRPS
            End Get
            Set(ByVal valor As String)
                Me._tipoRPS = valor
            End Set
        End Property
        Public Property SerieRPS() As String
            Get
                Return Me._serieRPS
            End Get
            Set(ByVal valor As String)
                Me._serieRPS = valor
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
        Public Property DataEmissaoRPS() As String
            Get
                Return Me._dataEmissaoRPS
            End Get
            Set(ByVal valor As String)
                Me._dataEmissaoRPS = valor
            End Set
        End Property
        Public Property SituacaoRPS() As String
            Get
                Return Me._situacaoRPS
            End Get
            Set(ByVal valor As String)
                Me._situacaoRPS = valor
            End Set
        End Property
        Public Property SerieRPSSubstituido() As String
            Get
                Return Me._serieRPSSubstituido
            End Get
            Set(ByVal valor As String)
                Me._serieRPSSubstituido = valor
            End Set
        End Property
        Public Property NumeroRPSSubstituido() As String
            Get
                Return Me._numeroRPSSubstituido
            End Get
            Set(ByVal valor As String)
                Me._numeroRPSSubstituido = valor
            End Set
        End Property
        Public Property NumeroNFSeSubstituida() As String
            Get
                Return Me._numeroNFSeSubstituida
            End Get
            Set(ByVal valor As String)
                Me._numeroNFSeSubstituida = valor
            End Set
        End Property
        Public Property DataEmissaoNFSeSubstituida() As String
            Get
                Return Me._dataEmissaoNFSeSubstituida
            End Get
            Set(ByVal valor As String)
                Me._dataEmissaoNFSeSubstituida = valor
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
        Public Property InscricaoMunicipalTomador() As String
            Get
                Return Me._inscricaoMunicipalTomador
            End Get
            Set(ByVal valor As String)
                Me._inscricaoMunicipalTomador = valor
            End Set
        End Property
        Public Property CPFCNPJTomador() As String
            Get
                Return Me._cpfCNPJTomador
            End Get
            Set(ByVal valor As String)
                Me._cpfCNPJTomador = valor
            End Set
        End Property
        Public Property RazaoSocialTomador() As String
            Get
                Return Me._razaoSocialTomador
            End Get
            Set(ByVal valor As String)
                Me._razaoSocialTomador = valor
            End Set
        End Property
        Public Property DocTomadorEstrangeiro() As String
            Get
                Return Me._docTomadorEstrangeiro
            End Get
            Set(ByVal valor As String)
                Me._docTomadorEstrangeiro = valor
            End Set
        End Property
        Public Property TipoLogradouroTomador() As String
            Get
                Return Me._tipoLogradouroTomador
            End Get
            Set(ByVal valor As String)
                Me._tipoLogradouroTomador = valor
            End Set
        End Property
        Public Property LogradouroTomador() As String
            Get
                Return Me._logradouroTomador
            End Get
            Set(ByVal valor As String)
                Me._logradouroTomador = valor
            End Set
        End Property
        Public Property NumeroEnderecoTomador() As String
            Get
                Return Me._numeroEnderecoTomador
            End Get
            Set(ByVal valor As String)
                Me._numeroEnderecoTomador = valor
            End Set
        End Property
        Public Property ComplementoEnderecoTomador() As String
            Get
                Return Me._complementoEnderecoTomador
            End Get
            Set(ByVal valor As String)
                Me._complementoEnderecoTomador = valor
            End Set
        End Property
        Public Property TipoBairroTomador() As String
            Get
                Return Me._tipoBairroTomador
            End Get
            Set(ByVal valor As String)
                Me._tipoBairroTomador = valor
            End Set
        End Property
        Public Property BairroTomador() As String
            Get
                Return Me._bairroTomador
            End Get
            Set(ByVal valor As String)
                Me._bairroTomador = valor
            End Set
        End Property
        Public Property CidadeTomador() As String
            Get
                Return Me._cidadeTomador
            End Get
            Set(ByVal valor As String)
                Me._cidadeTomador = valor
            End Set
        End Property
        Public Property CidadeTomadorDescricao() As String
            Get
                Return Me._cidadeTomadorDescricao
            End Get
            Set(ByVal valor As String)
                Me._cidadeTomadorDescricao = valor
            End Set
        End Property
        Public Property CEPTomador() As String
            Get
                Return Me._cepTomador
            End Get
            Set(ByVal valor As String)
                Me._cepTomador = valor
            End Set
        End Property
        Public Property EmailTomador() As String
            Get
                Return Me._emailTomador
            End Get
            Set(ByVal valor As String)
                Me._emailTomador = valor
            End Set
        End Property
        Public Property CodigoAtividade() As String
            Get
                Return Me._codigoAtividade
            End Get
            Set(ByVal valor As String)
                Me._codigoAtividade = valor
            End Set
        End Property
        Public Property AliquotaAtividade() As String
            Get
                Return Me._aliquotaAtividade
            End Get
            Set(ByVal valor As String)
                Me._aliquotaAtividade = valor
            End Set
        End Property
        Public Property TipoRecolhimento() As String
            Get
                Return Me._tipoRecolhimento
            End Get
            Set(ByVal valor As String)
                Me._tipoRecolhimento = valor
            End Set
        End Property
        Public Property MunicipioPrestacao() As String
            Get
                Return Me._municipioPrestacao
            End Get
            Set(ByVal valor As String)
                Me._municipioPrestacao = valor
            End Set
        End Property
        Public Property MunicipioPrestacaoDescricao() As String
            Get
                Return Me._municipioPrestacaoDescricao
            End Get
            Set(ByVal valor As String)
                Me._municipioPrestacaoDescricao = valor
            End Set
        End Property
        Public Property Operacao() As String
            Get
                Return Me._operacao
            End Get
            Set(ByVal valor As String)
                Me._operacao = valor
            End Set
        End Property
        Public Property Tributacao() As String
            Get
                Return Me._tributacao
            End Get
            Set(ByVal valor As String)
                Me._tributacao = valor
            End Set
        End Property
        Public Property ValorPIS() As String
            Get
                Return Me._valorPIS
            End Get
            Set(ByVal valor As String)
                Me._valorPIS = valor
            End Set
        End Property
        Public Property ValorCOFINS() As String
            Get
                Return Me._valorCOFINS
            End Get
            Set(ByVal valor As String)
                Me._valorCOFINS = valor
            End Set
        End Property
        Public Property ValorINSS() As String
            Get
                Return Me._valorINSS
            End Get
            Set(ByVal valor As String)
                Me._valorINSS = valor
            End Set
        End Property
        Public Property ValorIR() As String
            Get
                Return Me._valorIR
            End Get
            Set(ByVal valor As String)
                Me._valorIR = valor
            End Set
        End Property
        Public Property ValorCSLL() As String
            Get
                Return Me._valorCSLL
            End Get
            Set(ByVal valor As String)
                Me._valorCSLL = valor
            End Set
        End Property
        Public Property AliquotaPIS() As String
            Get
                Return Me._aliquotaPIS
            End Get
            Set(ByVal valor As String)
                Me._aliquotaPIS = valor
            End Set
        End Property
        Public Property AliquotaCOFINS() As String
            Get
                Return Me._aliquotaCOFINS
            End Get
            Set(ByVal valor As String)
                Me._aliquotaCOFINS = valor
            End Set
        End Property
        Public Property AliquotaINSS() As String
            Get
                Return Me._aliquotaINSS
            End Get
            Set(ByVal valor As String)
                Me._aliquotaINSS = valor
            End Set
        End Property
        Public Property AliquotaIR() As String
            Get
                Return Me._aliquotaIR
            End Get
            Set(ByVal valor As String)
                Me._aliquotaIR = valor
            End Set
        End Property
        Public Property AliquotaCSLL() As String
            Get
                Return Me._aliquotaCSLL
            End Get
            Set(ByVal valor As String)
                Me._aliquotaCSLL = valor
            End Set
        End Property
        Public Property DescricaoRPS() As String
            Get
                Return Me._descricaoRPS
            End Get
            Set(ByVal valor As String)
                Me._descricaoRPS = valor
            End Set
        End Property
        Public Property DDDPrestador() As String
            Get
                Return Me._dddPrestador
            End Get
            Set(ByVal valor As String)
                Me._dddPrestador = valor
            End Set
        End Property
        Public Property TelefonePrestador() As String
            Get
                Return Me._telefonePrestador
            End Get
            Set(ByVal valor As String)
                Me._telefonePrestador = valor
            End Set
        End Property
        Public Property DDDTomador() As String
            Get
                Return Me._dddTomador
            End Get
            Set(ByVal valor As String)
                Me._dddTomador = valor
            End Set
        End Property
        Public Property TelefoneTomador() As String
            Get
                Return Me._telefoneTomador
            End Get
            Set(ByVal valor As String)
                Me._telefoneTomador = valor
            End Set
        End Property
        Public Property MotCancelamento() As String
            Get
                Return Me._motCancelamento
            End Get
            Set(ByVal valor As String)
                Me._motCancelamento = valor
            End Set
        End Property
        Public Property CPFCNPJIntermediario() As String
            Get
                Return Me._cpfCnpjIntermediario
            End Get
            Set(ByVal valor As String)
                Me._cpfCnpjIntermediario = valor
            End Set
        End Property
        <XmlArray("Deducoes"), _
        XmlArrayItem("Deducao")> _
        Public Property listaDeducaoRPS() As List(Of Envio.DeducaoRPS)
            Get
                Return Me._listaDeducaoRPS
            End Get
            Set(ByVal valor As List(Of Envio.DeducaoRPS))
                Me._listaDeducaoRPS = valor
            End Set
        End Property
        <XmlArray("Itens"), _
        XmlArrayItem("Item")> _
        Public Property listaItemServicoRPS() As List(Of Envio.ItemServicoRPS)
            Get
                Return Me._listaItemServicoRPS
            End Get
            Set(ByVal valor As List(Of Envio.ItemServicoRPS))
                Me._listaItemServicoRPS = valor
            End Set
        End Property
        Public Property InscricaoPrestador() As String
            Get
                Return Me._InscricaoPrestador
            End Get
            Set(ByVal valor As String)
                Me._InscricaoPrestador = valor
            End Set
        End Property
        Public Property NumeroNFe() As String
            Get
                Return Me._numeroNFe
            End Get
            Set(ByVal valor As String)
                Me._numeroNFe = valor
            End Set
        End Property
        Public Sub New()
            listaDeducaoRPS = New List(Of Envio.DeducaoRPS)
            listaItemServicoRPS = New List(Of Envio.ItemServicoRPS)
        End Sub

    End Class

End Namespace