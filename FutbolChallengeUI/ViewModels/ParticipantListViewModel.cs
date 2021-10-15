using System.Collections.ObjectModel;

namespace FutbolChallengeUI.ViewModels
{
	public class ParticipantListViewModel : BindableBase
	{
		public ParticipantListViewModel()
		{
			
		}

		private ObservableCollection<ParticipantPanelViewModel> _Participants;

		public ObservableCollection<ParticipantPanelViewModel> Participants
		{
			get { return _Participants; }
			set
			{
				this._Participants = value;
				this.OnPropertyChanged();
			}
		}

	}

}
