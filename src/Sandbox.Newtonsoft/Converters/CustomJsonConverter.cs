namespace Sandbox.Newtonsoft.Converters
{
    using System;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Linq;

    /// <summary>
    /// Deals with custom deserialization with Newtonsoft.Json.
    /// Refer to <see href="https://stackoverflow.com/questions/40439290/custom-deserialization-using-json-net">Stack Overflow</see>.
    /// </summary>
    internal class CustomJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return string.Empty;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                return serializer.Deserialize(reader, objectType);
            }
            else
            {
                JObject obj = JObject.Load(reader);
                if (obj["$value"] != null)
                    return obj["$value"].ToString();
                else
                    return serializer.Deserialize(reader, objectType);
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
