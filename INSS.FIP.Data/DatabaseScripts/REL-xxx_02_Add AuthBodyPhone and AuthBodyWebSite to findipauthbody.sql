----Add AuthBodyPhone and AuthBodyWebSite into findipauthbody replacing phone and website which are defined as int and are not suitable

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

BEGIN TRY 

	BEGIN TRANSACTION

		SELECT * INTO #findipauthbodytemp FROM [dbo].[findipauthbody] 

		DROP TABLE [dbo].[findipauthbody]

		CREATE TABLE [dbo].[findipauthbody](
			[AuthBodyCode] [varchar](5) NOT NULL,
			[AuthBodyName] [varchar](8000) NULL,
			[AuthBodyAddressLine1] [varchar](8000) NULL,
			[AuthBodyAddressLine2] [varchar](8000) NULL,
			[AuthBodyAddressLine3] [varchar](8000) NULL,
			[AuthBodyAddressLine4] [varchar](8000) NULL,
			[AuthBodyAddressLine5] [varchar](8000) NULL,
			[AuthBodyPostcode] [varchar](8000) NULL,
			[AuthBodyPhone] [varchar](8000) NULL,
			[AuthBodyWebsite] [varchar](8000) NULL,
		PRIMARY KEY CLUSTERED 
		(
			[AuthBodyCode] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		INSERT INTO [dbo].[findipauthbody] (AuthBodyCode, AuthBodyName, AuthBodyAddressLine1, AuthBodyAddressLine2, AuthBodyAddressLine3, AuthBodyAddressLine4, AuthBodyAddressLine5, AuthBodyPostcode) 
			SELECT AuthBodyCode, AuthBodyName, AuthBodyAddressLine1, AuthBodyAddressLine2, AuthBodyAddressLine3, AuthBodyAddressLine4, AuthBodyAddressLine5, AuthBodyPostcode FROM #findipauthbodytemp

		DROP TABLE #findipauthbodytemp

	COMMIT TRANSACTION

END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() AS ErrorMessage

	IF OBJECT_ID('tempdb..#findipauthbodytemp') IS NOT NULL DROP TABLE #findipauthbodytemp

	ROLLBACK TRANSACTION
END CATCH


----Rollback script --Execute following commented sections to Rollback

--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--BEGIN TRY 

--	BEGIN TRANSACTION

--		SELECT * INTO #findipauthbodytemp FROM [dbo].[findipauthbody] 

--		DROP TABLE [dbo].[findipauthbody]

--		CREATE TABLE [dbo].[findipauthbody](
--			[AuthBodyCode] [varchar](5) NOT NULL,
--			[AuthBodyName] [varchar](8000) NULL,
--			[AuthBodyAddressLine1] [varchar](8000) NULL,
--			[AuthBodyAddressLine2] [varchar](8000) NULL,
--			[AuthBodyAddressLine3] [varchar](8000) NULL,
--			[AuthBodyAddressLine4] [varchar](8000) NULL,
--			[AuthBodyAddressLine5] [varchar](8000) NULL,
--			[AuthBodyPostcode] [varchar](8000) NULL,
--			[phone] [int] NULL,
--			[website] [int] NULL,
--		PRIMARY KEY CLUSTERED 
--		(
--			[AuthBodyCode] ASC
--		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--		) ON [PRIMARY]


--		INSERT INTO [dbo].[findipauthbody] (AuthBodyCode, AuthBodyName, AuthBodyAddressLine1, AuthBodyAddressLine2, AuthBodyAddressLine3, AuthBodyAddressLine4, AuthBodyAddressLine5, AuthBodyPostcode) 
--			SELECT AuthBodyCode, AuthBodyName, AuthBodyAddressLine1, AuthBodyAddressLine2, AuthBodyAddressLine3, AuthBodyAddressLine4, AuthBodyAddressLine5, AuthBodyPostcode FROM #findipauthbodytemp

--		DROP TABLE #findipauthbodytemp

--	COMMIT TRANSACTION

--END TRY
--BEGIN CATCH
--	SELECT ERROR_MESSAGE() AS ErrorMessage

--	IF OBJECT_ID('tempdb..#findipauthbodytemp') IS NOT NULL DROP TABLE #findipauthbodytemp

--	ROLLBACK TRANSACTION
--END CATCH