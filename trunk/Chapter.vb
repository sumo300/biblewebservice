Public Class Chapter
    Private _ChapterNo As Integer
    Private _Verses As Verses

    Public Property ChapterNo() As Integer
        Get
            Return _ChapterNo
        End Get
        Set(ByVal Value As Integer)
            _ChapterNo = Value
        End Set
    End Property

    Public Property Verses() As Verses
        Get
            Return _Verses
        End Get
        Set(ByVal Value As Verses)
            _Verses = Value
        End Set
    End Property
End Class
