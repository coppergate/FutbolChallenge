using System.Data.SqlClient;

namespace Helpers.Core {
	public class ConnectionStringHandler {
		readonly SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
		readonly string InitialCatalog;

		public ConnectionStringHandler(string connectionString) {
			bldr.ConnectionString = connectionString;
			InitialCatalog = bldr.InitialCatalog;
		}

		public string Source => bldr.DataSource;

		public string Catalog => bldr.InitialCatalog;

		public string ConnectionString => bldr.ToString();

		public void SwitchCatalog(string catalog) {
			bldr.InitialCatalog = catalog;
		}

		public void RestoreCatalog() {
			bldr.InitialCatalog = InitialCatalog;
		}

	}
}

