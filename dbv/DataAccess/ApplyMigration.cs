using Dapper;
using dbv.Models;
using Microsoft.Data.SqlClient;

namespace dbv.DataAccess;

internal class ApplyMigration : IApplyMigration
{
    private readonly IConnectionFactory _connectionFactory;

    public ApplyMigration(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<BatchResult> ExecuteAsync(string migrationSql)
    {
        var result = new BatchResult
        {
            Sql = migrationSql,
            StartedUtc = DateTime.UtcNow
        };

        using var connection = _connectionFactory.Open("Database");
        using var transaction = connection.BeginTransaction();

        try
        {
            var rows = await connection.ExecuteAsync(
                migrationSql,
                commandTimeout: 0,
                commandType: System.Data.CommandType.Text,
                transaction: transaction).ConfigureAwait(false);

            result.AffectedRows = rows;

            transaction.Commit();
        }
        catch (SqlException ex)
        {
            result.Errors = ex.Errors.OfType<SqlError>();
            transaction.Rollback();
        }
        finally
        {
            result.FinishedUtc = DateTime.UtcNow;
        }

        return result;
    }
}
