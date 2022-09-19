using System.Data;

namespace dbv.DataAccess;

public interface IConnectionFactory
{
  /// <summary>
  /// Create a new connection
  /// </summary>
  /// <param name="connectionName"></param>
  /// <returns></returns>
  IDbConnection Open(string connectionName);

  /// <summary>
  /// Open a connection providing your own connection string
  /// </summary>
  /// <param name="connectionString"></param>
  /// <returns></returns>
  IDbConnection OpenFromConnectionString(string connectionString);
}
