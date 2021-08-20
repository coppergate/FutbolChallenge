using Exceptions;
using System.Data;
using System.Data.SqlClient;
using System.Fabric;
using System.Threading.Tasks;


namespace Helpers.Core.ConnectionFactory
{
	public interface IDbConnectionFactory
	{
		Task<IDbConnection> CreateOpen();
	}


	public class ImplementedServiceConnectionFactoryBase : IDbConnectionFactory
	{
		private ConnectionStringHandler ConnectionStringHandler;

		public ImplementedServiceConnectionFactoryBase()
		{

		}

		protected void SetConnection(string connectionString)
		{
			ConnectionStringHandler = new ConnectionStringHandler(connectionString);
		}

		protected string DataSource => ConnectionStringHandler.Source;
		protected string Catalog => ConnectionStringHandler.Catalog;

		public async Task<IDbConnection> CreateOpen()
		{
			var result = new SqlConnection(ConnectionStringHandler.ConnectionString);

			if (result.State == ConnectionState.Closed)
			{
				await result.OpenAsync();
			}
			return result;
		}
	}

	public class ImplementedApplicationDbConnectionFactory : ImplementedServiceConnectionFactoryBase, IDbConnectionFactory
	{
		public ImplementedApplicationDbConnectionFactory(IHelperConfiguration configurationProvider)
		{
			configurationProvider.GetConnectionString("FutbolChallengeData");
		}
	}

	public class ImplementedServiceFabricDbConnectionFactory : ImplementedServiceConnectionFactoryBase, IDbConnectionFactory
	{
		public ImplementedServiceFabricDbConnectionFactory(ServiceFabricConfigurationProvider configurationProvider)
		{
			configurationProvider.Load();
			if (configurationProvider.TryGet("ConnectionStrings:FutbolChallengeData", out string connectionString))
			{
				SetConnection(connectionString);
			}
			else
			{
				throw new InvalidConfigurationException($"{nameof(ImplementedServiceFabricDbConnectionFactory)} expects a configuration property ('ConnectionStrings:FutbolChallengeData') which does not exist");
			}
		}
	}
}
