using Enum = FrameworkExtensions.Helpers.Enum;

namespace AutomationTests.Enums
{
    internal class ManageDatasetDialogInfo : Enum
    {
        private readonly string _name;

        private ManageDatasetDialogInfo(string name)
        {
            _name = name;
        }

        public override string ToString() => _name;

        public static ManageDatasetDialogInfo Create = new ManageDatasetDialogInfo("Name your dataset");
        public static ManageDatasetDialogInfo Delete = new ManageDatasetDialogInfo("Delete dataset?");
        public static ManageDatasetDialogInfo Edit = new ManageDatasetDialogInfo("Rename your dataset");
    }
}
