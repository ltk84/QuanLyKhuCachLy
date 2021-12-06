using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
        public ICommand ConfirmCommand { get; set; }
        public ICommand UpdatePersonsToAddToRoomCommand { get; set; }
        public ICommand AddPersonToRoomCommand { get; set; }
        public ICommand RemovePersonFromRoomCommand { get; set; }
        public ICommand AddAllPersonToRoomCommand { get; set; }
        public ICommand RemoveAllPersonFromRoomCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        #endregion
        public ArrangeRoomRecommendationViewModel()
        {
            Init();

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.DialogResult = false;
                p.Close();
            });
            ConfirmCommand = new RelayCommand<Window>((p) =>
            {
                if (AvailableRooms != null)
                {
                    for (int i = 0; i < AvailableRooms.Count(); i++)
                    {
                        if (_QuarantinePersonsToAddByRoom != null)
                        {
                            if (_QuarantinePersonsToAddByRoom[AvailableRooms[i]] != null)
                            {
                                if (_QuarantinePersonsToAddByRoom[AvailableRooms[i]].Count() != 0)
                                    return true;
                            }
                        }
                    }
                }
                return false;
            }, (p) =>
            {
                ConfirmAddPersonToRoom();
                p.DialogResult = true;
                p.Close();
            });
            UpdatePersonsToAddToRoomCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UpdateInfo();
            });
            AddPersonToRoomCommand = new RelayCommand<object>((p) => IsRoomAvailable(SelectedAvailableRoom, PersonsToAddToSelectedRoom), (p) =>
            {
                AddPersonToRoom(_QuarantinePersonsToAddByRoom[SelectedAvailableRoom], PersonsWithNoRoom, SelectedPersonWithNoRoom);
                UpdateInfo();
            });
            RemovePersonFromRoomCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                RemovePersonFromRoom(_QuarantinePersonsToAddByRoom[SelectedAvailableRoom], PersonsWithNoRoom, SelectedPersonToAddToRoom);
                UpdateInfo();
            });
            AddAllPersonToRoomCommand = new RelayCommand<object>((p) => {
                if (SelectedAvailableRoom == null || PersonsWithNoRoom == null)
                    return false;
                if (PersonsWithNoRoom.Count() > 0 && IsRoomAvailable(SelectedAvailableRoom, PersonsToAddToSelectedRoom))
                    return true;
                else
                    return false;
            }, (p) =>
            {
                AddManyPersonToRoom(SelectedAvailableRoom, _QuarantinePersonsToAddByRoom[SelectedAvailableRoom], PersonsWithNoRoom);
                UpdateInfo();
            });
            RemoveAllPersonFromRoomCommand = new RelayCommand<object>((p) => {
                if (SelectedAvailableRoom == null || _QuarantinePersonsToAddByRoom == null)
                    return false;
                if (_QuarantinePersonsToAddByRoom[SelectedAvailableRoom].Count() > 0)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                RemoveManyPersonFromRoom(_QuarantinePersonsToAddByRoom[SelectedAvailableRoom], PersonsWithNoRoom);
                UpdateInfo();
            });
            RefreshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Init();
            });
        }


        // Fundamental Init.
        private void Init()
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
                var TempPersonsWithNoRoom = new ObservableCollection<QuarantinePerson>(PersonsWithNoRoom);
                foreach (var person in TempPersonsWithNoRoom)
                {
                    if (IsRoomAvailable(AvailableRooms[i], _QuarantinePersonsToAddByRoom[AvailableRooms[i]]))
                    {
                        if (IsTheSameSeverity(person, AvailableRooms[i]))
                        {
                            AddPersonToRoom(_QuarantinePersonsToAddByRoom[AvailableRooms[i]], PersonsWithNoRoom, person);
                        }
                    }
                }
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
            else
            {
                UpdateInfo();
            }
        }

        // Cứ mỗi thao tác được thực thi sẽ phải chạy lại hàm này
        private void UpdateInfo()
        {
            if (SelectedAvailableRoom != null)
            {
                PersonsToAddToSelectedRoom = _QuarantinePersonsToAddByRoom[SelectedAvailableRoom];
                SelectedRoomDisplayName = $"phòng {SelectedAvailableRoom.displayName}";
                SelectedRoomAvailableCapacity = $"{PersonsToAddToSelectedRoom.Count()}/{SelectedAvailableRoom.capacity - SelectedAvailableRoom.QuarantinePersons.Count()}";
            } 
            else
            {
                PersonsToAddToSelectedRoom = null;
                SelectedRoomDisplayName = "(hết phòng)";
                SelectedRoomAvailableCapacity = "0/0";
            }
        }

        // Khởi tạo theo đề xuất
        private void InitByRecommendation()
        {
            for (int i = 0; i < AvailableRooms.Count(); i++)
            {
                _QuarantinePersonsToAddByRoom.Add(AvailableRooms[i], new ObservableCollection<QuarantinePerson>(PersonsWithNoRoom.Where(person => person.Severity.level == AvailableRooms[i].Severity.level)));
            }
        }
        
        // Hàm lọc người theo mức độ phòng
        private bool IsTheSameSeverity(QuarantinePerson person, QuarantineRoom room)
        {
            if (person.Severity == null & room.Severity == null)
            {
                return true;
            }
            else if (person.Severity != null && room.Severity != null)
            {
                if (person.Severity.level == room.Severity.level)
                {
                    return true;
                }
            }
            return false;
        }

        // Hàm kiểm tra xem phòng còn chỗ trống không
        private bool IsRoomAvailable(QuarantineRoom room, ObservableCollection<QuarantinePerson> personsToAddToRoom)
        {
            if (room == null)
                return false;
            if (room.QuarantinePersons.Count() + personsToAddToRoom.Count() < room.capacity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Hàm thêm người vào danh sách sẽ thêm phòng
        private void AddPersonToRoom(ObservableCollection<QuarantinePerson> personInRoomList, ObservableCollection<QuarantinePerson> personsWithNoRoom, QuarantinePerson person)
        {
            personInRoomList.Add(person);
            personsWithNoRoom.Remove(person);
        }

        // Hàm xóa người ra khỏi danh sách sẽ thêm vào phòng
        private void RemovePersonFromRoom(ObservableCollection<QuarantinePerson> personInRoomList, ObservableCollection<QuarantinePerson> personsWithNoRoom, QuarantinePerson person)
        {
            personsWithNoRoom.Add(person);
            personInRoomList.Remove(person);
        }
        
        // Hàm thêm nhiều người vào danh sách sẽ thêm phòng
        private void AddManyPersonToRoom(QuarantineRoom room, ObservableCollection<QuarantinePerson> personInRoomList, ObservableCollection<QuarantinePerson> personsWithNoRoom)
        {
            var TempPersonsWithNoRoom = new ObservableCollection<QuarantinePerson>(personsWithNoRoom);
            foreach (var person in TempPersonsWithNoRoom)
            {
                if (IsRoomAvailable(room, personInRoomList))
                {
                    personInRoomList.Add(person);
                    personsWithNoRoom.Remove(person);
                }
            }
        }

        // Hàm xóa nhiều người ra khỏi danh sách sẽ thêm vào phòng
        private void RemoveManyPersonFromRoom(ObservableCollection<QuarantinePerson> personInRoomList, ObservableCollection<QuarantinePerson> personsWithNoRoom)
        {
            var TempPersonsInRoomList = new ObservableCollection<QuarantinePerson>(personInRoomList);
            foreach (var person in TempPersonsInRoomList)
            {
                personsWithNoRoom.Add(person);
                personInRoomList.Remove(person);
            }
        }

        // Hàm thực thi thêm người vào phòng trên database
        private void ConfirmAddPersonToRoom()
        {
            try
            {
                foreach (var item in _QuarantinePersonsToAddByRoom)
                {
                    var room = DataProvider.ins.db.QuarantineRooms.Where(r => r.id == item.Key.id).FirstOrDefault();
                    if (room == null)
                        return;
                    foreach (var p in item.Value)
                    {
                        var person = DataProvider.ins.db.QuarantinePersons.Where(pe => pe.id == p.id).FirstOrDefault();
                        if (person == null)
                            return;
                        person.roomID = room.id;
                    }
                    DataProvider.ins.db.SaveChanges();
                }
                DataProvider.ins.db.SaveChanges();
            }
            catch
            {

                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;

                ErrorDialog.ShowDialog();
            }

            Window SuccessDialog = new Window
            {
                AllowsTransparency = true,
                Background = Brushes.Transparent,
                Width = 600,
                Height = 400,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.None,
                Content = new CustomUserControl.SuccessNotification()
            };
            SuccessDialog.ShowDialog();
        }
    }
}
