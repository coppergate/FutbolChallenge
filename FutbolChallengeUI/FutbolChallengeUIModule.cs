using FutbolChallengeUI.ViewModels;
using Helpers.Core;
using Ninject.Modules;
using System.Collections.Generic;

namespace FutbolChallengeUI
{
	public class FutbolChallengeUIModule : NinjectModule
	{
		public override void Load()
		{
			Bind<MainWindow>().ToSelf();
			Bind<ParticipantManagement>().ToSelf();
			Bind<SeasonScheduleManagement>().ToSelf();
			Bind<GameManagement>().ToSelf();

			Bind<MainWindowViewModel>().ToSelf();

		}
	}


	public class HelpersCoreBootstrapper : INinjectBootstrapper
	{
		public IList<INinjectModule> GetModules()
		{
			return new List<INinjectModule>()
				{
					new HelpersCoreModule(),
					new FutbolChallengeUIModule(),
				};
		}
	}

}
