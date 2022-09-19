using dbv.Models;

namespace dbv.DataAccess;

internal interface IApplyMigration
{
    public Task<BatchResult> ExecuteAsync(string migrationSql);
}
