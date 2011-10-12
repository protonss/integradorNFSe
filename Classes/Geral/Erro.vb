Public Class Erro
    Private _codigo As String = String.Empty
    Private _descricao As String = String.Empty
    Private _chaveRPS As Geral.RPS
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
    Public Property ChaveRPS() As Geral.RPS
        Get
            Return Me._chaveRPS
        End Get
        Set(ByVal valor As Geral.RPS)
            Me._chaveRPS = valor
        End Set
    End Property
End Class