using FutbolChallenge.Data.Repository.Model;
using System.Collections.ObjectModel;

namespace FutbolChallengeUI.ViewModels
{
	public class SeasonListViewModel : BindableBase
	{
		public SeasonListViewModel()
		{
			
		}

		private ObservableCollection<Season> _Seasons;

		public ObservableCollection<Season> Seasons
		{
			get { return _Seasons; }
			set
			{
				this._Seasons = value;
		//		this.OnPropertyChanged();
			}
		}

	}

}
