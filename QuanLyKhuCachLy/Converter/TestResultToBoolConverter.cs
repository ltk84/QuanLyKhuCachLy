using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLyKhuCachLy.Converter
{
    class TestResultToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            bool result = bool.Parse(value.ToString());
            if (result)
            {
                return "Dương tính";
            }
            else return "Âm tính";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var i = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => bool.Parse((string)prop.GetValue(value, null)));
            return i.Values.First();
        }
    }
}
