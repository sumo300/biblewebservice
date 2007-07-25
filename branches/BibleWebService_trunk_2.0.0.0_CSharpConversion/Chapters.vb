Imports System.Data.SqlClient
Imports System.Configuration

Public Class Chapters
    Inherits CollectionBase

    Default Public Property Item(ByVal index As Integer) As Chapter
        Get
            Return CType(list(index), Chapter)
        End Get
        Set(ByVal Value As Chapter)
            list(index) = Value
        End Set
    End Property

    Public Function Add(ByVal c As Chapter) As Integer
        Return list.Add(c)
    End Function

    Public Sub Remove(ByVal c As Chapter)
        list.Remove(c)
    End Sub

    Public Function Find(ByVal BibleID As Integer, ByVal BookID As Integer, ByVal PopulateVerses As Boolean) As Boolean
		Dim dtr As SqlDataReader = Nothing

        Try
            Dim strSQL As String = "bible_GetChapters"
			Dim cnn As New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim blnHasRows As Boolean

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@BibleID", BibleID))
            cmd.Parameters.Add(New SqlParameter("@BookID", BookID))

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Do While dtr.Read
                Dim c As New Chapter
                Dim vs As New Verses

                c.Chapterno = dtr.GetInt32(0)

                If PopulateVerses Then
                    vs.Find(BibleID, BookID, c.ChapterNo)
                    c.Verses = vs
                End If

                list.Add(c)
            Loop

            blnHasRows = dtr.HasRows
            dtr.Close()
            Return blnHasRows
		Catch ex As Exception
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim c As New Chapter
			c.ChapterNo = -1
			List.Add(c)
			Return False
        End Try
    End Function
End Class
