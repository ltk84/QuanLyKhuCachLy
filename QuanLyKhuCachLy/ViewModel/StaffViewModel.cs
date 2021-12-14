
using QuanLyKhuCachLy.Model;
using QuanLyKhuCachLy.CustomUserControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Media;
using System.Data.Entity;

namespace QuanLyKhuCachLy.ViewModel
{
    public class StaffViewModel : BaseViewModel
    {

        #region UI
        bool isAdding = true;
        EditStaffScreen editStaffScreen;

        AddStaffScreen addStaffScreen;
        private Visibility _AddStaffTab1;
        private Visibility _AddStaffTab2;
        private Visibility _AddStaffPreviousBtn;
        private Visibility _TabList;
        private Visibility _TabInformation;

        public Visibility TabList
        {
            get => _TabList;
            set
            {
                _TabList = value;
                OnPropertyChanged();
            }
        }

        public Visibility TabInformation
        {
            get => _TabInformation;
            set
            {
                _TabInformation = value;
                OnPropertyChanged();
            }
        }

        public Visibility AddStaffTab1
        {
            get => _AddStaffTab1;
            set
            {
                _AddStaffTab1 = value;
                OnPropertyChanged();
            }
        }

        public Visibility AddStaffPreviousBtn
        {
            get => _AddStaffPreviousBtn;
            set
            {
                _AddStaffPreviousBtn = value;
                OnPropertyChanged();
            }
        }

        public Visibility AddStaffTab2
        {
            get => _AddStaffTab2;
            set
            {
                _AddStaffTab2 = value;
                OnPropertyChanged();
            }
        }

        private String _TabPostion;
        public String TabPosition
        {
            get => _TabPostion; set
            {
                _TabPostion = value; OnPropertyChanged();
            }
        }


        public int AddStaffTabIndex { get; set; }


        private Visibility _Tab1;
        public Visibility Tab1
        {
            get => _Tab1; set
            {
                _Tab1 = value; OnPropertyChanged();
            }
        }

        private Visibility _Tab2;
        public Visibility Tab2
        {
            get => _Tab2; set
            {
                _Tab2 = value; OnPropertyChanged();
            }
        }
        #endregion

        #region property


        private string _Name;
        public string Name
        {
            get => _Name; set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        private string _SearchKey;
        public string SearchKey
        {
            get => _SearchKey; set
            {
                _SearchKey = value;
                OnPropertyChanged();
                FilterListView();
            }
        }

        private DateTime _DateOfBirth;
        public DateTime DateOfBirth
        {
            get => _DateOfBirth; set
            {
                _DateOfBirth = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedSex;
        public string SelectedSex
        {
            get => _SelectedSex; set
            {
                _SelectedSex = value;
                OnPropertyChanged();
            }
        }

        private string _CitizenID;
        public string CitizenID
        {
            get => _CitizenID; set
            {
                _CitizenID = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedNationality;
        public string SelectedNationality
        {
            get => _SelectedNationality; set
            {
                _SelectedNationality = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedFilterType;
        public string SelectedFilterType
        {
            get => _SelectedFilterType; set
            {
                _SelectedFilterType = value;
                OnPropertyChanged();
                getFilterProperty();
            }
        }

        private String _SelectedFilterProperty;
        public String SelectedFilterProperty
        {
            get => _SelectedFilterProperty; set
            {
                _SelectedFilterProperty = value;
                OnPropertyChanged();
                SelectFilterProperty();
                SearchKey = "";
            }
        }



        private string _HealthInsuranceID;
        public string HealthInsuranceID
        {
            get => _HealthInsuranceID; set
            {
                _HealthInsuranceID = value;
                OnPropertyChanged();
            }
        }

        private string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber; set
            {
                _PhoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _JobTitle;
        public string JobTitle
        {
            get => _JobTitle; set
            {
                _JobTitle = value;
                OnPropertyChanged();
            }
        }

        private string _Department;
        public string Department
        {
            get => _Department; set
            {
                _Department = value;
                OnPropertyChanged();
            }
        }

        #region address
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

        private string _DisplayAdress;
        public string DisplayAddress { get => _DisplayAdress; set { _DisplayAdress = value; OnPropertyChanged(); } }
        #endregion

        #region List

        private Staff _SelectedItem;
        public Staff SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SetSelectedItemToProperty();
                }
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

        private Staff[] _StaffListView;
        public Staff[] StaffListView
        {
            get => _StaffListView; set
            {
                _StaffListView = value; OnPropertyChanged();
            }
        }


        private string[] _FilterType;
        public string[] FilterType
        {
            get => _FilterType; set
            {
                _FilterType = value; OnPropertyChanged();
            }
        }

        private string[] _FilterProperty;
        public string[] FilterProperty
        {
            get => _FilterProperty; set
            {
                _FilterProperty = value; OnPropertyChanged();
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


        #region command
        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand ToAddManualCommand { get; set; }
        public ICommand ToAddExcelCommand { get; set; }
        public ICommand ToEditCommand { get; set; }
        public ICommand ToViewCommand { get; set; }
        public ICommand ToMainCommand { get; set; }

        public ICommand NextStaffTabCommand { get; set; }
        public ICommand PreviousStaffTabCommand { get; set; }
        public ICommand CancelAddStaffTabCommand { get; set; }
        public ICommand RefeshCommand { get; set; }
        public ICommand ToExportExcel { get; set; }

        public ICommand ToGetFormatExcel { get; set; }
        #endregion

        #region validation
        private bool _NameFieldHasError;
        public bool NameFieldHasError
        {
            get => _NameFieldHasError; set
            {
                _NameFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _SexFieldHasError;
        public bool SexFieldHasError
        {
            get => _SexFieldHasError; set
            {
                _SexFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _NationalityFieldHasError;
        public bool NationalityFieldHasError
        {
            get => _NationalityFieldHasError; set
            {
                _NationalityFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _ProvinceFieldHasError;
        public bool ProvinceFieldHasError
        {
            get => _ProvinceFieldHasError; set
            {
                _ProvinceFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _DistrictFieldHasError;
        public bool DistrictFieldHasError
        {
            get => _DistrictFieldHasError; set
            {
                _DistrictFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _WardFieldHasError;
        public bool WardFieldHasError
        {
            get => _WardFieldHasError; set
            {
                _WardFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _CitizenIDFieldHasError;
        public bool CitizenIDFieldHasError
        {
            get => _CitizenIDFieldHasError; set
            {
                _CitizenIDFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _PhoneNumberFieldHasError;
        public bool PhoneNumberFieldHasError
        {
            get => _PhoneNumberFieldHasError; set
            {
                _PhoneNumberFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _DateOfBirthFieldHasError;
        public bool DateOfBirthFieldHasError
        {
            get => _DateOfBirthFieldHasError; set
            {
                _DateOfBirthFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _JobTitleFieldHasError;
        public bool JobTitleFieldHasError
        {
            get => _JobTitleFieldHasError; set
            {
                _JobTitleFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _DepartmentFieldHasError;
        public bool DepartmentFieldHasError
        {
            get => _DepartmentFieldHasError; set
            {
                _DepartmentFieldHasError = value; OnPropertyChanged();
            }
        }


        #endregion

        #endregion
        public StaffViewModel()
        {
            SetDefaultUI();

            SetDefaultAddStaff();

            InitBasic();

            NationalityList = new ObservableCollection<string>();
            ProvinceList = new ObservableCollection<string>();
            DistrictList = new ObservableCollection<string>();
            WardList = new ObservableCollection<string>();

            InitNationList();

            InitProvinceList();

            SexList = new ObservableCollection<string>()
            {
                "Nam", "Nữ"
            };

            ToExportExcel = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ExportExcel();
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (!NameFieldHasError && !SexFieldHasError && !DateOfBirthFieldHasError && !CitizenIDFieldHasError && !NationalityFieldHasError && !PhoneNumberFieldHasError
                && !JobTitleFieldHasError && !DepartmentFieldHasError && !ProvinceFieldHasError && !DistrictFieldHasError && !WardFieldHasError)
                    return true;
                return false;
            }, (p) =>
            {
                AddStaff();
                CloseAddStaffWindown();
                SetDefaultAddStaff();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (!NameFieldHasError && !SexFieldHasError && !DateOfBirthFieldHasError && !CitizenIDFieldHasError && !NationalityFieldHasError && !PhoneNumberFieldHasError
                && !JobTitleFieldHasError && !DepartmentFieldHasError && !ProvinceFieldHasError && !DistrictFieldHasError && !WardFieldHasError)
                    return true;
                return false;
            }, (p) =>
            {
                EditStaff();
                editStaffScreen.Close();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                return false;
            }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                DeleteConfirmation confirmation = new DeleteConfirmation();
                if (confirmation.ShowDialog() == true)
                {
                    DeleteStaff();
                    BackToStaffList();
                }
            });

            ToAddManualCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

                ClearData();
                SetDefaultAddStaff();
                isAdding = true;
                addStaffScreen = new AddStaffScreen();
                addStaffScreen.ShowDialog();
                ClearData();

            });

            ToGetFormatExcel = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

                GetFormatExcel();

            });

            ToAddExcelCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddStaffFromExcel();
            });

            ToEditCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                isAdding = false;
                SetDefaultAddStaff();
                SetSelectedItemToProperty();
                editStaffScreen = new EditStaffScreen();
                editStaffScreen.ShowDialog();
            });


            SearchCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Console.WriteLine(SearchKey);
            });

            ToViewCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                ToDetailStaffTab();
            });

            ToMainCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                BackToStaffList();
            });

            CancelAddStaffTabCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                CloseAddStaffWindown();
            });

            PreviousStaffTabCommand = new RelayCommand<Window>((p) =>
            {
                if (AddStaffTabIndex > 1) return true;
                return false;
            }, (p) =>
            {
                HandleChangeTab(AddStaffTabIndex, "previous", addStaffScreen);
            });

            NextStaffTabCommand = new RelayCommand<Window>((p) =>
            {

                if (AddStaffTabIndex < 2) return true;

                return false;
            }, (p) =>
            {
                HandleChangeTab(AddStaffTabIndex, "next", addStaffScreen);
            });

            RefeshCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                RefeshTab();
            });

        }

        #region method

        void RefeshTab()
        {
            SetDefaultUI();
            SelectedItem = null;
            InitBasic();
        }

        void InitBasic()
        {
            StaffList = new ObservableCollection<Staff>(DataProvider.ins.db.Staffs);
            StaffListView = StaffList.ToArray();

            FilterType = new string[] { "Tất cả", "Giới tính", "Quốc tịch", "Chức vụ", "Bộ phận" };
            SelectedFilterType = "Tất cả";
            SelectedFilterProperty = "Chọn phương thức lọc";
            getFilterProperty();
        }

        void SetDefaultUI()
        {
            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
            TabList = Visibility.Visible;
            TabInformation = Visibility.Hidden;
        }

        void InitNationList()
        {
            foreach (var item in NationViewModel.NationList)
            {
                NationalityList.Add(item.NAME);
            }
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

        void BackToStaffList()
        {
            TabInformation = Visibility.Hidden;
            TabList = Visibility.Visible;
        }

        void ToDetailStaffTab()
        {
            TabInformation = Visibility.Visible;
            TabList = Visibility.Hidden;
        }

        void HandleChangeTab(int index, string action, Window p)
        {
            if (action == "next")
            {
                AddStaffTabIndex++;
            }
            else
            {
                AddStaffTabIndex--;
            }
            if (AddStaffTabIndex <= 2)
                TabPosition = $"{AddStaffTabIndex}/2";

            switch (AddStaffTabIndex)
            {
                case 1:
                    AddStaffTab1 = Visibility.Visible;
                    AddStaffTab2 = Visibility.Hidden;
                    AddStaffPreviousBtn = Visibility.Collapsed;
                    break;
                case 2:
                    AddStaffTab1 = Visibility.Hidden;
                    AddStaffPreviousBtn = Visibility.Visible;
                    AddStaffTab2 = Visibility.Visible;
                    break;
                default:

                    break;
            }

        }

        void CloseAddStaffWindown()
        {
            if (isAdding)
            {
                addStaffScreen.Close();

            }
            else
            {
                editStaffScreen.Close();
            }
        }

        void InitDisplayAddress(Address StaffAddress)
        {
            if (StaffAddress == null) return;
            List<string> list = new List<string>()
            {
                StaffAddress.apartmentNumber,
                StaffAddress.streetName,
                StaffAddress.ward,
                StaffAddress.district,
                StaffAddress.province

            };

            DisplayAddress = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(list[i]))
                {
                    DisplayAddress += list[i];
                }
                if (i != list.Count - 1)
                {
                    if (i != 0)
                        DisplayAddress += ", ";
                    else DisplayAddress += " ";
                }
            }
        }

        void ClearData()
        {
            Name = string.Empty;
            SelectedSex = string.Empty;
            DateOfBirth = DateTime.MinValue;
            CitizenID = string.Empty;
            SelectedNationality = string.Empty;
            PhoneNumber = string.Empty;
            HealthInsuranceID = string.Empty;
            Department = string.Empty;
            JobTitle = string.Empty;
            SelectedProvince = string.Empty;
            SelectedDistrict = string.Empty;
            SelectedWard = string.Empty;
            StreetName = string.Empty;
            ApartmentNumber = string.Empty;
        }

        void SetDefaultAddStaff()
        {
            AddStaffTabIndex = 1;
            AddStaffTab1 = Visibility.Visible;
            AddStaffTab2 = Visibility.Hidden;
            TabPosition = "1/2";
            AddStaffPreviousBtn = Visibility.Collapsed;
        }

        void AddStaff()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    Address StaffAddress = new Address()
                    {
                        province = SelectedProvince,
                        district = SelectedDistrict,
                        apartmentNumber = ApartmentNumber,
                        streetName = StreetName,
                        ward = SelectedWard
                    };

                    DataProvider.ins.db.Addresses.Add(StaffAddress);
                    DataProvider.ins.db.SaveChanges();

                    Staff NewStaff = new Staff()
                    {
                        addressID = StaffAddress.id,
                        citizenID = CitizenID,
                        dateOfBirth = DateOfBirth,
                        department = Department,
                        healthInsuranceID = HealthInsuranceID,
                        jobTitle = JobTitle,
                        name = Name,
                        nationality = SelectedNationality,
                        phoneNumber = PhoneNumber,
                        sex = SelectedSex,
                    };

                    DataProvider.ins.db.Staffs.Add(NewStaff);
                    DataProvider.ins.db.SaveChanges();

                    StaffList.Add(NewStaff);
                    StaffListView = StaffList.ToArray();
                    RollBackChange();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }
        void EditStaff()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    Staff SelectStaff = DataProvider.ins.db.Staffs.Where(x => x.id == SelectedItem.id).FirstOrDefault();

                    Address StaffAdress = DataProvider.ins.db.Addresses.Where(x => x.id == SelectStaff.addressID).FirstOrDefault();
                    StaffAdress.province = SelectedProvince;
                    StaffAdress.district = SelectedDistrict;
                    StaffAdress.ward = SelectedWard;
                    StaffAdress.streetName = StreetName;
                    StaffAdress.apartmentNumber = ApartmentNumber;

                    SelectStaff.name = Name;
                    SelectStaff.nationality = SelectedNationality;
                    SelectStaff.phoneNumber = PhoneNumber;
                    SelectStaff.sex = SelectedSex;
                    SelectStaff.healthInsuranceID = HealthInsuranceID;
                    SelectStaff.department = Department;
                    SelectStaff.dateOfBirth = DateOfBirth;
                    SelectStaff.citizenID = CitizenID;
                    SelectStaff.addressID = StaffAdress.id;
                    SelectStaff.jobTitle = JobTitle;
                    SelectStaff.department = Department;
                    InitDisplayAddress(StaffAdress);

                    DataProvider.ins.db.SaveChanges();

                    StaffListView = new ObservableCollection<Model.Staff>(DataProvider.ins.db.Staffs).ToArray();

                    transaction.Commit();

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }
        void DeleteStaff()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    Staff staff = DataProvider.ins.db.Staffs.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (staff == null) return;

                    DataProvider.ins.db.Staffs.Remove(staff);
                    DataProvider.ins.db.SaveChanges();

                    StaffList.Remove(staff);

                    StaffListView = new ObservableCollection<Model.Staff>(DataProvider.ins.db.Staffs).ToArray();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }


        void SetSelectedItemToProperty()
        {
            Name = SelectedItem.name;
            DateOfBirth = SelectedItem.dateOfBirth;
            SelectedSex = SelectedItem.sex;
            CitizenID = SelectedItem.citizenID;
            SelectedNationality = SelectedItem.nationality;
            HealthInsuranceID = SelectedItem.healthInsuranceID;
            PhoneNumber = SelectedItem.phoneNumber;
            JobTitle = SelectedItem.jobTitle;
            Department = SelectedItem.department;

            Address StaffAdress = DataProvider.ins.db.Addresses.Where(x => x.id == SelectedItem.addressID).FirstOrDefault();

            SelectedProvince = StaffAdress.province;
            SelectedDistrict = StaffAdress.district;
            SelectedWard = StaffAdress.ward;
            StreetName = StaffAdress.streetName;
            ApartmentNumber = StaffAdress.apartmentNumber;


            InitDisplayAddress(StaffAdress);
        }

        void RollBackChange()
        {
            DataProvider.ins.db.ChangeTracker.Entries().Where(ex => ex.Entity != null).ToList().ForEach(ex => ex.State = EntityState.Detached);
            StaffList = new ObservableCollection<Staff>(DataProvider.ins.db.Staffs);
            SelectedItem = null;
        }


        void FilterListView()
        {
            SelectFilterProperty();

            if (SearchKey == "" || SearchKey == null)
            {
            }
            else
            {


                String[] Value = new string[StaffListView.Length];

                for (int i = 0; i < StaffListView.Length; i++)
                {
                    Value[i] = StaffListView[i].name?.ToString() + "@@" + StaffListView[i].citizenID?.ToString() + "@@" + StaffListView[i].id.ToString() + "@@" + StaffListView[i].healthInsuranceID?.ToString() + "@@" + StaffListView[i]?.phoneNumber.ToString() ;

                }

                StaffListView = StaffListView.Where((val, index) => Value[index].ToUpper().Contains(SearchKey.ToUpper())).ToArray();


            }
        }


        void getFilterProperty()
        {
            SelectedFilterProperty = "";
            SearchKey = "";
            //FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.GetType().GetProperty(SelectedFilterType)).Distinct();
            if (SelectedFilterType == "Tất cả")
            {
                FilterProperty = new string[] { };
            }
            else if (SelectedFilterType == "Giới tính")
            {
                FilterProperty = new string[] { "Nam", "Nữ" };
                SelectedFilterProperty = "Tất cả";
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                FilterProperty = StaffList.Select(person => person.nationality).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Chức vụ")
            {
                FilterProperty = StaffList.Select(staff => staff.jobTitle).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Bộ phận")
            {
                FilterProperty = StaffList.Select(staff => staff.department).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }

            StaffListView = StaffList.ToArray();
        }

        void SelectFilterProperty()
        {
            StaffListView = StaffList.ToArray();
            if (SelectedFilterProperty == "" || SelectedFilterProperty == null || SelectedFilterProperty == "Tất cả") return;
            if (SelectedFilterType == "Tất cả")
            {
            }
            else if (SelectedFilterType == "Giới tính")
            {
                StaffListView = StaffList.Where(x => x.sex == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                StaffListView = StaffList.Where(x => x.nationality == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Chức vụ")
            {

                StaffListView = StaffList.Where(x => x.jobTitle == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Bộ phận")
            {

                StaffListView = StaffList.Where(x => x.department == SelectedFilterProperty).ToArray();
            }


        }

        //String RemoveSign4VietnameseString(String str)
        //{
        //    String[] VietnameseSigns = new String[]
        //        {

        //            "aAeEoOuUiIdDyY",

        //            "áàạảãâấầậẩẫăắằặẳẵ",

        //            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        //            "éèẹẻẽêếềệểễ",

        //            "ÉÈẸẺẼÊẾỀỆỂỄ",

        //            "óòọỏõôốồộổỗơớờợởỡ",

        //            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        //            "úùụủũưứừựửữ",

        //            "ÚÙỤỦŨƯỨỪỰỬỮ",

        //            "íìịỉĩ",

        //            "ÍÌỊỈĨ",

        //            "đ",

        //            "Đ",

        //            "ýỳỵỷỹ",

        //            "ÝỲỴỶỸ"
        //        };
        //    for (int i = 1; i < VietnameseSigns.Length; i++)
        //    {
        //        for (int j = 0; j < VietnameseSigns[i].Length; j++)
        //            str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
        //    }
        //    return str;
        //}

        async void AddStaffFromExcel()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx;*.xlsm;*xls)|*.xlsx;*.xlsm;*xls|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            else
                return;
            LoadingIndicator loadingIndicator = new LoadingIndicator();
            Task task = ExecuteAddStaffFromExcel(loadingIndicator, path);
            loadingIndicator.ShowDialog();
            await task;
        }

        async Task ExecuteAddStaffFromExcel(LoadingIndicator loadingIndicator, string path)
        {
            bool isSuccess = false;
            string error = "";
            await Task.Run(() =>
            {
                List<Address> listStaffAddress = new List<Address>();
                List<Staff> listStaff = new List<Staff>();
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
                xlWorkbook.RefreshAll();
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;
                if (xlRange.Cells[1, 1] == null || xlRange.Cells[1, 1].Value2.ToString().Trim().ToLower() != "stt" ||
                xlRange.Cells[1, 2] == null || xlRange.Cells[1, 2].Value2.ToString().Trim().ToLower() != "họ và tên" ||
                xlRange.Cells[1, 3] == null || xlRange.Cells[1, 3].Value2.ToString().Trim().ToLower() != "ngày sinh" ||
                xlRange.Cells[1, 4] == null || xlRange.Cells[1, 4].Value2.ToString().Trim().ToLower() != "giới tính" ||
                xlRange.Cells[1, 5] == null || xlRange.Cells[1, 5].Value2.ToString().Trim().ToLower() != "cmnd/cccd" ||
                xlRange.Cells[1, 6] == null || xlRange.Cells[1, 6].Value2.ToString().Trim().ToLower() != "quốc tịch" ||
                xlRange.Cells[1, 7] == null || xlRange.Cells[1, 7].Value2.ToString().Trim().ToLower() != "sđt" ||
                xlRange.Cells[1, 8] == null || xlRange.Cells[1, 8].Value2.ToString().Trim().ToLower() != "mabh" ||
                xlRange.Cells[1, 9] == null || xlRange.Cells[1, 9].Value2.ToString().Trim().ToLower() != "chức vụ" ||
                xlRange.Cells[1, 10] == null || xlRange.Cells[1, 10].Value2.ToString().Trim().ToLower() != "phòng ban" ||
                xlRange.Cells[1, 11] == null || xlRange.Cells[1, 11].Value2.ToString().Trim().ToLower() != "địa chỉ")
                {
                    xlWorkbook.Close();
                    error = "Không đúng định dạng file";
                    return;
                }
                List<int> listSTT = new List<int>();
                for (int i = 2; i <= rowCount; i++)
                {
                    Staff staff = new Staff();
                    Address address = new Address();
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                    {
                        int t;

                        if (Int32.TryParse(xlRange.Cells[i, 1].Value2.ToString(), out t))
                        {
                            if (t <= 0)
                            {
                                xlWorkbook.Close();
                                error = "STT để bé hơn 1";
                                return;
                            }
                            else
                            {
                                if (listSTT.Contains(t))
                                {
                                    xlWorkbook.Close();
                                    error = "STT " + t.ToString() + " bị trùng";
                                    return;
                                }
                                listSTT.Add(t);
                            }
                        }
                        else
                        {
                            xlWorkbook.Close();
                            error = "STT không là số";
                            return;
                        }
                    }
                    else
                    {
                        error = "STT trống ";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                    {
                        staff.name = xlRange.Cells[i, 2].Value2.ToString();
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " tên để trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                    {
                        DateTime birth;
                        double date;
                        if (double.TryParse(xlRange.Cells[i, 3].Value2.ToString(), out date))
                        {
                            birth = DateTime.FromOADate(double.Parse(xlRange.Cells[i, 3].Value2.ToString()));
                            staff.dateOfBirth = birth;
                        }
                        else
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " sai ngày sinh";
                            xlWorkbook.Close();
                            return;
                        }
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " ngày sinh trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null)
                    {
                        string sex = xlRange.Cells[i, 4].Value2.ToString().ToLower();
                        if (sex != "nam" && sex != "nữ")
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " giới tính không đúng (chỉ là Nam/Nữ)";
                            xlWorkbook.Close();
                            return;
                        }
                        staff.sex = (sex == "nữ" ? "Nữ" : "Nam");
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " giới tính để trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 11] != null && xlRange.Cells[i, 11].Value2 != null)
                    {
                        string[] arrListStr = xlRange.Cells[i, 11].Value2.ToString().Split(',');
                        if (arrListStr.Length < 3)
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " sai địa chỉ";
                            xlWorkbook.Close();
                            return;
                        }
                        if (arrListStr.Length == 3)
                        {
                            address.province = arrListStr[2].Trim();
                            address.district = arrListStr[1].Trim();
                            address.ward = arrListStr[0].Trim();
                        }
                        else
                        {
                            address.province = arrListStr[3].Trim();
                            address.district = arrListStr[2].Trim();
                            address.ward = arrListStr[1].Trim();
                            address.streetName = arrListStr[0].Trim();
                        }
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " địa chỉ trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 5] != null && xlRange.Cells[i, 5].Value2 != null)
                    {
                        staff.citizenID = xlRange.Cells[i, 5].Value2.ToString();
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " CMND/CCCD trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 8] != null && xlRange.Cells[i, 8].Value2 != null)
                    {
                        staff.healthInsuranceID = xlRange.Cells[i, 8].Value2.ToString();
                    }
                    if (xlRange.Cells[i, 6] != null && xlRange.Cells[i, 6].Value2 != null)
                    {
                        staff.nationality = xlRange.Cells[i, 6].Value2.ToString();
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " quốc tịch trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 7] != null && xlRange.Cells[i, 7].Value2 != null)
                    {
                        int t;
                        if (Int32.TryParse(xlRange.Cells[i, 7].Value2.ToString(), out t))
                        {
                            if(xlRange.Cells[i, 7].Value2.ToString()[0] == '0')
                            {
                                if (xlRange.Cells[i, 7].Value2.ToString().Length <= 10)
                                    staff.phoneNumber = xlRange.Cells[i, 7].Value2.ToString();
                                else
                                {
                                    error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " số điện thoại không đúng";
                                    xlWorkbook.Close();
                                    return;
                                }
                            }
                            else
                            {
                                if (xlRange.Cells[i, 7].Value2.ToString().Length <= 9)
                                    staff.phoneNumber = "0"+xlRange.Cells[i, 7].Value2.ToString();
                                else
                                {
                                    error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " số điện thoại không đúng";
                                    xlWorkbook.Close();
                                    return;
                                }
                            }

                        }
                        else
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " lỗi số điện thoại";
                            xlWorkbook.Close();
                            return;
                        }

                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " số điện thoại trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 9] != null && xlRange.Cells[i, 9].Value2 != null)
                    {
                        staff.jobTitle = xlRange.Cells[i, 9].Value2.ToString();
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " chức vụ trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 10] != null && xlRange.Cells[i, 10].Value2 != null)
                    {
                        staff.department = xlRange.Cells[i, 10].Value2.ToString();
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " phòng ban trống";
                        xlWorkbook.Close();
                        return;
                    }
                    listStaff.Add(staff);
                    listStaffAddress.Add(address);
                }
                xlWorkbook.Close();
                using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
                {
                    try
                    {
                        for (int i = 0; i < listStaff.Count; i++)
                        {
                            DataProvider.ins.db.Addresses.Add(listStaffAddress[i]);
                            DataProvider.ins.db.SaveChanges();
                            listStaff[i].addressID = listStaffAddress[i].id;
                            DataProvider.ins.db.Staffs.Add(listStaff[i]);
                            DataProvider.ins.db.SaveChanges();
                            StaffList.Add(listStaff[i]);
                        }
                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (DbUpdateException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        error = "Lỗi cơ sở dữ liệu cập nhật";
                    }
                    catch (DbEntityValidationException e)
                    {
                        transaction.Rollback();
                        RollBackChange();


                        error = "Lỗi xác thực";

                    }
                    catch (NotSupportedException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        error = "Lỗi database không hỗ trợ";

                    }
                    catch (ObjectDisposedException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        error = "Lỗi đối tượng database bị hủy";
                    }
                    catch (InvalidOperationException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        error = "Lỗi thao tác không hợp lệ";

                    }
                }
                StaffListView = new ObservableCollection<Model.Staff>(DataProvider.ins.db.Staffs).ToArray();
            });
            loadingIndicator.Close();
            if (isSuccess)
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
                SuccessDialog.ShowDialog();
            }
            else
            {
                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                if (error != "" && error != null)
                {
                    FailNotificationVM.Content = error;
                }
                ErrorDialog.ShowDialog();
            }
        }
        void ExportExcel()
        {
            int count = StaffListView.Length;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            Microsoft.Office.Interop.Excel.Workbook file = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet sheet = file.Worksheets[1];
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 25;
            sheet.Columns[3].ColumnWidth = 12;
            sheet.Columns[4].ColumnWidth = 9;
            sheet.Columns[5].ColumnWidth = 50;
            sheet.Columns[6].ColumnWidth = 12;
            sheet.Columns[7].ColumnWidth = 12;
            sheet.Columns[8].ColumnWidth = 10;
            sheet.Columns[9].ColumnWidth = 12;
            sheet.Columns[10].ColumnWidth = 12;
            sheet.Columns[11].ColumnWidth = 12;
            sheet.Range["A1"].Value = "STT";
            sheet.Range["B1"].Value = "Họ và tên";
            sheet.Range["C1"].Value = "Ngày sinh";
            sheet.Range["D1"].Value = "Giới tính";
            sheet.Range["E1"].Value = "Địa chỉ";
            sheet.Range["F1"].Value = "MaBH";
            sheet.Range["G1"].Value = "CMND/CCCD";
            sheet.Range["H1"].Value = "Quốc tịch";
            sheet.Range["I1"].Value = "SĐT";
            sheet.Range["J1"].Value = "Chức vụ";
            sheet.Range["K1"].Value = "Bộ phận";

            for (int i = 2; i <= count + 1; i++)
            {
                int addressID = StaffListView[i - 2].addressID;
                Address address = DataProvider.ins.db.Addresses.Where(x => x.id == addressID).FirstOrDefault();
                String personAddress = "";
                if (address.apartmentNumber != null)
                    personAddress += address.apartmentNumber.ToString();
                if (address.streetName != null)
                    personAddress += " " + address.streetName.ToString();
                if (address.ward != null)
                    personAddress += ", " + address.ward.ToString();
                if (address.district != null)
                    personAddress += ", " + address.district.ToString();
                if (address.province != null)
                    personAddress += ", " + address.province.ToString();
                sheet.Range["A" + i.ToString()].Value = (i - 1).ToString();
                sheet.Range["B" + i.ToString()].Value = StaffListView[i - 2].name;
                sheet.Range["C" + i.ToString()].Value = StaffListView[i - 2].dateOfBirth;
                sheet.Range["D" + i.ToString()].Value = StaffListView[i - 2].sex;
                sheet.Range["E" + i.ToString()].Value = personAddress;
                sheet.Range["F" + i.ToString()].Value = StaffListView[i - 2].healthInsuranceID;
                sheet.Range["G" + i.ToString()].Value = StaffListView[i - 2].citizenID;
                sheet.Range["H" + i.ToString()].Value = StaffListView[i - 2].nationality;
                sheet.Range["I" + i.ToString()].Value = StaffListView[i - 2].phoneNumber;
                sheet.Range["J" + i.ToString()].Value = StaffListView[i - 2].jobTitle;
                sheet.Range["K" + i.ToString()].Value = StaffListView[i - 2].department;
            }
            file.Close();
        }
        void GetFormatExcel()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            Microsoft.Office.Interop.Excel.Workbook file = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet sheet = file.Worksheets[1];
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 25;
            sheet.Columns[3].ColumnWidth = 12;
            sheet.Columns[4].ColumnWidth = 9;
            sheet.Columns[5].ColumnWidth = 12;
            sheet.Columns[6].ColumnWidth = 12;
            sheet.Columns[7].ColumnWidth = 12;
            sheet.Columns[8].ColumnWidth = 10;
            sheet.Columns[9].ColumnWidth = 12;
            sheet.Columns[10].ColumnWidth = 12;
            sheet.Columns[11].ColumnWidth = 30;
            sheet.Columns[12].ColumnWidth = 50;
            sheet.Range["A1"].Value = "STT";
            sheet.Range["B1"].Value = "Họ và tên";
            sheet.Range["C1"].Value = "Ngày sinh";
            sheet.Range["D1"].Value = "Giới tính";
            sheet.Range["E1"].Value = "CMND/CCCD";
            sheet.Range["F1"].Value = "Quốc tịch";
            sheet.Range["G1"].Value = "SĐT";
            sheet.Range["H1"].Value = "MaBH";
            sheet.Range["I1"].Value = "Chức vụ";
            sheet.Range["J1"].Value = "Phòng ban";
            sheet.Range["K1"].Value = "Địa chỉ";

            sheet.Range["L2"].Value = "Lưu ý:";
            sheet.Range["L3"].Value = "Các thảnh phần của điệm điểm cách nhau bởi dấu ',' ";
            sheet.Range["L4"].Value = "các từ chỉ địa phương ghi hoa chữ đầu.";
            sheet.Range["L5"].Value = "VD: Thôn A, Xã B, Huyện C, Tỉnh D";
            sheet.Range["L6"].Value = "MaBH có thể để trống";
            sheet.Range["L7"].Value = "Giới tính chỉ có thể là Nam/Nữ";

            sheet.Range["L8"].Value = "Xóa lưu ý trước khi thêm";
            file.Close();
        }
        #endregion
    }
}
