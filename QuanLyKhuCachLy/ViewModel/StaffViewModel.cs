using QuanLyKhuCachLy.Model;
using QuanLyKhuCachLy.UserControl;
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
    public class StaffViewModel : BaseViewModel
    {
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

        private string _StreetName;
        public string StreetName { get => _StreetName; set { _StreetName = value; OnPropertyChanged(); } }

        private string _ApartmentNumber;
        public string ApartmentNumber { get => _ApartmentNumber; set { ApartmentNumber = value; OnPropertyChanged(); } }

        private string _SelectedProvince;
        public string SelectedProvince { get => _SelectedProvince; set { _SelectedProvince = value; OnPropertyChanged(); } }

        private string _SelectedWard;
        public string SelectedWard { get => _SelectedWard; set { _SelectedWard = value; OnPropertyChanged(); } }

        private string _SelectedDistrict;
        public string SelectedDistrict { get => _SelectedDistrict; set { _SelectedDistrict = value; OnPropertyChanged(); } }

        #region List

        private Staff _SelectedItem;
        public Staff SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Name = SelectedItem.name;
                    DateOfBirth = SelectedItem.dateOfBirth;
                    SelectedNationality = SelectedItem.nationality;
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

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand ToAddCommand { get; set; }

        #endregion
        public StaffViewModel()

        {
            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
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
                return true;
            }, (p) =>
            {
                AddStaff();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return true;
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

            ToAddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddStaffScreen addStaffScreen = new AddStaffScreen();
                addStaffScreen.ShowDialog();
            });


        }

        #region method

        void AddStaff()
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

        #endregion
    }
}
