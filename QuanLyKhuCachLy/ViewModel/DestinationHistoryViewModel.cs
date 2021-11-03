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
    public class DestinationHistoryViewModel : BaseViewModel
    {
        #region property
        private int _PersonID;

        private DestinationHistory _SelectedItem;
        public DestinationHistory SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (_SelectedItem != null)
                {
                    SetSelectedItemToProperty();
                }
            }
        }

        private string _HDDisplayAddress;
        public string HDDisplayAddress { get => _HDDisplayAddress; set { _HDDisplayAddress = value; OnPropertyChanged(); } }

        #region destination history
        private System.DateTime _HDDateArrive;
        public System.DateTime HDDateArrive
        {
            get => _HDDateArrive; set
            {
                _HDDateArrive = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _HDQuarantinePersonID;
        public Nullable<int> HDQuarantinePersonID
        {
            get => _HDQuarantinePersonID; set
            {
                _HDQuarantinePersonID = value;
                OnPropertyChanged();
            }
        }

        #region address
        private string _HDStreetName;
        public string HDStreetName { get => _HDStreetName; set { _HDStreetName = value; OnPropertyChanged(); } }

        private string _HDApartmentNumber;
        public string HDApartmentNumber { get => _HDApartmentNumber; set { _HDApartmentNumber = value; OnPropertyChanged(); } }

        private string _HDSelectedProvince;
        public string HDSelectedProvince { get => _HDSelectedProvince; set { _HDSelectedProvince = value; OnPropertyChanged(); } }

        private string _HDSelectedWard;
        public string HDSelectedWard { get => _HDSelectedWard; set { _HDSelectedWard = value; OnPropertyChanged(); } }

        private string _HDSelectedDistrict;
        public string HDSelectedDistrict { get => _HDSelectedDistrict; set { _HDSelectedDistrict = value; OnPropertyChanged(); } }
        #endregion
        #endregion

        #region list
        private ObservableCollection<DestinationHistory> _DestinationHistoryList;
        public ObservableCollection<DestinationHistory> DestinationHistoryList
        {
            get => _DestinationHistoryList;
            set
            {
                _DestinationHistoryList = value;
                foreach (DestinationHistory dh in DestinationHistoryList)
                {
                    if (dh.displayAddress == null) return;
                    Address a = DataProvider.ins.db.Addresses.Where(x => x.id == dh.id).FirstOrDefault();
                    dh.displayAddress = $"{a.apartmentNumber} {a.streetName} {a.ward} {a.district} {a.province}";
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region command
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand ToAddCommand { get; set; }
        public ICommand ToEditCommand { get; set; }
        public ICommand ToDeleteCommand { get; set; }

        #endregion

        #endregion

        public DestinationHistoryViewModel() { }

        public DestinationHistoryViewModel(int currentPersonID)
        {
            _PersonID = currentPersonID;
            DestinationHistoryList = new ObservableCollection<DestinationHistory>(DataProvider.ins.db.DestinationHistories.Where(x => x.quarantinePersonID == _PersonID));


            AddCommand = new RelayCommand<object>((p) =>
            {
                Address DestinationHistoryAddress = new Address()
                {
                    apartmentNumber = HDApartmentNumber,
                    streetName = HDStreetName,
                    ward = HDSelectedWard,
                    district = HDSelectedDistrict,
                    province = HDSelectedProvince,
                };

                DestinationHistory PersonDestinationHistory = new DestinationHistory()
                {
                    dateArrive = HDDateArrive,
                    quarantinePersonID = _PersonID,
                };

                if (DestinationHistoryAddress.CheckValidateProperty() && PersonDestinationHistory.CheckValidProperty())
                    return true;

                return false;
            }
            , (p) =>
            {
                AddDestinationHistory();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                Address DestinationHistoryAddress = new Address()
                {
                    apartmentNumber = HDApartmentNumber,
                    streetName = HDStreetName,
                    ward = HDSelectedWard,
                    district = HDSelectedDistrict,
                    province = HDSelectedProvince,
                };

                DestinationHistory PersonDestinationHistory = new DestinationHistory()
                {
                    dateArrive = HDDateArrive,
                    quarantinePersonID = _PersonID,
                };

                if (DestinationHistoryAddress.CheckValidateProperty() && PersonDestinationHistory.CheckValidProperty())
                    return true;

                return false;
            }, (p) =>
            {
                EditDestinationHistory();
            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteDestinationHistory();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            ToAddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });

            ToEditCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });

            ToDeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });
        }

        #region method
        void SetSelectedItemToProperty()
        {
            HDDateArrive = SelectedItem.dateArrive;
        }

        void AddDestinationHistory()
        {
            //Tạo địa chỉ thông tin nơi đã tới(optional) vì là thành phần của thằng nơi đã tới
            Address DestinationHistoryAddress = new Address()
            {
                apartmentNumber = HDApartmentNumber,
                streetName = HDStreetName,
                ward = HDSelectedWard,
                district = HDSelectedDistrict,
                province = HDSelectedProvince,
            };

            DataProvider.ins.db.Addresses.Add(DestinationHistoryAddress);
            DataProvider.ins.db.SaveChanges();


            //Tạo thông tin nơi người cách ly đã tới(optional)
            DestinationHistory PersonDestinationHistory = new DestinationHistory()
            {
                dateArrive = HDDateArrive,
                quarantinePersonID = _PersonID,
                addressID = DestinationHistoryAddress.id,
            };

            DataProvider.ins.db.DestinationHistories.Add(PersonDestinationHistory);
            DataProvider.ins.db.SaveChanges();
        }

        void EditDestinationHistory()
        {
            DestinationHistory DestinationHistory = DataProvider.ins.db.DestinationHistories.Where(x => x.id == SelectedItem.id).FirstOrDefault();
            Address DesAdd = DataProvider.ins.db.Addresses.Where(x => x.id == DestinationHistory.addressID).FirstOrDefault();
            if (DesAdd == null) return;

            DestinationHistory.dateArrive = HDDateArrive;
            DestinationHistory.quarantinePersonID = _PersonID;

            DesAdd.apartmentNumber = HDApartmentNumber;
            DesAdd.streetName = HDStreetName;
            DesAdd.ward = HDSelectedWard;
            DesAdd.district = HDSelectedDistrict;
            DesAdd.province = HDSelectedProvince;

            DataProvider.ins.db.SaveChanges();
        }

        void DeleteDestinationHistory()
        {

        }
        #endregion
    }
}
