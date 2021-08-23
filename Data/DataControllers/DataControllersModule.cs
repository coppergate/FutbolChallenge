using FutbolChallenge.Data;
using Helpers.Core;
using Ninject.Modules;
using System.Collections.Generic;

namespace DataControllers
{
	public class DataControllersModule : NinjectModule
	{
		public override void Load()
		{
		}
	}
	public class DataControllersBootstrapper : INinjectBootstrapper
	{
		public IList<INinjectModule> GetModules()
		{
			return new List<INinjectModule>()
				{
					new DataRepositoryModule(),
					new DataControllersModule(),
				};
		}
	}
}
