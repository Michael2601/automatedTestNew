using System.Reflection;
using Aquality.Selenium.Core.Utilities;

namespace AutomationTests.Helpers
{
    internal class ProjectConfig
    {
        private readonly JsonSettingsFile _configFile = new JsonSettingsFile("Resources.projectConfig.json", Assembly.GetCallingAssembly());
        
        public string BaseUrl => _configFile.GetValue<string>(".testUrl");
    }
}
