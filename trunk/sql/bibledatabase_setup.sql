if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_AddUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_AddUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_GetBooks]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_GetBooks]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_GetChapterVerse]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_GetChapterVerse]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_GetChapterVerses]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_GetChapterVerses]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_GetChapters]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_GetChapters]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_GetTranslations]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_GetTranslations]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_GetVerseRange]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_GetVerseRange]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_RandomFavoriteVerse]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_RandomFavoriteVerse]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_RandomVerse]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_RandomVerse]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_Search]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_Search]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_SearchDictionary_Eastons]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_SearchDictionary_Eastons]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_ValidateUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sa_sumerano].[bible_ValidateUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[BookVerseText]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [sa_sumerano].[BookVerseText]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_EastonsDictionary]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [sa_sumerano].[bible_EastonsDictionary]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_FavoriteVerses]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [sa_sumerano].[bible_FavoriteVerses]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_bible]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [sa_sumerano].[bible_bible]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_bibletranslations]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [sa_sumerano].[bible_bibletranslations]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_books]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [sa_sumerano].[bible_books]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_section]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [sa_sumerano].[bible_section]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[sa_sumerano].[bible_users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [sa_sumerano].[bible_users]
GO

CREATE TABLE [sa_sumerano].[bible_EastonsDictionary] (
	[ID] [int] NOT NULL ,
	[Word] [varchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Meaning] [varchar] (8000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [sa_sumerano].[bible_FavoriteVerses] (
	[Verse] [varchar] (8000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [sa_sumerano].[bible_bible] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[BibleID] [int] NOT NULL ,
	[BookID] [int] NOT NULL ,
	[Chapter] [int] NOT NULL ,
	[Verse] [int] NOT NULL ,
	[VerseText] [varchar] (8000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [sa_sumerano].[bible_bibletranslations] (
	[BibleID] [int] IDENTITY (1, 1) NOT NULL ,
	[BibleAbbr] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[BibleName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [sa_sumerano].[bible_books] (
	[BookID] [int] NOT NULL ,
	[Abbrev] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[SectionID] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Book] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [sa_sumerano].[bible_section] (
	[SectionID] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Section] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [sa_sumerano].[bible_users] (
	[UserID] [int] IDENTITY (1, 1) NOT NULL ,
	[Token] [uniqueidentifier] NOT NULL ,
	[Username] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Password] [binary] (16) NOT NULL ,
	[WebSite] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Name] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CreatedOn] [datetime] NOT NULL ,
	[Expires] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [sa_sumerano].[bible_bible] WITH NOCHECK ADD 
	CONSTRAINT [PK_bible_asv] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [sa_sumerano].[bible_bibletranslations] WITH NOCHECK ADD 
	CONSTRAINT [PK_bible_bibles] PRIMARY KEY  CLUSTERED 
	(
		[BibleID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [sa_sumerano].[bible_books] WITH NOCHECK ADD 
	CONSTRAINT [PK_bible_books] PRIMARY KEY  CLUSTERED 
	(
		[BookID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [sa_sumerano].[bible_section] WITH NOCHECK ADD 
	CONSTRAINT [PK_bible_booktype] PRIMARY KEY  CLUSTERED 
	(
		[SectionID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [sa_sumerano].[bible_users] WITH NOCHECK ADD 
	CONSTRAINT [PK_bible_users] PRIMARY KEY  CLUSTERED 
	(
		[UserID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

 CREATE  CLUSTERED  INDEX [IX_bibledict_Eastons] ON [sa_sumerano].[bible_EastonsDictionary]([Word]) ON [PRIMARY]
GO

ALTER TABLE [sa_sumerano].[bible_EastonsDictionary] ADD 
	CONSTRAINT [PK_bibledict_Eastons] PRIMARY KEY  NONCLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [bible_BibleIDBookIDChapterVerseVerseText_NU_Nidx] ON [sa_sumerano].[bible_bible]([BibleID], [BookID], [Chapter], [Verse], [VerseText]) ON [PRIMARY]
GO

ALTER TABLE [sa_sumerano].[bible_bibletranslations] ADD 
	CONSTRAINT [DF_bible_bibles_BibleName] DEFAULT ('') FOR [BibleName]
GO

ALTER TABLE [sa_sumerano].[bible_books] ADD 
	CONSTRAINT [DF_bible_books_Abbrev] DEFAULT ('') FOR [Abbrev],
	CONSTRAINT [DF_bible_books_BookType] DEFAULT ('') FOR [SectionID],
	CONSTRAINT [DF_bible_books_Book] DEFAULT ('') FOR [Book]
GO

 CREATE  INDEX [bible_books_BookTypeID_NU_Nidx] ON [sa_sumerano].[bible_books]([SectionID]) WITH  FILLFACTOR = 90 ON [PRIMARY]
GO

ALTER TABLE [sa_sumerano].[bible_section] ADD 
	CONSTRAINT [DF_bible_booktype_BookTypeID] DEFAULT ('') FOR [SectionID],
	CONSTRAINT [DF_bible_booktype_BookType] DEFAULT ('') FOR [Section]
GO

ALTER TABLE [sa_sumerano].[bible_users] ADD 
	CONSTRAINT [DF_bible_users_Token] DEFAULT (newid()) FOR [Token],
	CONSTRAINT [DF_bible_users_WebSite] DEFAULT ('') FOR [WebSite],
	CONSTRAINT [DF_bible_users_Name] DEFAULT ('') FOR [Name],
	CONSTRAINT [DF_bible_users_CreatedOn] DEFAULT (getdate()) FOR [CreatedOn],
	CONSTRAINT [DF_bible_users_Expires] DEFAULT (dateadd(year,1,getdate())) FOR [Expires]
GO

 CREATE  UNIQUE  INDEX [bible_users_Username_Password_U_Nidx] ON [sa_sumerano].[bible_users]([Username], [Password]) WITH  FILLFACTOR = 90 ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE VIEW sa_sumerano.BookVerseText
AS
SELECT     TOP 100 PERCENT bible.ID, bible.BibleID, b.SectionID, b.BookID, b.Book, bible.Chapter, bible.Verse, bible.VerseText
FROM         sa_sumerano.bible_books b INNER JOIN
                      sa_sumerano.bible_bible bible ON b.BookID = bible.BookID

GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE [sa_sumerano].bible_AddUser
@Username varchar(50),
@Password binary(16),
@Name varchar(100),
@WebSite varchar(255)
AS
INSERT INTO
	bible_users (Username, [Password], [Name], WebSite)
VALUES
	(@Username, @Password, @Name, @WebSite)

GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE [sa_sumerano].bible_GetBooks

@Section char(2)

AS

SET NOCOUNT ON

IF @Section = '' OR @Section = '  '
	select b.bookid, s.[section], b.book
	from bible_books b
	inner join bible_section s on b.sectionid = s.sectionid
	where b.bookid <= 66
ELSE
	select b.bookid, s.[section], b.book
	from bible_books b
	inner join bible_section s on b.sectionid = s.sectionid
	where b.bookid <= 66 and b.sectionid = @Section

SET NOCOUNT OFF
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE [sa_sumerano].bible_GetChapterVerse

@BibleID int,
@BookID int,
@Chapter int,
@Verse int

AS

SET NOCOUNT ON

select s.[section], b.book, bible.bookid, bible.chapter, bible.verse, bible.versetext
from bible_bible bible
inner join bible_books b on bible.bookid = b.bookid
inner join bible_section s on b.sectionid = s.sectionid
where bible.bibleid = @BibleID and bible.bookid = @BookID and bible.chapter = @Chapter and bible.verse = @Verse

SET NOCOUNT OFF
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE [sa_sumerano].bible_GetChapterVerses

@BibleID int,
@BookID int,
@Chapter int

AS

SET NOCOUNT ON

select s.[section], b.book, bible.bookid, bible.chapter, bible.verse, bible.versetext
from bible_bible bible
inner join bible_books b on bible.bookid = b.bookid
inner join bible_section s on b.sectionid = s.sectionid
where bible.bibleid = @BibleID and bible.bookid = @BookID and bible.chapter = @Chapter

SET NOCOUNT OFF
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE [sa_sumerano].bible_GetChapters

@BibleID int,
@BookID int

AS

SET NOCOUNT ON

select distinct chapter
from bible_bible
where bibleid = @BibleID and bookid = @BookID
order by chapter

SET NOCOUNT OFF
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE sa_sumerano.bible_GetTranslations

AS

SET NOCOUNT ON

select BibleID, BibleAbbr, BibleName
from bible_bibletranslations
order by biblename

SET NOCOUNT OFF


GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE sa_sumerano.bible_GetVerseRange

@BibleID int,
@BookID int,
@ChapterStart int,
@ChapterEnd int,
@VerseStart int,
@VerseEnd int

AS

SET NOCOUNT ON

select s.[section], b.book, bible.bookid, bible.chapter, bible.verse, bible.versetext
from bible_bible bible
inner join bible_books b on bible.bookid = b.bookid
inner join bible_section s on b.sectionid = s.sectionid
where bible.bibleid = @BibleID and bible.bookid = @BookID and
bible.chapter between @ChapterStart and @ChapterEnd and
bible.verse between @VerseStart and @VerseEnd

SET NOCOUNT OFF
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

setuser N'sa_sumerano'
GO

CREATE procedure sa_sumerano.bible_RandomFavoriteVerse

as

set nocount on

select top 1 verse from bible_favoriteverses
order by newid()

set nocount off
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE [sa_sumerano].bible_RandomVerse

@BibleID int

AS

SET NOCOUNT ON

select top 1 s.[section], b.book, bible.bookid, bible.chapter, bible.verse, bible.versetext
from bible_bible bible
inner join bible_books b on bible.bookid = b.bookid
inner join bible_section s on b.sectionid = s.sectionid
where bible.bibleid = @BibleID
order by NEWID()

SET NOCOUNT OFF
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE sa_sumerano.bible_Search

@BibleID int,
@SearchTerms varchar(2000),
@Delimiter char(1),
@AllWords bit = 0

AS

SET NOCOUNT ON

DECLARE @delimiter_position int
DECLARE @search_term varchar(2000)
DECLARE @like_text varchar(2000)
DECLARE @word_count int

CREATE TABLE #SearchResults ([ID] int)

SET @SearchTerms = @SearchTerms + @Delimiter
SET @word_count = 0

WHILE patindex('%' + @Delimiter + '%', @SearchTerms) <> 0
BEGIN
	SELECT @delimiter_position = patindex('%' + @Delimiter + '%', @SearchTerms)
	SELECT @search_term = left(@SearchTerms, @delimiter_position - 1)
	SELECT @like_text = '%' + @search_term + '%'

	INSERT #SearchResults
	SELECT [ID]
	FROM BookVerseText
	WHERE BibleID = @BibleID and Book like @like_text

	INSERT #SearchResults
	SELECT [ID]
	FROM BookVerseText
	WHERE BibleID = @BibleID and VerseText like @like_text

	SELECT @SearchTerms = stuff(@SearchTerms, 1, @delimiter_position, '')
	SELECT @word_count = @word_count + 1
END

IF @AllWords = 0
	SELECT TOP 50 s.[ID], bvt.SectionID, bvt.BookID, bvt.Book, bvt.Chapter, bvt.Verse, bvt.VerseText, count(*) as Rating
	FROM #SearchResults s, BookVerseText bvt
	WHERE s.[ID] = bvt.[ID]
	GROUP BY s.[ID], bvt.SectionID, bvt.BookID, bvt.Book, bvt.Chapter, bvt.Verse, bvt.VerseText
	ORDER BY 8 DESC, 3 ASC, 5 ASC
ELSE
	SELECT TOP 50 s.[ID], bvt.SectionID, bvt.BookID, bvt.Book, bvt.Chapter, bvt.Verse, bvt.VerseText, count(*) as Rating
	FROM #SearchResults s, BookVerseText bvt
	WHERE s.[ID] = bvt.[ID]
	GROUP BY s.[ID], bvt.SectionID, bvt.BookID, bvt.Book, bvt.Chapter, bvt.Verse, bvt.VerseText
	HAVING count(*) = @word_count
	ORDER BY 8 DESC, 3 ASC, 5 ASC

SET NOCOUNT OFF
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

setuser N'sa_sumerano'
GO

CREATE procedure sa_sumerano.bible_SearchDictionary_Eastons

@word varchar(60),
@exactmatch bit = 0

as

set nocount on

IF @exactmatch = 1
BEGIN
	select word, meaning
	from bible_eastonsdictionary
	where word LIKE '%' + @word + '%'
END
ELSE
BEGIN
	select word, meaning
	from bible_eastonsdictionary
	where difference(@word, word) = 4
END

set nocount off
GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

setuser N'sa_sumerano'
GO

CREATE PROCEDURE [sa_sumerano].bible_ValidateUser
@Token uniqueidentifier,
@Username varchar(50),
@Password binary(16)
AS
SET NOCOUNT ON
IF EXISTS (SELECT UserID FROM bible_users WHERE Token = @Token AND Username = @Username AND [Password] = @Password)
	RETURN 1
ELSE
	RETURN 0
SET NOCOUNT OFF

GO
setuser
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

