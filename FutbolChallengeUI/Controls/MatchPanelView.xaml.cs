using FutbolChallengeUI.Enums;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

namespace FutbolChallengeUI.Controls
{
	public sealed partial class MatchPanelView : BindableUserControlBase
	{

		/*
		
			1) show teams and date - no edit, just for match group display
			2) show teams and date - edit (admin)
			3) show teams, scores and date - no edit, match group display with scores
			4) show teams, scores and date - edit scores (admin)
				a) edit scores (participant)

		*/

		public MatchPanelView()
		{
			this.InitializeComponent();
			this.LosingFocus += SeasonPanelView_LosingFocus;
		}

		private MatchPanelViewModel _Match;

		public MatchPanelViewModel MatchPanelViewModel
		{
			get { return _Match; }
			set
			{
				_Match = value;
				EnableTextEditing = _Match.EnableTextEditing;
				ShowScores = _Match.ShowScores;
				EditMode = _Match.EditMode;
				ShowEdit = _Match.ShowEdit;
				NotifyAllOnPropertyChanged();
			}
		}

		private bool TextChanged { get; set; } = false;

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
					case GridViewHeaderItem headerItem:
						return;
				}

				if (_EditMode == EditMode.Edit && TextChanged)
				{
					ContentDialogResult result = await this.EditInProgressDialog.ShowAsync();

					if (result == ContentDialogResult.Primary)
					{
						MatchPanelViewModel.MatchActionClick();
					}
				}

				ShowScores = false;
				EditMode = EditMode.None;
				EnableTextEditing = false;
				TextChanged = false;
			}
		}

		private bool _ShowEdit;
		public bool ShowEdit
		{
			get { return _ShowEdit; }
			set
			{
				_ShowEdit = value;
				EditMatchButton.Visibility = _ShowEdit ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		private bool _EnableTextEditing;
		public bool EnableTextEditing
		{
			get { return _EnableTextEditing; }
			set
			{
				_EnableTextEditing = value;

				TextBoxReadOnly(!value, MatchHomeTeamTextBox);

				TextBoxReadOnly(!value && _AllowScoreEdits, MatchHomeTeamScoreTextBox);
				TextBoxReadOnly(!value && _AllowScoreEdits, MatchAwayTeamScoreTextBox);

				TextBoxReadOnly(!value, MatchAwayTeamTextBox);
				TextBoxReadOnly(!value, MatchDateTextBox);
			}
		}

		public void TextBoxReadOnly(bool isReadOnly, TextBox textBox)
		{
			textBox.IsReadOnly = isReadOnly;
			textBox.AllowFocusOnInteraction = !isReadOnly;
			textBox.AllowFocusWhenDisabled = !isReadOnly;
		}

		private bool _AllowScoreEdits;
		public bool AllowScoreEdits
		{
			get => _AllowScoreEdits;
			set
			{
				_AllowScoreEdits = value;
			}
		}

		public int DisplayedScoreColumnWidth { get; set; } = 50;
		public int ScoreDisplayedActionColumnWidth { get; set; } = 45;
		public int ScoreDisplayedTextBoxColumnWidth { get; set; } = 650;
		public int NoScoreDisplayedTextBoxColumnWidth { get; set; } = 550;

		private bool _ShowScores;
		public bool ShowScores
		{
			get => _ShowScores;
			set
			{
				_ShowScores = value;
				var scoreBoxWidth = 0;
				var scoreBoxColumnWidth = 0;
				var actionColumnWidth = 0;
				var textBoxColumnWidth = NoScoreDisplayedTextBoxColumnWidth;

				if (_ShowScores)
				{
					scoreBoxWidth = DisplayedScoreColumnWidth;
					scoreBoxColumnWidth = DisplayedScoreColumnWidth;
					actionColumnWidth = ScoreDisplayedActionColumnWidth;
					textBoxColumnWidth = ScoreDisplayedTextBoxColumnWidth;

				}
				MatchHomeTeamScoreTextBox.Width = scoreBoxWidth;
				MatchHomeTeamScoreLabel.Width = scoreBoxWidth;
				PanelInnerGrid.ColumnDefinitions[1].Width = new GridLength(scoreBoxColumnWidth);

				MatchAwayTeamScoreTextBox.Width = scoreBoxWidth;
				MatchAwayTeamScoreLabel.Width = scoreBoxWidth;
				PanelInnerGrid.ColumnDefinitions[2].Width = new GridLength(scoreBoxColumnWidth);

				PanelOuterGrid.ColumnDefinitions[1].Width = new GridLength(textBoxColumnWidth);
				PanelOuterGrid.ColumnDefinitions[2].Width = new GridLength(actionColumnWidth);
			}
		}

		public object DeleteModeButtonContent { get; set; } = "X";
		public object AddModeButtonContent { get; set; } = "+";
		public object EditModeButtonContent { get; set; } = "^";

		public Color EditModeNoneActionButtonColor { get; set; } = Color.FromArgb(128, 45, 45, 45);
		public Color EditModeDeleteActionButtonColor { get; set; } = Color.FromArgb(255, 255, 0, 0);
		public Color EditModeAddActionButtonColor { get; set; } = Color.FromArgb(255, 0, 255, 0);
		public Color EditModeEditActionButtonColor { get; set; } = Color.FromArgb(128, 123, 0, 0);

		public Color EditModeNoneTextBoxBackgroundColor { get; set; } = Color.FromArgb(128, 45, 45, 45);
		public Color EditModeEditTextBoxBackgroundColor { get; set; } = Color.FromArgb(128, 123, 0, 0);
		public Color EditModeAddTextBoxBackgroundColor { get; set; } = Color.FromArgb(128, 0, 0, 123);
		public Color EditModeNoEditScoreTextBoxBackgroundColor { get; set; } = Color.FromArgb(128, 0, 123, 13);

		private EditMode _EditMode;
		public EditMode EditMode
		{
			get { return _EditMode; }
			set
			{
				_EditMode = value;
				var editBackground = new SolidColorBrush(EditModeNoneTextBoxBackgroundColor);
				var editScoreBackground = new SolidColorBrush(EditModeNoneTextBoxBackgroundColor);

				if (_EditMode == EditMode.Delete)
				{
					MatchActionButton.Visibility = Visibility.Visible;
					MatchActionButton.Content = DeleteModeButtonContent;
					MatchActionButton.Foreground = new SolidColorBrush(EditModeDeleteActionButtonColor);
				}
				else if (_EditMode == EditMode.Add)
				{
					MatchActionButton.Visibility = Visibility.Visible;
					MatchActionButton.Content = AddModeButtonContent;
					MatchActionButton.Foreground = new SolidColorBrush(EditModeAddActionButtonColor);
					editBackground = new SolidColorBrush(EditModeAddTextBoxBackgroundColor);
				}
				else if (_EditMode == EditMode.Edit)
				{
					MatchActionButton.Visibility = Visibility.Visible;
					MatchActionButton.Content = EditModeButtonContent;
					MatchActionButton.Foreground = new SolidColorBrush(EditModeEditActionButtonColor);
					editBackground = new SolidColorBrush(EditModeEditTextBoxBackgroundColor);
				}
				else
				{
					MatchActionButton.Visibility = Visibility.Collapsed;
					MatchActionButton.Content = "";
				}

				if(!_AllowScoreEdits)
				{
					editScoreBackground = new SolidColorBrush(EditModeNoEditScoreTextBoxBackgroundColor);
				}

				MatchDateTextBox.Background = editBackground;

				if (_ShowScores)
				{
					MatchHomeTeamScoreTextBox.Background = editScoreBackground;
					MatchAwayTeamScoreTextBox.Background = editScoreBackground;
				}
			}
		}


		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextChanged = true;
		}
	}
}
