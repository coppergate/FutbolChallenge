using FutbolChallenge.Data.Model;
using System.Collections.ObjectModel;

namespace FutbolChallengeUI.ViewModels
{
	public class SeasonListViewModel : BindableBase
	{
		public SeasonListViewModel()
		{
			
		}

		private ObservableCollection<SeasonPanelViewModel> _Seasons;

		public ObservableCollection<SeasonPanelViewModel> Seasons
		{
			get { return _Seasons; }
			set
			{
				this._Seasons = value;
				this.OnPropertyChanged();
			}
		}

		public int SelectedSeasonIndex {  get; set; }

		public Season SelectedSeason =>
			Seasons?[SelectedSeasonIndex].Season;
	}

}
