Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Security.Cryptography
Imports System.Text

Public Class User
    Private _UserID As Long
    Private _Token As String
    Private _Username As String
    Private _Password As String
    Private _Name As String
    Private _WebSite As String
    Private _CreatedOn As DateTime
    Private _Expires As DateTime

    Public ReadOnly Property UserID() As Long
        Get
            Return _UserID
        End Get
    End Property

    Public ReadOnly Property Token() As String
        Get
            Return _Token
        End Get
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

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property

    Public Property WebSite() As String
        Get
            Return _WebSite
        End Get
        Set(ByVal Value As String)
            _WebSite = Value
        End Set
    End Property

    Public ReadOnly Property CreatedOn() As DateTime
        Get
            Return _CreatedOn
        End Get
    End Property

    Public ReadOnly Property Expires() As DateTime
        Get
            Return _Expires
        End Get
    End Property

    Public Function Add() As Boolean
        If _Username <> "" And _Password <> "" Then
            Dim strSQL As String = "bible_AddUser"
            Dim cnn As New SqlConnection(AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim prmPassword As New SqlParameter
            Dim md5Hasher As New MD5CryptoServiceProvider
            Dim hashedBytes As Byte()
            Dim encoder As New UTF8Encoding
            Dim intRowsAffected As Integer

            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(_Password))

            With prmPassword
                .Direction = ParameterDirection.Input
                .ParameterName = "@Password"
                .SqlDbType = SqlDbType.Binary
                .Value = hashedBytes
            End With

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@Username", _Username))
            cmd.Parameters.Add(prmPassword)
            cmd.Parameters.Add(New SqlParameter("@Name", _Name))
            cmd.Parameters.Add(New SqlParameter("@WebSite", _WebSite))
            cnn.Open()
            intRowsAffected = cmd.ExecuteNonQuery()
            cnn.Close()

            If intRowsAffected = 1 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Public Function Validate(ByVal Token As String, ByVal Username As String, ByVal Password As String) As Boolean
        Dim strSQL As String = "bible_ValidateUser"
        Dim cnn As New SqlConnection(AppSettings("DataConn"))
        Dim cmd As New SqlCommand(strSQL, cnn)
        Dim prmRetVal As New SqlParameter
        Dim prmToken As New SqlParameter
        Dim prmPassword As New SqlParameter
        Dim md5Hasher As New MD5CryptoServiceProvider
        Dim hashedBytes As Byte()
        Dim encoder As New UTF8Encoding
        Dim intRowsAffected As Integer

        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password))

        With prmRetVal
            .Direction = ParameterDirection.ReturnValue
            .ParameterName = "@RetValue"
            .SqlDbType = SqlDbType.Int
        End With

        With prmToken
            .Direction = ParameterDirection.Input
            .ParameterName = "@Token"
            .SqlDbType = SqlDbType.UniqueIdentifier
            .Value = Token
        End With

        With prmPassword
            .Direction = ParameterDirection.Input
            .ParameterName = "@Password"
            .SqlDbType = SqlDbType.Binary
            .Value = hashedBytes
        End With

        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(prmRetVal)
        cmd.Parameters.Add(prmToken)
        cmd.Parameters.Add(New SqlParameter("@Username", Username))
        cmd.Parameters.Add(prmPassword)
        cnn.Open()
        intRowsAffected = cmd.ExecuteNonQuery()
        cnn.Close()

        Return CType(prmRetVal.Value, Boolean)
    End Function
End Class
