using DMController.Models;
using DMController.Models.OrganizeNode;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DMController.Converters
{
    class TypeNodeToIconConverter : IValueConverter
    {
        private const string ICON_FOLDER = "../Images/Grey_Generic_Folder.png";
        private const string ICON_FILE_EXT_SRT = "../Images/fileExtSrt.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string iconPath;
            switch ((eTypeNode)value)
            {
                case eTypeNode.IsFolder:
                    iconPath = ICON_FOLDER;
                    break;
                case eTypeNode.IsFileExtSrt:
                    iconPath = ICON_FILE_EXT_SRT;
                    break;
                default:
                    iconPath = string.Empty;
                    break;
            }
            return iconPath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
