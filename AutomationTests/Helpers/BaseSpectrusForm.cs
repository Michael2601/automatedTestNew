using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using FrameworkExtensions.Helpers;
using OpenQA.Selenium;

namespace AutomationTests.Helpers
{
    internal class BaseSpectrusForm : Form
    {
        protected new static IElementFactory ElementFactory = AqualityServices.Get<IElementFactory>();

        /// <summary>
        /// Script isAngularCompleted contains return condition, which wait for this tasks count
        /// </summary>
        private const int BaseFormMacroTaskCount = 3;

        /// <summary>
        /// Script isAngularCompleted contains return condition, which wait for this tasks count
        /// On page with widgets (opened dataset) pending macrotasks count is different with other pages
        /// </summary>
        protected const int WidgetsPageMacroTaskCount = 4;

        public BaseSpectrusForm(By locator, string name, bool waitForAngular = true, int macroTasksPendingOnPage = BaseFormMacroTaskCount) : base(locator, name)
        {
            if (waitForAngular)
                AqualityServices.ConditionalWait.WaitForTrue(() => AqualityServices.Browser.ExecuteScriptFromFile<bool>("Resources.isAngularCompleted.js", macroTasksPendingOnPage),
                    AqualityServices.Get<ITimeoutConfiguration>().PageLoad);

        }

        public BaseSpectrusForm(By locator, string name, int macroTasksPendingOnPage): this(locator, name, true, macroTasksPendingOnPage)
        {
        }


        protected By GetFormChildLocator(By childLocator) => ByParent.GetChild(Locator, childLocator);
    }
}
