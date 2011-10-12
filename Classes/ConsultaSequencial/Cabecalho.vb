Namespace ConsultaSequencial

    Public Class Cabecalho

        Private _codCid As String
        Private _imPrestador As String
        Private _cpfCNPJRemetente As String
        Private _nroUltimoRps As String
        Private _seriePrestacao As String
        Private _versao As String
        Public Property CodCid() As String
            Get
                Return Me._codCid
            End Get
            Set(ByVal valor As String)
                Me._codCid = valor
            End Set
        End Property
        Public Property IMPrestador() As String
            Get
                Return Me._imPrestador
            End Get
            Set(ByVal valor As String)
                Me._imPrestador = valor
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
        Public Property NroUltimoRps() As String
            Get
                Return Me._nroUltimoRps
            End Get
            Set(ByVal valor As String)
                Me._nroUltimoRps = valor
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
        Public Property Versao() As String
            Get
                Return Me._versao
            End Get
            Set(ByVal valor As String)
                Me._versao = valor
            End Set
        End Property
    End Class

End Namespace
