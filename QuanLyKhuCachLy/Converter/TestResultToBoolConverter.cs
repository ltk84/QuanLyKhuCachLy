using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLyKhuCachLy.Converter
{
    class TestResultToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = bool.Parse(value.ToString());
            if (result)
            {
                return "Dương tính";
            }
            else return "Âm tính";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "Dương tính")
                return true;
            else return false;
        }
    }
}
