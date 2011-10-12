Imports System.Xml.Serialization
Namespace Envio

    Public Class Lote
        Private _id As String = String.Empty
        Private _RPS As List(Of Geral.RPS)
        <XmlAttributeAttribute()> _
           Public Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal valor As String)
                Me._id = valor
            End Set
        End Property
        <XmlElement()> _
        Property RPS() As List(Of Geral.RPS)
            Get
                Return Me._RPS
            End Get
            Set(ByVal valor As List(Of Geral.RPS))
                Me._RPS = valor
            End Set
        End Property

        Public Sub New()
            RPS = New List(Of Geral.RPS)
        End Sub
    End Class

End Namespace