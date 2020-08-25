using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Phase4
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return new BitmapImage(new Uri(value.ToString()));
            }
            catch (Exception)
            {
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
