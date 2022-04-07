using System.Reflection;
using Aquality.Selenium.Core.Utilities;

namespace AutomationTests.Entities
{
    internal class SpectrusUser
    {
        public string Login { get; }
        public string Password { get; }

        private SpectrusUser(string loginPath, string passwordPath)
        {
            var loginsJson = new JsonSettingsFile("Resources.credentials.json", Assembly.GetCallingAssembly());
            Login = loginsJson.GetValue<string>(loginPath);
            Password = loginsJson.GetValue<string>(passwordPath);
        }


        public static SpectrusUser Test = new SpectrusUser(".test.login", ".test.password");
        public static SpectrusUser Test1 = new SpectrusUser(".test1.login", ".test1.password");
        public static SpectrusUser Test2 = new SpectrusUser(".test2.login", ".test2.password");
    }
}
