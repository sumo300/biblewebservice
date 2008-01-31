using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

public class User
{
	private long _UserID;
	private string _Token;
	private string _Username;
	private string _Password;
	private string _Name;
	private string _WebSite;
	private DateTime _CreatedOn;
	private DateTime _Expires;

	public long UserID {
		get { return _UserID; }
	}

	public string Token {
		get { return _Token; }
	}

	public string Username {
		get { return _Username; }
		set { _Username = value; }
	}

	public string Password {
		get { return _Password; }
		set { _Password = value; }
	}

	public string Name {
		get { return _Name; }
		set { _Name = value; }
	}

	public string WebSite {
		get { return _WebSite; }
		set { _WebSite = value; }
	}

	public DateTime CreatedOn {
		get { return _CreatedOn; }
	}

	public DateTime Expires {
		get { return _Expires; }
	}

	public bool Add()
	{
		if (_Username != "" & _Password != "")
		{
			string strSQL = "bible_AddUser";
			SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings("DataConn"));
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

	public bool Validate(string Token, string Username, string Password)
	{
		string strSQL = "bible_ValidateUser";
		SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings("DataConn"));
		SqlCommand cmd = new SqlCommand(strSQL, cnn);
		SqlParameter prmRetVal = new SqlParameter();
		SqlParameter prmToken = new SqlParameter();
		SqlParameter prmPassword = new SqlParameter();
		MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
		byte[] hashedBytes;
		UTF8Encoding encoder = new UTF8Encoding();
		int intRowsAffected;

		hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password));

		{
			prmRetVal.Direction = ParameterDirection.ReturnValue;
			prmRetVal.ParameterName = "@RetValue";
			prmRetVal.SqlDbType = SqlDbType.Int;
		}

		{
			prmToken.Direction = ParameterDirection.Input;
			prmToken.ParameterName = "@Token";
			prmToken.SqlDbType = SqlDbType.UniqueIdentifier;
			prmToken.Value = Token;
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
		cmd.Parameters.Add(new SqlParameter("@Username", Username));
		cmd.Parameters.Add(prmPassword);
		cnn.Open();
		intRowsAffected = cmd.ExecuteNonQuery();
		cnn.Close();

		return (bool)prmRetVal.Value;
	}
}
