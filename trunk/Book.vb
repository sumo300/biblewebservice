Public Class Book
    Private _BookNo As Integer
    Private _Section As String
    Private _BookText As String
    Private _Chapters As Chapters

    Public Property BookNo() As Integer
        Get
            Return _BookNo
        End Get
        Set(ByVal Value As Integer)
            _BookNo = Value
        End Set
    End Property

    Public Property Section() As String
        Get
            Return _Section
        End Get
        Set(ByVal Value As String)
            _Section = Value
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

	Public Property Chapters() As Chapters
		Get
			Return _Chapters
		End Get
		Set(ByVal Value As Chapters)
			_Chapters = Value
		End Set
	End Property
End Class
