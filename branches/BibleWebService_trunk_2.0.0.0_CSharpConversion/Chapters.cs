using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BibleWebService
{
    public class Chapters : CollectionBase
    {
        public Chapter this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        //public Chapter Item {
        //    get { return (Chapter)list(index); }
        //    set { list(index) = value; }
        //}

        public int Add(Chapter c)
        {
            return Add(c);
        }

        public void Remove(Chapter c)
        {
            Remove(c);
        }

        public bool Find(int BibleID, int BookID, bool PopulateVerses)
        {
            SqlDataReader dtr = null;

            try
            {
                string strSQL = "bible_GetChapters";
                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                SqlCommand cmd = new SqlCommand(strSQL, cnn);
                bool blnHasRows;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BibleID", BibleID));
                cmd.Parameters.Add(new SqlParameter("@BookID", BookID));

                cnn.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dtr.Read())
                {
                    Chapter c = new Chapter();
                    Verses vs = new Verses();

                    c.ChapterNo = dtr.GetInt32(0);

                    if (PopulateVerses)
                    {
                        vs.Find(BibleID, BookID, c.ChapterNo);
                        c.Verses = vs;
                    }

                    Add(c);
                }

                blnHasRows = dtr.HasRows;
                dtr.Close();
                return blnHasRows;
            }
            catch (Exception)
            {
                if (dtr != null)
                {
                    if (!dtr.IsClosed)
                    {
                        dtr.Close();
                    }
                }

                Chapter c = new Chapter();
                c.ChapterNo = -1;
                List.Add(c);
                return false;
            }
        }
    }
}