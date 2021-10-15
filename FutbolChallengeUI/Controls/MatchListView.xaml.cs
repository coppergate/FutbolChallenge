using FutbolChallengeUI.ViewModels;

namespace FutbolChallengeUI.Controls
{
	public sealed partial class MatchListView : BindableUserControlBase
	{

		private MatchListViewModel _MatchListViewModel;

		public MatchListViewModel MatchListViewModel
		{
			get { return _MatchListViewModel; }
			set
			{
				_MatchListViewModel = value;
				Reload();
			}
		}

		public int MatchGroupSequence
		{
			get; set;
		} = -1;

		public MatchListView()
		{
			this.InitializeComponent();
		}

		public void Reload()
		{
			OnPropertyChanged("MatchListViewModel");
		}

		private void MatchSequenceAccessButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
		{

		}
	}
}
