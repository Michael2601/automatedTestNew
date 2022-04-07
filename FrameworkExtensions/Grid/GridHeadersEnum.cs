using FrameworkExtensions.Helpers;

namespace FrameworkExtensions.Grid
{
    public class GridHeadersEnum : Enum
    {
        public const int WrongColumnIndex = -1;

        private readonly string _columnName;
        public int ColumnIndex { get; }

        protected GridHeadersEnum(string columnName, int columnIndex = WrongColumnIndex)
        {
            _columnName = columnName;
            ColumnIndex = columnIndex;
        }

        public override string ToString() => _columnName;
    }
}
