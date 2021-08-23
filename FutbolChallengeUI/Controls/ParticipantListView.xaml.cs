using FutbolChallenge.Data.Model;
using FutbolChallengeUI.ViewModels;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeUI.Controls
{
	public sealed partial class ParticipantListView : BindableUserControlBase
	{

		private ParticipantListViewModel _ParticipantListViewModel;

		public ParticipantListViewModel ParticipantListViewModel
		{
			get { return _ParticipantListViewModel; }
			set
			{
				_ParticipantListViewModel = value;
				Reload();
			}
		}


		public IEnumerable<Participant> Participants =>
			_ParticipantListViewModel?.Participants;

		public ParticipantListView()
		{
			this.InitializeComponent();
		}

		public void Reload()
		{
			OnPropertyChanged("ParticipantListViewModel");
			OnPropertyChanged("Participants");
		}
	}
}
