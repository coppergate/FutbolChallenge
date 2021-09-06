using FutbolChallenge.Data.Model;
using FutbolChallengeUI;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;
using System.IO;
using FutbolChallengeDataRepository.Composites;
using FutbolChallengeUI.Controls;

namespace FutbolChallengeApp
{

	public sealed partial class SeasonScheduleManagement : Window, INotifyPropertyChanged
	{
		private readonly IFutbolChallengeServiceClient _ServiceClient;
		private IntPtr m_hwnd;

		public event PropertyChangedEventHandler PropertyChanged;

		public SeasonScheduleManagement(IFutbolChallengeServiceClient serviceClient)
		{
			this.InitializeComponent();
			_ServiceClient = serviceClient;
			base.Title = "Manage Schedules";

			SelectSeasonComboBox.SelectedSeasonChanged += SelectSeasonComboBox_SelectedSeasonChanged;
			SeasonPanelView.EditSeason += SeasonPanelView_EditSeason;

			m_hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
			UIHelpers.SetWindowSize(m_hwnd, 1500, 700);

		}

		private async void SeasonPanelView_EditSeason(object sender, EditPanelEventArgs<Season> e)
		{
			bool ret = await _ServiceClient.UpdateSeason(e.EditTarget);
		}



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


		private SeasonDetailViewModel _SeasonDetailViewModel = new SeasonDetailViewModel();
		public SeasonDetailViewModel SeasonDetailViewModel
		{
			get { return _SeasonDetailViewModel; }
			set
			{
				_SeasonDetailViewModel = value;
				this.OnPropertyChanged();
			}
		}


		private string _LoadingMessage;
		public string LoadingMessage
		{
			get { return _LoadingMessage; }
			set { _LoadingMessage = value; OnPropertyChanged(); }
		}


		private async void SelectSeasonComboBox_SelectedSeasonChanged(object sender, FutbolChallengeUI.Controls.SelectedSeasonChangedEventArgs e)
		{
			var season = e.SelectedSeason;
			var seasonDetail = await _ServiceClient.FetchSeasonDetails(season.Id);
			_SeasonDetailViewModel.SeasonDetail = seasonDetail;

			SeasonDetailViewModel = _SeasonDetailViewModel;
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private async void UploadScheduleButton_Click(object sender, RoutedEventArgs e)
		{
			var file = FileToUploadPathTextBox.Text;
			if (string.IsNullOrWhiteSpace(file))
			{
				//	Show message;
				return;
			}

			var seasonId = SeasonListViewModel.SelectedSeason.Id;

			using FileStream strm = File.OpenRead(file);

			var schedule = await ScheduleFromCSV.Create(seasonId, $"UploadedSeason-{seasonId}", "Premier League Week {0}", strm);

			await _ServiceClient.UploadScheduledGames(seasonId, schedule);
			UploadFilePickPanel.Visibility = Visibility.Collapsed;

		}

		internal async Task LoadSeasons()
		{
			var seasons = await _ServiceClient.FetchAllSeasons();
			_SeasonListViewModel.Seasons = new ObservableCollection<Season>(seasons);
			SeasonListViewModel = _SeasonListViewModel;
		}

		private async void PickFileButton_Click(object sender, RoutedEventArgs e)
		{
			var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
			filePicker.FileTypeFilter.Add("*");

			//Make folder Picker work in Win32
			WinRT.Interop.InitializeWithWindow.Initialize(filePicker, m_hwnd);

			var file = await filePicker.PickSingleFileAsync();
			FileToUploadPathTextBox.Text = file != null ? file.Path : string.Empty;
		}

		private void ScheduleUploadButton_Click(object sender, RoutedEventArgs e)
		{
			UploadFilePickPanel.Visibility = UploadFilePickPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}
	}
}

