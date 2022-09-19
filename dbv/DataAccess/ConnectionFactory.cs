using Microsoft.Data.SqlClient;
using System.Data;

namespace dbv.DataAccess;

/// <summary>
/// Default connection factory implementation of <see cref="IConnectionFactory"/>
/// </summary>
public class ConnectionFactory : IConnectionFactory
{
  private readonly IConfiguration _configuration;

  /// <summary>
  /// Default constructor
  /// </summary>
  /// <param name="configuration"></param>
  public ConnectionFactory(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  ///< <inheritdoc/>
  public IDbConnection Open(string connectionName)
  {
    return OpenFromConnectionString(_configuration.GetConnectionString(connectionName));
  }

  ///< <inheritdoc/>
  public IDbConnection OpenFromConnectionString(string connectionString)
  {
    return new SqlConnection(connectionString);
  }
}
