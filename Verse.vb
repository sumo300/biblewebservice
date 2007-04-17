Public Class Verse
    Private _VerseNo As Integer
    Private _VerseText As String

    Public Property VerseNo() As Integer
        Get
            Return _VerseNo
        End Get
        Set(ByVal Value As Integer)
            _VerseNo = Value
        End Set
    End Property

    Public Property VerseText() As String
        Get
            Return _VerseText
        End Get
        Set(ByVal Value As String)
            _VerseText = Value
        End Set
    End Property
End Class
