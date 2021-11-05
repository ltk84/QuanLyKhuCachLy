using System;
using System.Globalization;
using System.Windows.Controls;

namespace QuanLyKhuCachLy.ValidationRules
{
    class NumberField : ValidationRule
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
        public NumberField()
        {
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int input = 0;
            try
            {
                if (((string)value).Length > 0)
                    input = Int32.Parse((String)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, $"Vui lòng chỉ nhập số.");
            }

            if (Min != null && Max != null)
            {
                if ((input < Min) || (input > Max))
                {
                    return new ValidationResult(false,
                      $"Vui lòng nhập số từ {Min} - {Max}.");
                }
            }
            else if (Max == null)
            {

                if (input < Min)
                {
                    return new ValidationResult(false, $"Vui lòng nhập số từ {Min} trở lên.");
                }
            }
            else
            {
                if (input > Max)
                {
                    return new ValidationResult(false, $"Vui lòng nhập số từ {Max} trở xuống.");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
