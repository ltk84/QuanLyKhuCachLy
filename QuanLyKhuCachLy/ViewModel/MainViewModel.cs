using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICommand qAInformationCommand { get; set; }

        public MainViewModel()
        {
            isLoaded = false;
            loadedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                loadLoginScreen(p);
            });

            //qAInformationCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    loadQAInformationWindow();
            //});



        }

        void loadQAInformationWindow()
        {
            //QuarantineAreaInformationWindow qAreaWindow = new QuarantineAreaInformationWindow();
            //qAreaWindow.Show();
        }

        void loadLoginScreen(Window p)
        {
            isLoaded = true;
            if (p == null) return;
            p.Hide();

            AuthenticationScreen loginWindow = new AuthenticationScreen();
            loginWindow.ShowDialog();

            if (loginWindow.DataContext == null) return;
            var loginVM = loginWindow.DataContext as LoginViewModel;

            bool CheckInitialSetUp = DataProvider.ins.db.QuarantineAreas.Count() > 0;
            InitialSettingScreen InitialSettingScreen = new InitialSettingScreen();
            if (InitialSettingScreen.DataContext == null) return;
            var InitVM = InitialSettingScreen.DataContext as QuarantineAreaInformationViewModel;

            if (loginVM.isLogin)
            {
                // Chỗ này cần xử lý thêm: Hiện tại là nếu tắt screen inital set up giữa chừng thì sẽ close chương trình (không có thông báo)
                // Mong muốn là khi đóng giữa chừng thì sẽ thông báo rồi mới close (hoặc xử lý kiểu khác)
                if (!CheckInitialSetUp)
                {
                    InitialSettingScreen.ShowDialog();
                    // Xử lý close InitScrren ở button Xác nhận nữa
                    if (InitVM.isDoneSetUp) p.Show();
                    else p.Close();
                }
                else
                {
                    p.Show();
                }
            }
            else
            {
                p.Close();
            }

        }
    }
}
