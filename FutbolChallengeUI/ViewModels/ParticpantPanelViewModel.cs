using FutbolChallenge.Data.Model;
using FutbolChallengeUI.Enums;
using FutbolChallengeUI.EventHandlers;
using FutbolChallengeUI.EventHandlers.EventArgs;

namespace FutbolChallengeUI.ViewModels
{
	public sealed partial class ParticipantPanelViewModel : BindableBase
	{
		static public event EditEntityEventHandler<Participant>? EditParticipant;
		static public event DeleteEntityEventHandler? DeleteParticipant;
		static public event AddEntityEventHandler<Participant>? AddParticipant;

		private Participant _Participant = new Participant();

		public Participant Participant
		{
			get => _Participant;
			set
			{
				_Participant = value;
				NotifyAllOnPropertyChanged();
			}
		}

		public ParticipantPanelViewModel()
		{

		}

		public void Clear()
		{
			Participant = new Participant();
		}

		public string EmailAddress
		{
			get => Participant?.EmailAddress ?? string.Empty;
			set { if (Participant != null) Participant.EmailAddress = value; }
		}

		public string FirstName
		{
			get => Participant?.FirstName ?? string.Empty;
			set { if (Participant != null) Participant.FirstName = value; }
		}

		public string LastName
		{
			get => Participant?.LastName ?? string.Empty;
			set { if (Participant != null) Participant.LastName = value; }
		}

		public string Id =>
			(Participant?.Id ?? 0).ToString();

		private bool _ShowEdit = true;
		public bool ShowEdit
		{
			get { return _ShowEdit; }
			set
			{
				_ShowEdit = value;
				OnPropertyChanged();
			}
		}

		private bool _EnableTextEditing = false;
		public bool EnableTextEditing
		{
			get { return _EnableTextEditing; }
			set
			{
				_EnableTextEditing = value;
				OnPropertyChanged();
			}
		}

		private EditMode _EditMode = EditMode.Delete;
		public EditMode EditMode
		{
			get { return _EditMode; }
			set
			{
				_EditMode = value;
				OnPropertyChanged();
			}
		}

		public void EditModeClick()
		{
			EditMode = EditMode == EditMode.Edit ? EditMode.Delete : EditMode.Edit;
			EnableTextEditing = !EnableTextEditing;
		}

		public void ParticipantActionClick()
		{
			if (_EditMode == EditMode.Edit)
			{
				EditParticipant?.Invoke(this, new EditEntityEventArgs<Participant>(Participant));
			}
			else if(_EditMode == EditMode.Add)
			{
				AddParticipant?.Invoke(this, new AddEntityEventArgs<Participant>(Participant));
			}
			else
			{
				DeleteParticipant?.Invoke(this, new DeleteEntityEventArgs(Participant.Id));
			}
		}

	}
}
