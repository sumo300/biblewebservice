using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BibleWebService
{
    public class Verses : CollectionBase
    {
        public Verse this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        //public Verse Item {
        //    get { return (Verse)list(index); }
        //    set { list(index) = value; }
        //}

        public int Add(Verse v)
        {
            return Add(v);
        }

        public void Remove(Verse v)
        {
            Remove(v);
        }

        public bool Find(int BibleID, int BookID, int Chapter)
        {
            SqlDataReader dtr = null;

            try
            {
                string strSQL = "bible_GetChapterVerses";
                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                SqlCommand cmd = new SqlCommand(strSQL, cnn);
                bool blnHasRows;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BibleID", BibleID));
                cmd.Parameters.Add(new SqlParameter("@BookID", BookID));
                cmd.Parameters.Add(new SqlParameter("@Chapter", Chapter));

                cnn.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dtr.Read())
                {
                    Verse v = new Verse();

                    v.VerseNo = dtr.GetInt32(4);
                    v.VerseText = dtr.GetString(5);
                    Add(v);
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

                Verse v = new Verse();
                v.VerseNo = -1;
                v.VerseText = sx.ToString();
                List.Add(v);
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

                Verse v = new Verse();
                v.VerseNo = -1;
                v.VerseText = ex.ToString();
                List.Add(v);
                return false;
            }
        }

        public bool Find(int BibleID, int BookID, int Chapter, int Verse)
        {
            SqlDataReader dtr = null;

            try
            {
                string strSQL = "bible_GetChapterVerse";
                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                SqlCommand cmd = new SqlCommand(strSQL, cnn);
                bool blnHasRows;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BibleID", BibleID));
                cmd.Parameters.Add(new SqlParameter("@BookID", BookID));
                cmd.Parameters.Add(new SqlParameter("@Chapter", Chapter));
                cmd.Parameters.Add(new SqlParameter("@Verse", Verse));

                cnn.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dtr.Read())
                {
                    Verse v = new Verse();

                    v.VerseNo = dtr.GetInt32(4);
                    v.VerseText = dtr.GetString(5);
                    Add(v);
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

                Verse v = new Verse();
                v.VerseNo = -1;
                v.VerseText = sx.ToString();
                List.Add(v);
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

                Verse v = new Verse();
                v.VerseNo = -1;
                v.VerseText = ex.ToString();
                List.Add(v);
                return false;
            }
        }

        public bool Find(int BibleID, int BookID, int ChapterStart, int ChapterEnd, int VerseStart, int VerseEnd)
        {
            SqlDataReader dtr = null;

            try
            {
                string strSQL = "bible_GetVerseRange";
                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                SqlCommand cmd = new SqlCommand(strSQL, cnn);
                bool blnHasRows;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BibleID", BibleID));
                cmd.Parameters.Add(new SqlParameter("@BookID", BookID));
                cmd.Parameters.Add(new SqlParameter("@ChapterStart", ChapterStart));
                cmd.Parameters.Add(new SqlParameter("@ChapterEnd", ChapterEnd));
                cmd.Parameters.Add(new SqlParameter("@VerseStart", VerseStart));
                cmd.Parameters.Add(new SqlParameter("@VerseEnd", VerseEnd));

                cnn.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dtr.Read())
                {
                    Verse v = new Verse();

                    v.VerseNo = dtr.GetInt32(4);
                    v.VerseText = dtr.GetString(5);
                    Add(v);
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

                Verse v = new Verse();
                v.VerseNo = -1;
                v.VerseText = sx.ToString();
                List.Add(v);
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

                Verse v = new Verse();
                v.VerseNo = -1;
                v.VerseText = ex.ToString();
                List.Add(v);
                return false;
            }
        }


        public Books Random(int BibleID)
        {
            SqlDataReader dtr = null;

            try
            {
                string strSQL = "bible_RandomVerse";
                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                SqlCommand cmd = new SqlCommand(strSQL, cnn);
                Books bs = new Books();
                Chapters cs = new Chapters();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BibleID", BibleID));

                cnn.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dtr.Read())
                {
                    Book b = new Book();
                    Chapter c = new Chapter();
                    Verse v = new Verse();

                    b.Section = dtr.GetString(0);
                    b.BookText = dtr.GetString(1);
                    b.BookNo = dtr.GetInt32(2);
                    c.ChapterNo = dtr.GetInt32(3);
                    v.VerseNo = dtr.GetInt32(4);
                    v.VerseText = dtr.GetString(5);
                    Add(v);

                    c.Verses = this;
                    cs.Add(c);
                    b.Chapters = cs;
                    bs.Add(b);
                }

                dtr.Close();

                return bs;
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

                Books bs = new Books();
                Book b = new Book();
                b.BookNo = -1;
                b.BookText = sx.ToString();
                bs.Add(b);

                return bs;
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

                Books bs = new Books();
                Book b = new Book();
                b.BookNo = -1;
                b.BookText = ex.ToString();
                bs.Add(b);

                return bs;
            }
        }
    }
}