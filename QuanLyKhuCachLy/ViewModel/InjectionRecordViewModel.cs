using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class InjectionRecordViewModel : BaseViewModel
    {
        #region property
        private int _PersonID;

        #region injection record
        private int _IRID;
        public int IRID
        {
            get => _IRID; set
            {
                _IRID = value;
                OnPropertyChanged();
            }
        }

        private System.DateTime _IRDateInjection;
        public System.DateTime IRDateInjection
        {
            get => _IRDateInjection; set
            {
                _IRDateInjection = value;
                OnPropertyChanged();
            }
        }

        private string _IRVaccineName;
        public string IRVaccineName
        {
            get => _IRVaccineName; set
            {
                _IRVaccineName = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _IRQuarantinePersonID;
        public Nullable<int> IRQuarantinePersonID
        {
            get => _IRQuarantinePersonID; set
            {
                _IRQuarantinePersonID = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private InjectionRecord _SelectedItem;
        public InjectionRecord SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (_SelectedItem != null)
                {
                    SetSelectedItemToProperty();
                }
            }
        }

        #region list
        private ObservableCollection<InjectionRecord> _InjectionRecordList;
        public ObservableCollection<InjectionRecord> InjectionRecordList
        {
            get => _InjectionRecordList; set
            {
                _InjectionRecordList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region command
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand ToAddCommand { get; set; }
        public ICommand ToEditCommand { get; set; }
        public ICommand ToDeleteCommand { get; set; }
        #endregion

        #endregion
        public InjectionRecordViewModel() { }

        public InjectionRecordViewModel(int currentPersonID)
        {
            _PersonID = currentPersonID;
            InjectionRecordList = new ObservableCollection<InjectionRecord>(DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == _PersonID));

            AddCommand = new RelayCommand<object>((p) =>
            {
                InjectionRecord PersonInjectionRecord = new InjectionRecord()
                {
                    dateInjection = IRDateInjection,
                    vaccineName = IRVaccineName
                };

                if (PersonInjectionRecord.CheckValidateProperty())
                    return true;
                return false;
            }
            , (p) =>
            {
                AddInjectionRecord();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                InjectionRecord PersonInjectionRecord = new InjectionRecord()
                {
                    dateInjection = IRDateInjection,
                    vaccineName = IRVaccineName
                };

                if (PersonInjectionRecord.CheckValidateProperty())
                    return true;
                return false;
            }, (p) =>
            {
                EditInjectionRecord();
            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteInjectionRecord();
            });

            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });

            ToAddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });

            ToEditCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });

            ToDeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });
        }

        #region method
        void SetSelectedItemToProperty()
        {
            IRID = SelectedItem.id;
            IRDateInjection = SelectedItem.dateInjection;
            IRVaccineName = SelectedItem.vaccineName;
        }

        void AddInjectionRecord()
        {
            // Tạo thông tin tiêm chủng (optional)
            InjectionRecord PersonInjectionRecord = new InjectionRecord()
            {
                dateInjection = IRDateInjection,
                vaccineName = IRVaccineName
            };

            DataProvider.ins.db.InjectionRecords.Add(PersonInjectionRecord);
            DataProvider.ins.db.SaveChanges();
        }

        void EditInjectionRecord()
        {
            InjectionRecord PersonInjectionRecord = DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == _PersonID).FirstOrDefault();

            PersonInjectionRecord.dateInjection = IRDateInjection;
            PersonInjectionRecord.vaccineName = IRVaccineName;

            DataProvider.ins.db.SaveChanges();
        }

        void DeleteInjectionRecord()
        {

        }
        #endregion
    }
}
