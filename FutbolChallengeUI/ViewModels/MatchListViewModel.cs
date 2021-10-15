using FutbolChallenge.Data.Model;
using FutbolChallengeDataRepository.Converters;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FutbolChallengeUI.ViewModels
{
	public class MatchListViewModel : BindableBase
	{
		public MatchListViewModel()
		{

		}

		private ObservableCollection<MatchPanelViewModel> _Matches;

		public IEnumerable<MatchPanelViewModel> Matches
		{
			get
			{
				if (MatchGroupSequence > 0)
					return _Matches.Where(m => m.SeasonId == SeasonId && m.MatchGroupSequence == MatchGroupSequence);
				return _Matches;
			}
			set
			{
				this._Matches = new ObservableCollection<MatchPanelViewModel>(value);
				_MatchGroupSequenceList = GameMatchGroupExtractor.ExtractMatchGroups(_Matches.Select(m => m.Game));
				this.OnPropertyChanged();
			}
		}

		private int _SeasonId = -1;
		public int SeasonId
		{
			get { return _SeasonId; }
			set { _SeasonId = value; NotifyAllOnPropertyChanged(); }
		}

		private int _MatchGroupSequence = -1;
		public int MatchGroupSequence
		{
			get { return _MatchGroupSequence; }
			set { _MatchGroupSequence = value; NotifyAllOnPropertyChanged(); }
		}

		public string SeasonName =>
			_Matches?.FirstOrDefault(m => m.SeasonId == SeasonId).SeasonName ?? string.Empty;

		public string MatchGroupName =>
			_Matches?.FirstOrDefault(m => m.SeasonId == SeasonId && m.MatchGroupSequence == MatchGroupSequence).MatchGroupTitle ?? string.Empty;


		int MatchGroupCount =>
			_Matches?.Select(m => m.MatchGroupSequence).Distinct().Count() ?? 0;


		IEnumerable<MatchGroup> _MatchGroupSequenceList;
		int _CurrentMatchGroupSequenceIndex = 0;

		public void MatchGroupButtonClick()
		{
			MatchGroupSequence = (MatchGroupSequence + 1) % MatchGroupCount;
		}
	}

}
