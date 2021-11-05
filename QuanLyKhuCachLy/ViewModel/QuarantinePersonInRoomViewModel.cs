using QuanLyKhuCachLy.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantinePersonInRoomViewModel : QuarantinePersonViewModel
    {
        #region property
        private int _RoomID;

        #endregion

        public QuarantinePersonInRoomViewModel(int CurrentRoomID)
        {
            _RoomID = CurrentRoomID;
            QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == _RoomID));
        }
    }
}
