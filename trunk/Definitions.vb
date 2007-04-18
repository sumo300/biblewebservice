Imports System.Data.SqlClient
Imports System.Configuration

Public Class Definitions
    Inherits CollectionBase

    Default Public Property Item(ByVal index As Integer) As Definition
        Get
            Return CType(list(index), Definition)
        End Get
        Set(ByVal Value As Definition)
            list(index) = Value
        End Set
    End Property

    Public Function Add(ByVal d As Definition) As Integer
        Return list.Add(d)
    End Function

    Public Sub Remove(ByVal d As Definition)
        list.Remove(d)
    End Sub

    Public Function Find(ByVal Word As String, ByVal MatchExact As Boolean) As Boolean
        Return GetDefinition(Word, MatchExact)
    End Function

    Private Function GetDefinition(ByVal Word As String, ByVal MatchExact As Boolean) As Boolean
		Dim dtr As SqlDataReader = Nothing

        Try
            Dim strSQL As String = "bible_SearchDictionary_Eastons"
			Dim cnn As New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim blnHasRows As Boolean

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@word", Word))

            If MatchExact Then
                cmd.Parameters.Add(New SqlParameter("@exactmatch", 1))
            End If

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Do While dtr.Read
                Dim d As New Definition

                d.Word = dtr.GetString(0)
                d.DefinitionText = dtr.GetString(1)
                d.ExactMatch = d.word.ToLower.Equals(Word.ToLower)

                list.Add(d)
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

			Dim d As New Definition
			d.Word = "Error"
			d.DefinitionText = ex.ToString
			List.Add(d)
			Return False
        End Try
    End Function
End Class
