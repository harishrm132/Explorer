using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Explorer
{
    /// <summary>
    /// View Model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Properties
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
        public string Name { get { return Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileFolderName(FullPath); } }

        /// <summary>
        /// List of all children inside the item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicate If this item can be expnaded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                //If UI Tells us to expand 
                if (value == true)
                    //Find all Children
                    Expand();
                else //UI Tells us to close
                    this.ClearChildren();
            }
        }

        #endregion

        /// <summary>
        /// Command To Expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="fullpath">Full Path of Item</param>
        /// <param name="type">Type of item</param>
        public DirectoryItemViewModel(string fullpath, DirectoryItemType type)
        {
            this.ExpandCommand = new RelayCommand(Expand);

            //Set path & type
            this.FullPath = fullpath;
            this.Type = type;

            //Setup the children as needed
            this.ClearChildren();
        }

        #region Helper Methods
        /// <summary>
        /// Remove all Children Fromn the List
        /// </summary>
        private void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            //Show Expand if not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        } 
        #endregion

        /// <summary>
        /// Expand this Directory & find all Children
        /// </summary>
        private void Expand()
        {
            //Cant Expand Filke
            if (this.Type == DirectoryItemType.File) return;

            this.Children = new ObservableCollection<DirectoryItemViewModel>
                (
                    DirectoryStructure.GetDirectoryContents(this.FullPath).
                    Select(content => new DirectoryItemViewModel(content.FullPath, content.Type))
                );
        }
    }
}