using FutbolChallengeUI;
using Helpers.Core;
using Microsoft.UI.Xaml;
using Ninject;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FutbolChallengeApp
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public partial class App : Application
	{
		private readonly IKernel _StandardKernel;
		private Window m_window;

		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			//this.InitializeComponent();
			_StandardKernel = BootstrapHelper.LoadNinjectKernel();
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used such as when the application is launched to open a specific file.
		/// </summary>
		/// <param name="args">Details about the launch request and process.</param>
		protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
		{
			m_window = _StandardKernel.Get<MainWindow>();
			IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(m_window);
			m_window.Title = "Futbol Challenge (.NET 5 Desktop WinUI 3)";

			// The Window object doesn't have Width and Height properties in WInUI 3.
			// You can use the Win32 API SetWindowPos to set the Width and Height.
			UIHelpers.SetWindowSize(hwnd, 800, 600);

			m_window.Activate();
		}
	}
}
