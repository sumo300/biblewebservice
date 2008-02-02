using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BibleWebService
{
    public class BibleTranslations : CollectionBase
    {
        public BibleTranslation this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        //public BibleTranslation Item {
        //    get { return (BibleTranslation)list(index); }
        //    set { list(index) = value; }
        //}

        public int Add(BibleTranslation bt)
        {
            return Add(bt);
        }

        public void Remove(BibleTranslation bt)
        {
            Remove(bt);
        }

        public bool Find()
        {
            SqlDataReader dtr = null;

            try
            {
                string strSQL = "bible_GetTranslations";
                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                SqlCommand cmd = new SqlCommand(strSQL, cnn);
                bool blnHasRows;

                cmd.CommandType = CommandType.StoredProcedure;

                cnn.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dtr.Read())
                {
                    BibleTranslation bt = new BibleTranslation();

                    bt.BibleNo = dtr.GetInt32(0);
                    bt.BibleAbbr = dtr.GetString(1);
                    bt.BibleName = dtr.GetString(2);

                    Add(bt);
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

                BibleTranslation bt = new BibleTranslation();
                bt.BibleNo = -1;
                bt.BibleName = sx.ToString();
                List.Add(bt);
                return false;
            }
            catch (Exception ex)
            {
                if (dtr != null)
                    if (!dtr.IsClosed)
                    {
                        dtr.Close();
                    }

                BibleTranslation bt = new BibleTranslation();
                bt.BibleNo = -1;
                bt.BibleName = ex.ToString();
                Add(bt);
                return false;
            }
        }
    }
}