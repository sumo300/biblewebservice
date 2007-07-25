Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Configuration

Public Class BibleTranslations
    Inherits CollectionBase

    Default Public Property Item(ByVal index As Integer) As BibleTranslation
        Get
            Return CType(list(index), BibleTranslation)
        End Get
        Set(ByVal Value As BibleTranslation)
            list(index) = Value
        End Set
    End Property

    Public Function Add(ByVal bt As BibleTranslation) As Integer
        Return list.Add(bt)
    End Function

    Public Sub Remove(ByVal bt As BibleTranslation)
        list.Remove(bt)
    End Sub

    Public Function Find() As Boolean
		Dim dtr As SqlDataReader = Nothing

        Try
            Dim strSQL As String = "bible_GetTranslations"
			Dim cnn As New SqlConnection(ConfigurationManager.AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim blnHasRows As Boolean

            cmd.CommandType = CommandType.StoredProcedure

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Do While dtr.Read
                Dim bt As New BibleTranslation

                bt.BibleNo = dtr.GetInt32(0)
                bt.BibleAbbr = dtr.GetString(1)
                bt.BibleName = dtr.GetString(2)

                list.Add(bt)
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

			Dim bt As New BibleTranslation
			bt.BibleNo = -1
			bt.BibleName = sx.ToString
			List.Add(bt)
			Return False
        Catch ex As Exception
            If Not dtr.IsClosed Then
                dtr.Close()
            End If

            Dim bt As New BibleTranslation
            bt.BibleNo = -1
            bt.BibleName = ex.ToString
            list.Add(bt)
            Return False
        End Try
    End Function
End Class
