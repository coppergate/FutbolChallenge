using System;

namespace FutbolChallengeDataRepository.Composites
{
	public class Game
	{
		public int GroupSequence {  get; set; }
		public DateTime GameDate {  get; set; }

		public string HomeTeam {  get; set; }
		public string AwayTeam {  get; set; }
	}
}