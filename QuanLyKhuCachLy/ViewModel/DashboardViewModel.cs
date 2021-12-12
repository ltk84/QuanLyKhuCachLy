using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyKhuCachLy.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        #region Property

        #region General Statistic
        private int _QuarantinePersonCount;

        public int QuarantinePersonCount
        {
            get { return _QuarantinePersonCount; }
            set { _QuarantinePersonCount = value; OnPropertyChanged(); }
        }

        private int _NewQuarantinePersonCount;

        public int NewQuarantinePersonCount
        {
            get { return _NewQuarantinePersonCount; }
            set { _NewQuarantinePersonCount = value; OnPropertyChanged(); }
        }

        // QP = Quarantine Person
        private int _QPWithNoRoomCount;

        public int QPWithNoRoomCount
        {
            get { return _QPWithNoRoomCount; }
            set { _QPWithNoRoomCount = value; OnPropertyChanged(); }
        }

        // QP = Quarantine Person
        private int _QPFinishCount;

        public int QPFinishCount
        {
            get { return _QPFinishCount; }
            set { _QPFinishCount = value; OnPropertyChanged(); }
        }

        private int _AvailableCapacity;

        public int AvailableCapacity
        {
            get { return _AvailableCapacity; }
            set { _AvailableCapacity = value; OnPropertyChanged(); }
        }

        #endregion

        #region Chart

        private ObservableCollection<string> _DashboardChartObjects;
        public ObservableCollection<string> DashboardChartObjects
        {
            get => _DashboardChartObjects; set
            {
                _DashboardChartObjects = value; OnPropertyChanged();
            }
        }

        private string _SelectedDashboardChartObject;
        public string SelectedDashboardChartObject
        {
            get => _SelectedDashboardChartObject; set
            {
                _SelectedDashboardChartObject = value; OnPropertyChanged();
            }
        }

        #region FirstChart

        private Visibility _FirstChartVisibility;
        public Visibility FirstChartVisibility
        {
            get => _FirstChartVisibility; set
            {
                _FirstChartVisibility = value; OnPropertyChanged();
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

        private Func<double, string> _FirstChartFormatter;
        public Func<double, string> FirstChartFormatter
        {
            get => _FirstChartFormatter; set
            {
                _FirstChartFormatter = value; OnPropertyChanged();
            }
        }

        #endregion

        #region SecondChart

        private Visibility _SecondChartVisibility;
        public Visibility SecondChartVisibility
        {
            get => _SecondChartVisibility; set
            {
                _SecondChartVisibility = value; OnPropertyChanged();
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

        #region ThirdChart

        private Visibility _ThirdChartVisibility;
        public Visibility ThirdChartVisibility
        {
            get => _ThirdChartVisibility; set
            {
                _ThirdChartVisibility = value; OnPropertyChanged();
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

        private Func<double, string> _ThirdChartFormatter;
        public Func<double, string> ThirdChartFormatter
        {
            get => _ThirdChartFormatter; set
            {
                _ThirdChartFormatter = value; OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Recommendation

        private Visibility _NoneRecommendationVisibility;

        public Visibility NoneRecommendationVisibility
        {
            get { return _NoneRecommendationVisibility; }
            set { _NoneRecommendationVisibility = value; OnPropertyChanged(); }
        }

        #region First Recommendation

        private Visibility _FirstRecommendationVisibility;

        public Visibility FirstRecommendationVisibility
        {
            get { return _FirstRecommendationVisibility; }
            set { _FirstRecommendationVisibility = value; OnPropertyChanged(); }
        }

        private string _FirstRecommendationTitle;

        public string FirstRecommendationTitle
        {
            get { return _FirstRecommendationTitle; }
            set { _FirstRecommendationTitle = value; OnPropertyChanged(); }
        }

        private string _FirstRecommendationContent;

        public string FirstRecommendationContent
        {
            get { return _FirstRecommendationContent; }
            set { _FirstRecommendationContent = value; OnPropertyChanged(); }
        }


        #endregion

        #region Second Recommendation

        private Visibility _SecondRecommendationVisibility;

        public Visibility SecondRecommendationVisibility
        {
            get { return _SecondRecommendationVisibility; }
            set { _SecondRecommendationVisibility = value; OnPropertyChanged(); }
        }

        private string _SecondRecommendationTitle;

        public string SecondRecommendationTitle
        {
            get { return _SecondRecommendationTitle; }
            set { _SecondRecommendationTitle = value; OnPropertyChanged(); }
        }

        private string _SecondRecommendationContent;

        public string SecondRecommendationContent
        {
            get { return _SecondRecommendationContent; }
            set { _SecondRecommendationContent = value; OnPropertyChanged(); }
        }

        #endregion

        #region Third Recommendation

        private Visibility _ThirdRecommendationVisibility;

        public Visibility ThirdRecommendationVisibility
        {
            get { return _ThirdRecommendationVisibility; }
            set { _ThirdRecommendationVisibility = value; OnPropertyChanged(); }
        }

        private string _ThirdRecommendationTitle;

        public string ThirdRecommendationTitle
        {
            get { return _ThirdRecommendationTitle; }
            set { _ThirdRecommendationTitle = value; OnPropertyChanged(); }
        }

        private string _ThirdRecommendationContent;

        public string ThirdRecommendationContent
        {
            get { return _ThirdRecommendationContent; }
            set { _ThirdRecommendationContent = value; OnPropertyChanged(); }
        }

        #endregion

        #region Fourth Recommendation

        private Visibility _FourthRecommendationVisibility;

        public Visibility FourthRecommendationVisibility
        {
            get { return _FourthRecommendationVisibility; }
            set { _FourthRecommendationVisibility = value; OnPropertyChanged(); }
        }

        private string _FourthRecommendationTitle;

        public string FourthRecommendationTitle
        {
            get { return _FourthRecommendationTitle; }
            set { _FourthRecommendationTitle = value; OnPropertyChanged(); }
        }

        private string _FourthRecommendationContent;

        public string FourthRecommendationContent
        {
            get { return _FourthRecommendationContent; }
            set { _FourthRecommendationContent = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion


        #endregion

        #region Command
        public ICommand SelectionChangedCommand { get; set; }

        public ICommand Refresh { get; set; }

        public ICommand ToArrangeRoomRecommendation { get; set; }
        public ICommand ToGuideNotificationRecommendation { get; set; }
        public ICommand ToFinishNotificationRecommendation { get; set; }
        public ICommand ToCompleteQuarantineRecommendation { get; set; }

        #endregion

        public DashboardViewModel()
        {
            Refresh = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Init();
            });

            Init();

        }
        #region Method

        public void Init()
        {
            RecommendationInit();
            StatisticInit();
            ChartInit();
        }

        #region Fundamental Function

        private void RecommendationInit()
        {
            DefineFirstRecommendation();
            DefineSecondRecommendation();
            DefineThirdRecommendation();
            DefineFourthRecommendation();

            if (FirstRecommendationVisibility == Visibility.Collapsed && SecondRecommendationVisibility == Visibility.Collapsed && ThirdRecommendationVisibility == Visibility.Collapsed && FourthRecommendationVisibility == Visibility.Collapsed)
            {
                NoneRecommendationVisibility = Visibility.Visible;
            }
            else
            {
                NoneRecommendationVisibility = Visibility.Collapsed;
            }

            ToArrangeRoomRecommendation = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ArrangeRoomRecommendation arrangeRoomRecommendation = new ArrangeRoomRecommendation();
                
                if (arrangeRoomRecommendation.ShowDialog() == true)
                {
                    Init();
                }
            });

            ToGuideNotificationRecommendation = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ProposeNotification proposeNotification = new ProposeNotification();
                var proposeNotificationVM = proposeNotification.DataContext as ProposeNotificationViewModel;
                proposeNotificationVM.Type = 1;
                if (proposeNotification.ShowDialog() == true)
                {
                    Init();
                }
            });

            ToFinishNotificationRecommendation = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ProposeNotification proposeNotification = new ProposeNotification();
                var proposeNotificationVM = proposeNotification.DataContext as ProposeNotificationViewModel;
                proposeNotificationVM.Type = 2;
                if (proposeNotification.ShowDialog() == true)
                {
                    Init();
                }
            });

            ToCompleteQuarantineRecommendation = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                CompleteQuarantineRecommendation completeQuarantineecommendation = new CompleteQuarantineRecommendation();

                if (completeQuarantineecommendation.ShowDialog() == true)
                {
                    Init();
                }
            });
        }

        private void StatisticInit()
        {
            QuarantinePersonCount = CountQuarantinePersonStatistic();
            NewQuarantinePersonCount = CountNewQuarantinePersonStatistic();
            QPWithNoRoomCount = CountQuarantinePersonWithNoRoomStatistic();
            QPFinishCount = CountFinishQuarantinePersonStatistic();
            AvailableCapacity = CountAvailableCapacityStatistic();
        }

        private void ChartInit()
        {
            DateTime BeginDate = DateTime.Now.AddDays(-7).Date;
            DateTime EndDate = DateTime.Now.Date;

            FirstChartVisibility = Visibility.Visible;
            SecondChartVisibility = Visibility.Hidden;
            ThirdChartVisibility = Visibility.Hidden;

            DashboardChartObjects = new ObservableCollection<string>()
            {
                "Người cách ly trong 7 ngày qua",
                "Nhóm đối tượng trong 7 ngày qua",
                "Sức chứa khu cách ly",
                "Số phòng trống",
                "Số phòng theo mức độ",
                "Nhân viên theo phòng ban",
                "Kết quả xét nghiệm trong 7 ngày qua"
            };

            SelectedDashboardChartObject = DashboardChartObjects[0];

            FirstChartFormatter = value => value.ToString("N0");
            ThirdChartFormatter = value => value.ToString("N0");

            SelectionChangedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                LoadChart(BeginDate, EndDate, SelectedDashboardChartObject);
            });

            LoadChart(BeginDate, EndDate, SelectedDashboardChartObject);
        }

        private void LoadChart(DateTime BeginDate, DateTime EndDate, string SelectedReportObject)
        {
            if (SelectedReportObject.CompareTo(DashboardChartObjects[0]) == 0)
            {
                ToFirstChart();
                LoadQuarantinePersonChart(BeginDate, EndDate);
            }
            else if (SelectedReportObject.CompareTo(DashboardChartObjects[1]) == 0)
            {
                ToFirstChart();
                LoadTargetGroupChart(BeginDate, EndDate);
            }
            else if (SelectedReportObject.CompareTo(DashboardChartObjects[2]) == 0)
            {
                ToSecondChart();
                LoadCapacityChart();
            }
            else if (SelectedReportObject.CompareTo(DashboardChartObjects[3]) == 0)
            {
                ToSecondChart();
                LoadRoomChart();
            }
            else if (SelectedReportObject.CompareTo(DashboardChartObjects[4]) == 0)
            {
                ToSecondChart();
                LoadRoomSeverityChart();
            }
            else if (SelectedReportObject.CompareTo(DashboardChartObjects[5]) == 0)
            {
                ToSecondChart();
                LoadStafChart();
            }
            if (SelectedReportObject.CompareTo(DashboardChartObjects[6]) == 0)
            {
                ToThirdChart();
                LoadTestingChart(BeginDate, EndDate);
            }
        }

        #endregion

        #region Navigation
        private void ToFirstChart()
        {
            FirstChartVisibility = Visibility.Visible;
            SecondChartVisibility = Visibility.Hidden;
            ThirdChartVisibility = Visibility.Hidden;
        }

        private void ToSecondChart()
        {
            FirstChartVisibility = Visibility.Hidden;
            SecondChartVisibility = Visibility.Visible;
            ThirdChartVisibility = Visibility.Hidden;
        }

        private void ToThirdChart()
        {
            FirstChartVisibility = Visibility.Hidden;
            SecondChartVisibility = Visibility.Hidden;
            ThirdChartVisibility = Visibility.Visible;
        }

        #endregion

        #region First Recommendation

        private void DefineFirstRecommendation()
        {
            int QPWithNoRoomCount = CountQuarantinePersonWithNoRoomStatistic();
            if (QPWithNoRoomCount != 0)
            {
                FirstRecommendationVisibility = Visibility.Visible;
                FirstRecommendationTitle = "SẮP XẾP PHÒNG CHO NGƯỜI CÁCH LY";
                FirstRecommendationContent = $"Có {QPWithNoRoomCount} người chưa được sắp xếp phòng.";
            }
            else
            {
                FirstRecommendationVisibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region Second Recommendation

        private void DefineSecondRecommendation()
        {
            int NewQuarantinePersonCount = CountNewQuarantinePersonStatistic();
            if (NewQuarantinePersonCount != 0)
            {
                SecondRecommendationVisibility = Visibility.Visible;
                SecondRecommendationTitle = "GỬI THÔNG BÁO HƯỚNG DẪN CÁCH LY";
                SecondRecommendationContent = $"Có {NewQuarantinePersonCount} người cách ly mới trong hôm nay.";
            }
            else
            {
                SecondRecommendationVisibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region Third Recommendation

        private void DefineThirdRecommendation()
        {
            int FinishQuarantinePersonCount = CountFinishQuarantinePersonStatistic();
            if (FinishQuarantinePersonCount != 0)
            {
                ThirdRecommendationVisibility = Visibility.Visible;
                ThirdRecommendationTitle = "GỬI THÔNG BÁO HOÀN THÀNH CÁCH LY";
                ThirdRecommendationContent = $"Có {FinishQuarantinePersonCount} người dự kiến hoàn thành cách ly hôm nay.";
            }
            else
            {
                ThirdRecommendationVisibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region Fourth Recommendation

        private void DefineFourthRecommendation()
        {
            int FinishQuarantinePersonStillInRoomCount = CountFinishQuarantinePersonStillInRoom();
            if (FinishQuarantinePersonStillInRoomCount != 0)
            {
                FourthRecommendationVisibility = Visibility.Visible;
                FourthRecommendationTitle = "CHUYỂN NGƯỜI HOÀN THÀNH RA KHỎI PHÒNG";
                FourthRecommendationContent = $"Có {FinishQuarantinePersonStillInRoomCount} người đã hoàn thành cách ly.";
            }
            else
            {
                FourthRecommendationVisibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region General Statistic

        private int CountQuarantinePersonStatistic()
        {
            int count = 0;
            try
            {
                count = DataProvider.ins.db.QuarantinePersons.Count();
            }
            catch
            {
                //MessageBox.Show("Xảy ra lỗi khi thực thi đếm người cách ly");
            }
            return count;
        }
        

        // Hàm trả về mảng chứa id những người đã nhận thông báo hôm nay
        private string[] getListHasReceivedMessage()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "receivedList.txt");

            string[] lines;

            if (System.IO.File.Exists(filePath))
            {
                lines = System.IO.File.ReadAllLines(filePath);
                if (lines.Length == 2)
                {
                    if (lines[0] == DateTime.Now.Date.ToString())
                    {
                        string[] hasReceiveList = lines[1].Split(' ');
                        return hasReceiveList;
                    }
                }
            }
            return new string[] { };
        }

        private int CountNewQuarantinePersonStatistic()
        {
            int count = 0;
            DateTime date = DateTime.Now.Date;
            try
            {
                string[] hasReceivedMessageList = getListHasReceivedMessage();
                count = DataProvider.ins.db.QuarantinePersons.Where(person => person.arrivedDate == date && !hasReceivedMessageList.ToList().Contains(person.id.ToString())).Count();

                
            }
            catch
            {
                //MessageBox.Show("Xảy ra lỗi khi thực thi đếm người cách ly mới");
            }
            return count;
        }

        private int CountQuarantinePersonWithNoRoomStatistic()
        {
            int count = 0;
            try
            {
                ObservableCollection<Model.QuarantinePerson> QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
                count = QuarantinePersonList.Where(person => person.roomID == null && person.leaveDate > DateTime.Today).Count();
            }
            catch
            {
                //MessageBox.Show("Xảy ra lỗi khi thực thi đếm người cách ly chưa có phòng");
            }
            return count;
        }

        private int CountFinishQuarantinePersonStatistic()
        {
            int count = 0;
            DateTime date = DateTime.Now.Date;
            try
            {
                string[] hasReceivedMessageList = getListHasReceivedMessage();
                count = DataProvider.ins.db.QuarantinePersons.Where(person => person.leaveDate == date && !hasReceivedMessageList.ToList().Contains(person.id.ToString())).Count();

            }
            catch
            {
                //MessageBox.Show("Xảy ra lỗi khi thực thi đếm người hoàn thành cách ly");
            }
            return count;
        }

        private int CountFinishQuarantinePersonStillInRoom()
        {
            int count = 0;
            DateTime date = DateTime.Now.Date;
            try
            {
                ObservableCollection<Model.QuarantinePerson> QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons);
                count = QuarantinePersonList.Where(person => person.roomID != null && person.leaveDate <= date).Count();
            }
            catch
            {
                //MessageBox.Show("Xảy ra lỗi khi thực thi đếm người hoàn thành cách ly");
            }
            return count;
        }

        private int CountAvailableCapacityStatistic()
        {
            int count = 0;
            DateTime date = DateTime.Now.Date;
            try
            {
                ObservableCollection<Model.QuarantineRoom> QuarantineRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
                for (int i = 0; i < QuarantineRoomList.Count(); i++)
                {
                    count += QuarantineRoomList[i].capacity - QuarantineRoomList[i].QuarantinePersons.Count();
                }
            }
            catch
            {
                //MessageBox.Show("Xảy ra lỗi khi thực thi tính sức chứa khả dụng");
            }
            return count;
        }

        #endregion

        #region First Chart

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

        #region Second Chart

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
                int StaffCount = CountStaffWithDepartment(DepartmentList?.ElementAt(i).ToString());
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
                //  MessageBox.Show("Đã có lỗi xảy ra khi xử lý đếm số lượng  nhân viên theo phòng ban!");
            }
            return count;
        }

        #endregion

        #endregion

        #region Third Chart

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