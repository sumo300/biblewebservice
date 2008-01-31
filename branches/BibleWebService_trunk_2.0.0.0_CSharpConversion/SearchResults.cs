using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data;
using System;

namespace BibleWebService
{
    public class SearchResults : CollectionBase
    {
        public SearchResult this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        //public SearchResult Item {
        //    get { return (SearchResult)list(index); }
        //    set { list(index) = value; }
        //}

        public int Add(SearchResult sr)
        {
            return this.Add(sr);
        }

        public void Remove(SearchResult sr)
        {
            this.Remove(sr);
        }

        public bool Find(int BibleID, string Keywords, string Delimiter, bool AllWords)
        {
            SqlDataReader dtr = null;
            SqlConnection cnn = null;
            SqlCommand cmd = null;

            try
            {
                string strSQL = "bible_Search";
                bool blnHasRows;

                cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                cmd = new SqlCommand(strSQL, cnn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BibleID", BibleID));
                cmd.Parameters.Add(new SqlParameter("@SearchTerms", Keywords));
                cmd.Parameters.Add(new SqlParameter("@Delimiter", Delimiter));
                cmd.Parameters.Add(new SqlParameter("@AllWords", AllWords));

                cnn.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dtr.Read())
                {
                    SearchResult sr = new SearchResult();

                    sr.Section = dtr.GetString(1);
                    sr.BookNo = dtr.GetInt32(2);
                    sr.BookText = dtr.GetString(3);
                    sr.ChapterNo = dtr.GetInt32(4);
                    sr.VerseNo = dtr.GetInt32(5);
                    sr.VerseText = dtr.GetString(6);
                    sr.MatchCount = dtr.GetInt32(7);
                    this.Add(sr);
                }

                blnHasRows = dtr.HasRows;
                dtr.Close();

                return blnHasRows;
            }
            catch (SqlException sx)
            {
                if (dtr != null)
                {
                    if (!dtr.IsClosed)
                    {
                        dtr.Close();
                    }
                }

                SearchResult sr = new SearchResult();
                sr.BookNo = -1;
                sr.VerseText = sx.ToString();
                List.Add(sr);
                return false;
            }
            catch (Exception ex)
            {
                if (dtr != null)
                {
                    if (!dtr.IsClosed)
                    {
                        dtr.Close();
                    }
                }

                SearchResult sr = new SearchResult();
                sr.BookNo = -1;
                sr.VerseText = ex.ToString();
                List.Add(sr);
                return false;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (cnn != null)
                {
                    cnn.Dispose();
                }
            }
        }
    }
}