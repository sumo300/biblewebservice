Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Public Class Books
    Inherits CollectionBase

    Default Public Property Item(ByVal index As Integer) As Book
        Get
            Return CType(list(index), Book)
        End Get
        Set(ByVal Value As Book)
            list(index) = Value
        End Set
    End Property

    Public Function Add(ByVal b As Book) As Integer
        Return list.Add(b)
    End Function

    Public Sub Remove(ByVal b As Book)
        list.Remove(b)
    End Sub

    Public Function Find(ByVal BibleID As Integer, ByVal Section As BookSection, ByVal PopulateChapters As Boolean, ByVal PopulateVerses As Boolean) As Boolean
        Dim dtr As SqlDataReader

        Try
            Dim strSQL As String = "bible_GetBooks"
            Dim cnn As New SqlConnection(AppSettings("DataConn"))
            Dim cmd As New SqlCommand(strSQL, cnn)
            Dim blnHasRows As Boolean
            Dim strSection As String

            Select Case Section
                Case BookSection.OldTestament
                    strSection = "OT"
                Case BookSection.NewTestament
                    strSection = "NT"
                Case BookSection.All
                    strSection = ""
                Case Else
                    strSection = ""
            End Select

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@Section", strSection))

            cnn.Open()
            dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Do While dtr.Read
                Dim b As New Book
                Dim cs As New Chapters

                b.BookNo = dtr.GetInt32(0)
                b.Section = dtr.GetString(1)
                b.BookText = dtr.GetString(2)

                If PopulateChapters Then
                    cs.Find(BibleID, b.BookNo, PopulateVerses)
                    b.Chapters = cs
                End If

                list.Add(b)
            Loop

            blnHasRows = dtr.HasRows
            dtr.Close()
            Return blnHasRows
        Catch sx As SqlException
            If Not dtr.IsClosed Then
                dtr.Close()
            End If

            Dim b As New Book
            b.BookNo = -1
            b.Booktext = sx.ToString
            list.Add(b)
            Return False
        Catch ex As Exception
            If Not dtr.IsClosed Then
                dtr.Close()
            End If

            Dim b As New Book
            b.BookNo = -1
            b.Booktext = ex.ToString
            list.Add(b)
            Return False
        End Try
    End Function
End Class
