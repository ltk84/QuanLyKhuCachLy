using QuanLyKhuCachLy.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuanLyKhuCachLy.ViewModel
{
    public class NotificationViewModel : BaseViewModel
    {

        private QuarantinePerson[] _StaffListView;
        public QuarantinePerson[] StaffListView
        {
            get => _StaffListView; set
            {
                _StaffListView = value; OnPropertyChanged();
            }
        }
        private QuarantinePerson _SelectedItem;
        public QuarantinePerson SelectedItem
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
            var StaffList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
            StaffListView = StaffList.ToArray();

        }
    }
}
