using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {

        private Model.QuarantineArea _QuarantineArea;

        public Model.QuarantineArea QuarantineArea
        {
            get { return _QuarantineArea; }
            set { _QuarantineArea = value; }
        }

        private Address _QAAdress;

        public Address QAAdress
        {
            get { return _QAAdress; }
            set { _QAAdress = value; }
        }

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

        private Model.Staff _Manager;

        public Model.Staff Manager
        {
            get { return _Manager; }
            set { _Manager = value; OnPropertyChanged(); }
        }

        private Model.Staff _SelectedStaff;

        public Model.Staff SelectedStaff
        {
            get { return _SelectedStaff; }
            set { _SelectedStaff = value; OnPropertyChanged(); }
        }

        private string _QuarantineAreaAddress;

        public string QuarantineAreaAddress
        {
            get { return _QuarantineAreaAddress; }
            set { _QuarantineAreaAddress = value; OnPropertyChanged(); }
        }

        private string _SelectedEntity;

        public string SelectedEntity
        {
            get { return _SelectedEntity; }
            set { _SelectedEntity = value; OnPropertyChanged(); }
        }

        //#region UI

        //private Visibility _Tab1;
        //public Visibility Tab1 { get => _Tab1; set { _Tab1 = value; OnPropertyChanged(); } }

        //private Visibility _Tab2;
        //public Visibility Tab2 { get => _Tab2; set { _Tab2 = value; OnPropertyChanged(); } }
        //public int TabIndex { get; set; }

        //private String _TabPostion;
        //public String TabPosition
        //{
        //    get => _TabPostion; set
        //    {
        //        _TabPostion = value; OnPropertyChanged();
        //    }
        //}

        //#endregion

        #region command
        public ICommand ToEditCommand { get; set; }
        public ICommand PreviousTabCommand { get; set; }
        public ICommand NextTabCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RefeshCommand { get; set; }
        public ICommand DeleteEntityCommand { get; set; }

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


        #endregion

        #region list

        private ObservableCollection<string> _EntityList;
        public ObservableCollection<string> EntityList
        {
            get => _EntityList; set
            {
                _EntityList = value; OnPropertyChanged();
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
        #endregion


        public SettingViewModel()
        {

            ToEditCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                EditQuarantineArea editScreen = new EditQuarantineArea();
                editScreen.ShowDialog();
            });

            EditCommand = new RelayCommand<Window>((p) => { return true; }, (o) =>
            {
                EditQuarantineAreaInformation();
                o.Close();
            });

            //PreviousTabCommand = new RelayCommand<object>((p) =>
            //{
            //    if (TabIndex > 1) return true;
            //    return false;
            //}, (o) =>
            //{
            //    HandleChangeTab(TabIndex, "previous");
            //});

            //NextTabCommand = new RelayCommand<object>((p) =>
            //{
            //    if (TabIndex <= 2)
            //        return true;
            //    return false;
            //}, (o) =>
            //{
            //    HandleChangeTab(TabIndex, "next");
            //});

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (o) =>
            {
                o.Close();
            });

            RefeshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (o) =>
            {
                RefeshTab();
            });

            DeleteEntityCommand = new RelayCommand<object>((p) =>
            {
                if (!string.IsNullOrWhiteSpace(SelectedEntity))
                    return true;
                return false;
            }, (o) =>
            {
                DeleteEntity();
            });


            ProvinceList = new ObservableCollection<string>();
            DistrictList = new ObservableCollection<string>();
            WardList = new ObservableCollection<string>();
            EntityList = new ObservableCollection<string>()
            {
                "Tất cả", "Phòng", "Người cách ly", "Nhân viên"
            };

            InitProvinceList();

            if (DataProvider.ins.db.QuarantineAreas.Count() != 0)
            {
                Init();
            }
        }

        #region method

        void DeleteEntity()
        {
            switch (SelectedEntity)
            {
                case "Tất cả":
                    DeleteAll();
                    break;
                case "Phòng":
                    DeleteAllRecordInRoomList();
                    break;
                case "Người cách ly":
                    DeleteAllRecordInPersonList();
                    break;
                case "Nhân viên":
                    DeleteAllRecordInStaffList();
                    break;
                default:
                    break;
            }
        }

        void RestartApplication()
        {
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

        void DeleteAll()
        {
            DeleteAllRecordInRoomList();
            DeleteAllRecordInPersonList();
            DeleteAllRecordInStaffList();
            DeleteQuarantineAreaInformation();
            DeleteSeverityList();
            RestartApplication();
        }

        void DeleteSeverityList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.Severities.RemoveRange(DataProvider.ins.db.Severities);
                    DataProvider.ins.db.SaveChanges();

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

        void DeleteQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.QuarantineAreas.RemoveRange(DataProvider.ins.db.QuarantineAreas);
                    DataProvider.ins.db.SaveChanges();

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

        void DeleteAllRecordInStaffList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.Staffs.RemoveRange(DataProvider.ins.db.Staffs);
                    DataProvider.ins.db.SaveChanges();

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

        void DeleteAllRecordInPersonList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.QuarantinePersons.RemoveRange(DataProvider.ins.db.QuarantinePersons);
                    DataProvider.ins.db.SaveChanges();

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

        void DeleteAllRecordInRoomList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.QuarantineRooms.RemoveRange(DataProvider.ins.db.QuarantineRooms);
                    DataProvider.ins.db.SaveChanges();

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

        void RefeshTab()
        {
            Init();
        }

        void Init()
        {
            QuarantineArea = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
            QAAdress = QuarantineArea.Address;
            QuarantineAreaAddress = $"{QuarantineArea.Address?.apartmentNumber} {QuarantineArea.Address?.streetName}, {QuarantineArea.Address.ward}, {QuarantineArea.Address.district}, {QuarantineArea.Address.province}";
            SelectedProvince = QAAdress.province;
            SelectedDistrict = QAAdress.district;
            SelectedWard = QAAdress.ward;
            ApartmentNumber = QAAdress.apartmentNumber;
            StreetName = QAAdress.streetName;
            Manager = DataProvider.ins.db.Staffs.Where(staff => staff.id == QuarantineArea.managerID).FirstOrDefault();
            SelectedStaff = Manager;
            StaffList = new ObservableCollection<Staff>(DataProvider.ins.db.Staffs);
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

        //void SetDefaultEditTab()
        //{
        //    Tab1 = Visibility.Visible;
        //    Tab2 = Visibility.Hidden;
        //    TabIndex = 1;
        //    TabPosition = $"{TabIndex}/2";
        //}

        void EditQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {

                    QAAdress.province = SelectedProvince;
                    QAAdress.district = SelectedDistrict;
                    QAAdress.ward = SelectedWard;
                    QAAdress.apartmentNumber = ApartmentNumber;
                    QAAdress.streetName = StreetName;

                    Manager = SelectedStaff;

                    if (Manager != null)
                        QuarantineArea.managerID = Manager.id;

                    DataProvider.ins.db.SaveChanges();

                    QuarantineAreaAddress = $"{QuarantineArea.Address?.apartmentNumber} {QuarantineArea.Address?.streetName}, {QuarantineArea.Address.ward}, {QuarantineArea.Address.district}, {QuarantineArea.Address.province}";

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

        //void HandleChangeTab(int index, string action)
        //{
        //    if (action == "next")
        //    {
        //        TabIndex++;
        //    }
        //    else
        //    {
        //        TabIndex--;
        //    }
        //    if (TabIndex <= 2)
        //        TabPosition = $"{TabIndex}/2";

        //    switch (TabIndex)
        //    {
        //        case 1:
        //            Tab1 = Visibility.Visible;
        //            Tab2 = Visibility.Hidden;
        //            break;
        //        case 2:
        //            Tab1 = Visibility.Hidden;
        //            Tab2 = Visibility.Visible;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        #endregion
    }
}
