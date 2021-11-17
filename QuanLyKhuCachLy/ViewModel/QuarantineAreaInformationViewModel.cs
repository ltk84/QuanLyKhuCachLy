﻿using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
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
        public string QASelectedProvince { get => _QASelectedProvince; set { _QASelectedProvince = value; OnPropertyChanged(); } }

        private string _QASelectedWard;
        public string QASelectedWard { get => _QASelectedWard; set { _QASelectedWard = value; OnPropertyChanged(); } }

        private string _QASelectedDistrict;
        public string QASelectedDistrict { get => _QASelectedDistrict; set { _QASelectedDistrict = value; OnPropertyChanged(); } }

        private int _QATestCycle;
        public int QATestCycle { get => _QATestCycle; set { _QATestCycle = value; OnPropertyChanged(); } }

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
        public DateTime ManagerDateOfBirth { get => _ManagerDateOfBirth; set { _ManagerDateOfBirth = (DateTime)value; OnPropertyChanged(); } }

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

        #region UI
        public bool isDoneSetUp { get; set; }

        private Visibility _Tab1;
        private Visibility _Tab2;
        private Visibility _Tab3;
        private Visibility _Tab4;
        public Visibility Tab1
        {
            get => _Tab1; set
            {
                _Tab1 = value; OnPropertyChanged();
            }
        }
        public Visibility Tab2
        {
            get => _Tab2; set
            {
                _Tab2 = value; OnPropertyChanged();
            }
        }

        public Visibility Tab3
        {
            get => _Tab3; set
            {
                _Tab3 = value; OnPropertyChanged();
            }
        }
        public Visibility Tab4
        {
            get => _Tab4; set
            {
                _Tab4 = value; OnPropertyChanged();
            }
        }
        public int TabIndex { get; set; }

        private String _TabPostion;
        public String TabPosition
        {
            get => _TabPostion; set
            {
                _TabPostion = value; OnPropertyChanged();
            }
        }



        #endregion

        #region list
        // đang dummy data (đáng lẻ nên lấy data từ đâu đó)
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

        private bool _MNameFieldHasError;
        public bool MNameFieldHasError { get => _MNameFieldHasError; set { _MNameFieldHasError = value; OnPropertyChanged(); } }

        private bool _MNationalityFieldHasError;
        public bool MNationalityFieldHasError { get => _MNationalityFieldHasError; set { _MNationalityFieldHasError = value; OnPropertyChanged(); } }

        private bool _MSexFieldHasError;
        public bool MSexFieldHasError { get => _MSexFieldHasError; set { _MSexFieldHasError = value; OnPropertyChanged(); } }

        private bool _MDateOfBirthFieldHasError;
        public bool MDateOfBirthFieldHasError { get => _MDateOfBirthFieldHasError; set { _MDateOfBirthFieldHasError = value; OnPropertyChanged(); } }

        private bool _MJobTitleFieldHasError;
        public bool MJobTitleFieldHasError { get => _MJobTitleFieldHasError; set { _MJobTitleFieldHasError = value; OnPropertyChanged(); } }

        private bool _MDepartmentFieldHasError;
        public bool MDepartmentFieldHasError { get => _MDepartmentFieldHasError; set { _MDepartmentFieldHasError = value; OnPropertyChanged(); } }

        private bool _MProvinceFieldHasError;
        public bool MProvinceFieldHasError { get => _MProvinceFieldHasError; set { _MProvinceFieldHasError = value; OnPropertyChanged(); } }

        private bool _MDistrictFieldHasError;
        public bool MDistrictFieldHasError { get => _MDistrictFieldHasError; set { _MDistrictFieldHasError = value; OnPropertyChanged(); } }

        private bool _MWardFieldHasError;
        public bool MWardFieldHasError { get => _MWardFieldHasError; set { _MWardFieldHasError = value; OnPropertyChanged(); } }

        private bool _MCitizenIDFieldHasError;
        public bool MCitizenIDFieldHasError { get => _MCitizenIDFieldHasError; set { _MCitizenIDFieldHasError = value; OnPropertyChanged(); } }

        private bool _MPhoneNumberFieldHasError;
        public bool MPhoneNumberFieldHasError { get => _MPhoneNumberFieldHasError; set { _MPhoneNumberFieldHasError = value; OnPropertyChanged(); } }

        #endregion

        #region command
        public ICommand NextTabCommand { get; set; }
        public ICommand PreviousTabCommand { get; set; }
        public ICommand SetUpCommand { get; set; }
        public ICommand EditCommand { get; set; }

        #endregion


        #endregion

        public QuarantineAreaInformationViewModel()
        {
            isDoneSetUp = false;

            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
            Tab3 = Visibility.Hidden;
            Tab4 = Visibility.Hidden;
            TabIndex = 1;
            TabPosition = $"{TabIndex}/4";

            SetUpCommand = new RelayCommand<Window>((p) =>
            {
                if (!QANameFieldHasError && !QAProvinceFieldHasError && !QADistrictFieldHasError && !QAWardFieldHasError && !QATestCycleFieldHasError && !QARequiredDayFieldHasError
                 && !MNameFieldHasError && !MDateOfBirthFieldHasError && !MSexFieldHasError && !MNationalityFieldHasError && !MJobTitleFieldHasError && !MDepartmentFieldHasError
                 && !MProvinceFieldHasError && !MDistrictFieldHasError && !MWardFieldHasError && !MCitizenIDFieldHasError && !MPhoneNumberFieldHasError)
                    return true;
                return false;
            }, (p) =>
            {
                UpdateQuarantineAreaInformation();
                p.Close();
            });

            EditCommand = new RelayCommand<Window>((p) =>
            {
                if (!QANameFieldHasError && !QAProvinceFieldHasError && !QADistrictFieldHasError && !QAWardFieldHasError && !QATestCycleFieldHasError && !QARequiredDayFieldHasError
                 && !MNameFieldHasError && !MDateOfBirthFieldHasError && !MSexFieldHasError && !MNationalityFieldHasError && !MJobTitleFieldHasError && !MDepartmentFieldHasError
                 && !MProvinceFieldHasError && !MDistrictFieldHasError && !MWardFieldHasError && !MCitizenIDFieldHasError && !MPhoneNumberFieldHasError)
                    return true;
                return false;
            }, (p) =>
            {
                EditQuarantineAreaInformation();
                p.Close();
            });

            NextTabCommand = new RelayCommand<Window>((p) =>
            {

                if (TabIndex <= 4) return true;

                return false;
            }, (p) =>
            {
                HandleChangeTab(TabIndex, "next", p);
            });

            PreviousTabCommand = new RelayCommand<Window>((p) =>
            {
                if (TabIndex > 1) return true;
                return false;
            }, (p) =>
            {
                HandleChangeTab(TabIndex, "previous", p);
            });

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

        }

        #region Methods

        void HandleChangeTab(int index, string action, Window p)
        {
            if (action == "next")
            {
                TabIndex++;
            }
            else
            {
                TabIndex--;
            }
            if (TabIndex <= 4)
                TabPosition = $"{TabIndex}/4";

            switch (TabIndex)
            {
                case 1:
                    Tab1 = Visibility.Visible;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Hidden;
                    Tab4 = Visibility.Hidden;
                    break;
                case 2:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Visible;
                    Tab3 = Visibility.Hidden;
                    Tab4 = Visibility.Hidden;
                    break;
                case 3:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Visible;
                    Tab4 = Visibility.Hidden;
                    break;
                case 4:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Hidden;
                    Tab4 = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        void UpdateQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    Address QAreaAddress = new Address()
                    {
                        province = QASelectedProvince,
                        district = QASelectedDistrict,
                        apartmentNumber = QAApartmentNumber,
                        streetName = QAStreetName,
                        ward = QASelectedWard
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

                    DataProvider.ins.db.Addresses.Add(ManagerAddress);
                    DataProvider.ins.db.SaveChanges();

                    Staff Manager = new Staff()
                    {
                        addressID = ManagerAddress.id,
                        citizenID = ManagerCitizenID,
                        dateOfBirth = (DateTime)ManagerDateOfBirth,
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
                        addressID = QAreaAddress.id,
                        name = QAname,
                        testCycle = QATestCycle,
                        requiredDayToFinish = QARequiredDayToFinish,
                        managerID = Manager.id,
                    };

                    DataProvider.ins.db.QuarantineAreas.Add(QuarantineArea);
                    DataProvider.ins.db.SaveChanges();

                    isDoneSetUp = true;

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

        void EditQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var QuarantineArea = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
                    if (QuarantineArea == null) return;

                    var Address = DataProvider.ins.db.Addresses.Where(x => x.id == QuarantineArea.addressID).FirstOrDefault();
                    if (Address == null) return;

                    QuarantineArea.name = QAname;
                    QuarantineArea.testCycle = QATestCycle;
                    QuarantineArea.requiredDayToFinish = QARequiredDayToFinish;

                    Address.apartmentNumber = QAApartmentNumber;
                    Address.streetName = QAStreetName;
                    Address.ward = QASelectedWard;
                    Address.district = QASelectedDistrict;
                    Address.province = QASelectedProvince;

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

        #endregion
    }
}
