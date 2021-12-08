using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyKhuCachLy.ViewModel
{

    public class QuarantineAreaInformationViewModel : BaseViewModel
    {
        #region property

        #region Quarantine Area
        private string _QAName;
        public string QAname { get => _QAName; set { _QAName = value; OnPropertyChanged(); } }

        private string _QAStreetName;
        public string QAStreetName { get => _QAStreetName; set { _QAStreetName = value; OnPropertyChanged(); } }

        private string _QAApartmentNumber;
        public string QAApartmentNumber { get => _QAApartmentNumber; set { _QAApartmentNumber = value; OnPropertyChanged(); } }

        private string _QASelectedProvince;
        public string QASelectedProvince { get => _QASelectedProvince; set { _QASelectedProvince = value; OnPropertyChanged(); InitQADistrictList(); } }

        private string _QASelectedWard;
        public string QASelectedWard { get => _QASelectedWard; set { _QASelectedWard = value; OnPropertyChanged(); } }

        private string _QASelectedDistrict;
        public string QASelectedDistrict { get => _QASelectedDistrict; set { _QASelectedDistrict = value; OnPropertyChanged(); InitQAWardList(); } }

        private int _QATestCycle;
        public int QATestCycle { get => _QATestCycle; set { _QATestCycle = value; OnPropertyChanged(); } }

        private int _QARequiredDayToFinish;
        public int QARequiredDayToFinish { get => _QARequiredDayToFinish; set { _QARequiredDayToFinish = value; OnPropertyChanged(); } }

        #endregion

        #region Manager
        private string _ManagerStreetName;
        public string ManagerStreetName { get => _ManagerStreetName; set { _ManagerStreetName = value; OnPropertyChanged(); } }

        private string _ManagerApartmentNumber;
        public string ManagerApartmentNumber { get => _ManagerApartmentNumber; set { _ManagerApartmentNumber = value; OnPropertyChanged(); } }

        private string _ManagerSelectedProvince;
        public string ManagerSelectedProvince { get => _ManagerSelectedProvince; set { _ManagerSelectedProvince = value; OnPropertyChanged(); InitManagerDistrictList(); } }

        private string _ManagerSelectedWard;
        public string ManagerSelectedWard { get => _ManagerSelectedWard; set { _ManagerSelectedWard = value; OnPropertyChanged(); } }

        private string _ManagerSelectedDistrict;
        public string ManagerSelectedDistrict { get => _ManagerSelectedDistrict; set { _ManagerSelectedDistrict = value; OnPropertyChanged(); InitManagerWardList(); } }

        private string _ManagerName;
        public string ManagerName { get => _ManagerName; set { _ManagerName = value; OnPropertyChanged(); } }

        private DateTime _ManagerDateOfBirth;
        public DateTime ManagerDateOfBirth { get => _ManagerDateOfBirth; set { _ManagerDateOfBirth = (DateTime)value; OnPropertyChanged(); } }

        private string _ManagerSex;
        public string ManagerSex { get => _ManagerSex; set { _ManagerSex = value; OnPropertyChanged(); } }

        private string _ManagerCitizenID;
        public string ManagerCitizenID { get => _ManagerCitizenID; set { _ManagerCitizenID = value; OnPropertyChanged(); } }

        private string _ManagerNationality;
        public string ManagerNationality { get => _ManagerNationality; set { _ManagerNationality = value; OnPropertyChanged(); } }

        private string _ManagerHealthInsuranceID;
        public string ManagerHealthInsuranceID { get => _ManagerHealthInsuranceID; set { _ManagerHealthInsuranceID = value; OnPropertyChanged(); } }

        private string _ManagerPhoneNumber;
        public string ManagerPhoneNumber { get => _ManagerPhoneNumber; set { _ManagerPhoneNumber = value; OnPropertyChanged(); } }

        private string _ManagerJobTitle;
        public string ManagerJobTitle { get => _ManagerJobTitle; set { _ManagerJobTitle = value; OnPropertyChanged(); } }

        private string _ManagerDepartment;
        public string ManagerDepartment { get => _ManagerDepartment; set { _ManagerDepartment = value; OnPropertyChanged(); } }

        #endregion

        #region UI
        public bool isDoneSetUp { get; set; }

        private Visibility _Tab1;
        private Visibility _Tab2;
        private Visibility _Tab3;
        private Visibility _Tab4;
        public Visibility Tab1
        {
            get => _Tab1; set
            {
                _Tab1 = value; OnPropertyChanged();
            }
        }
        public Visibility Tab2
        {
            get => _Tab2; set
            {
                _Tab2 = value; OnPropertyChanged();
            }
        }

        public Visibility Tab3
        {
            get => _Tab3; set
            {
                _Tab3 = value; OnPropertyChanged();
            }
        }
        public Visibility Tab4
        {
            get => _Tab4; set
            {
                _Tab4 = value; OnPropertyChanged();
            }
        }
        public int TabIndex { get; set; }

        private String _TabPostion;
        public String TabPosition
        {
            get => _TabPostion; set
            {
                _TabPostion = value; OnPropertyChanged();
            }
        }



        #endregion

        #region list
        // đang dummy data (đáng lẻ nên lấy data từ đâu đó)
        private ObservableCollection<string> _QAProvinceList;
        public ObservableCollection<string> QAProvinceList
        {
            get => _QAProvinceList; set
            {
                _QAProvinceList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _QADistrictList;
        public ObservableCollection<string> QADistrictList
        {
            get => _QADistrictList; set
            {
                _QADistrictList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _QAWardList;
        public ObservableCollection<string> QAWardList
        {
            get => _QAWardList; set
            {
                _QAWardList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _ManagerProvinceList;
        public ObservableCollection<string> ManagerProvinceList
        {
            get => _ManagerProvinceList; set
            {
                _ManagerProvinceList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _ManagerDistrictList;
        public ObservableCollection<string> ManagerDistrictList
        {
            get => _ManagerDistrictList; set
            {
                _ManagerDistrictList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _ManagerWardList;
        public ObservableCollection<string> ManagerWardList
        {
            get => _ManagerWardList; set
            {
                _ManagerWardList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _NationalityList;
        public ObservableCollection<string> NationalityList
        {
            get => _NationalityList; set
            {
                _NationalityList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _SexList;
        public ObservableCollection<string> SexList
        {
            get => _SexList; set
            {
                _SexList = value; OnPropertyChanged();
            }
        }

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

        private bool _MNameFieldHasError;
        public bool MNameFieldHasError { get => _MNameFieldHasError; set { _MNameFieldHasError = value; OnPropertyChanged(); } }

        private bool _MNationalityFieldHasError;
        public bool MNationalityFieldHasError { get => _MNationalityFieldHasError; set { _MNationalityFieldHasError = value; OnPropertyChanged(); } }

        private bool _MSexFieldHasError;
        public bool MSexFieldHasError { get => _MSexFieldHasError; set { _MSexFieldHasError = value; OnPropertyChanged(); } }

        private bool _MDateOfBirthFieldHasError;
        public bool MDateOfBirthFieldHasError { get => _MDateOfBirthFieldHasError; set { _MDateOfBirthFieldHasError = value; OnPropertyChanged(); } }

        private bool _MJobTitleFieldHasError;
        public bool MJobTitleFieldHasError { get => _MJobTitleFieldHasError; set { _MJobTitleFieldHasError = value; OnPropertyChanged(); } }

        private bool _MDepartmentFieldHasError;
        public bool MDepartmentFieldHasError { get => _MDepartmentFieldHasError; set { _MDepartmentFieldHasError = value; OnPropertyChanged(); } }

        private bool _MProvinceFieldHasError;
        public bool MProvinceFieldHasError { get => _MProvinceFieldHasError; set { _MProvinceFieldHasError = value; OnPropertyChanged(); } }

        private bool _MDistrictFieldHasError;
        public bool MDistrictFieldHasError { get => _MDistrictFieldHasError; set { _MDistrictFieldHasError = value; OnPropertyChanged(); } }

        private bool _MWardFieldHasError;
        public bool MWardFieldHasError { get => _MWardFieldHasError; set { _MWardFieldHasError = value; OnPropertyChanged(); } }

        private bool _MCitizenIDFieldHasError;
        public bool MCitizenIDFieldHasError { get => _MCitizenIDFieldHasError; set { _MCitizenIDFieldHasError = value; OnPropertyChanged(); } }

        private bool _MPhoneNumberFieldHasError;
        public bool MPhoneNumberFieldHasError { get => _MPhoneNumberFieldHasError; set { _MPhoneNumberFieldHasError = value; OnPropertyChanged(); } }

        #endregion

        #region command
        public ICommand NextTabCommand { get; set; }
        public ICommand PreviousTabCommand { get; set; }
        public ICommand SetUpCommand { get; set; }
        public ICommand EditCommand { get; set; }

        #endregion


        private MainViewModel _ParentVM;
        public MainViewModel ParentVM
        {
            get => _ParentVM;
            set
            {
                _ParentVM = value;
                OnPropertyChanged();
            }
        }

        #region singleton
        private static QuarantineAreaInformationViewModel _ins;
        public static QuarantineAreaInformationViewModel ins
        {
            get
            {
                if (_ins == null) _ins = new QuarantineAreaInformationViewModel();
                return _ins;
            }
            set => _ins = value;
        }
        #endregion


        #endregion

        public QuarantineAreaInformationViewModel()
        {
            isDoneSetUp = false;

            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
            Tab3 = Visibility.Hidden;
            Tab4 = Visibility.Hidden;
            TabIndex = 1;
            TabPosition = $"{TabIndex}/4";

            SetUpCommand = new RelayCommand<Window>((p) =>
            {
                if (!QANameFieldHasError && !QAProvinceFieldHasError && !QADistrictFieldHasError && !QAWardFieldHasError && !QATestCycleFieldHasError && !QARequiredDayFieldHasError
                 && !MNameFieldHasError && !MDateOfBirthFieldHasError && !MSexFieldHasError && !MNationalityFieldHasError && !MJobTitleFieldHasError && !MDepartmentFieldHasError
                 && !MProvinceFieldHasError && !MDistrictFieldHasError && !MWardFieldHasError && !MCitizenIDFieldHasError && !MPhoneNumberFieldHasError)
                    return true;
                return false;
            }, (p) =>
            {
                UpdateQuarantineAreaInformation();
                p.Close();
            });

            EditCommand = new RelayCommand<Window>((p) =>
            {
                if (!QANameFieldHasError && !QAProvinceFieldHasError && !QADistrictFieldHasError && !QAWardFieldHasError && !QATestCycleFieldHasError && !QARequiredDayFieldHasError
                 && !MNameFieldHasError && !MDateOfBirthFieldHasError && !MSexFieldHasError && !MNationalityFieldHasError && !MJobTitleFieldHasError && !MDepartmentFieldHasError
                 && !MProvinceFieldHasError && !MDistrictFieldHasError && !MWardFieldHasError && !MCitizenIDFieldHasError && !MPhoneNumberFieldHasError)
                    return true;
                return false;
            }, (p) =>
            {
                EditQuarantineAreaInformation();
                p.Close();
            });

            NextTabCommand = new RelayCommand<Window>((p) =>
            {
                if (TabIndex <= 4) return true;

                return false;
            }, (p) =>
            {
                HandleChangeTab(TabIndex, "next", p);
            });

            PreviousTabCommand = new RelayCommand<Window>((p) =>
            {
                if (TabIndex > 1) return true;
                return false;
            }, (p) =>
            {
                HandleChangeTab(TabIndex, "previous", p);
            });

            NationalityList = new ObservableCollection<string>();

            QAProvinceList = new ObservableCollection<string>();
            QADistrictList = new ObservableCollection<string>();
            QAWardList = new ObservableCollection<string>();


            ManagerProvinceList = new ObservableCollection<string>();
            ManagerDistrictList = new ObservableCollection<string>();
            ManagerWardList = new ObservableCollection<string>();

            SexList = new ObservableCollection<string>()
            {
                "Nam", "Nữ"
            };

            InitQAProvinceList();
            InitManagerProvinceList();
            InitNationList();
        }

        #region Methods

        void InitNationList()
        {
            foreach (var item in NationViewModel.NationList)
            {
                NationalityList.Add(item.NAME);
            }
        }

        void InitQAProvinceList()
        {
            foreach (var item in AddressViewModel.ProvinceList)
            {
                QAProvinceList.Add(item.name);
            }
        }

        void InitQADistrictList()
        {
            AddressViewModel.ProvinceSelectEvent(QASelectedProvince);
            QADistrictList.Clear();
            foreach (var item in AddressViewModel.DistrictList)
            {
                QADistrictList.Add(item.name);
            }
        }

        void InitQAWardList()
        {
            AddressViewModel.DistrictSelectEVent(QASelectedDistrict);
            QAWardList.Clear();
            foreach (var item in AddressViewModel.WardList)
            {
                QAWardList.Add(item.name);
            }
        }

        void InitManagerProvinceList()
        {
            foreach (var item in AddressViewModel.ProvinceList)
            {
                ManagerProvinceList.Add(item.name);
            }
        }

        void InitManagerDistrictList()
        {
            AddressViewModel.ProvinceSelectEvent(ManagerSelectedProvince);
            ManagerDistrictList.Clear();
            foreach (var item in AddressViewModel.DistrictList)
            {
                ManagerDistrictList.Add(item.name);
            }
        }

        void InitManagerWardList()
        {
            AddressViewModel.DistrictSelectEVent(ManagerSelectedDistrict);
            ManagerWardList.Clear();
            foreach (var item in AddressViewModel.WardList)
            {
                ManagerWardList.Add(item.name);
            }
        }

        void HandleChangeTab(int index, string action, Window p)
        {
            if (action == "next")
            {
                TabIndex++;
            }
            else
            {
                TabIndex--;
            }
            if (TabIndex <= 4)
                TabPosition = $"{TabIndex}/4";

            switch (TabIndex)
            {
                case 1:
                    Tab1 = Visibility.Visible;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Hidden;
                    Tab4 = Visibility.Hidden;
                    break;
                case 2:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Visible;
                    Tab3 = Visibility.Hidden;
                    Tab4 = Visibility.Hidden;
                    break;
                case 3:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Visible;
                    Tab4 = Visibility.Hidden;
                    break;
                case 4:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Hidden;
                    Tab4 = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        void UpdateQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    Address QAreaAddress = new Address()
                    {
                        province = QASelectedProvince,
                        district = QASelectedDistrict,
                        apartmentNumber = QAApartmentNumber,
                        streetName = QAStreetName,
                        ward = QASelectedWard
                    };

                    DataProvider.ins.db.Addresses.Add(QAreaAddress);
                    DataProvider.ins.db.SaveChanges();

                    Address ManagerAddress = new Address()
                    {
                        province = ManagerSelectedProvince,
                        district = ManagerSelectedDistrict,
                        apartmentNumber = ManagerApartmentNumber,
                        streetName = ManagerStreetName,
                        ward = ManagerSelectedWard
                    };

                    DataProvider.ins.db.Addresses.Add(ManagerAddress);
                    DataProvider.ins.db.SaveChanges();

                    Staff Manager = new Staff()
                    {
                        addressID = ManagerAddress.id,
                        citizenID = ManagerCitizenID,
                        dateOfBirth = (DateTime)ManagerDateOfBirth,
                        department = ManagerDepartment,
                        healthInsuranceID = ManagerHealthInsuranceID,
                        jobTitle = ManagerJobTitle,
                        name = ManagerName,
                        nationality = ManagerNationality,
                        phoneNumber = ManagerPhoneNumber,
                        sex = ManagerSex,
                    };

                    DataProvider.ins.db.Staffs.Add(Manager);
                    DataProvider.ins.db.SaveChanges();

                    QuarantineArea QuarantineArea = new QuarantineArea()
                    {
                        addressID = QAreaAddress.id,
                        name = QAname,
                        testCycle = QATestCycle,
                        requiredDayToFinish = QARequiredDayToFinish,
                        managerID = Manager.id,
                        googleFormURL = "https://docs.google.com/forms/d/1GF15ibJBQ0XpSc6lZproYXH7zuF1oXCVDoZyLWfh4TQ/edit?usp=drivesdk",
                        googleSheetURL = "https://docs.google.com/spreadsheets/d/1R6zuZB_xFuzWrCnl4j0JLZ3da5HtprRrmjeQ3LdxW44/edit?usp=drivesdk",
                    };

                    DataProvider.ins.db.QuarantineAreas.Add(QuarantineArea);
                    DataProvider.ins.db.SaveChanges();

                    isDoneSetUp = true;

                    transaction.Commit();

                    ParentVM.Init();
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

        void EditQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var QuarantineArea = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
                    if (QuarantineArea == null) return;

                    var Address = DataProvider.ins.db.Addresses.Where(x => x.id == QuarantineArea.addressID).FirstOrDefault();
                    if (Address == null) return;

                    QuarantineArea.name = QAname;
                    QuarantineArea.testCycle = QATestCycle;
                    QuarantineArea.requiredDayToFinish = QARequiredDayToFinish;

                    Address.apartmentNumber = QAApartmentNumber;
                    Address.streetName = QAStreetName;
                    Address.ward = QASelectedWard;
                    Address.district = QASelectedDistrict;
                    Address.province = QASelectedProvince;

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

        #endregion
    }
}
