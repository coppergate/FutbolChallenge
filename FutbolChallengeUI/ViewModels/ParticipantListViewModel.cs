using FutbolChallenge.Data.Model;
using System.Collections.ObjectModel;

namespace FutbolChallengeUI.ViewModels
{
	public class ParticipantListViewModel : BindableBase
	{
		public ParticipantListViewModel()
		{
			
		}

		private ObservableCollection<Participant> _Participants;

		public ObservableCollection<Participant> Participants
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
