
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/01/2018 12:24:11
-- Generated from EDMX file: C:\Users\s00172994\Documents\Visual Studio 2015\Projects\FOOP2\VideoGameLauncher\VideoGameLauncher\ModDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ModDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ModAuthor_Mod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModAuthor] DROP CONSTRAINT [FK_ModAuthor_Mod];
GO
IF OBJECT_ID(N'[dbo].[FK_ModAuthor_Author]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ModAuthor] DROP CONSTRAINT [FK_ModAuthor_Author];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Mods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mods];
GO
IF OBJECT_ID(N'[dbo].[Authors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Authors];
GO
IF OBJECT_ID(N'[dbo].[ModAuthor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ModAuthor];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Mods'
CREATE TABLE [dbo].[Mods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Version] float  NOT NULL,
    [Warnings] nvarchar(max)  NOT NULL,
    [Location] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Authors'
CREATE TABLE [dbo].[Authors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ModAuthor'
CREATE TABLE [dbo].[ModAuthor] (
    [Mods_Id] int  NOT NULL,
    [Authors_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Mods'
ALTER TABLE [dbo].[Mods]
ADD CONSTRAINT [PK_Mods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Authors'
ALTER TABLE [dbo].[Authors]
ADD CONSTRAINT [PK_Authors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Mods_Id], [Authors_Id] in table 'ModAuthor'
ALTER TABLE [dbo].[ModAuthor]
ADD CONSTRAINT [PK_ModAuthor]
    PRIMARY KEY CLUSTERED ([Mods_Id], [Authors_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Mods_Id] in table 'ModAuthor'
ALTER TABLE [dbo].[ModAuthor]
ADD CONSTRAINT [FK_ModAuthor_Mod]
    FOREIGN KEY ([Mods_Id])
    REFERENCES [dbo].[Mods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Authors_Id] in table 'ModAuthor'
ALTER TABLE [dbo].[ModAuthor]
ADD CONSTRAINT [FK_ModAuthor_Author]
    FOREIGN KEY ([Authors_Id])
    REFERENCES [dbo].[Authors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModAuthor_Author'
CREATE INDEX [IX_FK_ModAuthor_Author]
ON [dbo].[ModAuthor]
    ([Authors_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------