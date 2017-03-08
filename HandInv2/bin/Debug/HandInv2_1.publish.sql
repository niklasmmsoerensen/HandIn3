﻿/*
Deployment script for Handinv2

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Handinv2"
:setvar DefaultFilePrefix "Handinv2"
:setvar DefaultDataPath "C:\Users\nikla\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\nikla\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The type for column Efternavn in table [dbo].[Person] is currently  NVARCHAR (255) NOT NULL but is being changed to  NVARCHAR (20) NOT NULL. Data loss could occur.

The type for column Fornavn in table [dbo].[Person] is currently  NVARCHAR (255) NOT NULL but is being changed to  NVARCHAR (20) NOT NULL. Data loss could occur.

The type for column Mellemnavn in table [dbo].[Person] is currently  NVARCHAR (255) NOT NULL but is being changed to  NVARCHAR (20) NOT NULL. Data loss could occur.

The type for column PersonType in table [dbo].[Person] is currently  NVARCHAR (255) NOT NULL but is being changed to  NVARCHAR (20) NOT NULL. Data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Person])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The type for column TelefonNummer in table [dbo].[Telefon] is currently  NVARCHAR (255) NOT NULL but is being changed to  NVARCHAR (20) NOT NULL. Data loss could occur.

The type for column TelefonType in table [dbo].[Telefon] is currently  NVARCHAR (255) NOT NULL but is being changed to  NVARCHAR (20) NOT NULL. Data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Telefon])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[fk_PersonAdresse2]...';


GO
ALTER TABLE [dbo].[PersonAdresse] DROP CONSTRAINT [fk_PersonAdresse2];


GO
PRINT N'Altering [dbo].[Adresse]...';


GO
ALTER TABLE [dbo].[Adresse] ALTER COLUMN [Vejnavn] NVARCHAR (MAX) NOT NULL;


GO
PRINT N'Altering [dbo].[Person]...';


GO
ALTER TABLE [dbo].[Person] ALTER COLUMN [Efternavn] NVARCHAR (20) NOT NULL;

ALTER TABLE [dbo].[Person] ALTER COLUMN [Fornavn] NVARCHAR (20) NOT NULL;

ALTER TABLE [dbo].[Person] ALTER COLUMN [Mellemnavn] NVARCHAR (20) NOT NULL;

ALTER TABLE [dbo].[Person] ALTER COLUMN [PersonType] NVARCHAR (20) NOT NULL;


GO
PRINT N'Altering [dbo].[Telefon]...';


GO
ALTER TABLE [dbo].[Telefon] ALTER COLUMN [TelefonNummer] NVARCHAR (20) NOT NULL;

ALTER TABLE [dbo].[Telefon] ALTER COLUMN [TelefonType] NVARCHAR (20) NOT NULL;


GO
PRINT N'Creating [dbo].[fk_PersonAdresse2]...';


GO
ALTER TABLE [dbo].[PersonAdresse] WITH NOCHECK
    ADD CONSTRAINT [fk_PersonAdresse2] FOREIGN KEY ([AdresseID]) REFERENCES [dbo].[Adresse] ([AdresseID]) ON UPDATE CASCADE;


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[PersonAdresse] WITH CHECK CHECK CONSTRAINT [fk_PersonAdresse2];


GO
PRINT N'Update complete.';


GO
