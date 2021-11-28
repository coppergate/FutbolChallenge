using FutbolChallenge.Data.Model;
using System;
using System.Collections.ObjectModel;

namespace FutbolChallengeUI.ViewModels
{
	public class SeasonListViewModel : BindableBase
	{
		public SeasonListViewModel()
		{
			
		}

		private ObservableCollection<SeasonPanelViewModel>? _Seasons;

		public ObservableCollection<SeasonPanelViewModel> Seasons
		{
			get { return _Seasons ?? throw new InvalidOperationException("There is no view model associated to this view"); }
			set
			{
				this._Seasons = value;
				this.OnPropertyChanged();
			}
		}

		public int SelectedSeasonIndex {  get; set; }

		public Season? SelectedSeason =>
			Seasons?[SelectedSeasonIndex].Season;
	}

}
