using Aquality.Selenium.Elements.Interfaces;
using AutomationTests.CustomElements;
using AutomationTests.Dialogs.Base;
using OpenQA.Selenium;

namespace AutomationTests.Dialogs
{
    internal class UploadDialog : SpectrusWebDialog
    {
        public IButton BtnUpload { get; }
        public FileInput FIFolder { get; }
        public FileInput FIFile { get; }

        public UploadDialog() : base("upload-dataset", WidgetsPageMacroTaskCount)
        {
            FIFolder = ElementFactory.GetCustomElement((locator, name, state) => new FileInput(locator, name, state), GetFormChildLocator(By.XPath("(//input[@type='file'])[1]")), "Add folder to list");
            FIFile = ElementFactory.GetCustomElement((locator, name, state) => new FileInput(locator, name, state), GetFormChildLocator(By.XPath("(//input[@type='file'])[2]")), "Add folder to list");
            BtnUpload = ElementFactory.GetButton(GetFormChildLocator(By.XPath("//button[normalize-space()='Upload']")), "Upload file(-s)");
        }
    }
}
