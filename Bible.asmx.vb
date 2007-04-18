Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Configuration
Imports System.Text

<System.Web.Services.WebService(Namespace:="http://bible.sumerano.com/")> _
Public Class Bible
    Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Returns a list of available Bible translations.")> _
    Public Function GetTranslations() As BibleTranslations
        Dim bts As New BibleTranslations
        Dim blnSuccess As Boolean = bts.Find()

        Return bts
    End Function

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Returns a list of all books within the bible.  You have the option of returning all books or a specific section (Old Testament or New Testament).  If PopulateChapters and PopulateVerses are set to True, the *entire* Bible text will be returned.")> _
    Public Function GetBooks(ByVal TranslationNo As Integer, ByVal Section As BookSection, ByVal PopulateChapters As Boolean, ByVal PopulateVerses As Boolean) As Books
        Dim bs As New Books
        Dim blnSuccess As Boolean = bs.Find(TranslationNo, Section, PopulateChapters, PopulateVerses)

        Return bs
    End Function

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Returns a list of chapters within a particular Book.  If PopulateVerses is set to True, the verses within each chapter will be returned.")> _
    Public Function GetChapters(ByVal TranslationNo As Integer, ByVal BookNo As Integer, ByVal PopulateVerses As Boolean) As Chapters
        Dim cs As New Chapters
        Dim blnSuccess As Boolean = cs.Find(TranslationNo, BookNo, PopulateVerses)

        Return cs
    End Function

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Returns a list of verses within a particular Book and Chapter.")> _
    Public Function GetVerses(ByVal TranslationNo As Integer, ByVal BookNo As Integer, ByVal ChapterNo As Integer) As Verses
        Dim vs As New Verses
        Dim blnSuccess As Boolean = vs.Find(TranslationNo, BookNo, ChapterNo)

        Return vs
    End Function

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Returns a single verse within a particular Book and Chapter.")> _
    Public Function GetVerse(ByVal TranslationNo As Integer, ByVal BookNo As Integer, ByVal ChapterNo As Integer, ByVal VerseNo As Integer) As Verses
        Dim vs As New Verses
        Dim blnSuccess As Boolean = vs.Find(TranslationNo, BookNo, ChapterNo, VerseNo)

        Return vs
    End Function

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Returns a list of verses within a particular Book, Chapter and Verse range.")> _
    Public Function GetVerseRange(ByVal TranslationNo As Integer, ByVal BookNo As Integer, ByVal ChapterStartNo As Integer, ByVal ChapterEndNo As Integer, ByVal VerseStartNo As Integer, ByVal VerseEndNo As Integer) As Verses
        Dim vs As New Verses
        Dim blnSuccess As Boolean = vs.Find(TranslationNo, BookNo, ChapterStartNo, ChapterEndNo, VerseStartNo, VerseEndNo)

        Return vs
    End Function

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Returns a random verse.")> _
    Public Function GetRandomVerse(ByVal TranslationNo As Integer) As Books
        Dim vs As New Verses

        Return vs.Random(TranslationNo)
    End Function

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Returns definitions for a word or name from the Bible.")> _
    Public Function GetDefinitions(ByVal Word As String, ByVal MatchExact As Boolean) As Definitions
        Dim ds As New Definitions

        'If ValidateUser(UserToken) Then
        Dim blnSuccess As Boolean = ds.Find(Word.Trim, MatchExact)
        'Else
        '    Dim d As New Definition
        '    d.Word = "Invalid User"
        '    d.DefinitionText = "The token, username, or password supplied was invalid."
        '    ds.Add(d)
        'End If

        Return ds
    End Function

    <WebMethod(BufferResponse:=True, CacheDuration:=60, Description:="Performs a search on the Book Text and Verse Text of the entire Bible and returns the top 50 verses that match the kewords provided.  Set MatchAllWords to True to only match verses that contain all given keywords.  Delimiter can be set to any single character that separates each keyword.")> _
    Public Function SearchBible(ByVal TranslationNo As Integer, ByVal Keywords As String, ByVal Delimiter As String, ByVal MatchAllWords As Boolean) As SearchResults
        Dim srs As New SearchResults

        Dim blnSuccess As Boolean = srs.Find(TranslationNo, Keywords, Delimiter, MatchAllWords)

        Return srs
    End Function

    Private Function ValidateUser(ByVal st As SecurityToken) As Boolean
		Dim strSQL As String = "bible_ValidateUser"
		Dim cnn As New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
        Dim cmd As New SqlCommand(strSQL, cnn)
        Dim prmRetValue As New SqlParameter

        With prmRetValue
            .Direction = ParameterDirection.ReturnValue
            .ParameterName = "@RetValue"
            .SqlDbType = SqlDbType.Bit
        End With

        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add(prmRetValue)
        cmd.Parameters.Add(New SqlParameter("@Token", st.Token))
        cmd.Parameters.Add(New SqlParameter("@Username", st.Username))

        Dim hashedDataBytes As Byte()
        Dim encoder As New UTF8Encoding
        Dim md5Hasher As New MD5CryptoServiceProvider

        hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(st.Password))

        cmd.Parameters.Add(New SqlParameter("@Password", hashedDataBytes))

        cnn.Open()
        cmd.ExecuteNonQuery()
        cnn.Close()

        cmd.Dispose()
        cnn.Dispose()

        If Not IsNothing(prmRetValue.Value) Then
            Return CType(prmRetValue.Value, Boolean)
        Else
            Return False
        End If
    End Function
End Class
