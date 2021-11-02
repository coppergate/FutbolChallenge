using FutbolChallenge.Data.Model;
using FutbolChallengeUI.Enums;
using FutbolChallengeUI.EventHandlers;
using FutbolChallengeUI.EventHandlers.EventArgs;
using Helpers.Core.DateTimeProvider;
using System;

namespace FutbolChallengeUI.ViewModels
{
	public sealed partial class MatchPanelViewModel : BindableBase
	{
		static public event EditEntityEventHandler<SeasonGame> EditMatch;
		static public event DeleteEntityEventHandler DeleteMatch;
		static public event AddEntityEventHandler<SeasonGame> AddMatch;

		public MatchPanelViewModel(IDateTimeProvider dateTimeProvider)
		{
			_DateTimeProvider = dateTimeProvider;
		}

		//	TODO:Setup an AdminMode override state.

		private bool _AdminMode = false;
		private SeasonGame _Game = new();

		public SeasonGame Game
		{
			get { return _Game; }
			set
			{
				_Game = value;
				EnableTextEditing = _AdminMode || _Game.MatchDate < DateTime.Now;
				OnPropertyChanged();
			}
		}

		public string HomeTeam
		{
			get => Game.HomeTeam;
			set { Game.HomeTeam = value; OnPropertyChanged(); }
		}

		public int HomeTeamId
		{
			get => Game.HomeTeamId;
			set { Game.HomeTeamId = value; OnPropertyChanged(); }
		}

		public string VistingTeam
		{
			get => Game.VistingTeam;
			set { Game.VistingTeam = value; OnPropertyChanged(); }
		}

		public int VistingTeamId
		{
			get => Game.VisitingTeamId;
			set { Game.VisitingTeamId = value; OnPropertyChanged(); }
		}

		public DateTime? MatchDate
		{
			get => Game.MatchDate;
			set
			{
				Game.MatchDate = value;
				EnableTextEditing = value < _DateTimeProvider.CurrentUtcDateTime;
				AllowScoreEdits = value < _DateTimeProvider.CurrentUtcDateTime;
				OnPropertyChanged();
			}
		}

		public int SeasonId =>
			Game.SeasonId;

		public string SeasonName =>
			Game.SeasonName;

		public int MatchId =>
			Game.MatchId;

		public int MatchGroupId =>
			Game.MatchGroupId;

		public int MatchGroupSequence =>
			Game.MatchGroupSequence;

		public string MatchGroupTitle =>
			Game.MatchGroupTitle ?? string.Empty;

		public DateTime? MatchGroupStartDate =>
			Game.MatchGroupStartDate;

		public DateTime? MatchGroupEndDate =>
			Game.MatchGroupEndDate;

		public int? HomeTeamScore
		{
			get => Game.HomeTeamScore;
			set { Game.HomeTeamScore = value; OnPropertyChanged(); }
		}

		public int? VistingTeamScore
		{
			get => Game.VisitingTeamScore;
			set { Game.VisitingTeamScore = value; OnPropertyChanged(); }
		}

		private bool _ShowEdit;
		public bool ShowEdit
		{
			get => _ShowEdit;
			set
			{
				_ShowEdit = value;
				OnPropertyChanged();
			}
		}

		private bool _EnableTextEditing;
		public bool EnableTextEditing
		{
			get => _EnableTextEditing;
			set
			{
				_EnableTextEditing = value;
				OnPropertyChanged();
			}
		}

		private EditMode _EditMode;
		public EditMode EditMode
		{
			get => _EditMode;
			set
			{
				_EditMode = value;
				OnPropertyChanged();
			}
		}

		private bool _ShowScores = false;
		public bool ShowScores
		{
			get => _ShowScores;
			set
			{
				_ShowScores = value;
				OnPropertyChanged();
			}
		}

		private bool _AllowScoreEdits = false;
		private readonly IDateTimeProvider dateProvider;
		private readonly IDateTimeProvider _DateTimeProvider;

		public bool AllowScoreEdits
		{
			get => _AllowScoreEdits;
			set
			{
				_AllowScoreEdits = value;
				OnPropertyChanged();
			}
		}

		public void EditModeClick()
		{
			var showScores = !(EditMode == EditMode.Edit);
			ShowScores = showScores;
			EditMode = showScores ? EditMode.Edit : EditMode.None;
			AllowScoreEdits = _DateTimeProvider.CurrentUtcDateTime < Game.MatchDate;
			EnableTextEditing = showScores;
		}

		public void MatchActionClick()
		{
			if (_EditMode == EditMode.Edit)
			{
				EditMatch?.Invoke(this, new EditEntityEventArgs<SeasonGame>(Game));
			}
			else if (_EditMode == EditMode.Add)
			{
				AddMatch?.Invoke(this, new AddEntityEventArgs<SeasonGame>(Game));
			}
			else
			{
				DeleteMatch?.Invoke(this, new DeleteEntityEventArgs(Game.MatchId));
			}
			EditMode = EditMode.None;
			EditModeClick();
		}


	}
}
