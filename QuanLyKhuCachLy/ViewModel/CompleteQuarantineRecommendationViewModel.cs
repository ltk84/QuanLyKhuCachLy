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
    public class CompleteQuarantineRecommendationViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<Model.QuarantinePerson> _CompleteQuarantinePersons;

        public ObservableCollection<Model.QuarantinePerson> CompleteQuarantinePersons
        {
            get { return _CompleteQuarantinePersons; }
            set { _CompleteQuarantinePersons = value; OnPropertyChanged(); }
        }

        private Model.QuarantinePerson _SelectedCompleteQuarantinePersons;

        public Model.QuarantinePerson SelectedCompleteQuarantinePersons
        {
            get { return _SelectedCompleteQuarantinePersons; }
            set { _SelectedCompleteQuarantinePersons = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Model.QuarantinePerson> _CompleteQuarantinePersonsToResetRoom;

        public ObservableCollection<Model.QuarantinePerson> CompleteQuarantinePersonsToResetRoom
        {
            get { return _CompleteQuarantinePersonsToResetRoom; }
            set { _CompleteQuarantinePersonsToResetRoom = value; OnPropertyChanged(); }
        }
        private Model.QuarantinePerson _SelectedCompleteQuarantinePersonsToResetRoom;

        public Model.QuarantinePerson SelectedCompleteQuarantinePersonsToResetRoom
        {
            get { return _SelectedCompleteQuarantinePersonsToResetRoom; }
            set { _SelectedCompleteQuarantinePersonsToResetRoom = value; OnPropertyChanged(); }
        }

        #endregion

        #region Command

        public ICommand CancelCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand AddPersonToResetRoomListCommand { get; set; }
        public ICommand RemovePersonFromResetRoomListCommand { get; set; }
        public ICommand AddAllPersonToResetRoomListCommand { get; set; }
        public ICommand RemoveAllPersonFromResetRoomListCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        #endregion
        public CompleteQuarantineRecommendationViewModel()
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
                if (CompleteQuarantinePersonsToResetRoom == null || CompleteQuarantinePersonsToResetRoom.Count() == 0)
                    return false;
                else
                    return true;
            }, (p) =>
            {
                ConfirmResetRoom();
                p.DialogResult = true;
                p.Close();
            });
            AddPersonToResetRoomListCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                AddPersonToResetRoomList(SelectedCompleteQuarantinePersons);
            });
            RemovePersonFromResetRoomListCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                RemovePersonFromResetRoomList(SelectedCompleteQuarantinePersonsToResetRoom);
            });
            AddAllPersonToResetRoomListCommand = new RelayCommand<object>((p) => {
                if (CompleteQuarantinePersons == null || CompleteQuarantinePersons.Count() == 0)
                    return false;
                else
                    return true;
            }, (p) =>
            {
                AddManyPersonToResetRoomList();
            });
            RemoveAllPersonFromResetRoomListCommand = new RelayCommand<object>((p) => {
                if (CompleteQuarantinePersonsToResetRoom == null || CompleteQuarantinePersonsToResetRoom.Count() == 0)
                    return false;
                else
                    return true;
            }, (p) =>
            {
                RemoveManyPersonFromResetRoomList();
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

            // Lọc danh sách người đã hoàn thành cách ly nhưng vẫn ở trong phòng.
            DateTime date = DateTime.Now.Date;
            CompleteQuarantinePersons = new ObservableCollection<QuarantinePerson>();

            // Khởi tạo danh sách người sẽ được thực thi đánh dấu hoàn thành cách ly.
            CompleteQuarantinePersonsToResetRoom = new ObservableCollection<QuarantinePerson>(quarantinePersonList.Where(person => person.roomID != null && person.leaveDate <= date));
        }

        // Hàm thêm người vào danh sách sẽ được đánh dấu hoàn thành và chuyển khỏi phòng.
        private void AddPersonToResetRoomList(QuarantinePerson person)
        {
            if (person == null)
                return;
            CompleteQuarantinePersonsToResetRoom.Add(person);
            CompleteQuarantinePersons.Remove(person);
        }

        // Hàm xóa người ra khỏi danh sách sẽ được đánh dấu hoàn thành và chuyển khỏi phòng.
        private void RemovePersonFromResetRoomList(QuarantinePerson person)
        {
            if (person == null)
                return;
            CompleteQuarantinePersons.Add(person);
            CompleteQuarantinePersonsToResetRoom.Remove(person);
        }

        // Hàm thêm nhiều người vào danh sách sẽ được đánh dấu hoàn thành và chuyển khỏi phòng.
        private void AddManyPersonToResetRoomList()
        {
            if (CompleteQuarantinePersons == null)
                return;
            var TempCompleteQuarantinePersons = new ObservableCollection<QuarantinePerson>(CompleteQuarantinePersons);
            foreach (var person in TempCompleteQuarantinePersons)
            {
                CompleteQuarantinePersonsToResetRoom.Add(person);
                CompleteQuarantinePersons.Remove(person);
            }
        }

        // Hàm xóa nhiều người ra khỏi danh sách sẽ được đánh dấu hoàn thành và chuyển khỏi phòng.
        private void RemoveManyPersonFromResetRoomList()
        {
            if (CompleteQuarantinePersonsToResetRoom == null)
                return;
            var TempCompleteQuarantinePersonsToResetRoom = new ObservableCollection<QuarantinePerson>(CompleteQuarantinePersonsToResetRoom);
            foreach (var person in TempCompleteQuarantinePersonsToResetRoom)
            {
                CompleteQuarantinePersons.Add(person);
                CompleteQuarantinePersonsToResetRoom.Remove(person);
            }
        }

        // Hàm thực thi thêm đánh dấu hoàn thành và chuyển người cách ly ra khỏi phòng trên database.
        private void ConfirmResetRoom()
        {
            try
            {
                foreach (var person in CompleteQuarantinePersonsToResetRoom)
                {
                    var tempPerson = DataProvider.ins.db.QuarantinePersons.Where(p => p.id == person.id).FirstOrDefault();
                    if (tempPerson == null)
                        return;
                    tempPerson.roomID = null;
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
