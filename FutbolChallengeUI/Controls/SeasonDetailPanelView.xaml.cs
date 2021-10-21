using FutbolChallengeUI.ViewModels;
using System.ComponentModel;

namespace FutbolChallengeUI.Controls
{
	public sealed partial class SeasonDetailPanelView : BindableUserControlBase, INotifyPropertyChanged
	{

		private SeasonDetailViewModel _SeasonDetailViewModel = new SeasonDetailViewModel();

		public SeasonDetailViewModel SeasonDetailViewModel
		{
			get => _SeasonDetailViewModel;
			set
			{
				_SeasonDetailViewModel = value;
				OnPropertyChanged();
			}
		}

		public SeasonDetailPanelView()
		{
			this.InitializeComponent();
		}

		
	}
}
