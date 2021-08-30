
namespace Explorer
{
    /// <summary>
    /// Information about directory item (drive , file, folder)
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The Type of the item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// Absolute path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The Name of the directory Item
        /// </summary>
        public string Name { get { return Type == DirectoryItemType.Drive ? FullPath : DirectoryStucture.GetFileFolderName(FullPath); } }
    }
}
