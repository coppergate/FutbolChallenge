using FutbolChallengeUI;
using Helpers.Core.DateTimeProvider;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeApp
{

	public sealed partial class MainWindow : Window
	{
		private readonly IFutbolChallengeServiceClient _ServiceClient;
		private readonly IDateTimeProvider _DateTimeProvider;

		private ParticipantManagement _ParticipantManagement;
		private SeasonScheduleManagement _SeasonScheduleManagement;
		private GameManagement _GameManagement;

		public MainWindow(IFutbolChallengeServiceClient serviceClient, IDateTimeProvider dateTimeProvider)
		{
			this.InitializeComponent();
			_ServiceClient = serviceClient;
			_DateTimeProvider = dateTimeProvider;
		}

		private async void ParticipantMaintenanceButton_Click(object sender, RoutedEventArgs e)
		{
			await DisplayParticipantManagement();
		}

		private async void SeasonScheduleMaintenanceButton_Click(object sender, RoutedEventArgs e)
		{
			await DisplaySeasonScheduleManagement();
		}
		private async void GameMaintenanceButton_Click(object sender, RoutedEventArgs e)
		{
			await DisplayGameManagement();
		}

		
		private async Task DisplayParticipantManagement()
		{
			if (_ParticipantManagement is null)
			{
				_ParticipantManagement = new ParticipantManagement(_ServiceClient);
				_ParticipantManagement.Closed += _ParticipantManagementViewModel_Closed;
			}

			await _ParticipantManagement.LoadParticipants();
			_ParticipantManagement.Activate();
		}

		private void _ParticipantManagementViewModel_Closed(object sender, WindowEventArgs args)
		{
			_ParticipantManagement = null;
		}


		private async Task DisplaySeasonScheduleManagement()
		{
			if (_SeasonScheduleManagement is null)
			{
				_SeasonScheduleManagement = new SeasonScheduleManagement(_ServiceClient);
				_SeasonScheduleManagement.Closed += _DisplaySeasonScheduleManagementViewModel_Closed;
			}
			await _SeasonScheduleManagement.LoadSeasons();
			_SeasonScheduleManagement.Activate();
		}

		private void _DisplaySeasonScheduleManagementViewModel_Closed(object sender, WindowEventArgs args)
		{
			_SeasonScheduleManagement = null;
		}


		private async Task DisplayGameManagement()
		{
			if (_GameManagement is null)
			{
				_GameManagement = new GameManagement(_ServiceClient, _DateTimeProvider);
				_GameManagement.Closed += _GameManagementViewModel_Closed;
			}

			await _GameManagement.LoadMatches();
			await _GameManagement.SelectCurrentMatchGroup();
			_GameManagement.Activate();
		}

		private void _GameManagementViewModel_Closed(object sender, WindowEventArgs args)
		{
			_GameManagement = null;
		}
	}
}
