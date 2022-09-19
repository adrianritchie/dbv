using Microsoft.Data.SqlClient;

namespace dbv.Models;

internal class BatchResult
{
    public string Sql { get; init; } = string.Empty;
    public DateTime StartedUtc { get; init; }
    public DateTime FinishedUtc { get; set; }
    public int AffectedRows { get; set; }
    public IEnumerable<SqlError>? Errors { get; set; }
}
