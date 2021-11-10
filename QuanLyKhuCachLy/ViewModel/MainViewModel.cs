using QuanLyKhuCachLy.Model;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region property
        #region Handle Dashboard
        public DashboardViewModel DashboardVM { get; set; }
        public QuarantineRoomViewModel QuarantineRoomVM { get; set; }
        public QuarantinePersonViewModel quarantinePersonViewModel { get; set; }
        public StaffViewModel StaffVM { get; set; }
        public ReportViewModel ReportVM { get; set; }


        private object _currentView;
        private bool _isOnDashboard;
        private bool _isOnRoom;
        private bool _isOnPerson;
        private bool _isOnStaff;
        private bool _isOnStat;
        private bool _isOnNotify;
        public ICommand ToDashboardCommand { get; set; }
        public ICommand ToRoomCommand { get; set; }
        public ICommand ToPersonCommand { get; set; }
        public ICommand ToStaffCommand { get; set; }
        public ICommand ToStatCommand { get; set; }
        public ICommand ToNotifyCommand { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public bool IsOnDashboard
        {
            get { return _isOnDashboard; }
            set
            {
                _isOnDashboard = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnRoom
        {
            get { return _isOnRoom; }
            set
            {
                _isOnRoom = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnPerson
        {
            get { return _isOnPerson; }
            set
            {
                _isOnPerson = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnStaff
        {
            get { return _isOnStaff; }
            set
            {
                _isOnStaff = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnStat
        {
            get { return _isOnStat; }
            set
            {
                _isOnStat = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnNotify
        {
            get { return _isOnNotify; }
            set
            {
                _isOnNotify = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Handle Login Screen
        public bool IsLoaded { get; set; }
        public ICommand LoadedCommand { get; set; }

        #endregion
        #endregion

        public MainViewModel()
        {
            IsLoaded = false;
            LoadedCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadLoginScreenAndCheckInitSetUp(p);
            });

            Init();

            ToDashboardCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                ToDashboard();
            });
            ToRoomCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                ToRoom();
            });
            ToPersonCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                ToPerson();
            });
            ToStaffCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                ToStaff();
            });
            ToStatCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                ToStat();
            });
            ToNotifyCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                ToNotify();
            });
        }

        #region method
        private void Init()
        {
            DashboardVM = new DashboardViewModel();
            QuarantineRoomVM = new QuarantineRoomViewModel();
            StaffVM = new StaffViewModel();
            quarantinePersonViewModel = new QuarantinePersonViewModel();
            ReportVM = new ReportViewModel();
            // test room
            //ToDashboard();
            ToRoom();
        }

        private void ToDashboard()
        {
            CurrentView = DashboardVM;
            _isOnDashboard = true;
            _isOnRoom = false;
            _isOnPerson = false;
            _isOnStaff = false;
            _isOnStat = false;
            _isOnNotify = false;
        }

        private void ToRoom()
        {
            CurrentView = QuarantineRoomVM;
            _isOnDashboard = false;
            _isOnRoom = true;
            _isOnPerson = false;
            _isOnStaff = false;
            _isOnStat = false;
            _isOnNotify = false;
        }

        private void ToPerson()
        {
            CurrentView = quarantinePersonViewModel;
            _isOnDashboard = false;
            _isOnRoom = false;
            _isOnPerson = true;
            _isOnStaff = false;
            _isOnStat = false;
            _isOnNotify = false;
        }

        private void ToStaff()
        {
            CurrentView = StaffVM;
            _isOnDashboard = false;
            _isOnRoom = false;
            _isOnPerson = false;
            _isOnStaff = true;
            _isOnStat = false;
            _isOnNotify = false;
        }
        private void ToStat()
        {
            CurrentView = ReportVM;
            _isOnDashboard = false;
            _isOnRoom = false;
            _isOnPerson = false;
            _isOnStaff = false;
            _isOnStat = true;
            _isOnNotify = false;
        }
        private void ToNotify()
        {
            _isOnDashboard = false;
            _isOnRoom = false;
            _isOnPerson = false;
            _isOnStaff = false;
            _isOnStat = false;
            _isOnNotify = true;
        }

        void LoadLoginScreenAndCheckInitSetUp(Window p)
        {
            IsLoaded = true;
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
                // Test (suppose !CheckInit)
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

        #endregion
    }
}
