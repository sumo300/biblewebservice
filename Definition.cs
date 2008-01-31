namespace BibleWebService
{
    public class Definition
    {
        private string _Word;
        private string _DefinitionText;
        private bool _ExactMatch;

        public string Word
        {
            get { return _Word; }
            set { _Word = value; }
        }

        public string DefinitionText
        {
            get { return _DefinitionText; }
            set { _DefinitionText = value; }
        }

        public bool ExactMatch
        {
            get { return _ExactMatch; }
            set { _ExactMatch = value; }
        }
    }
}