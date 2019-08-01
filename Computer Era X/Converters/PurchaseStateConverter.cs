using Computer_Era_X.DataTypes.Objects;
using System;
using System.Windows.Data;

namespace Computer_Era_X.Converters
{
    public class PurchaseStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((House)value).IsPurchase == 1 || ((House)value).IsCreditPurchase == 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
