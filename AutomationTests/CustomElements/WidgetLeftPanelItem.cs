using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using Element = Aquality.Selenium.Elements.Element;

namespace AutomationTests.CustomElements
{
    internal class WidgetLeftPanelItem : Element
    {
        public WidgetLeftPanelItem(int index) : base(By.XPath($"//mat-list-item[.//*[contains(@class, 'widget')]][{index}]"), $"{index}th ", ElementState.Displayed)
        {
        }

        protected override string ElementType => "Left panel widget item";

        public string WidgetId => FindChildElement<ILabel>(By.XPath("//*[@data-test-id='widgetIdLabel']")).Text;
        public string WidgetName => FindChildElement<ILabel>(By.XPath("//*[@data-test-id='widgetNameLabel']")).Text;

        public IButton DeleteButton => FindChildElement<IButton>(By.XPath("//*[@data-test-id='removeWidgetButton']"));
    }
}
