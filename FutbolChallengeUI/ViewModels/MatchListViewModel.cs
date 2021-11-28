using FutbolChallenge.Data.Model;
using FutbolChallengeDataRepository.Converters;
using FutbolChallengeUI.EventHandlers.EventArgs;
using Helpers.Core.DateTimeProvider;
using Microsoft.UI.Xaml.Controls;
using System;
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

        private ObservableCollection<MatchPanelViewModel>? _Matches;

        public IEnumerable<MatchPanelViewModel> Matches
        {
            get
            {
                if (MatchGroupSequence > 0)
                    return _Matches.Where(m => m.SeasonId == SeasonId && m.MatchGroupSequence == MatchGroupSequence);
                return _Matches ?? Enumerable.Empty<MatchPanelViewModel>();
            }
            set
            {
                this._Matches = new ObservableCollection<MatchPanelViewModel>(value);
                _MatchGroupSequenceList = GameMatchGroupExtractor.ExtractMatchGroups(_Matches.Select(m => m.Game));
                _SeasonId = _Matches?.First().SeasonId ?? -1;
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

        internal void SliderValueSet(double newValue)
        {
            MatchGroupSequence = Convert.ToInt32(newValue);
        }

        public string SeasonName =>
            _Matches?.FirstOrDefault(m => m.SeasonId == SeasonId)?.SeasonName ?? string.Empty;

        public string MatchGroupName =>
            _Matches?.FirstOrDefault(m => m.SeasonId == SeasonId && m.MatchGroupSequence == MatchGroupSequence)?.MatchGroupTitle ?? string.Empty;


        public int MatchGroupCount =>
            _Matches?.Select(m => m.MatchGroupSequence).Distinct().Count() ?? 1;


        IEnumerable<MatchGroup>? _MatchGroupSequenceList;

        public void MatchGroupSelectionChange(object sender, SelectedMatchGroupChangedEventArgs args)
        {
            if (args.Direction == MatchGroupChangeDirection.DOWN
                && MatchGroupSequence > 1)
                MatchGroupSequence--;

            if (args.Direction == MatchGroupChangeDirection.UP
                && MatchGroupSequence < MatchGroupCount - 1)
                MatchGroupSequence++;

            OnPropertyChanged("MatchGroupName");
        }


        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = e.OriginalSource;
        }


        public void ItemChanged(object sender, ItemClickEventArgs e)
        {
            var s = e.ClickedItem;
        }

        public void SetCurrentMatchGroup(IDateTimeProvider dateTimeProvider)
        {
            MatchGroupSequence = _Matches.FirstOrDefault(m => m.MatchDate >= dateTimeProvider.CurrentUtcDateTime)?.MatchGroupSequence ?? 0;
        }
    }

}
