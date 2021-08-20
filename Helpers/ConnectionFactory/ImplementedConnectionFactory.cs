using System.Data;
using System.Threading.Tasks;
using Greenshades.Online.Data;
using Microsoft.Extensions.Configuration;


namespace Greenshades.Online.TemplateGeneration.Helpers.ConnectionFactory {
	public interface IImplementedConnectionFactory {
		/// <summary>
		/// Returns a brand new connection that is owned by the caller.
		/// This connection must be manually cleaned up, so don't forget a using()
		/// </summary>
		/// <returns></returns>
		Task<IDbConnection> Create();
	}


	public class ImplementedConnectionFactory : IImplementedConnectionFactory {
		public ImplementedConnectionFactory(IConnectionFactory connectionFactory, IConfiguration configuration) {
			ConnectionFactory = connectionFactory;
			Configuration = configuration;
		}

		private IConnectionFactory ConnectionFactory { get; }
		private IConfiguration Configuration { get; }

		public async Task<IDbConnection> Create() {
			var connection = await ConnectionFactory.CreateNew(Configuration["ConnectionStrings:Atlas"]);
			return connection;
		}
	}
}
