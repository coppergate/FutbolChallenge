CREATE VIEW SeasonMatchGroupDetail AS
  
  SELECT s.SeasonId, s.MatchGroupSequence, m.MatchGroupTitle, m.StartDate as GroupStartDate, m.EndDate as GroupEndDate, COUNT(1) as MatchGroupGameCount 
  FROM [SeasonSchedule] s
  INNER JOIN [MatchGroup] m
  ON m.SeasonId = s.SeasonId
  AND m.MatchGroupSequence = s.MatchGroupSequence
  GROUP BY s.SeasonId, s.MatchGroupSequence, m.MatchGroupTitle, m.StartDate, m.EndDate