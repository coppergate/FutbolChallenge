using Helpers.Core.ConnectionFactory;
using System.Data;
using System.Fabric;
using System.Threading.Tasks;

namespace Helpers.Core.DataServiceProvider {


	public class ImplementedDataServiceProvider : DataServiceProvider {

		async override protected Task<IDbConnection> GetConnection() {
			var connection = await ConnectionFactory.CreateOpen();
			return connection;
		}

		public ImplementedDataServiceProvider(IDbConnectionFactory connectionFactory) {
			ConnectionFactory = connectionFactory;
		}

		protected IDbConnectionFactory ConnectionFactory { get; set; }

	}

}
