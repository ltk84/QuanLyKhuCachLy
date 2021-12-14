using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace QuanLyKhuCachLy.ViewModel
{
    public class ProposeNotificationViewModel : BaseViewModel
    {

        private List<Model.QuarantinePerson> _unsendPeopleList;
        public List<Model.QuarantinePerson> UnsendList
        {
            get { return _unsendPeopleList; }
            set { _unsendPeopleList = value; OnPropertyChanged(); }
        }



        #region Property



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
        // message

        





        // Type = 1: Đề xuất gửi thông báo hướng dẫn cách ly.
        // Type = 2: Đề xuất gửi thông báo hoàn thành cách ly.
        private int _type;

        public int Type
        {
            get { return _type; }
            set {
                _type = value; 
                OnPropertyChanged();
                InitData();
            }
        }

        private string _notificationName;

        public string NotificationName
        {
            get { return _notificationName; }
            set { _notificationName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Model.QuarantinePerson> _peopleList;

        public ObservableCollection<Model.QuarantinePerson> PeopleList
        {
            get { return _peopleList; }
            set { _peopleList = value; OnPropertyChanged(); }
        }

        private QuarantinePerson[] _PeopleListView;
        public QuarantinePerson[] PeopleListView
        {
            get => _PeopleListView; set
            {
                _PeopleListView = value; OnPropertyChanged();
            }
        }


        private string _editableMessage;
        public string EditableMessage
        {
            get { return _editableMessage; }
            set { _editableMessage = value; OnPropertyChanged(); setFullMessgae(); }
        }

        private string _fullMessage;
        public string FullMessage
        {
            get { return _fullMessage; }
            set { _fullMessage = value; OnPropertyChanged(); }
        }


        public ICommand SendNotification { get; set; }
        public ICommand CancelCommand { get; set; }



        #endregion

        public ProposeNotificationViewModel()
        {
            UnsendList = new List<QuarantinePerson>();
            Type = 1;
            InitData();

            SendNotification = new RelayCommand<Window>((p) =>
            {
                if (EditableMessage == "" || PeopleList.ToArray().Length == 0) return false;
                return true;
            }, (p) =>
            {
                SendMessage();
                p.DialogResult = true;
                p.Close();
                setListHasReceiveMessage();
                UnsendList.Clear();
            });

            CancelCommand = new RelayCommand<Window>((p) =>
            {
               
                return true;
            }, (p) =>
            {
                p.DialogResult = true;
                UnsendList.Clear();
                p.Close();
            });
        }


        #region sendNotification

        async void SendMessage()
        {
            LoadingIndicator loadingIndicator = new LoadingIndicator();
            Task task = ExecuteSendMessage(loadingIndicator);
            loadingIndicator.ShowDialog();
            await task;
        }
        async Task ExecuteSendMessage(LoadingIndicator loadingIndicator)
        {
            bool isSuccess = false;
            await Task.Run(() =>
            {

                var messageContent = "";
                for (int i = 0; i < PeopleList.ToArray().Length; i++)
                {
                    var QAName = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().name;
                    messageContent = "Chào a/c " + PeopleList[i].name + ", đây là thông báo đến từ ban quản lý của khu cách ly " + QAName + ". " + EditableMessage + " Xin cảm ơn.";


                    if (PeopleList[i].phoneNumber != "" && PeopleList[i] != null)
                    {
                        SendMessageWithTwillo(messageContent, fomatPhoneNumber(PeopleList[i].phoneNumber), PeopleList[i]);

                    }

                }
                isSuccess = true;
                // Reset nè
                PeopleList = new ObservableCollection<QuarantinePerson>();
                EditableMessage = "";
                // Close form nè
            });
            loadingIndicator.Close();
            if (UnsendList.Count == 0)
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
                CannotSendPeopleList unsendPeopleList = new CannotSendPeopleList();
                var Vm = unsendPeopleList.DataContext as CannotSendPeopleViewModel;
                Vm.PeopleList = UnsendList;
                unsendPeopleList.ShowDialog();
                //UnsendList.Clear();
            }
        }
        void SendMessageWithTwillo(string messageContent, string phoneNumber, QuarantinePerson current)
        {
            try
            {
                var accountSid = "ACa850390501246f3c3611635b002e61f0";
                var authToken = "46e4bf0cb8e8e593d41b8a73aa5321e";
                authToken += "a";
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: messageContent,
                    from: new Twilio.Types.PhoneNumber("+19896536427"),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );


            }
            catch
            {
                //CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                //var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                //ErrorDialog.ShowDialog();
                UnsendList.Add(current);
            }
        }


        string fomatPhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(" ", String.Empty);
            phoneNumber = phoneNumber.Remove(0, 1);
            phoneNumber = "+84" + phoneNumber;
            return phoneNumber;
        }
        #endregion



        #region method
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
                FilterProperty = PeopleList.Select(person => person.nationality).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Phòng")
            {
                FilterProperty = PeopleList.Select(person => person.QuarantineRoom?.displayName).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Nhóm đối tượng")
            {
                FilterProperty = PeopleList.Select(person => person.Severity?.description).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Ngày đi")
            {
                FilterProperty = PeopleList.Select(person => person.leaveDate.ToString("dd'/'MM'/'yyyy")).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Ngày đến")
            {
                FilterProperty = PeopleList.Select(person => person.arrivedDate.ToString("dd'/'MM'/'yyyy")).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Ngày đến kì hạn xét nghiệm")
            {
                FilterProperty = new string[] { "Hôm qua", "Hôm nay", "Ngày mai" };
            }
            PeopleListView = PeopleList.ToArray();

        }




        // Hàm này filter người đền kì hạn xét nghiệm hôm nay, là người còn cách li, có ngầy xét nghiệm gần nhát >= số ngày tối thiểu, ngày cuối, hoặc chưa đc xét nghiệm.
        void FilterPersonIsOnTestingDateToday(DateTime SelectedDate)
        {

            int maxQuarantineDay = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().requiredDayToFinish;
            int testCycle = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().testCycle;
            var tempPeopleList = PeopleList.ToArray();
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
            PeopleListView = PeopleList.ToArray();
            if (SelectedFilterProperty == "" || SelectedFilterProperty == null || SelectedFilterProperty == "Tất cả") return;
            if (SelectedFilterType == "Giới tính")
            {
                PeopleListView = PeopleList.Where(x => x.sex == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Quốc tịch")
            {
                PeopleListView = PeopleList.Where(x => x.nationality == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Phòng")
            {

                PeopleListView = PeopleList.Where(x => x.QuarantineRoom?.displayName == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Nhóm đối tượng")
            {

                PeopleListView = PeopleList.Where(x => x.Severity?.description == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Ngày đi")
            {
                PeopleListView = PeopleList.Where(x => x.leaveDate.ToString("dd'/'MM'/'yyyy") == SelectedFilterProperty).ToArray();

            }
            else if (SelectedFilterType == "Ngày đến")
            {
                PeopleListView = PeopleList.Where(x => x.arrivedDate.ToString("dd'/'MM'/'yyyy") == SelectedFilterProperty).ToArray();

            }
            else if (SelectedFilterType == "Ngày đến kì hạn xét nghiệm")
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


        void setFullMessgae()
        {
            var QAName = DataProvider.ins.db.QuarantineAreas.FirstOrDefault().name;

           
            
                FullMessage = "Chào a/c " + "{Tên người nhận}" + ", đây là thông báo đến từ ban quản lý khu cách ly " + QAName + ". " + EditableMessage + " Xin cảm ơn.";
            
        }

        private void InitDataFilter()
        {
            FilterType = new string[] { "Tất cả", "Giới tính", "Quốc tịch", "Phòng", "Nhóm đối tượng", "Ngày đi", "Ngày đến", "Ngày đến kì hạn xét nghiệm" };
            SelectedFilterType = "Tất cả";
            SelectedFilterProperty = "Chọn phương thức lọc";
            getFilterProperty();
        }

        private void InitData()
        {
            string[] hasReceivedMessageList = getListHasReceivedMessage();
            if (Type == 1)
            {
                NotificationName = "Hướng dẫn cách ly";

                ObservableCollection<QuarantinePerson> tempPeopleList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
                PeopleList = new ObservableCollection<QuarantinePerson>(tempPeopleList.Where(person => person.arrivedDate == DateTime.Now.Date));
                
            }
            else if (Type == 2)
            {
                NotificationName = "Hoàn thành cách ly";

                ObservableCollection<QuarantinePerson> tempPeopleList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
                PeopleList = new ObservableCollection<QuarantinePerson>(tempPeopleList.Where(person => person.leaveDate == DateTime.Now.Date));
            }

               for (int i = 0; i< PeopleList.Count; i++)
            {
                if (hasReceivedMessageList.ToList().Contains(PeopleList[i].id.ToString()))
                {
                    PeopleList.Remove(PeopleList[i]);
                    i--;
                }
            }

            PeopleListView = PeopleList.ToArray();
            InitDataFilter();
            // get the template
            EditableMessage = DataProvider.ins.db.NotificationTemplates.Where(item => item.id == Type).FirstOrDefault().content;


        }


        private void setListHasReceiveMessage()
        {
            string[] hasReceivedList = getListHasReceivedMessage();
            string[] receivedList = PeopleListView.Where(person => !UnsendList.Contains(person)).Select(person => person.id.ToString()).ToArray();
            var allReceivedList = new string[hasReceivedList.Length + receivedList.Length];
            hasReceivedList.CopyTo(allReceivedList, 0);
            receivedList.CopyTo(allReceivedList, hasReceivedList.Length);
            string line1 = DateTime.Now.Date.ToString();
            string line2 = string.Join(" ", allReceivedList);

            string fullPath = Path.Combine(Environment.CurrentDirectory, "receivedList.txt");

            if (File.Exists(fullPath))
            {
                // File đã tồn tại - nối thêm nội dung
                File.AppendAllText(fullPath, "");
            }
            else
            {
                // tạo mới vì chưa tồn tại file
                File.WriteAllText(fullPath, "");
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath))
            {
                file.WriteLine(line1);
                file.WriteLine(line2);
            }


        }

        // Hàm trả về mảng chứa id những người đã nhận thông báo hôm nay
        private string[] getListHasReceivedMessage()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "receivedList.txt");

            string[] lines;

            if (System.IO.File.Exists(filePath))
            {
                lines = System.IO.File.ReadAllLines(filePath);
                if (lines.Length == 2)
                {
                    if (lines[0] == DateTime.Now.Date.ToString())
                    {
                        string[] hasReceiveList = lines[1].Split(' ');
                        return hasReceiveList;
                    }
                }
            }
            return new string[] { };
        }




        #endregion
    }
}
