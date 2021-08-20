using FutbolChallenge.Data.Repository.Model;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeUI.Controls
{
	public sealed partial class SeasonPanelView : BindableUserControlBase
	{
		static public event EditPanelEventHandler<Season> EditSeason;
		static public event DeleteOrAddPanelEventHandler DeleteOrAddSeason;

		private Season _Season = new Season();

		public Season Season
		{
			get => _Season;
			set
			{
				_Season = value;
//				OnPropertyChanged();
			}
		}

		public SeasonPanelView()
		{
			this.InitializeComponent();
		}

	
		public string SeasonName
		{
			get => Season?.Name;
			set { Season.Name = value; }
		}

		public DateTime StartDate
		{
			get => Season?.StartDate ?? new DateTime(1900, 1, 1);
			set { Season.StartDate = value; }
		}

		public DateTime EndDate
		{
			get => Season?.EndDate ?? new DateTime(1900, 1, 1);
			set { Season.EndDate = value; }
		}

		public string Id =>
			(Season?.Id ?? 0).ToString();

		private bool _ShowEdit;
		public string ShowEdit
		{
			get { return _ShowEdit.ToString(); }
			set
			{
				_ShowEdit = bool.Parse(value);
				EditSeasonButton.Visibility = _ShowEdit ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		private bool _EnableTextEditing;
		public string EnableTextEditing
		{
			get { return _EnableTextEditing.ToString(); }
			set
			{
				_EnableTextEditing = bool.Parse(value);
				SeasonNameTextBox.IsReadOnly = !_EnableTextEditing;
				SeasonStartDateTextBox.IsReadOnly = !_EnableTextEditing;
				SeasonEndDateTextBox.IsReadOnly = !_EnableTextEditing;
			}
		}

		enum EditMode
		{
			Add,
			Delete,
			Update,
			None,
		}

		private EditMode _EditMode;
		public string Mode
		{
			get { return _EditMode.ToString(); }
			set
			{
				_EditMode = EditMode.None;
				object? parseOut;
				if (Enum.TryParse(typeof(EditMode), value, out parseOut))
				{
					_EditMode = (EditMode)parseOut;
				}
				var editBackground = new SolidColorBrush(Color.FromArgb(128, 45, 45, 45));

				if (_EditMode == EditMode.Delete)
				{
					SeasonActionButton.Visibility = Visibility.Visible;
					SeasonActionButton.Content = "X";
					SeasonActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
				}
				else if (_EditMode == EditMode.Add)
				{
					SeasonActionButton.Visibility = Visibility.Visible;
					SeasonActionButton.Content = "+";
					SeasonActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
					editBackground = new SolidColorBrush(Color.FromArgb(128, 0, 0, 123));
				}
				else if (_EditMode == EditMode.Update)
				{
					SeasonActionButton.Visibility = Visibility.Visible;
					SeasonActionButton.Content = "^";
					SeasonActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
					editBackground = new SolidColorBrush(Color.FromArgb(128, 123, 0, 0));
				}
				else
				{
					SeasonActionButton.Visibility = Visibility.Collapsed;
					SeasonActionButton.Content = "";
				}

				SeasonNameTextBox.Background = editBackground;
				SeasonStartDateTextBox.Background = editBackground;
				SeasonEndDateTextBox.Background = editBackground;
			}
		}

		private void EditSeasonButton_Click(object sender, RoutedEventArgs e)
		{
			Mode = "Update";
			EnableTextEditing = "True";
		}

		private void SeasonActionButton_Click(object sender, RoutedEventArgs e)
		{
			if (_EditMode == EditMode.Update)
			{
				EditSeason?.Invoke(this, new EditPanelEventArgs<Season>(_Season));
				Mode = "Delete";
				EnableTextEditing = "False";
			}
			else
			{
				int id = int.Parse(((Button)sender).Tag.ToString());
				DeleteOrAddSeason?.Invoke(this, new DeleteOrAddPanelEventArgs(id));
			}
		}
	}
}
