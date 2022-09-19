using dbv.Models;
using Microsoft.Extensions.Options;

namespace dbv.Services.ScriptEnumerators;

internal class FileSystemScriptEnumerator : IScriptEnumerator
{
    private readonly Settings _settings;

    public FileSystemScriptEnumerator(IOptions<Settings> settings)
    {
        _settings = settings.Value;
    }

    public async IAsyncEnumerator<Script> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        foreach (var file in Directory.EnumerateFiles(_settings.ScriptsPath, "*.sql", new EnumerationOptions { RecurseSubdirectories = true }))
        {
            yield return new Script(new Uri(file));
        }
    }

    public async Task<Script> GetScriptContentAsync(Script script)
    {
        return script with
        {
            Sql = await File.ReadAllTextAsync(script.Path.OriginalString)
        };
    }
}
