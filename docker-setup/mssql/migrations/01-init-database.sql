
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;

USE [master];
GO

PRINT N'Creating FireflyAuth...'
GO

CREATE DATABASE "FireflyAuth"
GO

USE FireflyAuth;
GO

PRINT N'Creating [dbo].[Users]...';
GO
CREATE TABLE [dbo].[Users] (
    [Id]       INT IDENTITY   NOT NULL,
    [Username] NVARCHAR (128) NOT NULL,
    [Hash]     NVARCHAR (MAX) NOT NULL,
    [Salt]     NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'Creating [dbo].[Users].[IX_Users_Username]...';
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username]
    ON [dbo].[Users]([Username] ASC);
GO
