CREATE TABLE [dbo].[ParticipatingInSeason] (
    [ParticipantId] INT           NOT NULL,
    [SeasonId]      INT           NOT NULL,
    [Removed]       BIT           NULL,
    [RemovedDate]   DATETIME2 (7) NULL,
    CONSTRAINT [FK_ParticipatingInSeason_Participant] FOREIGN KEY ([ParticipantId]) REFERENCES [dbo].[Participant] ([Id]),
    CONSTRAINT [FK_ParticipatingInSeason_Season] FOREIGN KEY ([SeasonId]) REFERENCES [dbo].[Season] ([Id])
);





