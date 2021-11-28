using Helpers.Core.DateTimeProvider;
using Microsoft.UI.Xaml;
using Ninject;
using System.Threading.Tasks;

namespace FutbolChallengeUI.ViewModels
{

	public class MainWindowViewModel
	{

		private readonly IDateTimeProvider _DateTimeProvider;
		private readonly IKernel _StandardKernel;
		private  ParticipantManagement? _ParticipantManagement;
		private  SeasonScheduleManagement? _SeasonScheduleManagement;
		private  GameManagement? _GameManagement;

		public MainWindowViewModel(IDateTimeProvider dateTimeProvider,
									IKernel kernel
								)
		{
			_DateTimeProvider = dateTimeProvider;
			_StandardKernel = kernel;
		}

		public async Task ShowParticipantMaintenance()
		{
			await DisplayParticipantManagement();
		}

		public async Task ShowSeasonScheduleMaintenance()
		{
			await DisplaySeasonScheduleManagement();
		}

		public async Task ShowGameMaintenance()
		{
			await DisplayGameManagement();
		}

		private async Task DisplayParticipantManagement()
		{
			if(_ParticipantManagement == null)
			{
				_ParticipantManagement = _StandardKernel.Get<ParticipantManagement>();
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
				_SeasonScheduleManagement = _StandardKernel.Get<SeasonScheduleManagement>();
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
				_GameManagement = _StandardKernel.Get<GameManagement>();
				_GameManagement.Closed += _GameManagementViewModel_Closed;
			}

			await _GameManagement.LoadMatches();
			_GameManagement.SelectCurrentMatchGroup();
			_GameManagement.Activate();
		}

		private void _GameManagementViewModel_Closed(object sender, WindowEventArgs args)
		{
			_GameManagement = null;
		}

	}
}
