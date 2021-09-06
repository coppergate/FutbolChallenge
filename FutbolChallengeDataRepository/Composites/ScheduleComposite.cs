using System;
using System.Collections.Generic;

namespace FutbolChallengeDataRepository.Composites
{
	public class ScheduleComposite
	{
		public int SeasonId { get; set; }
		public string SeasonName { get; set; }
		public string SeasonDescription { get; set; }
		public DateTime SeasonStart { get; set; }
		public DateTime SeasonEnd { get; set; }

		public IList<SeasonGroupComposite> SeasonGroups {  get; set; } = new List<SeasonGroupComposite>();


	}
}
