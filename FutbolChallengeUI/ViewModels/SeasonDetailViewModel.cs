using FutbolChallenge.Data.Model;

namespace FutbolChallengeUI.ViewModels
{
	public class SeasonDetailViewModel : BindableBase
	{
		private SeasonDetail _SeasonDetail;
		public SeasonDetail SeasonDetail { get { return _SeasonDetail; } set { _SeasonDetail = value; OnPropertyChanged(); } }

		public string StartDate =>
			SeasonDetail?.StartDate.ToString("yyyy-MM-dd");

		public string EndDate =>
			SeasonDetail?.EndDate.ToString("yyyy-MM-dd");

		public string NextMatchDate =>
			SeasonDetail?.NextMatchDate?.ToString("yyyy-MM-dd") ?? string.Empty;


	}
}
