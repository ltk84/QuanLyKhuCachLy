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
            int price = (int)value;
            return price.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = (string)value;
            int price;
            if (!int.TryParse(text, out price))
            {

                return 0; //Some default value
            }

            return price;
        }
    }
}
