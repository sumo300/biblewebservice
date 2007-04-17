Public Class SecurityToken
    Private _Token As String
    Private _Username As String
    Private _Password As String
    Private _EncryptedPassword As String

    Public Property Token() As String
        Get
            Return _Token
        End Get
        Set(ByVal Value As String)
            _Token = Value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return _Username
        End Get
        Set(ByVal Value As String)
            _Username = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal Value As String)
            _Password = Value
        End Set
    End Property
End Class
