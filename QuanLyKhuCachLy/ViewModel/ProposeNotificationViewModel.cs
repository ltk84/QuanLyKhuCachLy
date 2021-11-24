using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhuCachLy.ViewModel
{
    public class ProposeNotificationViewModel : BaseViewModel
    {
        #region Property
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

        private string _editableMessage;
        public string editableMessage
        {
            get { return _editableMessage; }
            set { _editableMessage = value; OnPropertyChanged(); }
        }

        private string _fullMessage;
        public string FullMessage
        {
            get { return _fullMessage; }
            set { _fullMessage = value; OnPropertyChanged(); }
        }


        #endregion

        public ProposeNotificationViewModel()
        {
            Type = 1;
            InitData();
        }

        private void InitData()
        {
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
        }
    }
}
