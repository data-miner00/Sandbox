namespace Sandbox.Newtonsoft
{
    using global::Newtonsoft;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Linq;
    using Sandbox.Newtonsoft.Models;

    public static class Serialization
    {
        public static void SerializeExample()
        {
            JsonConvert.SerializeObject(new { Hello = "hallo" });

            var jsonString = "{\"name\":\"John\":,\"age\":30,\"city\":\"New York\"}";
            var person = JsonConvert.DeserializeObject<Person>(jsonString);
        }

        public static void DeserializeIntoJObject()
        {
            var jsonString = "{\"name\":\"John\":,\"age\":30,\"city\":\"New York\"}";

            var jobject = JObject.Parse(jsonString);

            var name = (string)jobject["name"];
            var age = (int)jobject["age"];
            var city = (string)jobject["city"];
        }

        public static void DeserializeIntoJArray()
        {
            var jsonString = "[{\"name\":\"John\":,\"age\":30,\"city\":\"New York\"}]";

            var jarray = JArray.Parse(jsonString);
            var query = jarray.Where(x => (int)x["age"] == 5).ToList();
        }
    }
}
