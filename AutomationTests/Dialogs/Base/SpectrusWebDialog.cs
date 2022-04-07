using Aquality.Selenium.Elements.Interfaces;
using AutomationTests.Helpers;
using OpenQA.Selenium;

namespace AutomationTests.Dialogs.Base
{
    internal class SpectrusWebDialog : BaseSpectrusForm
    {
        public IButton BtnCancel { get; }
        public ILabel LblTitle { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tagPart">Part of dialog tag name spectrus-web-{tagPart}-dialog, eg: spectrus-web-widget-add-dialog (tagPart="widget-add")</param>
        /// <param name="macroTasksPendingOnPage">Needed for angular waiting</param>
        public SpectrusWebDialog(string tagPart, int macroTasksPendingOnPage = 3) : base(By.TagName($"spectrus-web-{tagPart}-dialog"), $"{tagPart} dataset dialog", macroTasksPendingOnPage: macroTasksPendingOnPage)
        {
            BtnCancel = ElementFactory.GetButton(GetFormChildLocator(By.XPath("//button[normalize-space()='Cancel']")), $"Cancel {Name}");
            LblTitle = ElementFactory.GetLabel(GetFormChildLocator(By.ClassName("mat-dialog-title")), $"Title of {Name}");
        }
    }
}
