using FutbolChallengeUI.ViewModels;

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


		public ParticipantListView()
		{
			this.InitializeComponent();
		}

		public void Reload()
		{
			OnPropertyChanged("ParticipantListViewModel");
		}
	}
}
