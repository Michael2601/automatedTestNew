using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using FrameworkExtensions.Helpers;
using OpenQA.Selenium;

namespace AutomationTests.Helpers
{
    internal class BaseBattleForm : Form
    {
        protected new static IElementFactory ElementFactory = AqualityServices.Get<IElementFactory>();

        /// <summary>
        /// Script isAngularCompleted contains return condition, which wait for this tasks count
        /// On page with widgets (opened dataset) pending macrotasks count is different with other pages
        /// </summary>
        protected const int WidgetsPageMacroTaskCount = 4;

        public BaseBattleForm(By locator, string name) : base(locator, name)
        {
                 AqualityServices.ConditionalWait.WaitForTrue(() => true, AqualityServices.Get<ITimeoutConfiguration>().PageLoad);
        }

        protected By GetFormChildLocator(By childLocator) => ByParent.GetChild(Locator, childLocator);
    }
}
