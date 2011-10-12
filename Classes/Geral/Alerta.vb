Public Class Alerta
    Private _codigo As String
    Private _descricao As String
    Private _chaveNFe As Geral.RPS

    Public Property Codigo() As String
        Get
            Return Me._codigo
        End Get
        Set(ByVal valor As String)
            Me._codigo = valor
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return Me._descricao
        End Get
        Set(ByVal valor As String)
            Me._descricao = valor
        End Set
    End Property

    Public Property ChaveNFe() As Geral.RPS
        Get
            Return Me._chaveNFe
        End Get
        Set(ByVal valor As Geral.RPS)
            Me._chaveNFe = valor
        End Set
    End Property
End Class
