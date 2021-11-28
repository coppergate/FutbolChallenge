using FutbolChallengeUI.EventHandlers;
using FutbolChallengeUI.EventHandlers.EventArgs;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;

namespace FutbolChallengeUI.Controls
{
	public sealed partial class MatchGroupSelector : BindableUserControlBase
	{

		public event SelectedMatchGroupChangedEventHandler? SelectedMatchGroupChangedEventHandler;

		public MatchGroupSelector()
		{
			this.InitializeComponent();
		}

		private string _MatchGroupTitle = string.Empty;
		public string MatchGroupTitle
		{
			get { return _MatchGroupTitle; }
			set { _MatchGroupTitle = value; OnPropertyChanged(); }
		}

		private void PreviousGroupButton_Click(object sender, RoutedEventArgs e)
		{
			SelectedMatchGroupChangedEventHandler?.Invoke(this, new SelectedMatchGroupChangedEventArgs(MatchGroupChangeDirection.DOWN));
		}

		private void NextGroupButton_Click(object sender, RoutedEventArgs e)
		{
			SelectedMatchGroupChangedEventHandler?.Invoke(this, new SelectedMatchGroupChangedEventArgs(MatchGroupChangeDirection.UP));
		}

	}
}
