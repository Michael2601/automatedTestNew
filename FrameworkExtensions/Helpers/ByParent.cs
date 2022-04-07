using System;
using System.Text.RegularExpressions;
using AngleSharp.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace FrameworkExtensions.Helpers
{
    public class ByParent
    {
        private class LocatorProperties
        {
            public LocatorProperties(string method, string value)
            {
                Method = method;
                Value = value;
            }

            public string Method { get; }

            public string Value { get; }
        }
        private static LocatorProperties GetLocatorAttributes(By locator)
        {
            var parentMatch = new Regex("By.(?<method>\\w+)(\\[.*?\\])?: (?<value>.*)").Match(locator.ToString());
            return new LocatorProperties(parentMatch.Groups["method"].Value, parentMatch.Groups["value"].Value);
        }

        private static string ConvertToCss(string attribute, string value, bool isContains = false) => $"[{attribute}{(isContains ? "*" : "")}='{value}']";
        private static string ConvertToXpath(string attribute, string value, bool isContains = false) => isContains ? $"//*[contains(@{attribute}, '{value}')]"  : $"//*[@{attribute}='{value}']";


        private const string XpathMethodName = "XPath";
        private const string CssMethodName = "CssSelector";

        private static LocatorProperties GetByCss(LocatorProperties locatorProperties)
        {
            return locatorProperties.Method switch
            {
                "Id" => new LocatorProperties(CssMethodName, ConvertToCss("id", locatorProperties.Value)),
                "Name" => new LocatorProperties(CssMethodName, ConvertToCss("name", locatorProperties.Value)),
                "ClassName" => new LocatorProperties(CssMethodName, ConvertToCss("class", locatorProperties.Value, true)),
                "TagName" => new LocatorProperties(CssMethodName, locatorProperties.Value),
                CssMethodName => locatorProperties,
                _ => throw new Exception($"{locatorProperties.Method} method not implemented for search child")
            };
        }

        private static LocatorProperties GetByXPath(LocatorProperties locatorProperties)
        {
            return locatorProperties.Method switch
            {
                "Id" => new LocatorProperties(CssMethodName, ConvertToXpath("id", locatorProperties.Value)),
                "Name" => new LocatorProperties(CssMethodName, ConvertToXpath("name", locatorProperties.Value)),
                "ClassName" => new LocatorProperties(CssMethodName, ConvertToXpath("class", locatorProperties.Value, true)),
                "TagName" => new LocatorProperties(CssMethodName, $"//{locatorProperties.Value}"),
                XpathMethodName => locatorProperties,
                _ => throw new Exception($"{locatorProperties.Method} method not implemented for search child")
            };
        }

        public static By GetChildByCssParent(By parentLocator, By childLocator)
        {
            var parentProperties = GetLocatorAttributes(parentLocator);
            var childProperties = GetByCss(GetLocatorAttributes(childLocator));
            
            return By.CssSelector(parentProperties.Value + " " + childProperties.Value);
        }

        public static By GetChild(By parentLocator, By childLocator)
        {
            var childProperties = GetLocatorAttributes(childLocator);
            var parentProperties = GetLocatorAttributes(parentLocator);
            if (childProperties.Method == XpathMethodName && childProperties.Value.StartsWith("("))
                return By.XPath("(" + GetByXPath(parentProperties).Value + childProperties.Value.Remove(0, 1));
            return childProperties.Method.Equals(CssMethodName) || parentProperties.Method.Equals(CssMethodName)
                ? By.CssSelector(GetByCss(parentProperties).Value + " " + GetByCss(childProperties).Value)
                : By.XPath(GetByXPath(parentProperties).Value + GetByXPath(childProperties).Value);
        }
    }
}
