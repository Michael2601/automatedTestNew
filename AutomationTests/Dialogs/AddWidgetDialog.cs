using Aquality.Selenium.Elements.Interfaces;
using AutomationTests.CustomElements;
using AutomationTests.Dialogs.Base;
using FrameworkExtensions.Helpers;
using OpenQA.Selenium;

namespace AutomationTests.Dialogs
{
    internal class AddWidgetDialog : SpectrusWebDialog
    {
        public IButton BtnAdd { get; }
        public AddWidgetDialog() : base("widget-add", WidgetsPageMacroTaskCount)
        {
            BtnAdd = ElementFactory.GetButton(GetFormChildLocator(By.XPath("//button[normalize-space()='Add']")), "Add widget");
        }

        public MatRadioButton GetRadioButton(WidgetName widgetName) =>
            ElementFactory.GetCustomElement((locator, name, state) => new MatRadioButton(name), null,
                widgetName.ToString());

        public class WidgetName : Enum
        {
            private readonly string _checkboxName;
            private WidgetName(string checkboxName) => _checkboxName = checkboxName;
            public override string ToString() => _checkboxName;

            public static WidgetName Spectra = new WidgetName("Spectra");
            public static WidgetName Structure => new WidgetName("Structure");
            public static WidgetName SpectralTable = new WidgetName("Spectral Data Table");
            public static WidgetName PeaksTable = new WidgetName("Peaks Table");
        }
    }
}
