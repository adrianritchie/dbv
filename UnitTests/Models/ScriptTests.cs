using dbv.Models;
using Moq;
using Xunit;

namespace UnitTests.Models;

public class ScriptTests
{
    [Theory]
    [InlineData("c:\\test.txt", ScriptType.Unknown, null, false)]
    [InlineData("c:\\V_1__test.sql", ScriptType.Version, null, false)]
    [InlineData("c:\\A_1.0__test.sql", ScriptType.Unknown, null, false)]
    [InlineData("c:\\V_1.0__test.sql", ScriptType.Version, "1.0", true)]
    [InlineData("c:\\R_1.0.1__test.sql", ScriptType.Repeatable, "1.0.1", true)]
    [InlineData("c:\\B_2.0__test.sql", ScriptType.Baseline, "2.0", true)]
    public void can_create_instance(string p, ScriptType t, string? v, bool isValid)
    {
        var path = new Uri(p);
        _ = Version.TryParse(v, out var version);

        var sut = new Script(path);
        Assert.NotNull(sut);

        Assert.Equal(path, sut.Path);
        Assert.Equal(t, sut.Type);
        Assert.Equal(version, sut.Version);
        Assert.Equal(isValid, sut.IsValid);
    }

}
