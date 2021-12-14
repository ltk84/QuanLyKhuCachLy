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
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Data.Entity;
using QuanLyKhuCachLy.Utility;

using System.Globalization;
namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantinePersonViewModel : BaseViewModel
    {
        #region property



        private string[] _ExperationType;
        public string[] ExperationType
        {
            get => _ExperationType; set
            {
                _ExperationType = value; OnPropertyChanged();
            }
        }


        private string _ExperationProperty;
        public string ExperationProperty
        {
            get => _ExperationProperty; set
            {
                _ExperationProperty = value;
                OnPropertyChanged();
                ExperationPropertySelected();
            }
        }

        //Searching 
        private string _SearchKey;
        public string SearchKey
        {
            get => _SearchKey; set
            {
                _SearchKey = value;
                OnPropertyChanged();
                SearchWithKey();
            }
        }



        private QuarantinePerson[] _PeopleListView;
        public QuarantinePerson[] PeopleListView
        {
            get => _PeopleListView; set
            {
                _PeopleListView = value; OnPropertyChanged();
                updateQuarantineStatus();

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
                SearchKey = "";
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
                    InitRemainRoomList();
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

        private DateTime _ConstraintDate;

        public DateTime ConstraintDate
        {
            get { return _ConstraintDate; }
            set { _ConstraintDate = value; OnPropertyChanged(); }
        }


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

        public ICommand ToInportFormGoogleSheet { get; set; }

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
        public ICommand ChangeRoomCommand { get; set; }
        public ICommand ToChangeCompleteDateCommand { get; set; }
        public ICommand CloseChangeCompleteDateCommand { get; set; }
        public ICommand ChangeCompleteDateCommand { get; set; }

        public ICommand ToExportExcel { get; set; }
        public ICommand ToAddTestingResutlFromExcel { get; set; }

        public ICommand ToViewListInGoogleSheet { get; set; }
        public ICommand ToGetFormatExcel { get; set; }

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
                {
                    BufferWindow bufferWindow = new BufferWindow();
                    bufferWindow.ShowDialog();
                    ChangeRoom();
                }

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

            InitBasic();

            InjectionRecordViewModel = InjectionRecordViewModel.ins;
            DestinationHistoryViewModel = DestinationHistoryViewModel.ins;
            TestingResultViewModel = TestingResultViewModel.ins;

            NationalityList = new ObservableCollection<string>();

            ProvinceList = new ObservableCollection<string>();
            DistrictList = new ObservableCollection<string>();

            WardList = new ObservableCollection<string>();

            InitNationList();

            InitProvinceList();

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


            InitPersonList();
            RemainRoomList = new ObservableCollection<Model.QuarantineRoom>();
            PeopleListView = QuarantinePersonList.ToArray();

            ExperationType = new string[] { "Toàn bộ", "Người đang cách ly", "Hoàn thành cách ly" };
            ExperationProperty = "Toàn bộ";
            FilterType = new string[] { "Tất cả", "Giới tính", "Quốc tịch", "Phòng", "Nhóm đối tượng", "Ngày hoàn thành", "Ngày đến", "Đến kì hạn xét nghiệm" };
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

            ToExportExcel = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ExportExcel();
            });

            ToAddTestingResutlFromExcel = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
           {
               var QA = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
               if (QA == null) return;

               ActionConfirmation confirmDialog = new ActionConfirmation();
               var vm = confirmDialog.DataContext as ActionConfirmationViewModel;
               vm.IsThreeButton = false;
               vm.Title = $"Gia hạn thời gian dự kiến hoàn thành cách ly thêm {QA.requiredDayToFinish} ngày";
               vm.Content = "Bạn có muốn thay đổi thời gian dự kiến hoàn thành cách ly của người có kết quả xét nghiệm dương tính?";

               bool result = (bool)confirmDialog.ShowDialog();

               AddTestingResutlFromExcel(result);
           });
            ToInportFormGoogleSheet = new RelayCommand<Window>((p) =>
            {
                return true;
            }, async (p) =>
            {
                await ImportFileFromGoogleSheetAsync();
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
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
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

            ToViewListInGoogleSheet = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                string linkSheet = "";
                linkSheet = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().googleSheetURL;
                if (linkSheet == "" || linkSheet == null)
                {
                    System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1R6zuZB_xFuzWrCnl4j0JLZ3da5HtprRrmjeQ3LdxW44/edit");
                }
                else
                {
                    System.Diagnostics.Process.Start(linkSheet);
                }
            });

            ToGetFormatExcel = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                GetFormatExcel();
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
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                DeleteConfirmation confirmation = new DeleteConfirmation();
                if (confirmation.ShowDialog() == true)
                {
                    DeleteQuarantinePerson();
                    BackToPersonList();
                }
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
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                CompleteQuarantinePerson();
            });

            RefeshCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                RefeshTab();
            });

            ChangeRoomCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem != null && SelectedItem.roomID != null && SelectedItem.leaveDate > DateTime.Today)
                    return true;
                return false;
            }, (p) =>
            {
            });

            ToChangeCompleteDateCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                return false;
            }, (p) =>
            {
                ChangeCompleteDate changeCDDialog = new ChangeCompleteDate();
                changeCDDialog.DataContext = this;
                ConstraintDate = QPArrivedDate;

                changeCDDialog.ShowDialog();
            });

            CloseChangeCompleteDateCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });

            ChangeCompleteDateCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChangeCompleteDate(p);
            });
        }

        #region method

        protected virtual void ChangeCompleteDate(Window p)
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    if (QPLeaveDate <= DateTime.Today && SelectedItem.roomID != null)
                    {
                        ActionConfirmation actionConfirmation = new ActionConfirmation();
                        var vm = actionConfirmation.DataContext as ActionConfirmationViewModel;
                        vm.Title = "Thay đổi thời gian dự kiến hoàn thành";
                        vm.Content = "Ban có muốn xóa người cách ly ra khỏi phòng";
                        var result = actionConfirmation.ShowDialog();
                        if (result == true)
                        {
                            if (vm.IsYes)
                            {
                                SelectedItem.roomID = null;
                            }

                            SelectedItem.leaveDate = QPLeaveDate;
                        }

                        else return;
                    }
                    else
                    {
                        SelectedItem.leaveDate = QPLeaveDate;
                    }

                    p.Close();

                    DataProvider.ins.db.SaveChanges();

                    PeopleListView = QuarantinePersonList.ToArray();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }
        public void updateQuarantineStatus()
        {
            for (int i = 0; i < PeopleListView.Length; i++)
            {
                PeopleListView[i].quarantineStatus = (PeopleListView[i].leaveDate.Date - DateTime.Now.Date).TotalDays <= 0 ? "Đã hoàn thành" : "Đang cách ly";
            }
        }

        public void UpdateQuarantineDaysForPerson()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var p in QuarantinePersonList)
                    {
                        var now = DateTime.Today;
                        if (p.leaveDate > now) return;
                        var numberOfDays = (int)(now - p.arrivedDate).TotalDays;
                        if (p.quarantineDays != numberOfDays) p.quarantineDays = numberOfDays;
                    }

                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

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

                    InitRemainRoomList();

                    PeopleListView = QuarantinePersonList.ToArray();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void RefeshTab()
        {
            SetDefaultUI();
            SelectedItem = null;
            InitBasic();
        }






        void InitBasic()
        {
            InitPersonList();
            RemainRoomList = new ObservableCollection<Model.QuarantineRoom>();
            PeopleListView = QuarantinePersonList.ToArray();

            FilterType = new string[] { "Tất cả", "Giới tính", "Quốc tịch", "Phòng", "Nhóm đối tượng", "Ngày hoàn thành", "Ngày đến", "Đến kì hạn xét nghiệm" };
            SelectedFilterType = "Tất cả";
            SelectedFilterProperty = "Chọn phương thức lọc";
            getFilterProperty();

            SeverityList = new ObservableCollection<Severity>(DataProvider.ins.db.Severities);

            QAInformation = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
        }

        void InitPersonList()
        {
            QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
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

                    if (Person.arrivedDate > DateTime.Today) { throw new InvalidOperationException(); }
                    if (Person.leaveDate > DateTime.Today) Person.leaveDate = DateTime.Today;
                    Person.roomID = null;

                    DataProvider.ins.db.SaveChanges();

                    SelectedItem = Person;

                    PeopleListView = QuarantinePersonList.ToArray();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void InitRemainRoomList()
        {
            RemainRoomList = SelectedItem.Severity == null ?
                        new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms.Where(x => x.id != SelectedItem.roomID && x.QuarantinePersons.Count < x.capacity && x.Severity == null)) :
                    new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms.Where(x => x.id != SelectedItem.roomID && x.QuarantinePersons.Count < x.capacity && x.Severity.id == SelectedItem.Severity.id));
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



        void ExperationPropertySelected()
        {
            var today = DateTime.Now.Date;
            InitPersonList();
            if (ExperationProperty == "Người đang cách ly")
            {
                var tempPeopleArray = QuarantinePersonList.ToArray();
                for (int i = 0; i < tempPeopleArray.Length; i++)
                {
                    if ((today - tempPeopleArray[i].leaveDate.Date).TotalDays > 0) QuarantinePersonList.Remove(tempPeopleArray[i]);
                }

            }

            else if (ExperationProperty == "Hoàn thành cách ly")
            {
                var tempPeopleArray = QuarantinePersonList.ToArray();
                for (int i = 0; i < tempPeopleArray.Length; i++)
                {
                    if ((today - tempPeopleArray[i].leaveDate.Date).TotalDays <= 0) QuarantinePersonList.Remove(tempPeopleArray[i]);
                }
            }

            PeopleListView = QuarantinePersonList.ToArray();
        }

        // Searching
        void SearchWithKey()
        {
            SelectFilterProperty();

            if (SearchKey == "" || SearchKey == null)
            {
            }
            else
            {


                String[] Value = new string[PeopleListView.Length];

                for (int i = 0; i < PeopleListView.Length; i++)
                {
                    Value[i] = PeopleListView[i].name?.ToString() + "@@" + PeopleListView[i].citizenID?.ToString() + "@@" + PeopleListView[i].id.ToString() + "@@" + PeopleListView[i].healthInsuranceID?.ToString() + "@@" + PeopleListView[i]?.phoneNumber.ToString() + "@@" + PeopleListView[i].QuarantineRoom?.displayName.ToString();

                }

                PeopleListView = PeopleListView.Where((val, index) => Value[index].ToUpper().Contains(SearchKey.ToUpper())).ToArray();


            }
        }


        void getFilterProperty()
        {
            SelectedFilterProperty = "";
            SearchKey = "";

            //FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.GetType().GetProperty(SelectedFilterType)).Distinct();
            if (SelectedFilterType == "Tất cả")
            {
                FilterProperty = new string[] { };
            }
            else if (SelectedFilterType == "Giới tính")
            {
                FilterProperty = new string[] { "Nam", "Nữ" };
                SelectedFilterProperty = "Tất cả";
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                FilterProperty = QuarantinePersonList.Select(person => person.nationality).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Phòng")
            {
                FilterProperty = QuarantinePersonList.Select(person => person.QuarantineRoom?.displayName).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Nhóm đối tượng")
            {
                FilterProperty = QuarantinePersonList.Select(person => person.Severity?.description).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Ngày hoàn thành")
            {
                FilterProperty = QuarantinePersonList.Select(person => person.leaveDate.Date.ToString("dd'/'MM'/'yyyy")).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Ngày đến")
            {
                FilterProperty = QuarantinePersonList.Select(person => person.arrivedDate.Date.ToString("dd'/'MM'/'yyyy")).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Đến kì hạn xét nghiệm")
            {
                FilterProperty = new string[] { "Hôm qua", "Hôm nay", "Ngày mai" };
            }
            PeopleListView = QuarantinePersonList.ToArray();

        }




        // Hàm này filter người đền kì hạn xét nghiệm hôm nay, là người còn cách li, có ngầy xét nghiệm gần nhát >= số ngày tối thiểu, ngày cuối, hoặc chưa đc xét nghiệm.
        void FilterPersonIsOnTestingDateToday(DateTime SelectedDate)
        {
            int maxQuarantineDay = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().requiredDayToFinish;
            int testCycle = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().testCycle;
            var tempPeopleList = QuarantinePersonList.ToArray();
            var tempQuarantinePersonList = new ObservableCollection<QuarantinePerson>();
            for (int i = 0; i < tempPeopleList.Length; i++)
            {
                var tempID = tempPeopleList[i].id;
                var TestingResultList = new ObservableCollection<TestingResult>(DataProvider.ins.db.TestingResults.Where(x => x.quarantinePersonID == tempID));

                DateTime max = TestingResultList.Count == 0 ? DateTime.Today : TestingResultList[0].dateTesting;


                // Nếu còn cách li
                if ((SelectedDate - tempPeopleList[i].leaveDate.Date).TotalDays <= 0)
                {

                    // Chưa xét nghiệm lần nào
                    if (TestingResultList.ToArray().Length == 0)
                    {
                        if ((SelectedDate - tempPeopleList[i].arrivedDate.Date).TotalDays >= testCycle)
                        {
                            tempQuarantinePersonList.Add(tempPeopleList[i]);
                        }


                    }
                    // Đã xét nghiệm
                    else if (TestingResultList.ToArray().Length > 0)
                    {
                        for (int j = 1; j < TestingResultList.ToArray().Length; j++)
                            if ((max - TestingResultList[j].dateTesting).TotalDays < 0) max = TestingResultList[j].dateTesting;

                        if ((DateTime.Now.Date - max.Date).TotalDays >= testCycle)
                        {
                            tempQuarantinePersonList.Add(tempPeopleList[i]);
                        }
                    }
                    // Ngày cuối cách ly và chưa xét nghiệm hôm đó :3
                    else if ((SelectedDate - tempPeopleList[i].leaveDate.Date).TotalDays == 0 && max.Date.ToString() != SelectedDate.Date.ToString())
                    {
                        tempQuarantinePersonList.Add(tempPeopleList[i]);
                    }


                }
            }

            //if (SearchKey != "" && SearchKey != null)
            PeopleListView = tempQuarantinePersonList.ToArray();
        }



        void SelectFilterProperty()
        {
            PeopleListView = QuarantinePersonList.ToArray();
            if (SelectedFilterProperty == "" || SelectedFilterProperty == null || SelectedFilterProperty == "Tất cả") return;
            if (SelectedFilterType == "Giới tính")
            {
                PeopleListView = QuarantinePersonList.Where(x => x.sex == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                PeopleListView = QuarantinePersonList.Where(x => x.nationality == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Phòng")
            {

                PeopleListView = QuarantinePersonList.Where(x => x.QuarantineRoom?.displayName == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Nhóm đối tượng")
            {

                PeopleListView = QuarantinePersonList.Where(x => x.Severity?.description == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Ngày hoàn thành")
            {
                PeopleListView = QuarantinePersonList.Where(x => x.leaveDate.ToString("dd'/'MM'/'yyyy") == SelectedFilterProperty).ToArray();

            }
            else if (SelectedFilterType == "Ngày đến")
            {
                PeopleListView = QuarantinePersonList.Where(x => x.arrivedDate.ToString("dd'/'MM'/'yyyy") == SelectedFilterProperty).ToArray();

            }
            else if (SelectedFilterType == "Đến kì hạn xét nghiệm")
            {
                if (SelectedFilterProperty == "Hôm nay")
                {
                    FilterPersonIsOnTestingDateToday(DateTime.Now.Date);
                }
                else if (SelectedFilterProperty == "Hôm qua")
                {
                    FilterPersonIsOnTestingDateToday(DateTime.Today.AddDays(-1).Date);

                }
                else if (SelectedFilterProperty == "Ngày mai")
                {
                    FilterPersonIsOnTestingDateToday(DateTime.Today.AddDays(1).Date);

                }


            }


        }


        async void AddPersonFromExcel()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx;*.xlsm;*xls)|*.xlsx;*.xlsm;*xls|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            else
                return;
            LoadingIndicator loadingIndicator = new LoadingIndicator();
            Task task = ExecuteAddPersonFromExcel(loadingIndicator, path);
            loadingIndicator.ShowDialog();
            await task;
        }

        async Task ExecuteAddPersonFromExcel(LoadingIndicator loadingIndicator, string path)
        {
            bool isSuccess = false;
            string error = "";
            await Task.Run(() =>
            {
                List<Address> listAdress = new List<Address>();
                List<QuarantinePerson> listQuarantinePerson = new List<QuarantinePerson>();
                List<HealthInformation> listHealthInformation = new List<HealthInformation>();
                List<List<InjectionRecord>> listInjectionRecords = new List<List<InjectionRecord>>();
                List<int> ListSTTSheet1 = new List<int>();
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
                xlWorkbook.RefreshAll();
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                if (xlRange.Cells[1, 1] == null || xlRange.Cells[1, 1].Value2.ToString().Trim().ToLower() != "stt" ||
                xlRange.Cells[1, 2] == null || xlRange.Cells[1, 2].Value2.ToString().Trim().ToLower() != "họ và tên" ||
                xlRange.Cells[1, 3] == null || xlRange.Cells[1, 3].Value2.ToString().Trim().ToLower() != "ngày sinh" ||
                xlRange.Cells[1, 4] == null || xlRange.Cells[1, 4].Value2.ToString().Trim().ToLower() != "giới tính" ||
                xlRange.Cells[1, 5] == null || xlRange.Cells[1, 5].Value2.ToString().Trim().ToLower() != "địa chỉ thường trú" ||
                xlRange.Cells[1, 7] == null || xlRange.Cells[1, 7].Value2.ToString().Trim().ToLower() != "cmnd/cccd" ||
                xlRange.Cells[1, 8] == null || xlRange.Cells[1, 8].Value2.ToString().Trim().ToLower() != "mã bảo hiểm" ||
                xlRange.Cells[1, 9] == null || xlRange.Cells[1, 9].Value2.ToString().Trim().ToLower() != "quốc tịch" ||
                xlRange.Cells[1, 10] == null || xlRange.Cells[1, 10].Value2.ToString().Trim().ToLower() != "sđt" ||
                xlRange.Cells[1, 11] == null || xlRange.Cells[1, 11].Value2.ToString().Trim().ToLower() != "triệu chứng" ||
                xlRange.Cells[1, 12] == null || xlRange.Cells[1, 12].Value2.ToString().Trim().ToLower() != "nhóm đối tượng" ||
                xlRange.Cells[1, 13] == null || xlRange.Cells[1, 13].Value2.ToString().Trim().ToLower() != "ngày đến" ||
                xlRange.Cells[1, 14] == null || xlRange.Cells[1, 14].Value2.ToString().Trim().ToLower() != "thông tin tiêm chủng")
                {
                    xlWorkbook.Close();
                    error = "Không đúng định dạng file";
                    return;
                }
                List<int> listSTT = new List<int>();
                for (int i = 2; i <= rowCount; i++)
                {
                    Address personAddress = new Address();
                    QuarantinePerson quarantinePerson = new QuarantinePerson();
                    HealthInformation healthInformation = new HealthInformation();
                    List<InjectionRecord> injectionRecords = new List<InjectionRecord>();
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                    {
                        int t;
                        if (Int32.TryParse(xlRange.Cells[i, 1].Value2.ToString(), out t))
                        {
                            if (t <= 0)
                            {
                                xlWorkbook.Close();
                                error = "STT để bé hơn 1";
                                return;
                            }
                            else
                            {
                                if (listSTT.Contains(t))
                                {
                                    xlWorkbook.Close();
                                    error = "STT " + t.ToString() + " bị trùng";
                                    return;
                                }
                                listSTT.Add(t);
                            }
                            ListSTTSheet1.Add(t);
                        }
                        else
                        {
                            xlWorkbook.Close();
                            error = "STT không là số";
                            return;
                        }
                    }
                    else
                    {
                        xlWorkbook.Close();
                        error = "Số thứ tự để trống";
                        return;
                    }
                    if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                    {
                        quarantinePerson.name = xlRange.Cells[i, 2].Value2.ToString();
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " tên để trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                    {
                        DateTime birth;
                        double date;
                        if (double.TryParse(xlRange.Cells[i, 3].Value2.ToString(), out date))
                        {
                            birth = DateTime.FromOADate(double.Parse(xlRange.Cells[i, 3].Value2.ToString()));
                            quarantinePerson.dateOfBirth = birth;
                        }
                        else
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " sai ngày sinh";
                            xlWorkbook.Close();
                            return;
                        }
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " ngày sinh để trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null)
                    {
                        string sex = xlRange.Cells[i, 4].Value2.ToString().ToLower();
                        if (sex != "nam" && sex != "nữ")
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " giới tính không đúng (chỉ là Nam/Nữ)";
                            xlWorkbook.Close();
                            return;
                        }
                        quarantinePerson.sex = (sex == "nữ" ? "Nữ" : "Nam");
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " giới tính để trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 5] != null && xlRange.Cells[i, 5].Value2 != null)
                    {
                        string[] arrListStr = xlRange.Cells[i, 5].Value2.ToString().Split(',');
                        if (arrListStr.Length < 3)
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " sai địa chỉ";
                            xlWorkbook.Close();
                            return;
                        }
                        if (arrListStr.Length == 3)
                        {
                            personAddress.province = arrListStr[2].Trim();
                            personAddress.district = arrListStr[1].Trim();
                            personAddress.ward = arrListStr[0].Trim();
                        }
                        else
                        {
                            personAddress.province = arrListStr[3].Trim();
                            personAddress.district = arrListStr[2].Trim();
                            personAddress.ward = arrListStr[1].Trim();
                            personAddress.streetName = arrListStr[0].Trim();
                        }
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " địa chỉ để trống";
                        xlWorkbook.Close();
                        return;
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
                        quarantinePerson.nationality = xlRange.Cells[i, 9].Value2.ToString().Trim();
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " quốc tịch để trống";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 10] != null && xlRange.Cells[i, 10].Value2 != null)
                    {
                        int t;
                        if (Int32.TryParse(xlRange.Cells[i, 10].Value2.ToString(), out t))
                        {
                            if (xlRange.Cells[i, 10].Value2.ToString()[0] == '0')
                            {
                                if (xlRange.Cells[i, 10].Value2.ToString().Length <= 10)
                                    quarantinePerson.phoneNumber = xlRange.Cells[i, 10].Value2.ToString();
                                else
                                {
                                    error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " số điện thoại không đúng";
                                    xlWorkbook.Close();
                                    return;
                                }
                            }
                            else
                            {
                                if (xlRange.Cells[i, 10].Value2.ToString().Length <= 9)
                                    quarantinePerson.phoneNumber = "0" + xlRange.Cells[i, 10].Value2.ToString();
                                else
                                {
                                    error = "STT e" + xlRange.Cells[i, 1].Value2.ToString() + " số điện thoại không đúng";
                                    xlWorkbook.Close();
                                    return;
                                }
                            }

                        }
                        else
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " số điện thoại không đúng";
                            xlWorkbook.Close();
                            return;
                        }
                    }
                    else
                    {
                        error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " không có số điện thoại";
                        xlWorkbook.Close();
                        return;
                    }
                    if (xlRange.Cells[i, 11] != null && xlRange.Cells[i, 11].Value2 != null)
                    {
                        string health = xlRange.Cells[i, 11].Value2.ToString().ToLower();
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
                        if (health.Contains("mất vị giác") || health.Contains("mất mùi vị"))
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
                            healthInformation.isDisease = true;
                        }
                        else healthInformation.isDisease = false;
                    }
                    if (xlRange.Cells[i, 12] != null && xlRange.Cells[i, 12].Value2 != null)
                    {
                        string description = xlRange.Cells[i, 12].Value2.ToString();
                        int levelId;
                        bool checkLevel = DataProvider.ins.db.Severities.Where(x => x.description == description).Count() >= 1 ? true : false;
                        if (checkLevel)
                        {
                            levelId = DataProvider.ins.db.Severities.Where(x => x.description == description).FirstOrDefault().id;
                            quarantinePerson.levelID = levelId;
                        }
                        else
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " nhóm đối tượng không đúng";
                            xlWorkbook.Close();
                            return;
                        }
                    }
                    if (xlRange.Cells[i, 13] != null && xlRange.Cells[i, 13].Value2 != null)
                    {
                        DateTime dateTime;
                        double date;
                        if (double.TryParse(xlRange.Cells[i, 13].Value2.ToString(), out date))
                        {
                            dateTime = DateTime.FromOADate(double.Parse(xlRange.Cells[i, 13].Value2.ToString()));
                            quarantinePerson.arrivedDate = dateTime;
                        }
                        else
                        {
                            error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " sai ngày tháng đến";
                            xlWorkbook.Close();
                            return;
                        }
                    }
                    if (xlRange.Cells[i, 14] != null && xlRange.Cells[i, 14].Value2 != null)
                    {

                        var records = xlRange.Cells[i, 14].Value2.ToString().Split(',');
                        for (int j = 0; j < records.Length; j++)
                        {
                            InjectionRecord rc = new InjectionRecord();
                            string[] str = records[j].Trim().Split(' ');
                            if (str.Length >= 2)
                            {
                                DateTime dateTime;
                                if (DateTime.TryParse(str[0].ToString().Trim(), out dateTime))
                                {
                                    dateTime = Convert.ToDateTime(str[0].ToString().Trim());
                                    rc.dateInjection = dateTime;
                                }
                                else
                                {
                                    error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " sai ngày tháng tiêm chủng";
                                    xlWorkbook.Close();
                                    return;
                                }
                                string vaccine = str[1].ToString().Trim();
                                rc.dateInjection = dateTime;
                                rc.vaccineName = vaccine;
                                injectionRecords.Add(rc);
                            }
                            else
                            {
                                error = "STT " + xlRange.Cells[i, 1].Value2.ToString() + " sai thông tin tiêm chủng";
                                xlWorkbook.Close();
                                return;
                            }
                        }
                        if (injectionRecords.Count() == 0)
                        {
                            InjectionRecord rc = new InjectionRecord();
                            rc.vaccineName = "Không có nhá";
                            injectionRecords.Add(rc);
                        }
                    }
                    else
                    {
                        InjectionRecord rc = new InjectionRecord();
                        rc.vaccineName = "Không có nhá";
                        injectionRecords.Add(rc);
                    }
                    listAdress.Add(personAddress);
                    listHealthInformation.Add(healthInformation);
                    listQuarantinePerson.Add(quarantinePerson);
                    listInjectionRecords.Add(injectionRecords);

                }
                List<DestinationHistory> ListDestinationHistories = new List<DestinationHistory>();
                List<Address> ListAddressDestinations = new List<Address>();
                List<int> listSTTSheet2 = new List<int>();
                if (xlWorkbook.Sheets.Count >= 1)
                {
                    Excel._Worksheet xlWorksheet2 = xlWorkbook.Sheets[2];
                    Excel.Range xlRange2 = xlWorksheet2.UsedRange;
                    int rowCount2 = xlRange2.Rows.Count;
                    if (xlRange2.Cells[1, 1] != null && xlRange2.Cells[1, 1].Value2.ToString().Trim().ToLower() == "stt trong ds" &&
                xlRange2.Cells[1, 2] != null && xlRange2.Cells[1, 2].Value2.ToString().Trim().ToLower() == "ngày" &&
                xlRange2.Cells[1, 3] != null && xlRange2.Cells[1, 3].Value2.ToString().Trim().ToLower() == "địa điểm")
                    {
                        for (int i = 2; i <= rowCount2; i++)
                        {
                            DestinationHistory destination = new DestinationHistory();
                            Address address = new Address();
                            if (xlRange2.Cells[i, 1] != null && xlRange2.Cells[i, 1].Value2 != null)
                            {
                                bool checkSTT = false;
                                if (ListSTTSheet1.Contains(Int32.Parse(xlRange2.Cells[i, 1].Value2.ToString())))
                                {
                                    checkSTT = true;
                                }
                                if (checkSTT)
                                {
                                    if (xlRange2.Cells[i, 2] != null && xlRange2.Cells[i, 2].Value2 != null)
                                    {
                                        DateTime dateTime;
                                        double date;
                                        if (double.TryParse(xlRange2.Cells[i, 2].Value2.ToString(), out date))
                                        {
                                            dateTime = DateTime.FromOADate(double.Parse(xlRange2.Cells[i, 2].Value2.ToString()));
                                            destination.dateArrive = dateTime;
                                        }
                                        else
                                        {
                                            error = "STT " + xlRange2.Cells[i, 1].Value2.ToString() + " sai ngày tháng di chuyển";
                                            xlWorkbook.Close();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        error = "STT " + xlRange2.Cells[i, 1].Value2.ToString() + " không có ngày di chuyển";
                                        xlWorkbook.Close();
                                        return;
                                    }
                                    if (xlRange2.Cells[i, 3] != null && xlRange2.Cells[i, 3].Value2 != null)
                                    {
                                        string[] arrListStr = xlRange2.Cells[i, 3].Value2.ToString().Split(',');
                                        if (arrListStr.Length < 3)
                                        {
                                            error = "STT " + xlRange2.Cells[i, 1].Value2.ToString() + " sai địa chỉ di chuyển";
                                            xlWorkbook.Close();
                                            return;
                                        }
                                        if (arrListStr.Length == 3)
                                        {
                                            address.province = arrListStr[2].Trim();
                                            address.district = arrListStr[1].Trim();
                                            address.ward = arrListStr[0].Trim();
                                        }
                                        else
                                        {
                                            address.province = arrListStr[3].Trim();
                                            address.district = arrListStr[2].Trim();
                                            address.ward = arrListStr[1].Trim();
                                            address.streetName = arrListStr[0].Trim();
                                        }
                                    }
                                    else
                                    {
                                        error = "STT " + xlRange2.Cells[i, 1].Value2.ToString() + " không có địa điểm đến";
                                        xlWorkbook.Close();
                                        return;
                                    }
                                    ListAddressDestinations.Add(address);
                                    ListDestinationHistories.Add(destination);
                                    listSTTSheet2.Add(Int32.Parse(xlRange2.Cells[i, 1].Value2.ToString()));
                                }
                            }

                        }
                    }
                }
                xlWorkbook.Close();
                using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
                {

                    List<int> ListIdPerson = new List<int>();
                    for (int i = 0; i < listSTTSheet2.Count; i++)
                    {
                        ListIdPerson.Add(-1);
                    }
                    try
                    {
                        var temptQAInformation = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
                        if (temptQAInformation == null) return;
                        QAInformation = temptQAInformation;

                        for (int i = 0; i < listQuarantinePerson.Count; i++)
                        {
                            DataProvider.ins.db.Addresses.Add(listAdress[i]);
                            DataProvider.ins.db.SaveChanges();
                            listQuarantinePerson[i].leaveDate = listQuarantinePerson[i].arrivedDate.AddDays(QAInformation.requiredDayToFinish).Date;
                            listQuarantinePerson[i].addressID = listAdress[i].id;
                            DataProvider.ins.db.QuarantinePersons.Add(listQuarantinePerson[i]);
                            DataProvider.ins.db.SaveChanges();
                            QuarantinePersonList.Add(listQuarantinePerson[i]);
                            listHealthInformation[i].quarantinePersonID = listQuarantinePerson[i].id;
                            DataProvider.ins.db.HealthInformations.Add(listHealthInformation[i]);
                            DataProvider.ins.db.SaveChanges();
                            for (int j = 0; j < listSTTSheet2.Count; j++)
                            {
                                if (ListSTTSheet1[i] == listSTTSheet2[j])
                                {
                                    ListIdPerson[j] = listQuarantinePerson[i].id;
                                }
                            }
                            if (listInjectionRecords[i][0].vaccineName != "Không có nhá")
                            {
                                for (int j = 0; j < listInjectionRecords[i].Count; j++)
                                {
                                    listInjectionRecords[i][j].quarantinePersonID = listQuarantinePerson[i].id;
                                    DataProvider.ins.db.InjectionRecords.Add(listInjectionRecords[i][j]);
                                    DataProvider.ins.db.SaveChanges();
                                }
                                DataProvider.ins.db.SaveChanges();
                            }
                        }
                        for (int i = 0; i < ListDestinationHistories.Count; i++)
                        {

                            ListDestinationHistories[i].quarantinePersonID = ListIdPerson[i];
                            DataProvider.ins.db.Addresses.Add(ListAddressDestinations[i]);
                            DataProvider.ins.db.SaveChanges();
                            ListDestinationHistories[i].addressID = ListAddressDestinations[i].id;
                            DataProvider.ins.db.DestinationHistories.Add(ListDestinationHistories[i]);
                            DataProvider.ins.db.SaveChanges();

                        }
                        PeopleListView = DataProvider.ins.db.QuarantinePersons.ToArray();
                        InitPersonList();


                        DataProvider.ins.db.SaveChanges();

                        transaction.Commit();

                        //MessageBox.Show("Đã thêm từ file excel");

                        isSuccess = true;

                        //DashboardViewModel.ins.Init();
                    }
                    catch (DbUpdateException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        error = "Lỗi cơ sở dữ liệu cập nhật";
                    }
                    catch (DbEntityValidationException e)
                    {
                        transaction.Rollback();
                        RollBackChange();


                        error = "Lỗi xác thực";

                    }
                    catch (NotSupportedException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        error = "Lỗi database không hỗ trợ";

                    }
                    catch (ObjectDisposedException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        error = "Lỗi đối tượng database bị hủy";
                    }
                    catch (InvalidOperationException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        error = "Lỗi thao tác không hợp lệ";

                    }
                }
            });
            loadingIndicator.Close();
            if (isSuccess)
            {

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
                SuccessDialog.ShowDialog();
            }
            else
            {

                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                if (error != "" && error != null)
                {
                    FailNotificationVM.Content = error;
                }
                ErrorDialog.ShowDialog();
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
            QPArrivedDate = Person.arrivedDate;
            QPLeaveDate = Person.leaveDate;

            //SelectedItem.name = Person.name;
            //SelectedItem.sex = Person.sex;
            //SelectedItem.dateOfBirth = Person.dateOfBirth;
            //SelectedItem.citizenID = Person.citizenID;
            //SelectedItem.nationality = Person.nationality;
            //SelectedItem.phoneNumber = Person.phoneNumber;
            //SelectedItem.healthInsuranceID = Person.healthInsuranceID;
            //if (PersonSeverity != null) SelectedItem.levelID = PersonSeverity.id;
            //SelectedItem.arrivedDate = Person.arrivedDate;
            //SelectedItem.leaveDate = Person.leaveDate;
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
            QPLeaveDate = DateTime.MinValue;
            QPArrivedDate = DateTime.MinValue;

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
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
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

                    DataProvider.ins.db.SaveChanges();

                    if (QPSelectedLevel != null) Person.levelID = QPSelectedLevel.id;

                    DataProvider.ins.db.SaveChanges();

                    InitDisplayAddress(PersonAddress);
                    InitDisplayHealthInformation(PersonHealthInformation);

                    InjectionRecordViewModel.ApplyInjectionRecordToDB(Person.id, "EditOrDelete");
                    TestingResultViewModel.ApplyTestingResultToDb(Person.id, "EditOrDelete");
                    DestinationHistoryViewModel.ins.ApplayDestinationHistoryToDB(Person.id, "EditOrDelete");

                    transaction.Commit();

                    InitPersonList();
                    PeopleListView = QuarantinePersonList.ToArray();

                    SelectedItem = Person;

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
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
                    QuarantinePersonList.Remove(Person);

                    DataProvider.ins.db.SaveChanges();

                    PeopleListView = QuarantinePersonList.ToArray();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollBackChange();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void RollBackChange()
        {
            DBUtilityTracker.Rollback();
            InitPersonList();
            SelectedItem = null;
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
        async System.Threading.Tasks.Task ImportFileFromGoogleSheetAsync()
        {
            string error = "";
            try
            {
                string[] Scopes = { SheetsService.Scope.Spreadsheets };
                string ApplicationName = "QLKCL";
                string linkSheet = "https://docs.google.com/spreadsheets/d/1R6zuZB_xFuzWrCnl4j0JLZ3da5HtprRrmjeQ3LdxW44/edit#gid=0";
                try
                {
                    linkSheet = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().googleSheetURL.ToString();
                }
                catch { }
                var ctrc = linkSheet.Split('/');

                String spreadsheetId = "1R6zuZB_xFuzWrCnl4j0JLZ3da5HtprRrmjeQ3LdxW44";
                if (ctrc.Length >= 2 && ctrc[ctrc.Length - 2] != "" && ctrc[ctrc.Length - 2] != null)
                {
                    spreadsheetId = ctrc[ctrc.Length - 2].ToString();
                }
                String range = "Sheet1";
                string credentialPath = Path.Combine(Environment.CurrentDirectory, ".credentials", ApplicationName);

                UserCredential credential;
                using (var stream =
                    new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets: GoogleClientSecrets.FromStream(stream).Secrets,
                    scopes: Scopes,
                    user: "user",
                    taskCancellationToken: CancellationToken.None,
                    new FileDataStore(credentialPath, true)
                    );
                }
                var service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
                var request =
                        service.Spreadsheets.Values.Get(spreadsheetId, range);
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;
                List<Address> listAdress = new List<Address>();
                List<QuarantinePerson> listQuarantinePerson = new List<QuarantinePerson>();
                List<HealthInformation> listHealthInformation = new List<HealthInformation>();
                List<List<InjectionRecord>> listInjectionRecords = new List<List<InjectionRecord>>();
                int rowCount = values.Count();
                if (rowCount > 1)
                {

                    if (values != null && values.Count > 0)
                    {

                        for (int i = 1; i < rowCount; i++)
                        {

                            List<InjectionRecord> injectionRecords = new List<InjectionRecord>();
                            Address personAddress = new Address();
                            QuarantinePerson quarantinePerson = new QuarantinePerson();
                            HealthInformation healthInformation = new HealthInformation();
                            if (values[i][1] != null)
                            {
                                quarantinePerson.name = values[i][1].ToString();
                            }
                            if (values[i][2] != null)
                            {
                                try
                                {
                                    DateTime birth = Convert.ToDateTime(values[i][2].ToString());
                                    quarantinePerson.dateOfBirth = birth;
                                }
                                catch
                                {
                                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                    FailNotificationVM.Content = values[i][1].ToString() + " ngày sinh bị lỗi \n Lưu ý: chỉnh định dạng ngày tháng của máy thành (MM/dd/yyyy)";
                                    ErrorDialog.ShowDialog();
                                    return;
                                }
                            }

                            if (values[i][3] != null)
                            {
                                quarantinePerson.sex = values[i][3].ToString();
                            }

                            if (values[i][4] != null)
                            {
                                string[] arrListStr = values[i][4].ToString().Trim().Split(',');
                                if (arrListStr.Length < 3)
                                {
                                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                    FailNotificationVM.Content = values[i][1].ToString() + " has error in address";
                                    ErrorDialog.ShowDialog();
                                    return;
                                }
                                if (arrListStr.Length == 3)
                                {
                                    personAddress.province = arrListStr[2].Trim();
                                    personAddress.district = arrListStr[1].Trim();
                                    personAddress.ward = arrListStr[0].Trim();
                                }
                                else
                                {
                                    personAddress.province = arrListStr[3].Trim();
                                    personAddress.district = arrListStr[2].Trim();
                                    personAddress.ward = arrListStr[1].Trim();
                                    personAddress.streetName = arrListStr[0].Trim();
                                }
                            }

                            if (values[i][6] != null)
                            {
                                quarantinePerson.citizenID = values[i][6].ToString();
                            }
                            if (values[i][7] != null)
                            {
                                quarantinePerson.healthInsuranceID = values[i][7].ToString();
                            }
                            if (values[i][8] != null)
                            {
                                quarantinePerson.nationality = values[i][8].ToString().Trim();
                            }
                            if (values[i][9] != null)
                            {
                                quarantinePerson.phoneNumber = "0" + values[i][9].ToString().Trim();
                            }
                            if (values[i][10] != null)
                            {
                                string health = values[i][10].ToString().ToLower();

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
                                if (health.Contains("mất vị giác") || health.Contains("mất mùi vị"))
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
                                    healthInformation.isDisease = true;
                                }
                                else healthInformation.isDisease = false;
                            }
                            if (values[i][11] != null)
                            {
                                string description = values[i][11].ToString();
                                int levelId;
                                bool checkLevel = DataProvider.ins.db.Severities.Where(x => x.description == description).Count() >= 1 ? true : false;
                                if (checkLevel)
                                {
                                    levelId = DataProvider.ins.db.Severities.Where(x => x.description == description).FirstOrDefault().id;
                                    quarantinePerson.levelID = levelId;
                                }
                            }
                            if (values[i][12] != null)
                            {
                                DateTime arrivedTime = Convert.ToDateTime(values[i][12].ToString());
                                quarantinePerson.arrivedDate = arrivedTime;

                            }
                            if (values[i][12] != null)
                            {
                                try
                                {
                                    DateTime arrivedTime = Convert.ToDateTime(values[i][12].ToString());
                                    quarantinePerson.arrivedDate = arrivedTime;
                                }
                                catch
                                {
                                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                    FailNotificationVM.Content = values[i][1].ToString() + " bị lỗi \n Lưu ý: chỉnh định dạng ngày tháng của máy thành (MM/dd/yyyy)";
                                    ErrorDialog.ShowDialog();
                                    return;
                                }
                            }
                            if (values[i][13].ToString() != "Chưa tiêm")
                            {

                                try
                                {
                                    var records = values[i][13].ToString().Split(',');
                                    for (int j = 0; j < records.Length; j++)
                                    {
                                        InjectionRecord rc = new InjectionRecord();
                                        var str = records[j].Split(' ');
                                        DateTime date = Convert.ToDateTime(str[0].ToString());
                                        string vaccine = str[1].ToString();
                                        rc.dateInjection = date;
                                        rc.vaccineName = vaccine;
                                        injectionRecords.Add(rc);
                                    }
                                }
                                catch
                                {
                                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                    FailNotificationVM.Content = values[i][1].ToString() + " bị lỗi \n Lưu ý: chỉnh định dạng ngày tháng của máy thành (MM/dd/yyyy)";
                                    ErrorDialog.ShowDialog();
                                    return;
                                }
                            }
                            else
                            {
                                InjectionRecord rc = new InjectionRecord();
                                rc.vaccineName = "Không có nhá";
                                injectionRecords.Add(rc);
                            }
                            listAdress.Add(personAddress);
                            listHealthInformation.Add(healthInformation);
                            listQuarantinePerson.Add(quarantinePerson);
                            listInjectionRecords.Add(injectionRecords);
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
                                    listQuarantinePerson[i].leaveDate = listQuarantinePerson[i].arrivedDate.AddDays(QAInformation.requiredDayToFinish).Date;
                                    listQuarantinePerson[i].addressID = listAdress[i].id;
                                    DataProvider.ins.db.QuarantinePersons.Add(listQuarantinePerson[i]);
                                    DataProvider.ins.db.SaveChanges();
                                    QuarantinePersonList.Add(listQuarantinePerson[i]);
                                    listHealthInformation[i].quarantinePersonID = listQuarantinePerson[i].id;
                                    DataProvider.ins.db.HealthInformations.Add(listHealthInformation[i]);
                                    DataProvider.ins.db.SaveChanges();

                                    if (listInjectionRecords[i][0].vaccineName != "Không có nhá")
                                    {
                                        for (int j = 0; j < listInjectionRecords[i].Count; j++)
                                        {
                                            listInjectionRecords[i][j].quarantinePersonID = listQuarantinePerson[i].id;
                                            DataProvider.ins.db.InjectionRecords.Add(listInjectionRecords[i][j]);
                                            DataProvider.ins.db.SaveChanges();
                                        }
                                        DataProvider.ins.db.SaveChanges();
                                    }

                                }
                                PeopleListView = DataProvider.ins.db.QuarantinePersons.ToArray();
                                InitPersonList();
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
                                BatchUpdateSpreadsheetRequest content = new BatchUpdateSpreadsheetRequest();
                                Request RequestBody = new Request()
                                {
                                    DeleteDimension = new DeleteDimensionRequest()
                                    {
                                        Range = new DimensionRange()
                                        {
                                            SheetId = 0,
                                            Dimension = "ROWS",
                                            StartIndex = 1,
                                            EndIndex = listQuarantinePerson.Count + 1,
                                        }
                                    }
                                };
                                List<Request> requests = new List<Request>();
                                requests.Add(RequestBody);
                                content.Requests = requests;
                                try
                                {
                                    SpreadsheetsResource.BatchUpdateRequest Deletion = new SpreadsheetsResource.BatchUpdateRequest(service, content, spreadsheetId);
                                    Deletion.Execute();
                                }
                                catch
                                {
                                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                    FailNotificationVM.Content = "Thao tác xóa bị lỗi";
                                    ErrorDialog.ShowDialog();
                                };

                                SuccessDialog.ShowDialog();

                                //DashboardViewModel.ins.Init();
                            }
                            catch (DbUpdateException e)
                            {
                                transaction.Rollback();
                                RollBackChange();

                                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                                ErrorDialog.ShowDialog();
                            }
                            catch (DbEntityValidationException e)
                            {
                                transaction.Rollback();
                                RollBackChange();

                                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                FailNotificationVM.Content = "Lỗi xác thực";
                                ErrorDialog.ShowDialog();
                            }
                            catch (NotSupportedException e)
                            {
                                transaction.Rollback();
                                RollBackChange();

                                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                                ErrorDialog.ShowDialog();
                            }
                            catch (ObjectDisposedException e)
                            {
                                transaction.Rollback();
                                RollBackChange();

                                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                                ErrorDialog.ShowDialog();
                            }
                            catch (InvalidOperationException e)
                            {
                                transaction.Rollback();
                                RollBackChange();

                                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                                FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                                ErrorDialog.ShowDialog();
                            }
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("No data found.");
                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    if (rowCount == 1)
                    {
                        FailNotificationVM.Content = "Không tìm thấy dữ liệu";
                    }
                    ErrorDialog.ShowDialog();
                }
            }
            catch (Exception e)
            {
                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                FailNotificationVM.Content = e.Message;
                ErrorDialog.ShowDialog();
            }
        }
        void ExportExcel()
        {
            int count = PeopleListView.Length;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            Microsoft.Office.Interop.Excel.Workbook file = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            file.Sheets.Add();
            Microsoft.Office.Interop.Excel.Worksheet sheet = file.Worksheets[1];
            Microsoft.Office.Interop.Excel.Worksheet sheet2 = file.Worksheets[2];
            sheet2.Name = "DS ID";
            sheet.Name = "Danh sách người cách ly";
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 25;
            sheet.Columns[3].ColumnWidth = 12;
            sheet.Columns[4].ColumnWidth = 9;
            sheet.Columns[5].ColumnWidth = 50;
            sheet.Columns[6].ColumnWidth = 12;
            sheet.Columns[7].ColumnWidth = 12;
            sheet.Columns[8].ColumnWidth = 10;
            sheet.Columns[9].ColumnWidth = 12;
            sheet.Columns[10].ColumnWidth = 5;
            sheet.Columns[11].ColumnWidth = 12;
            sheet.Columns[12].ColumnWidth = 12;
            sheet.Columns[13].ColumnWidth = 10;
            sheet.Columns[14].ColumnWidth = 7;
            sheet.Columns[15].ColumnWidth = 15;
            sheet.Columns[16].ColumnWidth = 15;
            sheet2.Columns[1].ColumnWidth = 10;
            sheet2.Columns[2].ColumnWidth = 10;
            sheet2.Columns[3].ColumnWidth = 15;
            sheet2.Range["A1"].Value = "ID";
            sheet2.Range["B1"].Value = "Kết quả";
            sheet2.Range["C1"].Value = "Ngày xét nghiệm";
            sheet.Range["A1"].Value = "STT";
            sheet.Range["B1"].Value = "Họ và tên";
            sheet.Range["C1"].Value = "Ngày sinh";
            sheet.Range["D1"].Value = "Giới tính";
            sheet.Range["E1"].Value = "Địa chỉ";
            sheet.Range["F1"].Value = "MaBH";
            sheet.Range["G1"].Value = "CMND/CCCD";
            sheet.Range["H1"].Value = "Quốc tịch";
            sheet.Range["I1"].Value = "SĐT";
            sheet.Range["J1"].Value = "Nhóm đối tượng";
            sheet.Range["K1"].Value = "Ngày đến";
            sheet.Range["L1"].Value = "Ngày hoàn thành";
            sheet.Range["M1"].Value = "Số ngày\ncách ly";
            sheet.Range["N1"].Value = "Phòng";
            sheet.Range["O1"].Value = "Đã hoàn thành\ncách ly";
            sheet.Range["P1"].Value = "Số mũi vaccine\nđã tiêm";

            for (int i = 2; i <= count + 1; i++)
            {
                int addressID = PeopleListView[i - 2].addressID.Value;
                Address address = DataProvider.ins.db.Addresses.Where(x => x.id == addressID).FirstOrDefault();
                String personAddress = "";
                if (address.apartmentNumber != null)
                    personAddress += address.apartmentNumber.ToString();
                if (address.streetName != null)
                    personAddress += " " + address.streetName.ToString();
                if (address.ward != null)
                    personAddress += ", " + address.ward.ToString();
                if (address.district != null)
                    personAddress += ", " + address.district.ToString();
                if (address.province != null)
                    personAddress += ", " + address.province.ToString();
                Severity severity = new Severity();
                if (PeopleListView[i - 2].levelID != null)
                {
                    int severityID = PeopleListView[i - 2].levelID.Value;
                    severity = DataProvider.ins.db.Severities.Where(x => x.id == severityID).FirstOrDefault();
                }
                int personId = PeopleListView[i - 2].id; ;
                int countInjectionRecord = DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == personId).Count();
                QuanLyKhuCachLy.Model.QuarantineRoom room = new Model.QuarantineRoom();
                if (PeopleListView[i - 2].roomID != null)
                {
                    int roomID = PeopleListView[i - 2].roomID.Value;
                    room = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == roomID).FirstOrDefault();
                }
                sheet.Range["A" + i.ToString()].Value = (i - 1).ToString();
                sheet.Range["B" + i.ToString()].Value = PeopleListView[i - 2].name;
                sheet.Range["C" + i.ToString()].Value = PeopleListView[i - 2].dateOfBirth;
                sheet.Range["D" + i.ToString()].Value = PeopleListView[i - 2].sex;
                sheet.Range["E" + i.ToString()].Value = personAddress;
                sheet.Range["F" + i.ToString()].Value = PeopleListView[i - 2].healthInsuranceID;
                sheet.Range["G" + i.ToString()].Value = PeopleListView[i - 2].citizenID;
                sheet.Range["H" + i.ToString()].Value = PeopleListView[i - 2].nationality;
                sheet.Range["I" + i.ToString()].Value = PeopleListView[i - 2].phoneNumber;
                sheet.Range["J" + i.ToString()].Value = severity.description != null ? severity.description : "";
                sheet.Range["K" + i.ToString()].Value = PeopleListView[i - 2].arrivedDate;
                sheet.Range["L" + i.ToString()].Value = PeopleListView[i - 2].leaveDate;
                sheet.Range["M" + i.ToString()].Value = PeopleListView[i - 2].quarantineDays;
                sheet.Range["N" + i.ToString()].Value = PeopleListView[i - 2].roomID != null ? room.displayName : "";
                sheet.Range["O" + i.ToString()].Value = PeopleListView[i - 2].completeQuarantine == true ? "X" : "";
                sheet.Range["P" + i.ToString()].Value = countInjectionRecord;
                sheet2.Range["A" + i.ToString()].Value = PeopleListView[i - 2].id;
            }
            file.Close();
        }

        async void AddTestingResutlFromExcel(bool isExecute)
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx;*.xlsm;*xls)|*.xlsx;*.xlsm;*xls|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            else
                return;
            LoadingIndicator loadingIndicator = new LoadingIndicator();
            Task task = AddTestingResutlFromExcelAsync(loadingIndicator, path, isExecute);
            loadingIndicator.ShowDialog();
            await task;
        }
        async Task AddTestingResutlFromExcelAsync(LoadingIndicator loadingIndicator, string path, bool isExecute)
        {
            bool isSuccess = false;
            string errorMessage = "";
            await Task.Run(() =>
            {
                List<TestingResult> listTestingResults = new List<TestingResult>();
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];

                Excel.Range xlRange = xlWorksheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;
                if (xlRange.Cells[1, 1] == null || xlRange.Cells[1, 1].Value2.ToString().Trim().ToLower() != "id" ||
                xlRange.Cells[1, 2] == null || xlRange.Cells[1, 2].Value2.ToString().Trim().ToLower() != "kết quả" ||
                xlRange.Cells[1, 3] == null || xlRange.Cells[1, 3].Value2.ToString().Trim().ToLower() != "ngày xét nghiệm")
                {
                    if (xlWorkbook.Sheets.Count > 1)
                    {
                        Excel._Worksheet xlWorksheet2 = xlWorkbook.Sheets[2];
                        Excel.Range xlRange2 = xlWorksheet2.UsedRange;

                        if (xlRange2.Cells[1, 1] == null || xlRange2.Cells[1, 1].Value2.ToString().Trim().ToLower() != "id" ||
                        xlRange2.Cells[1, 2] == null || xlRange2.Cells[1, 2].Value2.ToString().Trim().ToLower() != "kết quả" ||
                        xlRange2.Cells[1, 3] == null || xlRange2.Cells[1, 3].Value2.ToString().Trim().ToLower() != "ngày xét nghiệm")
                        {
                            errorMessage = "Không đúng định dạng file";
                            xlWorkbook.Close();
                            return;
                        }
                        else
                        {
                            xlRange = xlWorksheet2.UsedRange;
                            rowCount = xlRange.Rows.Count;
                            colCount = xlRange.Columns.Count;
                        }
                    }
                    else
                    {
                        errorMessage = "Không đúng định dạng file";
                        xlWorkbook.Close();
                        return;
                    }

                }
                for (int i = 2; i <= rowCount; i++)
                {
                    TestingResult testingResult = new TestingResult();
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                    {
                        int t;
                        if (Int32.TryParse(xlRange.Cells[i, 1].Value2.ToString(), out t))
                        {
                            testingResult.quarantinePersonID = Int32.Parse(xlRange.Cells[i, 1].Value2.ToString());
                        }
                        else
                        {
                            errorMessage = "ID bị lỗi";
                            xlWorkbook.Close();
                            return;
                        }
                    }
                    else
                    {
                        xlWorkbook.Close();
                        errorMessage = "ID bị lỗi";
                        return;
                    }
                    if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                    {
                        string temptResult = xlRange.Cells[i, 2].Value2.ToString().ToLower();
                        if (temptResult != "âm tính" && temptResult != "dương tính")
                        {
                            xlWorkbook.Close();
                            errorMessage = "Kết quả bị lỗi";
                            return;
                        }
                        testingResult.isPositive = (temptResult == "âm tính") ? false : true;
                    }
                    else
                    {
                        xlWorkbook.Close();
                        errorMessage = "Kết quả bị lỗi";
                        return;
                    }
                    if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                    {
                        DateTime dateTime;
                        double date;
                        if (double.TryParse(xlRange.Cells[i, 3].Value2.ToString(), out date))
                        {
                            dateTime = DateTime.FromOADate(double.Parse(xlRange.Cells[i, 3].Value2.ToString()));
                            testingResult.dateTesting = dateTime;
                        }
                        else
                        {
                            xlWorkbook.Close();
                            errorMessage = "Ngày bị lỗi";
                            return;
                        }
                    }
                    else
                    {
                        xlWorkbook.Close();
                        errorMessage = "Ngày bị lỗi";
                        return;
                    }
                    listTestingResults.Add(testingResult);
                }
                xlWorkbook.Close();
                using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
                {

                    try
                    {
                        for (int i = 0; i < listTestingResults.Count; i++)
                        {
                            int personID = listTestingResults[i].quarantinePersonID;
                            bool checkID = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == personID).Count() == 1 ? true : false;
                            if (checkID)
                            {
                                DataProvider.ins.db.TestingResults.Add(listTestingResults[i]);
                                UpdateLeaveDateAfterAddTestResult(listTestingResults[i], isExecute);
                            }
                        }
                        DataProvider.ins.db.SaveChanges();
                        transaction.Commit();
                        //MessageBox.Show("Đã thêm từ file excel");
                        isSuccess = true;
                        //DashboardViewModel.ins.Init();
                    }
                    catch (DbUpdateException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        errorMessage = "Lỗi cơ sở dữ liệu cập nhật";
                    }
                    catch (DbEntityValidationException e)
                    {
                        transaction.Rollback();
                        RollBackChange();


                        errorMessage = "Lỗi xác thực";

                    }
                    catch (NotSupportedException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        errorMessage = "Lỗi database không hỗ trợ";

                    }
                    catch (ObjectDisposedException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        errorMessage = "Lỗi đối tượng database bị hủy";
                    }
                    catch (InvalidOperationException e)
                    {
                        transaction.Rollback();
                        RollBackChange();
                        errorMessage = "Lỗi thao tác không hợp lệ";

                    }
                }
            });
            loadingIndicator.Close();
            if (isSuccess)
            {

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
                SuccessDialog.ShowDialog();
            }
            else
            {
                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                if (errorMessage != "" && errorMessage != null)
                {
                    FailNotificationVM.Content = errorMessage;
                }
                ErrorDialog.ShowDialog();

            }

            RefeshTab();
        }

        void UpdateLeaveDateAfterAddTestResult(TestingResult testing, bool isExecute)
        {
            if (!testing.isPositive) return;

            var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == testing.quarantinePersonID).FirstOrDefault();
            if (Person == null) return;

            var QA = DataProvider.ins.db.QuarantineAreas.FirstOrDefault();
            if (QA == null) return;

            var ShouldBeAddDate = testing.dateTesting.AddDays(QA.requiredDayToFinish);
            if (ShouldBeAddDate > Person.leaveDate)
            {
                if (isExecute)
                {
                    Person.leaveDate = ShouldBeAddDate;
                }

            }
        }
        void GetFormatExcel()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            Microsoft.Office.Interop.Excel.Workbook file = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            file.Sheets.Add();
            Microsoft.Office.Interop.Excel.Worksheet sheet = file.Worksheets[1];
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = file.Worksheets[2];

            sheet.Name = "Danh sách người cách ly";
            sheet1.Name = "Lịch sử di chuyển";
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 25;
            sheet.Columns[3].ColumnWidth = 12;
            sheet.Columns[4].ColumnWidth = 9;
            sheet.Columns[5].ColumnWidth = 30;
            sheet.Columns[6].ColumnWidth = 30;
            sheet.Columns[7].ColumnWidth = 12;
            sheet.Columns[8].ColumnWidth = 10;
            sheet.Columns[9].ColumnWidth = 12;
            sheet.Columns[10].ColumnWidth = 10;
            sheet.Columns[11].ColumnWidth = 12;
            sheet.Columns[12].ColumnWidth = 12;
            sheet.Columns[13].ColumnWidth = 10;
            sheet.Columns[14].ColumnWidth = 20;
            sheet.Columns[15].ColumnWidth = 50;
            sheet1.Columns[1].ColumnWidth = 20;
            sheet1.Columns[2].ColumnWidth = 10;
            sheet1.Columns[3].ColumnWidth = 25;
            sheet.Range["A1"].Value = "STT";
            sheet.Range["B1"].Value = "Họ và tên";
            sheet.Range["C1"].Value = "Ngày sinh";
            sheet.Range["D1"].Value = "Giới tính";
            sheet.Range["E1"].Value = "Địa chỉ thường trú";
            sheet.Range["F1"].Value = "Địa chỉ tạm trú";
            sheet.Range["G1"].Value = "CMND/CCCD";
            sheet.Range["H1"].Value = "Mã bảo hiểm";
            sheet.Range["I1"].Value = "Quốc tịch";
            sheet.Range["J1"].Value = "SĐT";
            sheet.Range["K1"].Value = "Triệu chứng";
            sheet.Range["L1"].Value = "Nhóm đối tượng";
            sheet.Range["M1"].Value = "Ngày đến";
            sheet.Range["N1"].Value = "Thông tin tiêm chủng";
            sheet.Range["O2"].Value = "Lưu ý:Các thành phần của điệm điểm cách nhau bởi dấu ','";
            sheet.Range["O3"].Value = "các từ chỉ địa phương ghi hoa chữ đầu.";
            sheet.Range["O4"].Value = "VD: Thôn A,Xã B,Huyện C,Tỉnh D";
            sheet.Range["O6"].Value = "CMND/CCCD, Mã BH, Triệu chứng, nhóm đối tượng,";
            sheet.Range["O7"].Value = "Thông tin tiêm chủng và di chuyển có thể để trống.";
            sheet.Range["O9"].Value = "Giới tính chỉ có thể là Nam/Nữ";
            sheet.Range["O10"].Value = "Với mỗi lần tiêm chủng, ghi theo cú pháp 'ngày vaccine' ";
            sheet.Range["O11"].Value = "Các lần tiêm cách nhau bởi dấu ','";
            sheet.Range["O12"].Value = "Vd: 1/1/2021 Astra,2/4/2021 Astra ";
            sheet.Range["O13"].Value = "Thêm lịch sử di chuyển ở sheet thứ 2";
            sheet.Range["O15"].Value = "Xóa lưu ý này trước khi thêm.";

            sheet1.Range["A1"].Value = "STT trong DS";
            sheet1.Range["B1"].Value = "Ngày";
            sheet1.Range["C1"].Value = "Địa điểm";
            sheet1.Range["D2"].Value = "Lưu ý:Các thảnh phần của điệm điểm cách nhau bởi dấu ','";
            sheet1.Range["D3"].Value = "các từ chỉ địa phương ghi hoa chữ đầu.";
            sheet1.Range["D4"].Value = "VD: Thôn A,Xã B,Huyện C,Tỉnh D";
            sheet1.Range["D6"].Value = "Xóa lưu ý này trước khi thêm.";
            file.Close();
        }
        #endregion
    }
}
