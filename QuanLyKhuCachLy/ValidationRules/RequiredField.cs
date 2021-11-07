using QuanLyKhuCachLy.Model;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace QuanLyKhuCachLy.ValidationRules
{
    public class RequiredField : ValidationRule
    {
        public RequiredField() : base(ValidationStep.CommittedValue, true)
        {
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //if (value is System.Windows.Data.BindingExpression)
            //{
            //    var binding = value as BindingExpression;
            //    ComboBox comboBox = binding.Target as ComboBox;
            //    if (comboBox == null) return null;
            //    return comboBox.SelectedItem == null ? new ValidationResult(false, $"Thông tin này là bắt buộc.") : ValidationResult.ValidResult;
            //}

            var expression = value as BindingExpression;
            if (expression != null)
            {
                var sourceItem = expression.DataItem;
                if (sourceItem != null)
                {
                    var propertyName = expression.ParentBinding != null && expression.ParentBinding.Path != null ? expression.ParentBinding.Path.Path : null;
                    var sourceValue = sourceItem.GetType().GetProperty(propertyName).GetValue(sourceItem, null);
                    var propertyType = sourceItem.GetType().GetProperty(propertyName).PropertyType;

                    if (propertyType.Name == "Int32")
                    {
                        return Int32.Parse(sourceValue.ToString()) <= 0 ? new ValidationResult(false, $"Thông tin này là bắt buộc.") : ValidationResult.ValidResult;
                    }
                    else if (propertyType.Name == "String")
                    {
                        return string.IsNullOrWhiteSpace((string)sourceValue) ? new ValidationResult(false, $"Thông tin này là bắt buộc.") : ValidationResult.ValidResult;

                    }
                    else if (propertyType.Name == "Severity")
                    {

                        return sourceValue == null ? new ValidationResult(false, $"Thông tin này là bắt buộc.") : ValidationResult.ValidResult;
                    }

                }
            }




            if (value is DateTime)
            {
                return String.IsNullOrWhiteSpace(value.ToString())
                    ? new ValidationResult(false, $"Thông tin này là bắt buộc.")
                    : ValidationResult.ValidResult;
            }

            if (value is string)
            {
                if (String.IsNullOrWhiteSpace(value.ToString()))
                    return new ValidationResult(false, $"Thông tin này là bắt buộc.");
                return ValidationResult.ValidResult;
            }

            return ValidationResult.ValidResult;

        }
    }
}