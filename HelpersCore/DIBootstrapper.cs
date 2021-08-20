using Microsoft.ServiceFabric.Services.Runtime;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Reflection;

namespace Helpers.Core
{

	public interface IDIBootstrapper<TModuleType>
	{
		IList<TModuleType> GetModules();
	}

	public interface INinjectBootstrapper : IDIBootstrapper<INinjectModule>
	{
	}

	public static class BootstrapHelper
	{
		public static StandardKernel LoadNinjectKernel()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			return LoadNinjectKernel(assemblies);
		}

		public static StandardKernel LoadNinjectKernel(IEnumerable<Assembly> assemblies)
		{
			var standardKernel = new StandardKernel();
			List<INinjectModule> loadedModules = new List<INinjectModule>();
			foreach (var assembly in assemblies)
			{
				assembly
					.GetTypes()
					.Where(t =>
						   t.GetInterfaces()
							   .Any(i =>
									i.Name == typeof(INinjectBootstrapper).Name))
					.ToList()
					.ForEach(t => {

						var ninjectModuleBootstrapper = (INinjectBootstrapper)Activator.CreateInstance(t);
						foreach (var m in ninjectModuleBootstrapper.GetModules())
						{
							if (loadedModules.Any(l => l.GetType() == m.GetType()))
							{
								continue;
							}
							standardKernel.Load(m);
							loadedModules.Add(m);
						}
					});
			}
			return standardKernel;
		}

		public static TType TypeCreate<TType>(StandardKernel kernel)
		{
			return kernel.Get<TType>();
		}

		public static StatelessService CreateService<TServiceType>(StandardKernel kernel, StatelessServiceContext context) where TServiceType : StatelessService
		{
			var container = ContainerConfig.CreateContainer<TServiceType>(kernel, context);
			return (StatelessService)container.Get<TServiceType>();
		}

		public static StatefulServiceBase CreateService<TServiceType>(StandardKernel kernel, StatefulServiceContext context) where TServiceType : StatefulServiceBase
		{
			var container = ContainerConfig.CreateContainer<TServiceType>(kernel, context);
			return (StatefulServiceBase)container.Get<TServiceType>();
		}

		internal static class ContainerConfig
		{
			public static StandardKernel CreateContainer<TServiceType>(StandardKernel kernel, StatelessServiceContext context) where TServiceType : StatelessService
			{
				kernel.Rebind<StatelessServiceContext>().ToConstant(context);
				kernel.Rebind<ServiceContext>().ToConstant(context);
				kernel.Rebind<TServiceType>().To<TServiceType>();

				return kernel;
			}

			public static StandardKernel CreateContainer<TServiceType>(StandardKernel kernel, StatefulServiceContext context) where TServiceType : StatefulServiceBase
			{
				kernel.Rebind<StatefulServiceContext>().ToConstant(context);
				kernel.Rebind<ServiceContext>().ToConstant(context);
				kernel.Rebind<TServiceType>().To<TServiceType>();

				return kernel;
			}
		}
	}

}
