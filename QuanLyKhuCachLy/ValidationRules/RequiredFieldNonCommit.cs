using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyKhuCachLy.ValidationRules
{
    class RequiredFieldNonCommit : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (value is int)
            {
                return Int32.Parse(value.ToString()) <= 0 ? new ValidationResult(false, $"Thông tin này là bắt buộc.") : ValidationResult.ValidResult;
            }
            else if (value is string)
            {
                return string.IsNullOrWhiteSpace((string)value) ? new ValidationResult(false, $"Thông tin này là bắt buộc.") : ValidationResult.ValidResult;

            }
            else if (value is Severity)
            {
                return value == null ? new ValidationResult(false, $"Thông tin này là bắt buộc.") : ValidationResult.ValidResult;
            }
            else if (value is DateTime)
            {
                DateTime date;
                bool result = DateTime.TryParse(value.ToString(), out date);
                if (!result || date.ToString() == DateTime.MinValue.ToString())
                {
                    return new ValidationResult(false, $"Thông tin này là bắt buộc.");
                }
            }

            if (value == null) return new ValidationResult(false, $"Thông tin này là bắt buộc.");

            return ValidationResult.ValidResult;
        }
    }
}
