namespace BibleWebService
{
    public class Book
    {
        private int _BookNo;
        private string _BookText;
        private Chapters _Chapters;
        private string _Section;

        public int BookNo
        {
            get { return _BookNo; }
            set { _BookNo = value; }
        }

        public string Section
        {
            get { return _Section; }
            set { _Section = value; }
        }

        public string BookText
        {
            get { return _BookText; }
            set { _BookText = value; }
        }

        public Chapters Chapters
        {
            get { return _Chapters; }
            set { _Chapters = value; }
        }
    }
}