using System.Text.RegularExpressions;

namespace dbv.Models;

public enum ScriptType
{
    Unknown = 0,
    Version = 1,
    Repeatable = 2,
    Baseline = 3
}

public record Script
{
    public Script(Uri path)
    {
        Path = path;
        (Type, Version) = Parse(Path);
    }

    public Uri Path { get; init; }

    public ScriptType Type { get; init; }

    public Version? Version { get; init; }

    public string? Sql { get; init; }

    public bool IsValid => Type == ScriptType.Repeatable || (Type != ScriptType.Unknown && Version is not null);

    private (ScriptType, Version?) Parse(Uri path)
    {
        var regex = new Regex("([VRB])_([0-9.]+)?_?_.*\\.sql");

        var result = regex.Match(path.Segments.Last());

        if (!result.Success || result.Groups.Count < 3)
        {
            return (ScriptType.Unknown, null);
        }

        var type = result.Groups[1].Value switch
        {
            "V" => ScriptType.Version,
            "R" => ScriptType.Repeatable,
            "B" => ScriptType.Baseline,
            _ => ScriptType.Unknown
        };

        _ = Version.TryParse(result.Groups[2].ValueSpan, out var version);

        return (type, version);
    }
}
