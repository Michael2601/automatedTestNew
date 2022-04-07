using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using Element = Aquality.Selenium.Elements.Element;

namespace AutomationTests.CustomElements
{
    internal class MatRadioButton : Element, IRadioButton
    {
        private const string WrapLocator = "//mat-radio-button[substring(normalize-space(), 3)='{0}']";
        private readonly ILabel _wrapElement;

        public MatRadioButton(string name) : base(By.XPath(string.Format(WrapLocator, name) + "//div[contains(@class, 'mat-radio-inner-circle')]"), name, ElementState.Displayed)
        {
            _wrapElement = AqualityServices.Get<IElementFactory>()
                .GetLabel(By.XPath(string.Format(WrapLocator, name)), $"Wrap of {name} {ElementType}");
        }

        protected sealed override string ElementType { get; } = "Mat radio button";
        public bool IsChecked => _wrapElement.GetAttribute("class").Contains("checked");
    }
}
