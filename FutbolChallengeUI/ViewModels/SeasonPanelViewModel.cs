using FutbolChallenge.Data.Model;
using FutbolChallengeUI.Enums;
using FutbolChallengeUI.EventHandlers;
using FutbolChallengeUI.EventHandlers.EventArgs;
using System;

namespace FutbolChallengeUI.ViewModels
{
	public sealed partial class SeasonPanelViewModel : BindableBase
	{
		static public event EditEntityEventHandler<Season>? EditSeason;
		static public event DeleteEntityEventHandler? DeleteSeason;
		static public event AddEntityEventHandler<Season>? AddSeason;


		public SeasonPanelViewModel()
		{
		}

		private Season _Season = new();

		public Season Season 
		{ 
			get { return _Season; }
			set { _Season = value; OnPropertyChanged(); }
		}

		public string SeasonName
		{
			get => Season?.Name ?? string.Empty;
			set { Season.Name = value; }
		}

		public DateTime StartDate
		{
			get => Season.StartDate;
			set => Season.StartDate = value;
		}

		public DateTime EndDate
		{
			get => Season.EndDate;
			set => Season.EndDate = value;
		}

		public string Id =>
			(Season?.Id ?? 0).ToString();


		private bool _ShowEdit;
		public bool ShowEdit
		{
			get { return _ShowEdit; }
			set
			{
				_ShowEdit = value;
				OnPropertyChanged();
			}
		}

		private bool _EnableTextEditing;
		public bool EnableTextEditing
		{
			get { return _EnableTextEditing; }
			set
			{
				_EnableTextEditing = value;
				OnPropertyChanged();
			}
		}

		private EditMode _EditMode;
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
				EditSeason?.Invoke(this, new EditEntityEventArgs<Season>(Season));
			}
			else if (_EditMode == EditMode.Add)
			{
				AddSeason?.Invoke(this, new AddEntityEventArgs<Season>(Season));
			}
			else
			{
				DeleteSeason?.Invoke(this, new DeleteEntityEventArgs(Season.Id));
			}
		}

	}
}
