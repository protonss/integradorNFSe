Imports System.Xml.Serialization
Namespace ConsultaNFSeRPS

    Public Class Lote
        Private _id As String = String.Empty
        Private _notaConsulta As List(Of ConsultaNFSeRPS.Nota)
        Private _rpsConsulta As List(Of ConsultaNFSeRPS.RPS)
        <XmlAttributeAttribute()> _
           Public Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal valor As String)
                Me._id = valor
            End Set
        End Property
        Property NotaConsulta() As List(Of ConsultaNFSeRPS.Nota)
            Get
                Return Me._notaConsulta
            End Get
            Set(ByVal valor As List(Of ConsultaNFSeRPS.Nota))
                Me._notaConsulta = valor
            End Set
        End Property
        Property RPSConsulta() As List(Of ConsultaNFSeRPS.RPS)
            Get
                Return Me._rpsConsulta
            End Get
            Set(ByVal valor As List(Of ConsultaNFSeRPS.RPS))
                Me._rpsConsulta = valor
            End Set
        End Property
        Public Sub New()
            NotaConsulta = New List(Of ConsultaNFSeRPS.Nota)
            RPSConsulta = New List(Of ConsultaNFSeRPS.RPS)
        End Sub
    End Class

End Namespace