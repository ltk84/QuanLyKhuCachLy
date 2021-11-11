﻿using LiveCharts;
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
            TestingReportObjects = new ObservableCollection<string>() { "Xét nghiệm" };

            QuarantinePersonFormatter = value => value.ToString() + " người";
            TestingFormatter = value => value.ToString() + " mẫu";

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
            SelectedQuarantinePersonReportObjects = QuarantinePersonReportObjects[0];
            LoadChart(SelectedTab, BeginDate, EndDate, SelectedQuarantinePersonReportObjects);
        }

        private void ToSecondTab()
        {
            SelectedTab = 1;
            SelectedQuarantineAreaReportObjects = QuarantineAreaReportObjects[0];
            LoadChart(SelectedTab, BeginDate, EndDate, SelectedQuarantineAreaReportObjects);
        }

        private void ToThirdTab()
        {
            SelectedTab = 2;
            SelectedTestingReportObjects = TestingReportObjects[0];
            LoadChart(SelectedTab, BeginDate, EndDate, SelectedTestingReportObjects);
        }

        #endregion

        #region FirstTab

        private void LoadQuarantinePersonChart(DateTime BeginDate, DateTime EndDate)
        {
            FirstLabels = new ObservableCollection<string>();
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
                FirstLabels.Add(date.ToString("dd/MM/yyyy"));
                FirstSeriesCollection[0].Values.Add(CountQuarantinePerson(date));
                FirstSeriesCollection[1].Values.Add(CountQuarantinePerson(date));
                FirstSeriesCollection[2].Values.Add(CountQuarantinePerson(date));
            }
        }

        private void LoadTargetGroupChart(DateTime BeginDate, DateTime EndDate)
        {
            FirstLabels = new ObservableCollection<string>();
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
                FirstLabels.Add(date.ToString("dd/MM/yyyy"));
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
            return 5;
        }
        private int CountNegativeTestingResult(DateTime date)
        {
            return 5;
        }

        #endregion

        #endregion

    }
}
