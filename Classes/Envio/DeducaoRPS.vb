Namespace Envio

    Public Class DeducaoRPS

        Private _deducaoPor As String
        Private _tipoDeducao As String
        Private _cpfCNPJReferencia As String
        Private _numeroNFReferencia As String
        Private _valorTotalReferencia As String
        Private _percentualDeduzir As String
        Private _valorDeduzir As String

        Public Property DeducaoPor() As String
            Get
                Return Me._deducaoPor
            End Get
            Set(ByVal valor As String)
                Me._deducaoPor = valor
            End Set
        End Property
        Public Property TipoDeducao() As String
            Get
                Return Me._tipoDeducao
            End Get
            Set(ByVal valor As String)
                Me._tipoDeducao = valor
            End Set
        End Property
        Public Property CPFCNPJReferencia() As String
            Get
                Return Me._cpfCNPJReferencia
            End Get
            Set(ByVal valor As String)
                Me._cpfCNPJReferencia = valor
            End Set
        End Property
        Public Property NumeroNFReferencia() As String
            Get
                Return Me._numeroNFReferencia
            End Get
            Set(ByVal valor As String)
                Me._numeroNFReferencia = valor
            End Set
        End Property
        Public Property ValorTotalReferencia() As String
            Get
                Return Me._valorTotalReferencia
            End Get
            Set(ByVal valor As String)
                Me._valorTotalReferencia = valor
            End Set
        End Property
        Public Property PercentualDeduzir() As String
            Get
                Return Me._percentualDeduzir
            End Get
            Set(ByVal valor As String)
                Me._percentualDeduzir = valor
            End Set
        End Property
        Public Property ValorDeduzir() As String
            Get
                Return Me._valorDeduzir
            End Get
            Set(ByVal valor As String)
                Me._valorDeduzir = valor
            End Set
        End Property
    End Class

End Namespace