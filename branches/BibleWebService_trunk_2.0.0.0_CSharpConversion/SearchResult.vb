Public Class SearchResult
    Private _MatchCount As Integer
    Private _Section As String
    Private _BookNo As Integer
    Private _BookText As String
    Private _ChapterNo As Integer
    Private _VerseNo As Integer
    Private _VerseText As String

    Public Property MatchCount() As Integer
        Get
            Return _MatchCount
        End Get
        Set(ByVal Value As Integer)
            _MatchCount = Value
        End Set
    End Property

    Public Property Section() As String
        Get
            Select Case _Section
                Case "OT"
                    Return "Old Testament"
                Case "NT"
                    Return "New Testament"
                Case Else
                    Return ""
            End Select
        End Get
        Set(ByVal Value As String)
            _Section = Value
        End Set
    End Property

    Public Property BookNo() As Integer
        Get
            Return _BookNo
        End Get
        Set(ByVal Value As Integer)
            _BookNo = Value
        End Set
    End Property

    Public Property BookText() As String
        Get
            Return _BookText
        End Get
        Set(ByVal Value As String)
            _BookText = Value
        End Set
    End Property

    Public Property ChapterNo() As Integer
        Get
            Return _ChapterNo
        End Get
        Set(ByVal Value As Integer)
            _ChapterNo = Value
        End Set
    End Property

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
