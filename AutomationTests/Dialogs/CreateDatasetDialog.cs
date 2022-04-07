using Aquality.Selenium.Elements.Interfaces;
using AutomationTests.Dialogs.Base;
using OpenQA.Selenium;

namespace AutomationTests.Dialogs
{
    internal class CreateDatasetDialog : SpectrusWebInputDialog
    {
        public IButton BtnCreate { get; }

        public CreateDatasetDialog() : base("create-dataset")
        {
            BtnCreate = ElementFactory.GetButton(GetFormChildLocator(By.XPath("//button[normalize-space()='Create']")), "Create dataset");
        }
    }
}
