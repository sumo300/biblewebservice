namespace BibleWebService
{
    public class Verse
    {
        private int _VerseNo;
        private string _VerseText;

        public int VerseNo
        {
            get { return _VerseNo; }
            set { _VerseNo = value; }
        }

        public string VerseText
        {
            get { return _VerseText; }
            set { _VerseText = value; }
        }
    }
}