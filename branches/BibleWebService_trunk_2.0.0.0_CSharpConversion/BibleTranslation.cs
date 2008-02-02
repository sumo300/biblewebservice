namespace BibleWebService
{
    public class BibleTranslation
    {
        private string _BibleAbbr;
        private string _BibleName;
        private int _BibleNo;

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