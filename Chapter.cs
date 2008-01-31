namespace BibleWebService
{
    public class Chapter
    {
        private int _ChapterNo;
        private Verses _Verses;

        public int ChapterNo
        {
            get { return _ChapterNo; }
            set { _ChapterNo = value; }
        }

        public Verses Verses
        {
            get { return _Verses; }
            set { _Verses = value; }
        }
    }
}