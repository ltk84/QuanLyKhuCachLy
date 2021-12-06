using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region property
        public bool isLogin { get; set; }

        private string _username;
        public string username { get => _username; set { _username = value; OnPropertyChanged(); } }

        private string _password;
        public string password { get => _password; set { _password = value; OnPropertyChanged(); } }

        public ICommand loginCommand { get; set; }
        public ICommand closeCommand { get; set; }
        public ICommand passwordChangedCommand { get; set; }
        #endregion

        public LoginViewModel()
        {
            isLogin = false;

            loginCommand = new RelayCommand<AuthenticationScreen>((p) =>
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                    return true;
                return false;
            }, (p) =>
            {
                login(p);
            });

            closeCommand = new RelayCommand<AuthenticationScreen>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            passwordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                password = p.Password;
            });
        }

        #region methods



        /// <summary>
        /// Xử lý đăng nhập
        /// </summary>
        /// <param name="p"> Màn hình login</param>
        void login(AuthenticationScreen p)
        {
            if (p == null) return;

            // Trường hợp trường username bị để trống
            if (username == "")
            {
                FailNotification ErrorDialog = new FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = "Tên đăng nhập không được để trống";

                ErrorDialog.ShowDialog();
                return;
            }

            // Trường hợp trường username bị để trống
            if (password == "")
            {
                FailNotification ErrorDialog = new FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = "Mật khẩu không được để trống";

                ErrorDialog.ShowDialog();
                return;
            }

            // mã hóa mật khẩu
            string encodePassword = MD5Hash(Base64Encode(password));

            var accountList = DataProvider.ins.db.Accounts;

            bool isCorrectPass = false;
            bool isExistUserName = false;

            // xử lý so sánh lần lượt các account trong danh sách tài khoản 
            foreach (var acc in accountList)
            {
                if (acc.username.Equals(username))
                {
                    isExistUserName = true;
                    if (acc.password.Equals(encodePassword))
                    {
                        isLogin = true;
                        p.Close();
                        return;
                    }
                }
            }

            isLogin = false;

            // trường hợp tên đăng nhập chưa dược đặng ký
            if (!isExistUserName)
            {
                FailNotification ErrorDialog = new FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = "Tài khoản chưa được đăng ký";

                ErrorDialog.ShowDialog();
                return;
            }

            // trường hợp sai mật khẩu
            if (!isCorrectPass)
            {

                FailNotification ErrorDialog = new FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = "Mật khẩu bị sai";

                ErrorDialog.ShowDialog();
                return;
            }
        }

        /// <summary>
        /// Hàm mã hõa chuỗi thành Base64
        /// </summary>
        /// <param name="plainText">Chuỗi</param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Hàm mã hóa chuỗi thành MD5
        /// </summary>
        /// <param name="input">Chuỗi</param>
        /// <returns></returns>
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }


        #endregion
    }
}
