﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyKhuCachLy.ValidationRules
{
    public class OnlyNumber : ValidationRule
    {
        public OnlyNumber()
        {

        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var regexItem = new Regex("^[0-9]*$");
            if (regexItem.IsMatch((string)value))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Vui lòng chỉ nhập chuỗi có kí tự là số.");
            }
        }
    }
}
