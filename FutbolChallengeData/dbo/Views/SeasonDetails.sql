CREATE VIEW SeasonDetails 
AS

WITH GameCount as (
	SELECT SeasonId, COUNT(1) as TotalGameCount 
	FROM [SeasonSchedule]
	GROUP BY SeasonId),

	GamesPlayedCount as (
	SELECT SeasonId, COUNT(1) as GamesPlayedCount 
	FROM [SeasonSchedule]
	WHERE MatchDate < GETUTCDATE()
	GROUP BY SeasonId),
	
	ParticipantCount as (
	SELECT SeasonId, COUNT(1) as ParticipantCount
	FROM [ParticipatingInSeason]
	WHERE [Removed] is null
	GROUP BY SeasonId),

	NextMatchDate as (
	SELECT SeasonId, MIN(MatchDate) as NextMatchDate 
	FROM [SeasonSchedule]
	WHERE MatchDate >= GETUTCDATE()
	GROUP BY SeasonId)

	SELECT s.Id, 
		s.Name, 
		s.StartDate, 
		s.EndDate,
		ISNULL(g.TotalGameCount, 0) as SeasonGameCount,
		ISNULL(gp.GamesPlayedCount, 0) as SeasonGamesPlayedCount,
		ISNULL(pc.ParticipantCount, 0) as ParticipantCount,
		n.NextMatchDate
	FROM [Season] s
	LEFT JOIN GameCount g
		ON s.Id = g.SeasonId
	LEFT JOIN GamesPlayedCount gp
		ON s.Id = gp.SeasonId
	LEFT JOIN ParticipantCount pc
		ON s.Id	= pc.SeasonId
	LEFT JOIN NextMatchDate n
		ON s.Id = n.SeasonId