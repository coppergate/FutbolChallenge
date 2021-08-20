using System;
using System.Data;
using System.Threading.Tasks;

namespace Helpers.ConnectionFactory {
	public interface IConnectionFactory : IDisposable {

		
		Task<IDbConnection> Create(string connectionString);
		Task<IDbConnection> CreateNew(string connectionString);
		Task<IDbConnection> CreateNew(string connectionString, bool? useMicrosoftSqlConnection);
	}

	
}