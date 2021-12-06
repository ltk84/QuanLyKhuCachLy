using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
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
                    object sourceValue;
                    Type propertyType;
                    if (!propertyName.Contains("."))
                    {
                        sourceValue = sourceItem.GetType().GetProperty(propertyName).GetValue(sourceItem, null);
                        propertyType = sourceItem.GetType().GetProperty(propertyName).PropertyType;
                    }
                    else
                    {
                        var dotIndex = propertyName.IndexOf(".");
                        var childVM = propertyName.Substring(0, dotIndex);
                        var subProperty = propertyName.Substring(dotIndex + 1);

                        var x = sourceItem.GetType().GetProperty(childVM).GetValue(sourceItem);
                        var y = x.GetType().GetProperty(subProperty).GetValue(x, null);
                        sourceValue = y;
                        propertyType = x.GetType().GetProperty(subProperty).PropertyType;
                    }

                    if (propertyType.Name == "Int32")
                    {
                        if (propertyName == "RoomCapacity")
                        {
                            var SelectedItemVM = sourceItem.GetType().GetProperty("SelectedItem").GetValue(sourceItem);
                            if (SelectedItemVM != null)
                            {
                                var PersonListCount = SelectedItemVM.GetType().GetProperty("QuarantinePersons").GetValue(SelectedItemVM);
                                HashSet<QuarantinePerson> convertList = (HashSet<QuarantinePerson>)PersonListCount;
                                if ((int)sourceValue < convertList.Count)
                                    return new ValidationResult(false, $"Giá trị bé hơn số người trong phòng ({convertList.Count} người)");
                                else return ValidationResult.ValidResult;
                            }
                        }
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
                    else if (propertyType.Name == "DateTime")
                    {
                        DateTime date;
                        bool result = DateTime.TryParse(sourceValue.ToString(), out date);
                        if (!result || date.ToString() == DateTime.MinValue.ToString())
                        {
                            return new ValidationResult(false, $"Thông tin này là bắt buộc.");
                        }
                    }
                }
            }

            return ValidationResult.ValidResult;

        }
    }
}