using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLyKhuCachLy.Converter
{
    public class IntToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int number = (int)value;
            return number.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = (string)value;
            int number;
            if (!int.TryParse(text, out number))
            {

                return 0; //Some default value
            }

            return number;
        }
    }
}
