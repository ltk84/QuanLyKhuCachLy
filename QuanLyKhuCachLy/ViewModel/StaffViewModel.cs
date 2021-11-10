
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

            NationalityList = new ObservableCollection<string>() {
                "VietNam", "Ameriden", "Phap", "Dut", "Em"
            };

            ProvinceList = new ObservableCollection<string>() {
                "Ho Chi Minh", "Binh Duong", "Vinh Long"
            };
            DistrictList = new ObservableCollection<string>() {
                "Quan 1", "Quan 2", "Quan 3", "Quan 4"
            };
            WardList = new ObservableCollection<string>()
            {
                "Phu Thanh", "Phu Tho Hoa", "Binh Hung Hoa"
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
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                DeleteStaff();
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

            });

            ToEditCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                isAdding = false;
                SetSelectedItemToProperty();
                editStaffScreen = new EditStaffScreen();
                editStaffScreen.ShowDialog();
            });

            ToViewCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TabInformation = Visibility.Visible;
                TabList = Visibility.Hidden;
            });

            ToMainCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TabInformation = Visibility.Hidden;
                TabList = Visibility.Visible;
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

            DataProvider.ins.db.SaveChanges();
        }
        void DeleteStaff() { }


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
        }

        #endregion
    }
}
