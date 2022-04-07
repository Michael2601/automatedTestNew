using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace AutomationTests.Dialogs.Base
{
    internal class SpectrusWebInputDialog : SpectrusWebDialog
    {
        public ILabel InputWrap { get; }
        public ITextBox TbxDatasetName { get; }
        public const string InputTipMessage = "Dataset name *";

        public const string ErrorCaretColor = "#f44336";

        public SpectrusWebInputDialog(string tagPart) : base(tagPart)
        {
            InputWrap = ElementFactory.GetLabel(GetFormChildLocator(By.TagName("mat-form-field")), "Dataset name input wrap");
            TbxDatasetName = ElementFactory.GetTextBox(GetFormChildLocator(By.Name("datasetName")), "Dataset name");
        }
    }
}
