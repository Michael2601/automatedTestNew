using Aquality.Selenium.Core.Elements;
using OpenQA.Selenium;
using Element = Aquality.Selenium.Elements.Element;

namespace AutomationTests.CustomElements
{
    internal class FileInput : Element
    {
        public FileInput(By locator, string name, ElementState state = ElementState.Displayed) : base(locator, name, state)
        {
        }

        protected override string ElementType { get; } = "File input";
    }
}
