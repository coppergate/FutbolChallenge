CREATE TABLE [dbo].[Season] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (100) NOT NULL,
    [StartDate] DATETIME2 (7) NOT NULL,
    [EndDate]   DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Season] PRIMARY KEY CLUSTERED ([Id] ASC)
);

