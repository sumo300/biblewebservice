Imports System.Data.SqlClient
Imports System.Configuration

Public Class SearchResults
    Inherits CollectionBase

    Default Public Property Item(ByVal index As Integer) As SearchResult
        Get
            Return CType(list(index), SearchResult)
        End Get
        Set(ByVal Value As SearchResult)
            list(index) = Value
        End Set
    End Property

    Public Function Add(ByVal sr As SearchResult) As Integer
        Return list.Add(sr)
    End Function

    Public Sub Remove(ByVal sr As SearchResult)
        list.Remove(sr)
    End Sub

    Public Function Find(ByVal BibleID As Integer, ByVal Keywords As String, ByVal Delimiter As String, ByVal AllWords As Boolean) As Boolean
		Dim dtr As SqlDataReader = Nothing
		Dim cnn As SqlConnection = Nothing
		Dim cmd As SqlCommand = Nothing

        Try
            Dim strSQL As String = "bible_Search"
            Dim blnHasRows As Boolean

			cnn = New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
            cmd = New SqlCommand(strSQL, cnn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@BibleID", BibleID))
            cmd.Parameters.Add(New SqlParameter("@SearchTerms", Keywords))
            cmd.Parameters.Add(New SqlParameter("@Delimiter", Delimiter))
            cmd.Parameters.Add(New SqlParameter("@AllWords", AllWords))

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Do While dtr.Read
                Dim sr As New SearchResult

                sr.Section = dtr.GetString(1)
                sr.BookNo = dtr.GetInt32(2)
                sr.BookText = dtr.GetString(3)
                sr.ChapterNo = dtr.GetInt32(4)
                sr.VerseNo = dtr.GetInt32(5)
                sr.VerseText = dtr.GetString(6)
                sr.MatchCount = dtr.GetInt32(7)
                list.Add(sr)
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

			Dim sr As New SearchResult
			sr.BookNo = -1
			sr.VerseText = sx.ToString
			List.Add(sr)
			Return False
		Catch ex As Exception
			If dtr IsNot Nothing Then
				If Not dtr.IsClosed Then
					dtr.Close()
				End If
			End If

			Dim sr As New SearchResult
			sr.BookNo = -1
			sr.VerseText = ex.ToString
			List.Add(sr)
			Return False
        Finally
			If cmd IsNot Nothing Then
				cmd.Dispose()
			End If

			If cnn IsNot Nothing Then
				cnn.Dispose()
			End If
        End Try
    End Function
End Class
