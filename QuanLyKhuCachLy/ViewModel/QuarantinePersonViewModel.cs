﻿using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantinePersonViewModel : BaseViewModel
    {
        #region property

        #region UI
        private Visibility _Tab1;
        private Visibility _Tab2;
        private Visibility _TabInformation1;
        private Visibility _TabInformation2;
        private Visibility _Tab3;
        private Visibility _ButtonReturn;
        private Visibility _TabList;
        private Visibility _TabInformation;
        public Visibility TabList
        {
            get => _TabList; set
            {
                _TabList = value; OnPropertyChanged();
            }
        }
        public Visibility TabInformation
        {
            get => _TabInformation; set
            {
                _TabInformation = value; OnPropertyChanged();
            }
        }
        public Visibility ButtonReturn
        {
            get => _ButtonReturn; set
            {
                _ButtonReturn = value; OnPropertyChanged();
            }
        }
        public Visibility TabInformation1
        {
            get => _TabInformation1; set
            {
                _TabInformation1 = value; OnPropertyChanged();
            }
        }
        public Visibility TabInformation2
        {
            get => _TabInformation2; set
            {
                _TabInformation2 = value; OnPropertyChanged();
            }
        }
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
        public int TabIndex { get; set; }

        private String _TabPostion;
        public String TabPosition
        {
            get => _TabPostion; set
            {
                _TabPostion = value; OnPropertyChanged();
            }
        }

        public int TabIndexInformation { get; set; }

        private String _TabPostionInformation;
        public String TabPositionInformation
        {
            get => _TabPostionInformation; set
            {
                _TabPostionInformation = value; OnPropertyChanged();
            }
        }
        #endregion

        #region Quarantine Person
        private int _QPID;
        public int QPID
        {
            get => _QPID; set
            {
                _QPID = value;
                OnPropertyChanged();
            }
        }

        private string _QPName;
        public string QPName
        {
            get => _QPName; set
            {
                _QPName = value;
                OnPropertyChanged();
            }
        }

        private System.DateTime _QPDateOfBirth;
        public System.DateTime QPDateOfBirth
        {
            get => _QPDateOfBirth; set
            {
                _QPDateOfBirth = value;
                OnPropertyChanged();
            }
        }

        private string _QPSelectedSex;
        public string QPSelectedSex
        {
            get => _QPSelectedSex; set
            {
                _QPSelectedSex = value;
                OnPropertyChanged();
            }
        }

        private string _QPCitizenID;
        public string QPCitizenID
        {
            get => _QPCitizenID; set
            {
                _QPCitizenID = value;
                OnPropertyChanged();
            }
        }

        private string _QPSelectedNationality;
        public string QPSelectedNationality
        {
            get => _QPSelectedNationality; set
            {
                _QPSelectedNationality = value;
                OnPropertyChanged();
            }
        }

        private string _QPHealthInsuranceID;
        public string QPHealthInsuranceID
        {
            get => _QPHealthInsuranceID; set
            {
                _QPHealthInsuranceID = value;
                OnPropertyChanged();
            }
        }

        private string _QPPhoneNumber;
        public string QPPhoneNumber
        {
            get => _QPPhoneNumber; set
            {
                _QPPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        private Severity _QPSelectedLevel;
        public Severity QPSelectedLevel
        {
            get => _QPSelectedLevel; set
            {
                _QPSelectedLevel = value;
                OnPropertyChanged();
            }
        }

        private System.DateTime _QPArrivedDate;
        public System.DateTime QPArrivedDate
        {
            get => _QPArrivedDate; set
            {
                _QPArrivedDate = value;
                OnPropertyChanged();
            }
        }

        private System.DateTime _QPLeaveDate;
        public System.DateTime QPLeaveDate
        {
            get => _QPLeaveDate; set
            {
                _QPLeaveDate = value;
                OnPropertyChanged();
            }
        }

        private int _QPQuarantineDays;
        public int QPQuarantineDays
        {
            get => _QPQuarantineDays; set
            {
                _QPQuarantineDays = value;
                OnPropertyChanged();
            }
        }

        private int _QPAddressID;
        public int QPAddressID
        {
            get => _QPAddressID; set
            {
                _QPAddressID = value;
                OnPropertyChanged();
            }
        }

        private int _QPHealthInformationID;
        public int QPHealthInformationID
        {
            get => _QPHealthInformationID; set
            {
                _QPHealthInformationID = value;
                OnPropertyChanged();
            }
        }

        private int _QPRoomID;
        public int roomID
        {
            get => _QPRoomID; set
            {
                _QPRoomID = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private QuarantinePerson _SelectedItem;
        public QuarantinePerson SelectedItem
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

        #region address
        private string _QPStreetName;
        public string QPStreetName { get => _QPStreetName; set { _QPStreetName = value; OnPropertyChanged(); } }

        private string _QPApartmentNumber;
        public string QPApartmentNumber { get => _QPApartmentNumber; set { _QPApartmentNumber = value; OnPropertyChanged(); } }

        private string _QPSelectedProvince;
        public string QPSelectedProvince { get => _QPSelectedProvince; set { _QPSelectedProvince = value; OnPropertyChanged(); } }

        private string _QPSelectedWard;
        public string QPSelectedWard { get => _QPSelectedWard; set { _QPSelectedWard = value; OnPropertyChanged(); } }

        private string _QPSelectedDistrict;
        public string QPSelectedDistrict { get => _QPSelectedDistrict; set { _QPSelectedDistrict = value; OnPropertyChanged(); } }
        #endregion

        #region health information

        private bool _IsFever;
        public bool IsFever
        {
            get => _IsFever; set
            {
                _IsFever = value;
                OnPropertyChanged();
            }
        }

        private bool _IsCough;
        public bool IsCough
        {
            get => _IsCough; set
            {
                _IsCough = value;
                OnPropertyChanged();
            }
        }

        private bool _IsSoreThroat;
        public bool IsSoreThroat
        {
            get => _IsSoreThroat; set
            {
                _IsSoreThroat = value;
                OnPropertyChanged();
            }
        }

        private bool _IsLossOfTatse;
        public bool IsLossOfTatse
        {
            get => _IsLossOfTatse; set
            {
                _IsLossOfTatse = value;
                OnPropertyChanged();
            }
        }

        private bool _IsTired;
        public bool IsTired
        {
            get => _IsTired; set
            {
                _IsTired = value;
                OnPropertyChanged();
            }
        }

        private bool _IsShortnessOfBreath;
        public bool IsShortnessOfBreath
        {
            get => _IsShortnessOfBreath; set
            {
                _IsShortnessOfBreath = value;
                OnPropertyChanged();
            }
        }

        private bool _IsOtherSymptoms;
        public bool IsOtherSymptoms
        {
            get => _IsOtherSymptoms; set
            {
                _IsOtherSymptoms = value;
                OnPropertyChanged();
            }
        }

        private bool _IsDisease;
        public bool IsDisease
        {
            get => _IsDisease; set
            {
                _IsDisease = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region child view model
        private InjectionRecordViewModel _InjectionRecordViewModel;
        private DestinationHistoryViewModel _DestinationHistoryViewModel;
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

        private bool _SeverityFieldHasError;
        public bool SeverityFieldHasError
        {
            get => _SeverityFieldHasError; set
            {
                _SeverityFieldHasError = value; OnPropertyChanged();
            }
        }
        #endregion

        #region quarantine area information
        private QuarantineArea _QAInformation;
        public QuarantineArea QAInformation
        {
            get => _QAInformation; set
            {
                _QAInformation = value;
                OnPropertyChanged();
            }
        }
        #endregion



        #endregion

        #region list
        private ObservableCollection<QuarantinePerson> _QuarantinePersonList;
        public ObservableCollection<QuarantinePerson> QuarantinePersonList
        {
            get => _QuarantinePersonList; set
            {
                _QuarantinePersonList = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Severity> _SeverityList;
        public ObservableCollection<Severity> SeverityList
        {
            get => _SeverityList; set
            {
                _SeverityList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _SexList;
        public ObservableCollection<string> SexList
        {
            get => _SexList; set
            {
                _SexList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _NationalityList;
        public ObservableCollection<string> NationalityList
        {
            get => _NationalityList; set
            {
                _NationalityList = value;
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
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand ToAddManualCommand { get; set; }
        public ICommand ToAddExcelCommand { get; set; }
        public ICommand ToEditCommand { get; set; }
        public ICommand ToDeleteCommand { get; set; }

        public ICommand NextTabCommand { get; set; }
        public ICommand PreviousTabCommand { get; set; }

        public ICommand NextTabCommandInformation { get; set; }
        public ICommand PreviousTabCommandInformation { get; set; }

        #endregion
        public QuarantinePersonViewModel()
        {
            TabList = Visibility.Visible;
            TabInformation = Visibility.Hidden;
            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
            Tab3 = Visibility.Hidden;
            TabInformation1 = Visibility.Visible;
            TabInformation2 = Visibility.Hidden;
            TabIndexInformation = 1;
            TabPositionInformation = $"{TabIndexInformation}/2";
            ButtonReturn = Visibility.Hidden;
            TabIndex = 1;
            TabPosition = $"{TabIndex}/3";

            NextTabCommandInformation = new RelayCommand<Window>((p) =>
            {

                if (TabIndexInformation < 2) return true;
                return false;
            }, (p) =>
            {
                HandleChangeTabInforamtion(TabIndexInformation, "next", p);
            });

            PreviousTabCommandInformation = new RelayCommand<Window>((p) =>
            {
                if (TabIndexInformation > 1) return true;
                return false;
            }, (p) =>
            {
                HandleChangeTabInforamtion(TabIndexInformation, "previous", p);
            });

            NextTabCommand = new RelayCommand<Window>((p) =>
            {

                if (TabIndex < 3) return true;
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
            QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
            SeverityList = new ObservableCollection<Severity>(DataProvider.ins.db.Severities);

            QAInformation = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();

            _InjectionRecordViewModel = new InjectionRecordViewModel(currentPersonID: QPID);
            _DestinationHistoryViewModel = new DestinationHistoryViewModel(currentPersonID: QPID);

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

            ToAddManualCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddQuarantinedPerson addQuarantinePerson = new AddQuarantinedPerson();
                addQuarantinePerson.ShowDialog();
            });

            ToAddExcelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {

            });

            ToEditCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {

            });


            AddCommand = new RelayCommand<object>((p) =>
            {
                if (!NameFieldHasError && !NationalityFieldHasError && !SexFieldHasError && !ProvinceFieldHasError && !DistrictFieldHasError && !WardFieldHasError && !SeverityFieldHasError)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                AddQuarantinePerson();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                Address PersonAddress = new Address()
                {
                    apartmentNumber = QPApartmentNumber,
                    streetName = QPStreetName,
                    ward = QPSelectedWard,
                    district = QPSelectedDistrict,
                    province = QPSelectedProvince,
                };

                // Tạo người cách ly
                QuarantinePerson Person = new QuarantinePerson()
                {
                    name = QPName,
                    dateOfBirth = QPDateOfBirth,
                    sex = QPSelectedSex,
                    citizenID = QPCitizenID,
                    nationality = QPSelectedNationality,
                    phoneNumber = QPPhoneNumber,
                    healthInsuranceID = QPHealthInsuranceID
                };

                if (Person.CheckValidateProperty() && PersonAddress.CheckValidateProperty())
                    return true;
                return false;

            }, (p) =>
            {
                EditQuarantinePerson();
            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteQuarantinePerson();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }

        #region method
        void SetSelectedItemToProperty()
        {
            QPName = SelectedItem.name;
            QPDateOfBirth = SelectedItem.dateOfBirth;
            QPSelectedNationality = SelectedItem.nationality;
        }

        void AddQuarantinePerson()
        {
            // Tạo địa chỉ hiện ở của người cách ly
            Address PersonAddress = new Address()
            {
                apartmentNumber = QPApartmentNumber,
                streetName = QPStreetName,
                ward = QPSelectedWard,
                district = QPSelectedDistrict,
                province = QPSelectedProvince,
            };

            DataProvider.ins.db.Addresses.Add(PersonAddress);
            DataProvider.ins.db.SaveChanges();

            // Tạo thông tin sức khỏe
            HealthInformation PersonHealthInformation = new HealthInformation()
            {
                isCough = IsCough,
                isDisease = IsDisease,
                isFever = IsFever,
                isLossOfTatse = IsLossOfTatse,
                isTired = IsTired,
                isOtherSymptoms = IsOtherSymptoms,
                isShortnessOfBreath = IsShortnessOfBreath,
                isSoreThroat = IsSoreThroat
            };

            DataProvider.ins.db.HealthInformations.Add(PersonHealthInformation);
            DataProvider.ins.db.SaveChanges();

            // Tạo người cách ly
            QuarantinePerson Person = new QuarantinePerson()
            {
                addressID = PersonAddress.id,
                healthInformationID = PersonHealthInformation.id,
                name = QPName,
                dateOfBirth = QPDateOfBirth,
                sex = QPSelectedSex,
                citizenID = QPCitizenID,
                nationality = QPSelectedNationality,
                phoneNumber = QPPhoneNumber,
                healthInsuranceID = QPHealthInsuranceID,
                level = QPSelectedLevel.level,
                roomID = -1,
                quarantineDays = 0,
                arrivedDate = DateTime.Today,
                leaveDate = DateTime.Today.AddDays(QAInformation.requiredDayToFinish),
            };

            DataProvider.ins.db.QuarantinePersons.Add(Person);
            DataProvider.ins.db.SaveChanges();

        }
        void EditQuarantinePerson()
        {
            QuarantinePerson Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();

            // Tạo địa chỉ hiện ở của người cách ly
            Address PersonAddress = DataProvider.ins.db.Addresses.Where(x => x.id == Person.addressID).FirstOrDefault();

            PersonAddress.apartmentNumber = QPApartmentNumber;
            PersonAddress.streetName = QPStreetName;
            PersonAddress.ward = QPSelectedWard;
            PersonAddress.district = QPSelectedDistrict;
            PersonAddress.province = QPSelectedProvince;

            DataProvider.ins.db.SaveChanges();

            // Tạo thông tin sức khỏe
            HealthInformation PersonHealthInformation = DataProvider.ins.db.HealthInformations.Where(x => x.id == Person.healthInformationID).FirstOrDefault();

            PersonHealthInformation.isCough = IsCough;
            PersonHealthInformation.isDisease = IsDisease;
            PersonHealthInformation.isFever = IsFever;
            PersonHealthInformation.isLossOfTatse = IsLossOfTatse;
            PersonHealthInformation.isTired = IsTired;
            PersonHealthInformation.isOtherSymptoms = IsOtherSymptoms;
            PersonHealthInformation.isShortnessOfBreath = IsShortnessOfBreath;
            PersonHealthInformation.isSoreThroat = IsSoreThroat;

            DataProvider.ins.db.SaveChanges();

            // Tạo người cách ly
            Person.name = QPName;
            Person.dateOfBirth = QPDateOfBirth;
            Person.sex = QPSelectedSex;
            Person.citizenID = QPCitizenID;
            Person.nationality = QPSelectedNationality;
            Person.phoneNumber = QPPhoneNumber;
            Person.healthInsuranceID = QPHealthInsuranceID;

            DataProvider.ins.db.SaveChanges();

            // Tạo thông tin nơi người cách ly đã tới (optional) 
            DestinationHistory PersonDestinationHistory = DataProvider.ins.db.DestinationHistories.Where(x => x.quarantinePersonID == Person.id).FirstOrDefault();

            //PersonDestinationHistory.dateArrive = HDDateArrive;

            //if (PersonDestinationHistory.CheckValidProperty())
            //{
            //    DataProvider.ins.db.SaveChanges();
            //}

            //// Tạo địa chỉ thông tin nơi đã tới (optional) vì là thành phần của thằng nơi đã tới
            //Address DestinationHistoryAddress = DataProvider.ins.db.Addresses.Where(x => x.id == PersonDestinationHistory.addressID).FirstOrDefault();

            //DestinationHistoryAddress.apartmentNumber = HDApartmentNumber;
            //DestinationHistoryAddress.streetName = HDStreetName;
            //DestinationHistoryAddress.ward = HDSelectedWard;
            //DestinationHistoryAddress.district = HDSelectedDistrict;
            //DestinationHistoryAddress.province = HDSelectedProvince;

            //if (DestinationHistoryAddress.CheckValidateProperty())
            //{
            //    DataProvider.ins.db.SaveChanges();
            //}

            //// Tạo thông tin tiêm chủng (optional)
            //InjectionRecord PersonInjectionRecord = DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == Person.id).FirstOrDefault();

            //PersonInjectionRecord.dateInjection = IRDateInjection;
            //PersonInjectionRecord.vaccineName = IRVaccineName;

            //if (PersonInjectionRecord.CheckValidateProperty())
            //{
            //    DataProvider.ins.db.SaveChanges();
            //}

        }
        void DeleteQuarantinePerson() { }

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
            if (TabIndex <= 3)
                TabPosition = $"{TabIndex}/3";

            switch (TabIndex)
            {
                case 1:
                    Tab1 = Visibility.Visible;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Hidden;
                    ButtonReturn = Visibility.Hidden;
                    break;
                case 2:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Visible;
                    Tab3 = Visibility.Hidden;
                    ButtonReturn = Visibility.Visible;
                    break;
                case 3:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Visible;
                    ButtonReturn = Visibility.Visible;
                    break;
                default:

                    p.Close();
                    break;
            }
        }

        void HandleChangeTabInforamtion(int index, string action, Window p)
        {
            if (action == "next")
            {
                TabIndexInformation++;
            }
            else
            {
                TabIndexInformation--;
            }
            if (TabIndexInformation <= 2)
                TabPositionInformation = $"{TabIndexInformation}/2";

            switch (TabIndexInformation)
            {
                case 1:
                    TabInformation1 = Visibility.Visible;
                    TabInformation2 = Visibility.Hidden;
                    break;
                default:
                    TabInformation1 = Visibility.Hidden;
                    TabInformation2 = Visibility.Visible;
                    break;
            }
        }
        #endregion
    }
}
