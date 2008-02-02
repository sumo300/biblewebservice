using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BibleWebService
{
    public class Definitions : CollectionBase
    {
        public Definition this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        //public Definition Item {
        //    get { return (Definition)list(index); }
        //    set { list(index) = value; }
        //}

        public int Add(Definition d)
        {
            return Add(d);
        }

        public void Remove(Definition d)
        {
            Remove(d);
        }

        public bool Find(string Word, bool MatchExact)
        {
            return GetDefinition(Word, MatchExact);
        }

        private bool GetDefinition(string Word, bool MatchExact)
        {
            SqlDataReader dtr = null;

            try
            {
                string strSQL = "bible_SearchDictionary_Eastons";
                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                SqlCommand cmd = new SqlCommand(strSQL, cnn);
                bool blnHasRows;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@word", Word));

                if (MatchExact)
                {
                    cmd.Parameters.Add(new SqlParameter("@exactmatch", 1));
                }

                cnn.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dtr.Read())
                {
                    Definition d = new Definition();

                    d.Word = dtr.GetString(0);
                    d.DefinitionText = dtr.GetString(1);
                    //d.ExactMatch = d.Word.ToLower.Equals(Word.ToLower)
                    d.ExactMatch = (string.Compare(d.Word, Word, true) == 0);

                    Add(d);
                }

                blnHasRows = dtr.HasRows;
                dtr.Close();
                return blnHasRows;
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

                Definition d = new Definition();
                d.Word = "Error";
                d.DefinitionText = ex.ToString();
                List.Add(d);
                return false;
            }
        }
    }
}