using Helpers.ConnectionFactory;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace Helpers.DataServiceProvider {


	public class ImplementedDataServiceProvider : DataServiceProvider {

		async override protected Task<IDbConnection> GetConnection() {
			var connection = await ConnectionFactory.CreateNew(Configuration["ConnectionStrings:ImplementedDB"]);
			return connection;
		}

		public ImplementedDataServiceProvider(IConnectionFactory connectionFactory, IConfiguration configuration) {
			ConnectionFactory = connectionFactory;
			Configuration = configuration;
		}

		protected IConnectionFactory ConnectionFactory { get; set; }
		protected IConfiguration Configuration { get; set; }

	}

}
