namespace Sandbox.Newtonsoft.UnitTests;

using FluentAssertions;
using global::Newtonsoft.Json;
using global::Newtonsoft.Json.Linq;

public sealed class ProcessJTokenTests
{
    [Fact]
    public void ProcessJToken_GivenInput_ExpectedOutput()
    {
        var inputJson = File.ReadAllText("Data/Input.json");
        var outputJson = File.ReadAllText("Data/Output.json");

        var parsedInput = JToken.Parse(inputJson);

        JsonProcessor.ProcessJToken(parsedInput);

        var actual = parsedInput.ToObject<object>();
        var expected = JsonConvert.DeserializeObject(outputJson);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ProcessJTokenWithJsonPath_GivenInput_ExpectedOutput()
    {
        var inputJson = File.ReadAllText("Data/Input.json");
        var outputJson = File.ReadAllText("Data/Output.json");

        var parsedInput = JToken.Parse(inputJson);

        JsonProcessor.ProcessJTokenWithJsonPath(parsedInput);

        var actual = parsedInput.ToObject<object>();
        var expected = JsonConvert.DeserializeObject(outputJson);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ProcessJTokenWithJsonPathInPlace_GivenInput_ExpectedOutput()
    {
        var inputJson = File.ReadAllText("Data/Input.json");
        var outputJson = File.ReadAllText("Data/Output.json");

        var parsedInput = JToken.Parse(inputJson);

        JsonProcessor.ProcessJTokenWithJsonPathInplace(parsedInput);

        var actual = parsedInput.ToObject<object>();
        var expected = JsonConvert.DeserializeObject(outputJson);

        actual.Should().BeEquivalentTo(expected);
    }
}
