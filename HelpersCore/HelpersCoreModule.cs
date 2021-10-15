using Helpers.Core.ConnectionFactory;
using Helpers.Core.DateTimeProvider;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;
using System.Collections.Generic;

namespace Helpers.Core
{
	public class HelpersCoreModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IDateTimeProvider>().To<DefaultDateTimeProvider>();
			Bind<ImplementedServiceFabricDbConnectionFactory>().To<ImplementedServiceFabricDbConnectionFactory>();
			Bind<ImplementedApplicationDbConnectionFactory>().To<ImplementedApplicationDbConnectionFactory>();
			Bind<IConfiguration>().To<HelperConfiguration>();
			Bind<IHelperConfiguration>().To<HelperConfiguration>();
		}
	}


	public class HelpersCoreBootstrapper : INinjectBootstrapper
	{
		public IList<INinjectModule> GetModules()
		{
			return new List<INinjectModule>()
				{
					new HelpersCoreModule(),
				};
		}
	}

}
