public class SearchResult
{
	private int _MatchCount;
	private string _Section;
	private int _BookNo;
	private string _BookText;
	private int _ChapterNo;
	private int _VerseNo;
	private string _VerseText;

	public int MatchCount {
		get { return _MatchCount; }
		set { _MatchCount = value; }
	}

	public string Section {
		get {
			switch (_Section) {
				case "OT":
					return "Old Testament";
				case "NT":
					return "New Testament";
				default:
					return "";
			}
		}
		set { _Section = value; }
	}

	public int BookNo {
		get { return _BookNo; }
		set { _BookNo = value; }
	}

	public string BookText {
		get { return _BookText; }
		set { _BookText = value; }
	}

	public int ChapterNo {
		get { return _ChapterNo; }
		set { _ChapterNo = value; }
	}

	public int VerseNo {
		get { return _VerseNo; }
		set { _VerseNo = value; }
	}

	public string VerseText {
		get { return _VerseText; }
		set { _VerseText = value; }
	}
}
