using FutbolChallengeUI.ViewModels;

namespace FutbolChallengeUI.Controls
{
	public sealed partial class MatchListView : BindableUserControlBase
	{

		private MatchListViewModel? _MatchListViewModel;

		public MatchListViewModel? MatchListViewModel
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

		private void ParticipantViewGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
		{
			var i = e.OriginalSource;
		}

		private void ParticipantViewGrid_ItemClick(object sender, Microsoft.UI.Xaml.Controls.ItemClickEventArgs e)
		{
			var v = e.ClickedItem;
		}

		private void matchPanel_GotFocus(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
		{
			var p = e.OriginalSource;
		}

		private void matchGoupSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
		{
			_MatchListViewModel?.SliderValueSet(e.NewValue);
		}

	}
}
