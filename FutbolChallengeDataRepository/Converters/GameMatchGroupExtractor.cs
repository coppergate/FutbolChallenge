using FutbolChallenge.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace FutbolChallengeDataRepository.Converters
{
	public class GameMatchGroupExtractor
	{
		static public IEnumerable<MatchGroup> ExtractMatchGroups(IEnumerable<SeasonGame> game)
		{
			return
				game.GroupBy(g => g.MatchGroupId).Select(
					e =>
					 new MatchGroup() {
						 Id = e.Max(v => v.MatchGroupId),
						 SeasonId = e.Min(v => v.SeasonId),
						 MatchGroupSequence = e.Max(v => v.MatchGroupSequence),
						 MatchGroupTitle = e.Max(v => v.MatchGroupTitle),
						 StartDate = e.Max(v => v.MatchGroupStartDate),
						 EndDate = e.Min(v => v.MatchGroupEndDate)
					 }
					);
		}
	}
}
