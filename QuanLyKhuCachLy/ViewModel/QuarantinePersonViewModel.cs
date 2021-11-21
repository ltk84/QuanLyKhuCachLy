using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Media;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantinePersonViewModel : BaseViewModel
    {
        #region property


        //Searching 
        private string _SearchKey;
        public string SearchKey
        {
            get => _SearchKey; set
            {
                _SearchKey = value;
                OnPropertyChanged();
                FilterListView();
            }
        }



        private QuarantinePerson[] _PeopleListView;
        public QuarantinePerson[] PeopleListView
        {
            get => _PeopleListView; set
            {
                _PeopleListView = value; OnPropertyChanged();
            }
        }


        //Filter

        private string[] _FilterType;
        public string[] FilterType
        {
            get => _FilterType; set
            {
                _FilterType = value; OnPropertyChanged();
            }
        }

        private string[] _FilterProperty;
        public string[] FilterProperty
        {
            get => _FilterProperty; set
            {
                _FilterProperty = value; OnPropertyChanged();
            }
        }


        private string _SelectedFilterType;
        public string SelectedFilterType
        {
            get => _SelectedFilterType; set
            {
                _SelectedFilterType = value;
                OnPropertyChanged();
                getFilterProperty();
            }
        }

        private String _SelectedFilterProperty;
        public String SelectedFilterProperty
        {
            get => _SelectedFilterProperty; set
            {
                _SelectedFilterProperty = value;
                OnPropertyChanged();
                SelectFilterProperty();
            }
        }

        #region UI
        private Visibility _Tab1;
        private Visibility _Tab2;
        private Visibility _TabInformation1;
        private Visibility _TabInformation2;
        private Visibility _Tab3;
        private Visibility _Tab4;
        private Visibility _TabEdit1;
        private Visibility _TabEdit2;
        private Visibility _TabEdit3;
        private Visibility _TabEdit4;
        private Visibility _ButtonReturn;
        private Visibility _ButtonEditReturn;
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

        public Visibility ButtonEditReturn
        {
            get => _ButtonEditReturn; set
            {
                _ButtonEditReturn = value; OnPropertyChanged();
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
        public Visibility Tab4
        {
            get => _Tab4; set
            {
                _Tab4 = value; OnPropertyChanged();
            }
        }
        public Visibility TabEdit1
        {
            get => _TabEdit1; set
            {
                _TabEdit1 = value; OnPropertyChanged();
            }
        }
        public Visibility TabEdit2
        {
            get => _TabEdit2; set
            {
                _TabEdit2 = value; OnPropertyChanged();
            }
        }

        public Visibility TabEdit3
        {
            get => _TabEdit3; set
            {
                _TabEdit3 = value; OnPropertyChanged();
            }
        }
        public Visibility TabEdit4
        {
            get => _TabEdit4; set
            {
                _TabEdit4 = value; OnPropertyChanged();
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

        public int TabEditIndex { get; set; }
        private String _TabEditPostion;
        public String TabEditPosition
        {
            get => _TabEditPostion; set
            {
                _TabEditPostion = value; OnPropertyChanged();
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

        private Model.QuarantineRoom _Room;
        public Model.QuarantineRoom Room
        {
            get => _Room; set
            {
                _Room = value;
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
                    InjectionRecordViewModel.ins.IRQuarantinePersonID = SelectedItem.id;
                    DestinationHistoryViewModel.ins.PersonID = SelectedItem.id;
                    TestingResultViewModel.ins.PersonID = SelectedItem.id;
                    RemainRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms.Where(x => x.id != SelectedItem.roomID && x.QuarantinePersons.Count < x.capacity));
                }
            }
        }

        #region address
        private string _QPStreetName;
        public string QPStreetName { get => _QPStreetName; set { _QPStreetName = value; OnPropertyChanged(); } }

        private string _QPApartmentNumber;
        public string QPApartmentNumber { get => _QPApartmentNumber; set { _QPApartmentNumber = value; OnPropertyChanged(); } }

        private string _QPSelectedProvince;
        public string QPSelectedProvince
        {
            get => _QPSelectedProvince;
            set { _QPSelectedProvince = value; OnPropertyChanged(); InitDistrictList(); }
        }

        private string _QPSelectedWard;
        public string QPSelectedWard
        {
            get => _QPSelectedWard;
            set { _QPSelectedWard = value; OnPropertyChanged(); }
        }

        private string _QPSelectedDistrict;
        public string QPSelectedDistrict
        {
            get => _QPSelectedDistrict;
            set { _QPSelectedDistrict = value; OnPropertyChanged(); InitWardList(); }
        }

        private string _DisplayAddress;
        public string DisplayAddress { get => _DisplayAddress; set { _DisplayAddress = value; OnPropertyChanged(); } }
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

        private string _DisplayHealthInfor;
        public string DisplayHealthInfor
        {
            get => _DisplayHealthInfor; set
            {
                _DisplayHealthInfor = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region child view model

        private InjectionRecordViewModel _InjectionRecordViewModel;
        public InjectionRecordViewModel InjectionRecordViewModel
        {
            get => _InjectionRecordViewModel; set
            {
                _InjectionRecordViewModel = value;
                OnPropertyChanged();
            }
        }

        private DestinationHistoryViewModel _DestinationHistoryViewModel;
        public DestinationHistoryViewModel DestinationHistoryViewModel
        {
            get => _DestinationHistoryViewModel; set
            {
                _DestinationHistoryViewModel = value;
                OnPropertyChanged();
            }
        }

        private TestingResultViewModel _TestingResultViewModel;
        public TestingResultViewModel TestingResultViewModel
        {
            get => _TestingResultViewModel; set
            {
                _TestingResultViewModel = value;
                OnPropertyChanged();
            }
        }
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
        public ICommand ToViewCommand { get; set; }
        public ICommand ToMainCommand { get; set; }

        public ICommand NextTabCommand { get; set; }
        public ICommand PreviousTabCommand { get; set; }

        public ICommand NextTabCommandInformation { get; set; }
        public ICommand PreviousTabCommandInformation { get; set; }

        public ICommand NextTabEditCommand { get; set; }
        public ICommand PreviousTabEditCommand { get; set; }
        public ICommand CompleteQuarantinePersonCommand { get; set; }
        public ICommand RefeshCommand { get; set; }



        #endregion

        #region change room 

        protected Model.QuarantineRoom _NewRoomSelected;
        public Model.QuarantineRoom NewRoomSelected
        {
            get => _NewRoomSelected;
            set
            {
                _NewRoomSelected = value;
                OnPropertyChanged();
                if (_NewRoomSelected != null)
                    ChangeRoom();

            }
        }

        protected ObservableCollection<Model.QuarantineRoom> _RemainRoomList;
        public ObservableCollection<Model.QuarantineRoom> RemainRoomList
        {
            get => _RemainRoomList;
            set
            {
                _RemainRoomList = value;
                OnPropertyChanged();
            }
        }

        #endregion



        public QuarantinePersonViewModel()
        {
            SetDefaultUI();

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
            NextTabEditCommand = new RelayCommand<Window>((p) =>
            {

                if (TabEditIndex < 4) return true;
                return false;
            }, (p) =>
            {
                HandleChangeTabEdit(TabIndex, "next", p);
            });

            PreviousTabEditCommand = new RelayCommand<Window>((p) =>
            {
                if (TabEditIndex > 1) return true;
                return false;
            }, (p) =>
            {
                HandleChangeTabEdit(TabIndex, "previous", p);
            });
            NextTabCommand = new RelayCommand<Window>((p) =>
            {

                if (TabIndex < 4) return true;
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
            RemainRoomList = new ObservableCollection<Model.QuarantineRoom>();
            PeopleListView = QuarantinePersonList.ToArray();

            FilterType = new string[] { "Tất cả", "Giới tính", "Quốc tịch", "Phòng", "Nhóm đối tượng", "Ngày đi", "Ngày đến" };
            SelectedFilterType = "Tất cả";
            SelectedFilterProperty = "Chọn phương thức lọc";
            getFilterProperty();

            SeverityList = new ObservableCollection<Severity>(DataProvider.ins.db.Severities);

            QAInformation = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();

            InjectionRecordViewModel = InjectionRecordViewModel.ins;
            DestinationHistoryViewModel = DestinationHistoryViewModel.ins;
            TestingResultViewModel = TestingResultViewModel.ins;

            NationalityList = new ObservableCollection<string>();

            ProvinceList = new ObservableCollection<string>();

            DistrictList = new ObservableCollection<string>();

            WardList = new ObservableCollection<string>();

            InitNationList();

            InitProvinceList();

            SexList = new ObservableCollection<string>()
            {
                "Nam", "Nữ"
            };


            ToAddManualCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ClearData();
                AddQuarantinedPerson addQuarantinePerson = new AddQuarantinedPerson();
                addQuarantinePerson.ShowDialog();
                ClearData();

                TabIndex = 1;
                Tab1 = Visibility.Visible;
                Tab2 = Visibility.Hidden;
                Tab3 = Visibility.Hidden;
                Tab4 = Visibility.Hidden;
                TabPosition = $"{TabIndex}/4";
            });

            ToAddExcelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddPersonFromExcel();
                //MessageBox.Show(rowCount.ToString());
            });

            ToEditCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                SetSelectedItemToProperty();
                EditQuarantinedPersonInformation editPerson = new EditQuarantinedPersonInformation();
                editPerson.ShowDialog();
                ResetToDeaultTabEditAfterEdit();
            });

            ToViewCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ToDetailPersonTab();
            });

            ToMainCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TabIndexInformation = 1;
                BackToPersonList();
            });


            AddCommand = new RelayCommand<Window>((p) =>
            {
                if (!NameFieldHasError && !NationalityFieldHasError && !SexFieldHasError && !ProvinceFieldHasError && !DistrictFieldHasError && !WardFieldHasError)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                AddQuarantinePerson();
                p.Close();
                TabIndex = 1;
            });

            EditCommand = new RelayCommand<Window>((p) =>
            {
                if (!NameFieldHasError && !NationalityFieldHasError && !SexFieldHasError && !ProvinceFieldHasError && !DistrictFieldHasError && !WardFieldHasError)
                {
                    return true;
                }
                return false;

            }, (p) =>
            {
                EditQuarantinePerson();
                p.Close();
                TabEditIndex = 1;
            });

            DeleteCommand = new RelayCommand<object>((p) => { if (SelectedItem != null) return true; return false; }, (p) =>
            {
                DeleteQuarantinePerson();
                BackToPersonList();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    InjectionRecordViewModel.RollbackTransaction(SelectedItem.id);
                    TestingResultViewModel.RollbackTransaction(SelectedItem.id);
                    DestinationHistoryViewModel.RollbackTransaction(SelectedItem.id);
                }
                p.Close();
            });

            CompleteQuarantinePersonCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem != null && SelectedItem.roomID != null)
                    return true;
                return false;
            }, (p) =>
            {
                CompleteQuarantinePerson();
            });

            RefeshCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                RefeshTab();
            });
        }

        #region method

        protected virtual void ChangeRoom()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    var Room = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == NewRoomSelected.id).FirstOrDefault();
                    if (Room == null) return;

                    Person.roomID = Room.id;

                    DataProvider.ins.db.SaveChanges();

                    NewRoomSelected = null;

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
            SetDefaultUI();
            SelectedItem = null;
        }

        void SetDefaultUI()
        {
            TabList = Visibility.Visible;
            TabInformation = Visibility.Hidden;
            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
            Tab3 = Visibility.Hidden;
            Tab4 = Visibility.Hidden;
            TabEdit1 = Visibility.Visible;
            TabEdit2 = Visibility.Hidden;
            TabEdit3 = Visibility.Hidden;
            TabEdit4 = Visibility.Hidden;
            TabInformation1 = Visibility.Visible;
            TabInformation2 = Visibility.Hidden;
            TabIndexInformation = 1;
            TabPositionInformation = $"{TabIndexInformation}/2";
            ButtonReturn = Visibility.Collapsed;
            ButtonEditReturn = Visibility.Collapsed;
            TabIndex = 1;
            TabPosition = $"{TabIndex}/4";
            TabEditIndex = 1;
            TabEditPosition = $"{TabEditIndex}/4";
        }

        protected virtual void CompleteQuarantinePerson()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    Person.roomID = null;
                    Person.completeQuarantine = true;

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

        void InitNationList()
        {
            foreach (var item in NationViewModel.NationList)
            {
                NationalityList.Add(item.NAME);
            }
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
            AddressViewModel.ProvinceSelectEvent(QPSelectedProvince);
            DistrictList.Clear();
            foreach (var item in AddressViewModel.DistrictList)
            {
                DistrictList.Add(item.name);
            }
        }

        void InitWardList()
        {
            AddressViewModel.DistrictSelectEVent(QPSelectedDistrict);
            WardList.Clear();
            foreach (var item in AddressViewModel.WardList)
            {
                WardList.Add(item.name);
            }
        }

        protected void ResetToDeaultTabEditAfterEdit()
        {
            TabEditIndex = 1;
            TabEdit1 = Visibility.Visible;
            TabEdit2 = Visibility.Hidden;
            TabEdit3 = Visibility.Hidden;
            TabEdit4 = Visibility.Hidden;
            TabEditPosition = $"{TabEditIndex}/4";
        }

        void ToDetailPersonTab()
        {
            TabInformation = Visibility.Visible;
            TabList = Visibility.Hidden;
        }

        void BackToPersonList()
        {
            TabInformation = Visibility.Hidden;
            TabList = Visibility.Visible;
            TabInformation1 = Visibility.Visible;
            TabInformation2 = Visibility.Hidden;
        }

        // Searching
        void FilterListView()
        {
            //SelectedFilterType = "Tất cả";
            if (SearchKey == "")
            {
                PeopleListView = QuarantinePersonList.ToArray();

            }

            else
            {


                PeopleListView = QuarantinePersonList.ToArray();
                String[] Value = new string[PeopleListView.Length];

                for (int i = 0; i < PeopleListView.Length; i++)
                {
                    Value[i] = PeopleListView[i].name?.ToString() + "@@" + PeopleListView[i].citizenID?.ToString() + "@@" + PeopleListView[i].id.ToString() + "@@" + PeopleListView[i].healthInsuranceID?.ToString() + "@@" + PeopleListView[i]?.phoneNumber.ToString() + "@@" + PeopleListView[i].QuarantineRoom?.displayName.ToString();

                }

                PeopleListView = PeopleListView.Where((val, index) => Value[index].Contains(SearchKey)).ToArray();
            }
        }



        void getFilterProperty()
        {
            SelectedFilterProperty = "Tất cả";

            //FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.GetType().GetProperty(SelectedFilterType)).Distinct();
            if (SelectedFilterType == "Tất cả")
            {
                SelectedFilterProperty = "Chọn phương thức lọc";
                FilterProperty = new string[] { "Chọn phương thức lọc" };
            }
            else if (SelectedFilterType == "Giới tính")
            {
                FilterProperty = new string[] { "Nam", "Nữ" };
                SelectedFilterProperty = "Tất cả";
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                FilterProperty = DataProvider.ins.db.QuarantinePersons.Select(person => person.nationality).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Phòng")
            {
                FilterProperty = DataProvider.ins.db.QuarantinePersons.Select(person => person.QuarantineRoom.displayName).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Nhóm đối tượng")
            {
                FilterProperty = DataProvider.ins.db.QuarantinePersons.Select(person => person.Severity.level).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Ngày đi")
            {
                FilterProperty = DataProvider.ins.db.QuarantinePersons.Select(person => person.leaveDate.ToString()).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Ngày đến")
            {
                FilterProperty = DataProvider.ins.db.QuarantinePersons.Select(person => person.arrivedDate.ToString()).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }

            PeopleListView = DataProvider.ins.db.QuarantinePersons.ToArray();
        }

        void SelectFilterProperty()
        {
            if (SelectedFilterType == "Tất cả")
            {
            }
            else if (SelectedFilterType == "Giới tính")
            {
                PeopleListView = DataProvider.ins.db.QuarantinePersons.Where(x => x.sex == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                PeopleListView = DataProvider.ins.db.QuarantinePersons.Where(x => x.nationality == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Phòng")
            {

                PeopleListView = DataProvider.ins.db.QuarantinePersons.Where(x => x.QuarantineRoom.displayName == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Nhóm đối tượng")
            {

                PeopleListView = DataProvider.ins.db.QuarantinePersons.Where(x => x.Severity.level == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Ngày đi")
            {
                PeopleListView = DataProvider.ins.db.QuarantinePersons.Where(x => x.leaveDate.ToString() == SelectedFilterProperty).ToArray();

            }
            else if (SelectedFilterType == "Ngày đến")
            {
                PeopleListView = DataProvider.ins.db.QuarantinePersons.Where(x => x.arrivedDate.ToString() == SelectedFilterProperty).ToArray();

            }


        }


        void AddPersonFromExcel()
        {
            List<Address> listAdress = new List<Address>();
            List<QuarantinePerson> listQuarantinePerson = new List<QuarantinePerson>();
            List<HealthInformation> listHealthInformation = new List<HealthInformation>();
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx;*.xlsm;*xls)|*.xlsx;*.xlsm;*xls|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            else
                return;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            if (xlRange.Cells[1, 1] == null || xlRange.Cells[1, 1].Value2 != "STT" ||
            xlRange.Cells[1, 2] == null || xlRange.Cells[1, 2].Value2 != "Họ và tên" ||
            xlRange.Cells[1, 3] == null || xlRange.Cells[1, 3].Value2 != "Ngày sinh" ||
            xlRange.Cells[1, 4] == null || xlRange.Cells[1, 4].Value2 != "Giới tính" ||
            xlRange.Cells[1, 5] == null || xlRange.Cells[1, 5].Value2 != "Địa chỉ thường trú" ||
            xlRange.Cells[1, 7] == null || xlRange.Cells[1, 7].Value2 != "CMND/CCCD" ||
            xlRange.Cells[1, 8] == null || xlRange.Cells[1, 8].Value2 != "Mã bảo hiểm" ||
            xlRange.Cells[1, 9] == null || xlRange.Cells[1, 9].Value2 != "Quốc tịch" ||
            xlRange.Cells[1, 10] == null || xlRange.Cells[1, 10].Value2 != "SĐT" ||
            xlRange.Cells[1, 11] == null || xlRange.Cells[1, 11].Value2 != "Triệu chứng" ||
            xlRange.Cells[1, 12] == null || xlRange.Cells[1, 12].Value2 != "Nhóm đối tượng" ||
            xlRange.Cells[1, 13] == null || xlRange.Cells[1, 13].Value2 != "Ngày đến")
            {
                MessageBox.Show("Không đúng định dạng file");
                return;
            }
            for (int i = 2; i <= rowCount; i++)
            {
                Address personAddress = new Address();
                QuarantinePerson quarantinePerson = new QuarantinePerson();
                HealthInformation healthInformation = new HealthInformation();
                if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                {
                    quarantinePerson.name = xlRange.Cells[i, 2].Value2.ToString();
                }
                if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                {
                    DateTime birth = DateTime.FromOADate(double.Parse(xlRange.Cells[i, 3].Value2.ToString()));
                    quarantinePerson.dateOfBirth = birth;
                }
                if (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null)
                {
                    quarantinePerson.sex = xlRange.Cells[i, 4].Value2.ToString();
                }
                if (xlRange.Cells[i, 5] != null && xlRange.Cells[i, 5].Value2 != null)
                {
                    string[] arrListStr = xlRange.Cells[i, 5].Value2.ToString().Split(',');
                    personAddress.district = arrListStr[2];
                    personAddress.province = arrListStr[3];
                    personAddress.ward = arrListStr[1];
                    personAddress.streetName = arrListStr[0];
                }
                if (xlRange.Cells[i, 7] != null && xlRange.Cells[i, 7].Value2 != null)
                {
                    quarantinePerson.citizenID = xlRange.Cells[i, 7].Value2.ToString();
                }
                if (xlRange.Cells[i, 8] != null && xlRange.Cells[i, 8].Value2 != null)
                {
                    quarantinePerson.healthInsuranceID = xlRange.Cells[i, 8].Value2.ToString();
                }
                if (xlRange.Cells[i, 9] != null && xlRange.Cells[i, 9].Value2 != null)
                {
                    quarantinePerson.nationality = xlRange.Cells[i, 9].Value2.ToString();
                }
                if (xlRange.Cells[i, 10] != null && xlRange.Cells[i, 10].Value2 != null)
                {
                    quarantinePerson.phoneNumber = xlRange.Cells[i, 10].Value2.ToString();
                }
                if (xlRange.Cells[i, 11] != null && xlRange.Cells[i, 11].Value2 != null)
                {
                    string health = xlRange.Cells[i, 11].Value2.ToString();
                    if (health.Contains("sốt") || health.Contains("Sốt"))
                    {
                        healthInformation.isFever = true;
                    }
                    else healthInformation.isFever = false;
                    if (health.Contains("ho") || health.Contains("Ho"))
                    {
                        healthInformation.isCough = true;
                    }
                    else healthInformation.isCough = false;
                    if (health.Contains("đau họng") || health.Contains("Đau họng"))
                    {
                        healthInformation.isSoreThroat = true;
                    }
                    else healthInformation.isSoreThroat = false;
                    if (health.Contains("mất vị giác") || health.Contains("Mất vị giác"))
                    {
                        healthInformation.isLossOfTatse = true;
                    }
                    else healthInformation.isLossOfTatse = false;
                    if (health.Contains("mệt mỏi") || health.Contains("Mệt mỏi"))
                    {
                        healthInformation.isTired = true;
                    }
                    else healthInformation.isTired = false;
                    if (health.Contains("khó thở") || health.Contains("Khó thở"))
                    {
                        healthInformation.isShortnessOfBreath = true;
                    }
                    else healthInformation.isShortnessOfBreath = false;
                    if (health.Contains("khác") || health.Contains("Khác"))
                    {
                        healthInformation.isOtherSymptoms = true;
                    }
                    else healthInformation.isOtherSymptoms = false;
                    if (health.Contains("có bệnh nền") || health.Contains("Có bệnh nền"))
                    {
                        healthInformation.isFever = true;
                    }
                    else healthInformation.isFever = false;
                }
                if (xlRange.Cells[i, 12] != null && xlRange.Cells[i, 12].Value2 != null)
                {
                    quarantinePerson.levelID = Int32.Parse(xlRange.Cells[i, 12].Value2.ToString());
                }
                if (xlRange.Cells[i, 13] != null && xlRange.Cells[i, 13].Value2 != null)
                {

                    DateTime arrivedTime = DateTime.FromOADate(double.Parse(xlRange.Cells[i, 13].Value2.ToString()));
                    quarantinePerson.arrivedDate = arrivedTime;
                }
                listAdress.Add(personAddress);
                listHealthInformation.Add(healthInformation);
                listQuarantinePerson.Add(quarantinePerson);
            }
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var temptQAInformation = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
                    if (temptQAInformation == null) return;
                    QAInformation = temptQAInformation;

                    for (int i = 0; i < listQuarantinePerson.Count; i++)
                    {
                        DataProvider.ins.db.Addresses.Add(listAdress[i]);
                        DataProvider.ins.db.SaveChanges();
                        listQuarantinePerson[i].leaveDate = listQuarantinePerson[i].arrivedDate.AddDays(QAInformation.requiredDayToFinish);
                        listQuarantinePerson[i].addressID = listAdress[i].id;
                        DataProvider.ins.db.QuarantinePersons.Add(listQuarantinePerson[i]);
                        DataProvider.ins.db.SaveChanges();
                        QuarantinePersonList.Add(listQuarantinePerson[i]);
                        listHealthInformation[i].quarantinePersonID = listQuarantinePerson[i].id;
                        DataProvider.ins.db.HealthInformations.Add(listHealthInformation[i]);
                        DataProvider.ins.db.SaveChanges();
                    }
                    PeopleListView = DataProvider.ins.db.QuarantinePersons.ToArray();
                    QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
                    Window SuccessDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new SuccessNotification()
                    };


                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();

                    //MessageBox.Show("Đã thêm từ file excel");

                    SuccessDialog.ShowDialog();

                    //DashboardViewModel.ins.Init();
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
        protected void InitDisplayAddress(Address PersonAddress)
        {
            if (PersonAddress == null) return;
            List<string> list = new List<string>()
            {
                PersonAddress.apartmentNumber,
                PersonAddress.streetName,
                PersonAddress.ward,
                PersonAddress.district,
                PersonAddress.province

            };

            DisplayAddress = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(list[i]))
                {
                    DisplayAddress += list[i];
                }
                if (i != list.Count - 1)
                {
                    if (i != 0)
                        DisplayAddress += ", ";
                    else DisplayAddress += " ";
                }
            }
        }

        protected void InitDisplayHealthInformation(HealthInformation HF)
        {
            DisplayHealthInfor = string.Empty;

            if (HF == null) return;
            if (HF.isCough) DisplayHealthInfor += "Ho, ";
            if (HF.isDisease) DisplayHealthInfor += "Có bệnh nền, ";
            if (HF.isFever) DisplayHealthInfor += "Sốt, ";
            if (HF.isLossOfTatse) DisplayHealthInfor += "Mất mùi vị, ";
            if (HF.isShortnessOfBreath) DisplayHealthInfor += "Khó thở, ";
            if (HF.isSoreThroat) DisplayHealthInfor += "Đau họng, ";
            if (HF.isTired) DisplayHealthInfor += "Mệt mỏi, ";
            if (HF.isOtherSymptoms) DisplayHealthInfor += "Triệu chứng khác";
            else
            {
                if (DisplayHealthInfor != "")
                {
                    DisplayHealthInfor = DisplayHealthInfor.Remove(DisplayHealthInfor.LastIndexOf(","));
                }
            }
        }

        void SetSelectedItemToProperty()
        {
            var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
            var PersonAddress = DataProvider.ins.db.Addresses.Where(x => x.id == Person.addressID).FirstOrDefault();
            var HealthInfor = DataProvider.ins.db.HealthInformations.Where(x => x.quarantinePersonID == Person.id).FirstOrDefault();
            var PersonSeverity = DataProvider.ins.db.Severities.Where(x => x.id == Person.levelID).FirstOrDefault();
            var PersonRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == Person.roomID).FirstOrDefault();

            if (PersonAddress != null)
            {
                QPSelectedProvince = PersonAddress.province;
                QPSelectedDistrict = PersonAddress.district;
                QPSelectedWard = PersonAddress.ward;
                QPStreetName = PersonAddress.streetName;
                QPApartmentNumber = PersonAddress.apartmentNumber;
            }

            if (HealthInfor != null)
            {
                IsCough = HealthInfor.isCough;
                IsDisease = HealthInfor.isDisease;
                IsFever = HealthInfor.isFever;
                IsLossOfTatse = HealthInfor.isLossOfTatse;
                IsOtherSymptoms = HealthInfor.isOtherSymptoms;
                IsShortnessOfBreath = HealthInfor.isShortnessOfBreath;
                IsSoreThroat = HealthInfor.isSoreThroat;
                IsTired = HealthInfor.isTired;
            }

            QPName = Person.name;
            QPSelectedSex = Person.sex;
            QPDateOfBirth = Person.dateOfBirth;
            QPCitizenID = Person.citizenID;
            QPSelectedNationality = Person.nationality;
            QPPhoneNumber = Person.phoneNumber;
            QPHealthInsuranceID = Person.healthInsuranceID;
            QPSelectedLevel = PersonSeverity;

            SelectedItem.name = Person.name;
            SelectedItem.sex = Person.sex;
            SelectedItem.dateOfBirth = Person.dateOfBirth;
            SelectedItem.citizenID = Person.citizenID;
            SelectedItem.nationality = Person.nationality;
            SelectedItem.phoneNumber = Person.phoneNumber;
            SelectedItem.healthInsuranceID = Person.healthInsuranceID;
            if (PersonSeverity != null) SelectedItem.levelID = PersonSeverity.id;
            SelectedItem.arrivedDate = Person.arrivedDate;
            SelectedItem.leaveDate = Person.leaveDate;
            Room = PersonRoom;
            InitDisplayAddress(PersonAddress);
            InitDisplayHealthInformation(HealthInfor);
        }

        void ClearData()
        {
            QPName = string.Empty;
            QPSelectedSex = string.Empty;
            QPDateOfBirth = DateTime.MinValue;
            QPCitizenID = string.Empty;
            QPSelectedNationality = string.Empty;
            QPPhoneNumber = string.Empty;
            QPHealthInsuranceID = string.Empty;
            QPSelectedProvince = string.Empty;
            QPSelectedDistrict = string.Empty;
            QPSelectedWard = string.Empty;
            QPStreetName = string.Empty;
            QPApartmentNumber = string.Empty;
            IsCough = false;
            IsDisease = false;
            IsFever = false;
            IsLossOfTatse = false;
            IsOtherSymptoms = false;
            IsShortnessOfBreath = false;
            IsSoreThroat = false;
            IsTired = false;
            QPSelectedLevel = null;

            InjectionRecordViewModel.ins.ClearInjectionRecordList();
            TestingResultViewModel.ins.ClearTestingResultList();
            DestinationHistoryViewModel.ins.ClearDestinationHistoryList();
        }

        void AddQuarantinePerson()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
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

                    if (PersonAddress.CheckValidateProperty())
                    {
                        DataProvider.ins.db.Addresses.Add(PersonAddress);
                        DataProvider.ins.db.SaveChanges();
                    }

                    // Tạo thông tin sức khỏe


                    var temptQAInformation = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
                    if (temptQAInformation == null) return;
                    QAInformation = temptQAInformation;

                    // Tạo người cách ly
                    QuarantinePerson Person = new QuarantinePerson()
                    {
                        name = QPName,
                        dateOfBirth = QPDateOfBirth,
                        sex = QPSelectedSex,
                        citizenID = QPCitizenID,
                        nationality = QPSelectedNationality,
                        phoneNumber = QPPhoneNumber,
                        healthInsuranceID = QPHealthInsuranceID,

                        quarantineDays = 0,
                        arrivedDate = DateTime.Today,
                        leaveDate = DateTime.Today.AddDays(QAInformation.requiredDayToFinish),
                    };

                    if (QPSelectedLevel != null) Person.levelID = QPSelectedLevel.id;
                    if (PersonAddress.CheckValidateProperty()) Person.addressID = PersonAddress.id;

                    DataProvider.ins.db.QuarantinePersons.Add(Person);
                    DataProvider.ins.db.SaveChanges();

                    QuarantinePersonList.Add(Person);
                    PeopleListView = QuarantinePersonList.ToArray();

                    HealthInformation PersonHealthInformation = new HealthInformation()
                    {
                        isCough = IsCough,
                        isDisease = IsDisease,
                        isFever = IsFever,
                        isLossOfTatse = IsLossOfTatse,
                        isTired = IsTired,
                        isOtherSymptoms = IsOtherSymptoms,
                        isShortnessOfBreath = IsShortnessOfBreath,
                        isSoreThroat = IsSoreThroat,
                        quarantinePersonID = Person.id,
                    };

                    DataProvider.ins.db.HealthInformations.Add(PersonHealthInformation);
                    DataProvider.ins.db.SaveChanges();

                    InjectionRecordViewModel.ins.ApplyInjectionRecordToDB(Person.id, "Add");
                    TestingResultViewModel.ins.ApplyTestingResultToDb(Person.id, "Add");
                    DestinationHistoryViewModel.ins.ApplayDestinationHistoryToDB(Person.id, "Add");

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
        protected virtual void EditQuarantinePerson()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    QuarantinePerson Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    // Tạo địa chỉ hiện ở của người cách ly
                    Address PersonAddress = DataProvider.ins.db.Addresses.Where(x => x.id == Person.addressID).FirstOrDefault();

                    if (PersonAddress != null)
                    {
                        PersonAddress.apartmentNumber = QPApartmentNumber;
                        PersonAddress.streetName = QPStreetName;
                        PersonAddress.ward = QPSelectedWard;
                        PersonAddress.district = QPSelectedDistrict;
                        PersonAddress.province = QPSelectedProvince;

                        DataProvider.ins.db.SaveChanges();
                    }
                    else
                    {
                        PersonAddress = new Address()
                        {
                            apartmentNumber = QPApartmentNumber,
                            streetName = QPStreetName,
                            ward = QPSelectedWard,
                            district = QPSelectedDistrict,
                            province = QPSelectedProvince
                        };

                        if (PersonAddress.CheckValidateProperty())
                        {
                            DataProvider.ins.db.Addresses.Add(PersonAddress);
                            Person.addressID = PersonAddress.id;
                            DataProvider.ins.db.SaveChanges();
                        }
                    }


                    // Tạo thông tin sức khỏe
                    HealthInformation PersonHealthInformation = DataProvider.ins.db.HealthInformations.Where(x => x.quarantinePersonID == Person.id).FirstOrDefault();

                    if (PersonHealthInformation != null)
                    {
                        PersonHealthInformation.isCough = IsCough;
                        PersonHealthInformation.isDisease = IsDisease;
                        PersonHealthInformation.isFever = IsFever;
                        PersonHealthInformation.isLossOfTatse = IsLossOfTatse;
                        PersonHealthInformation.isTired = IsTired;
                        PersonHealthInformation.isOtherSymptoms = IsOtherSymptoms;
                        PersonHealthInformation.isShortnessOfBreath = IsShortnessOfBreath;
                        PersonHealthInformation.isSoreThroat = IsSoreThroat;

                        DataProvider.ins.db.SaveChanges();
                    }

                    // Tạo người cách ly
                    Person.name = QPName;
                    Person.dateOfBirth = QPDateOfBirth;
                    Person.sex = QPSelectedSex;
                    Person.citizenID = QPCitizenID;
                    Person.nationality = QPSelectedNationality;
                    Person.phoneNumber = QPPhoneNumber;
                    Person.healthInsuranceID = QPHealthInsuranceID;

                    if (QPSelectedLevel != null) Person.levelID = QPSelectedLevel.id;

                    DataProvider.ins.db.SaveChanges();

                    InitDisplayAddress(PersonAddress);
                    InitDisplayHealthInformation(PersonHealthInformation);

                    InjectionRecordViewModel.ApplyInjectionRecordToDB(Person.id, "EditOrDelete");
                    TestingResultViewModel.ApplyTestingResultToDb(Person.id, "EditOrDelete");
                    DestinationHistoryViewModel.ins.ApplayDestinationHistoryToDB(Person.id, "EditOrDelete");

                    transaction.Commit();

                    QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
                    PeopleListView = QuarantinePersonList.ToArray();

                    //SelectedItem = Person;

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
        void DeleteQuarantinePerson()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    DataProvider.ins.db.QuarantinePersons.Remove(Person);
                    DataProvider.ins.db.SaveChanges();

                    QuarantinePersonList.Remove(Person);

                    PeopleListView = QuarantinePersonList.ToArray();

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
                    ButtonReturn = Visibility.Collapsed;

                    break;
                case 2:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Visible;
                    Tab3 = Visibility.Hidden;
                    Tab4 = Visibility.Hidden;
                    ButtonReturn = Visibility.Visible;
                    break;
                case 3:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Visible;
                    Tab4 = Visibility.Hidden;
                    ButtonReturn = Visibility.Visible;
                    break;
                case 4:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Hidden;
                    Tab3 = Visibility.Hidden;
                    Tab4 = Visibility.Visible;
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
        void HandleChangeTabEdit(int index, string action, Window p)
        {
            if (action == "next")
            {
                TabEditIndex++;
            }
            else
            {
                TabEditIndex--;
            }
            if (TabEditIndex <= 4)
                TabEditPosition = $"{TabEditIndex}/4";

            switch (TabEditIndex)
            {
                case 1:
                    TabEdit1 = Visibility.Visible;
                    TabEdit2 = Visibility.Hidden;
                    TabEdit3 = Visibility.Hidden;
                    TabEdit4 = Visibility.Hidden;
                    ButtonEditReturn = Visibility.Collapsed;
                    break;
                case 2:
                    TabEdit1 = Visibility.Hidden;
                    TabEdit2 = Visibility.Visible;
                    TabEdit3 = Visibility.Hidden;
                    TabEdit4 = Visibility.Hidden;
                    ButtonEditReturn = Visibility.Visible;
                    break;
                case 3:
                    TabEdit1 = Visibility.Hidden;
                    TabEdit2 = Visibility.Hidden;
                    TabEdit3 = Visibility.Visible;
                    TabEdit4 = Visibility.Hidden;
                    ButtonEditReturn = Visibility.Visible;
                    break;
                default:
                    TabEdit1 = Visibility.Hidden;
                    TabEdit2 = Visibility.Hidden;
                    TabEdit3 = Visibility.Hidden;
                    TabEdit4 = Visibility.Visible;
                    ButtonEditReturn = Visibility.Visible;
                    break;
            }
        }
        #endregion
    }
}
