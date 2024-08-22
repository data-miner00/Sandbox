namespace Sandbox.Newtonsoft.UnitTests;

using Sandbox.Newtonsoft.Models;
using global::Newtonsoft.Json;
using FluentAssertions;

public class UnitTest1
{
    [Fact]
    public void CompareRawJsonWithStronglyTypedModel_ObjectEquivalent()
    {
        var rawJson = File.ReadAllTextAsync("rawJson.json").GetAwaiter().GetResult();

        var rawJsonDeserialized = JsonConvert.DeserializeObject(rawJson);

        var dotNetObject = new RawJsonModel
        {
            Property1 = 1,
            Property2 = "New Property",
            NestedProperty = new RawJsonModel.Nested
            {
                Nested1 = null,
                Nested2 = 53,
            },
            Arrays = [
                "String1",
                "String2"
            ],
        };

        var serialized = JsonConvert.SerializeObject(dotNetObject);

        var objectDeserialized = JsonConvert.DeserializeObject(serialized);

        rawJsonDeserialized.Should().BeEquivalentTo(objectDeserialized);
    }
}
