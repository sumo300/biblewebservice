Imports System.Data.SqlClient
Imports System.Configuration

Public Class Verses
    Inherits CollectionBase

    Default Public Property Item(ByVal index As Integer) As Verse
        Get
            Return CType(list(index), Verse)
        End Get
        Set(ByVal Value As Verse)
            list(index) = Value
        End Set
    End Property

    Public Function Add(ByVal v As Verse) As Integer
        Return list.Add(v)
    End Function

    Public Sub Remove(ByVal v As Verse)
        list.Remove(v)
    End Sub

    Public Function Find(ByVal BibleID As Integer, ByVal BookID As Integer, ByVal Chapter As Integer) As Boolean
		Dim dtr As SqlDataReader = Nothing

        Try
            Dim strSQL As String = "bible_GetChapterVerses"
			Dim cnn As New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim blnHasRows As Boolean

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@BibleID", BibleID))
            cmd.Parameters.Add(New SqlParameter("@BookID", BookID))
            cmd.Parameters.Add(New SqlParameter("@Chapter", Chapter))

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Do While dtr.Read
                Dim v As New Verse

                v.VerseNo = dtr.GetInt32(4)
                v.VerseText = dtr.GetString(5)
                list.Add(v)
            Loop

            blnHasRows = dtr.HasRows
            dtr.Close()
            Return blnHasRows
		Catch sx As SqlException
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim v As New Verse
			v.VerseNo = -1
			v.VerseText = sx.ToString
			List.Add(v)
			Return False
		Catch ex As Exception
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim v As New Verse
			v.VerseNo = -1
			v.VerseText = ex.ToString
			List.Add(v)
			Return False
        End Try
    End Function

    Public Function Find(ByVal BibleID As Integer, ByVal BookID As Integer, ByVal Chapter As Integer, ByVal Verse As Integer) As Boolean
		Dim dtr As SqlDataReader = Nothing

        Try
            Dim strSQL As String = "bible_GetChapterVerse"
			Dim cnn As New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim blnHasRows As Boolean

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@BibleID", BibleID))
            cmd.Parameters.Add(New SqlParameter("@BookID", BookID))
            cmd.Parameters.Add(New SqlParameter("@Chapter", Chapter))
            cmd.Parameters.Add(New SqlParameter("@Verse", Verse))

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Do While dtr.Read
                Dim v As New Verse

                v.VerseNo = dtr.GetInt32(4)
                v.VerseText = dtr.GetString(5)
                list.Add(v)
            Loop

            blnHasRows = dtr.HasRows
            dtr.Close()
            Return blnHasRows
		Catch sx As SqlException
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim v As New Verse
			v.VerseNo = -1
			v.VerseText = sx.ToString
			List.Add(v)
			Return False
		Catch ex As Exception
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim v As New Verse
			v.VerseNo = -1
			v.VerseText = ex.ToString
			List.Add(v)
			Return False
        End Try
    End Function

    Public Function Find(ByVal BibleID As Integer, ByVal BookID As Integer, ByVal ChapterStart As Integer, ByVal ChapterEnd As Integer, ByVal VerseStart As Integer, ByVal VerseEnd As Integer) As Boolean
		Dim dtr As SqlDataReader = Nothing

        Try
            Dim strSQL As String = "bible_GetVerseRange"
			Dim cnn As New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim blnHasRows As Boolean

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@BibleID", BibleID))
            cmd.Parameters.Add(New SqlParameter("@BookID", BookID))
            cmd.Parameters.Add(New SqlParameter("@ChapterStart", ChapterStart))
            cmd.Parameters.Add(New SqlParameter("@ChapterEnd", ChapterEnd))
            cmd.Parameters.Add(New SqlParameter("@VerseStart", VerseStart))
            cmd.Parameters.Add(New SqlParameter("@VerseEnd", VerseEnd))

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Do While dtr.Read
                Dim v As New Verse

                v.VerseNo = dtr.GetInt32(4)
                v.VerseText = dtr.GetString(5)
                list.Add(v)
            Loop

            blnHasRows = dtr.HasRows
            dtr.Close()
            Return blnHasRows
		Catch sx As SqlException
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim v As New Verse
			v.VerseNo = -1
			v.VerseText = sx.ToString
			List.Add(v)
			Return False
		Catch ex As Exception
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim v As New Verse
			v.VerseNo = -1
			v.VerseText = ex.ToString
			List.Add(v)
			Return False
        End Try
    End Function


    Public Function Random(ByVal BibleID As Integer) As Books
		Dim dtr As SqlDataReader = Nothing

        Try
            Dim strSQL As String = "bible_RandomVerse"
			Dim cnn As New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim blnHasRows As Boolean
            Dim bs As New Books
            Dim cs As New Chapters

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@BibleID", BibleID))

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtr.Read Then
                Dim b As New Book
                Dim c As New Chapter
                Dim v As New Verse

                b.Section = dtr.GetString(0)
                b.BookText = dtr.GetString(1)
                b.BookNo = dtr.GetInt32(2)
                c.ChapterNo = dtr.GetInt32(3)
                v.VerseNo = dtr.GetInt32(4)
                v.VerseText = dtr.GetString(5)
                list.Add(v)

                c.Verses = Me
                cs.Add(c)
                b.Chapters = cs
                bs.Add(b)
            End If

            blnHasRows = dtr.HasRows
            dtr.Close()

            Return bs
		Catch sx As SqlException
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim bs As New Books
			Dim b As New Book
			b.BookNo = -1
			b.BookText = sx.ToString
			bs.Add(b)

			Return bs
		Catch ex As Exception
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim bs As New Books
			Dim b As New Book
			b.BookNo = -1
			b.BookText = ex.ToString
			bs.Add(b)

			Return bs
        End Try
    End Function
End Class
