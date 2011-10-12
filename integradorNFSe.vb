'**
'* ------------- PROTONSS4FUN SERVIÇOS DE INFORMÁTICA LTDA
'*
'* CLASSE      : SmkNFSe
'* ANALISTA    : Márcio Rosa
'* PROGRAMADOR : Márcio Rosa
'* DATA CRIACAO: 04/04/2011
'* DATA ALTER. :
'* COMENTARIOS : Comunicação com WebService da Nota Fiscal de Serviço Eletrônica
'*
'**

Option Explicit On
Imports System
Imports System.Security.Cryptography
Imports System.Security.Cryptography.Xml
Imports System.Security.Cryptography.X509Certificates
Imports System.Xml
Imports System.Net
Imports System.Xml.Schema
Imports System.Text
Imports System.Reflection

<ComClass(integradorNFSe.ClassId, integradorNFSe.InterfaceId, integradorNFSe.EventsId)> Public Class integradorNFSe

#Region "Variaveis e Constantes"

    Protected Const OK = 0
    Protected Const ERRO = -1
    Protected Const CANCEL = -2

    Protected _objProxy As New Object
    Protected _objProxyCredentials As New Object
    Protected _reqEnvioLoteRPS As Envio.ReqEnvioLoteRPS
    Protected _reqConsultaNFSeRPS As ConsultaNFSeRPS.ReqConsultaNFSeRPS
    Protected _webService As LoteRpsServiceHomol.LoteRpsService
    Protected _bRPS As Boolean = False
    Protected _rpsIdAtual As Integer = 1
    Protected _rpsIndexAtual As Integer = 0
    Protected _erros As List(Of Erro) = New List(Of Erro)
    Protected _alertas As List(Of Alerta) = New List(Of Alerta)
    Protected _notasCanceladas As List(Of Geral.RPS) = New List(Of Geral.RPS)()
    Protected _notasProcessadas As List(Of Geral.RPS) = New List(Of Geral.RPS)()
    Protected _notasConsultadas As List(Of Geral.RPS) = New List(Of Geral.RPS)()
    Protected _nroUltimoRps As String = String.Empty
    Protected _assincrono As String = String.Empty
    Protected _sucesso As String = String.Empty
    Protected _numeroDoLoteGerado As String = String.Empty
    Protected _cUltMens As String = String.Empty
    Protected _cHTTPWASDL As String = String.Empty
    Protected _cPathCertificado As String = String.Empty
    Protected _bProxyInformado As Boolean = False

#End Region
    Private Property ReqEnvioLoteRPS() As Envio.ReqEnvioLoteRPS
        Get
            Return Me._reqEnvioLoteRPS
        End Get
        Set(ByVal valor As Envio.ReqEnvioLoteRPS)
            Me._reqEnvioLoteRPS = valor
        End Set
    End Property
    Private Property ReqConsultaNFSeRPS() As ConsultaNFSeRPS.ReqConsultaNFSeRPS
        Get
            Return Me._reqConsultaNFSeRPS
        End Get
        Set(ByVal valor As ConsultaNFSeRPS.ReqConsultaNFSeRPS)
            Me._reqConsultaNFSeRPS = valor
        End Set
    End Property
    Private Property WebService() As LoteRpsServiceHomol.LoteRpsService
        Get
            Return Me._webService
        End Get
        Set(ByVal valor As LoteRpsServiceHomol.LoteRpsService)
            Me._webService = valor
        End Set
    End Property
    Private Sub LimparVariaveis()
        _rpsIdAtual = 1
        _rpsIndexAtual = 0
        _erros = New List(Of Erro)
        _alertas = New List(Of Alerta)
        _notasCanceladas = New List(Of Geral.RPS)
        _notasProcessadas = New List(Of Geral.RPS)
        _notasConsultadas = New List(Of Geral.RPS)
        _bRPS = False
        _nroUltimoRps = String.Empty
        _assincrono = String.Empty
        _sucesso = String.Empty
        _numeroDoLoteGerado = String.Empty
        _cUltMens = String.Empty
    End Sub
    Sub New()
        ReqEnvioLoteRPS = New Envio.ReqEnvioLoteRPS()
        ReqConsultaNFSeRPS = New ConsultaNFSeRPS.ReqConsultaNFSeRPS()
        WebService = New LoteRpsServiceHomol.LoteRpsService
    End Sub
    Dim _xmlRetorno As String = String.Empty
    Dim _xmlGerado As String = String.Empty
#Region "COM GUIDs"

    Public Const ClassId As String = "c96a4bd3-8a2b-4a3d-9975-76316c54d942"
    Public Const InterfaceId As String = "b239dc0c-119e-40d3-bfcc-add3c4598f35"
    Public Const EventsId As String = "db6ba341-00d5-4977-991e-6e15c4e259be"

#End Region

#Region "ConfigProxy"

    ''' <summary> 
    ''' Configuração de acesso a internet para redes com proxy
    ''' Linguagem: VB.NET 
    ''' Framework: 2.0 
    ''' </summary> 
    ''' <param name="servidor"> IP do servidor de acesso a internet </param> 
    ''' <param name="porta"> Porta do servidor de acesso a internet </param> 
    ''' <param name="usuario"> Usuário do proxy </param> 
    ''' <param name="senha"> Senha do usuário do proxy </param> 
    Public Sub ConfigProxy(ByVal servidor As String, _
                           ByVal porta As Integer, _
                           ByVal usuario As String, _
                           ByVal senha As String)

        If String.IsNullOrEmpty(servidor) = False And _
           String.IsNullOrEmpty(porta) = False And _
           String.IsNullOrEmpty(usuario) = False And _
           String.IsNullOrEmpty(senha) = False Then

            _objProxy = New WebProxy(servidor, porta)
            _objProxyCredentials = New NetworkCredential(usuario, senha)
            _objProxy.Credentials = _objProxyCredentials

            _bProxyInformado = True

        Else

            _bProxyInformado = False

        End If

        'Exemplo
        'ObjNfeConsultaMT.Proxy = New WebProxy("10.1.0.253", 8080)
        'ObjNfeConsultaMT.Proxy.Credentials = New NetworkCredential("tr_smknfe", "*****")

    End Sub

#End Region

#Region "Assinatura Digital"

    Private Function ObterCertificadoRepositorio(ByVal sCertificadoSubject As String)

        Dim oCert As New X509Certificate2
        Dim oRepositorio As New X509Store
        Dim oRepositorioLocalMachine As New X509Store
        Dim bLocalizadoCert As Boolean = False

        oRepositorio = New X509Store("My", StoreLocation.CurrentUser)
        oRepositorio.Open(OpenFlags.ReadOnly & OpenFlags.OpenExistingOnly)

        'Todos Certificados do Usuário Corrente
        Dim oCertCollection As New X509Certificate2Collection
        oCertCollection = oRepositorio.Certificates

        Dim oCertTemp As New X509Certificate2

        For Each oCertTemp In oCertCollection

            If oCertTemp.Subject = sCertificadoSubject Then
                oCert = oCertTemp
                bLocalizadoCert = True
                Exit For
            End If

        Next

        If bLocalizadoCert = False Then

            oRepositorioLocalMachine = New X509Store("My", StoreLocation.LocalMachine)
            oRepositorioLocalMachine.Open(OpenFlags.ReadOnly & OpenFlags.OpenExistingOnly)

            Dim oCertCollectionLocalMachine As New X509Certificate2Collection
            oCertCollectionLocalMachine = oRepositorioLocalMachine.Certificates

            Dim oCertTempLocalMachine As New X509Certificate2

            For Each oCertTempLocalMachine In oCertCollectionLocalMachine
                If oCertTempLocalMachine.Subject = sCertificadoSubject Then
                    oCert = oCertTempLocalMachine
                    bLocalizadoCert = True
                    Exit For
                End If
            Next

            If bLocalizadoCert = False Then
                Err.Raise("-20000", "obterCertificadoRepositorio", "Certificado não localizado no Repositório local do Servidor!" & vbNewLine & "Certificado: " & sCertificadoSubject)
                ObterCertificadoRepositorio = Nothing
                Exit Function
            Else
                ObterCertificadoRepositorio = oCert
            End If

        Else
            ObterCertificadoRepositorio = oCert
        End If

    End Function

    ''' <summary> 
    ''' Assina os elementos de um documento XML com um certificado digital. 
    ''' Linguagem: VB.NET 
    ''' Framework: 2.0 
    ''' </summary> 
    ''' <param name="xml"> O documento que contem os elementos que devem ser assinados </param> 
    ''' <param name="ParentElementName">O elemento que contem as tags a serem assinadas 
    ''' Ex.:  
    ''' Para assinar o rps em um lote o ParentElementName = "RPS" 
    ''' Para assinar o pedido do lote de rps o ParentElementName = "ns1:ReqEnvioLoteRPS" 
    ''' </param> 
    ''' <param name="ElementName">O elemento (tag) que será assinado 
    ''' Ex.:  
    ''' Para assinar o rps em um lote o ElementName = "InfRps" 
    ''' ''' Para assinar o pedido do lote de rps o ElementName = "Lote" 
    ''' </param> 
    ''' <param name="AttributeName"> 
    ''' O nome do atributo do elemento que será assinado 
    ''' Obs.: 
    ''' Por padrão o NOME do atributo possui a mesma identificação. 
    ''' AtributeName = "Id" 
    ''' </param> 
    Public Sub AssinarElementosXML(ByVal xml As XmlDocument, _
                                   ByVal ParentElementName As String, _
                                   ByVal ElementName As String, _
                                   ByVal AttributeName As String)

        Dim elemento As XmlElement
        Dim elementoInf As XmlElement
        Dim elementoInfId As String
        Dim elSigned As SignedXml
        Dim Key As RSACryptoServiceProvider
        Dim keyInfo As New KeyInfo

        'Declara variável de certificado com conteúdo do Certificado de Transmissão
        Dim oCertificado As X509Certificate = X509Certificate.CreateFromCertFile(_cPathCertificado)
        Dim oCertificadoRepositorio As New X509Certificate2
        oCertificadoRepositorio = ObterCertificadoRepositorio(oCertificado.Subject)

        If oCertificadoRepositorio.Subject = "" Then
            Err.Raise("-20000", "AssinarElementos", "Certificado Digital não encontrado")
            Exit Sub
        End If

        If oCertificadoRepositorio.HasPrivateKey = False Then
            Err.Raise("-20000", "AssinarElementos", "Certificado Digital deve possuir chave privada")
            Exit Sub
        End If

        'Retira chave privada ligada ao certificado 
        Key = CType(oCertificadoRepositorio.PrivateKey, RSACryptoServiceProvider)
        'Adiciona Certificado ao Key Info 
        keyInfo.AddClause(New KeyInfoX509Data(oCertificadoRepositorio))

        For Each elemento In xml.GetElementsByTagName(ParentElementName)

            elementoInf = CType(elemento.GetElementsByTagName(ElementName)(elemento.GetElementsByTagName(ElementName).Count - 1), XmlElement)
            elementoInfId = elementoInf.Attributes.GetNamedItem(AttributeName).Value
            elSigned = New SignedXml(elementoInf)

            'Seta chaves 
            elSigned.SigningKey = Key
            elSigned.KeyInfo = keyInfo

            ' Cria referencia 
            Dim reference As New Reference()
            reference.Uri = "#" & elementoInfId

            ' Adiciona tranformacao a referencia 
            reference.AddTransform(New XmlDsigEnvelopedSignatureTransform())
            reference.AddTransform(New XmlDsigC14NTransform(False))

            ' Adiciona referencia ao xml 
            elSigned.AddReference(reference)

            ' Calcula Assinatura 
            elSigned.ComputeSignature()

            'Adiciona assinatura 
            elemento.AppendChild(xml.ImportNode(elSigned.GetXml(), True))

        Next

    End Sub
    '''<summary> 
    ''' Retorna assinatura padrão SHA1 de acordo com o parâmetro passado baseado em certificado digital.
    ''' Linguagem: VB.NET 
    ''' Framework: 2.0 
    ''' </summary> 
    ''' <param name="stringAssinatura"> String de dados concatenados para assinatura da tag RPS</param> 
    Private Function GeraAssinaturaHash(ByVal stringAssinatura As String) As String

        'Dim InscricaoMunicipalContribuinte As String = "00000317330"
        'Dim SerieRPS As String = "NF"
        'Dim NumeroRPS As String = "000000038663"
        'Dim DataEmissaoRPS As String = "20090905"
        'Dim Tributacao As String = "T"
        'Dim SituacaoRPS As String = "N"
        'Dim TipoRecolhimento As String = "N"
        'Dim ValorServicoSubtraindoDeducao As String = "000000000001686"
        'Dim ValorDeducao As String = "000000000000000"
        'Dim CodigoAtividade As String = "0829979900"
        'Dim CPFCNPJTomador As String = "08764130000102"

        Dim AssinaturaByte As Byte() = System.Text.Encoding.Default.GetBytes(stringAssinatura)
        Dim CriptografarSHA1 As New SHA1Managed()
        Dim AssinaturaHashSHA1Byte As Byte() = CriptografarSHA1.ComputeHash(AssinaturaByte)
        Dim AssinaturaHashSHA1 As String = String.Empty

        For i As Integer = 0 To AssinaturaHashSHA1Byte.Length - 1

            'Convertendo de Byte para string no formato Hexadecimal (x2)
            AssinaturaHashSHA1 = AssinaturaHashSHA1 & AssinaturaHashSHA1Byte(i).ToString("x2")

        Next

        Return AssinaturaHashSHA1

    End Function

#End Region

    ''' <summary> 
    ''' Cancela o documento XML
    ''' Linguagem: VB.NET 
    ''' Framework: 2.0 
    ''' </summary> 
    ''' <param name="CodCidade">Código da cidade da declaração padrão SIAFI.</param> 
    ''' <param name="CPFCNPJRemetente">CPF /CNPJ do remetente autorizado a transmitir o Comum.RPS</param> 
    ''' <param name="InscricaoMunicipalPrestador">Inscrição Municipal do Prestador</param> 
    ''' <param name="NumeroNota">Número da Nota Fiscal</param> 
    ''' <param name="CodigoVerificacao">Código de Verificação Indentificador da NFSe</param> 
    ''' <param name="MotivoCancelamento">Motivo do cancelamento</param> 
    Public Function Cancelar(ByVal CodCidade As String, _
                             ByVal CPFCNPJRemetente As String, _
                             ByVal InscricaoMunicipalPrestador As String, _
                             ByVal NumeroNota As String, _
                             ByVal CodigoVerificacao As String, _
                             ByVal MotivoCancelamento As String) As Integer

        Cancelar = ERRO

        Try

            'Seta a url do webservice
            If String.IsNullOrEmpty(_cHTTPWASDL) = False Then
                WebService.Url = _cHTTPWASDL
            End If

            'Seta as configurações do proxy
            If _bProxyInformado = True Then
                WebService.Proxy = _objProxy
            End If

            Dim ReqCancelamentoNFSe As New Cancelamento.ReqCancelamentoNFSe

            ReqCancelamentoNFSe.Cabecalho.CodCidade = CodCidade
            ReqCancelamentoNFSe.Cabecalho.CPFCNPJRemetente = CPFCNPJRemetente
            ReqCancelamentoNFSe.Cabecalho.Transacao = "true"
            ReqCancelamentoNFSe.Cabecalho.Versao = "1"

            Dim nota As New Cancelamento.Nota

            ReqCancelamentoNFSe.Lote.Id = String.Format("lote:{0}ABCDZ", 1)

            nota.Id = NumeroNota
            nota.InscricaoMunicipalPrestador = InscricaoMunicipalPrestador
            nota.NumeroNota = NumeroNota
            nota.CodigoVerificacao = CodigoVerificacao
            nota.MotivoCancelamento = MotivoCancelamento

            ReqCancelamentoNFSe.Lote.Nota.Add(nota)

            Dim xmlReqCancelamentoNFSe As String = Serializacao.MemorySerializer.Serialize(ReqCancelamentoNFSe)

            Dim xmlReq As New XmlDocument
            xmlReq.LoadXml(xmlReqCancelamentoNFSe)
            xmlReq.PreserveWhitespace = False

            _xmlGerado = xmlReq.OuterXml

            Dim oCertificado As X509Certificate = X509Certificate.CreateFromCertFile(_cPathCertificado)
            WebService.ClientCertificates.Add(oCertificado)
            oCertificado = Nothing

            _xmlRetorno = WebService.cancelar(xmlReq.OuterXml)

            Try

                Dim retornoSerializado As Cancelamento.RetornoCancelamentoNFSe = Serializacao.MemorySerializer.Deserialize(_xmlRetorno, GetType(Cancelamento.RetornoCancelamentoNFSe))

                LimparVariaveis()

                _sucesso = retornoSerializado.Cabecalho.Sucesso
                _erros = retornoSerializado.Erros
                _alertas = retornoSerializado.Alertas
                _notasCanceladas = retornoSerializado.NotasCanceladas

            Catch ex As Exception
                _cUltMens = "Cancelar lançou uma exceção: " + _xmlRetorno
                Exit Function
            Finally
                xmlReq = Nothing
            End Try

            xmlReq = Nothing

            Cancelar = OK

        Catch ex As Exception
            _cUltMens = "Cancelar lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function

    Public Function ConsultarNFSeRps(ByVal Assintatura As String) As Integer

        ConsultarNFSeRps = ERRO

        Try

            'Seta a url do webservice
            If String.IsNullOrEmpty(_cHTTPWASDL) = False Then
                WebService.Url = _cHTTPWASDL
            End If

            'Seta as configurações do proxy
            If _bProxyInformado = True Then
                WebService.Proxy = _objProxy
            End If

            Dim xmlReqConsultaNFSeRPS As String = Serializacao.MemorySerializer.Serialize(ReqConsultaNFSeRPS)

            Dim xmlReq As New XmlDocument
            xmlReq.LoadXml(xmlReqConsultaNFSeRPS)
            xmlReq.PreserveWhitespace = False

            _xmlGerado = xmlReq.OuterXml

            Dim oCertificado As X509Certificate = X509Certificate.CreateFromCertFile(_cPathCertificado)
            WebService.ClientCertificates.Add(oCertificado)
            oCertificado = Nothing

            _xmlRetorno = WebService.consultarNFSeRps(xmlReq.OuterXml)

            Try

                Dim retornoSerializado As ConsultaNFSeRPS.RetornoConsultaNFSeRPS = Serializacao.MemorySerializer.Deserialize(_xmlRetorno, GetType(ConsultaNFSeRPS.RetornoConsultaNFSeRPS))

                LimparVariaveis()

                _sucesso = retornoSerializado.Cabecalho.Sucesso
                _erros = retornoSerializado.Erros
                _alertas = retornoSerializado.Alertas
                _notasConsultadas = retornoSerializado.NotasConsultadas

                retornoSerializado = Nothing

            Catch ex As Exception
                _cUltMens = "ConsultarNFSeRps lançou uma exceção: " + _xmlRetorno
                Exit Function
            Finally
                xmlReq = Nothing
            End Try

            xmlReq = Nothing

            ConsultarNFSeRps = OK

        Catch ex As Exception

            _cUltMens = "ConsultarNFSeRps lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function ConsultarNotas(ByVal CodCidade As String, _
                                   ByVal CPFCNPJRemetente As String, _
                                   ByVal InscricaoMunicipalPrestador As String, _
                                   ByVal dtInicio As String, _
                                   ByVal dtFim As String, _
                                   Optional ByVal AssinaXML As String = "", _
                                   Optional ByVal NotaInicial As String = "1", _
                                   Optional ByVal Versao As String = "1") As Integer

        ConsultarNotas = ERRO

        Try

            'Seta a url do webservice
            If String.IsNullOrEmpty(_cHTTPWASDL) = False Then
                WebService.Url = _cHTTPWASDL
            End If

            'Seta as configurações do proxy
            If _bProxyInformado = True Then
                WebService.Proxy = _objProxy
            End If

            Dim ReqConsultaNFSeRPS As New ConsultaNotas.ReqConsultaNotas
            ReqConsultaNFSeRPS.Cabecalho.Id = "Consulta:notas"
            ReqConsultaNFSeRPS.Cabecalho.CodCidade = CodCidade
            ReqConsultaNFSeRPS.Cabecalho.CPFCNPJRemetente = CPFCNPJRemetente
            ReqConsultaNFSeRPS.Cabecalho.InscricaoMunicipalPrestador = InscricaoMunicipalPrestador
            ReqConsultaNFSeRPS.Cabecalho.dtInicio = dtInicio
            ReqConsultaNFSeRPS.Cabecalho.dtFim = dtFim
            ReqConsultaNFSeRPS.Cabecalho.NotaInicial = NotaInicial
            ReqConsultaNFSeRPS.Cabecalho.Versao = Versao

            Dim xmlReqConsultaNotas As String = Serializacao.MemorySerializer.Serialize(ReqConsultaNFSeRPS)

            Dim xmlReq As New XmlDocument
            xmlReq.LoadXml(xmlReqConsultaNotas)
            xmlReq.PreserveWhitespace = False

            _xmlGerado = xmlReq.OuterXml

            Dim oCertificado As X509Certificate = X509Certificate.CreateFromCertFile(_cPathCertificado)
            WebService.ClientCertificates.Add(oCertificado)
            oCertificado = Nothing

            _xmlRetorno = WebService.consultarNota(xmlReq.OuterXml)

            Try

                Dim retornoSerializado As ConsultaNotas.RetornoConsultaNotas = Serializacao.MemorySerializer.Deserialize(_xmlRetorno, GetType(ConsultaNotas.RetornoConsultaNotas))

                LimparVariaveis()

                _notasConsultadas = retornoSerializado.Notas

                retornoSerializado = Nothing

            Catch ex As Exception
                _cUltMens = "ConsultarNotas lançou uma exceção: " + _xmlRetorno
                Exit Function
            Finally
                xmlReq = Nothing
            End Try

            xmlReq = Nothing

            ConsultarNotas = OK

        Catch ex As Exception

            _cUltMens = "ConsultarNotas lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function ConsultarSequencialRps(ByVal CodCidade As String, _
                                           ByVal CPFCNPJRemetente As String, _
                                           ByVal InscricaoMunicipalPrestador As String, _
                                           ByVal SeriePrestacao As String, _
                                           Optional ByVal Versao As String = "1") As Integer

        ConsultarSequencialRps = ERRO

        Try

            'Seta a url do webservice
            If String.IsNullOrEmpty(_cHTTPWASDL) = False Then
                WebService.Url = _cHTTPWASDL
            End If

            'Seta as configurações do proxy
            If _bProxyInformado = True Then
                WebService.Proxy = _objProxy
            End If

            Dim ConsultaSeqRps As New ConsultaSequencial.ConsultaSeqRps
            ConsultaSeqRps.Cabecalho.CodCid = CodCidade
            ConsultaSeqRps.Cabecalho.CPFCNPJRemetente = CPFCNPJRemetente
            ConsultaSeqRps.Cabecalho.IMPrestador = InscricaoMunicipalPrestador
            ConsultaSeqRps.Cabecalho.SeriePrestacao = SeriePrestacao
            ConsultaSeqRps.Cabecalho.Versao = Versao

            Dim xmlConsultaSeqRps As String = Serializacao.MemorySerializer.Serialize(ConsultaSeqRps)

            Dim xmlReq As New XmlDocument
            xmlReq.LoadXml(xmlConsultaSeqRps)
            xmlReq.PreserveWhitespace = False

            _xmlGerado = xmlReq.OuterXml

            Dim oCertificado As X509Certificate = X509Certificate.CreateFromCertFile(_cPathCertificado)
            WebService.ClientCertificates.Add(oCertificado)
            oCertificado = Nothing

            _xmlRetorno = WebService.consultarSequencialRps(xmlReq.OuterXml)

            Try

                Dim retornoSerializado As ConsultaSequencial.RetornoConsultaSeqRps = Serializacao.MemorySerializer.Deserialize(_xmlRetorno, GetType(ConsultaSequencial.RetornoConsultaSeqRps))

                LimparVariaveis()

                _nroUltimoRps = retornoSerializado.Cabecalho.NroUltimoRps

                retornoSerializado = Nothing

            Catch ex As Exception
                _cUltMens = "ConsultarSequencialRps lançou uma exceção: " + _xmlRetorno
                Exit Function
            Finally
                xmlReq = Nothing
            End Try

            xmlReq = Nothing

            ConsultarSequencialRps = OK

        Catch ex As Exception

            _cUltMens = "ConsultarSequencialRps lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function

    ''' <summary> 
    ''' Envia o documento XML
    ''' Linguagem: VB.NET 
    ''' Framework: 2.0 
    ''' </summary> 
    ''' <param name="Xml">Documento XML</param> 
    ''' <param name="Assinatura">Preencha com "S" para enviar o XML Assinado</param>
    Public Function EnviarXml(ByVal Xml As String, _
                              Optional ByVal Assinatura As String = "") As Integer
        EnviarXml = ERRO

        Try

            'Seta a url do webservice
            If String.IsNullOrEmpty(_cHTTPWASDL) = False Then
                WebService.Url = _cHTTPWASDL
            End If

            'Seta as configurações do proxy
            If _bProxyInformado = True Then
                WebService.Proxy = _objProxy
            End If

            Dim xmlReq As New XmlDocument

            ReqEnvioLoteRPS = Serializacao.MemorySerializer.Deserialize(Xml, GetType(Envio.ReqEnvioLoteRPS))

            PreencheTagAssinatura()

            Dim xmlReqEnvioLoteRPS As String = Serializacao.MemorySerializer.Serialize(ReqEnvioLoteRPS)
            xmlReq.LoadXml(xmlReqEnvioLoteRPS)
            xmlReq.PreserveWhitespace = False

            If Assinatura = "S" Then
                AssinarElementosXML(xmlReq, "ns1:ReqEnvioLoteRPS", "Lote", "Id")
            End If

            _xmlGerado = xmlReq.OuterXml

            Dim oCertificado As X509Certificate = X509Certificate.CreateFromCertFile(_cPathCertificado)
            WebService.ClientCertificates.Add(oCertificado)
            oCertificado = Nothing

            _xmlRetorno = WebService.enviar(xmlReq.OuterXml)

            Try

                Dim retornoSerializado As Envio.RetornoEnvioLoteRPS = Serializacao.MemorySerializer.Deserialize(_xmlRetorno, GetType(Envio.RetornoEnvioLoteRPS))

                LimparVariaveis()

                _assincrono = retornoSerializado.Cabecalho.Assincrono
                _numeroDoLoteGerado = retornoSerializado.Cabecalho.NumeroLote
                _erros = retornoSerializado.Erros
                _alertas = retornoSerializado.Alertas
                _notasProcessadas = retornoSerializado.Notas

                retornoSerializado = Nothing

            Catch ex As Exception
                _cUltMens = "EnviarXml lançou uma exceção: " + _xmlRetorno
                Exit Function
            Finally
                xmlReq = Nothing
            End Try

            xmlReq = Nothing

            EnviarXml = OK

        Catch ex As Exception

            _cUltMens = "EnviarXML lançou uma exceção: " + _xmlRetorno
            Exit Function

        End Try
    End Function
    ''' <summary> 
    ''' Envia o documento XML
    ''' Linguagem: VB.NET 
    ''' Framework: 2.0 
    ''' </summary> 
    ''' <param name="Assinatura">Preencha com "S" para enviar o XML Assinado</param> 
    Public Function Enviar(Optional ByVal Assinatura As String = "") As Integer

        Enviar = ERRO

        Try

            'Seta a url do webservice
            If String.IsNullOrEmpty(_cHTTPWASDL) = False Then
                WebService.Url = _cHTTPWASDL
            End If

            'Seta as configurações do proxy
            If _bProxyInformado = True Then
                WebService.Proxy = _objProxy
            End If

            PreencheTagAssinatura()

            Dim xmlReq As New XmlDocument

            Dim xmlReqEnvioLoteRPS As String = Serializacao.MemorySerializer.Serialize(ReqEnvioLoteRPS)
            xmlReq.LoadXml(xmlReqEnvioLoteRPS)
            xmlReq.PreserveWhitespace = False

            If Assinatura = "S" Then
                AssinarElementosXML(xmlReq, "ns1:ReqEnvioLoteRPS", "Lote", "Id")
            End If

            _xmlGerado = xmlReq.OuterXml

            Dim oCertificado As X509Certificate = X509Certificate.CreateFromCertFile(_cPathCertificado)
            WebService.ClientCertificates.Add(oCertificado)
            oCertificado = Nothing

            _xmlRetorno = WebService.enviar(xmlReq.OuterXml)

            Try

                Dim retornoSerializado As Envio.RetornoEnvioLoteRPS = Serializacao.MemorySerializer.Deserialize(_xmlRetorno, GetType(Envio.RetornoEnvioLoteRPS))

                LimparVariaveis()

                _assincrono = retornoSerializado.Cabecalho.Assincrono
                _numeroDoLoteGerado = retornoSerializado.Cabecalho.NumeroLote
                _erros = retornoSerializado.Erros
                _alertas = retornoSerializado.Alertas
                _notasProcessadas = retornoSerializado.Notas
                _sucesso = retornoSerializado.Cabecalho.Sucesso

                retornoSerializado = Nothing

            Catch ex As Exception
                _cUltMens = "Enviar lançou uma exceção: " + _xmlRetorno
                Exit Function
            Finally
                xmlReq = Nothing
            End Try

            xmlReq = Nothing

            Enviar = OK

        Catch ex As Exception

            _cUltMens = "Enviar lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function ObterXmlEnvio(Optional ByVal Assinado As String = "") As String

        Try

            PreencheTagAssinatura()

            Dim xmlReq As New XmlDocument
            Dim xmlReqEnvioLoteRPS As String = Serializacao.MemorySerializer.Serialize(ReqEnvioLoteRPS)
            xmlReq.LoadXml(xmlReqEnvioLoteRPS)
            xmlReq.PreserveWhitespace = False

            If Assinado = "S" Then
                AssinarElementosXML(xmlReq, "ns1:ReqEnvioLoteRPS", "Lote", "Id")
            End If

            ObterXmlEnvio = xmlReq.OuterXml

        Catch ex As Exception

            ObterXmlEnvio = "ObterXmlEnvio lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    ''' <summary> 
    ''' Consulta de Lote
    ''' Linguagem: VB.NET 
    ''' Framework: 2.0 
    ''' </summary> 
    ''' <param name="numeroLote">Número do Lote</param> 
    Public Function ConsultarLote(ByVal CodCidade As String, _
                                  ByVal CPFCNPJRemetente As String, _
                                  ByVal NumeroLote As String) As Integer

        ConsultarLote = ERRO

        Try

            'Seta a url do webservice
            If String.IsNullOrEmpty(_cHTTPWASDL) = False Then
                WebService.Url = _cHTTPWASDL
            End If

            'Seta as configurações do proxy
            If _bProxyInformado = True Then
                WebService.Proxy = _objProxy
            End If

            Dim ReqConsultaLote As New ConsultaLote.ReqConsultaLote
            ReqConsultaLote.Cabecalho.CodCidade = CodCidade
            ReqConsultaLote.Cabecalho.CPFCNPJRemetente = CPFCNPJRemetente
            ReqConsultaLote.Cabecalho.Versao = "1"
            ReqConsultaLote.Cabecalho.NumeroLote = NumeroLote

            Dim xmlReqConsultaLote As String = Serializacao.MemorySerializer.Serialize(ReqConsultaLote)

            Dim xmlReq As New XmlDocument
            xmlReq.LoadXml(xmlReqConsultaLote)
            xmlReq.PreserveWhitespace = False

            _xmlGerado = xmlReq.OuterXml

            Dim oCertificado As X509Certificate = X509Certificate.CreateFromCertFile(_cPathCertificado)
            WebService.ClientCertificates.Add(oCertificado)
            oCertificado = Nothing

            _xmlRetorno = WebService.consultarLote(xmlReq.OuterXml)

            Try

                Dim retornoSerializado As ConsultaLote.RetornoConsultaLote = Serializacao.MemorySerializer.Deserialize(_xmlRetorno, GetType(ConsultaLote.RetornoConsultaLote))

                LimparVariaveis()

                _sucesso = retornoSerializado.Cabecalho.Sucesso
                _erros = retornoSerializado.Erros
                _alertas = retornoSerializado.Alertas
                _notasProcessadas = retornoSerializado.Lote

                retornoSerializado = Nothing

            Catch ex As Exception
                _cUltMens = "ConsultarLote lançou uma exceção: " + _xmlRetorno
                Exit Function
            Finally
                xmlReq = Nothing
            End Try

            xmlReq = Nothing

            ConsultarLote = OK

        Catch ex As Exception

            _cUltMens = "ConsultarLote lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function CriarLoteConsultaNFSeRPS(ByVal CodCidade As String, _
                                             ByVal CPFCNPJRemetente As String) As Integer

        CriarLoteConsultaNFSeRPS = ERRO

        Try

            ReqConsultaNFSeRPS.Cabecalho.CodCidade = CodCidade
            ReqConsultaNFSeRPS.Cabecalho.CPFCNPJRemetente = CPFCNPJRemetente
            ReqConsultaNFSeRPS.Cabecalho.Transacao = "true"
            ReqConsultaNFSeRPS.Cabecalho.Versao = "1"
            ReqConsultaNFSeRPS.Lote.Id = String.Format("lote:{0}ABCDZ", 1)

            CriarLoteConsultaNFSeRPS = OK

        Catch ex As Exception

            _cUltMens = "CriarLoteConsultaNFSeRPS lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function AdicionarNotaConsulta(ByVal InscricaoMunicipalPrestador As String, _
                                          ByVal NumeroNota As String, _
                                          ByVal CodigoVerificacao As String) As Integer

        AdicionarNotaConsulta = ERRO

        Try

            Dim nota As New ConsultaNFSeRPS.Nota
            nota.Id = NumeroNota
            nota.InscricaoMunicipalPrestador = InscricaoMunicipalPrestador
            nota.NumeroNota = NumeroNota
            nota.CodigoVerificacao = CodigoVerificacao
            ReqConsultaNFSeRPS.Lote.NotaConsulta.Add(nota)
            nota = Nothing

            AdicionarNotaConsulta = OK

        Catch ex As Exception

            _cUltMens = "AdicionarNotaConsulta lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function AdicionarRPSConsulta(ByVal InscricaoMunicipalPrestador As String, _
                                         ByVal NumeroRPS As String, _
                                         ByVal SeriePrestacao As String) As Integer

        AdicionarRPSConsulta = ERRO

        Try

            Dim rps As New ConsultaNFSeRPS.RPS
            rps.Id = String.Format("rps:{0}", NumeroRPS)
            rps.InscricaoMunicipalPrestador = InscricaoMunicipalPrestador
            rps.NumeroRPS = NumeroRPS
            rps.SeriePrestacao = SeriePrestacao
            ReqConsultaNFSeRPS.Lote.RPSConsulta.Add(rps)
            rps = Nothing

            AdicionarRPSConsulta = OK

        Catch ex As Exception

            _cUltMens = "AdicionarRPSConsulta lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function CriarLote(ByVal CodCidade As String, _
                              ByVal CPFCNPJRemetente As String, _
                              ByVal RazaoSocialRemetente As String, _
                              Optional ByVal Transacao As String = "true", _
                              Optional ByVal Versao As String = "1") As Integer

        CriarLote = ERRO

        Try

            ReqEnvioLoteRPS.Cabecalho.CodCidade = CodCidade
            ReqEnvioLoteRPS.Cabecalho.CPFCNPJRemetente = CPFCNPJRemetente
            ReqEnvioLoteRPS.Cabecalho.RazaoSocialRemetente = RazaoSocialRemetente
            ReqEnvioLoteRPS.Cabecalho.MetodoEnvio = "WS"
            ReqEnvioLoteRPS.Cabecalho.Transacao = Transacao
            ReqEnvioLoteRPS.Cabecalho.Versao = Versao
            ReqEnvioLoteRPS.Cabecalho.VersaoComponente = VersaoDLL()
            ReqEnvioLoteRPS.Cabecalho.ValorTotalDeducoes = 0
            ReqEnvioLoteRPS.Cabecalho.ValorTotalServicos = 0
            ReqEnvioLoteRPS.Lote.Id = String.Format("lote:{0}ABCDZ", 1)

            CriarLote = OK

        Catch ex As Exception

            _cUltMens = "CriarLote lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function

    Public Function AdicionarRPS(ByVal InscricaoMunicipalPrestador As String, _
                                 ByVal RazaoSocialPrestador As String, _
                                 ByVal TipoRPS As String, _
                                 ByVal SerieRPS As String, _
                                 ByVal NumeroRPS As String, _
                                 ByVal DataEmissaoRPS As String, _
                                 ByVal SituacaoRPS As String, _
                                 ByVal SerieRPSSubstituido As String, _
                                 ByVal NumeroRPSSubstituido As String, _
                                 ByVal NumeroNFSeSubstituida As String, _
                                 ByVal DataEmissaoNFSeSubstituida As String, _
                                 ByVal SeriePrestacao As String, _
                                 ByVal InscricaoMunicipalTomador As String, _
                                 ByVal CPFCNPJTomador As String, _
                                 ByVal RazaoSocialTomador As String, _
                                 ByVal DocTomadorEstrangeiro As String, _
                                 ByVal TipoLogradouroTomador As String, _
                                 ByVal LogradouroTomador As String, _
                                 ByVal NumeroEnderecoTomador As String, _
                                 ByVal ComplementoEnderecoTomador As String, _
                                 ByVal TipoBairroTomador As String, _
                                 ByVal BairroTomador As String, _
                                 ByVal CidadeTomador As String, _
                                 ByVal CidadeTomadorDescricao As String, _
                                 ByVal CEPTomador As String, _
                                 ByVal EmailTomador As String, _
                                 ByVal CodigoAtividade As String, _
                                 ByVal AliquotaAtividade As String, _
                                 ByVal TipoRecolhimento As String, _
                                 ByVal MunicipioPrestacao As String, _
                                 ByVal MunicipioPrestacaoDescricao As String, _
                                 ByVal Operacao As String, _
                                 ByVal Tributacao As String, _
                                 ByVal ValorPIS As String, _
                                 ByVal ValorCOFINS As String, _
                                 ByVal ValorINSS As String, _
                                 ByVal ValorIR As String, _
                                 ByVal ValorCSLL As String, _
                                 ByVal AliquotaPIS As String, _
                                 ByVal AliquotaCOFINS As String, _
                                 ByVal AliquotaINSS As String, _
                                 ByVal AliquotaIR As String, _
                                 ByVal AliquotaCSLL As String, _
                                 ByVal DescricaoRPS As String, _
                                 ByVal DDDPrestador As String, _
                                 ByVal TelefonePrestador As String, _
                                 ByVal DDDTomador As String, _
                                 ByVal TelefoneTomador As String, _
                                 ByVal MotCancelamento As String, _
                                 ByVal CpfCnpjIntermediario As String) As Integer

        Try

            AdicionarRPS = ERRO

            Dim rps As New Geral.RPS

            If _bRPS = True Then
                _rpsIdAtual += 1
                _rpsIndexAtual += 1
                'Data de emissão do ultimo RPS
                ReqEnvioLoteRPS.Cabecalho.dtFim = Format(CDate(DataEmissaoRPS), "yyyy-MM-dd")
            Else
                _bRPS = True
                'Data de emissão do primeiro RPS adicionado
                ReqEnvioLoteRPS.Cabecalho.dtInicio = Format(CDate(DataEmissaoRPS), "yyyy-MM-dd")
                'Já seta a data de fim prevendo que só terá um item
                ReqEnvioLoteRPS.Cabecalho.dtFim = Format(CDate(DataEmissaoRPS), "yyyy-MM-dd")
            End If
            'Format(CDate(DataEmissaoRPS), "yyyy-MM-ddThh:mm:ss")

            rps.Id = String.Format("rps:{0}", _rpsIdAtual)
            rps.InscricaoMunicipalPrestador = InscricaoMunicipalPrestador
            rps.RazaoSocialPrestador = RazaoSocialPrestador
            rps.TipoRPS = TipoRPS
            rps.SerieRPS = SerieRPS
            rps.NumeroRPS = NumeroRPS
            rps.DataEmissaoRPS = Format(CDate(DataEmissaoRPS), "yyyy-MM-ddThh:mm:ss")
            rps.SituacaoRPS = SituacaoRPS
            rps.SerieRPSSubstituido = SerieRPSSubstituido
            rps.NumeroRPSSubstituido = NumeroRPSSubstituido
            rps.NumeroNFSeSubstituida = NumeroNFSeSubstituida
            rps.DataEmissaoNFSeSubstituida = Format(CDate(DataEmissaoNFSeSubstituida), "yyyy-MM-dd")
            rps.SeriePrestacao = SeriePrestacao
            rps.InscricaoMunicipalTomador = InscricaoMunicipalTomador
            rps.CPFCNPJTomador = FormataZerosAEsquerda(CPFCNPJTomador, 14)
            rps.RazaoSocialTomador = RazaoSocialTomador
            rps.DocTomadorEstrangeiro = DocTomadorEstrangeiro
            rps.TipoLogradouroTomador = TipoLogradouroTomador
            rps.LogradouroTomador = LogradouroTomador
            rps.NumeroEnderecoTomador = NumeroEnderecoTomador
            rps.ComplementoEnderecoTomador = ComplementoEnderecoTomador
            rps.TipoBairroTomador = TipoBairroTomador
            rps.BairroTomador = BairroTomador
            rps.CidadeTomador = CidadeTomador
            rps.CidadeTomadorDescricao = CidadeTomadorDescricao
            rps.CEPTomador = CEPTomador
            rps.EmailTomador = EmailTomador
            rps.CodigoAtividade = CodigoAtividade
            rps.AliquotaAtividade = AliquotaAtividade
            rps.TipoRecolhimento = TipoRecolhimento
            rps.MunicipioPrestacao = MunicipioPrestacao
            rps.MunicipioPrestacaoDescricao = MunicipioPrestacaoDescricao
            rps.Operacao = Operacao
            rps.Tributacao = Tributacao
            rps.ValorPIS = FormataNumero(ValorPIS, 2)
            rps.ValorCOFINS = FormataNumero(ValorCOFINS, 2)
            rps.ValorINSS = FormataNumero(ValorINSS, 2)
            rps.ValorIR = FormataNumero(ValorIR, 2)
            rps.ValorCSLL = FormataNumero(ValorCSLL, 2)
            rps.AliquotaPIS = FormataNumero(AliquotaPIS, 4)
            rps.AliquotaCOFINS = FormataNumero(AliquotaCOFINS, 4)
            rps.AliquotaINSS = FormataNumero(AliquotaINSS, 4)
            rps.AliquotaIR = FormataNumero(AliquotaIR, 4)
            rps.AliquotaCSLL = FormataNumero(AliquotaCSLL, 4)
            rps.DescricaoRPS = DescricaoRPS
            rps.DDDPrestador = DDDPrestador
            rps.TelefonePrestador = TelefonePrestador
            rps.DDDTomador = DDDTomador
            rps.TelefoneTomador = TelefoneTomador
            rps.MotCancelamento = MotCancelamento
            rps.CPFCNPJIntermediario = CpfCnpjIntermediario

            ReqEnvioLoteRPS.Lote.RPS.Add(rps)
            ReqEnvioLoteRPS.Cabecalho.QtdRPS += 1

            rps = Nothing

            AdicionarRPS = OK

        Catch ex As Exception

            _cUltMens = "AdicionarRPS lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function


    Public Function AdicionarItemServicoRPS(ByVal DiscriminacaoServico As String, _
                                            ByVal Quantidade As Double, _
                                            ByVal ValorUnitario As Double, _
                                            ByVal Tributavel As String) As Integer

        AdicionarItemServicoRPS = ERRO

        Try

            Dim itemServicoRPS As New Envio.ItemServicoRPS
            itemServicoRPS.DiscriminacaoServico = DiscriminacaoServico
            itemServicoRPS.Quantidade = FormataNumero(Quantidade, 4)
            itemServicoRPS.ValorUnitario = FormataNumero(ValorUnitario, 4)
            itemServicoRPS.Tributavel = Tributavel
            itemServicoRPS.ValorTotal = FormataNumero(Quantidade * ValorUnitario, 2)

            ReqEnvioLoteRPS.Lote.RPS.Item(_rpsIndexAtual).listaItemServicoRPS.Add(itemServicoRPS)

            ReqEnvioLoteRPS.Cabecalho.ValorTotalServicos = FormataNumero(StringToDouble(ReqEnvioLoteRPS.Cabecalho.ValorTotalServicos) + (Quantidade * ValorUnitario), 2)

            AdicionarItemServicoRPS = OK

        Catch ex As Exception

            _cUltMens = "AdicionarItemServicoRPS lançou uma exceção: " + ex.Message
            Exit Function

        End Try


    End Function
    Public Function AdicionarDeducaoRPS(ByVal DeducaoPor As String, _
                                        ByVal TipoDeducao As String, _
                                        ByVal CPFCNPJReferencia As String, _
                                        ByVal NumeroNFReferencia As Integer, _
                                        ByVal ValorTotalReferencia As Double, _
                                        ByVal PercentualDeduzir As Double, _
                                        ByVal ValorDeduzir As Double) As Integer
        AdicionarDeducaoRPS = ERRO

        Try

            Dim deducaoRPS As New Envio.DeducaoRPS

            deducaoRPS.DeducaoPor = DeducaoPor
            deducaoRPS.TipoDeducao = TipoDeducao

            If String.IsNullOrEmpty(CPFCNPJReferencia) = False Then
                deducaoRPS.CPFCNPJReferencia = FormataZerosAEsquerda(CPFCNPJReferencia, 14)
            Else
                deducaoRPS.CPFCNPJReferencia = ""
            End If

            deducaoRPS.NumeroNFReferencia = NumeroNFReferencia
            deducaoRPS.ValorTotalReferencia = FormataNumero(ValorTotalReferencia, 2)
            deducaoRPS.PercentualDeduzir = FormataNumero(PercentualDeduzir, 2)
            deducaoRPS.ValorDeduzir = FormataNumero(ValorDeduzir, 2)

            ReqEnvioLoteRPS.Lote.RPS.Item(_rpsIndexAtual).listaDeducaoRPS.Add(deducaoRPS)
            ReqEnvioLoteRPS.Cabecalho.ValorTotalDeducoes = FormataNumero(StringToDouble(ReqEnvioLoteRPS.Cabecalho.ValorTotalDeducoes) + (ValorDeduzir), 2)

            AdicionarDeducaoRPS = OK

        Catch ex As Exception

            _cUltMens = "AdicionarDeducaoRPS lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function NumeroDeErros() As String
        NumeroDeErros = _erros.Count
    End Function
    Public Function NumeroDeAlertas() As String
        NumeroDeAlertas = _alertas.Count
    End Function
    Public Function XmlGerado() As String
        XmlGerado = _xmlGerado
    End Function
    Public Function XmlRetorno() As String
        XmlRetorno = _xmlRetorno
    End Function
    Public Function ObterAlerta(ByVal nPos As Integer) As String

        Try

            Dim alerta As Alerta
            alerta = _alertas(nPos)

            Dim retorno As String
            retorno = ObterPropriedades(Of Alerta)(alerta)
            ObterAlerta = retorno

        Catch ex As Exception

            ObterAlerta = "ObterAlerta lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function ObterAlertas() As String

        Try

            Dim retorno As String = String.Empty
            retorno = ObterPropriedades(Of Alerta)(_alertas)
            ObterAlertas = retorno

        Catch ex As Exception
            ObterAlertas = "ObterAlertas lançou uma exceção: " + ex.Message
            Exit Function
        End Try

    End Function
    Public Function ObterErro(ByVal nPos As Integer) As String

        Try

            Dim erro As Erro
            erro = _erros(nPos)

            Dim retorno As String
            retorno = ObterPropriedades(Of Erro)(erro)
            ObterErro = retorno

        Catch ex As Exception

            ObterErro = "obterErro lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function ObterErros() As String

        Try

            Dim retorno As String = String.Empty
            retorno = ObterPropriedades(Of Erro)(_erros)
            ObterErros = retorno

        Catch ex As Exception

            ObterErros = "ObterErros lançou uma exceção: " + ex.Message
            Exit Function

        End Try

    End Function
    Public Function NumeroDeNotasProcessadas() As String
        NumeroDeNotasProcessadas = _notasProcessadas.Count
    End Function
    Public Function ObterNotaProcessada(ByVal nPos As Integer) As String
        Try

            Dim registro As New Geral.RPS
            registro = _notasProcessadas(nPos)

            Dim retorno As String
            retorno = ObterPropriedades(Of Geral.RPS)(registro)
            ObterNotaProcessada = retorno

        Catch ex As Exception

            ObterNotaProcessada = "ObterNotaProcessada lançou uma exceção: " + ex.Message
            Exit Function

        End Try
    End Function
    Public Function ObterNotasProcessadas() As String

        Try

            Dim retorno As String = String.Empty
            retorno = ObterPropriedades(Of Geral.RPS)(_notasProcessadas)
            ObterNotasProcessadas = retorno

        Catch ex As Exception
            ObterNotasProcessadas = "ObterNotasProcessadas lançou uma exceção: " + ex.Message
            Exit Function
        End Try

    End Function
    Public Function NumeroDeNotasCanceladas() As String
        NumeroDeNotasCanceladas = _notasCanceladas.Count
    End Function
    Public Function ObterNotaCancelada(ByVal nPos As Integer) As String

        Try

            Dim registro As New Geral.RPS
            registro = _notasCanceladas(nPos)

            Dim retorno As String = String.Empty
            retorno = ObterPropriedades(Of Geral.RPS)(registro)
            ObterNotaCancelada = retorno

        Catch ex As Exception
            ObterNotaCancelada = "ObterNotaCancelada lançou uma exceção: " + ex.Message
            Exit Function
        End Try

    End Function
    Public Function ObterNotasCanceladas() As String

        Try

            Dim retorno As String = String.Empty
            retorno = ObterPropriedades(Of Geral.RPS)(_notasCanceladas)

            ObterNotasCanceladas = retorno

        Catch ex As Exception
            ObterNotasCanceladas = "ObterNotasCanceladas lançou uma exceção: " + ex.Message
            Exit Function
        End Try

    End Function
    Public Function NumeroDeNotasConsultadas() As String
        NumeroDeNotasConsultadas = _notasConsultadas.Count
    End Function
    Public Function ObterNotaConsultada(ByVal nPos As Integer) As String

        Try

            Dim registro As New Geral.RPS
            registro = _notasConsultadas(nPos)

            Dim retorno As String = String.Empty
            retorno = ObterPropriedades(Of Geral.RPS)(registro)
            ObterNotaConsultada = retorno

        Catch ex As Exception
            ObterNotaConsultada = "ObterNotaConsultada lançou uma exceção: " + ex.Message
            Exit Function
        End Try

    End Function
    Public Function ObterNotasConsultadas() As String

        Try

            Dim retorno As String = String.Empty
            retorno = ObterPropriedades(Of Geral.RPS)(_notasConsultadas)

            ObterNotasConsultadas = retorno

        Catch ex As Exception
            ObterNotasConsultadas = "ObterNotasCanceladas lançou uma exceção: " + ex.Message
            Exit Function
        End Try

    End Function
    Public Function NumeroDoUltimoRps() As String
        NumeroDoUltimoRps = _nroUltimoRps
    End Function
    Private Function ObterPropriedades(Of T)(ByVal Objeto As Object) As String

        Try

            Dim retorno As String = String.Empty
            Dim property_value As Object

            Dim properties As PropertyInfo() = GetType(T).GetProperties()

            For i As Integer = 0 To properties.Length - 1
                With properties(i)

                    If .GetIndexParameters().Length = 0 Then

                        property_value = .GetValue(Objeto, Nothing)
                        If TypeOf (property_value) Is String Then
                            If property_value <> Nothing Then
                                retorno += .Name + "=" + property_value.ToString + "|"
                                Debug.Print(retorno)
                            End If
                        End If

                    End If

                End With

            Next i

            retorno += "\n"

            ObterPropriedades = retorno

        Catch ex As Exception
            ObterPropriedades = "ObterPropriedades objeto lançou uma exceção: " + ex.Message
            Exit Function
        End Try

    End Function
    Private Function ObterPropriedades(Of T)(ByVal ListaObjeto As IEnumerable) As String

        Try

            Dim retorno As String = String.Empty
            Dim property_value As Object

            For Each registro As T In ListaObjeto

                Dim properties As PropertyInfo() = GetType(T).GetProperties()

                For i As Integer = 0 To properties.Length - 1
                    With properties(i)

                        If .GetIndexParameters().Length = 0 Then

                            property_value = .GetValue(registro, Nothing)
                            If TypeOf (property_value) Is String Then
                                If property_value <> Nothing Then
                                    retorno += .Name + "=" + property_value.ToString + "|"
                                    Debug.Print(retorno)
                                End If
                            End If

                        End If

                    End With

                Next i

                retorno += "\n"

            Next

            ObterPropriedades = retorno

        Catch ex As Exception
            ObterPropriedades = "ObterPropriedades lista lançou uma exceção: " + ex.Message
            Exit Function
        End Try

    End Function
    Private Sub PreencheTagAssinatura()

        Try

            For x As Integer = 0 To ReqEnvioLoteRPS.Lote.RPS.Count - 1

                Dim valorRPSDeducoes As Double = 0
                Dim valorRPSServicos As Double = 0

                Dim RPS As New Geral.RPS
                RPS = ReqEnvioLoteRPS.Lote.RPS(x)

                If RPS.listaItemServicoRPS.Count > 0 Then
                    For Each deducaoRPS As Envio.ItemServicoRPS In RPS.listaItemServicoRPS
                        valorRPSServicos += StringToDouble(deducaoRPS.ValorTotal)
                    Next
                End If

                If RPS.listaDeducaoRPS.Count > 0 Then
                    For Each deducaoRPS As Envio.DeducaoRPS In RPS.listaDeducaoRPS
                        valorRPSDeducoes += StringToDouble(deducaoRPS.ValorDeduzir)
                    Next
                End If

                'Dim InscricaoMunicipalContribuinte As String = "00000317330"
                'Dim SerieRPS As String = "NF"
                'Dim NumeroRPS As String = "000000038663"
                'Dim DataEmissaoRPS As String = "20090905"
                'Dim Tributacao As String = "T"
                'Dim SituacaoRPS As String = "N"
                'Dim TipoRecolhimento As String = "N"
                'Dim ValorServicoSubtraindoDeducao As String = "000000000001686"
                'Dim ValorDeducao As String = "000000000000000"
                'Dim CodigoAtividade As String = "0829979900"
                'Dim CPFCNPJTomador As String = "08764130000102"

                Dim AssinaturaHash As String

                Dim valorServicoSubtraindoDeducao As String
                valorServicoSubtraindoDeducao = (valorRPSServicos - valorRPSDeducoes)
                valorServicoSubtraindoDeducao = FormataNumero(StringToDouble(valorServicoSubtraindoDeducao), 2)
                valorServicoSubtraindoDeducao = valorServicoSubtraindoDeducao.Replace(".", "")

                Dim valorRPSDeducoesTotal As String
                valorRPSDeducoesTotal = FormataNumero(valorRPSDeducoes, 2)
                valorRPSDeducoesTotal = valorRPSDeducoesTotal.Replace(".", "")

                Dim DataEmissaoRPS As String()
                DataEmissaoRPS = RPS.DataEmissaoRPS.Replace("-", "").Split("T")

                AssinaturaHash = FormataZerosAEsquerda(RPS.InscricaoMunicipalPrestador, 11) + _
                                 FormataEspacosADireita(RPS.SerieRPS, 5) + _
                                 FormataZerosAEsquerda(RPS.NumeroRPS, 12) + _
                                 DataEmissaoRPS(0) + _
                                 FormataEspacosADireita(RPS.Tributacao, 2) + _
                                 RPS.SituacaoRPS + _
                                 IIf(RPS.TipoRecolhimento = "A", "N", "S") + _
                                 FormataZerosAEsquerda(valorServicoSubtraindoDeducao, 15) + _
                                 FormataZerosAEsquerda(valorRPSDeducoesTotal, 15) + _
                                 FormataZerosAEsquerda(RPS.CodigoAtividade, 10) + _
                                 FormataZerosAEsquerda(RPS.CPFCNPJTomador, 14)

                RPS.Assinatura = GeraAssinaturaHash(AssinaturaHash)

            Next

        Catch ex As Exception
        End Try

    End Sub
    Private Function StringToDouble(ByVal valor As String) As Double
        StringToDouble = Double.Parse(valor.Replace(".", ","))
    End Function
    Private Function FormataNumero(ByVal valor As Double, ByVal nDecimais As String) As String
        FormataNumero = Format(valor, "f" + nDecimais.ToString).Replace(",", ".")
    End Function
    Private Function FormataZerosAEsquerda(ByVal Valor As String, ByVal nZeros As String)
        FormataZerosAEsquerda = Valor.PadLeft(nZeros, "0")
    End Function
    Private Function FormataEspacosADireita(ByVal Valor As String, ByVal nEspacos As String)
        FormataEspacosADireita = Valor.PadRight(nEspacos, " ")
    End Function
    Public Function NumeroDoLoteGerado()
        NumeroDoLoteGerado = _numeroDoLoteGerado
    End Function
    Public Function EhAssincrono() As String
        EhAssincrono = _assincrono
    End Function
    Public Function EhSucesso() As String
        EhSucesso = _sucesso
    End Function
    Public Function ObterUltMensErro() As String
        ObterUltMensErro = _cUltMens
    End Function
    Public Sub SetCertificado(ByVal cPathCertifcadoCER As String)
        _cPathCertificado = cPathCertifcadoCER
    End Sub
    Public Sub SetURL(ByVal HTTPWASDL As String)
        _cHTTPWASDL = HTTPWASDL
    End Sub
    Public Function VersaoDLL() As String
        Dim VersionNo As System.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version()
        Return VersionNo.Major.ToString & "." & _
        VersionNo.Minor.ToString & "." & _
        VersionNo.Build.ToString & "." & _
        VersionNo.Revision.ToString
    End Function
    Protected Overrides Sub Finalize()
        _objProxy = Nothing
        _objProxyCredentials = Nothing

        ReqConsultaNFSeRPS = Nothing
        ReqEnvioLoteRPS = Nothing

        _reqEnvioLoteRPS = Nothing
        _reqConsultaNFSeRPS = Nothing
        _webService = Nothing

        _bRPS = Nothing
        _rpsIdAtual = Nothing
        _rpsIndexAtual = Nothing
        _erros = Nothing
        _alertas = Nothing
        _notasCanceladas = Nothing
        _notasProcessadas = Nothing
        _notasConsultadas = Nothing
        _nroUltimoRps = Nothing
        _assincrono = Nothing
        _sucesso = String.Empty
        _numeroDoLoteGerado = Nothing
        _cUltMens = Nothing
        _cHTTPWASDL = Nothing
        _cPathCertificado = Nothing
        _bProxyInformado = Nothing
        MyBase.Finalize()
    End Sub

End Class