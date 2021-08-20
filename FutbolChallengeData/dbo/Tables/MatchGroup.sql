CREATE TABLE [dbo].[MatchGroup] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [SeasonId]           INT           NOT NULL,
    [MatchGroupSequence] INT           NOT NULL,
    [StartDate]          DATETIME2 (7) NOT NULL,
    [EndDate]            DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_MatchGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MatchGroup_Season] FOREIGN KEY ([SeasonId]) REFERENCES [dbo].[Season] ([Id])
);

