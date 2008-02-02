using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace BibleWebService
{
    public class User
    {
        private readonly DateTime _CreatedOn;
        private readonly DateTime _Expires;
        private readonly string _Token;
        private readonly long _UserID;
        private string _Name;
        private string _Password;
        private string _Username;
        private string _WebSite;

        public User(DateTime _CreatedOn, DateTime _Expires, string _Token, long _UserID)
        {
            this._CreatedOn = _CreatedOn;
            this._UserID = _UserID;
            this._Token = _Token;
            this._Expires = _Expires;
        }

        public long UserID
        {
            get { return _UserID; }
        }

        public string Token
        {
            get { return _Token; }
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

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string WebSite
        {
            get { return _WebSite; }
            set { _WebSite = value; }
        }

        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
        }

        public DateTime Expires
        {
            get { return _Expires; }
        }

        public bool Add()
        {
            if (_Username != "" & _Password != "")
            {
                string strSQL = "bible_AddUser";
                SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
                SqlCommand cmd = new SqlCommand(strSQL, cnn);
                SqlParameter prmPassword = new SqlParameter();
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                int intRowsAffected;

                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(_Password));

                {
                    prmPassword.Direction = ParameterDirection.Input;
                    prmPassword.ParameterName = "@Password";
                    prmPassword.SqlDbType = SqlDbType.Binary;
                    prmPassword.Value = hashedBytes;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Username", _Username));
                cmd.Parameters.Add(prmPassword);
                cmd.Parameters.Add(new SqlParameter("@Name", _Name));
                cmd.Parameters.Add(new SqlParameter("@WebSite", _WebSite));
                cnn.Open();
                intRowsAffected = cmd.ExecuteNonQuery();
                cnn.Close();

                if (intRowsAffected == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Validate(string username, string password)
        {
            return Validate("", username, password);
        }

        public bool Validate(string token, string username, string password)
        {
            string strSQL = "bible_ValidateUser";
            SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings["DataConn"]);
            SqlCommand cmd = new SqlCommand(strSQL, cnn);
            SqlParameter prmRetVal = new SqlParameter();
            SqlParameter prmToken = new SqlParameter();
            SqlParameter prmPassword = new SqlParameter();
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();

            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(password));

            {
                prmRetVal.Direction = ParameterDirection.ReturnValue;
                prmRetVal.ParameterName = "@RetValue";
                prmRetVal.SqlDbType = SqlDbType.Int;
            }

            {
                prmToken.Direction = ParameterDirection.Input;
                prmToken.ParameterName = "@Token";
                prmToken.SqlDbType = SqlDbType.UniqueIdentifier;
                prmToken.Value = token;
            }

            {
                prmPassword.Direction = ParameterDirection.Input;
                prmPassword.ParameterName = "@Password";
                prmPassword.SqlDbType = SqlDbType.Binary;
                prmPassword.Value = hashedBytes;
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(prmRetVal);
            cmd.Parameters.Add(prmToken);
            cmd.Parameters.Add(new SqlParameter("@Username", username));
            cmd.Parameters.Add(prmPassword);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            return (bool) prmRetVal.Value;
        }
    }
}