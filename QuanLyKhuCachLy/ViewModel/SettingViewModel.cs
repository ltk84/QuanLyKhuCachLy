using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyKhuCachLy.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {

        private Model.QuarantineArea _QuarantineArea;

        public Model.QuarantineArea QuarantineArea
        {
            get { return _QuarantineArea; }
            set { _QuarantineArea = value; }
        }

        #region address
        private Address _QAAdress;

        public Address QAAdress
        {
            get { return _QAAdress; }
            set { _QAAdress = value; }
        }

        private string _StreetName;
        public string StreetName { get => _StreetName; set { _StreetName = value; OnPropertyChanged(); } }

        private string _ApartmentNumber;
        public string ApartmentNumber { get => _ApartmentNumber; set { _ApartmentNumber = value; OnPropertyChanged(); } }

        private string _SelectedProvince;
        public string SelectedProvince { get => _SelectedProvince; set { _SelectedProvince = value; OnPropertyChanged(); InitDistrictList(); } }

        private string _SelectedWard;
        public string SelectedWard { get => _SelectedWard; set { _SelectedWard = value; OnPropertyChanged(); } }

        private string _SelectedDistrict;
        public string SelectedDistrict { get => _SelectedDistrict; set { _SelectedDistrict = value; OnPropertyChanged(); InitWardList(); } }

        #endregion

        private Model.Staff _Manager;

        public Model.Staff Manager
        {
            get { return _Manager; }
            set { _Manager = value; OnPropertyChanged(); }
        }

        private Model.Staff _SelectedStaff;

        public Model.Staff SelectedStaff
        {
            get { return _SelectedStaff; }
            set { _SelectedStaff = value; OnPropertyChanged(); }
        }

        private string _QuarantineAreaAddress;

        public string QuarantineAreaAddress
        {
            get { return _QuarantineAreaAddress; }
            set { _QuarantineAreaAddress = value; OnPropertyChanged(); }
        }

        private string _SelectedEntity;

        public string SelectedEntity
        {
            get { return _SelectedEntity; }
            set { _SelectedEntity = value; OnPropertyChanged(); }
        }

        #region authentication
        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private string _rePassword;

        public string RePassword
        {
            get { return _rePassword; }
            set { _rePassword = value; OnPropertyChanged(); }
        }



        #endregion

        #region command
        public ICommand ToEditCommand { get; set; }
        public ICommand PreviousTabCommand { get; set; }
        public ICommand NextTabCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RefeshCommand { get; set; }
        public ICommand DeleteEntityCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RePasswordChangedCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand CloseEditAuthenCommand { get; set; }
        public ICommand ToChangePasswordCommand { get; set; }

        #endregion

        #region validation rule

        private bool _QANameFieldHasError;
        public bool QANameFieldHasError { get => _QANameFieldHasError; set { _QANameFieldHasError = value; OnPropertyChanged(); } }

        private bool _QAProvinceFieldHasError;
        public bool QAProvinceFieldHasError { get => _QAProvinceFieldHasError; set { _QAProvinceFieldHasError = value; OnPropertyChanged(); } }

        private bool _QADistrictFieldHasError;
        public bool QADistrictFieldHasError { get => _QADistrictFieldHasError; set { _QADistrictFieldHasError = value; OnPropertyChanged(); } }

        private bool _QAWardFieldHasError;
        public bool QAWardFieldHasError { get => _QAWardFieldHasError; set { _QAWardFieldHasError = value; OnPropertyChanged(); } }

        private bool _QATestCycleFieldHasError;
        public bool QATestCycleFieldHasError { get => _QATestCycleFieldHasError; set { _QATestCycleFieldHasError = value; OnPropertyChanged(); } }

        private bool _QARequiredDayFieldHasError;
        public bool QARequiredDayFieldHasError { get => _QARequiredDayFieldHasError; set { _QARequiredDayFieldHasError = value; OnPropertyChanged(); } }


        #endregion

        #region list

        private ObservableCollection<string> _EntityList;
        public ObservableCollection<string> EntityList
        {
            get => _EntityList; set
            {
                _EntityList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<Staff> _StaffList;
        public ObservableCollection<Staff> StaffList
        {
            get => _StaffList; set
            {
                _StaffList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _ProvinceList;
        public ObservableCollection<string> ProvinceList
        {
            get => _ProvinceList; set
            {
                _ProvinceList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _DistrictList;
        public ObservableCollection<string> DistrictList
        {
            get => _DistrictList; set
            {
                _DistrictList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _WardList;
        public ObservableCollection<string> WardList
        {
            get => _WardList; set
            {
                _WardList = value; OnPropertyChanged();
            }
        }
        #endregion


        public SettingViewModel()
        {

            ToEditCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                EditQuarantineArea editScreen = new EditQuarantineArea();
                editScreen.ShowDialog();
            });

            EditCommand = new RelayCommand<Window>((p) => { return true; }, (o) =>
            {
                EditQuarantineAreaInformation();
                o.Close();
            });

            //PreviousTabCommand = new RelayCommand<object>((p) =>
            //{
            //    if (TabIndex > 1) return true;
            //    return false;
            //}, (o) =>
            //{
            //    HandleChangeTab(TabIndex, "previous");
            //});

            //NextTabCommand = new RelayCommand<object>((p) =>
            //{
            //    if (TabIndex <= 2)
            //        return true;
            //    return false;
            //}, (o) =>
            //{
            //    HandleChangeTab(TabIndex, "next");
            //});

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (o) =>
            {
                o.Close();
            });

            RefeshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (o) =>
            {
                RefeshTab();
            });

            DeleteEntityCommand = new RelayCommand<object>((p) =>
            {
                if (!string.IsNullOrWhiteSpace(SelectedEntity))
                    return true;
                return false;
            }, (o) =>
            {

                DeleteConfirmation confirmation = new DeleteConfirmation();
                if (confirmation.ShowDialog() == true)
                {
                    DeleteEntity();
                }
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) =>
            {
                return true;
            }, (o) =>
            {
                Password = o.Password;
            });

            RePasswordChangedCommand = new RelayCommand<PasswordBox>((p) =>
            {
                return true;
            }, (o) =>
            {
                RePassword = o.Password;
            });

            CloseEditAuthenCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (o) =>
            {
                o.Close();
                ClearPassAndRePass();
            });

            ChangePasswordCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (o) =>
            {
                ChangePassword(o);
                ClearPassAndRePass();
            });

            ToChangePasswordCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (o) =>
            {
                EditAuthenticationAccount editAuthenticationScreen = new EditAuthenticationAccount();
                editAuthenticationScreen.ShowDialog();
            });


            ProvinceList = new ObservableCollection<string>();
            DistrictList = new ObservableCollection<string>();
            WardList = new ObservableCollection<string>();
            EntityList = new ObservableCollection<string>()
            {
                "Tất cả", "Phòng", "Người cách ly", "Nhân viên"
            };

            InitProvinceList();

            if (DataProvider.ins.db.QuarantineAreas.Count() != 0)
            {
                Init();
            }
        }

        #region method
        void ClearPassAndRePass()
        {
            Password = String.Empty;
            RePassword = String.Empty;
        }

        void ChangePassword(Window p)
        {
            var accountInDB = DataProvider.ins.db.Accounts.FirstOrDefault();
            if (accountInDB == null) return;

            if (UserName != accountInDB.username)
            {
                // Show dialog thông báo sai username
                FailNotification ErrorDialog = new FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = "Sai tên tài khoản";

                ErrorDialog.ShowDialog();
                return;
            }
            else if (Password != RePassword)
            {
                // Show dialog thông báo password và repass khác nhau
                FailNotification ErrorDialog = new FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = "Mật khẩu nhập lại không trùng khớp";

                ErrorDialog.ShowDialog();
                return;
            }
            else if (Password.Length == 0 || RePassword.Length == 0)
            {
                // Show dialog thông báo password hoặc repass ko đc rỗng
                FailNotification ErrorDialog = new FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = "Mật khẩu đang được để trống";
                ErrorDialog.ShowDialog();
                return;
            }
            else if (Password.Length > 100)
            {
                // Show dialog password quá dài
                FailNotification ErrorDialog = new FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = "Password quá dài (> 100 ký tự)";
                ErrorDialog.ShowDialog();
                return;
            }

            accountInDB.password = MD5Hash(Base64Encode(Password));
            DataProvider.ins.db.SaveChanges();
            p.Close();

            // dialog success
            Window SuccessDialog = new Window
            {
                AllowsTransparency = true,
                Background = Brushes.Transparent,
                Width = 600,
                Height = 400,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.None,
                Content = new SuccessNotification()
            };
            SuccessDialog.ShowDialog();
        }

        string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        string MD5Hash(string input)
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

        void DeleteEntity()
        {
            Window SuccessDialog = new Window
            {
                AllowsTransparency = true,
                Background = Brushes.Transparent,
                Width = 600,
                Height = 400,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.None,
                Content = new SuccessNotification()
            };
            switch (SelectedEntity)
            {
                case "Tất cả":
                    DeleteAll();
                    break;
                case "Phòng":
                    DeleteAllRecordInRoomList();
                    SuccessDialog.ShowDialog();
                    break;
                case "Người cách ly":
                    DeleteAllRecordInPersonList();
                    SuccessDialog.ShowDialog();
                    break;
                case "Nhân viên":
                    DeleteAllRecordInStaffList();
                    SuccessDialog.ShowDialog();
                    break;
                default:
                    break;
            }
            RefeshTab();
        }

        void RestartApplication()
        {
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

        void DeleteAllRecordInAddrerssList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.Addresses.RemoveRange(DataProvider.ins.db.Addresses);
                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void DeleteAll()
        {
            DeleteAllRecordInRoomList();
            DeleteAllRecordInPersonList();
            DeleteAllRecordInStaffList();
            DeleteQuarantineAreaInformation();
            DeleteSeverityList();
            DeleteAllRecordInAddrerssList();

            Window SuccessDialog = new Window
            {
                AllowsTransparency = true,
                Background = Brushes.Transparent,
                Width = 600,
                Height = 400,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.None,
                Content = new SuccessNotification()
            };
            SuccessDialog.ShowDialog();

            RestartApplication();
        }

        void DeleteSeverityList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.Severities.RemoveRange(DataProvider.ins.db.Severities);
                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void DeleteQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.QuarantineAreas.RemoveRange(DataProvider.ins.db.QuarantineAreas);
                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void DeleteAllRecordInStaffList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.Staffs.RemoveRange(DataProvider.ins.db.Staffs);
                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void DeleteAllRecordInPersonList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.QuarantinePersons.RemoveRange(DataProvider.ins.db.QuarantinePersons);
                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void DeleteAllRecordInRoomList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.QuarantineRooms.RemoveRange(DataProvider.ins.db.QuarantineRooms);
                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void RefeshTab()
        {
            Init();
        }

        void Init()
        {
            QuarantineArea = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
            QAAdress = QuarantineArea?.Address;

            if (QAAdress == null) return;

            SelectedProvince = QAAdress.province;
            SelectedDistrict = QAAdress.district;
            SelectedWard = QAAdress.ward;
            ApartmentNumber = QAAdress.apartmentNumber;
            StreetName = QAAdress.streetName;
            Manager = DataProvider.ins.db.Staffs.Where(staff => staff.id == QuarantineArea.managerID).FirstOrDefault();
            SelectedStaff = Manager;
            StaffList = new ObservableCollection<Staff>(DataProvider.ins.db.Staffs);

            UpdateDisplayAddress(QuarantineArea.Address);

            var account = DataProvider.ins.db.Accounts.FirstOrDefault();
            if (account == null) return;
            UserName = account.username;

            Password = String.Empty;
            RePassword = String.Empty;
        }

        void InitProvinceList()
        {
            foreach (var item in AddressViewModel.ProvinceList)
            {
                ProvinceList.Add(item.name);
            }
        }

        void InitDistrictList()
        {
            AddressViewModel.ProvinceSelectEvent(SelectedProvince);
            DistrictList.Clear();
            foreach (var item in AddressViewModel.DistrictList)
            {
                DistrictList.Add(item.name);
            }
        }

        void InitWardList()
        {
            AddressViewModel.DistrictSelectEVent(SelectedDistrict);
            WardList.Clear();
            foreach (var item in AddressViewModel.WardList)
            {
                WardList.Add(item.name);
            }
        }

        //void SetDefaultEditTab()
        //{
        //    Tab1 = Visibility.Visible;
        //    Tab2 = Visibility.Hidden;
        //    TabIndex = 1;
        //    TabPosition = $"{TabIndex}/2";
        //}

        void EditQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {

                    QAAdress.province = SelectedProvince;
                    QAAdress.district = SelectedDistrict;
                    QAAdress.ward = SelectedWard;
                    QAAdress.apartmentNumber = ApartmentNumber;
                    QAAdress.streetName = StreetName;

                    Manager = SelectedStaff;

                    if (Manager != null)
                        QuarantineArea.managerID = Manager.id;

                    DataProvider.ins.db.SaveChanges();

                    UpdateDisplayAddress(QuarantineArea.Address);

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        //void HandleChangeTab(int index, string action)
        //{
        //    if (action == "next")
        //    {
        //        TabIndex++;
        //    }
        //    else
        //    {
        //        TabIndex--;
        //    }
        //    if (TabIndex <= 2)
        //        TabPosition = $"{TabIndex}/2";

        //    switch (TabIndex)
        //    {
        //        case 1:
        //            Tab1 = Visibility.Visible;
        //            Tab2 = Visibility.Hidden;
        //            break;
        //        case 2:
        //            Tab1 = Visibility.Hidden;
        //            Tab2 = Visibility.Visible;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        void UpdateDisplayAddress(Address PersonAddress)
        {
            if (PersonAddress == null) return;
            List<string> list = new List<string>()
            {
                PersonAddress.apartmentNumber,
                PersonAddress.streetName,
                PersonAddress.ward,
                PersonAddress.district,
                PersonAddress.province

            };

            QuarantineAreaAddress = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(list[i]))
                {
                    QuarantineAreaAddress += list[i];
                }
                if (i != list.Count - 1)
                {
                    if (i != 0)
                        QuarantineAreaAddress += ", ";
                    else QuarantineAreaAddress += " ";
                }
            }
        }


        #endregion
    }
}
