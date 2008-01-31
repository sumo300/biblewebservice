using System.Data.SqlClient;
using System.Configuration.ConfigurationSettings;
using System.Configuration;

public class BibleTranslations : CollectionBase
{

	public BibleTranslation Item {
		get { return (BibleTranslation)list(index); }
		set { list(index) = value; }
	}

	public int Add(BibleTranslation bt)
	{
		return list.Add(bt);
	}

	public void Remove(BibleTranslation bt)
	{
		list.Remove(bt);
	}

	public bool Find()
	{
		SqlDataReader dtr = null;

		try {
			string strSQL = "bible_GetTranslations";
			SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings("DataConn"));
			SqlCommand cmd = new SqlCommand(strSQL, cnn);
			bool blnHasRows;

			cmd.CommandType = CommandType.StoredProcedure;

			cnn.Open();
			dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

			while (dtr.Read) {
				BibleTranslation bt = new BibleTranslation();

				bt.BibleNo = dtr.GetInt32(0);
				bt.BibleAbbr = dtr.GetString(1);
				bt.BibleName = dtr.GetString(2);

				list.Add(bt);
			}

			blnHasRows = dtr.HasRows;
			dtr.Close();
			return blnHasRows;
		}
		catch (SqlException sx) {
			if (dtr != null)
			{
				if (!dtr.IsClosed)
				{
					dtr.Close();
				}
			}

			BibleTranslation bt = new BibleTranslation();
			bt.BibleNo = -1;
			bt.BibleName = sx.ToString;
			List.Add(bt);
			return false;
		}
		catch (Exception ex) {
			if (!dtr.IsClosed)
			{
				dtr.Close();
			}

			BibleTranslation bt = new BibleTranslation();
			bt.BibleNo = -1;
			bt.BibleName = ex.ToString;
			list.Add(bt);
			return false;
		}
	}
}
