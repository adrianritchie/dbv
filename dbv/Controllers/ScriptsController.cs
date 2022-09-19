using dbv.Models;
using dbv.Services;
using Microsoft.AspNetCore.Mvc;

namespace dbv.Controllers;

[ApiController]
[Route("[controller]")]
public class ScriptsController : ControllerBase
{
    private readonly ILogger<ScriptsController> _logger;
    private readonly IScriptEnumerator _scriptEnumerator;

    public ScriptsController(ILogger<ScriptsController> logger, IScriptEnumerator scriptEnumerator)
    {
        _logger = logger;
        _scriptEnumerator = scriptEnumerator;
    }

    [HttpGet(Name = "GetScript")]
    public async IAsyncEnumerable<Script> Get()
    {
        await foreach (var script in _scriptEnumerator)
        {
            yield return script;
        }
    }
}
