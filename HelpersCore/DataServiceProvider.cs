using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Helpers.Core.DataServiceProvider
{

	abstract public class DataServiceProvider
	{
		async public Task<bool> CheckSqlConnection()
		{
			using (IDbConnection conn = await GetConnection())
			{
				conn.Execute("SELECT 1 AS FIELD1");
			}
			return true;
		}

		abstract protected Task<IDbConnection> GetConnection();

		#region Fetch Helpers

		async public Task<T> FetchEntity<T>(object key)
		{
			using (IDbConnection conn = await GetConnection()) { return await SimpleCRUD.GetAsync<T>(conn, key); }
		}

		async public Task<T> FetchValueWithSql<T>(string sql, object parameters)
		{
			using (IDbConnection conn = await GetConnection()) { return await conn.QueryFirstOrDefaultAsync<T>(sql, parameters); }
		}

		//  NB: the SimpleCRUD GetList requires either a property with the [Key] attribute in the type T
		//      or the type T must have a field named 'Id'
		async public Task<List<T>> FetchListEntity<T>(dynamic selector)
		{
			using (IDbConnection conn = await GetConnection()) { return new List<T>(await SimpleCRUD.GetListAsync<T>(conn, selector)); }
		}

		async public Task<List<T>> FetchListEntity<T>(string whereClause, object parameters)
		{
			using (IDbConnection conn = await GetConnection())
			{
				return new List<T>(await SimpleCRUD.GetListAsync<T>(conn, whereClause, parameters));
			}
		}

		async public Task<List<T>> FetchListEntity<T>(int maxReturnRowCount, string whereClause, object parameters)
		{
			using (IDbConnection conn = await GetConnection())
			{
				conn.Open();
				string setRowCount = $"set ROWCOUNT {maxReturnRowCount};";
				conn.Execute(setRowCount);
				List<T> retVal = new List<T>(await SimpleCRUD.GetListAsync<T>(conn, whereClause, parameters));
				setRowCount = $"set ROWCOUNT 0;";
				conn.Execute(setRowCount);
				return retVal;
			}
		}

		async public Task<List<T>> FetchListEntityWithSql<T>(string sql, object parameters)
		{
			using (IDbConnection conn = await GetConnection())
			{
				return new List<T>(await conn.QueryAsync<T>(sql, parameters));
			}
		}

		async public Task<List<T>> FetchListEntityWithSql<T>(int maxReturnRowCount, string sql, object parameters)
		{
			using (IDbConnection conn = await GetConnection())
			{
				conn.Open();
				string setRowCount = $"set ROWCOUNT {maxReturnRowCount};";
				conn.Execute(setRowCount);
				List<T> retVal = new List<T>(await conn.QueryAsync<T>(sql, parameters));
				setRowCount = $"set ROWCOUNT 0;";
				conn.Execute(setRowCount);
				return retVal;
			}
		}

		async public Task<List<T>> FetchListEntity<T>()
		{
			using (IDbConnection conn = await GetConnection()) { return new List<T>(await SimpleCRUD.GetListAsync<T>(conn)); }
		}

		async public Task<List<Dictionary<string, object>>> FetchListEntity(string commandString, Dictionary<string, object> parameters)
		{
			List<Dictionary<string, object>> rowOut = new List<Dictionary<string, object>>();
			using (IDbConnection conn = await GetConnection())
			{
				conn.Open();
				using (IDbCommand cmd = conn.CreateCommand())
				{
					foreach (KeyValuePair<string, object> entry in parameters)
					{
						IDbDataParameter param = cmd.CreateParameter();
						param.ParameterName = entry.Key;
						param.Value = entry.Value;
						cmd.Parameters.Add(param);
					}
					using (IDataReader rows = cmd.ExecuteReader())
					{
						while (rows.Read())
						{
							Dictionary<string, object> columns = new Dictionary<string, object>();
							for (int col = 0; col < rows.FieldCount; ++col)
							{
								columns.Add(rows.GetName(col), rows[col]);
							}
							rowOut.Add(columns);
						}
					}
				}
			}
			return rowOut;
		}

		async public Task<List<Dictionary<string, object>>> FetchListEntity(string commandString, object parameters)
		{
			List<Dictionary<string, object>> rowOut = new List<Dictionary<string, object>>();
			using (IDbConnection conn = await GetConnection())
			{
				conn.Open();
				using (IDbCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = commandString;
					using (IDataReader rows = await conn.ExecuteReaderAsync(commandString, parameters))
					{
						while (rows.Read())
						{
							Dictionary<string, object> columns = new Dictionary<string, object>();
							for (int col = 0; col < rows.FieldCount; ++col)
							{
								string name = rows.GetName(col);
								if (!columns.ContainsKey(name))
								{
									columns.Add(name, rows[col]);
								}
							}
							rowOut.Add(columns);
						}
					}
				}
			}
			return rowOut;
		}

		#endregion

		#region Insert Helpers

		async public Task<int?> InsertEntity<T>(T entity)
		{
			using (IDbConnection conn = await GetConnection())
			{
				var key = await Dapper.SimpleCRUD.InsertAsync<T>(conn, entity);
				return key;
			}
		}

		async public Task<T> InsertEntity<T>(string insertSql, object param)
		{
			using (IDbConnection conn = await GetConnection())
			{
				var key = await conn.ExecuteScalarAsync<T>(insertSql, param);
				return key;
			}
		}

		#endregion

		#region Update Helpers
		async public Task<int> UpdateEntity<T>(T entity)
		{
			using (IDbConnection conn = await GetConnection()) { return await SimpleCRUD.UpdateAsync(conn, entity); }
		}

		async public Task<int> UpdateEntity<T>(IDbConnection conn, SqlTransaction trans, T entity)
		{
			return await SimpleCRUD.UpdateAsync(conn, entity, trans);
		}

		#endregion

		#region Delete Helpers
		async public Task<int> DeleteEntity<T>(T entity)
		{
			using (IDbConnection conn = await GetConnection()) { return await SimpleCRUD.DeleteAsync(conn, entity); }
		}

		async public Task<int> DeleteEntity(string deleteSql, object param)
		{
			using (IDbConnection conn = await GetConnection())
			{
				return await conn.ExecuteAsync(deleteSql, param);
			}
		}

		#endregion

		#region Raw SQL

		async public Task<T> ExecuteSql<T>(string sql, object param)
		{
			using (IDbConnection conn = await GetConnection())
			{
				return await conn.ExecuteScalarAsync<T>(sql, param);
			}
		}

		async public Task<T> ExecuteSql<T>(string sql, object param, IDbConnection conn)
		{
			return await conn.ExecuteScalarAsync<T>(sql, param);
		}

		async public Task<T> ExecuteSql<T>(string sql, object param, IDbConnection conn, IDbTransaction tran)
		{
			return await conn.ExecuteScalarAsync<T>(sql, param, tran);
		}

		async public Task<int> ExecuteSql(string sql, object param, IDbConnection conn)
		{
			return await conn.ExecuteAsync(sql, param);
		}

		async public Task<int> ExecuteSql(string sql, object param, IDbConnection conn, IDbTransaction tran)
		{
			return await conn.ExecuteAsync(sql, param, tran);
		}

		async public Task<int> ExecuteSql(string sql, object param)
		{
			using (IDbConnection conn = await GetConnection())
			{
				return await conn.ExecuteAsync(sql, param);
			}
		}

		#endregion

	}
}
