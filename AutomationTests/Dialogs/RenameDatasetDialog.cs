using Aquality.Selenium.Elements.Interfaces;
using AutomationTests.Dialogs.Base;
using OpenQA.Selenium;

namespace AutomationTests.Dialogs
{
    internal class RenameDatasetDialog : SpectrusWebInputDialog
    {
        public IButton BtnRename { get; }
        public RenameDatasetDialog() : base("rename-dataset")
        {
            BtnRename = ElementFactory.GetButton(GetFormChildLocator(By.XPath("//button[normalize-space()='Rename']")), "Rename dataset");
        }
    }
}
