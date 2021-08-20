using System;
using System.Data;
using System.Threading.Tasks;

namespace Helpers.Core.ConnectionFactory {
	public interface IConnectionFactory : IDisposable {

		
		Task<IDbConnection> Create(string connectionString);
		Task<IDbConnection> CreateNew(string connectionString);
	}

	
}