
/*
DELETE ParticipantPrediction;
DELETE ParticipatingInSeason;
DELETE ScheduledGame;
DELETE Participant;
DELETE Team;
DELETE MatchGroup;
DELETE Season;
*/




INSERT Season VALUES ('First Test Season', '2021-08-01', '2022-06-12');

INSERT Season VALUES ('Second Test Season', '2021-12-01', '2022-10-12');
INSERT Season VALUES ('Third Test Season', '2022-03-01', '2022-09-12');
INSERT Season VALUES ('Cup Test Season', '2022-06-01', '2022-08-12');


DECLARE @cnt int = 0;
DECLARE @date datetime = '2021-08-01'

while @cnt < 21
BEGIN

INSERT MatchGroup (SeasonId, MatchGroupSequence, MatchGroupTitle, StartDate, EndDate) 
SELECT s.Id, @cnt, 'Week ' + cast(@cnt + 1 as varchar(10)), @date, DATEADD(DAY, 7, @date)
FROM Season s
WHERE s.Name = 'First Test Season';

SET @date = DATEADD(DAY, 8, @date);
SET @cnt += 1;

END

INSERT [dbo].[Participant] VALUES ('Tom' , 'Hound' , 'tom.hound@islay.org');
INSERT [dbo].[Participant] VALUES ('Germ' , 'Sidman' , 'gsidman@base.org');
INSERT [dbo].[Participant] VALUES ('Fabio' , 'Grande' , 'fg@only.edu');

INSERT INTO [dbo].[ParticipatingInSeason]
           ([ParticipantId]
           ,[SeasonId]
)
select
           p.Id,
           s.Id
FROM [dbo].[Participant] p,
	[dbo].Season s

INSERT dbo.Team VALUES ('Ardent Antelope', 'Antelope Stadium');
INSERT dbo.Team VALUES ('Blue Beacons', 'Beacon Stadium');
INSERT dbo.Team VALUES ('Claret Cats', 'Cat Stadium');
INSERT dbo.Team VALUES ('Drab Dogs', 'Dog Stadium');
INSERT dbo.Team VALUES ('Effervescent Emus', 'Emu Stadium');
INSERT dbo.Team VALUES ('Fuscia Flamingos', 'Flamingo Stadium');


DECLARE @midSeason datetime = '2021-10-01'

INSERT dbo.ScheduledGame (MatchGroupId, HomeTeamId, VisitingTeamId, MatchDate)
SELECT  m.id, home = h.Id, away = v.Id, dateadd(day, h.id, dateadd(WEEK, h.id - v.Id, @midSeason))
FROM dbo.Season s,
	dbo.Team h
	Cross Join dbo.Team v, 
	MatchGroup m
where h.id != v.id
and dateadd(day, h.id, dateadd(WEEK, h.id - v.Id, @midSeason)) between m.StartDate and m.EndDate
order by h.id, v.id, dateadd(day, h.id, dateadd(WEEK, h.id - v.Id, @midSeason))


INSERT INTO [dbo].[ParticipantPrediction]
           ([ScheduledGameId]
           ,[ParticipantId]
		)
SELECT g.Id,
     p.Id
From dbo.ScheduledGame g,
dbo.Participant p

