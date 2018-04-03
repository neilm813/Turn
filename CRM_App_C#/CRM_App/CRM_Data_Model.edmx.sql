
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/22/2016 11:17:18
-- Generated from EDMX file: C:\Users\Neil\Documents\CODING\Redwood Projects\CRM_App_C#\CRM_App\CRM_Data_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CRMdatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK_UserCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_CustomerNote];
GO
IF OBJECT_ID(N'[dbo].[FK_NoteNoteEdits]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NoteEdits] DROP CONSTRAINT [FK_NoteNoteEdits];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Notes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notes];
GO
IF OBJECT_ID(N'[dbo].[NoteEdits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NoteEdits];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NULL,
    [DOB] nvarchar(max)  NULL,
    [LeadState] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NULL,
    [DateAdded] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [City] nvarchar(max)  NULL,
    [State] nvarchar(max)  NULL,
    [Zip] int  NULL,
    [StreetAddress] nvarchar(max)  NULL,
    [Employer] nvarchar(max)  NULL,
    [JobTitle] nvarchar(max)  NULL,
    [Tenure] int  NULL,
    [Nickname] nvarchar(max)  NULL
);
GO

-- Creating table 'Notes'
CREATE TABLE [dbo].[Notes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [Mood] nvarchar(max)  NULL,
    [DateAdded] datetime  NOT NULL,
    [CustomerId] int  NOT NULL,
    [LastEditDate] datetime  NULL,
    [LastEditAuthor] nvarchar(max)  NULL
);
GO

-- Creating table 'NoteEdits'
CREATE TABLE [dbo].[NoteEdits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [Mood] nvarchar(max)  NULL,
    [DateEdited] datetime  NOT NULL,
    [CustomerId] nvarchar(max)  NOT NULL,
    [NoteId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [PK_Notes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NoteEdits'
ALTER TABLE [dbo].[NoteEdits]
ADD CONSTRAINT [PK_NoteEdits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_UserCustomer]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCustomer'
CREATE INDEX [IX_FK_UserCustomer]
ON [dbo].[Customers]
    ([UserId]);
GO

-- Creating foreign key on [CustomerId] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [FK_CustomerNote]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerNote'
CREATE INDEX [IX_FK_CustomerNote]
ON [dbo].[Notes]
    ([CustomerId]);
GO

-- Creating foreign key on [NoteId] in table 'NoteEdits'
ALTER TABLE [dbo].[NoteEdits]
ADD CONSTRAINT [FK_NoteNoteEdits]
    FOREIGN KEY ([NoteId])
    REFERENCES [dbo].[Notes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NoteNoteEdits'
CREATE INDEX [IX_FK_NoteNoteEdits]
ON [dbo].[NoteEdits]
    ([NoteId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------