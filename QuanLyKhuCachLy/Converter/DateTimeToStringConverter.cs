using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLyKhuCachLy.Converter
{
    class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            return dateTime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string dateTimeStr = value as string;
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeStr, out dateTime))
            {
                return DateTime.Now;
            }
            return dateTime;
        }
    }
}
