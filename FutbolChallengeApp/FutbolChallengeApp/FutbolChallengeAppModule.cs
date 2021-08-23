using FutbolChallenge.Data;
using FutbolChallengeUI;
using Helpers.Core;
using Helpers.Core.ConnectionFactory;
using Ninject.Modules;
using System.Collections.Generic;

namespace FutbolChallengeApp
{
	public class FutbolChallengeAppModule : NinjectModule
	{
		public override void Load()
		{
			Rebind<IDbConnectionFactory>().To<ImplementedApplicationDbConnectionFactory>();
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
					new DataRepositoryModule(),
					new FutbolChallengeAppModule(),
				};
		}
	}

}
