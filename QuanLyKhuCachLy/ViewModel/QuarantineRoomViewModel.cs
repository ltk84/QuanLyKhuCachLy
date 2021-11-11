using MaterialDesignThemes.Wpf;
using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantineRoomViewModel : BaseViewModel
    {

        #region property
        #region list
        private ObservableCollection<Model.QuarantineRoom> _RoomList;
        public ObservableCollection<Model.QuarantineRoom> RoomList
        {
            get => _RoomList; set
            {
                _RoomList = value;
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
        #endregion

        #region child view model
        private QuarantinePersonInRoomViewModel _PersonInRoomViewModel;
        #endregion

        private Model.QuarantineRoom _SelectedItem;
        public Model.QuarantineRoom SelectedItem
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

        #region Room
        private int _RoomID;
        public int RoomID
        {
            get => _RoomID; set
            {
                _RoomID = value;
                OnPropertyChanged();
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

        private int _RoomCapacity;
        public int RoomCapacity
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
        #endregion

        #region ui
        private Visibility _Tab1;
        public Visibility Tab1
        {
            get => _Tab1; set
            {
                _Tab1 = value; OnPropertyChanged();
            }
        }

        private Visibility _Tab2;
        public Visibility Tab2
        {
            get => _Tab2; set
            {
                _Tab2 = value; OnPropertyChanged();
            }
        }


        #endregion

        #region validation
        private bool _DisplayNameFieldHasError;
        public bool DisplayNameFieldHasError
        {
            get => _DisplayNameFieldHasError; set
            {
                _DisplayNameFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _CapacityFieldHasError;
        public bool CapacityFieldHasError
        {
            get => _CapacityFieldHasError; set
            {
                _CapacityFieldHasError = value; OnPropertyChanged();
            }
        }

        private bool _SeverityFieldHasError;
        public bool SeverityFieldHasError
        {
            get => _SeverityFieldHasError; set
            {
                _SeverityFieldHasError = value; OnPropertyChanged();
            }
        }
        #endregion

        #endregion

        #region command
        public ICommand AddRoomManualCommand { get; set; }
        public ICommand AddRoomExcelCommand { get; set; }
        public ICommand EditRoomCommand { get; set; }
        public ICommand DeleteRoomCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CompleteQuarantineCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public ICommand ToAddManualCommand { get; set; }
        public ICommand ToAddExcelCommand { get; set; }
        public ICommand ToEditCommand { get; set; }
        public ICommand ToViewCommand { get; set; }
        public ICommand ToMainCommand { get; set; }
        #endregion


        public QuarantineRoomViewModel()
        {

            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;

            RoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
            RoomLevelList = new ObservableCollection<Severity>(DataProvider.ins.db.Severities);

            _PersonInRoomViewModel = new QuarantinePersonInRoomViewModel(CurrentRoomID: RoomID);

            ToAddManualCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ClearData();
                AddRoom AddRoomScreen = new AddRoom();
                AddRoomScreen.ShowDialog();
                ClearData();
            });

            ToEditCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem == null) return false;
                return true;
            }, (p) =>
            {
                EditRoom EditScreen = new EditRoom();
                SetSelectedItemToProperty();
                EditScreen.ShowDialog();
            });

            ToViewCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Tab1 = Visibility.Hidden;
                Tab2 = Visibility.Visible;

            });

            ToMainCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Tab2 = Visibility.Hidden;
                Tab1 = Visibility.Visible;
            });

            AddRoomManualCommand = new RelayCommand<Window>((p) =>
            {
                if (!DisplayNameFieldHasError && !CapacityFieldHasError && !SeverityFieldHasError) return true;
                return false;

            }, (p) =>
            {
                AddQuarantineRoom();
                p.Close();
            });

            EditRoomCommand = new RelayCommand<Window>((p) =>
            {
                if (!DisplayNameFieldHasError && !CapacityFieldHasError && !SeverityFieldHasError) return true;
                return false;
            }, (p) =>
            {
                EditQuarantineRoom();
                p.Close();
            });

            DeleteRoomCommand = new RelayCommand<object>((p) => { if (SelectedItem != null) return true; return false; }, (p) =>
            {
                DeleteQuarantineRoom();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            ClearCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });
        }

        #region method
        void AddQuarantineRoom()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    // List Severity được tạo từ trước nên không cần thêm
                    Model.QuarantineRoom QuarantineRoom = new Model.QuarantineRoom { displayName = RoomDisplayName, capacity = RoomCapacity, levelID = RoomSelectedSeverity.id };

                    DataProvider.ins.db.QuarantineRooms.Add(QuarantineRoom);
                    DataProvider.ins.db.SaveChanges();

                    RoomList.Add(QuarantineRoom);

                    transaction.Commit();

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db update";

                    MessageBox.Show(error);
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi validation";

                    MessageBox.Show(error);
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db đéo support";

                    MessageBox.Show(error);
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db object disposed";

                    MessageBox.Show(error);
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi invalid operation";

                    MessageBox.Show(error);
                }

            }
        }
        //untest
        void EditQuarantineRoom()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    Model.QuarantineRoom QuarantineRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    QuarantineRoom.displayName = RoomDisplayName;
                    QuarantineRoom.capacity = RoomCapacity;
                    QuarantineRoom.levelID = RoomSelectedSeverity.id;

                    DataProvider.ins.db.SaveChanges();

                    SelectedItem = QuarantineRoom;

                    transaction.Commit();

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db update";

                    MessageBox.Show(error);
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi validation";

                    MessageBox.Show(error);
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db đéo support";

                    MessageBox.Show(error);
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db object disposed";

                    MessageBox.Show(error);
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi invalid operation";

                    MessageBox.Show(error);
                }

            }
        }
        void DeleteQuarantineRoom()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var Room = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Room == null) return;

                    DataProvider.ins.db.QuarantineRooms.Remove(Room);
                    DataProvider.ins.db.SaveChanges();

                    RoomList.Remove(Room);

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db update";

                    MessageBox.Show(error);
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi validation";

                    MessageBox.Show(error);
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db đéo support";

                    MessageBox.Show(error);
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db object disposed";

                    MessageBox.Show(error);
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi invalid operation";

                    MessageBox.Show(error);
                }

            }
        }
        void ClearData()
        {
            RoomDisplayName = "";
            RoomCapacity = 0;
            RoomSelectedSeverity = null;
        }
        void SetSelectedItemToProperty()
        {
            RoomDisplayName = SelectedItem.displayName;
            RoomCapacity = SelectedItem.capacity;
            RoomSelectedSeverity = SelectedItem.Severity;
        }
        #endregion
    }
}