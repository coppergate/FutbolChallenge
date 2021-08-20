using FutbolChallenge.Data.Repository.Model;
using FutbolChallengeUI;
using FutbolChallengeUI.Controls;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeApp
{

	public sealed partial class ParticipantManagement : Window, INotifyPropertyChanged
	{
		private readonly IFutbolChallengeServiceClient _ServiceClient;

		public event PropertyChangedEventHandler PropertyChanged;

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

		public ParticipantManagement(IFutbolChallengeServiceClient serviceClient)
		{
			this.InitializeComponent();
			_ServiceClient = serviceClient;
			base.Title = "Manage Participants";

			ParticipantPanelView.EditParticipant += ParticpantPanelView_EditParticipant;
			ParticipantPanelView.DeleteAddParticipant += ParticipantPanelView_DeleteOrAddParticipant;

			

		}

		private async void ParticipantPanelView_DeleteOrAddParticipant(object sender, DeleteOrAddPanelEventArgs e)
		{
			if (e.Id > 0)
			{
				await _ServiceClient.DeleteParticipant(e.Id);
			}
			else
			{
				Participant local = new Participant() {
					EmailAddress = participantAddView.EmailAddress,
					LastName = participantAddView.LastName,
					FirstName = participantAddView.FirstName,
				};

				await _ServiceClient.InsertParticipant(local);
			}
			await LoadParticipants();
		}

		private async void ParticpantPanelView_EditParticipant(object sender, EditPanelEventArgs<Participant> e)
		{
			await _ServiceClient.UpdateParticipant(e.EditTarget);
			await LoadParticipants();
		}

		async public Task LoadParticipants()
		{
			LoadingMessage = "Loading...";
			var participants = await _ServiceClient.FetchAllParticipants();
			ParticipantListViewModel.Participants = new ObservableCollection<Participant>(participants);
			participantView.Reload();
			participantAddView.Clear();
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

