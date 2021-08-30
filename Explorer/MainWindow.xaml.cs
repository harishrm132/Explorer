using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CFlamingo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        } 
        #endregion

        /// <summary>
        /// When Application First Opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get Logical drives in machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem()
                {
                    Header = drive,
                    Tag = drive,
                };

                //Add a Dummy item
                item.Items.Add(null);

                //Event for Drive Expanded
                item.Expanded += Folder_Expanded;


                //Add to tree view
                FolderView.Items.Add(item);
            } 
        }

        /// <summary>
        /// When a folder is expanded, find the subfolder/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region InitialChecks
            var item = (TreeViewItem)sender;

            //If the item only contains dummy data
            if (item.Items.Count != 1 || item.Items[0] != null) return;

            //clear dummy data
            item.Items.Clear();

            //Get Full path
            var fullpath = (string)item.Tag;
            #endregion

            #region GetDirectories
            //Create a Blank List & get dirs from folders
            var directories = new List<string>();
            try
            {
                var dirs = Directory.GetDirectories(fullpath);
                if (dirs.Length > 0) directories.AddRange(dirs);
            }
            catch (Exception) { }

            directories.ForEach((Action<string>)(directorypath =>
            {
                //Create Directory item
                var subitem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directorypath), //Folder Name
                    Tag = directorypath
                };
                subitem.Items.Add(null);

                subitem.Expanded += Folder_Expanded;

                //Add Items to add
                item.Items.Add(subitem);
            }));

            #endregion

            #region GetFiles
            //Create a Blank List & get dirs from files
            var files = new List<string>();
            try
            {
                var file = Directory.GetFiles(fullpath);
                if (file.Length > 0) files.AddRange(file);
            }
            catch (Exception) { }

            files.ForEach((Action<string>)(filepath =>
            {
                //Create file item
                var subitem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filepath), //Folder Name
                    Tag = filepath
                };
                //Add Items to add
                item.Items.Add(subitem);
            }));

            #endregion
        }

        /// <summary>
        /// Find the File or foldername from fullpath
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return string.Empty;

            //Maek it unique
            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');
            if (lastIndex <= 0) return path;

            return path.Substring(lastIndex + 1);
        }
    }
}
