using FutbolChallenge.Data.Model;
using System;

namespace FutbolChallengeUI.ViewModels
{
    public class SeasonDetailViewModel : BindableBase
    {
        private SeasonDetail? _SeasonDetail;
        public SeasonDetail SeasonDetail
        {
            get { return _SeasonDetail ?? throw new InvalidOperationException("View model has no detail"); }
            set { _SeasonDetail = value; OnPropertyChanged(); }
        }

        public DateTime? StartDate =>
            SeasonDetail?.StartDate;

        public DateTime? EndDate =>
            SeasonDetail?.EndDate;

        public DateTime? NextMatchDate =>
            SeasonDetail?.NextMatchDate;


    }
}
