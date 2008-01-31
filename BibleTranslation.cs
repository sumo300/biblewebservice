namespace BibleWebService
{
    public class BibleTranslation
    {
        private int _BibleNo;
        private string _BibleAbbr;
        private string _BibleName;

        public int BibleNo
        {
            get { return _BibleNo; }
            set { _BibleNo = value; }
        }

        public string BibleAbbr
        {
            get { return _BibleAbbr; }
            set { _BibleAbbr = value; }
        }

        public string BibleName
        {
            get { return _BibleName; }
            set { _BibleName = value; }
        }
    }
}