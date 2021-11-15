using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
        public string HDSelectedProvince { get => _HDSelectedProvince; set { _HDSelectedProvince = value; OnPropertyChanged(); } }

        private string _HDSelectedWard;
        public string HDSelectedWard { get => _HDSelectedWard; set { _HDSelectedWard = value; OnPropertyChanged(); } }

        private string _HDSelectedDistrict;
        public string HDSelectedDistrict { get => _HDSelectedDistrict; set { _HDSelectedDistrict = value; OnPropertyChanged(); } }

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
                        //if (dh.displayAddress == null) return;
                        //Address a = DataProvider.ins.db.Addresses.Where(x => x.id == dh.id).FirstOrDefault();
                        //dh.displayAddress = $"{a.apartmentNumber} {a.streetName} {a.ward} {a.district} {a.province}";
                        var add = DataProvider.ins.db.Addresses.Where(x => x.id == dh.quarantinePersonID).FirstOrDefault();
                        AddressList.Add(add);
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

        #endregion

        #region command
        public ICommand ToAddCommand;

        public ICommand ToEditCommand;

        public ICommand AddOnUICommand;

        public ICommand EditOnUICommand;

        public ICommand DeleteOnUICommand;

        #endregion


        #endregion


        public DestinationHistoryViewModel()
        {
            DestinationHistoryList = new ObservableCollection<DestinationHistory>();
            AddressList = new ObservableCollection<Address>();
            idForTask = 0;

            AddOnUICommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddDestinationHistoryUI();
            });

            EditOnUICommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                EditDestinationHistoryUI();
            });

            DeleteOnUICommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                DeleteDestinationHistoryUI();
            });

            ToAddCommand = new RelayCommand<object>((p) =>
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
                AddDestinationHistoryUI();
            });
        }

        #region method
        void SetSelectedItemToProperty()
        {
            HDDateArrive = SelectedItem.dateArrive;

            Address HDAddress = DataProvider.ins.db.Addresses.Where(x => x.id == SelectedItem.addressID).FirstOrDefault();
            if (HDAddress == null) return;

            HDApartmentNumber = HDAddress.apartmentNumber;
            HDStreetName = HDAddress.streetName;
            HDSelectedWard = HDAddress.ward;
            HDSelectedDistrict = HDAddress.district;
            HDSelectedProvince = HDAddress.province;
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
            };

            DestinationHistoryList.Add(PersonDestinationHistory);
        }

        void EditDestinationHistoryUI()
        {
            DestinationHistoryList[DestinationHistoryList.IndexOf(SelectedItem)].dateArrive = HDDateArrive;

            var DesAdd = AddressList.Where(x => x.id == SelectedItem.addressID).FirstOrDefault();
            if (DesAdd == null) return;

            AddressList[AddressList.IndexOf(DesAdd)].apartmentNumber = HDApartmentNumber;
            AddressList[AddressList.IndexOf(DesAdd)].streetName = HDStreetName;
            AddressList[AddressList.IndexOf(DesAdd)].ward = HDSelectedWard;
            AddressList[AddressList.IndexOf(DesAdd)].district = HDSelectedDistrict;
            AddressList[AddressList.IndexOf(DesAdd)].province = HDSelectedProvince;
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
                ClearAddressList();
                foreach (var dh in DestinationHistoryList)
                {
                    var add = DataProvider.ins.db.Addresses.Where(x => x.id == dh.quarantinePersonID).FirstOrDefault();
                    AddressList.Add(add);
                }
            }
        }

        public void RollbackTransaction(int PersonID)
        {
            DataProvider.ins.db.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
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
        #endregion
    }
}
