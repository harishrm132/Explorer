using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer
{
    /// <summary>
    /// View Model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The Type of the item
        /// </summary>
        public DirectoryItemType Type { get; set; }


        /// <summary>
        /// Full path of item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The Name of the directory Item
        /// </summary>
        public string Name { get { return Type == DirectoryItemType.Drive ? FullPath : DirectoryStucture.GetFileFolderName(FullPath); } }

        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicate If this item can be expnaded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }
    }
}