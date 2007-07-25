Public Class Definition
    Private _Word As String
    Private _DefinitionText As String
    Private _ExactMatch As Boolean

    Public Property Word() As String
        Get
            Return _Word
        End Get
        Set(ByVal Value As String)
            _Word = Value
        End Set
    End Property

    Public Property DefinitionText() As String
        Get
            Return _DefinitionText
        End Get
        Set(ByVal Value As String)
            _DefinitionText = Value
        End Set
    End Property

    Public Property ExactMatch() As Boolean
        Get
            Return _ExactMatch
        End Get
        Set(ByVal Value As Boolean)
            _ExactMatch = Value
        End Set
    End Property
End Class
