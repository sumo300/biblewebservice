namespace BibleWebService
{
    public class SecurityToken
    {
        private string _Token;
        private string _Username;
        private string _Password;
        private string _EncryptedPassword;

        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
    }
}