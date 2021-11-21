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

        private Model.QuarantinePerson _SelectedPersonWithNoRoom;

        public Model.QuarantinePerson SelectedPersonWithNoRoom
        {
            get { return _SelectedPersonWithNoRoom; }
            set { _SelectedPersonWithNoRoom = value; OnPropertyChanged(); }
        }

        private Model.QuarantineRoom _SelectedAvailableRoom;

        public Model.QuarantineRoom SelectedAvailableRoom
        {
            get { return _SelectedAvailableRoom; }
            set { _SelectedAvailableRoom = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Model.QuarantinePerson> _PersonsToAddToSelectedRoom;

        public ObservableCollection<Model.QuarantinePerson> PersonsToAddToSelectedRoom
        {
            get { return _PersonsToAddToSelectedRoom; }
            set { _PersonsToAddToSelectedRoom = value; OnPropertyChanged(); }
        }
        private Model.QuarantinePerson _SelectedPersonToAddToRoom;

        public Model.QuarantinePerson SelectedPersonToAddToRoom
        {
            get { return _SelectedPersonToAddToRoom; }
            set { _SelectedPersonToAddToRoom = value; OnPropertyChanged(); }
        }

        private string _SelectedRoomDisplayName;

        public string SelectedRoomDisplayName
        {
            get { return _SelectedRoomDisplayName; }
            set { _SelectedRoomDisplayName = value; OnPropertyChanged(); }
        }

        private string _SelectedRoomAvailableCapacity;

        public string SelectedRoomAvailableCapacity
        {
            get { return _SelectedRoomAvailableCapacity; }
            set { _SelectedRoomAvailableCapacity = value; OnPropertyChanged(); }
        }


        private Dictionary<Model.QuarantineRoom, ObservableCollection<Model.QuarantinePerson>> _QuarantinePersonsToAddByRoom;

        #endregion

        #region Command

        public ICommand CancelCommand { get; set; }
        public ICommand UpdatePersonsToAddToRoomCommand { get; set; }
        public ICommand AddPersonToRoomCommand { get; set; }
        public ICommand RemovePersonFromRoomCommand { get; set; }

        #endregion
        public ArrangeRoomRecommendationViewModel()
        {
            // Lấy toàn bộ danh sách người và phòng từ database
            ObservableCollection<Model.QuarantinePerson> quarantinePersonList = new ObservableCollection<Model.QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
            ObservableCollection<Model.QuarantineRoom> quarantineRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);

            // Lọc danh sách người chưa có phòng và phòng còn trống
            PersonsWithNoRoom = new ObservableCollection<QuarantinePerson>(quarantinePersonList.Where(person => person.roomID == null));
            AvailableRooms = new ObservableCollection<QuarantineRoom>(quarantineRoomList.Where(room => room.QuarantinePersons.Count() < room.capacity));

            // Khởi tạo map giữa phòng còn trống và người chưa có phòng sẽ được thêm vào phòng đó
            _QuarantinePersonsToAddByRoom = new Dictionary<Model.QuarantineRoom, ObservableCollection<Model.QuarantinePerson>>();
            for (int i = 0; i < AvailableRooms.Count(); i++)
            {
                _QuarantinePersonsToAddByRoom.Add(AvailableRooms[i], new ObservableCollection<QuarantinePerson>());
            }

            if (AvailableRooms.Count() > 0)
            {
                // Khởi tạo giá trị mặc định
                SelectedAvailableRoom = AvailableRooms[0];

                if (PersonsWithNoRoom.Count() > 0)
                {
                    SelectedPersonWithNoRoom = PersonsWithNoRoom[0];
                }

                // Chạy hàm khởi tạo (cập nhật lại các biến binding lên view)
                UpdateInfo();
            }

            InitCommands();
        }


        // Khởi tạo các hàm Command 
        private void InitCommands()
        {
            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.DialogResult = false;
                p.Close();
            });
            UpdatePersonsToAddToRoomCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UpdateInfo();
            });
            AddPersonToRoomCommand = new RelayCommand<object>((p) => {
                if (SelectedAvailableRoom.QuarantinePersons.Count() + PersonsToAddToSelectedRoom.Count() < SelectedAvailableRoom.capacity)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) =>
            {
                _QuarantinePersonsToAddByRoom[SelectedAvailableRoom].Add(SelectedPersonWithNoRoom);
                PersonsWithNoRoom.Remove(SelectedPersonWithNoRoom);
                UpdateInfo();
            });
            RemovePersonFromRoomCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                PersonsWithNoRoom.Add(SelectedPersonToAddToRoom);
                _QuarantinePersonsToAddByRoom[SelectedAvailableRoom].Remove(SelectedPersonToAddToRoom);
                UpdateInfo();
            });
        }

        // Cứ mỗi thao tác được thực thi sẽ phải chạy lại hàm này
        private void UpdateInfo()
        {
            PersonsToAddToSelectedRoom = _QuarantinePersonsToAddByRoom[SelectedAvailableRoom];
            SelectedRoomDisplayName = $"phòng {SelectedAvailableRoom.displayName}";
            SelectedRoomAvailableCapacity = $"{PersonsToAddToSelectedRoom.Count()}/{SelectedAvailableRoom.capacity - SelectedAvailableRoom.QuarantinePersons.Count()}";
        }

        // Khởi tạo theo đề xuất
        private void InitByRecommendation()
        {
            //for (int i = 0; i < AvailableRooms.Count(); i++)
            //{
            //    _QuarantinePersonsToAddByRoom.Add(AvailableRooms[i], new ObservableCollection<QuarantinePerson>(PersonWitN));
            //}
        }
    }
}
