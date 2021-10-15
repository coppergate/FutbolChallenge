using FutbolChallengeUI.Enums;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;


namespace FutbolChallengeUI.Controls
{
	public sealed partial class ParticipantPanelView : BindableUserControlBase
	{
		public ParticipantPanelView()
		{
			this.InitializeComponent();
			this.LosingFocus += ParticipantPanelView_LosingFocus;
		}

		private ParticipantPanelViewModel _ParticipantPanelViewModel;
		public ParticipantPanelViewModel ParticipantPanelViewModel
		{
			get { return _ParticipantPanelViewModel; }
			set 
			{ 
				_ParticipantPanelViewModel = value;
				ShowEdit = _ParticipantPanelViewModel.ShowEdit;
				EnableTextEditing = _ParticipantPanelViewModel.EnableTextEditing;
				EditMode = _ParticipantPanelViewModel.EditMode;
				NotifyAllOnPropertyChanged();
			}
		}

		private async void ParticipantPanelView_LosingFocus(UIElement sender, Microsoft.UI.Xaml.Input.LosingFocusEventArgs args)
		{
			if (args.NewFocusedElement != null)
			{
				switch (args.NewFocusedElement)
				{
					case Button bt:
						if (bt.Parent == this.ControlsOuterGrid) return;
						break;
					case TextBox box:
						if (box.Parent == this.GridControlInner) return;
						break;
				}

				if (_EditMode == EditMode.Edit)
				{
					ContentDialogResult result = await this.EditInProgressDialog.ShowAsync();

					if (result == ContentDialogResult.Primary)
					{
						ParticipantPanelViewModel.ParticipantActionClick();
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
				EditParticipantButton.Visibility = _ShowEdit ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		private bool _EnableTextEditing;
		public bool EnableTextEditing
		{
			get { return _EnableTextEditing; }
			set
			{
				_EnableTextEditing = value;
				ParticipantEmail.IsReadOnly = !_EnableTextEditing;
				ParticipantFName.IsReadOnly = !_EnableTextEditing;
				ParticipantLName.IsReadOnly = !_EnableTextEditing;
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
				_EditMode = value;

				var editBackground = new SolidColorBrush(Color.FromArgb(128, 45, 45, 45));

				if (_EditMode == EditMode.Delete)
				{
					ParticipantActionButton.Visibility = Visibility.Visible;
					ParticipantActionButton.Content = DeleteModeButtonContent;
					ParticipantActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
				}
				else if (_EditMode == EditMode.Add)
				{
					ParticipantActionButton.Visibility = Visibility.Visible;
					ParticipantActionButton.Content = AddModeButtonContent;
					ParticipantActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
					editBackground = new SolidColorBrush(Color.FromArgb(128, 0, 0, 123));
				}
				else if (_EditMode == EditMode.Edit)
				{
					ParticipantActionButton.Visibility = Visibility.Visible;
					ParticipantActionButton.Content = EditModeButtonContent;
					ParticipantActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
					editBackground = new SolidColorBrush(Color.FromArgb(128, 123, 0, 0));
				}
				else
				{
					ParticipantActionButton.Visibility = Visibility.Collapsed;
					ParticipantActionButton.Content = "";
				}

				ParticipantEmail.Background = editBackground;
				ParticipantFName.Background = editBackground;
				ParticipantLName.Background = editBackground;
				ParticipantEmail.Background = editBackground;
			}
		}

		public void Clear()
		{
			ParticipantEmail.Text = "";
			ParticipantFName.Text = "";
			ParticipantLName.Text = "";
		}

	}
}
