using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Configuration;
using System.Runtime.InteropServices;

namespace Greenshades.Online.TemplateGeneration.TemplateCodeGenerator {

	public class MessageFilter : IOleMessageFilter {
		//
		// Class containing the IOleMessageFilter
		// thread error-handling functions.

		// Start the filter.
		public static void Register() {
			IOleMessageFilter newFilter = new MessageFilter();
			IOleMessageFilter oldFilter = null;
			CoRegisterMessageFilter(newFilter, out oldFilter);
		}

		// Done with the filter, close it.
		public static void Revoke() {
			IOleMessageFilter oldFilter = null;
			CoRegisterMessageFilter(null, out oldFilter);
		}

		//
		// IOleMessageFilter functions.
		// Handle incoming thread requests.
		int IOleMessageFilter.HandleInComingCall(int dwCallType,
		  System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr
		  lpInterfaceInfo) {
			//Return the flag SERVERCALL_ISHANDLED.
			return 0;
		}

		// Thread call was rejected, so try again.
		int IOleMessageFilter.RetryRejectedCall(System.IntPtr
		  hTaskCallee, int dwTickCount, int dwRejectType) {
			if (dwRejectType == 2)
			// flag = SERVERCALL_RETRYLATER.
			{
				// Retry the thread call immediately if return >=0 & 
				// <100.
				return 99;
			}
			// Too busy; cancel call.
			return -1;
		}

		int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee,
		  int dwTickCount, int dwPendingType) {
			//Return the flag PENDINGMSG_WAITDEFPROCESS.
			return 2;
		}

		// Implement the IOleMessageFilter interface.
		[DllImport("Ole32.dll")]
		private static extern int
		  CoRegisterMessageFilter(IOleMessageFilter newFilter, out
		  IOleMessageFilter oldFilter);
	}

	[ComImport(), Guid("00000016-0000-0000-C000-000000000046"),
	InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	interface IOleMessageFilter {
		[PreserveSig]
		int HandleInComingCall(
			int dwCallType,
			IntPtr hTaskCaller,
			int dwTickCount,
			IntPtr lpInterfaceInfo);

		[PreserveSig]
		int RetryRejectedCall(
			IntPtr hTaskCallee,
			int dwTickCount,
			int dwRejectType);

		[PreserveSig]
		int MessagePending(
			IntPtr hTaskCallee,
			int dwTickCount,
			int dwPendingType);
	}


	class ConnectionStringProvider {

		string _providerName;
		string _connectionString;
		string _ConnectionStringName;
		readonly ITextTemplatingEngineHost _Host;

		public ConnectionStringProvider(string connectionStringName, ITextTemplatingEngineHost host) {
			_ConnectionStringName = connectionStringName;
			_Host = host;
		}

		public string ProviderName {
			get {
				InitConnectionString();
				return _providerName;
			}
		}
		public string ConnectionString {
			get {
				InitConnectionString();
				return _connectionString;
			}
		}

		public string ConfigPath { get; set; } = @""; //Looks in current project for web.config or app.config by default. This overrides to a relative path - useful for seperate class library projects.


		public EnvDTE.Project GetCurrentProject() {
			EnvDTE.DTE dte = (EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.15.0");

			if (dte == null) {
				throw new Exception("Unable to retrieve EnvDTE.DTE");
			}

			Array activeSolutionProjects = (Array)dte.ActiveSolutionProjects;
			if (activeSolutionProjects == null) {
				throw new Exception("DTE.ActiveSolutionProjects returned null");
			}

			EnvDTE.Project dteProject = (EnvDTE.Project)activeSolutionProjects.GetValue(0);
			if (dteProject == null) {
				throw new Exception("DTE.ActiveSolutionProjects[0] returned null");
			}

			return dteProject;
		}


		private void InitConnectionString() {
			if (String.IsNullOrEmpty(_connectionString)) {
				_connectionString = GetConnectionString(out _providerName);

				if (_connectionString.Contains("|DataDirectory|")) {
					//have to replace it
					string dataFilePath = GetDataDirectory();
					_connectionString = _connectionString.Replace("|DataDirectory|", dataFilePath);
				}
			}
		}

		private string GetProjectPath() {
			System.IO.FileInfo info = new System.IO.FileInfo(GetCurrentProject().FullName);
			return info.Directory.FullName;
		}

		private string GetDataDirectory() {
			EnvDTE.Project project = GetCurrentProject();
			MessageFilter.Register();

			string path = System.IO.Path.GetDirectoryName(project.FileName) + "\\App_Data\\";
			MessageFilter.Revoke();
			return path;
		}


		private string GetConfigPath() {
			if (ConfigPath != "") {
				return _Host.ResolvePath(ConfigPath);
			}

			foreach (EnvDTE.ProjectItem item in GetCurrentProject().ProjectItems) {
				// if it is the app.config file, then open it up
				if (item.Name.Equals("App.config", StringComparison.InvariantCultureIgnoreCase) || item.Name.Equals("Web.config", StringComparison.InvariantCultureIgnoreCase)) {
					return GetProjectPath() + "\\" + item.Name;
				}
			}
			MessageFilter.Revoke();
			return String.Empty;
		}

		private string GetConnectionString(out string providerName) {
			providerName = null;

			string result = "";
			ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
			configFile.ExeConfigFilename = GetConfigPath();

			if (string.IsNullOrEmpty(configFile.ExeConfigFilename)) {
				throw new ArgumentNullException("The project does not contain App.config or Web.config file.");
			}

			var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
			var connSection = config.ConnectionStrings;

			if (string.IsNullOrEmpty(_ConnectionStringName)) {
				if (connSection.ConnectionStrings.Count > 1) {
					_ConnectionStringName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].Name;
					result = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ConnectionString;
					providerName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ProviderName;
				}
			}
			else {
				try {
					result = connSection.ConnectionStrings[_ConnectionStringName].ConnectionString;
					providerName = connSection.ConnectionStrings[_ConnectionStringName].ProviderName;
				}
				catch {
					result = "There is no connection string name called '" + _ConnectionStringName + "'";
				}
			}

			return result;
		}
	}

}
