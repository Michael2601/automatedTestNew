using System;

namespace FrameworkExtensions.Helpers
{
    public class Logger
    {
        private static readonly Lazy<Logger> LazyInstance = new Lazy<Logger>(() => new Logger());

        protected Aquality.Selenium.Core.Logging.Logger LoggerCore { get; } = Aquality.Selenium.Core.Logging.Logger.Instance;

        public static Logger Instance => LazyInstance.Value;

        public void Step(int number, string description)
        {
            Info($"Step {number} --- {description}");
        }

        public void Info(string message)
        {
            LoggerCore.Info(message);
        }

        public void Warn(string message)
        {
            LoggerCore.Warn(message);
        }
    }
}
