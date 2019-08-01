using System;
using System.Windows.Data;

namespace Computer_Era_X.Converters
{
    public class RealtyPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = "Assets/Realty/" + (string)value + ".png";

            if (System.IO.File.Exists(System.IO.Path.GetFullPath(path)) == false)
            {
                return "/Computer Era X;component/Assets/Realty/house.png";
            } else {
                return "/Computer Era X;component/" + path;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
