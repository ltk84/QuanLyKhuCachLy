using QuanLyKhuCachLy.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuanLyKhuCachLy.ViewModel
{
    public class NotificationViewModel : BaseViewModel
    {

        private Staff[] _StaffListView;
        public Staff[] StaffListView
        {
            get => _StaffListView; set
            {
                _StaffListView = value; OnPropertyChanged();
            }
        }
        private Staff _SelectedItem;
        public Staff SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                //if (SelectedItem != null)
                //{
                //    SetSelectedItemToProperty();
                //}
            }
        }
        public NotificationViewModel() {
            var StaffList = new ObservableCollection<Staff>(DataProvider.ins.db.Staffs);
            StaffListView = StaffList.ToArray();

        }
    }
}
