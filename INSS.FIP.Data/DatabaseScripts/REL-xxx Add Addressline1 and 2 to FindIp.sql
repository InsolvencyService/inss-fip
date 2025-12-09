----Add RegisteredAddressLine1 & 2 into find_ip - this approach ensures addresslines 1-5 will appear in order - which is was bound to offend someone if they weren't :)

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

BEGIN TRY 

	BEGIN TRANSACTION

		SELECT * INTO #findiptemp FROM [dbo].[find_ip] 

		DROP TABLE [dbo].[find_ip]

		CREATE TABLE [dbo].[find_ip](
			[IpNo] [int] NOT NULL,
			[Forenames] [varchar](8000) NULL,
			[Surname] [varchar](8000) NULL,
			[RegisteredFirmName] [varchar](8000) NULL,
			[RegisteredAddressLine1] [varchar](8000) NULL,
			[RegisteredAddressLine2] [varchar](8000) NULL,
			[RegisteredAddressLine3] [varchar](8000) NULL,
			[RegisteredAddressLine4] [varchar](8000) NULL,
			[RegisteredAddressLine5] [varchar](8000) NULL,
			[RegisteredPostCode] [varchar](8000) NULL,
			[RegisteredPhone] [varchar](8000) NULL,
			[IpEmailAddress] [varchar](8000) NULL,
			[IncludeOnInternet] [varchar](8000) NULL,
			[LicensingBody] [varchar](8000) NULL,
		PRIMARY KEY CLUSTERED 
		(
			[IpNo] ASC
		)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]


		ALTER TABLE [dbo].[find_ip] ADD  DEFAULT (' ') FOR [IncludeOnInternet]


		INSERT INTO [dbo].[find_ip] (IpNo, Forenames, Surname, RegisteredFirmName, RegisteredAddressLine3, RegisteredAddressLine4, RegisteredAddressLine5, RegisteredPostCode, RegisteredPhone, IpEmailAddress, IncludeOnInternet, LicensingBody) 
			SELECT IpNo, Forenames, Surname, RegisteredFirmName, RegisteredAddressLine3, RegisteredAddressLine4, RegisteredAddressLine5, RegisteredPostCode, RegisteredPhone, IpEmailAddress, IncludeOnInternet, LicensingBody FROM #findiptemp

		DROP TABLE #findiptemp

	COMMIT TRANSACTION

END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() AS ErrorMessage

	IF OBJECT_ID('tempdb..#findiptemp') IS NOT NULL DROP TABLE #findiptemp

	ROLLBACK TRANSACTION
END CATCH


----Rollback script --Execute following commented sections to Rollback

--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--BEGIN TRY 

--	BEGIN TRANSACTION

--		SELECT * INTO #findiptemp FROM [dbo].[find_ip] 

--		DROP TABLE [dbo].[find_ip]

--		CREATE TABLE [dbo].[find_ip](
--			[IpNo] [int] NOT NULL,
--			[Forenames] [varchar](8000) NULL,
--			[Surname] [varchar](8000) NULL,
--			[RegisteredFirmName] [varchar](8000) NULL,
--			[RegisteredAddressLine3] [varchar](8000) NULL,
--			[RegisteredAddressLine4] [varchar](8000) NULL,
--			[RegisteredAddressLine5] [varchar](8000) NULL,
--			[RegisteredPostCode] [varchar](8000) NULL,
--			[RegisteredPhone] [varchar](8000) NULL,
--			[IpEmailAddress] [varchar](8000) NULL,
--			[IncludeOnInternet] [varchar](8000) NULL,
--			[LicensingBody] [varchar](8000) NULL,
--		PRIMARY KEY CLUSTERED 
--		(
--			[IpNo] ASC
--		)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--		) ON [PRIMARY]


--		ALTER TABLE [dbo].[find_ip] ADD  DEFAULT (' ') FOR [IncludeOnInternet]


--		INSERT INTO [dbo].[find_ip] (IpNo, Forenames, Surname, RegisteredFirmName, RegisteredAddressLine3, RegisteredAddressLine4, RegisteredAddressLine5, RegisteredPostCode, RegisteredPhone, IpEmailAddress, IncludeOnInternet, LicensingBody) 
--			SELECT IpNo, Forenames, Surname, RegisteredFirmName, RegisteredAddressLine3, RegisteredAddressLine4, RegisteredAddressLine5, RegisteredPostCode, RegisteredPhone, IpEmailAddress, IncludeOnInternet, LicensingBody FROM #findiptemp


--		DROP TABLE #findiptemp

--	COMMIT TRANSACTION

--END TRY
--BEGIN CATCH
--	SELECT ERROR_MESSAGE() AS ErrorMessage

--	IF OBJECT_ID('tempdb..#findiptemp') IS NOT NULL DROP TABLE #findiptemp

--	ROLLBACK TRANSACTION
--END CATCH


