namespace Sandbox.Selenium.Settings
{
    internal class ExecutionSettings
    {
        public bool RunInCloud { get; set; }

        public string DefaultBrowser { get; set; }

        public string BrowserVersion { get; set; }

        public string PlatformName { get; set; }

        public List<Dictionary<string, string>> Arguments { get; set; }
    }
}
