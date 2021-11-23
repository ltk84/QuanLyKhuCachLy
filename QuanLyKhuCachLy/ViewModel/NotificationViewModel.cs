using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace QuanLyKhuCachLy.ViewModel
{
    public class NotificationViewModel : BaseViewModel
    {

        private String _fullyMessage;
        public string fullyMessage
        {
            get => _fullyMessage; set
            {
                _fullyMessage = value;
                OnPropertyChanged();
            }
        }


        #region SelectPeopleWindown


        private string[] _ExperationType1;
        public string[] ExperationType1
        {
            get => _ExperationType1; set
            {
                _ExperationType1 = value; OnPropertyChanged();
            }
        }


        private string _ExperationProperty1;
        public string ExperationProperty1
        {
            get => _ExperationProperty1; set
            {
                _ExperationProperty1 = value;
                OnPropertyChanged();
                ExperationPropertySelected1();
            }
        }

        private string[] _ExperationType2;
        public string[] ExperationType2
        {
            get => _ExperationType2; set
            {
                _ExperationType2 = value; OnPropertyChanged();
            }
        }


        private string _ExperationProperty2;
        public string ExperationProperty2
        {
            get => _ExperationProperty2; set
            {
                _ExperationProperty2 = value;
                OnPropertyChanged();
                ExperationPropertySelected2();
            }
        }


        private String _countReceiver;
        public string countReceiver
        {
            get => _countReceiver; set
            {
                _countReceiver = value;
                OnPropertyChanged();
            }
        }

        private string _SearchKey1;
        public string SearchKey1
        {
            get => _SearchKey1; set
            {
                _SearchKey1 = value;
                OnPropertyChanged();
                SearchList1();
            }
        }


        private string _SearchKey2;
        public string SearchKey2
        {
            get => _SearchKey2; set
            {
                _SearchKey2 = value;
                OnPropertyChanged();
                SearchList2();
            }
        }






        //Filter

        private string[] _FilterType1;
        public string[] FilterType1
        {
            get => _FilterType1; set
            {
                _FilterType1 = value; OnPropertyChanged();
            }
        }

        private string[] _FilterType2;
        public string[] FilterType2
        {
            get => _FilterType2; set
            {
                _FilterType2 = value; OnPropertyChanged();
            }
        }




        private string[] _FilterProperty1;
        public string[] FilterProperty1
        {
            get => _FilterProperty1; set
            {
                _FilterProperty1 = value; OnPropertyChanged();
            }
        }


        private string[] _FilterProperty2;
        public string[] FilterProperty2
        {
            get => _FilterProperty2; set
            {
                _FilterProperty2 = value; OnPropertyChanged();
            }
        }



        private string _SelectedFilterType1;
        public string SelectedFilterType1
        {
            get => _SelectedFilterType1; set
            {
                _SelectedFilterType1 = value;
                OnPropertyChanged();
                getFilterProperty1();
            }
        }

        private string _SelectedFilterType2;
        public string SelectedFilterType2
        {
            get => _SelectedFilterType2; set
            {
                _SelectedFilterType2 = value;
                OnPropertyChanged();
                getFilterProperty2();
            }
        }


        private string _SelectedFilterProperty1;
        public string SelectedFilterProperty1
        {
            get => _SelectedFilterProperty1; set
            {
                _SelectedFilterProperty1 = value;
                SelectFilterProperty1();

            }
        }

        private string _SelectedFilterProperty2;
        public string SelectedFilterProperty2
        {
            get => _SelectedFilterProperty2; set
            {
                _SelectedFilterProperty2 = value;
                SelectFilterProperty2();

            }
        }







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

        private QuarantinePerson _SelectedItem1;
        public QuarantinePerson SelectedItem1
        {
            get => _SelectedItem1;
            set
            {
                _SelectedItem1 = value;
                OnPropertyChanged();
                if (_SelectedItem1 != null)
                {
                    SetSelectedItemToProperty1();
                    InjectionRecordViewModel.ins.IRQuarantinePersonID = SelectedItem1.id;
                    DestinationHistoryViewModel.ins.PersonID = SelectedItem1.id;
                    TestingResultViewModel.ins.PersonID = SelectedItem1.id;
                }
            }
        }
        private QuarantinePerson _SelectedItem2;
        public QuarantinePerson SelectedItem2
        {
            get => _SelectedItem2; set
            {
                _SelectedItem2 = value;
                OnPropertyChanged();
                if (_SelectedItem2 != null)
                {
                    SetSelectedItemToProperty2();
                    InjectionRecordViewModel.ins.IRQuarantinePersonID = SelectedItem2.id;
                    DestinationHistoryViewModel.ins.PersonID = SelectedItem2.id;
                    TestingResultViewModel.ins.PersonID = SelectedItem2.id;
                }
            }
        }




        private QuarantinePerson[] _PeopleListView1;
        public QuarantinePerson[] PeopleListView1
        {
            get => _PeopleListView1; set
            {
                _PeopleListView1 = value;
                OnPropertyChanged();
            }
        }

        private QuarantinePerson[] _PeopleListView2;
        public QuarantinePerson[] PeopleListView2
        {
            get => _PeopleListView2; set
            {
                _PeopleListView2 = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<QuarantinePerson> _PeopleList1;
        public ObservableCollection<QuarantinePerson> PeopleList1
        {
            get => _PeopleList1; set
            {
                _PeopleList1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<QuarantinePerson> _PeopleList2;
        public ObservableCollection<QuarantinePerson> PeopleList2
        {
            get => _PeopleList2; set
            {
                _PeopleList2 = value;
                OnPropertyChanged();
            }
        }











        public ICommand SelectPerson { get; set; }
        public ICommand DeletePerSon { get; set; }
        public ICommand SelectAllPeople { get; set; }
        public ICommand DeleteAllPeople { get; set; }
        public ICommand ConfirmPeopleList { get; set; }




        #endregion


        #region NotiScreen

        private Visibility _MainTab;

        public Visibility MainTab
        {
            get => _MainTab;
            set
            {
                _MainTab = value;
                OnPropertyChanged();
            }
        }


        private Visibility _TemplateTab;

        public Visibility ManagerTemplateTab
        {
            get => _TemplateTab;
            set
            {
                _TemplateTab = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<NotificationTemplate> _TemplateList;
        public ObservableCollection<NotificationTemplate> TemplateList
        {
            get => _TemplateList; set
            {
                _TemplateList = value; OnPropertyChanged();
            }
        }


        private NotificationTemplate _selectedTemplate;
        public NotificationTemplate selectedTemplate
        {
            get => _selectedTemplate; set
            {
                _selectedTemplate = value; OnPropertyChanged();
                message = _selectedTemplate?.content;
            }
        }


        private string _title;
        public string title
        {
            get => _title; set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        private string _text_btn;
        public string textBtnTitle
        {
            get => _text_btn; set
            {
                _text_btn = value;
                OnPropertyChanged();
            }
        }



        private string _content;
        public string content
        {
            get => _content; set
            {
                _content = value;
                OnPropertyChanged();
            }
        }


        private NotificationPeopleList _ReceiverList;
        public NotificationPeopleList receiverList
        {
            get => _ReceiverList; set
            {
                _ReceiverList = value;
                OnPropertyChanged();
            }
        }


        private AddNewNotificationTemplate _AddTemplate;
        public AddNewNotificationTemplate addTemplate
        {
            get => _AddTemplate; set
            {
                _AddTemplate = value;
                OnPropertyChanged();
            }
        }


        private EditNotificationTemplateWindown _Edittemplate;
        public EditNotificationTemplateWindown editTemplateWindown
        {
            get => _Edittemplate; set
            {
                _Edittemplate = value;
                OnPropertyChanged();
            }
        }



        private string _Message;
        public string message
        {
            get => _Message; set
            {
                _Message = value;
                OnPropertyChanged();
                SetFullyMessage();
            }
        }



        public ICommand SwitchTab { get; set; }
        public ICommand ShowPeopleList { get; set; }
        public ICommand ShowAddNewNT { get; set; }
        public ICommand editTemplateCommand { get; set; }


        #endregion


        #region addTemplate

        public ICommand addNotificationTemplate { get; set; }
        public ICommand editNotificationTemplate { get; set; }
        public ICommand deleteNotificationTemplate { get; set; }
        public ICommand SaveNotificationTemplate { get; set; }
        public ICommand CancelAddTemplate { get; set; }
        public ICommand CancelEditTemplate { get; set; }


        #endregion
        public ICommand SendNotification { get; set; }


        public NotificationViewModel()
        {

            fullyMessage = "";
            TemplateList = new ObservableCollection<NotificationTemplate>(DataProvider.ins.db.NotificationTemplates);
            PeopleList1 = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
            PeopleListView1 = PeopleList1.ToArray();
            PeopleList2 = new ObservableCollection<QuarantinePerson>();
            PeopleListView2 = PeopleList2.ToArray();

            MainTab = Visibility.Hidden;
            ManagerTemplateTab = Visibility.Visible;

            countReceiver = "Danh sách nhận thông báo có " + PeopleList2.ToArray().Length + " người";

            message = title = content = "";
            textBtnTitle = PeopleList2.ToArray().Length > 0 ? "Xem danh sách người nhận thông báo (" + PeopleList2.ToArray().Length + ')' : "Chọn danh sách người nhận thông báo";


            // Filter init
            InitFilter();


            SendNotification = new RelayCommand<object>((p) =>
            {
                if (message == "" || PeopleList2.ToArray().Length == 0) return false;
                return true;
            }, (p) =>
            {
                SendMessage();
            });


            SelectPerson = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                PeopleList2.Add(SelectedItem1);
                PeopleList1.Remove(SelectedItem1);
                InitFilter();
            });

            SelectAllPeople = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                PeopleListView1.ToList().ForEach(PeopleList2.Add);
                PeopleListView1.ToList().ForEach(RemoveItemFormList1);
                InitFilter();
            });

            DeletePerSon = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                PeopleList1.Add(SelectedItem2);
                PeopleList2.Remove(SelectedItem2);

                InitFilter();
            });

            DeleteAllPeople = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                //PeopleList1 = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
                //PeopleList2 = new ObservableCollection<QuarantinePerson>();
                PeopleListView2.ToList().ForEach(PeopleList1.Add);
                PeopleListView2.ToList().ForEach(RemoveItemFormList2);
                InitFilter();
            });



            ConfirmPeopleList = new RelayCommand<NotificationPeopleList>((p) =>
            {
                return true;
            }, (p) =>
            {
                //Close();
                receiverList.Close();
                textBtnTitle = PeopleList2.ToArray().Length > 0 ? "Xem danh sách người nhận thông báo (" + PeopleList2.ToArray().Length + ')' : "Chọn danh sách người nhận thông báo";


                InitFilter();
            });


            SwitchTab = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                //MessageBox.Show("llll");
                if (MainTab == Visibility.Visible)
                {
                    MainTab = Visibility.Hidden;
                    ManagerTemplateTab = Visibility.Visible;
                }
                else
                {
                    MainTab = Visibility.Visible;
                    ManagerTemplateTab = Visibility.Hidden;
                }
                message = "";
            });


            ShowPeopleList = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                receiverList = new NotificationPeopleList();
                receiverList.ShowDialog();
            });


            ShowAddNewNT = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                addTemplate = new AddNewNotificationTemplate();
                addTemplate.ShowDialog();
            });


            editTemplateCommand = new RelayCommand<object>((p) =>
            {
                if (selectedTemplate == null) return false;
                return true;
            }, (p) =>
            {
                editTemplateWindown = new EditNotificationTemplateWindown();
                title = selectedTemplate.name;
                content = selectedTemplate.content;
                editTemplateWindown.ShowDialog();
            });


            SaveNotificationTemplate = new RelayCommand<object>((p) =>
            {
                if (title == "" || content == "") return false;
                return true;
            }, (p) =>
            {
                EditTemplate();
                message = content;
                title = content = "";
                editTemplateWindown.Close();
                TemplateList = new ObservableCollection<NotificationTemplate>(DataProvider.ins.db.NotificationTemplates);

            });



            addNotificationTemplate = new RelayCommand<object>((p) =>
            {
                if (title == "" || content == "") return false;
                return true;
            }, (p) =>
            {
                AddTemplate();
                addTemplate.Close();

            });



            deleteNotificationTemplate = new RelayCommand<object>((p) =>
            {
                if (selectedTemplate == null || selectedTemplate.id < 5) return false;
                return true;
            }, (p) =>
            {
                DeleteTemplate();

            });

            CancelAddTemplate = new RelayCommand<object>((p) =>
            {

                return true;
            }, (p) =>
            {
                title = content = "";
                addTemplate.Close();

            });

            CancelEditTemplate = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

                title = content = "";
                editTemplateWindown.Close();


            });




        }

        #region methodSelectPeopleWindown
        void RemoveItemFormList1(QuarantinePerson temp)
        {
            PeopleList1.Remove(temp);
        }

        void RemoveItemFormList2(QuarantinePerson temp)
        {
            PeopleList2.Remove(temp);
        }



        // Init filter

        void InitFilter()
        {
            FilterType1 = new string[] { "Tất cả", "Giới tính", "Quốc tịch", "Phòng", "Nhóm đối tượng", "Ngày đi", "Ngày đến", "Ngày đến kì hạn xét nghiệm" };
            FilterType2 = new string[] { "Tất cả", "Giới tính", "Quốc tịch", "Phòng", "Nhóm đối tượng", "Ngày đi", "Ngày đến", "Ngày đến kì hạn xét nghiệm" };
            SelectedFilterType1 = "Tất cả";
            SelectedFilterType2 = "Tất cả";
            SelectedFilterProperty1 = "Chọn phương thức lọc";
            SelectedFilterProperty2 = "Chọn phương thức lọc";
            getFilterProperty1();
            getFilterProperty2();

            PeopleListView1 = PeopleList1.ToArray();
            PeopleListView2 = PeopleList2.ToArray();

            countReceiver = "Danh sách nhận thông báo có " + PeopleListView2.ToArray().Length + " người";
            ExperationType1 = new string[] { "Toàn bộ", "Người đang cách ly", "Hoàn thành cách ly" };
            ExperationProperty1 = "Toàn bộ";
            ExperationType2 = new string[] { "Toàn bộ", "Người đang cách ly", "Hoàn thành cách ly" };
            ExperationProperty2 = "Toàn bộ";

        }

        // Searching
        void SearchList1()
        {
            //SelectedFilterType = "Tất cả";
            if (SearchKey1 == "")
            {
                PeopleListView1 = PeopleList1.ToArray();

            }

            else
            {


                var temp = PeopleList1.ToArray();
                String[] Value = new string[temp.Length];

                for (int i = 0; i < PeopleList1.ToArray().Length; i++)
                {
                    Value[i] = temp[i].name?.ToString() + "@@" + temp[i].citizenID?.ToString() + "@@" + temp[i].id.ToString() + "@@" + temp[i].healthInsuranceID?.ToString() + "@@" + temp[i]?.phoneNumber.ToString() + "@@" + temp[i].QuarantineRoom?.displayName.ToString();

                }

                PeopleListView1 = temp.Where((val, index) => Value[index].ToUpper().Contains(SearchKey1.ToUpper())).ToArray();

            }
        }

        void ExperationPropertySelected1()
        {
            var today = DateTime.Now.Date;
            var temp = new ObservableCollection<QuarantinePerson>(PeopleList1);

            if (ExperationProperty1 == "Người đang cách ly")
            {
                var tempPeopleArray = temp.ToArray();
                for (int i = 0; i < tempPeopleArray.Length; i++)
                {
                    if ((today - tempPeopleArray[i].leaveDate).TotalDays > 0) temp.Remove(tempPeopleArray[i]);
                }

            }

            else if (ExperationProperty1 == "Hoàn thành cách ly")
            {
                var tempPeopleArray = temp.ToArray();
                for (int i = 0; i < tempPeopleArray.Length; i++)
                {
                    if ((today - tempPeopleArray[i].leaveDate).TotalDays <= 0) temp.Remove(tempPeopleArray[i]);
                }
            }
            

            PeopleListView1 = temp.ToArray();
        }


        void ExperationPropertySelected2()
        {
            var today = DateTime.Now.Date;
            var temp = new ObservableCollection<QuarantinePerson>(PeopleList2);
            if (ExperationProperty2 == "Người đang cách ly")
            {
                var tempPeopleArray = temp.ToArray();
                for (int i = 0; i < tempPeopleArray.Length; i++)
                {
                    if ((today - tempPeopleArray[i].leaveDate).TotalDays > 0) temp.Remove(tempPeopleArray[i]);
                }

            }

            else if (ExperationProperty2 == "Hoàn thành cách ly")
            {
                var tempPeopleArray = temp.ToArray();
                for (int i = 0; i < tempPeopleArray.Length; i++)
                {
                    if ((today - tempPeopleArray[i].leaveDate).TotalDays <= 0) temp.Remove(tempPeopleArray[i]);
                }
            }

            

            PeopleListView2 = temp.ToArray();
        }

        void SearchList2()
        {
            //SelectedFilterType = "Tất cả";
            if (SearchKey2 == "")
            {
                PeopleListView2 = PeopleList2.ToArray();

            }

            else
            {


                var temp = PeopleList2.ToArray();
                String[] Value = new string[temp.Length];

                for (int i = 0; i < PeopleList2.ToArray().Length; i++)
                {
                    Value[i] = temp[i].name?.ToString() + "@@" + temp[i].citizenID?.ToString() + "@@" + temp[i].id.ToString() + "@@" + temp[i].healthInsuranceID?.ToString() + "@@" + temp[i]?.phoneNumber.ToString() + "@@" + temp[i].QuarantineRoom?.displayName.ToString();

                }

                PeopleListView2 = temp.Where((val, index) => Value[index].ToUpper().Contains(SearchKey2.ToUpper())).ToArray();

            }
        }



        void getFilterProperty1()
        {
            SelectedFilterProperty1 = "Tất cả";

            //FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.GetType().GetProperty(SelectedFilterType)).Distinct();
            if (SelectedFilterType1 == "Tất cả")
            {
                SelectedFilterProperty1 = "Chọn phương thức lọc";
                FilterProperty1 = new string[] { "Chọn phương thức lọc" };
            }
            else if (SelectedFilterType1 == "Giới tính")
            {
                FilterProperty1 = new string[] { "Nam", "Nữ" };
                SelectedFilterProperty1 = "Tất cả";
            }
            else if (SelectedFilterType1 == "Quốc tịch")
            {
                FilterProperty1 = PeopleList1.Select(person => person.nationality).ToArray();
                FilterProperty1 = FilterProperty1.Distinct().ToArray();
            }
            else if (SelectedFilterType1 == "Phòng")
            {
                FilterProperty1 = PeopleList1.Select(person => person.QuarantineRoom.displayName).ToArray();
                FilterProperty1 = FilterProperty1.Distinct().ToArray();
            }
            else if (SelectedFilterType1 == "Nhóm đối tượng")
            {
                FilterProperty1 = PeopleList1.Select(person => person.Severity.level).ToArray();
                FilterProperty1 = FilterProperty1.Distinct().ToArray();
            }
            else if (SelectedFilterType1 == "Ngày đi")
            {
                FilterProperty1 = PeopleList1.Select(person => person.leaveDate.ToString()).ToArray();
                FilterProperty1 = FilterProperty1.Distinct().ToArray();
            }
            else if (SelectedFilterType1 == "Ngày đến")
            {
                FilterProperty1 = PeopleList1.Select(person => person.arrivedDate.ToString()).ToArray();
                FilterProperty1 = FilterProperty1.Distinct().ToArray();
            }
            else if (SelectedFilterType1 == "Ngày đến kì hạn xét nghiệm")
            {
                FilterProperty1 = new string[] { "Hôm qua", "Hôm nay", "Ngày mai" };
            }

            PeopleListView1 = PeopleList1.ToArray();
        }



        void getFilterProperty2()
        {
            SelectedFilterProperty2 = "Tất cả";

            //FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.GetType().GetProperty(SelectedFilterType)).Distinct();
            if (SelectedFilterType2 == "Tất cả")
            {
                SelectedFilterProperty2 = "Chọn phương thức lọc";
                FilterProperty2 = new string[] { "Chọn phương thức lọc" };
            }
            else if (SelectedFilterType2 == "Giới tính")
            {
                FilterProperty2 = new string[] { "Nam", "Nữ" };
                SelectedFilterProperty2 = "Tất cả";
            }
            else if (SelectedFilterType2 == "Quốc tịch")
            {
                FilterProperty2 = PeopleList2.Select(person => person.nationality).ToArray();
                FilterProperty2 = FilterProperty2.Distinct().ToArray();
            }
            else if (SelectedFilterType2 == "Phòng")
            {
                FilterProperty2 = PeopleList2.Select(person => person.QuarantineRoom.displayName).ToArray();
                FilterProperty2 = FilterProperty2.Distinct().ToArray();
            }
            else if (SelectedFilterType2 == "Nhóm đối tượng")
            {
                FilterProperty2 = PeopleList2.Select(person => person.Severity.level).ToArray();
                FilterProperty2 = FilterProperty2.Distinct().ToArray();
            }
            else if (SelectedFilterType2 == "Ngày đi")
            {
                FilterProperty2 = PeopleList2.Select(person => person.leaveDate.ToString()).ToArray();
                FilterProperty2 = FilterProperty2.Distinct().ToArray();
            }
            else if (SelectedFilterType2 == "Ngày đến")
            {
                FilterProperty2 = PeopleList2.Select(person => person.arrivedDate.ToString()).ToArray();
                FilterProperty2 = FilterProperty2.Distinct().ToArray();
            }
            else if (SelectedFilterType2 == "Ngày đến kì hạn xét nghiệm")
            {
                FilterProperty2 = new string[] { "Hôm qua", "Hôm nay", "Ngày mai" };
            }


            PeopleListView2 = PeopleList2.ToArray();
        }

        void SelectFilterProperty1()
        {
            if (SelectedFilterType1 == "Tất cả")
            {
            }
            else if (SelectedFilterType1 == "Giới tính")
            {
                PeopleListView1 = PeopleList1.Where(x => x.sex == SelectedFilterProperty1).ToArray();
            }
            else if (SelectedFilterType1 == "Quốc tịch")
            {
                PeopleListView1 = PeopleList1.Where(x => x.nationality == SelectedFilterProperty1).ToArray();
            }
            else if (SelectedFilterType1 == "Phòng")
            {

                PeopleListView1 = PeopleList1.Where(x => x.QuarantineRoom?.displayName == SelectedFilterProperty1).ToArray();
            }
            else if (SelectedFilterType1 == "Nhóm đối tượng")
            {

                PeopleListView1 = PeopleList1.Where(x => x.Severity?.level == SelectedFilterProperty1).ToArray();
            }
            else if (SelectedFilterType1 == "Ngày đi")
            {
                PeopleListView1 = PeopleList1.Where(x => x.leaveDate.ToString() == SelectedFilterProperty1).ToArray();

            }
            else if (SelectedFilterType1 == "Ngày đến")
            {
                PeopleListView1 = PeopleList1.Where(x => x.arrivedDate.ToString() == SelectedFilterProperty1).ToArray();
            }
            else if (SelectedFilterType1 == "Ngày đến kì hạn xét nghiệm")
            {
                if (SelectedFilterProperty1 == "Hôm nay")
                {
                    FilterPersonIsOnTestingDateToday1(DateTime.Now.Date);
                }
                else if (SelectedFilterProperty1 == "Hôm qua")
                {
                    FilterPersonIsOnTestingDateToday1(DateTime.Today.AddDays(-1).Date);

                }
                else if (SelectedFilterProperty1 == "Ngày mai")
                {
                    FilterPersonIsOnTestingDateToday1(DateTime.Today.AddDays(1).Date);

                }


            }



        }


        void SelectFilterProperty2()
        {
            if (SelectedFilterType2 == "Tất cả")
            {
            }
            else if (SelectedFilterType2 == "Giới tính")
            {
                PeopleListView2 = PeopleList2.Where(x => x.sex == SelectedFilterProperty2).ToArray();
            }
            else if (SelectedFilterType2 == "Quốc tịch")
            {
                PeopleListView2 = PeopleList2.Where(x => x.nationality == SelectedFilterProperty2).ToArray();
            }
            else if (SelectedFilterType2 == "Phòng")
            {

                PeopleListView2 = PeopleList2.Where(x => x.QuarantineRoom?.displayName == SelectedFilterProperty2).ToArray();
            }
            else if (SelectedFilterType2 == "Nhóm đối tượng")
            {

                PeopleListView2 = PeopleList2.Where(x => x.Severity?.level == SelectedFilterProperty2).ToArray();
            }
            else if (SelectedFilterType2 == "Ngày đi")
            {
                PeopleListView2 = PeopleList2.Where(x => x.leaveDate.ToString() == SelectedFilterProperty2).ToArray();

            }
            else if (SelectedFilterType2 == "Ngày đến")
            {
                PeopleListView2 = PeopleList2.Where(x => x.arrivedDate.ToString() == SelectedFilterProperty2).ToArray();

            }
            else if (SelectedFilterType2 == "Ngày đến kì hạn xét nghiệm")
            {
                if (SelectedFilterProperty2 == "Hôm nay")
                {
                    FilterPersonIsOnTestingDateToday2(DateTime.Now.Date);
                }
                else if (SelectedFilterProperty2 == "Hôm qua")
                {
                    FilterPersonIsOnTestingDateToday2(DateTime.Today.AddDays(-1).Date);

                }
                else if (SelectedFilterProperty2 == "Ngày mai")
                {
                    FilterPersonIsOnTestingDateToday2(DateTime.Today.AddDays(1).Date);

                }


            }


        }

        void FilterPersonIsOnTestingDateToday1(DateTime SelectedDate)
        {
            int maxQuarantineDay = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().requiredDayToFinish;
            int testCycle = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().testCycle;
            var tempPeopleList = PeopleList1.ToArray();
            var tempQuarantinePersonList = new ObservableCollection<QuarantinePerson>();
            for (int i = 0; i < tempPeopleList.Length; i++)
            {
                var tempID = tempPeopleList[i].id;
                var TestingResultList = new ObservableCollection<TestingResult>(DataProvider.ins.db.TestingResults.Where(x => x.quarantinePersonID == tempID));

                // Nếu còn cách li
                if ((SelectedDate - tempPeopleList[i].leaveDate.Date).TotalDays <= 0)
                {
                    // Ngày cuối cách ly
                    if ((SelectedDate - tempPeopleList[i].leaveDate.Date).TotalDays == 0)
                    {
                        tempQuarantinePersonList.Add(tempPeopleList[i]);
                    }
                    // Chưa xét nghiệm lần nào
                    else if (TestingResultList.ToArray().Length == 0)
                    {
                        if ((SelectedDate - tempPeopleList[i].arrivedDate.Date).TotalDays >= testCycle)
                        {
                            tempQuarantinePersonList.Add(tempPeopleList[i]);
                        }


                    }
                    // Đã xét nghiệm
                    else
                    {
                        DateTime max = TestingResultList[0].dateTesting;
                        for (int j = 1; j < TestingResultList.ToArray().Length; j++)
                            if ((max - TestingResultList[j].dateTesting).TotalDays < 0) max = TestingResultList[j].dateTesting;

                        if ((DateTime.Now.Date - max.Date).TotalDays >= testCycle)
                        {
                            tempQuarantinePersonList.Add(tempPeopleList[i]);
                        }
                    }
                }
            }

            //if (SearchKey1 != "" && SearchKey1 != null)
                PeopleListView1 = tempQuarantinePersonList.ToArray();
        }


        void FilterPersonIsOnTestingDateToday2(DateTime SelectedDate)
        {
            int maxQuarantineDay = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().requiredDayToFinish;
            int testCycle = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().testCycle;
            var tempPeopleList = PeopleList1.ToArray();
            var tempQuarantinePersonList = new ObservableCollection<QuarantinePerson>();
            for (int i = 0; i < tempPeopleList.Length; i++)
            {
                var tempID = tempPeopleList[i].id;
                var TestingResultList = new ObservableCollection<TestingResult>(DataProvider.ins.db.TestingResults.Where(x => x.quarantinePersonID == tempID));

                // Nếu còn cách li
                if ((SelectedDate - tempPeopleList[i].leaveDate.Date).TotalDays <= 0)
                {
                    // Ngày cuối cách ly
                    if ((SelectedDate - tempPeopleList[i].leaveDate.Date).TotalDays == 0)
                    {
                        tempQuarantinePersonList.Add(tempPeopleList[i]);
                    }
                    // Chưa xét nghiệm lần nào
                    else if (TestingResultList.ToArray().Length == 0)
                    {
                        if ((SelectedDate - tempPeopleList[i].arrivedDate.Date).TotalDays >= testCycle)
                        {
                            tempQuarantinePersonList.Add(tempPeopleList[i]);
                        }


                    }
                    // Đã xét nghiệm
                    else
                    {
                        DateTime max = TestingResultList[0].dateTesting;
                        for (int j = 1; j < TestingResultList.ToArray().Length; j++)
                            if ((max - TestingResultList[j].dateTesting).TotalDays < 0) max = TestingResultList[j].dateTesting;

                        if ((DateTime.Now.Date - max.Date).TotalDays >= testCycle)
                        {
                            tempQuarantinePersonList.Add(tempPeopleList[i]);
                        }
                    }
                }
            }

            //if (SearchKey1 != "" && SearchKey1 != null)
                PeopleListView1 = tempQuarantinePersonList.ToArray();
        }



        void SetSelectedItemToProperty1()
        {
            var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem1.id).FirstOrDefault();
            var PersonSeverity = DataProvider.ins.db.Severities.Where(x => x.id == Person.levelID).FirstOrDefault();
            var PersonRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == Person.roomID).FirstOrDefault();





            QPName = Person.name;
            QPSelectedSex = Person.sex;
            QPDateOfBirth = Person.dateOfBirth;
            QPCitizenID = Person.citizenID;
            QPSelectedNationality = Person.nationality;
            QPPhoneNumber = Person.phoneNumber;
            QPHealthInsuranceID = Person.healthInsuranceID;
            QPSelectedLevel = PersonSeverity;

            SelectedItem1.name = Person.name;
            SelectedItem1.sex = Person.sex;
            SelectedItem1.dateOfBirth = Person.dateOfBirth;
            SelectedItem1.citizenID = Person.citizenID;
            SelectedItem1.nationality = Person.nationality;
            SelectedItem1.phoneNumber = Person.phoneNumber;
            SelectedItem1.healthInsuranceID = Person.healthInsuranceID;
            if (PersonSeverity != null) SelectedItem1.levelID = PersonSeverity.id;
            SelectedItem1.arrivedDate = Person.arrivedDate;
            SelectedItem1.leaveDate = Person.leaveDate;
            Room = PersonRoom;
        }


        void SetSelectedItemToProperty2()
        {
            var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem2.id).FirstOrDefault();
            var PersonSeverity = DataProvider.ins.db.Severities.Where(x => x.id == Person.levelID).FirstOrDefault();
            var PersonRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == Person.roomID).FirstOrDefault();





            QPName = Person.name;
            QPSelectedSex = Person.sex;
            QPDateOfBirth = Person.dateOfBirth;
            QPCitizenID = Person.citizenID;
            QPSelectedNationality = Person.nationality;
            QPPhoneNumber = Person.phoneNumber;
            QPHealthInsuranceID = Person.healthInsuranceID;
            QPSelectedLevel = PersonSeverity;

            SelectedItem2.name = Person.name;
            SelectedItem2.sex = Person.sex;
            SelectedItem2.dateOfBirth = Person.dateOfBirth;
            SelectedItem2.citizenID = Person.citizenID;
            SelectedItem2.nationality = Person.nationality;
            SelectedItem2.phoneNumber = Person.phoneNumber;
            SelectedItem2.healthInsuranceID = Person.healthInsuranceID;
            if (PersonSeverity != null) SelectedItem2.levelID = PersonSeverity.id;
            SelectedItem2.arrivedDate = Person.arrivedDate;
            SelectedItem2.leaveDate = Person.leaveDate;
            Room = PersonRoom;
        }

        #endregion


        #region methodAddEdit


        void DeleteTemplate()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    NotificationTemplate NotiTemplate = DataProvider.ins.db.NotificationTemplates.Where(x => x.id == selectedTemplate.id).FirstOrDefault();
                    if (NotiTemplate == null) return;

                    DataProvider.ins.db.NotificationTemplates.Remove(NotiTemplate);
                    DataProvider.ins.db.SaveChanges();

                    TemplateList.Remove(NotiTemplate);


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

        void AddTemplate()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {

                    NotificationTemplate notificationTemplate = new NotificationTemplate()
                    {
                        name = title,
                        content = content
                    };

                    DataProvider.ins.db.NotificationTemplates.Add(notificationTemplate);
                    DataProvider.ins.db.SaveChanges();
                    title = content = "";

                    TemplateList.Add(notificationTemplate);

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
        void EditTemplate()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    NotificationTemplate SelectedTemplate = DataProvider.ins.db.NotificationTemplates.Where(x => x.id == selectedTemplate.id).FirstOrDefault();

                    SelectedTemplate.name = title;
                    SelectedTemplate.content = content;

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


        void SetFullyMessage()
        {
            var QAName = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().name;

            if (selectedTemplate == null)
            {
                fullyMessage = "Chao a/c " + "{Tên người nhận}" + ", day la thong bao den tu ban quan ly cua khu cach ly " + QAName + ". " + message + "Xin cam on";
            }
            else
            {
                if (selectedTemplate.id == 1) fullyMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Xin cảm ơn";
                else if (selectedTemplate.id == 2) fullyMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Xin cảm ơn";
                else if (selectedTemplate.id == 3) fullyMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Xin cảm ơn";
                else if (selectedTemplate.id == 4) fullyMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Bạn sẽ được chuyển tới phòng " + "{Ten phong}" + " với sức chứa " + "{Suc chua}" + " nguoi" + "Xin cảm ơn";

                else fullyMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Xin cảm ơn";
            }
        }


        void SendMessage()
        {
            for (int i = 0; i < PeopleList2.ToArray().Length; i++)
            {
                var messageContent = "";
                if (selectedTemplate == null)
                {
                    messageContent = SendMessageToPerson(PeopleList2[i]);
                }
                else
                {
                    if (selectedTemplate.id == 1)
                    {
                        messageContent = StartIntroduction(PeopleList2[i]);
                    }
                    else if (selectedTemplate.id == 2)
                    {
                        messageContent = EndIntroduction(PeopleList2[i]);
                    }
                    else if (selectedTemplate.id == 3)
                    {
                        messageContent = TestingNotification(PeopleList2[i]);
                    }
                    else if (selectedTemplate.id == 4)
                    {
                        messageContent = ChangeRoomNotification(PeopleList2[i]);
                    }
                    else messageContent = SendMessageToPerson(PeopleList2[i]);






                }


                if (PeopleList2[i].phoneNumber != "" && PeopleList2[i] != null)
                {
                    sendMessageWithTwillo(messageContent, fomatPhoneNumber(PeopleList2[i].phoneNumber));

                }

            }

            MessageBox.Show("Gửi thông báo thành công!");
            // Reset nè
            PeopleList2 = new ObservableCollection<QuarantinePerson>();


        }

        string fomatPhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(" ", String.Empty);
            phoneNumber = phoneNumber.Remove(0, 1);
            phoneNumber = "+84" + phoneNumber;
            return phoneNumber;
        }

        void sendMessageWithTwillo(string messageContent, string phoneNumber)
        {
            try
            {
                var accountSid = "AC9cb120d0ee9f5196f765af6db11ce3dd";
                var authToken = "bc71a9857ca07babd055e845b0eeef2c";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                   new PhoneNumber(phoneNumber));
                messageOptions.MessagingServiceSid = "MG9ba537e9324fff8eeac2f4eeb109d1f0";
                messageOptions.Body = messageContent;

                var message = MessageResource.Create(messageOptions);
            }
            catch
            {
                MessageBox.Show("Loi roi");
            }
        }


        string SendMessageToPerson(QuarantinePerson person)
        {

            var QAName = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().name;
            string CombinedMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Xin cảm ơn";
            return CombinedMessage;
        }

        string ChangeRoomNotification(QuarantinePerson person)
        {
            var DestinationRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == person.roomID).FirstOrDefault();
            var QAName = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().name;
            string CombinedMessage = "Chào a/c " + person.name + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + "Bạn sẽ được chuyễn đến phòng " + DestinationRoom?.displayName + " với sức chứa " + DestinationRoom?.capacity + " người" + ". Xin cảm ơn";
            return CombinedMessage;
        }


        string TestingNotification(QuarantinePerson person)
        {
            var DestinationRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == person.roomID).FirstOrDefault();
            var QAName = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().name;
            string CombinedMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Xin cảm ơn";
            return CombinedMessage;
        }


        string StartIntroduction(QuarantinePerson person)
        {
            var DestinationRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == person.roomID).FirstOrDefault();
            var QAName = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().name;
            string CombinedMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Xin cảm ơn";
            return CombinedMessage;
        }


        string EndIntroduction(QuarantinePerson person)
        {
            var DestinationRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == person.roomID).FirstOrDefault();
            var QAName = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().name;
            string CombinedMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đền từ ban quản lý của khu cách ly " + QAName + ". " + message + ". Xin cảm ơn";
            return CombinedMessage;
        }

    }
}