using System.Data.SqlClient;
using System.Configuration;

public class Books : CollectionBase
{

	public Book Item {
		get { return (Book)list(index); }
		set { list(index) = value; }
	}

	public int Add(Book b)
	{
		return list.Add(b);
	}

	public void Remove(Book b)
	{
		list.Remove(b);
	}

	public bool Find(int BibleID, BookSection Section, bool PopulateChapters, bool PopulateVerses)
	{
		SqlDataReader dtr = null;

		try {
			string strSQL = "bible_GetBooks";
			SqlConnection cnn = new SqlConnection(ConfigurationManager.AppSettings("DataConn"));
			SqlCommand cmd = new SqlCommand(strSQL, cnn);
			bool blnHasRows;
			string strSection;

			switch (Section) {
				case BookSection.OldTestament:
					strSection = "OT";
					break;
				case BookSection.NewTestament:
					strSection = "NT";
					break;
				case BookSection.All:
					strSection = "";
					break;
				default:
					strSection = "";
					break;
			}

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@Section", strSection));

			cnn.Open();
			dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

			while (dtr.Read) {
				Book b = new Book();
				Chapters cs = new Chapters();

				b.BookNo = dtr.GetInt32(0);
				b.Section = dtr.GetString(1);
				b.BookText = dtr.GetString(2);

				if (PopulateChapters)
				{
					cs.Find(BibleID, b.BookNo, PopulateVerses);
					b.Chapters = cs;
				}

				list.Add(b);
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

			Book b = new Book();
			b.BookNo = -1;
			b.BookText = sx.ToString;
			List.Add(b);
			return false;
		}
		catch (Exception ex) {
			if (!dtr.IsClosed)
			{
				dtr.Close();
			}

			Book b = new Book();
			b.BookNo = -1;
			b.Booktext = ex.ToString;
			list.Add(b);
			return false;
		}
	}
}
