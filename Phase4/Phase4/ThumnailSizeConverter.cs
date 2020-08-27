using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Phase4
{
    public class ThumnailSizeNameConverter : IValueConverter
    {
        private readonly string m_ThumnailSizeStrS = "Small";
        private readonly string m_ThumnailSizeStrM = "Middle";
        private readonly string m_ThumnailSizeStrL = "Large";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String size_name = "";
            switch (System.Convert.ToInt32(value))
            {
                case 1:
                    size_name = m_ThumnailSizeStrS;
                    break;

                default:    // no break
                case 2:
                    size_name = m_ThumnailSizeStrM;
                    break;

                case 3:
                    size_name = m_ThumnailSizeStrL;
                    break;
            }
            return size_name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class ThumnailSizeWidthConverter : IValueConverter
    {
        private readonly int m_ThumnailScaleWidth = 40;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double)value * m_ThumnailScaleWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class ThumnailSizeHeightConverter : IValueConverter
    {
        private readonly int m_ThumnailScaleHeight = 30;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double)value * m_ThumnailScaleHeight;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
