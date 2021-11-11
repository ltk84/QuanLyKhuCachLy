using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class ReportViewModel : BaseViewModel
    {
        #region Property

        #region Report Data

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

        private ObservableCollection<string> _QuarantinePersonReportObjects;
        public ObservableCollection<string> QuarantinePersonReportObjects
        {
            get => _QuarantinePersonReportObjects; set
            {
                _QuarantinePersonReportObjects = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _QuarantineAreaReportObjects;
        public ObservableCollection<string> QuarantineAreaReportObjects
        {
            get => _QuarantineAreaReportObjects; set
            {
                _QuarantineAreaReportObjects = value; OnPropertyChanged();
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

        private string _SelectedQuarantineAreaReportObjects;
        public string SelectedQuarantineAreaReportObjects
        {
            get => _SelectedQuarantineAreaReportObjects; set
            {
                _SelectedQuarantineAreaReportObjects = value; OnPropertyChanged();
            }
        }

        #endregion

        #region UI
        private SeriesCollection _FirstSeriesCollection;
        public SeriesCollection FirstSeriesCollection
        {
            get => _FirstSeriesCollection; set
            {
                _FirstSeriesCollection = value; OnPropertyChanged();
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

        private ObservableCollection<string> _Labels;
        public ObservableCollection<string> Labels
        {
            get => _Labels; set
            {
                _Labels = value; OnPropertyChanged();
            }
        }

        private Func<double, string> _Formatter;
        public Func<double, string> Formatter
        {
            get => _Formatter; set
            {
                _Formatter = value; OnPropertyChanged();
            }
        }
        #endregion

        #endregion

        #region Command

        public ICommand SelectionChangedCommand { get; set; }

        #endregion

        public ReportViewModel() {
            Init();
        }

        #region Method

        #region Fundamental Function

        private void Init()
        {
            EndDate = DateTime.Now.Date;
            BeginDate = DateTime.Now.Date;

            QuarantinePersonReportObjects = new ObservableCollection<string>() { "Người cách ly", "Nhóm đối tượng" };
            QuarantineAreaReportObjects = new ObservableCollection<string>() { "Sức chứa", "Phòng", "Mức độ phòng", "Nhân viên" };

            Formatter = value => value.ToString() + "người";

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
                }
            });

            ToFirstTab();
        }

        private void LoadChart(int SelectedTab, DateTime BeginDate, DateTime EndDate, string SelectedReportObject)
        {
            if (SelectedTab == 0)
            {
                if (SelectedReportObject.CompareTo(QuarantinePersonReportObjects[0]) == 0)
                {
                    LoadQuarantinePersonChart(BeginDate, EndDate);
                }
                else if (SelectedReportObject.CompareTo(QuarantinePersonReportObjects[1]) == 0)
                {
                    LoadTargetGroupChart(BeginDate, EndDate);
                }
            }
            else if (SelectedTab == 1)
            {
                if (SelectedReportObject.CompareTo(QuarantineAreaReportObjects[0]) == 0)
                {
                    LoadCapacityChart(EndDate);
                }
                else if (SelectedReportObject.CompareTo(QuarantineAreaReportObjects[1]) == 0)
                {
                    LoadRoomChart(EndDate);
                }
                else if (SelectedReportObject.CompareTo(QuarantineAreaReportObjects[2]) == 0)
                {
                    LoadRoomSeverityChart(EndDate);
                }
                else if (SelectedReportObject.CompareTo(QuarantineAreaReportObjects[3]) == 0)
                {
                    LoadStafChart(EndDate);
                }
            }
        }

        #endregion

        #region Navigation

        private void ToFirstTab()
        {
            SelectedTab = 0;
            SelectedQuarantinePersonReportObjects = QuarantinePersonReportObjects[0];
            LoadChart(SelectedTab, BeginDate, EndDate, SelectedQuarantinePersonReportObjects);
        }

        private void ToSecondTab()
        {
            SelectedTab = 1;
            SelectedQuarantineAreaReportObjects = QuarantineAreaReportObjects[0];
            LoadChart(SelectedTab, BeginDate, EndDate, SelectedQuarantineAreaReportObjects);
        }

        #endregion

        #region FirstTab

        private void LoadQuarantinePersonChart(DateTime BeginDate, DateTime EndDate)
        {
            Labels = new ObservableCollection<string>();
            FirstSeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Số người đang cách ly",
                    Values = new ChartValues<int>(),
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Số người cách ly mới",
                    Values = new ChartValues<int>(),
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Số người hoàn thành",
                    Values = new ChartValues<int>(),
                    StackMode = StackMode.Values,
                    DataLabels = true
                }
            };

            for (DateTime date = BeginDate; date <= EndDate; date = date.AddDays(1))
            {
                Labels.Add(date.ToString("dd/MM/yyyy"));
                FirstSeriesCollection[0].Values.Add(CountQuarantinePerson(date));
                FirstSeriesCollection[1].Values.Add(CountQuarantinePerson(date));
                FirstSeriesCollection[2].Values.Add(CountQuarantinePerson(date));
            }
        }

        private void LoadTargetGroupChart(DateTime BeginDate, DateTime EndDate)
        {
            Labels = new ObservableCollection<string>();
            FirstSeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Số người đang cách ly",
                    Values = new ChartValues<int>(),
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Số người cách ly mới",
                    Values = new ChartValues<int>(),
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Số người hoàn thành",
                    Values = new ChartValues<int>(),
                    StackMode = StackMode.Values,
                    DataLabels = true
                }
            };

            for (DateTime date = BeginDate; date <= EndDate; date = date.AddDays(1))
            {
                Labels.Add(date.ToString("dd/MM/yyyy"));
                //FirstSeriesCollection[0].Values.Add(CountTargetGroup(date));
                //FirstSeriesCollection[1].Values.Add(CountTargetGroup(date));
                //FirstSeriesCollection[2].Values.Add(CountTargetGroup(date));
            }
        }

        private int CountQuarantinePerson(DateTime date)
        {
            return 10;
        }

        private int CountTargetGroup(DateTime date, Severity severity)
        {
            return 10;
        }

        #endregion

        #region SecondTab

        private void LoadCapacityChart(DateTime EndDate)
        {
            SecondSeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Chrome",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Mozilla",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Opera",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Explorer",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true
                }
            };
        }

        private void LoadRoomChart(DateTime EndDate)
        {

        }

        private void LoadRoomSeverityChart(DateTime EndDate)
        {

        }

        private void LoadStafChart(DateTime EndDate)
        {

        }

        #endregion

        #endregion

    }
}
