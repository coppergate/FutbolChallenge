using FutbolChallengeUI;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeApp
{

	public sealed partial class MainWindow : Window
	{
		private readonly IFutbolChallengeServiceClient _ServiceClient;
		private ParticipantManagement _ParticipantManagementViewModel;
		private SeasonScheduleManagement _DisplaySeasonScheduleManagementViewModel;

		public MainWindow(IFutbolChallengeServiceClient serviceClient)
		{
			this.InitializeComponent();
			_ServiceClient = serviceClient;
		}

		private async void ParticipantMaintenanceButton_Click(object sender, RoutedEventArgs e)
		{
			DisplayParticipantManagement();
		}

		private async void SeasonScheduleMaintenanceButton_Click(object sender, RoutedEventArgs e)
		{
			DisplaySeasonScheduleManagement();
		}
		

		private async void DisplayParticipantManagement()
		{
			if (_ParticipantManagementViewModel is null)
			{
				_ParticipantManagementViewModel = new ParticipantManagement(_ServiceClient);
				_ParticipantManagementViewModel.Closed += _ParticipantManagementViewModel_Closed;
			}

			await _ParticipantManagementViewModel.LoadParticipants();
			_ParticipantManagementViewModel.Activate();
		}

		private void _ParticipantManagementViewModel_Closed(object sender, WindowEventArgs args)
		{
			_ParticipantManagementViewModel = null;
		}


		private async void DisplaySeasonScheduleManagement()
		{
			if (_DisplaySeasonScheduleManagementViewModel is null)
			{
				_DisplaySeasonScheduleManagementViewModel = new SeasonScheduleManagement(_ServiceClient);
				_DisplaySeasonScheduleManagementViewModel.Closed += _DisplaySeasonScheduleManagementViewModel_Closed;
			}
			await _DisplaySeasonScheduleManagementViewModel.LoadSeasons();
			_DisplaySeasonScheduleManagementViewModel.Activate();
		}

		private void _DisplaySeasonScheduleManagementViewModel_Closed(object sender, WindowEventArgs args)
		{
			_DisplaySeasonScheduleManagementViewModel = null;
		}
	}
}
