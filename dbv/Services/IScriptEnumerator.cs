using dbv.Models;

namespace dbv.Services;

public interface IScriptEnumerator : IAsyncEnumerable<Script>
{
    public Task<Script> GetScriptContentAsync(Script script);
}

