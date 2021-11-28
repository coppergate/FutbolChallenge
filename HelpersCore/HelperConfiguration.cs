using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Configuration;
using System.Fabric;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Helpers.Core
{

	public interface IHelperConfiguration
	{
		string GetConnectionString(string key);
	}

	public class HelperConfiguration : IHelperConfiguration, IConfiguration
	{
		public string GetValue(string key)
		{
			return ConfigurationSettings.AppSettings[key];
		}

		public string this[string key]
		{
			get => ConfigurationManager.AppSettings[key];
			set => ConfigurationManager.AppSettings[key] = value;
		}

		public string GetConnectionString(string key)
		{
			return ConfigurationManager.ConnectionStrings[key].ConnectionString;
		}

		public IEnumerable<IConfigurationSection> GetChildren() =>
			throw new System.NotImplementedException();

		public IChangeToken GetReloadToken() =>
			throw new System.NotImplementedException();

		public IConfigurationSection GetSection(string key)
		{
			return ConfigurationManager.GetSection(key) as IConfigurationSection;
		}
	}

	public class ServiceFabricConfigurationProvider : ConfigurationProvider
	{
		private readonly ServiceContext serviceContext;

		public ServiceFabricConfigurationProvider(ServiceContext serviceContext)
		{
			this.serviceContext = serviceContext;
		}

		public override void Load()
		{
			var config = serviceContext.CodePackageActivationContext.GetConfigurationPackageObject("Config");
		
			foreach (var section in config.Settings.Sections)
			{
				foreach (var parameter in section.Parameters)
				{
					Data[$"{section.Name}{ConfigurationPath.KeyDelimiter}{parameter.Name}"] = parameter.Value;
				}
			}
		}
	}

	public class ServiceFabricConfigurationSource : IConfigurationSource
	{
		private readonly ServiceContext serviceContext;

		public ServiceFabricConfigurationSource(ServiceContext serviceContext)
		{
			this.serviceContext = serviceContext;
		}

		public IConfigurationProvider Build(IConfigurationBuilder builder)
		{
			return new ServiceFabricConfigurationProvider(serviceContext);
		}
	}

	public static class ServiceFabricConfigurationExtensions
	{
		public static IConfigurationBuilder AddServiceFabricConfiguration(this IConfigurationBuilder builder, ServiceContext serviceContext)
		{
			builder.Add(new ServiceFabricConfigurationSource(serviceContext));
			return builder;
		}
	}

}
