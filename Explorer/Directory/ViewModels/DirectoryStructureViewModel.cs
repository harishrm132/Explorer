 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer
{
    /// <summary>
    /// View Model For application main Directory View
    /// </summary>
    public class DirectoryStructureViewModel
    {
        /// <summary>
        /// List of all directories of the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            var children = DirectoryStructure.GetLogicalDrives();
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }

    }
}
