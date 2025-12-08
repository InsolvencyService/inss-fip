--Apply script

IF EXISTS (SELECT 1 FROM sys.columns 
        WHERE Name = N'fax' AND 
        Object_ID=Object_ID(N'dbo.findipauthbody'))
BEGIN
	ALTER TABLE [dbo].[findipauthbody] DROP COLUMN [fax]
END
GO


----Rollback script - Uncomment following section to apply

--IF NOT EXISTS (SELECT 1 FROM sys.columns 
--        WHERE Name = N'fax' AND 
--        Object_ID=Object_ID(N'dbo.findipauthbody'))
--BEGIN
--    ALTER TABLE [dbo].[findipauthbody] ADD [fax] int NULL
--END
--GO