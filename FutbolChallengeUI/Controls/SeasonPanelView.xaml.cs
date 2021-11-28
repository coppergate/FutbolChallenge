using FutbolChallengeUI.Enums;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

namespace FutbolChallengeUI.Controls
{
	public sealed partial class SeasonPanelView : BindableUserControlBase
	{


		public SeasonPanelView()
		{
			this.InitializeComponent();
			this.LosingFocus += SeasonPanelView_LosingFocus;
		}

		private SeasonPanelViewModel? _Season = null;

		public SeasonPanelViewModel SeasonViewModel
		{
			get { return _Season ??  new SeasonPanelViewModel(); } 
			set 
			{ 
				_Season = value;
				ShowEdit = _Season.ShowEdit;
				EnableTextEditing = _Season.EnableTextEditing;
				EditMode = _Season.EditMode;
				NotifyAllOnPropertyChanged();
			}
		}


		private async void SeasonPanelView_LosingFocus(UIElement sender, Microsoft.UI.Xaml.Input.LosingFocusEventArgs args)
		{
			if (args.NewFocusedElement != null)
			{
				switch (args.NewFocusedElement)
				{
					case Button bt:
						if (bt.Parent == this.PanelOuterGrid) return;
						break;
					case TextBox box:
						if (box.Parent == this.PanelInnerGrid) return;
						break;
				}


				if (_EditMode == EditMode.Edit)
				{
					ContentDialogResult result = await this.EditInProgressDialog.ShowAsync();

					if (result == ContentDialogResult.Primary)
					{
						SeasonViewModel.ParticipantActionClick();
					}
					EditMode = EditMode.Delete;
					EnableTextEditing = false;
				}
			}
		}

		private bool _ShowEdit;
		public bool ShowEdit
		{
			get { return _ShowEdit; }
			set
			{
				_ShowEdit = value;
				EditSeasonButton.Visibility = _ShowEdit ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		private bool _EnableTextEditing;
		public bool EnableTextEditing
		{
			get { return _EnableTextEditing; }
			set
			{
				_EnableTextEditing = value;
				SeasonNameTextBox.IsReadOnly = !_EnableTextEditing;
				SeasonStartDateTextBox.IsReadOnly = !_EnableTextEditing;
				SeasonEndDateTextBox.IsReadOnly = !_EnableTextEditing;
			}
		}

		public object DeleteModeButtonContent { get; set; } = "X";
		public object AddModeButtonContent { get; set; } = "+";
		public object EditModeButtonContent { get; set; } = "^";

		private EditMode _EditMode;
		public EditMode EditMode
		{
			get { return _EditMode; }
			set
			{
				_EditMode =  value;
				var editBackground = new SolidColorBrush(Color.FromArgb(128, 45, 45, 45));

				if (_EditMode == EditMode.Delete)
				{
					SeasonActionButton.Visibility = Visibility.Visible;
					SeasonActionButton.Content = DeleteModeButtonContent;
					SeasonActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
				}
				else if (_EditMode == EditMode.Add)
				{
					SeasonActionButton.Visibility = Visibility.Visible;
					SeasonActionButton.Content = AddModeButtonContent;
					SeasonActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
					editBackground = new SolidColorBrush(Color.FromArgb(128, 0, 0, 123));
				}
				else if (_EditMode == EditMode.Edit)
				{
					SeasonActionButton.Visibility = Visibility.Visible;
					SeasonActionButton.Content = EditModeButtonContent;
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
	}
}
