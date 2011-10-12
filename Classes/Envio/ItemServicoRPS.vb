Namespace Envio

    Public Class ItemServicoRPS

        Protected _discriminacaoServico As String
        Protected _quantidade As String
        Protected _valorUnitario As String
        Protected _valorTotal As String
        Protected _tributavel As String

        Public Property DiscriminacaoServico() As String
            Get
                Return Me._discriminacaoServico
            End Get
            Set(ByVal valor As String)
                Me._discriminacaoServico = valor
            End Set
        End Property

        Public Property Quantidade() As String
            Get
                Return Me._quantidade
            End Get
            Set(ByVal valor As String)
                Me._quantidade = valor
            End Set
        End Property

        Public Property ValorUnitario() As String
            Get
                Return Me._valorUnitario
            End Get
            Set(ByVal valor As String)
                Me._valorUnitario = valor
            End Set
        End Property

        Public Property ValorTotal() As String
            Get
                Return Me._valorTotal
            End Get
            Set(ByVal valor As String)
                Me._valorTotal = valor
            End Set
        End Property

        Public Property Tributavel() As String
            Get
                Return Me._tributavel
            End Get
            Set(ByVal valor As String)
                Me._tributavel = valor
            End Set
        End Property

    End Class

End Namespace