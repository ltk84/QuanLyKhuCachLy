using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace QuanLyKhuCachLy.ValidationRules
{
    public class NoSpecialChar : ValidationRule
    {
        public NoSpecialChar()
        {
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var regexItem = new Regex("^[a-zA-Z0-9áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệóòỏõọôốồổỗộơớờởỡợíìỉĩịúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÍÌỈĨỊÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ ]*$");
            if (regexItem.IsMatch((string)value))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Vui lòng không nhập ký tự đặc biệt.");
            }
        }
    }
}
