using FutbolChallenge.Data.Model;
using FutbolChallengeDataRepository.Composites;
using FutbolChallengeUI.Controls;
using FutbolChallengeUI.EventHandlers.EventArgs;
using FutbolChallengeUI.ServiceClient;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FutbolChallengeUI
{

    public sealed partial class SeasonScheduleManagement : Window, INotifyPropertyChanged
    {
        private readonly IFutbolChallengeScheduleServiceClient _ScheduleClient;
        private readonly IFutbolChallengeSeasonServiceClient _SeasonClient;
        private IntPtr m_hwnd;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SeasonScheduleManagement(IFutbolChallengeScheduleServiceClient scheduleClient, IFutbolChallengeSeasonServiceClient seasonClient)
        {
            this.InitializeComponent();
            _ScheduleClient = scheduleClient;
            _SeasonClient = seasonClient;
            base.Title = "Manage Schedules";

            SelectSeasonComboBox.SelectedSeasonChanged += SelectSeasonComboBox_SelectedSeasonChanged;
            SeasonPanelViewModel.EditSeason += SeasonPanelView_EditSeason;

            m_hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            UIHelpers.SetWindowSize(m_hwnd, 1000, 700);

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

        private MatchListViewModel _MatchListViewModel = new MatchListViewModel();
        public MatchListViewModel MatchListViewModel
        {
            get { return _MatchListViewModel; }
            set
            {
                _MatchListViewModel = value;
                this.OnPropertyChanged();
            }
        }

        private string _LoadingMessage = "";
        public string LoadingMessage
        {
            get { return _LoadingMessage; }
            set { _LoadingMessage = value; OnPropertyChanged(); }
        }

        private async void SeasonPanelView_EditSeason(object? sender, EditEntityEventArgs<Season> e)
        {
            bool ret = await _SeasonClient.UpdateSeason(e.EditTarget);
        }

        private async void SelectSeasonComboBox_SelectedSeasonChanged(object? sender, SelectedSeasonChangedEventArgs e)
        {
            var season = e.SelectedSeason;
            var seasonDetail = await _SeasonClient.FetchSeasonDetails(season.Id);
            _SeasonDetailViewModel.SeasonDetail = seasonDetail;

            SeasonDetailViewModel = _SeasonDetailViewModel;
        }

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
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

            var seasonId = SeasonListViewModel?.SelectedSeason?.Id;

            if (seasonId != null)
            {
                using FileStream strm = File.OpenRead(file);
                var schedule = await ScheduleFromCSV.Create(seasonId.Value, $@"UploadedSeason-{seasonId}", $"UploadedSeason-{seasonId} {{0}}", strm);
                await _ScheduleClient.UploadScheduledGames(seasonId.Value, schedule);
            }
            UploadFilePickPanel.Visibility = Visibility.Collapsed;

        }

        internal async Task LoadSeasons()
        {
            var seasons = await _SeasonClient.FetchAllSeasons();
            _SeasonListViewModel.Seasons = new ObservableCollection<SeasonPanelViewModel>(seasons.Select(s => new SeasonPanelViewModel() {
                Season = s,
                EditMode = FutbolChallengeUI.Enums.EditMode.None,
                EnableTextEditing = false,
                ShowEdit = false
            }));
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

        private void ShowSeasonSchedule_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

