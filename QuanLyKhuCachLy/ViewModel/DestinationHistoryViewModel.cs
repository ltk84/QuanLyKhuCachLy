using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Utility;

namespace QuanLyKhuCachLy.ViewModel
{
    public class DestinationHistoryViewModel : BaseViewModel
    {
        #region property
        private int idForTask { get; set; }


        private int _PersonID;
        public int PersonID
        {
            get => _PersonID; set
            {
                _PersonID = value;
                DestinationHistoryList = new ObservableCollection<DestinationHistory>(DataProvider.ins.db.DestinationHistories.Where(x => x.quarantinePersonID == _PersonID));
                if (DestinationHistoryList.Count != 0)
                    idForTask = DestinationHistoryList.Last().id + 1;
                OnPropertyChanged();
            }
        }

        private static DestinationHistoryViewModel _ins;
        public static DestinationHistoryViewModel ins
        {
            get
            {
                if (_ins == null) _ins = new DestinationHistoryViewModel();
                return _ins;
            }
            set => _ins = value;
        }

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

        #region address
        private string _HDStreetName;
        public string HDStreetName { get => _HDStreetName; set { _HDStreetName = value; OnPropertyChanged(); } }

        private string _HDApartmentNumber;
        public string HDApartmentNumber { get => _HDApartmentNumber; set { _HDApartmentNumber = value; OnPropertyChanged(); } }

        private string _HDSelectedProvince;
        public string HDSelectedProvince
        {
            get => _HDSelectedProvince;
            set { _HDSelectedProvince = value; OnPropertyChanged(); InitDistrictList(); }
        }

        private string _HDSelectedWard;
        public string HDSelectedWard
        {
            get => _HDSelectedWard;
            set { _HDSelectedWard = value; OnPropertyChanged(); }
        }

        private string _HDSelectedDistrict;
        public string HDSelectedDistrict
        {
            get => _HDSelectedDistrict;
            set { _HDSelectedDistrict = value; OnPropertyChanged(); InitWardList(); }
        }

        private string _HDDisplayAddress;
        public string HDDisplayAddress { get => _HDDisplayAddress; set { _HDDisplayAddress = value; OnPropertyChanged(); } }
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
                if (_DestinationHistoryList.Count != 0)
                {
                    ClearAddressList();
                    foreach (DestinationHistory dh in DestinationHistoryList)
                    {
                        var add = DataProvider.ins.db.Addresses.Where(x => x.id == dh.addressID).FirstOrDefault();
                        AddressList.Add(add);

                        dh.displayAddress = InitDisplayAddress(add);
                    }
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Address> _AddressList;
        public ObservableCollection<Address> AddressList
        {
            get => _AddressList;
            set
            {
                _AddressList = value;
                OnPropertyChanged();
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

        #region command
        public ICommand ToAddCommand { get; set; }

        public ICommand ToEditCommand { get; set; }

        public ICommand AddOnUICommand { get; set; }

        public ICommand EditOnUICommand { get; set; }

        public ICommand DeleteOnUICommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #endregion

        #region validation rule

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

        private bool _DateTimeFieldHasError;
        public bool DateTimeFieldHasError
        {
            get => _DateTimeFieldHasError; set
            {
                _DateTimeFieldHasError = value; OnPropertyChanged();
            }
        }

        #endregion


        #endregion


        public DestinationHistoryViewModel()
        {
            DestinationHistoryList = new ObservableCollection<DestinationHistory>();
            AddressList = new ObservableCollection<Address>();
            ProvinceList = new ObservableCollection<string>();
            WardList = new ObservableCollection<string>();
            DistrictList = new ObservableCollection<string>();

            InitProvinceList();

            idForTask = 0;

            AddOnUICommand = new RelayCommand<Window>((p) =>
            {
                if (!DistrictFieldHasError && !ProvinceFieldHasError && !WardFieldHasError && !DateTimeFieldHasError)
                    return true;
                return false;
            }, (p) =>
            {
                AddDestinationHistoryUI();
                p.Close();
            });

            EditOnUICommand = new RelayCommand<Window>((p) =>
            {
                if (!DistrictFieldHasError && !ProvinceFieldHasError && !WardFieldHasError && !DateTimeFieldHasError)
                    return true;
                return false;
            }, (p) =>
            {
                EditDestinationHistoryUI();
                p.Close();
            });

            DeleteOnUICommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                return false;
            }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                DeleteDestinationHistoryUI();
            });

            ToAddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ClearData();
                AddDestinationHistory AddDestinationHistory = new AddDestinationHistory();
                AddDestinationHistory.ShowDialog();
                ClearData();
            });

            ToEditCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                SetSelectedItemToProperty();
                EditDestinationHistory EditScreen = new EditDestinationHistory();
                EditScreen.ShowDialog();
            });

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });
        }

        #region method

        void InitProvinceList()
        {
            foreach (var item in AddressViewModel.ProvinceList)
            {
                ProvinceList.Add(item.name);
            }
        }

        void InitDistrictList()
        {
            AddressViewModel.ProvinceSelectEvent(HDSelectedProvince);
            DistrictList.Clear();
            foreach (var item in AddressViewModel.DistrictList)
            {
                DistrictList.Add(item.name);
            }
        }

        void InitWardList()
        {
            AddressViewModel.DistrictSelectEVent(HDSelectedDistrict);
            WardList.Clear();
            foreach (var item in AddressViewModel.WardList)
            {
                WardList.Add(item.name);
            }
        }

        void ClearData()
        {
            HDDateArrive = DateTime.MinValue;
            HDApartmentNumber = string.Empty;
            HDStreetName = string.Empty;
            HDSelectedWard = string.Empty;
            HDSelectedDistrict = string.Empty;
            HDSelectedProvince = string.Empty;
        }

        void SetSelectedItemToProperty()
        {
            HDDateArrive = SelectedItem.dateArrive;

            Address HDAddress = AddressList.Where(x => x.id == SelectedItem.addressID).FirstOrDefault();
            if (HDAddress == null) return;


            HDSelectedProvince = HDAddress.province;
            HDSelectedDistrict = HDAddress.district;
            HDSelectedWard = HDAddress.ward;
            HDApartmentNumber = HDAddress.apartmentNumber;
            HDStreetName = HDAddress.streetName;
        }

        void AddDestinationHistoryUI()
        {
            //Tạo địa chỉ thông tin nơi đã tới(optional) vì là thành phần của thằng nơi đã tới
            Address DestinationHistoryAddress = new Address()
            {
                apartmentNumber = HDApartmentNumber,
                streetName = HDStreetName,
                ward = HDSelectedWard,
                district = HDSelectedDistrict,
                province = HDSelectedProvince,
                id = idForTask,
            };

            if (DestinationHistoryAddress.CheckValidateProperty())
            {
                AddressList.Add(DestinationHistoryAddress);
            }


            //Tạo thông tin nơi người cách ly đã tới(optional)
            DestinationHistory PersonDestinationHistory = new DestinationHistory()
            {
                dateArrive = HDDateArrive,
                quarantinePersonID = PersonID,
                addressID = DestinationHistoryAddress.id,
                displayAddress = InitDisplayAddress(DestinationHistoryAddress),
            };

            DestinationHistoryList.Add(PersonDestinationHistory);

            idForTask++;
        }

        void EditDestinationHistoryUI()
        {
            DestinationHistoryList[DestinationHistoryList.IndexOf(SelectedItem)].dateArrive = HDDateArrive;

            var DesAdd = AddressList.Where(x => x.id == SelectedItem.addressID).FirstOrDefault();
            if (DesAdd == null) return;

            //AddressList[AddressList.IndexOf(DesAdd)].apartmentNumber = HDApartmentNumber;
            //AddressList[AddressList.IndexOf(DesAdd)].streetName = HDStreetName;
            //AddressList[AddressList.IndexOf(DesAdd)].ward = HDSelectedWard;
            //AddressList[AddressList.IndexOf(DesAdd)].district = HDSelectedDistrict;
            //AddressList[AddressList.IndexOf(DesAdd)].province = HDSelectedProvince;

            DesAdd.apartmentNumber = HDApartmentNumber;
            DesAdd.streetName = HDStreetName;
            DesAdd.ward = HDSelectedWard;
            DesAdd.district = HDSelectedDistrict;
            DesAdd.province = HDSelectedProvince;

            DestinationHistoryList[DestinationHistoryList.IndexOf(SelectedItem)].displayAddress = InitDisplayAddress(DesAdd);
        }

        void DeleteDestinationHistoryUI()
        {
            var add = AddressList.Where(x => x.id == SelectedItem.addressID).FirstOrDefault();
            if (add == null) return;

            DestinationHistoryList.Remove(SelectedItem);
            AddressList.Remove(add);
        }

        public void ApplayDestinationHistoryToDB(int PersonID, string action)
        {
            if (action == "Add")
            {
                foreach (var add in AddressList)
                {
                    var des = DestinationHistoryList.Where(x => x.addressID == add.id).FirstOrDefault();

                    DataProvider.ins.db.Addresses.Add(add);
                    DataProvider.ins.db.SaveChanges();

                    des.addressID = add.id;
                }


                foreach (var dh in DestinationHistoryList)
                {
                    dh.quarantinePersonID = PersonID;
                    DataProvider.ins.db.DestinationHistories.Add(dh);
                }

                DataProvider.ins.db.SaveChanges();
                ClearDestinationHistoryList();
                ClearAddressList();
            }
            else
            {
                List<DestinationHistory> DHList = DataProvider.ins.db.DestinationHistories.Where(x => x.quarantinePersonID == PersonID).ToList();

                foreach (var dhUI in DestinationHistoryList)
                {
                    if (!DHList.Contains(dhUI))
                    {
                        var add = AddressList.Where(x => x.id == dhUI.addressID).FirstOrDefault();
                        DataProvider.ins.db.Addresses.Add(add);
                        DataProvider.ins.db.SaveChanges();

                        dhUI.addressID = add.id;
                        dhUI.quarantinePersonID = PersonID;
                        DataProvider.ins.db.DestinationHistories.Add(dhUI);
                        DataProvider.ins.db.SaveChanges();
                    }
                }

                foreach (var dhInDB in DHList)
                {
                    if (!DestinationHistoryList.Contains(dhInDB))
                    {
                        DataProvider.ins.db.DestinationHistories.Remove(dhInDB);
                    }
                }

                DataProvider.ins.db.SaveChanges();
                DestinationHistoryList = new ObservableCollection<DestinationHistory>(DataProvider.ins.db.DestinationHistories.Where(x => x.quarantinePersonID == PersonID));
                //ClearAddressList();
                //foreach (var dh in DestinationHistoryList)
                //{
                //    var add = DataProvider.ins.db.Addresses.Where(x => x.id == dh.quarantinePersonID).FirstOrDefault();
                //    AddressList.Add(add);
                //}
            }
        }

        public void RollbackTransaction(int PersonID)
        {
            DBUtilityTracker.Rollback();
            DestinationHistoryList = new ObservableCollection<DestinationHistory>(DataProvider.ins.db.DestinationHistories.Where(x => x.quarantinePersonID == PersonID));
            ClearAddressList();
            foreach (var dh in DestinationHistoryList)
            {
                var add = DataProvider.ins.db.Addresses.Where(x => x.id == dh.quarantinePersonID).FirstOrDefault();
                AddressList.Add(add);
            }
        }

        public void ClearDestinationHistoryList() { DestinationHistoryList.Clear(); ClearAddressList(); }
        public void ClearAddressList() => AddressList.Clear();

        string InitDisplayAddress(Address address)
        {
            string DisplayAdress = String.Empty;

            if (address == null) return "";
            List<string> list = new List<string>()
            {
                address.apartmentNumber,
                address.streetName,
                address.ward,
                address.district,
                address.province

            };

            for (int i = 0; i < list.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(list[i]))
                {
                    DisplayAdress += list[i];
                }
                if (i != list.Count - 1)
                {
                    if (i != 0)
                        DisplayAdress += ", ";
                    else DisplayAdress += " ";
                }
            }

            return DisplayAdress;
        }
        #endregion
    }
}
