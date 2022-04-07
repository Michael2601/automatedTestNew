using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Text;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using FrameworkExtensions.Helpers;
using OpenQA.Selenium;

namespace FrameworkExtensions.Grid
{

    public class Grid<T> where T : GridHeadersEnum
    {
        private readonly IElementFactory _elementFactory;
        private readonly ILabel _gridElement;
        private readonly By _headersLocator;

        private List<GridHeader> GridHeaders => _elementFactory.FindElements<GridHeader>(_headersLocator).ToList();
        
        private readonly Logger _logger = Logger.Instance;

        private const string RowByValueXpathTemplate = "//tr[.//td[normalize-space()='{0}']]";
        private const string CellByValueXPathTemplate = "//td[normalize-space()='{0}']";
        private const string RowByIndexTemplate = "//tr[{0}]";
        private const string ColumnByIndexTemplate = "//td[{0}]";
        private static string CellByColumnValuePair(int column, string value) => $"({string.Format(ColumnByIndexTemplate, column)})[normalize-space()='{value}']";

        public Grid(By locator, string name, By headersLocator = null)
        {
            _elementFactory = AqualityServices.Get<IElementFactory>();
            _gridElement = _elementFactory.GetLabel(locator, $"Grid {name}", Aquality.Selenium.Core.Elements.ElementState.ExistsInAnyState);

            _headersLocator = GetGridChildSelector(headersLocator ?? By.TagName("th"));
        }

        public int RowsCount => GetColumn(1).Count;

        public int ColumnsCount => _elementFactory.FindElements<GridCell>(GetGridChildSelector(By.TagName("td"))).Count;

        private By GetGridChildSelector(By childSelector) => ByParent.GetChild(_gridElement.Locator, childSelector);

        private int GetColumnNumber(T headerMember)
        {
            if (headerMember.ColumnIndex != GridHeadersEnum.WrongColumnIndex) return headerMember.ColumnIndex;
            var arr = GridHeaders.Select(h => h.GetText());
            var headerIndex = GridHeaders.FindIndex(header => header.GetText().Trim().Equals(headerMember.ToString()));
            if(headerIndex == -1) throw new GridElementException($"Header {headerMember} doesn't exist in {_gridElement.Name}");
            return headerIndex + 1;
        }

        public GridCell GetCell(int row, int column)
        {
            _logger.Info($"Getting cell ({row};{column}) of {_gridElement.Name}");
            return _gridElement.FindChildElement<GridCell>(By.XPath("." + string.Format(RowByIndexTemplate, row) + string.Format(ColumnByIndexTemplate, column)));
        }

        public GridCell GetCell(int row, T headerMember)
        {
            var columnNumber = GetColumnNumber(headerMember);
            return GetCell(row, columnNumber);
        }

        public GridCell GetCellByValueExact(int row, string value)
        {
            _logger.Info($"Getting cell with value {value} in row {row} of {_gridElement.Name}");
            return GetRow(row).FindChildElement<GridCell>(By.XPath("." + string.Format(CellByValueXPathTemplate, value)));
        }

        public GridCell GetCellByValueExact(string value, int column)
        {
            _logger.Info($"Getting cell with value {value} in column {column} of {_gridElement.Name}");
            return _gridElement.FindChildElement<GridCell>(By.XPath("." + CellByColumnValuePair(column, value)));
        }

        public GridCell GetCellByValue(string value, int column)
        {
            _logger.Info($"Getting cell with value {value} in column {column} of {_gridElement.Name}");
            return _gridElement.FindChildElement<GridCell>(By.XPath($".({string.Format(ColumnByIndexTemplate, column)})[contains(normalize-space(), '{value}')]"));
        }


        public GridCell GetCellByValueExact(string value, T headerMember) =>
            GetCellByValueExact(value, GetColumnNumber(headerMember));

        public GridCell GetCellByValueExact(string value)
        {
            _logger.Info($"Getting cell with value {value} of {_gridElement.Name}");
            return _gridElement.FindChildElement<GridCell>(By.XPath(string.Format(CellByValueXPathTemplate, value)));
        }

        public List<GridCell> GetColumn(T headerMember) => GetColumn(GetColumnNumber(headerMember));

        public List<GridCell> GetColumn(int column) {
            _logger.Info($"Getting {column} column of {_gridElement.Name}");
            return _elementFactory.FindElements<GridCell>(GetGridChildSelector(By.XPath(string.Format(ColumnByIndexTemplate, column)))).ToList();
        }

        public GridRow GetRow(string value) {
            _logger.Info($"Getting row with value {value} of {_gridElement.Name}");
            return _gridElement.FindChildElement<GridRow>(By.XPath("." + string.Format(RowByValueXpathTemplate, value)));
        }

        public GridRow GetRow(int row) {
            _logger.Info($"Getting {row} row of {_gridElement.Name}");
            return _gridElement.FindChildElement<GridRow>(By.XPath("." + string.Format(RowByIndexTemplate, row)));
        }

        public List<GridRow> GetRowsByValueExact(string value) {
            _logger.Info($"Getting rows with value {value} of {_gridElement.Name}");
            return _elementFactory.FindElements<GridRow>(GetGridChildSelector(By.XPath(string.Format(RowByValueXpathTemplate, value)))).ToList();
        }

        private string GetRowsByValuesLocator(Dictionary<T, string> columnValuesDictionary)
        {
            var formattedDictionary = columnValuesDictionary.Select(pair =>
                KeyValuePair.Create(GetColumnNumber(pair.Key), pair.Value));
            var rowInnerString = string.Join(" and ", formattedDictionary.Select(pair => CellByColumnValuePair(pair.Key, pair.Value)));
            return $"//tr[{rowInnerString}]".ReplaceFirst("//td", ".//td");
        }

        public List<GridRow> GetRowsByValueExact(string value, T headerMember) {
            throw new System.Exception("Not implemented");
        }

        public List<GridRow> GetRowsByValuesExact(Dictionary<T, string> columnValuesDictionary)
        {
            var rowsSelector = GetRowsByValuesLocator(columnValuesDictionary);
            return _elementFactory.FindElements<GridRow>(GetGridChildSelector(By.XPath(rowsSelector))).ToList();
        }

        public List<GridRow> GetRowsByValueExact(string value, int column) {
            throw new System.Exception("Not implemented");
        }

        public GridCell GetCellByAnotherCell(T neededColumn, T knownColumn, string knownValue) => 
            GetCellByAnotherCell(GetColumnNumber(neededColumn), knownColumn, knownValue);

        public GridCell GetCellByAnotherCell(int neededColumn, T knownColumn, string knownValue)
        {
            _logger.Info($"Getting cell from column {neededColumn} with value {knownValue} in column {knownColumn} of {_gridElement.Name}");
            
            var rowSelector = GetRowsByValuesLocator(new Dictionary<T, string> {{knownColumn, knownValue}});
            return _gridElement.FindChildElement<GridCell>(By.XPath($".{rowSelector}//td[{neededColumn}]"));
        }

        public List<GridCell> GetCellsByAnotherCell(T neededColumn, T knownColumn, string knownValue)
        {
            throw new System.Exception("Not implemented");
        }

        private static List<string> GetRowText(string rowHtml) {
            var result = new List<string>();
            var cellMatches = new Regex("<td.*?>(.*?)</td>", RegexOptions.Singleline).Matches(rowHtml);
            
            foreach (Match cellMatch in cellMatches)
            {
                var cellHtml = cellMatch.Groups[1].Value;
                var cellText = "";
                var cellTextMatches = new Regex(">(.*?)<", RegexOptions.Singleline).Matches(cellHtml);
                foreach (Match cellTextMatch in cellTextMatches)
                {
                    cellText += cellTextMatch.Groups[1].Value;
                }
                result.Add(cellText);
            }

            return result;
        }

        public List<string> GetRowText(int row)
        {
            _logger.Info($"Getting {row} row text of {_gridElement.Name}");
            return GetRowText(GetRow(row).GetAttribute("innerHTML"));
        }

        public List<List<string>> GetGridText() {
            _logger.Info($"Getting text of {_gridElement.Name}");
            var result = new List<List<string>>();
            var gridHtml = _gridElement.GetAttribute("innerHTML");
            var rowMatches = new Regex("<tr.*?>(.*?)</tr>", RegexOptions.Singleline).Matches(gridHtml);
            foreach (Match rowMatch in rowMatches)
            {
                var rowText = GetRowText(rowMatch.Groups[1].Value);
                result.Add(rowText);
            }
            return result;
        }

        public GridHeader GetHeader(T headerMember) => GetHeader(GetColumnNumber(headerMember));

        public GridHeader GetHeader(int column)
        {
            _logger.Info($"Getting {column} header of {_gridElement.Name}");
            return GridHeaders[column - 1];
        }
    }
}