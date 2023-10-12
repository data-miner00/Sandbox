namespace Sandbox.Newtonsoft
{
    using global::Newtonsoft;
    using global::Newtonsoft.Json;

    public class Serialization
    {
        public void SerializeExample()
        {
            JsonConvert.SerializeObject(new { Hello = "hallo" });
        }
    }
}
