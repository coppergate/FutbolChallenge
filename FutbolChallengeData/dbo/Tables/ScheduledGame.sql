CREATE TABLE [dbo].[ScheduledGame] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [MatchGroupId]      INT           NOT NULL,
    [HomeTeamId]        INT           NOT NULL,
    [VisitingTeamId]    INT           NOT NULL,
    [HomeTeamScore]     INT           NULL,
    [VisitingTeamScore] INT           NULL,
    [MatchDate]         DATETIME2 (7) NULL,
    CONSTRAINT [PK_ScheduledGame] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Schedule_HomeTeam] FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Team] ([Id]),
    CONSTRAINT [FK_Schedule_VisitingTeam] FOREIGN KEY ([VisitingTeamId]) REFERENCES [dbo].[Team] ([Id]),
    CONSTRAINT [FK_ScheduledGame_MatchGroup] FOREIGN KEY ([MatchGroupId]) REFERENCES [dbo].[MatchGroup] ([Id])
);



