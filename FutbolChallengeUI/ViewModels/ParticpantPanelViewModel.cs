using FutbolChallenge.Data.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.ComponentModel;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeUI.Controls
{
	public sealed partial class ParticipantPanelView : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		static public event EditPanelEventHandler<Participant> EditParticipant;
		static public event DeleteOrAddPanelEventHandler DeleteAddParticipant;

		private Participant _Participant = new Participant();

		public Participant Participant
		{
			get => _Participant;
			set
			{
				_Participant = value;
				OnPropertyChanged();
			}
		}

		public ParticipantPanelView()
		{
			this.InitializeComponent();
			this.LosingFocus += ParticipantPanelView_LosingFocus;
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


				if (_EditMode == EditMode.Update)
				{
					ContentDialogResult result = await this.EditInProgressDialog.ShowAsync();

					if (result == ContentDialogResult.Primary)
					{
						EditParticipant?.Invoke(this, new EditPanelEventArgs<Participant>(Participant));
					}
					Mode = "Delete";
					EnableTextEditing = "False";
				}
			}
		}

		public void Clear()
		{
			Participant = new Participant();
		}

		public string EmailAddress
		{
			get => Participant?.EmailAddress;
			set { if (Participant != null) Participant.EmailAddress = value; }
		}

		public string FirstName
		{
			get => Participant?.FirstName;
			set { if (Participant != null) Participant.FirstName = value; }
		}
		public string LastName
		{
			get => Participant?.LastName;
			set { if (Participant != null) Participant.LastName = value; }
		}

		public string Id =>
			(Participant?.Id ?? 0).ToString();

		private bool _ShowEdit;
		public string ShowEdit
		{
			get { return _ShowEdit.ToString(); }
			set
			{
				_ShowEdit = bool.Parse(value);
				EditParticipantButton.Visibility = _ShowEdit ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		private bool _EnableTextEditing;
		public string EnableTextEditing
		{
			get { return _EnableTextEditing.ToString(); }
			set
			{
				_EnableTextEditing = bool.Parse(value);
				ParticipantEmail.IsReadOnly = !_EnableTextEditing;
				ParticipantFName.IsReadOnly = !_EnableTextEditing;
				ParticipantLName.IsReadOnly = !_EnableTextEditing;
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
					ParticipantActionButton.Visibility = Visibility.Visible;
					ParticipantActionButton.Content = "X";
					ParticipantActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
				}
				else if (_EditMode == EditMode.Add)
				{
					ParticipantActionButton.Visibility = Visibility.Visible;
					ParticipantActionButton.Content = "+";
					ParticipantActionButton.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
					editBackground = new SolidColorBrush(Color.FromArgb(128, 0, 0, 123));
				}
				else if (_EditMode == EditMode.Update)
				{
					ParticipantActionButton.Visibility = Visibility.Visible;
					ParticipantActionButton.Content = "^";
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


		public void OnPropertyChanged()
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}

		private void EditParticipantButton_Click(object sender, RoutedEventArgs e)
		{
			Mode = "Update";
			EnableTextEditing = "True";
		}

		private void ParticipantActionButton_Click(object sender, RoutedEventArgs e)
		{
			if (_EditMode == EditMode.Update)
			{
				EditParticipant?.Invoke(this, new EditPanelEventArgs<Participant>(_Participant));
				Mode = "Delete";
				EnableTextEditing = "False";
			}
			else
			{
				int id = int.Parse(((Button)sender).Tag.ToString());
				DeleteAddParticipant?.Invoke(this, new DeleteOrAddPanelEventArgs(id));
			}
		}
	}
}
