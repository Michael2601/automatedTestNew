using Aquality.Selenium.Elements;
using OpenQA.Selenium;

namespace FrameworkExtensions.Grid
{
    public class GridRow : Element
    {
        public GridRow(By loc, string nameOf, Aquality.Selenium.Core.Elements.ElementState stateOf) : base(loc, nameOf, stateOf)
        {
        }

        protected override string ElementType => "Grid row";
    }
}
