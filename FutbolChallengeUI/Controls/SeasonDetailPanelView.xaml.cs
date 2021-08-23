using FutbolChallengeUI.ViewModels;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

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
