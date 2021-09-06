using FutbolChallenge.Data.Model;
using FutbolChallengeUI.ViewModels;

namespace FutbolChallengeUI.Controls
{
	public sealed partial class SeasonComboView : BindableUserControlBase
	{
		public event SelectedSeasonChangedEventHandler SelectedSeasonChanged;

		private SeasonListViewModel _SeasonListViewModel = new SeasonListViewModel();

		public SeasonListViewModel SeasonListViewModel
		{
			get => _SeasonListViewModel;
			set
			{
				_SeasonListViewModel = value;
				NotifyAllOnPropertyChanged();
			}
		}

		public SeasonComboView()
		{
			this.InitializeComponent();
		}

		public System.Collections.Generic.IEnumerable<Season> Seasons =>
			SeasonListViewModel?.Seasons;

		public int SelectedSeason
		{
			get => _SeasonListViewModel.SelectedSeasonIndex;
			set
			{
				if (_SeasonListViewModel.Seasons?.Count > value)
				{
					_SeasonListViewModel.SelectedSeasonIndex = value;
					SelectedSeasonChanged?.Invoke(this, new SelectedSeasonChangedEventArgs(_SeasonListViewModel.Seasons[value]));
					OnPropertyChanged();
				}
			}
		}
	}
}
