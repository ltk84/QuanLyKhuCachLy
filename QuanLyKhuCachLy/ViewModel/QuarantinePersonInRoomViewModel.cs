using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantinePersonInRoomViewModel : QuarantinePersonViewModel
    {
        #region property

        private QuarantineRoomViewModel _Parent;
        public QuarantineRoomViewModel Parent
        {
            get => _Parent; set
            {
                _Parent = value;
                RoomID = _Parent.RoomID;
                OnPropertyChanged();
            }
        }

        private int _RoomID;
        public int RoomID
        {
            get => _RoomID; set
            {
                _RoomID = value;
                QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == RoomID));
                OnPropertyChanged();
            }
        }

        private static QuarantinePersonInRoomViewModel _ins;
        public static QuarantinePersonInRoomViewModel ins
        {
            get
            {
                if (_ins == null) _ins = new QuarantinePersonInRoomViewModel();
                return _ins;
            }
            set => _ins = value;
        }


        #endregion

        public QuarantinePersonInRoomViewModel()
        {

            QuarantinePersonList = new ObservableCollection<QuarantinePerson>();

            ToViewCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Parent.ToPersonInformation();
            });

            ToMainCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Parent.BackToRoomInformation();
            });

            ToEditCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                EditQuarantinePersonInRoom editScreen = new EditQuarantinePersonInRoom();
                editScreen.ShowDialog();
            });
        }

        #region method

        #endregion
    }
}
