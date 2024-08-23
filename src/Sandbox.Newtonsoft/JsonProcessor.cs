namespace Sandbox.Newtonsoft;

using global::Newtonsoft.Json.Linq;

public static class JsonProcessor
{
    public static void ProcessJToken(JToken token)
    {
        if (token is JObject obj)
        {
            // Check if the object has the specific pattern
            if (obj["$type"] != null && obj["$value"] != null)
            {
                // Replace the entire object with the value of $value
                var value = obj["$value"];

                ProcessJToken(value);

                obj.Replace(value);
            }
            else
            {
                obj["$type"]?.Parent?.Remove();

                // Recursively process each property of the object
                foreach (var property in obj.Properties().ToList())
                {
                    ProcessJToken(property.Value);
                }
            }
        }
        else if (token is JArray array)
        {
            // Recursively process each element of the array
            for (int i = 0; i < array.Count; i++)
            {
                ProcessJToken(array[i]);
            }
        }
    }

    public static void ProcessJTokenWithJsonPath(JToken token)
    {
        var listOfTypes = token.SelectTokens("..$type")
            .ToList();

        foreach (var type in listOfTypes)
        {
            type.Parent?.Remove();
        }

        var listOfValues = token.SelectTokens("..$value")
            .ToList();

        foreach (var value in listOfValues)
        {
            value.Parent?.Parent?.Replace(value);
        }
    }

    public static string ProcessJson(string json)
    {
        var jsonObject = JToken.Parse(json);
        ProcessJToken(jsonObject);
        return jsonObject.ToString();
    }
}
