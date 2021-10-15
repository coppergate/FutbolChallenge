using FutbolChallenge.Data.Model;

namespace FutbolChallengeUI.EventHandlers.EventArgs
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