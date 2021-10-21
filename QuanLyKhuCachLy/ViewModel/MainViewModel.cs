using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool isLoaded { get; set; }
        public ICommand loadedCommand { get; set; }

        public MainViewModel()
        {
            isLoaded = false;
            loadedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                loadLoginScreen(p);
            });
            
        }

        void loadLoginScreen(Window p)
        {
            isLoaded = true;
            if (p == null) return;
            p.Hide();

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            if (loginWindow.DataContext == null) return;
            var loginVM = loginWindow.DataContext as LoginViewModel;

            if (loginVM.isLogin) {
                p.Show();
            }
            else {
                p.Close();
            }

        }
    }
}
