using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeUI
{

	public sealed partial class MainWindow : Window
	{

		private MainWindowViewModel _MainWindowViewModel;

		public MainWindow(MainWindowViewModel mainWindowViewModel)
		{
			this.InitializeComponent();
			_MainWindowViewModel = mainWindowViewModel;
		}

		private async void ParticipantMaintenanceButton_Click(object sender, RoutedEventArgs e)
		{
			await _MainWindowViewModel.ShowParticipantMaintenance();
		}

		private async void SeasonScheduleMaintenanceButton_Click(object sender, RoutedEventArgs e)
		{
			await _MainWindowViewModel.ShowSeasonScheduleMaintenance();
		}

		private async void GameMaintenanceButton_Click(object sender, RoutedEventArgs e)
		{
			await _MainWindowViewModel.ShowGameMaintenance();
		}
	}
}
