
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/08/2023 19:10:20
-- Generated from EDMX file: C:\Users\sh0ut\OneDrive\Desktop\The Finding of Seven (Text Adventure Game)COPY_TO_TEST_NEW_NAVIGATION\The Finding of Seven (Text Adventure Game)\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Assets];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PageTBLs'
CREATE TABLE [dbo].[PageTBLs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PageNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SoundTBLs'
CREATE TABLE [dbo].[SoundTBLs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SoundSrc] nvarchar(max)  NOT NULL,
    [SoundName] nvarchar(max)  NOT NULL,
    [PageTBLId] int  NOT NULL
);
GO

-- Creating table 'ImageTBLs'
CREATE TABLE [dbo].[ImageTBLs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ImageSrc] nvarchar(max)  NOT NULL,
    [ImageName] nvarchar(max)  NOT NULL,
    [PageTBLId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PageTBLs'
ALTER TABLE [dbo].[PageTBLs]
ADD CONSTRAINT [PK_PageTBLs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SoundTBLs'
ALTER TABLE [dbo].[SoundTBLs]
ADD CONSTRAINT [PK_SoundTBLs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ImageTBLs'
ALTER TABLE [dbo].[ImageTBLs]
ADD CONSTRAINT [PK_ImageTBLs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PageTBLId] in table 'SoundTBLs'
ALTER TABLE [dbo].[SoundTBLs]
ADD CONSTRAINT [FK_PageTBLSoundTBL]
    FOREIGN KEY ([PageTBLId])
    REFERENCES [dbo].[PageTBLs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PageTBLSoundTBL'
CREATE INDEX [IX_FK_PageTBLSoundTBL]
ON [dbo].[SoundTBLs]
    ([PageTBLId]);
GO

-- Creating foreign key on [PageTBLId] in table 'ImageTBLs'
ALTER TABLE [dbo].[ImageTBLs]
ADD CONSTRAINT [FK_PageTBLImageTBL]
    FOREIGN KEY ([PageTBLId])
    REFERENCES [dbo].[PageTBLs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PageTBLImageTBL'
CREATE INDEX [IX_FK_PageTBLImageTBL]
ON [dbo].[ImageTBLs]
    ([PageTBLId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------