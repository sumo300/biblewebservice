using System.Web.Services;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;
using System.Text;
using System.Data;

namespace BibleWebService
{
    [System.Web.Services.WebService(Namespace = "http://bible.sumerano.com/")]
    public class Bible : System.Web.Services.WebService
    {

        #region " Web Services Designer Generated Code "

        public Bible()
            : base()
        {

            //This call is required by the Web Services Designer.
            InitializeComponent();

            //Add your own initialization code after the InitializeComponent() call

        }

        //Required by the Web Services Designer
        private System.ComponentModel.IContainer components;

        //NOTE: The following procedure is required by the Web Services Designer
        //It can be modified using the Web Services Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        protected override void Dispose(bool disposing)
        {
            //CODEGEN: This procedure is required by the Web Services Designer
            //Do not modify it using the code editor.
            if (disposing)
            {
                if ((components != null))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Returns a list of available Bible translations.")]
        public BibleTranslations GetTranslations()
        {
            BibleTranslations bts = new BibleTranslations();
            bts.Find();

            return bts;
        }

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Returns a list of all books within the bible.  You have the option of returning all books or a specific section (Old Testament or New Testament).  If PopulateChapters and PopulateVerses are set to True, the *entire* Bible text will be returned.")]
        public Books GetBooks(int TranslationNo, BookSection Section, bool PopulateChapters, bool PopulateVerses)
        {
            Books bs = new Books();
            bs.Find(TranslationNo, Section, PopulateChapters, PopulateVerses);

            return bs;
        }

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Returns a list of chapters within a particular Book.  If PopulateVerses is set to True, the verses within each chapter will be returned.")]
        public Chapters GetChapters(int TranslationNo, int BookNo, bool PopulateVerses)
        {
            Chapters cs = new Chapters();
            cs.Find(TranslationNo, BookNo, PopulateVerses);

            return cs;
        }

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Returns a list of verses within a particular Book and Chapter.")]
        public Verses GetVerses(int TranslationNo, int BookNo, int ChapterNo)
        {
            Verses vs = new Verses();
            vs.Find(TranslationNo, BookNo, ChapterNo);

            return vs;
        }

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Returns a single verse within a particular Book and Chapter.")]
        public Verses GetVerse(int TranslationNo, int BookNo, int ChapterNo, int VerseNo)
        {
            Verses vs = new Verses();
            vs.Find(TranslationNo, BookNo, ChapterNo, VerseNo);

            return vs;
        }

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Returns a list of verses within a particular Book, Chapter and Verse range.")]
        public Verses GetVerseRange(int TranslationNo, int BookNo, int ChapterStartNo, int ChapterEndNo, int VerseStartNo, int VerseEndNo)
        {
            Verses vs = new Verses();
            vs.Find(TranslationNo, BookNo, ChapterStartNo, ChapterEndNo, VerseStartNo, VerseEndNo);

            return vs;
        }

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Returns a random verse.")]
        public Books GetRandomVerse(int TranslationNo)
        {
            Verses vs = new Verses();

            return vs.Random(TranslationNo);
        }

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Returns definitions for a word or name from the Bible.")]
        public Definitions GetDefinitions(string Word, bool MatchExact)
        {
            Definitions ds = new Definitions();

            //If ValidateUser(UserToken) Then
            ds.Find(Word.Trim(), MatchExact);
            //Else
            //    Dim d As New Definition
            //    d.Word = "Invalid User"
            //    d.DefinitionText = "The token, username, or password supplied was invalid."
            //    ds.Add(d)
            //End If

            return ds;
        }

        [WebMethod(BufferResponse = true, CacheDuration = 60, Description = "Performs a search on the Book Text and Verse Text of the entire Bible and returns the top 50 verses that match the kewords provided.  Set MatchAllWords to True to only match verses that contain all given keywords.  Delimiter can be set to any single character that separates each keyword.")]
        public SearchResults SearchBible(int TranslationNo, string Keywords, string Delimiter, bool MatchAllWords)
        {
            SearchResults srs = new SearchResults();

            srs.Find(TranslationNo, Keywords, Delimiter, MatchAllWords);

            return srs;
        }

        private static bool ValidateUser(SecurityToken st)
        {
            string strSQL = "bible_ValidateUser";
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
            SqlCommand cmd = new SqlCommand(strSQL, cnn);
            SqlParameter prmRetValue = new SqlParameter();

            {
                prmRetValue.Direction = ParameterDirection.ReturnValue;
                prmRetValue.ParameterName = "@RetValue";
                prmRetValue.SqlDbType = SqlDbType.Bit;
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(prmRetValue);
            cmd.Parameters.Add(new SqlParameter("@Token", st.Token));
            cmd.Parameters.Add(new SqlParameter("@Username", st.Username));

            byte[] hashedDataBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(st.Password));

            cmd.Parameters.Add(new SqlParameter("@Password", hashedDataBytes));

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            cmd.Dispose();
            cnn.Dispose();

            if ((prmRetValue.Value != null))
            {
                return (bool)prmRetValue.Value;
            }
            else
            {
                return false;
            }
        }
    }
}