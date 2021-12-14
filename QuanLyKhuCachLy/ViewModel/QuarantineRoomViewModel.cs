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
using System.Threading.Tasks;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantineRoomViewModel : BaseViewModel
    {

        #region property


        #region search & filter
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
        #endregion

        #region list


        private Model.QuarantineRoom[] _RoomListView;
        public Model.QuarantineRoom[] RoomListView
        {
            get => _RoomListView; set
            {
                _RoomListView = value;
                OnPropertyChanged();
                updateAvailableSlot();
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
        public ICommand RefeshCommand { get; set; }

        public ICommand ToExportExcel { get; set; }

        public ICommand ToGetFormatExcel { get; set; }
        #endregion


        public QuarantineRoomViewModel()
        {
            SetDefaultUI();



            InitBasic();

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
                updateAvailableSlot();
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
                updateAvailableSlot();
            });

            ToViewCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                ToDetailRoomTab();

            });

            ToMainCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                BackToListRoomTab();
                updateAvailableSlot();
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
                EditQuarantineRoom(p);
            });

            ToExportExcel = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ExportExcel();
            });

            DeleteRoomCommand = new RelayCommand<object>((p) => { if (SelectedItem != null) return true; return false; }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                DeleteConfirmation confirmation = new DeleteConfirmation();
                if (confirmation.ShowDialog() == true)
                {
                    DeleteQuarantineRoom();
                    BackToListRoomTab();
                }
            });

            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            CompleteQuarantineCommand = new RelayCommand<object>((p) =>
            {
                if (PersonInRoomViewModel.QuarantinePersonList.Count > 0)
                    return true;
                return false;
            },
            (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                CompleteQuarantine();
            });

            ClearCommand = new RelayCommand<object>((p) =>
            {
                if (PersonInRoomViewModel.QuarantinePersonList.Count > 0)
                    return true;
                return false;
            }
            , (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                ClearPersonList();
            });
            ToGetFormatExcel = new RelayCommand<object>((p) =>
            {
                return true;
            }
            , (p) =>
            {
                GetFormatExcel();
            });
            RefeshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }
            , (p) =>
            {
                RefeshTab();
            });
        }

        #region method

        void updateAvailableSlot()
        {
            for (int i = 0; i < RoomListView.Length; i++)
            {
                int id = RoomListView[i].id;
                RoomListView[i].available = DataProvider.ins.db.QuarantinePersons.Where(person => person.roomID == id).ToArray().Length.ToString() + "/" + RoomListView[i].capacity.ToString();
            }
        }


        void RefeshTab()
        {
            SetDefaultUI();
            InitBasic();
            SelectedItem = null;
        }

        void InitBasic()
        {
            RoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
            RoomListView = RoomList.ToArray();
            RoomLevelList = new ObservableCollection<Severity>(DataProvider.ins.db.Severities);
            RoomListView = RoomList.ToArray();

            FilterType = new string[] { "Tất cả", "Nhóm đối tượng", "Sức chứa" };
            SelectedFilterType = "Tất cả";
            SelectedFilterProperty = "Chọn phương thức lọc";
            getFilterProperty();

            PersonInRoomViewModel = QuarantinePersonInRoomViewModel.ins;
        }

        void CompleteQuarantine()
        {
            PersonInRoomViewModel.CompeleteQuarantineRoom();
        }

        void ClearPersonList()
        {
            PersonInRoomViewModel.ClearPersonList();
        }

        void BackToListRoomTab()
        {
            Tab2 = Visibility.Hidden;
            Tab1 = Visibility.Visible;
            Tab3 = Visibility.Hidden;
        }

        public void SetDefaultUI()
        {
            Tab2 = Visibility.Hidden;
            Tab1 = Visibility.Visible;
            Tab3 = Visibility.Hidden;
        }

        void ToDetailRoomTab()
        {
            Tab1 = Visibility.Hidden;
            Tab2 = Visibility.Visible;
            Tab3 = Visibility.Hidden;
        }

        // hàm gọi khi thay đổi SearchKey, thay đổi giá trị của RoomListView.
        void SearchListRoom()
        {
            SelectFilterProperty();

            if (SearchKey == "" || SearchKey == null)
            {
            }
            else
            {


                String[] Value = new string[RoomListView.Length];

                for (int i = 0; i < RoomListView.Length; i++)
                {
                    Value[i] = RoomListView[i].displayName?.ToString() + "@@" + RoomListView[i].capacity.ToString() + "@@" + RoomListView[i].id.ToString();

                }

                RoomListView = RoomListView.Where((val, index) => Value[index].ToUpper().Contains(SearchKey.ToUpper())).ToArray();


            }
        }


        // Hàm cho filter
        void getFilterProperty()
        {
            SelectedFilterProperty = "";
            SearchKey = "";
            //FilterProperty = DataProvider.ins.db.Staffs.Select(staff => staff.GetType().GetProperty(SelectedFilterType)).Distinct();
            if (SelectedFilterType == "Tất cả")
            {
                SelectedFilterProperty = "Chọn phương thức lọc";
                FilterProperty = new string[] { "Chọn phương thức lọc" };
            }
            else if (SelectedFilterType == "Nhóm đối tượng")
            {
                
                FilterProperty = RoomList.Select(room =>
                {
                    if (room.Severity == null) return "Chưa thiết lập";
                    else return room.Severity?.description.ToString();
                }).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
                
            }
            else if (SelectedFilterType == "Sức chứa")
            {
                FilterProperty = RoomList.Select(room => room.capacity.ToString()).ToArray();
                FilterProperty = FilterProperty.Distinct().ToArray();
            }


            RoomListView = RoomList.ToArray();
        }

        void SelectFilterProperty()
        {
            RoomListView = RoomList.ToArray();
            if (SelectedFilterProperty == "" || SelectedFilterProperty == null || SelectedFilterProperty == "Tất cả") return;

            if (SelectedFilterType == "Tất cả")
            {
            }
            else if (SelectedFilterType == "Nhóm đối tượng")
            {
                RoomListView = RoomList.Where(x =>
                {
                    if (SelectedFilterProperty == "Chưa thiết lập") {
                        return x.Severity == null;
                    }
                    else return x.Severity?.description == SelectedFilterProperty;
                }).ToArray();
            }
            else if (SelectedFilterType == "Sức chứa")
            {
                RoomListView = RoomList.Where(x => x.capacity.ToString() == SelectedFilterProperty).ToArray();
            }

        }





        async void AddRoomFromExcel()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xlsx;*.xlsm;*xls)|*.xlsx;*.xlsm;*xls|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            else
                return;
            LoadingIndicator loadingIndicator = new LoadingIndicator();
            Task task = ExecuteAddRoomFromExcel(loadingIndicator, path);
            loadingIndicator.ShowDialog();
            await task;
        }

        async Task ExecuteAddRoomFromExcel(LoadingIndicator loadingIndicator, string path)
        {
            bool isSuccess = false;
            string error = "";
            await Task.Run(() =>
            {
                List<Model.QuarantineRoom> listRoom = new List<Model.QuarantineRoom>();
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
                xlWorkbook.RefreshAll();
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;
                if (xlRange.Cells[1, 1] == null || xlRange.Cells[1, 1].Value2.ToString().Trim().ToLower() != "stt" ||
                xlRange.Cells[1, 2] == null || xlRange.Cells[1, 2].Value2.ToString().Trim().ToLower() != "tên" ||
                xlRange.Cells[1, 3] == null || xlRange.Cells[1, 3].Value2.ToString().Trim().ToLower() != "sức chứa" ||
                xlRange.Cells[1, 4] == null || xlRange.Cells[1, 4].Value2.ToString().Trim().ToLower() != "nhóm đối tượng")
                {
                    xlWorkbook.Close();
                    error = "Không đúng định dạng file";
                    return;
                }

                List<int> listSTT = new List<int>();
                for (int i = 2; i <= rowCount; i++)
                {
                    Model.QuarantineRoom room = new Model.QuarantineRoom();
                    if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                    {
                        int t;
                        if (Int32.TryParse(xlRange.Cells[i, 1].Value2.ToString(), out t))
                        {
                            if (t <= 0)
                            {
                                xlWorkbook.Close();
                                error = "STT để bé hơn 1";
                                return;
                            }
                            else
                            {
                                if (listSTT.Contains(t))
                                {
                                    xlWorkbook.Close();
                                    error = "STT " + t.ToString() + " bị trùng";
                                    return;
                                }
                                listSTT.Add(t);
                            }
                        }
                        else
                        {
                            xlWorkbook.Close();
                            error = "STT không là số";
                            return;
                        }
                    }
                    else
                    {
                        xlWorkbook.Close();
                        error = "STT để trống";
                        return;
                    }
                    if (xlRange.Cells[i, 2] != null && xlRange.Cells[i, 2].Value2 != null)
                    {
                        room.displayName = xlRange.Cells[i, 2].Value2.ToString();
                    }
                    else
                    {
                        xlWorkbook.Close();
                        error = "Tên để trống";
                        return;
                    }
                    if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                    {
                        int t;
                        if (Int32.TryParse(xlRange.Cells[i, 3].Value2.ToString(), out t))
                        {
                            room.capacity = Int32.Parse(xlRange.Cells[i, 3].Value2.ToString());
                            if (t <= 0)
                            {
                                error = "Phòng " + xlRange.Cells[i, 2].Value2.ToString() + " sức chứa bé hơn 0";
                                xlWorkbook.Close();
                                return;
                            }
                        }
                        else
                        {
                            error = "Phòng " + xlRange.Cells[i, 2].Value2.ToString() + " sức chứa không là số";
                            xlWorkbook.Close();
                            return;
                        };

                    }
                    else
                    {
                        error = "Phòng " + xlRange.Cells[i, 2].Value2.ToString() + " sức chứa trống";
                        xlWorkbook.Close();
                        return;
                    }

                    if (xlRange.Cells[i, 4] != null && xlRange.Cells[i, 4].Value2 != null)
                    {
                        string description = xlRange.Cells[i, 4].Value2.ToString();
                        int levelId;
                        bool checkLevel = DataProvider.ins.db.Severities.Where(x => x.description == description).Count() >= 1 ? true : false;
                        if (checkLevel)
                        {
                            levelId = DataProvider.ins.db.Severities.Where(x => x.description == description).FirstOrDefault().id;
                            room.levelID = levelId;
                        }
                        else
                        {
                            error = "Phòng " + xlRange.Cells[i, 2].Value2.ToString() + " loại phòng không đúng";
                            xlWorkbook.Close();
                            return;
                        }
                    }
                    listRoom.Add(room);
                }
                xlWorkbook.Close();
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
                        transaction.Commit();
                        isSuccess = true;
                    }
                    catch (DbUpdateException e)
                    {
                        transaction.Rollback();
                    }
                    catch (DbEntityValidationException e)
                    {
                        transaction.Rollback();
                    }
                    catch (NotSupportedException e)
                    {
                        transaction.Rollback();
                    }
                    catch (ObjectDisposedException e)
                    {
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException e)
                    {
                        transaction.Rollback();
                    }
                }
                RoomListView = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms).ToArray();
            });
            loadingIndicator.Close();
            if (isSuccess)
            {

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
                SuccessDialog.ShowDialog();
            }
            else
            {
                CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                if (error != "" && error != null)
                {
                    FailNotificationVM.Content = error;
                }
                ErrorDialog.ShowDialog();
            }
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
                    Model.QuarantineRoom QuarantineRoom = new Model.QuarantineRoom { displayName = RoomDisplayName, capacity = RoomCapacity, levelID = RoomSelectedSeverity?.id };

                    DataProvider.ins.db.QuarantineRooms.Add(QuarantineRoom);
                    DataProvider.ins.db.SaveChanges();

                    RoomList.Add(QuarantineRoom);
                    RoomListView = RoomList.ToArray();
                    transaction.Commit();

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }

            }
        }

        void EditQuarantineRoom(Window ph)
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    Model.QuarantineRoom QuarantineRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    QuarantineRoom.displayName = RoomDisplayName;
                    QuarantineRoom.capacity = RoomCapacity;

                    if (QuarantineRoom.levelID != RoomSelectedSeverity?.id)
                    {
                        ActionConfirmation ConfirmScreen = new ActionConfirmation();
                        var vm = ConfirmScreen.DataContext as ActionConfirmationViewModel;
                        vm.Title = "Thay đổi mức độ nhóm đối tượng";
                        vm.Content = "Bạn có muốn thay đổi nhóm đối tượng của những người trong phòng theo giá trị vừa thay đổi của phòng?";
                        vm.IsThreeButton = true;
                        var result = ConfirmScreen.ShowDialog();
                        if (result == true)
                        {
                            if (vm.IsYes)
                            {
                                foreach (var p in QuarantineRoom.QuarantinePersons)
                                {
                                    p.levelID = RoomSelectedSeverity?.id;
                                }
                            }
                        }
                        else
                        {
                            return;
                        }

                    }

                    QuarantineRoom.levelID = RoomSelectedSeverity?.id;

                    DataProvider.ins.db.SaveChanges();

                    SelectedItem = QuarantineRoom;

                    ph.Close();

                    transaction.Commit();

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
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

                    RoomList.Remove(Room);
                    DataProvider.ins.db.QuarantineRooms.Remove(Room);
                    DataProvider.ins.db.SaveChanges();

                    RoomListView = RoomList.ToArray();
                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
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
        void ExportExcel()
        {
            int count = RoomListView.Length;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            Microsoft.Office.Interop.Excel.Workbook file = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet sheet = file.Worksheets[1];
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 15;
            sheet.Columns[3].ColumnWidth = 10;
            sheet.Columns[4].ColumnWidth = 10;
            sheet.Columns[5].ColumnWidth = 15;
            sheet.Range["A1"].Value = "STT";
            sheet.Range["B1"].Value = "Tên";
            sheet.Range["C1"].Value = "Sức chứa";
            sheet.Range["D1"].Value = "Còn trống";
            sheet.Range["E1"].Value = "Nhóm đối tượng";

            for (int i = 2; i <= count + 1; i++)
            {
                int roomID = RoomListView[i - 2].id;
                Severity severity = new Severity();
                if (RoomListView[i - 2].Severity != null)
                {
                    int severityID = RoomListView[i - 2].levelID.Value;
                    severity = DataProvider.ins.db.Severities.Where(x => x.id == severityID).FirstOrDefault();
                }
                int countInRoom = DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == roomID).Count();
                sheet.Range["A" + i.ToString()].Value = (i - 1).ToString();
                sheet.Range["B" + i.ToString()].Value = RoomListView[i - 2].displayName;
                sheet.Range["C" + i.ToString()].Value = RoomListView[i - 2].capacity;
                sheet.Range["D" + i.ToString()].Value = (RoomListView[i - 2].capacity - countInRoom).ToString();
                sheet.Range["E" + i.ToString()].Value = RoomListView[i - 2].levelID != null ? severity.description : "";
            }
            file.Close();
        }
        void GetFormatExcel()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            Microsoft.Office.Interop.Excel.Workbook file = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet sheet = file.Worksheets[1];
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 15;
            sheet.Columns[3].ColumnWidth = 10;
            sheet.Columns[4].ColumnWidth = 10;
            sheet.Range["A1"].Value = "STT";
            sheet.Range["B1"].Value = "Tên";
            sheet.Range["C1"].Value = "Sức chứa";
            sheet.Range["D1"].Value = "Nhóm đối tượng";
            sheet.Range["E2"].Value = "Lưu ý: Nhóm đối tượng có thể để trống";
            sheet.Range["E3"].Value = "Xóa lưu ý trước khi thêm";
            file.Close();
        }
        #endregion
    }
}