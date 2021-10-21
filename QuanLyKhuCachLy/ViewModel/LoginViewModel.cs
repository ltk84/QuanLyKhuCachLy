using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region property
        public bool  isLogin { get; set; }

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
            username = "";
            password = "";

            loginCommand = new RelayCommand<LoginWindow>((p) => { return true; }, (p) =>
            {
                login(p);
            });

            closeCommand = new RelayCommand<LoginWindow>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            passwordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                password = p.Password;
            });
        }

        #region methods
        void login(LoginWindow p) {
            if (username == "tunglete" && password == "tunglete")
            {
                isLogin = true;
                p.Close();
            }
            else
            {
                MessageBox.Show("Sai rui!");
            }
        }



        #endregion
    }
}
