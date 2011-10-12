Imports System.Xml.Serialization
Namespace Cancelamento

    Public Class Lote
        Private _id As String = String.Empty
        Private _nota As List(Of Cancelamento.Nota)
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
        Property Nota() As List(Of Cancelamento.Nota)
            Get
                Return Me._nota
            End Get
            Set(ByVal valor As List(Of Cancelamento.Nota))
                Me._nota = valor
            End Set
        End Property

        Public Sub New()
            Nota = New List(Of Cancelamento.Nota)
        End Sub
    End Class

End Namespace