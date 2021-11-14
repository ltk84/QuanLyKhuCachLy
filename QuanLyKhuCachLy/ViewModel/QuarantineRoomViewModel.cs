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
using System.IO;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Windows.Media;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantineRoomViewModel : BaseViewModel
    {

        #region property


        // Từ khóa cho việc tìm kiếm
        private String _SearchKey;
        public String SearchKey
        {
            get => _SearchKey; set
            {
                _SearchKey = value;
                OnPropertyChanged();
                SearchListRoom();
            }
        }

        // Các tùy chọn lọc
        private string[] _FilterType;
        public string[] FilterType
        {
            get => _FilterType; set
            {
                _FilterType = value; OnPropertyChanged();
            }
        }

        private string[] _FilterProperty;
        public string[] FilterProperty
        {
            get => _FilterProperty; set
            {
                _FilterProperty = value; OnPropertyChanged();
            }
        }

        // Các giá trị lọc
        private string _SelectedFilterType;
        public string SelectedFilterType
        {
            get => _SelectedFilterType; set
            {
                _SelectedFilterType = value;
                OnPropertyChanged();
                getFilterProperty();
            }
        }

        private String _SelectedFilterProperty;
        public String SelectedFilterProperty
        {
            get => _SelectedFilterProperty; set
            {
                _SelectedFilterProperty = value;
                OnPropertyChanged();
                SelectFilterProperty();
            }
        }

        #region list


        private Model.QuarantineRoom[] _RoomListView;
        public Model.QuarantineRoom[] RoomListView
        {
            get => _RoomListView; set
            {
                _RoomListView = value;
                OnPropertyChanged();
            }
        }


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
        public QuarantinePersonInRoomViewModel PersonInRoomViewModel
        {
            get => _PersonInRoomViewModel;
            set
            {
                _PersonInRoomViewModel = value;
                OnPropertyChanged();
            }
        }

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
                    PersonInRoomViewModel.Parent = this;

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

        private Visibility _Tab3;
        public Visibility Tab3
        {
            get => _Tab3; set
            {
                _Tab3 = value; OnPropertyChanged();
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
            Tab3 = Visibility.Hidden;

            RoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
            RoomListView = RoomList.ToArray();
            RoomLevelList = new ObservableCollection<Severity>(DataProvider.ins.db.Severities);




            RoomListView = RoomList.ToArray();
            FilterType = new string[] { "Loại phòng", "Sức chứa" };
            SelectedFilterType = "Tất cả";
            SelectedFilterProperty = "Chọn phương thức lọc";
            getFilterProperty();
            PersonInRoomViewModel = QuarantinePersonInRoomViewModel.ins;

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
            ToAddExcelCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddRoomFromExcel();
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
                Tab3 = Visibility.Hidden;
            });

            ToMainCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Tab2 = Visibility.Hidden;
                Tab1 = Visibility.Visible;
                Tab3 = Visibility.Hidden;
            });

            AddRoomManualCommand = new RelayCommand<Window>((p) =>
            {
                if (!DisplayNameFieldHasError && !CapacityFieldHasError) return true;
                return false;

            }, (p) =>
            {
                AddQuarantineRoom();
                p.Close();
            });

            EditRoomCommand = new RelayCommand<Window>((p) =>
            {
                if (!DisplayNameFieldHasError && !CapacityFieldHasError) return true;
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


        // hàm gọi khi thay đổi SearchKey, thay đổi giá trị của RoomListView.
        void SearchListRoom()
        {
            SelectedFilterType = "Tất cả";
            if (SearchKey == "")
            {
                RoomListView = RoomList.ToArray();

            }

            else
            {


                RoomListView = RoomList.ToArray();
                String[] Value = new string[RoomListView.Length];

                for (int i = 0; i < RoomListView.Length; i++)
                {
                    Value[i] = RoomListView[i].displayName.ToString() + "@@" + RoomListView[i].Severity.level.ToString() + "@@" + RoomListView[i].capacity.ToString();

                }

                RoomListView = RoomListView.Where((val, index) => Value[index].Contains(SearchKey)).ToArray();
            }
        }


        // Hàm cho filter
        void getFilterProperty()
        {
            SelectedFilterProperty = "Tất cả";

            //FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.GetType().GetProperty(SelectedFilterType)).Distinct();
            if (SelectedFilterType == "Tất cả")
            {
                SelectedFilterProperty = "Chọn phương thức lọc";
                FilterProperty = new string[] { "Chọn phương thức lọc" };
            }
            else if (SelectedFilterType == "Loại phòng")
            {
                FilterProperty = DataProvider.ins.db.QuarantineRooms.Select(room => room.Severity.level).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            else if (SelectedFilterType == "Sức chứa")
            {
                FilterProperty = DataProvider.ins.db.QuarantineRooms.Select(room => room.capacity.ToString()).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }
            

            RoomListView = DataProvider.ins.db.QuarantineRooms.ToArray();
        }

        void SelectFilterProperty()
        {
            if (SelectedFilterType == "Tất cả")
            {
            }
            else if (SelectedFilterType == "Loại phòng")
            {
                RoomListView = DataProvider.ins.db.QuarantineRooms.Where(x => x.Severity.level == SelectedFilterProperty).ToArray();
            }
            else if (SelectedFilterType == "Sức chứa")
            {
                RoomListView = DataProvider.ins.db.QuarantineRooms.Where(x => x.capacity.ToString() == SelectedFilterProperty).ToArray();
            }
      


        }





        void AddRoomFromExcel()
        {
            List<Model.QuarantineRoom> listRoom = new List<Model.QuarantineRoom>();
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx;*.xlsm;*xls)|*.xlsx;*.xlsm;*xls|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            else
                return;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            if (xlRange.Cells[1, 1] == null || xlRange.Cells[1, 1].Value2 != "STT" ||
            xlRange.Cells[1, 2] == null || xlRange.Cells[1, 2].Value2 != "Tên" ||
            xlRange.Cells[1, 3] == null || xlRange.Cells[1, 3].Value2 != "Sức chứa" ||
            xlRange.Cells[1, 4] == null || xlRange.Cells[1, 4].Value2 != "Loại phòng" || colCount != 4)
            {
                MessageBox.Show("Không đúng định dạng file");
                return;
            }
            for (int i = 2; i <= rowCount; i++)
            {
                Model.QuarantineRoom room = new Model.QuarantineRoom();
                if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                {
                    room.displayName = xlRange.Cells[i, 2].Value2.ToString();
                }
                if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                {
                    room.capacity = Int32.Parse(xlRange.Cells[i, 3].Value2.ToString());
                }
                if (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null)
                {
                    room.levelID = Int32.Parse(xlRange.Cells[i, 4].Value2.ToString());
                }
                listRoom.Add(room);
            }
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < listRoom.Count; i++)
                    {
                        DataProvider.ins.db.QuarantineRooms.Add(listRoom[i]);
                        DataProvider.ins.db.SaveChanges();

                        RoomList.Add(listRoom[i]);
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
                        Content = new SuccessNotification()
                    };
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
            RoomListView = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms).ToArray();
        }
        public void ToPersonInformation()
        {
            Tab1 = Visibility.Hidden;
            Tab2 = Visibility.Hidden;
            Tab3 = Visibility.Visible;
        }

        public void BackToRoomInformation()
        {
            Tab1 = Visibility.Hidden;
            Tab3 = Visibility.Hidden;
            Tab2 = Visibility.Visible;
        }

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
                    RoomListView = RoomList.ToArray();
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
                    RoomListView = RoomList.ToArray();
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
            RoomID = SelectedItem.id;
            RoomDisplayName = SelectedItem.displayName;
            RoomCapacity = SelectedItem.capacity;
            RoomSelectedSeverity = SelectedItem.Severity;
        }
        #endregion
    }
}