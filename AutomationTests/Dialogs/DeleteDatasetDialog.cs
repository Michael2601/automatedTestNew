using Aquality.Selenium.Elements.Interfaces;
using AutomationTests.Dialogs.Base;
using OpenQA.Selenium;

namespace AutomationTests.Dialogs
{
    internal class DeleteDatasetDialog : SpectrusWebDialog
    {
        public IButton BtnDelete { get; }

        public DeleteDatasetDialog() : base("delete-dataset")
        {
            BtnDelete = ElementFactory.GetButton(GetFormChildLocator(By.XPath("//button[normalize-space()='Delete']")), "Delete dataset");

        }
    }
}
