using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantineRoomViewModel : BaseViewModel
    {
        static int LIMIT = 20;
        #region property
        private ObservableCollection<QuarantineRoom> _RoomList;
        public ObservableCollection<QuarantineRoom> RoomList
        {
            get => _RoomList; set
            {
                _RoomList = value;
                OnPropertyChanged();
            }
        }

        private QuarantineRoom _SelectedItem;
        public QuarantineRoom SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (_SelectedItem != null)
                {
                    SetSelectedItemToProperty();
                }
            }
        }

        private string _RoomDisplayName;
        public string RoomDisplayName
        {
            get => _RoomDisplayName; set
            {
                _RoomDisplayName = value;
                OnPropertyChanged();
            }
        }

        private string _RoomCapacity;
        public string RoomCapacity
        {
            get => _RoomCapacity; set
            {
                _RoomCapacity = value;
                OnPropertyChanged();
            }
        }

        private Severity _RoomSelectedSeverity;
        public Severity RoomSelectedSeverity
        {
            get => _RoomSelectedSeverity; set
            {
                _RoomSelectedSeverity = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Severity> _RoomLevelList;
        public ObservableCollection<Severity> RoomLevelList
        {
            get => _RoomLevelList; set
            {
                _RoomLevelList = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand ToAddCommand { get; set; }
        public ICommand ToEditCommand { get; set; }
        public ICommand ToDeleteCommand { get; set; }

        #endregion

        public QuarantineRoomViewModel()
        {
            RoomList = new ObservableCollection<QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
            RoomLevelList = new ObservableCollection<Severity>(DataProvider.ins.db.Severities);

            ToAddCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ClearData();
                DemoAdd AddScreen = new DemoAdd();
                AddScreen.ShowDialog();
                ClearData();
            });

            ToEditCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem == null) return false;
                return true;
            }, (p) =>
            {
                DemoEdit EditScreen = new DemoEdit();
                SetSelectedItemToProperty();
                EditScreen.ShowDialog();

            });


            AddCommand = new RelayCommand<object>((p) =>
            {
                int ParsedCapacity = 0;
                var result = Int32.TryParse(RoomCapacity, out ParsedCapacity);

                if (RoomSelectedSeverity == null || !result)
                    return false;
                if (!string.IsNullOrEmpty(RoomDisplayName) && RoomDisplayName.Length <= LIMIT && ParsedCapacity != 0) return true;
                return false;
            }, (p) =>
            {
                AddQuarantineRoom();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                int ParsedCapacity = 0;
                var result = Int32.TryParse(RoomCapacity, out ParsedCapacity);

                if (RoomSelectedSeverity == null || !result)
                    return false;
                if (!string.IsNullOrEmpty(RoomDisplayName) && RoomDisplayName.Length <= LIMIT && ParsedCapacity != 0) return true;
                return false;
            }, (p) =>
            {
                //EditQuarantineRoom();
            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //DeleteQuarantineRoom();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }

        #region method
        // untest
        // handle cho screen inital nữa
        void AddQuarantineRoom()
        {
            // List Severity được tạo từ trước nên không cần thêm
            QuarantineRoom QuarantineRoom = new QuarantineRoom { displayName = RoomDisplayName, capacity = Int32.Parse(RoomCapacity), level = RoomSelectedSeverity.level };

            DataProvider.ins.db.QuarantineRooms.Add(QuarantineRoom);
            DataProvider.ins.db.SaveChanges();

            RoomList.Add(QuarantineRoom);
        }
        //untest
        void EditQuarantineRoom()
        {
            QuarantineRoom QuarantineRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == SelectedItem.id).FirstOrDefault();
            QuarantineRoom.displayName = RoomDisplayName;
            QuarantineRoom.capacity = Int32.Parse(RoomCapacity);
            QuarantineRoom.level = RoomSelectedSeverity.level;

            DataProvider.ins.db.SaveChanges();

            SelectedItem.displayName = RoomDisplayName;
        }
        void DeleteQuarantineRoom() { }
        void ClearData()
        {
            RoomDisplayName = "";
            RoomCapacity = "";
            RoomSelectedSeverity = null;
        }
        void SetSelectedItemToProperty()
        {
            RoomDisplayName = SelectedItem.displayName;
            RoomCapacity = SelectedItem.capacity.ToString();
            RoomSelectedSeverity = SelectedItem.Severity;
        }
        #endregion
    }
}
