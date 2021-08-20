using FutbolChallenge.Data.Repository.Model;
using FutbolChallengeUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.ComponentModel;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeUI.Controls
{
	public sealed partial class SeasonComboView : BindableUserControlBase
	{
		static public event EditPanelEventHandler<Season> EditSeason;
		static public event DeleteOrAddPanelEventHandler DeleteOrAddSeason;

		private SeasonListViewModel _SeasonListViewModel = new SeasonListViewModel();

		public SeasonListViewModel SeasonListViewModel
		{
			get => _SeasonListViewModel;
			set
			{
				_SeasonListViewModel = value;
				NotifyAllOnPropertyChanged();
			}
		}

		public SeasonComboView()
		{
			this.InitializeComponent();
		}


		public System.Collections.Generic.IEnumerable<Season> Seasons => 
			SeasonListViewModel?.Seasons;

	}
}
