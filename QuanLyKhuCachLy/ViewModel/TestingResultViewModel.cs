using QuanLyKhuCachLy.Model;
using QuanLyKhuCachLy.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class TestingResultViewModel : BaseViewModel
    {
        #region property
        private int _PersonID;
        public int PersonID
        {
            get => _PersonID;
            set
            {
                _PersonID = value;
                TestingResultList = new ObservableCollection<TestingResult>(DataProvider.ins.db.TestingResults.Where(x => x.quarantinePersonID == PersonID));
                OnPropertyChanged();

            }
        }

        private static TestingResultViewModel _ins;
        public static TestingResultViewModel ins
        {
            get
            {
                if (_ins == null) _ins = new TestingResultViewModel();
                return _ins;
            }
            set => _ins = value;
        }

        #endregion

        private TestingResult _SelectedItem;
        public TestingResult SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        private bool _SelectedResult;
        public bool SelectedResult
        {
            get => _SelectedResult;
            set
            {
                _SelectedResult = value;
                if (SelectedItem != null)
                    SelectedItem.isPositive = SelectedResult;
                OnPropertyChanged();
            }
        }



        #region list
        private ObservableCollection<TestingResult> _TestingResultList;
        public ObservableCollection<TestingResult> TestingResultList
        {
            get => _TestingResultList; set
            {
                _TestingResultList = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<string, bool> _ResultList;
        public Dictionary<string, bool> ResultList
        {
            get => _ResultList; set
            {
                _ResultList = value;
                OnPropertyChanged();
            }
        }

        //private ObservableCollection<string> _ResultList;
        //public ObservableCollection<string> ResultList
        //{
        //    get => _ResultList; set
        //    {
        //        _ResultList = value;
        //        OnPropertyChanged();
        //    }
        //}
        #endregion

        #region command
        public ICommand AddOnUICommand { get; set; }
        public ICommand EditOnUICommand { get; set; }
        public ICommand DeleteOnUICommand { get; set; }
        #endregion


        public TestingResultViewModel()
        {
            TestingResultList = new ObservableCollection<TestingResult>();

            //ResultList = new ObservableCollection<string>()
            //{
            //   "Dương tính", "Âm tính"
            //};

            ResultList = new Dictionary<string, bool>()
            {
                {"Âm tính", false },
                {"Dương tính", true },
            };

            AddOnUICommand = new RelayCommand<object>((p) =>
            {
                return true;
            }
           , (p) =>
           {
               AddTestingResultUI();
           });

            DeleteOnUICommand = new RelayCommand<object>((p) => {
                return true;
                //if (SelectedItem != null) return true; return false;
            }, (p) =>
            {
                DeleteTestingResultUI();
            });
        }

        #region method
        void AddTestingResultUI()
        {
            TestingResult testingResult = new TestingResult()
            {
                id = -1,
                dateTesting = DateTime.Today,
                isPositive = false,
            };

            TestingResultList.Add(testingResult);
        }

        void DeleteTestingResultUI()
        {
            TestingResultList.Remove(SelectedItem);
        }

        public void ApplyTestingResultToDb(int PersonID, string action)
        {
            if (action == "Add")
            {
                foreach (var tr in TestingResultList)
                {
                    tr.quarantinePersonID = PersonID;
                    DataProvider.ins.db.TestingResults.Add(tr);
                }

                DataProvider.ins.db.SaveChanges();
                ClearTestingResultList();
            }
            else
            {
                List<TestingResult> TRList = DataProvider.ins.db.TestingResults.Where(x => x.quarantinePersonID == PersonID).ToList();

                foreach (var trUI in TestingResultList)
                {
                    if (!TRList.Contains(trUI))
                    {
                        trUI.quarantinePersonID = PersonID;
                        DataProvider.ins.db.TestingResults.Add(trUI);
                        DataProvider.ins.db.SaveChanges();
                    }
                }

                foreach (var irInDB in TRList)
                {
                    if (!TestingResultList.Contains(irInDB))
                    {
                        DataProvider.ins.db.TestingResults.Remove(irInDB);
                    }
                }

                DataProvider.ins.db.SaveChanges();
                TestingResultList = new ObservableCollection<TestingResult>(DataProvider.ins.db.TestingResults.Where(x => x.quarantinePersonID == PersonID));
            }
        }

        public void RollbackTransaction(int PersonID)
        {
            DBUtilityTracker.Rollback();
            TestingResultList = new ObservableCollection<TestingResult>(DataProvider.ins.db.TestingResults.Where(x => x.quarantinePersonID == PersonID));
        }

        public void ClearTestingResultList() => TestingResultList.Clear();
        #endregion
    }

    public class ComboBoxTestingResult
    {
        public string label;
        public bool value;

        public ComboBoxTestingResult(string k, bool v)
        {
            label = k;
            value = v;
        }
    }
}
