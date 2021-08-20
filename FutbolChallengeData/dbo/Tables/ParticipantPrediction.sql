CREATE TABLE [dbo].[ParticipantPrediction] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [ScheduledGameId]   INT NOT NULL,
    [ParticipantId]     INT NOT NULL,
    [HomeTeamScore]     INT NULL,
    [VisitingTeamScore] INT NULL,
    CONSTRAINT [PK_ParticipantPrediction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParticipantPrediction_Participant] FOREIGN KEY ([ParticipantId]) REFERENCES [dbo].[Participant] ([Id]),
    CONSTRAINT [FK_ParticipantPrediction_ScheduledGame] FOREIGN KEY ([ScheduledGameId]) REFERENCES [dbo].[ScheduledGame] ([Id])
);

