using System;

namespace AutomationTests.Entities
{
    internal class Dataset
    {
        /// <summary>
        /// Index of dataset with same name, e.g. if table has two "TestDataset", the first one has 0 index, the second one - 1
        /// </summary>
        public int SameNameIndex { get; }
        public string Name { get; }

        /// <summary>
        /// Describe last edit date of the dataset in format MMM d, yyyy, e.g. "Apr 4, 1990"
        /// </summary>
        public string ModifiedDate { get; }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="name">Name of the dataset</param>
        /// <param name="modifiedDate">DateTime struct, which describe last edit date of the dataset</param>
        /// <param name="sameNameIndex">Index of dataset with same name, e.g. if table has two "TestDataset", the first one has 0</param>
        public Dataset(string name, DateTime modifiedDate, int sameNameIndex = 0)
        {
            SameNameIndex = sameNameIndex;
            Name = name;
            ModifiedDate = modifiedDate.ToString("MMM d, yyyy");
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="name">Name of the dataset</param>
        /// <param name="modifiedDate">Describe last edit date of the dataset in format MMM d, yyyy, e.g. "Apr 4, 1990"</param>
        /// <param name="sameNameIndex">Index of dataset with same name, e.g. if table has two "TestDataset", the first one has 0</param>
        public Dataset(string name, string modifiedDate, int sameNameIndex = 0)
        {
            SameNameIndex = sameNameIndex;
            Name = name;
            ModifiedDate = modifiedDate;
        }

        /// <summary>
        /// Constructor with parameters
        /// Modified date is set as current
        /// </summary>
        /// <param name="name">Name of the dataset</param>
        /// <param name="sameNameIndex">Index of dataset with same name, e.g. if table has two "TestDataset", the first one has 0</param>
        public Dataset(string name, int sameNameIndex = 0) : this(name, DateTime.Now)
        {
            SameNameIndex = sameNameIndex;
        }

        public override string ToString()
        {
            return $"Dataset {{Name: {Name}; Date: {ModifiedDate}; Index with same name: {SameNameIndex}}}";
        }
    }
}
