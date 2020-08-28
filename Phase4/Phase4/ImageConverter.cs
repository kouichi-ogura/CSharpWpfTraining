using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;

namespace Phase4
{
    public class ImageConverter : IValueConverter
    {
        private readonly int m_ThumailDecodePixelWidth = 50;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(value.ToString());
            img.DecodePixelWidth = m_ThumailDecodePixelWidth;
            img.EndInit();
            return img;
        }

        //-----------------------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
