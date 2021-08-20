using FutbolChallenge.Data.Repository.Model;
using FutbolChallengeUI;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage.Pickers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeApp
{

	public sealed partial class SeasonScheduleManagement : Window, INotifyPropertyChanged
	{
		private readonly IFutbolChallengeServiceClient _ServiceClient;

		public event PropertyChangedEventHandler PropertyChanged;

		private SeasonListViewModel _SeasonListViewModel = new SeasonListViewModel();
		public SeasonListViewModel SeasonListViewModel
		{
			get { return _SeasonListViewModel; }
			set
			{
				_SeasonListViewModel = value;
				this.OnPropertyChanged();
			}
		}

		private string _LoadingMessage;
		public string LoadingMessage
		{
			get { return _LoadingMessage; }
			set { _LoadingMessage = value; OnPropertyChanged(); }
		}

		public SeasonScheduleManagement(IFutbolChallengeServiceClient serviceClient)
		{
			this.InitializeComponent();
			_ServiceClient = serviceClient;
			base.Title = "Manage Schedules";
		}


		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void UploadScheduleButton_Click(object sender, RoutedEventArgs e)
		{
			FileOpenPicker open = new();
			open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
			open.FileTypeFilter.Add("*");

		}

		internal async Task LoadSeasons()
		{
			var seasons = await _ServiceClient.FetchAllSeasons();
			var newSeasonList = new SeasonListViewModel();
			newSeasonList.Seasons = new ObservableCollection<Season>(seasons);
			SeasonListViewModel = newSeasonList;
		}
	}
}

