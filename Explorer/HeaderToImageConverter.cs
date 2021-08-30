using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Explorer
{
    /// <summary>
    /// Conver full path to Image based on file or drive or folder
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            if (string.IsNullOrWhiteSpace(path)) return null;

            var name = DirectoryStucture.GetFileFolderName(path);

            var image = "Images/file.png";
            //if name is black we presume it as drive 
            if(string.IsNullOrWhiteSpace(name)) 
                image = "Images/drive.png";
            else if(new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory)) 
                image = "Images/folder-closed.png";

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
