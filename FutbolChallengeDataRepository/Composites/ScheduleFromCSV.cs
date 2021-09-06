using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolChallengeDataRepository.Composites
{
	public static class ScheduleFromCSV
	{

		static public Task<ScheduleComposite> Create(int seasonId, string seasonName, string groupNameFormat, FileStream strm)
		{
			//	read a list of games (match group sequence, date/time, home team, away team)
			//	determine the start/end dates of the groups
			//	determine the start/end dates of the season.

			ScheduleComposite schedule = new ScheduleComposite();
			schedule.SeasonStart = DateTime.MaxValue;
			schedule.SeasonEnd = DateTime.MinValue;
			schedule.SeasonId = seasonId;
			schedule.SeasonName = seasonName;
			schedule.SeasonDescription = "Newly uploaded";

			using TextReader rdr = new StreamReader(strm);
			string line;
			while ((line = rdr.ReadLine()) != null)
			{
				string[] components = line.Split('|');

				if (components.Length != 4)
					continue;

				if (components.Any(c => string.IsNullOrEmpty(c)))
					continue;

				int seq = int.Parse(components[0]);
				DateTime gameDate = DateTime.Parse(components[1]);
				string home = components[2];
				string away = components[3];

				SeasonGroupComposite grp = schedule.SeasonGroups.SingleOrDefault(g => g.Sequence == seq);
				if (grp == null)
				{
					grp = new SeasonGroupComposite() { Name = string.Format(groupNameFormat, seq), GroupStart = gameDate, GroupEnd = gameDate, Sequence = seq };
					schedule.SeasonGroups.Add(grp);
					schedule.SeasonStart = schedule.SeasonStart > gameDate ? gameDate : schedule.SeasonStart;
					schedule.SeasonEnd = schedule.SeasonEnd < gameDate ? gameDate : schedule.SeasonEnd;
				}
				grp.GroupStart = grp.GroupStart > gameDate ? gameDate : grp.GroupStart;
				grp.GroupEnd = grp.GroupEnd < gameDate ? gameDate : grp.GroupEnd;

				grp.Games.Add(new Game() { GroupSequence = seq, GameDate = gameDate, HomeTeam = home, AwayTeam = away });

			}

			return Task.FromResult(schedule);
		}

	}
}
