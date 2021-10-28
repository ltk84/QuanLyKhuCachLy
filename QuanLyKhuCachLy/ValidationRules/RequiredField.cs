﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyKhuCachLy.ValidationRules
{
    public class RequiredField : ValidationRule
    {

        public RequiredField()
        {
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, $"Thông tin này là bắt buộc.");
            }

            if (String.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, $"Thông tin này là bắt buộc.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
