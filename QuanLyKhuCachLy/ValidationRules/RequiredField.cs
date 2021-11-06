﻿using QuanLyKhuCachLy.Model;
using System;
using System.Globalization;
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
            if (value is Severity)
            {
                Severity severity = value as Severity;
                return String.IsNullOrWhiteSpace(severity.level)
                    ? new ValidationResult(false, $"Thông tin này là bắt buộc.")
                    : ValidationResult.ValidResult;
            }

            if (value is DateTime)
            {
                return String.IsNullOrWhiteSpace(value.ToString())
                    ? new ValidationResult(false, $"Thông tin này là bắt buộc.")
                    : ValidationResult.ValidResult;
            }

            if (value is string && String.IsNullOrWhiteSpace((string)value))
            {
                return new ValidationResult(false, $"Thông tin này là bắt buộc.");
            }
            return ValidationResult.ValidResult;
        }
    }
}