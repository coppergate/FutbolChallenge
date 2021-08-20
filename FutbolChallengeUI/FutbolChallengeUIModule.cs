using Helpers.Core;
using Ninject.Modules;
using System.Collections.Generic;

namespace FutbolChallengeUI
{
	public class FutbolChallengeUIModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IFutbolChallengeServiceClient>().To<FutbolChallengeServiceClient>();

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
