namespace Sandbox.Selenium.Settings
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;

    internal static class ConfigurationSettings
    {
        private static readonly IConfigurationRoot Root = InitializeConfiguration();

        public static ExecutionSettings GetExecutionSettings()
        {
            return Root.GetSection("executionSettings").Get<ExecutionSettings>();
        }

        private static IConfigurationRoot InitializeConfiguration()
        {
            var filesInExecutionDir = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var settingsFile = filesInExecutionDir.FirstOrDefault(x => x.Contains("executionSettings") && x.EndsWith(".json"));
            var builder = new ConfigurationBuilder();

            if (settingsFile != null)
            {
                builder.AddJsonFile(settingsFile, optional: true, reloadOnChange: true);
            }

            return builder.Build();
        }
    }
}
