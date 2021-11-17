
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
        public string SelectedProvince { get => _SelectedProvince; set { _SelectedProvince = value; OnPropertyChanged(); } }

        private string _SelectedWard;
        public string SelectedWard { get => _SelectedWard; set { _SelectedWard = value; OnPropertyChanged(); } }

        private string _SelectedDistrict;
        public string SelectedDistrict { get => _SelectedDistrict; set { _SelectedDistrict = value; OnPropertyChanged(); } }

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
            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
            TabList = Visibility.Visible;
            TabInformation = Visibility.Hidden;

            SetDefaultAddStaff();

            StaffList = new ObservableCollection<Staff>(DataProvider.ins.db.Staffs);
            StaffListView = StaffList.ToArray();
            FilterType = new string[] { "Tất cả", "Giới tính", "Quốc tịch", "Chức vụ", "Bộ phận" };
            SelectedFilterType = "Tất cả";
            SelectedFilterProperty = "Chọn phương thức lọc";
            getFilterProperty();

            NationalityList = new ObservableCollection<string>() {
                "Việt Nam", "Mỹ", "Pháp", "Đức", "Trung Quốc"
            };

            ProvinceList = new ObservableCollection<string>() {
                "Hồ Chí Minh", "Bình Dương", "Vĩnh Long"
            };
            DistrictList = new ObservableCollection<string>() {
                "Quận 1", "Quận 2", "Quận 3", "Quận 4"
            };
            WardList = new ObservableCollection<string>()
            {
                "Phú Thạnh", "Phú Thọ Hòa", "Bình Hưng Hòa"
            };

            SexList = new ObservableCollection<string>()
            {
                "Nam", "Nữ"
            };

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
                DeleteStaff();
                BackToStaffList();
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

        }

        #region method
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

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db update";

                    MessageBox.Show(error);
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi validation";

                    MessageBox.Show(error);
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db đéo support";

                    MessageBox.Show(error);
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db object disposed";

                    MessageBox.Show(error);
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi invalid operation";

                    MessageBox.Show(error);
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

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db update";

                    MessageBox.Show(error);
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi validation";

                    MessageBox.Show(error);
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db đéo support";

                    MessageBox.Show(error);
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db object disposed";

                    MessageBox.Show(error);
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi invalid operation";

                    MessageBox.Show(error);
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
                    string error = "Lỗi db update";

                    MessageBox.Show(error);
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi validation";

                    MessageBox.Show(error);
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db đéo support";

                    MessageBox.Show(error);
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db object disposed";

                    MessageBox.Show(error);
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi invalid operation";

                    MessageBox.Show(error);
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


            StreetName = StaffAdress.streetName;
            ApartmentNumber = StaffAdress.apartmentNumber;
            SelectedProvince = StaffAdress.province;
            SelectedWard = StaffAdress.ward;
            SelectedDistrict = StaffAdress.district;

            InitDisplayAddress(StaffAdress);
        }


        void FilterListView()
        {
            SelectedFilterType = "Tất cả";
            if (SearchKey == "")
            {
                StaffListView = StaffList.ToArray();

            }

            else
            {


                StaffListView = StaffList.ToArray();
                String[] Value = new string[StaffListView.Length];

                for (int i = 0; i < StaffListView.Length; i++)
                {
                    Value[i] = StaffListView[i].name.ToString() + "@@" + StaffListView[i].citizenID.ToString() + "@@" + StaffListView[i].id.ToString() + "@@" + StaffListView[i].department.ToString() + "@@" + StaffListView[i].jobTitle.ToString();

                }

                StaffListView = StaffListView.Where((val, index) => Value[index].Contains(SearchKey)).ToArray();
            }
        }


        void getFilterProperty()
        {
            SelectedFilterProperty = "Tất cả";

            //FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.GetType().GetProperty(SelectedFilterType)).Distinct();
            if (SelectedFilterType == "Tất cả")
            {
                SelectedFilterProperty = "Chọn phương thức lọc";
                FilterProperty = new string[] { "Chọn phương thức lọc" };
            }
            else if (SelectedFilterType == "Giới tính")
            {
                FilterProperty = new string[] { "Nam", "Nữ" };
                SelectedFilterProperty = "Tất cả";
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.nationality).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Chức vụ")
            {
                FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.jobTitle).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Bộ phận")
            {
                FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.department).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }

            StaffListView = DataProvider.ins.db.Staffs.ToArray();
        }

        void SelectFilterProperty()
        {
            if (SelectedFilterType == "Tất cả")
            {
            }
            else if (SelectedFilterType == "Giới tính")
            {
                StaffListView = DataProvider.ins.db.Staffs.Where(x => x.sex == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                StaffListView = DataProvider.ins.db.Staffs.Where(x => x.nationality == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Chức vụ")
            {

                StaffListView = DataProvider.ins.db.Staffs.Where(x => x.jobTitle == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Bộ phận")
            {

                StaffListView = DataProvider.ins.db.Staffs.Where(x => x.department == SelectedFilterProperty).ToArray();
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

        void AddStaffFromExcel()
        {
            List<Address> listStaffAddress = new List<Address>();
            List<Staff> listStaff = new List<Staff>();
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx;*.xlsm;*xls)|*.xlsx;*.xlsm;*xls|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            else
                return;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            if (xlRange.Cells[1, 1] == null || xlRange.Cells[1, 1].Value2 != "STT" ||
            xlRange.Cells[1, 2] == null || xlRange.Cells[1, 2].Value2 != "Họ và tên" ||
            xlRange.Cells[1, 3] == null || xlRange.Cells[1, 3].Value2 != "Ngày sinh" ||
            xlRange.Cells[1, 4] == null || xlRange.Cells[1, 4].Value2 != "Giới tính" ||
            xlRange.Cells[1, 5] == null || xlRange.Cells[1, 5].Value2 != "CMND/CCCD" ||
            xlRange.Cells[1, 6] == null || xlRange.Cells[1, 6].Value2 != "Quốc tịch" ||
            xlRange.Cells[1, 7] == null || xlRange.Cells[1, 7].Value2 != "SĐT" ||
            xlRange.Cells[1, 8] == null || xlRange.Cells[1, 8].Value2 != "MaBH" ||
            xlRange.Cells[1, 9] == null || xlRange.Cells[1, 9].Value2 != "Chức vụ" ||
            xlRange.Cells[1, 10] == null || xlRange.Cells[1, 10].Value2 != "Phòng ban" ||
            xlRange.Cells[1, 11] == null || xlRange.Cells[1, 11].Value2 != "Địa chỉ")
            {
                MessageBox.Show("Không đúng định dạng file");
                return;
            }
            for (int i = 2; i <= rowCount; i++)
            {
                Staff staff = new Staff();
                Address address = new Address();
                if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                {
                    staff.name = xlRange.Cells[i, 2].Value2.ToString();
                }
                if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                {
                    DateTime birth = DateTime.FromOADate(double.Parse(xlRange.Cells[i, 3].Value2.ToString()));
                    staff.dateOfBirth = birth;
                }
                if (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null)
                {
                    staff.sex = xlRange.Cells[i, 4].Value2.ToString();
                }
                if (xlRange.Cells[i, 11] != null && xlRange.Cells[i, 11].Value2 != null)
                {
                    string[] arrListStr = xlRange.Cells[i, 11].Value2.ToString().Split(',');
                    address.district = arrListStr[2];
                    address.province = arrListStr[3];
                    address.ward = arrListStr[1];
                    address.streetName = arrListStr[0];
                }
                if (xlRange.Cells[i, 5] != null && xlRange.Cells[i, 5].Value2 != null)
                {
                    staff.citizenID = xlRange.Cells[i, 5].Value2.ToString();
                }
                if (xlRange.Cells[i, 8] != null && xlRange.Cells[i, 8].Value2 != null)
                {
                    staff.healthInsuranceID = xlRange.Cells[i, 8].Value2.ToString();
                }
                if (xlRange.Cells[i, 6] != null && xlRange.Cells[i, 6].Value2 != null)
                {
                    staff.nationality = xlRange.Cells[i, 6].Value2.ToString();
                }
                if (xlRange.Cells[i, 7] != null && xlRange.Cells[i, 7].Value2 != null)
                {
                    staff.phoneNumber = xlRange.Cells[i, 7].Value2.ToString();
                }
                if (xlRange.Cells[i, 9] != null && xlRange.Cells[i, 9].Value2 != null)
                {
                    staff.jobTitle = xlRange.Cells[i, 9].Value2.ToString();
                }
                if (xlRange.Cells[i, 10] != null && xlRange.Cells[i, 10].Value2 != null)
                {
                    staff.department = xlRange.Cells[i, 10].Value2.ToString();
                }
                listStaff.Add(staff);
                listStaffAddress.Add(address);
            }
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
                    transaction.Commit();
                    SuccessDialog.ShowDialog();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db update";

                    MessageBox.Show(error);
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi validation";

                    MessageBox.Show(error);
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db đéo support";

                    MessageBox.Show(error);
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db object disposed";

                    MessageBox.Show(error);
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi invalid operation";

                    MessageBox.Show(error);
                }
            }
            StaffListView = new ObservableCollection<Model.Staff>(DataProvider.ins.db.Staffs).ToArray();
        }

        #endregion
    }
}
