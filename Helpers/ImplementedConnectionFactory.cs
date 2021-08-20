using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;


namespace Helpers.ConnectionFactory {
	public interface IImplementedConnectionFactory {
		
		Task<IDbConnection> CreateOpen();
	}


	public class ImplementedConnectionFactory : IImplementedConnectionFactory {


		private readonly IConnectionFactory ConnectionFactory;
		private readonly ConnectionStringHandler ConnectionStringHandler;

		public ImplementedConnectionFactory(IConnectionFactory connectionFactory, IConfiguration configuration) {
			ConnectionFactory = connectionFactory;
			ConnectionStringHandler = new ConnectionStringHandler(configuration["FutbolChallengeData"]);
		}

		public string DataSource => ConnectionStringHandler.Source;
		public string Catalog => ConnectionStringHandler.Catalog;


		public async Task<IDbConnection> CreateOpen() {
			var result = await ConnectionFactory.Create(ConnectionStringHandler.ConnectionString);
			if (result.State == ConnectionState.Closed) {
				result.Open();
			}
			return result;
		}

		public Task<IDbConnection> Create(string connectionString)
			=> ConnectionFactory.Create(connectionString);

		public Task<IDbConnection> CreateNew()
			=> ConnectionFactory.CreateNew(ConnectionStringHandler.ConnectionString);

		public Task<IDbConnection> CreateNew(string connectionString)
			=> ConnectionFactory.CreateNew(connectionString);

	}
}
