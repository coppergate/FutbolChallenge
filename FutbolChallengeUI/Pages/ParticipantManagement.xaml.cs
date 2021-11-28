using FutbolChallenge.Data.Model;
using FutbolChallengeUI;
using FutbolChallengeUI.EventHandlers.EventArgs;
using FutbolChallengeUI.ServiceClient;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace FutbolChallengeUI
{

	public sealed partial class ParticipantManagement : Window, INotifyPropertyChanged
	{
		private readonly IFutbolChallengeParticipantServiceClient _ServiceClient;

		public event PropertyChangedEventHandler? PropertyChanged;

		private ParticipantListViewModel _ParticipantListViewModel = new ParticipantListViewModel();
		public ParticipantListViewModel ParticipantListViewModel
		{
			get { return _ParticipantListViewModel; }
			set
			{
				_ParticipantListViewModel = value;
				participantView.ParticipantListViewModel = _ParticipantListViewModel;
				this.OnPropertyChanged();
			}
		}

		private string _LoadingMessage;
		public string LoadingMessage
		{
			get { return _LoadingMessage; }
			set { _LoadingMessage = value; OnPropertyChanged(); }
		}

		public ParticipantManagement(IFutbolChallengeParticipantServiceClient serviceClient)
		{
			this.InitializeComponent();
			_ServiceClient = serviceClient;
			base.Title = "Manage Participants";

			ParticipantPanelViewModel.EditParticipant += ParticpantPanelView_EditParticipant;
			ParticipantPanelViewModel.DeleteParticipant += ParticipantPanelView_DeleteParticipant;
			ParticipantPanelViewModel.AddParticipant += ParticipantPanelView_AddParticipant;

			_LoadingMessage = "";
		}

		private async void ParticipantPanelView_DeleteParticipant(object? sender, DeleteEntityEventArgs e)
		{
			await _ServiceClient.DeleteParticipant(e.Id);
			await LoadParticipants();
		}

		private async void ParticipantPanelView_AddParticipant(object? sender, AddEntityEventArgs<Participant> e)
		{
			Participant local = new() {
				EmailAddress = e.AddTarget.EmailAddress,
				LastName = e.AddTarget.LastName,
				FirstName = e.AddTarget.FirstName,
			};

			await _ServiceClient.InsertParticipant(local);
			await LoadParticipants();
		}


		private async void ParticpantPanelView_EditParticipant(object? sender, EditEntityEventArgs<Participant> e)
		{
			await _ServiceClient.UpdateParticipant(e.EditTarget);
			await LoadParticipants();
		}

		async public Task LoadParticipants()
		{
			LoadingMessage = "Loading...";
			var participants = await _ServiceClient.FetchAllParticipants();

			ParticipantListViewModel.Participants = new ObservableCollection<ParticipantPanelViewModel>(participants.Select(p => new ParticipantPanelViewModel() { Participant = p }));
			participantView.Reload();
			participantAddView.Clear();
			LoadingMessage = "Loaded";
		}

		public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}

