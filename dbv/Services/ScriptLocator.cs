using dbv.Models;
using System.Collections.ObjectModel;

namespace dbv.Services;

internal class ScriptLocator
{
    private readonly IScriptEnumerator _fileEnumerator;

    public ScriptLocator(IScriptEnumerator fileEnumerator)
    {
        _fileEnumerator = fileEnumerator;
    }

    public async Task<RunnableScripts> GetScriptsAsync()
    {
        var versionScripts = new SortedList<Version, Script>();
        var baselineScripts = new SortedList<Version, Script>();
        var repeatableScripts = new List<Script>();

        await foreach (var script in _fileEnumerator)
        {
            if (!script.IsValid) { continue; }

            switch (script.Type)
            {
                case ScriptType.Version:
                    versionScripts.Add(script.Version!, script);
                    break;
                case ScriptType.Baseline:
                    baselineScripts.Add(script.Version!, script);
                    break;
                case ScriptType.Repeatable:
                    repeatableScripts.Add(script);
                    break;
            }
        }

        return new RunnableScripts
        {
            VersionScripts = new ReadOnlyCollection<Script>(versionScripts.Values),
            RepeatableScripts = new ReadOnlyCollection<Script>(repeatableScripts),
            BaselineScripts = new ReadOnlyCollection<Script>(baselineScripts.Values)
        };
    }
}
