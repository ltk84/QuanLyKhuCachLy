using LiveCharts;
using LiveCharts.Wpf;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
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

        private ObservableCollection<string> _ReportObjects;
        public ObservableCollection<string> ReportObjects
        {
            get => _ReportObjects; set
            {
                _ReportObjects = value; OnPropertyChanged();
            }
        }

        private string _SelectedReportObject;
        public string SelectedReportObject
        {
            get => _SelectedReportObject; set
            {
                _SelectedReportObject = value; OnPropertyChanged();
            }
        }

        #endregion

        #region UI
        private SeriesCollection _SeriesCollection;
        public SeriesCollection SeriesCollection
        {
            get => _SeriesCollection; set
            {
                _SeriesCollection = value; OnPropertyChanged();
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

        public ICommand AddRoomManualCommand { get; set; }

        #endregion

        public ReportViewModel() {
            Init();
            LoadChart(BeginDate, EndDate, SelectedReportObject);
        }

        private void Init()
        {
            SelectedTab = 0;
            EndDate = DateTime.Now.Date;
            BeginDate = DateTime.Now.Date;

            ReportObjects = new ObservableCollection<string>() { "Người cách ly", "Nhóm đối tượng" };
            SelectedReportObject = ReportObjects[0];
            Formatter = value => value + "ng";
        }

        private void LoadChart(DateTime BeginDate, DateTime EndDate, string SelectedReportObject)
        {
            if (SelectedReportObject.CompareTo(ReportObjects[0]) == 0)
            {
                LoadQuarantinePersonChart(BeginDate, EndDate);
            } 
            else if (SelectedReportObject.CompareTo(ReportObjects[1]) == 0)
            {
                LoadTargetGroupChart(BeginDate, EndDate);
            }
        }

        #region FirstTab

        private void LoadQuarantinePersonChart(DateTime BeginDate, DateTime EndDate)
        {
            Labels = new ObservableCollection<string>();
            SeriesCollection = new SeriesCollection
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
                SeriesCollection[0].Values.Add(CountQuarantinePerson(date));
                SeriesCollection[1].Values.Add(CountQuarantinePerson(date));
                SeriesCollection[2].Values.Add(CountQuarantinePerson(date));
            }
        }

        private void LoadTargetGroupChart(DateTime BeginDate, DateTime EndDate)
        {
            Labels = new ObservableCollection<string>();
            SeriesCollection = new SeriesCollection
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
                //SeriesCollection[0].Values.Add(CountTargetGroup(date));
                //SeriesCollection[1].Values.Add(CountTargetGroup(date));
                //SeriesCollection[2].Values.Add(CountTargetGroup(date));
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
    }
}
