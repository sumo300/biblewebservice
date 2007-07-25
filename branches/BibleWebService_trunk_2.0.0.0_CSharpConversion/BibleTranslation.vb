Public Class BibleTranslation
    Private _BibleNo As Integer
    Private _BibleAbbr As String
    Private _BibleName As String

    Public Property BibleNo() As Integer
        Get
            Return _BibleNo
        End Get
        Set(ByVal Value As Integer)
            _BibleNo = Value
        End Set
    End Property

    Public Property BibleAbbr() As String
        Get
            Return _BibleAbbr
        End Get
        Set(ByVal Value As String)
            _BibleAbbr = Value
        End Set
    End Property

    Public Property BibleName() As String
        Get
            Return _BibleName
        End Get
        Set(ByVal Value As String)
            _BibleName = Value
        End Set
    End Property
End Class
