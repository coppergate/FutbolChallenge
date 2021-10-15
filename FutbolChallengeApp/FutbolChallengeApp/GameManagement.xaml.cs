using FutbolChallenge.Data.Model;
using FutbolChallengeUI;
using FutbolChallengeUI.EventHandlers.EventArgs;
using FutbolChallengeUI.ViewModels;
using Helpers.Core.DateTimeProvider;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace FutbolChallengeApp
{

	public sealed partial class GameManagement : Window, INotifyPropertyChanged
	{
		private readonly IFutbolChallengeServiceClient _ServiceClient;
		private readonly IDateTimeProvider _DateTimeProvider;

		public event PropertyChangedEventHandler PropertyChanged;

		private MatchListViewModel _MatchListViewModel = new MatchListViewModel();
		public MatchListViewModel MatchListViewModel
		{
			get { return _MatchListViewModel; }
			set
			{
				_MatchListViewModel = value;
				matchListView.MatchListViewModel = _MatchListViewModel;
				this.OnPropertyChanged();
			}
		}

		private string _LoadingMessage;
		public string LoadingMessage
		{
			get { return _LoadingMessage; }
			set { _LoadingMessage = value; OnPropertyChanged(); }
		}

		public GameManagement(IFutbolChallengeServiceClient serviceClient, IDateTimeProvider dateTimeProvider)
		{
			this.InitializeComponent();
			_ServiceClient = serviceClient;
			_DateTimeProvider = dateTimeProvider;

			base.Title = "Manage Games";

			MatchPanelViewModel.EditMatch += MatchPanelView_EditMatch;
			MatchPanelViewModel.DeleteMatch += MatchPanelView_DeleteMatch;
			MatchPanelViewModel.AddMatch += MatchPanelView_AddMatch;
		}

		private async void MatchPanelView_DeleteMatch(object sender, DeleteEntityEventArgs e)
		{
			await _ServiceClient.DeleteMatch(e.Id);
			await LoadMatches();
		}

		private async void MatchPanelView_AddMatch(object sender, AddEntityEventArgs<SeasonGame> e)
		{
			ScheduledGame local = new() {
				Id = e.AddTarget.MatchId,
				HomeTeamId = e.AddTarget.HomeTeamId,
				VisitingTeamId = e.AddTarget.VisitingTeamId,
				HomeTeamScore = e.AddTarget.HomeTeamScore,
				VisitingTeamScore = e.AddTarget.VisitingTeamScore,
				MatchDate = e.AddTarget.MatchDate,
				MatchGroupId = e.AddTarget.MatchGroupId,
			};

			await _ServiceClient.InsertMatch(local);
			await LoadMatches();
		}


		private async void MatchPanelView_EditMatch(object sender, EditEntityEventArgs<SeasonGame> e)
		{
			ScheduledGame local = new() {
				Id = e.EditTarget.MatchId,
				HomeTeamId = e.EditTarget.HomeTeamId,
				VisitingTeamId = e.EditTarget.VisitingTeamId,
				HomeTeamScore = e.EditTarget.HomeTeamScore,
				VisitingTeamScore = e.EditTarget.VisitingTeamScore,
				MatchDate = e.EditTarget.MatchDate,
				MatchGroupId = e.EditTarget.MatchGroupId,
			};
			await _ServiceClient.UpdateMatch(local);
			await LoadMatches();
		}

		async public Task LoadMatches()
		{
			LoadingMessage = "Loading...";
			var Matches = await _ServiceClient.GetSeasonGames(27);
			if(Matches == null)
			{
				MatchListViewModel.MatchGroupSequence = -1;
				MatchListViewModel.SeasonId = -1;
				matchListView.Reload();
				LoadingMessage = "Loaded";
			}

			MatchListViewModel.Matches = new ObservableCollection<MatchPanelViewModel>(Matches.Select(p => new MatchPanelViewModel(_DateTimeProvider) {
																															Game = p,
																															ShowScores = false,
																															EditMode = FutbolChallengeUI.Enums.EditMode.None,
																															EnableTextEditing = false,
																															ShowEdit = true,
																															AllowScoreEdits = _DateTimeProvider.CurrentUtcDateTime <= p.MatchDate,
																														})) ;

			//	TODO: fix this.....
			MatchListViewModel.MatchGroupSequence = 1;
			MatchListViewModel.SeasonId = 27;


			matchListView.Reload();
			LoadingMessage = "Loaded";
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}

