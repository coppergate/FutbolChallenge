using FutbolChallenge.Data.Model;

namespace FutbolChallengeUI.Controls
{
	public class SelectedSeasonChangedEventArgs
	{
		public Season SelectedSeason;

		public SelectedSeasonChangedEventArgs(Season selectedSeason)
		{
			this.SelectedSeason = selectedSeason;
		}
	}
}