using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FutbolChallengeUI.ViewModels
{
	public class BindableBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void NotifyAllOnPropertyChanged()
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}

	public class BindableUserControlBase : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public void NotifyAllOnPropertyChanged()
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}
}
