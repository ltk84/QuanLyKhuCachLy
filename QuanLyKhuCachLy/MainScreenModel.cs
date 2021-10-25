using QuanLyKhuCachLy.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhuCachLy
{
    class MainScreenModel : BaseViewModel
    {
        private bool _isOnDashboard;
        private bool _isOnRoom;
        private bool _isOnPerson;
        private bool _isOnStaff;
        private bool _isOnStat;
        private bool _isOnNotify;
        public RelayCommand<object> ToDashboardCommand { get; set; }
        public RelayCommand<object> ToRoomCommand { get; set; }
        public RelayCommand<object> ToPersonCommand { get; set; }
        public RelayCommand<object> ToStaffCommand { get; set; }
        public RelayCommand<object> ToStatCommand { get; set; }
        public RelayCommand<object> ToNotifyCommand { get; set; }

        public bool IsOnDashboard
        {
            get { return _isOnDashboard; }
            set 
            { 
                _isOnDashboard = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnRoom
        {
            get { return _isOnRoom; }
            set
            {
                _isOnRoom = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnPerson
        {
            get { return _isOnPerson; }
            set
            {
                _isOnPerson = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnStaff
        {
            get { return _isOnStaff; }
            set
            {
                _isOnStaff = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnStat
        {
            get { return _isOnStat; }
            set
            {
                _isOnStat = value;
                OnPropertyChanged();
            }
        }
        public bool IsOnNotify
        {
            get { return _isOnNotify; }
            set
            {
                _isOnNotify = value;
                OnPropertyChanged();
            }
        }

        public MainScreenModel()
        {
            Init();

            ToDashboardCommand = new RelayCommand<object>((p) => { return true; }, (o) => {
                ToDashboard();
            });
            ToRoomCommand = new RelayCommand<object>((p) => { return true; }, (o) => {
                ToRoom();
            });
            ToPersonCommand = new RelayCommand<object>((p) => { return true; }, (o) => {
                ToPerson();
            });
            ToStaffCommand = new RelayCommand<object>((p) => { return true; }, (o) => {
                ToStaff();
            });
            ToStatCommand = new RelayCommand<object>((p) => { return true; }, (o) => {
                ToStat();
            });
            ToNotifyCommand = new RelayCommand<object>((p) => { return true; }, (o) => {
                ToNotify();
            });
        }

        private void Init()
        {
            ToDashboard();
        }

        private void ToDashboard()
        {
            _isOnDashboard = true;
            _isOnRoom = false;
            _isOnPerson = false;
            _isOnStaff = false;
            _isOnStat = false;
            _isOnNotify = false;
        }

        private void ToRoom()
        {
            _isOnDashboard = false;
            _isOnRoom = true;
            _isOnPerson = false;
            _isOnStaff = false;
            _isOnStat = false;
            _isOnNotify = false;
        }

        private void ToPerson()
        {
            _isOnDashboard = false;
            _isOnRoom = false;
            _isOnPerson = true;
            _isOnStaff = false;
            _isOnStat = false;
            _isOnNotify = false;
        }

        private void ToStaff()
        {
            _isOnDashboard = false;
            _isOnRoom = false;
            _isOnPerson = false;
            _isOnStaff = true;
            _isOnStat = false;
            _isOnNotify = false;
        }
        private void ToStat()
        {
            _isOnDashboard = false;
            _isOnRoom = false;
            _isOnPerson = false;
            _isOnStaff = false;
            _isOnStat = true;
            _isOnNotify = false;
        }
        private void ToNotify()
        {
            _isOnDashboard = false;
            _isOnRoom = false;
            _isOnPerson = false;
            _isOnStaff = false;
            _isOnStat = false;
            _isOnNotify = true;
        }
    }
}
