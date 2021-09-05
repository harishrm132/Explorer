using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer
{
    /// <summary>
    /// Helper class to query information
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Get Logical Drive of PC
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem
            {
                FullPath = drive,
                Type = DirectoryItemType.Drive
            }).ToList();
        }

        /// <summary>
        /// Get Directories of top level content
        /// </summary>
        /// <param name="fullpath">Directory Full path</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullpath)
        {
            //Create empty list 
            var items = new List<DirectoryItem>();

            #region GetDirectories
            try
            {
                var dirs = Directory.GetDirectories(fullpath);
                if (dirs.Length > 0) items.AddRange(dirs.Select(dir => new DirectoryItem() 
                {
                    FullPath = dir,
                    Type = DirectoryItemType.Folder
                }));
            }
            catch (Exception) { }
            #endregion

            #region GetFiles
            try
            {
                var file = Directory.GetFiles(fullpath);
                if (file.Length > 0) items.AddRange(file.Select(fs => new DirectoryItem() 
                {
                    FullPath = fs,
                    Type = DirectoryItemType.File
                }));
            }
            catch (Exception) { }
            #endregion

            return items;
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
