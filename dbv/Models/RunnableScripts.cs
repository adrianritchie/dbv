namespace dbv.Models;

internal class RunnableScripts
{
    public IReadOnlyList<Script> VersionScripts { get; init; }

    public IReadOnlyList<Script> RepeatableScripts { get; init; }

    public IReadOnlyList<Script> BaselineScripts { get; init; }
}
