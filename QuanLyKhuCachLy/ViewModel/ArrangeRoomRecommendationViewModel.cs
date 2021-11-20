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
    public class ArrangeRoomRecommendationViewModel : BaseViewModel
    {

        #region Property

        private ObservableCollection<Model.QuarantinePerson> _PersonsWithNoRoom;

        public ObservableCollection<Model.QuarantinePerson> PersonsWithNoRoom
        {
            get { return _PersonsWithNoRoom; }
            set { _PersonsWithNoRoom = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Model.QuarantineRoom> _AvailableRooms;

        public ObservableCollection<Model.QuarantineRoom> AvailableRooms
        {
            get { return _AvailableRooms; }
            set { _AvailableRooms = value; OnPropertyChanged(); }
        }


        #endregion

        #region Command

        public ICommand CancelCommand { get; set; }

        #endregion
        public ArrangeRoomRecommendationViewModel()
        {
            ObservableCollection<Model.QuarantinePerson> quarantinePersonList = new ObservableCollection<Model.QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
            ObservableCollection<Model.QuarantineRoom> quarantineRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);

            PersonsWithNoRoom = new ObservableCollection<QuarantinePerson>(quarantinePersonList.Where(person => person.roomID == null));
            AvailableRooms = new ObservableCollection<QuarantineRoom>(quarantineRoomList.Where(room => room.QuarantinePersons.Count() < room.capacity));

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.DialogResult = false;
                p.Close();
            });
        }
    }
}
