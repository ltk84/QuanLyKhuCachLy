using System;
using System.Globalization;
using System.Windows.Controls;

namespace QuanLyKhuCachLy.ValidationRules
{
    public class DateFormat : ValidationRule
    {
        public DateFormat()
        {

        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime input;
            try
            {
                if (((string)value).Length > 0)
                    input = DateTime.Parse((String)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, $"Vui lòng nhập theo mẫu: 08/04/2001.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
