using dbv.Services;
using Moq;
using Xunit;

namespace UnitTests.Services;

public class ScriptLocatorTests
{
    [Fact]
    public void can_create_instance()
    {
        var fileEnumerator = new Mock<IScriptEnumerator>();
        var sut = new ScriptLocator(fileEnumerator.Object);
        Assert.NotNull(sut);
    }

    [Fact]
    public async Task returns_empty_result_when_no_filesAsync()
    {
        var fileEnumerator = new Mock<IScriptEnumerator>();

        var sut = new ScriptLocator(fileEnumerator.Object);

        var result = await sut.GetScriptsAsync();

        Assert.NotNull(result);
    }
}
