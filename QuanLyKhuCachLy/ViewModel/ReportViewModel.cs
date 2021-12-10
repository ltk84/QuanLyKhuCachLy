using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyKhuCachLy.ViewModel
{
    public class ReportViewModel : BaseViewModel
    {
        #region Property

        private int _SelectedTab;
        public int SelectedTab
        {
            get { return _SelectedTab; }
            set { _SelectedTab = value; OnPropertyChanged(); }
        }

        private DateTime _BeginDate;
        public DateTime BeginDate
        {
            get { return _BeginDate; }
            set { _BeginDate = value; OnPropertyChanged(); }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; OnPropertyChanged(); }
        }

        #region First Tab

        private ObservableCollection<string> _QuarantinePersonReportObjects;
        public ObservableCollection<string> QuarantinePersonReportObjects
        {
            get => _QuarantinePersonReportObjects; set
            {
                _QuarantinePersonReportObjects = value; OnPropertyChanged();
            }
        }

        private string _SelectedQuarantinePersonReportObjects;
        public string SelectedQuarantinePersonReportObjects
        {
            get => _SelectedQuarantinePersonReportObjects; set
            {
                _SelectedQuarantinePersonReportObjects = value; OnPropertyChanged();
            }
        }

        private SeriesCollection _FirstSeriesCollection;
        public SeriesCollection FirstSeriesCollection
        {
            get => _FirstSeriesCollection; set
            {
                _FirstSeriesCollection = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _FirstLabels;
        public ObservableCollection<string> FirstLabels
        {
            get => _FirstLabels; set
            {
                _FirstLabels = value; OnPropertyChanged();
            }
        }

        private Func<double, string> _QuarantinePersonFormatter;
        public Func<double, string> QuarantinePersonFormatter
        {
            get => _QuarantinePersonFormatter; set
            {
                _QuarantinePersonFormatter = value; OnPropertyChanged();
            }
        }

        #endregion

        #region Second Tab

        private ObservableCollection<string> _QuarantineAreaReportObjects;
        public ObservableCollection<string> QuarantineAreaReportObjects
        {
            get => _QuarantineAreaReportObjects; set
            {
                _QuarantineAreaReportObjects = value; OnPropertyChanged();
            }
        }

        private string _SelectedQuarantineAreaReportObjects;
        public string SelectedQuarantineAreaReportObjects
        {
            get => _SelectedQuarantineAreaReportObjects; set
            {
                _SelectedQuarantineAreaReportObjects = value; OnPropertyChanged();
            }
        }

        private SeriesCollection _SecondSeriesCollection;
        public SeriesCollection SecondSeriesCollection
        {
            get => _SecondSeriesCollection; set
            {
                _SecondSeriesCollection = value; OnPropertyChanged();
            }
        }

        #endregion

        #region Third Tab

        private ObservableCollection<string> _TestingReportObjects;
        public ObservableCollection<string> TestingReportObjects
        {
            get => _TestingReportObjects; set
            {
                _TestingReportObjects = value; OnPropertyChanged();
            }
        }

        private string _SelectedTestingReportObjects;
        public string SelectedTestingReportObjects
        {
            get => _SelectedTestingReportObjects; set
            {
                _SelectedTestingReportObjects = value; OnPropertyChanged();
            }
        }

        private SeriesCollection _ThirdSeriesCollection;
        public SeriesCollection ThirdSeriesCollection
        {
            get => _ThirdSeriesCollection; set
            {
                _ThirdSeriesCollection = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _ThirdLabels;
        public ObservableCollection<string> ThirdLabels
        {
            get => _ThirdLabels; set
            {
                _ThirdLabels = value; OnPropertyChanged();
            }
        }

        private Func<double, string> _TestingFormatter;
        public Func<double, string> TestingFormatter
        {
            get => _TestingFormatter; set
            {
                _TestingFormatter = value; OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Command

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand RefeshCommand { get; set; }


        #endregion

        public ReportViewModel()
        {
            Init();

            SelectionChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                switch (SelectedTab)
                {
                    case 0:
                        ToFirstTab();
                        break;
                    case 1:
                        ToSecondTab();
                        break;
                    case 2:
                        ToThirdTab();
                        break;
                }
            });

            PrintCommand = new RelayCommand<Visual>((p) =>
            {
                return true;
            }, (v) =>
            {
                try
                {
                    PrintDialog printDlg = new PrintDialog();
                    if (printDlg.ShowDialog() == true)
                    {
                        printDlg.PrintVisual(v, "Chart Printing");
                    }
                }
                catch
                {
                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    ErrorDialog.ShowDialog();
                }
            });

            RefeshCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (v) =>
            {
                Init();
            });
        }

        #region Method

        #region Fundamental Function

        private void Init()
        {
            EndDate = DateTime.Now.Date;
            BeginDate = DateTime.Now.Date;

            QuarantinePersonReportObjects = new ObservableCollection<string>() { "Người cách ly", "Nhóm đối tượng" };
            SelectedQuarantinePersonReportObjects = QuarantinePersonReportObjects[0];
            QuarantineAreaReportObjects = new ObservableCollection<string>() { "Sức chứa", "Phòng", "Mức độ phòng", "Nhân viên" };
            SelectedQuarantineAreaReportObjects = QuarantineAreaReportObjects[0];
            TestingReportObjects = new ObservableCollection<string>() { "Xét nghiệm" };
            SelectedTestingReportObjects = TestingReportObjects[0];

            QuarantinePersonFormatter = value => value.ToString("N0") + " người";
            TestingFormatter = value => value.ToString("N0") + " mẫu";

            ToFirstTab();
        }

        private void LoadChart(int SelectedTab, DateTime BeginDate, DateTime EndDate, string SelectedReportObject)
        {
            if (SelectedTab == 0)
            {
                if (SelectedReportObject.CompareTo(QuarantinePersonReportObjects[0]) == 0)
                {
                    // Report Object 1
                    LoadQuarantinePersonChart(BeginDate, EndDate);
                }
                else if (SelectedReportObject.CompareTo(QuarantinePersonReportObjects[1]) == 0)
                {
                    // Report Object 2
                    LoadTargetGroupChart(BeginDate, EndDate);
                }
            }
            else if (SelectedTab == 1)
            {
                if (SelectedReportObject.CompareTo(QuarantineAreaReportObjects[0]) == 0)
                {
                    LoadCapacityChart();
                }
                else if (SelectedReportObject.CompareTo(QuarantineAreaReportObjects[1]) == 0)
                {
                    LoadRoomChart();
                }
                else if (SelectedReportObject.CompareTo(QuarantineAreaReportObjects[2]) == 0)
                {
                    LoadRoomSeverityChart();
                }
                else if (SelectedReportObject.CompareTo(QuarantineAreaReportObjects[3]) == 0)
                {
                    LoadStafChart();
                }
            }
            else if (SelectedTab == 2)
            {
                if (SelectedReportObject.CompareTo(TestingReportObjects[0]) == 0)
                {
                    LoadTestingChart(BeginDate, EndDate);
                }
            }
        }

        #endregion

        #region Navigation

        private void ToFirstTab()
        {
            SelectedTab = 0;
            LoadChart(SelectedTab, BeginDate, EndDate, SelectedQuarantinePersonReportObjects);
        }

        private void ToSecondTab()
        {
            SelectedTab = 1;
            LoadChart(SelectedTab, BeginDate, EndDate, SelectedQuarantineAreaReportObjects);
        }

        private void ToThirdTab()
        {
            SelectedTab = 2;
            LoadChart(SelectedTab, BeginDate, EndDate, SelectedTestingReportObjects);
        }

        #endregion

        #region FirstTab

        #region Load Report Object 1

        private void LoadQuarantinePersonChart(DateTime BeginDate, DateTime EndDate)
        {
            FirstLabels = new ObservableCollection<string>();
            FirstSeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Số người đang cách ly",
                    Values = new ChartValues<int>(),
                    //DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Số người cách ly mới",
                    Values = new ChartValues<int>(),
                    //DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Số người hoàn thành",
                    Values = new ChartValues<int>(),
                    //DataLabels = true
                }
            };

            for (DateTime date = BeginDate; date <= EndDate; date = date.AddDays(1))
            {
                FirstLabels.Add(date.ToString("dd/MM/yyyy"));
                FirstSeriesCollection[0].Values.Add(CountQuarantinePerson(date));
                FirstSeriesCollection[1].Values.Add(CountNewQuarantinePerson(date));
                FirstSeriesCollection[2].Values.Add(CountCompeleteQuarantinePerson(date));
            }
        }

        private int CountQuarantinePerson(DateTime date)
        {
            int count = 0;
            try
            {
                // DataProvider.ins.db.Database.SqlQuery<string>("select count(id) from QuarantinePerson where leaveDate < @p0", date.ToString("MM/dd/yyyy"));
                count = DataProvider.ins.db.QuarantinePersons.Where(person => person.leaveDate > date && person.arrivedDate < date).Count();
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng người cách ly!");
            }

            return count;
        }

        private int CountNewQuarantinePerson(DateTime date)
        {
            int count = 0;
            try
            {
                // DataProvider.ins.db.Database.SqlQuery<int>("select count(id) from QuarantinePerson where arrivedDate = @p0", date.ToString("MM/dd/yyyy"));
                count = DataProvider.ins.db.QuarantinePersons.Where(person => person.arrivedDate == date).Count();
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng người cách ly mới!");
            }

            return count;
        }

        private int CountCompeleteQuarantinePerson(DateTime date)
        {

            int count = 0;
            try
            {
                // Do chưa có thuộc tính hoàn thành cách ly nên phải xét điều kiện leaveDate >= currentDate và sẽ được xem là hoàn thành cách ly nếu điều kiện này thỏa.
                // DataProvider.ins.db.Database.SqlQuery<int>("select count(id) from QuarantinePerson where leaveDate >= @p0", date.ToString("MM/dd/yyyy"));
                count = DataProvider.ins.db.QuarantinePersons.Where(person => person.leaveDate <= date).Count();
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng người hoàn thành cách ly!");
            }

            return count;
        }

        #endregion

        #region Load Report Object 2

        private void LoadTargetGroupChart(DateTime BeginDate, DateTime EndDate)
        {
            ObservableCollection<Model.Severity> SeverityList = new ObservableCollection<Model.Severity>(DataProvider.ins.db.Severities);

            FirstLabels = new ObservableCollection<string>();
            FirstSeriesCollection = new SeriesCollection();

            for (int i = 0; i < SeverityList.Count; i++)
            {
                FirstSeriesCollection.Add(new StackedColumnSeries
                {
                    Title = SeverityList[i].description,
                    Values = new ChartValues<int>(),
                    StackMode = StackMode.Values,
                    //DataLabels = true
                });
            }

            FirstSeriesCollection.Add(new StackedColumnSeries
            {
                Title = "Khác",
                Values = new ChartValues<int>(),
                StackMode = StackMode.Values,
                //DataLabels = true
            });

            for (DateTime date = BeginDate; date <= EndDate; date = date.AddDays(1))
            {
                FirstLabels.Add(date.ToString("dd/MM/yyyy"));
                for (int i = 0; i < SeverityList.Count; i++)
                {
                    FirstSeriesCollection[i].Values.Add(CountTargetGroup(date, SeverityList[i]));
                }

                FirstSeriesCollection[SeverityList.Count].Values.Add(CountTargetGroup(date, null));
            }
        }

        private int CountTargetGroup(DateTime date, Severity severity)
        {
            int count = 0;
            try
            {
                if (severity != null)
                {
                    count = DataProvider.ins.db.QuarantinePersons.Where(person => (person.leaveDate > date) && (person.arrivedDate <= date) && (person.levelID == severity.id)).Count();
                }
                else
                {
                    ObservableCollection<Model.QuarantinePerson> QuarantinePersonList = new ObservableCollection<Model.QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
                    count = QuarantinePersonList.Where(person => (person.leaveDate > date) && (person.arrivedDate <= date) && (person.levelID == null)).Count();
                }
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng người cách ly mới!");
            }

            return count;
        }

        #endregion

        #endregion

        #region SecondTab

        #region Load Report Object 1
        private void LoadCapacityChart()
        {
            int AvailableCapacity = CountAvailableCapacity();
            int OccupiedCapacity = CountOccupiedCapacity();
            if (AvailableCapacity != 0 || OccupiedCapacity != 0)
            {
                SecondSeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Giường trống",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(AvailableCapacity) },
                        DataLabels = AvailableCapacity != 0,
                    },
                    new PieSeries
                    {
                        Title = "Giường đã có người",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(OccupiedCapacity) },
                        DataLabels = OccupiedCapacity != 0
                    },
                };
            }
            else
            {
                SecondSeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Fill = Brushes.Gray,
                        Title = "Trống",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                        DataLabels = false
                    },
                };
            }
        }

        private int CountOccupiedCapacity()
        {
            ObservableCollection<Model.QuarantineRoom> QuarantineRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
            int count = 0;
            for (int i = 0; i < QuarantineRoomList.Count(); i++)
            {
                count += QuarantineRoomList[i].QuarantinePersons.Count();
            }
            return count;
        }

        private int CountAvailableCapacity()
        {
            ObservableCollection<Model.QuarantineRoom> QuarantineRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
            int count = 0;
            for (int i = 0; i < QuarantineRoomList.Count(); i++)
            {
                count += QuarantineRoomList[i].capacity - QuarantineRoomList[i].QuarantinePersons.Count();
            }
            return count;
        }

        #endregion

        #region Load Report Object 2

        private void LoadRoomChart()
        {
            int AvailableRoom = CountAvailableRoom();
            int OccupiedRoom = CountOccupiedRoom();
            if (AvailableRoom != 0 || OccupiedRoom != 0)
            {
                SecondSeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Số phòng còn trống",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(AvailableRoom) },
                        DataLabels = AvailableRoom != 0
                    },
                    new PieSeries
                    {
                        Title = "Số phòng đầy",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(OccupiedRoom) },
                        DataLabels = OccupiedRoom != 0
                    },
                };
            }
            else
            {
                SecondSeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Fill = Brushes.Gray,
                        Title = "Trống",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                        DataLabels = false
                    },
                };
            }
        }
        private int CountOccupiedRoom()
        {
            int count = 0;
            try
            {
                count = DataProvider.ins.db.QuarantineRooms.Where(room => room.QuarantinePersons.Count == room.capacity).Count();
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng phòng đã đầy!");
            }
            return count;
        }

        private int CountAvailableRoom()
        {
            int count = 0;
            try
            {
                count = DataProvider.ins.db.QuarantineRooms.Where(room => room.QuarantinePersons.Count < room.capacity).Count();
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng phòng trống!");
            }
            return count;
        }

        #endregion

        #region Load Report Object 3

        private void LoadRoomSeverityChart()
        {

            ObservableCollection<Model.Severity> SeverityList = new ObservableCollection<Model.Severity>(DataProvider.ins.db.Severities);

            SecondSeriesCollection = new SeriesCollection();

            for (int i = 0; i < SeverityList.Count; i++)
            {
                int RoomCount = CountRoomWithSeverity(SeverityList[i]);
                SecondSeriesCollection.Add(
                new PieSeries
                {
                    Title = SeverityList[i].description,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(RoomCount) },
                    DataLabels = RoomCount != 0
                });
            }

            int RoomNoSeverityCount = CountRoomWithSeverity(null);

            SecondSeriesCollection.Add(new PieSeries
            {
                Title = "Khác",
                Values = new ChartValues<ObservableValue> { new ObservableValue(RoomNoSeverityCount) },
                DataLabels = RoomNoSeverityCount != 0
            });

            if (DataProvider.ins.db.QuarantineRooms.Count() == 0)
            {
                SecondSeriesCollection.Add(
                new PieSeries
                {
                    Fill = Brushes.Gray,
                    Title = "Trống",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = false
                });
            }
        }

        private int CountRoomWithSeverity(Severity severity)
        {
            int count = 0;
            try
            {
                if (severity != null)
                {
                    count = DataProvider.ins.db.QuarantineRooms.Where(room => room.Severity.level == severity.level).Count();
                }
                else
                {
                    ObservableCollection<Model.QuarantineRoom> QuarantineRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
                    count = QuarantineRoomList.Where(room => room.levelID == null).Count();
                }
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng phòng theo mức độ!");
            }

            return count;
        }

        #endregion

        #region Load Report Object 4

        private void LoadStafChart()
        {
            ObservableCollection<Model.Staff> StaffList = new ObservableCollection<Staff>(DataProvider.ins.db.Staffs);
            var DepartmentList = StaffList.Select(staff => staff.department).Distinct();
            SecondSeriesCollection = new SeriesCollection();

            for (int i = 0; i < DepartmentList.Count(); i++)
            {
                int StaffCount = CountStaffWithDepartment(DepartmentList.ElementAt(i).ToString());
                SecondSeriesCollection.Add(
                new PieSeries
                {
                    Title = DepartmentList.ElementAt(i).ToString(),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(StaffCount) },
                    DataLabels = StaffCount != 0
                });
            }
            if (DepartmentList.Count() == 0 || DataProvider.ins.db.Staffs.Count() == 0)
            {
                SecondSeriesCollection.Add(
                new PieSeries
                {
                    Fill = Brushes.Gray,
                    Title = "Trống",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = false
                });
            }
        }

        private int CountStaffWithDepartment(string department)
        {
            int count = 0;
            try
            {
                count = DataProvider.ins.db.Staffs.Where(staff => staff.department == department).Count();
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng  nhân viên theo phòng ban!");
            }
            return count;
        }

        #endregion

        #endregion

        #region Third Tab

        private void LoadTestingChart(DateTime BeginDate, DateTime EndDate)
        {
            ThirdLabels = new ObservableCollection<string>();
            ThirdSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Số kết quả dương tính",
                    Values = new ChartValues<int>(),
                },
                new ColumnSeries
                {
                    Title = "Số kết quả âm tính",
                    Values = new ChartValues<int>(),
                },
            };

            for (DateTime date = BeginDate; date <= EndDate; date = date.AddDays(1))
            {
                ThirdLabels.Add(date.ToString("dd/MM/yyyy"));
                ThirdSeriesCollection[0].Values.Add(CountPositiveTestingResult(date));
                ThirdSeriesCollection[1].Values.Add(CountNegativeTestingResult(date));
            }
        }

        private int CountPositiveTestingResult(DateTime date)
        {
            int count = 0;
            try
            {
                count = DataProvider.ins.db.TestingResults.Where(result => result.dateTesting <= date && result.isPositive).Count();
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng xét nghiệm dương tính!");
            }
            return count;
        }

        private int CountNegativeTestingResult(DateTime date)
        {
            int count = 0;
            try
            {
                count = DataProvider.ins.db.TestingResults.Where(result => result.dateTesting <= date && !result.isPositive).Count();
            }
            catch
            {
                // MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng xét nghiệm âm tính!");
            }
            return count;
        }

        #endregion

        #endregion

    }
}
