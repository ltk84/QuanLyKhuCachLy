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
        private string _name;
        public string name { get => _name; set { _name = value; OnPropertyChanged(); } }

        private string _province;
        public string province { get => _province; set { _province = value; OnPropertyChanged(); } }

        private string _district;
        public string district { get => _district; set { _district = value; OnPropertyChanged(); } }

        private string _ward;
        public string ward { get => _ward; set { _ward = value; OnPropertyChanged(); } }

        private string _streetName;
        public string streetName { get => _streetName; set { _streetName = value; OnPropertyChanged(); } }

        private string _apartmentNumber;
        public string apartmentNumber { get => _apartmentNumber; set { _apartmentNumber = value; OnPropertyChanged(); } }

        private string _selectedProvince;
        public string selectedProvince { get => _selectedProvince; set { _selectedProvince = value; OnPropertyChanged(); province = selectedProvince; } }

        private string _selectedWard;
        public string selectedWard { get => _selectedWard; set { _selectedWard = value; OnPropertyChanged(); ward = selectedWard; } }
        
        private string _selectedDistrict;
        public string selectedDistrict { get => _selectedDistrict; set { _selectedDistrict = value; OnPropertyChanged(); district = selectedDistrict; } }

        private ObservableCollection<string> _provinceList;
        public ObservableCollection<string> provinceList { get => _provinceList; set {
                _provinceList = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _districtList;
        public ObservableCollection<string> districtList
        {
            get => _districtList; set
            {
                _districtList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _wardList;
        public ObservableCollection<string> wardList
        {
            get => _wardList; set
            {
                _wardList = value; OnPropertyChanged();
            }
        }

        public ICommand showCommand { get; set; }

        #endregion

        public QuarantineAreaInformationViewModel()
        {
            provinceList = new ObservableCollection<string>() { 
                "HCM", "Binh Duong", "Vinh Long"
            };
            districtList = new ObservableCollection<string>() { 
                "A", "B", "C"
            };
            wardList = new ObservableCollection<string>()
            {
                "E", "F", "G"
            };

            showCommand = new RelayCommand<object>((p) => 
            {
                if (!string.IsNullOrEmpty(name) && selectedDistrict != null && selectedProvince != null && selectedWard != null && !string.IsNullOrEmpty(streetName) && !string.IsNullOrEmpty(apartmentNumber))
                    return true;
                return false;
            }, (p) =>
            {
                updateQuarantineAreaInformation();
            });
        }

        void updateQuarantineAreaInformation()
        {
            Address qAreaAddress = new Address() { 
                province = province, district = district, apartmentNumber = apartmentNumber, streetName = streetName, ward = ward
            };

            DataProvider.ins.db.Addresses.Add(qAreaAddress);
            DataProvider.ins.db.SaveChanges();

            

           
        }
    }
}
