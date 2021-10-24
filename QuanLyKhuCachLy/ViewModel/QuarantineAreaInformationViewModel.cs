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
        public string QAselectedProvince { get => _QASelectedProvince; set { _QASelectedProvince = value; OnPropertyChanged();  } }

        private string _QASelectedWard;
        public string QAselectedWard { get => _QASelectedWard; set { _QASelectedWard = value; OnPropertyChanged(); } }
        
        private string _QASelectedDistrict;
        public string QASelectedDistrict { get => _QASelectedDistrict; set { _QASelectedDistrict = value; OnPropertyChanged(); } }

        private int _QATestCycle;
        public int QAtestCycle { get => _QATestCycle; set { _QATestCycle = value; OnPropertyChanged(); } }

        private int _QARequiredDayToFinish;
        public int QARequiredDayToFinish { get => _QARequiredDayToFinish; set { _QARequiredDayToFinish = value; OnPropertyChanged(); } }

        #endregion

        #region Manager
        private string _ManagerStreetName;
        public string ManagerStreetName { get => _ManagerStreetName; set { _ManagerStreetName = value; OnPropertyChanged(); } }

        private string _ManagerApartmentNumber;
        public string ManagerApartmentNumber { get => _ManagerApartmentNumber; set { _ManagerApartmentNumber = value; OnPropertyChanged(); } }

        private string _ManagerSelectedProvince;
        public string ManagerSelectedProvince { get => _ManagerSelectedProvince; set { _ManagerSelectedProvince = value; OnPropertyChanged(); } }

        private string _ManagerSelectedWard;
        public string ManagerSelectedWard { get => _ManagerSelectedWard; set { _ManagerSelectedWard = value; OnPropertyChanged(); } }

        private string _ManagerSelectedDistrict;
        public string ManagerSelectedDistrict { get => _ManagerSelectedDistrict; set { _ManagerSelectedDistrict = value; OnPropertyChanged(); } }

        private string _ManagerName;
        public string ManagerName { get => _ManagerName; set { _ManagerName = value; OnPropertyChanged(); } }

        private DateTime _ManagerDateOfBirth;
        public DateTime ManagerDateOfBirth { get => _ManagerDateOfBirth; set { _ManagerDateOfBirth = value; OnPropertyChanged(); } }

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



        // đang dummy data (đáng lẻ nên lấy data từ đâu đó)
        private ObservableCollection<string> _ProvinceList;
        public ObservableCollection<string> ProvinceList { get => _ProvinceList; set {
                _ProvinceList = value; OnPropertyChanged(); } }

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

        public ICommand showCommand { get; set; }

        #endregion

        public QuarantineAreaInformationViewModel()
        {
            ProvinceList = new ObservableCollection<string>() {
                "HCM", "Binh Duong", "Vinh Long"
            };
            DistrictList = new ObservableCollection<string>() { 
                "A", "B", "C"
            };
            WardList = new ObservableCollection<string>()
            {
                "E", "F", "G"
            };

            showCommand = new RelayCommand<object>((p) =>
            {
                if (!string.IsNullOrEmpty(QAname) && QASelectedDistrict != null && QAselectedProvince != null && QAselectedWard != null && !string.IsNullOrEmpty(QAStreetName) && !string.IsNullOrEmpty(QAApartmentNumber))
                    return true;
                return false;
            }, (p) =>
            {
                updateQuarantineAreaInformation();
            });
        }

        #region Methods
        // Chưa test
        void updateQuarantineAreaInformation()
        {
            Address QAreaAddress = new Address()
            {
                province = QAselectedProvince,
                district = QASelectedDistrict,
                apartmentNumber = QAApartmentNumber,
                streetName = QAStreetName,
                ward = QAselectedWard
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

            DataProvider.ins.db.Addresses.Add(QAreaAddress);
            DataProvider.ins.db.SaveChanges();

            Staff Manager = new Staff()
            {
                addressID = ManagerAddress.id,
                citizenID = ManagerCitizenID,
                dateOfBirth = ManagerDateOfBirth,
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
                addressID = QAreaAddress.id, name = QAname, testCycle = QAtestCycle, requiredDayToFinish = QARequiredDayToFinish, managerID = Manager.id, 
            };

            DataProvider.ins.db.QuarantineAreas.Add(QuarantineArea);
            DataProvider.ins.db.SaveChanges();
        }

        #endregion
    }
}
