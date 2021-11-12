using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class InjectionRecordViewModel : BaseViewModel
    {
        #region property

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
                if (_IRDateInjection != null)
                    InjectionRecordList = new ObservableCollection<InjectionRecord>(DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == _IRQuarantinePersonID));
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
                    MessageBox.Show(SelectedItem.vaccineName);
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
        public ICommand AddOnUICommand { get; set; }
        public ICommand EditOnUICommand { get; set; }
        public ICommand DeleteOnUICommand { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        #endregion

        private static InjectionRecordViewModel _ins;
        public static InjectionRecordViewModel ins
        {
            get
            {
                if (_ins == null) _ins = new InjectionRecordViewModel();
                return _ins;
            }
            set => _ins = value;
        }


        #endregion
        public InjectionRecordViewModel()
        {

            InjectionRecordList = new ObservableCollection<InjectionRecord>();
            AddOnUICommand = new RelayCommand<object>((p) =>
            {
                return true;
            }
            , (p) =>
            {
                AddInjectionRecordUI();
            });

            EditOnUICommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                return false;
            }, (p) =>
            {
                EditInjectionRecordUI();
            });

            DeleteOnUICommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteInjectionRecordUI();
            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteInjectionRecord();
            });

            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 AddInjectionRecord();
             });

            EditCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditInjectionRecord();
            });

        }

        #region method
        void SetSelectedItemToProperty()
        {
            IRID = SelectedItem.id;
            IRDateInjection = SelectedItem.dateInjection;
            IRVaccineName = SelectedItem.vaccineName;
        }

        void AddInjectionRecordUI()
        {
            InjectionRecord injectionRecord = new InjectionRecord()
            {
                dateInjection = DateTime.Today,
                vaccineName = "(ALO)",
            };

            InjectionRecordList.Add(injectionRecord);
        }

        void EditInjectionRecordUI()
        {
            var injectionRecord = InjectionRecordList.Where(x => x.id == SelectedItem.id).FirstOrDefault();

            InjectionRecordList[InjectionRecordList.IndexOf(injectionRecord)].dateInjection = SelectedItem.dateInjection;
            InjectionRecordList[InjectionRecordList.IndexOf(injectionRecord)].vaccineName = SelectedItem.vaccineName;
        }

        void DeleteInjectionRecordUI()
        {
            var injectionRecord = InjectionRecordList.Where(x => x == SelectedItem).FirstOrDefault();
            InjectionRecordList.Remove(injectionRecord);
        }

        public void ApplyInjectionRecordToDB(int PersonID, string action)
        {
            if (action == "add")
            {
                foreach (var ir in InjectionRecordList)
                {
                    ir.quarantinePersonID = PersonID;
                    DataProvider.ins.db.InjectionRecords.Add(ir);
                }

                DataProvider.ins.db.SaveChanges();
                InjectionRecordList.Clear();
            }
            else if (action == "edit")
            {
                List<InjectionRecord> IRList = DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == PersonID).ToList();

                foreach (var irUI in InjectionRecordList)
                {
                    if (IRList.Contains(irUI))
                    {
                        var irDB = DataProvider.ins.db.InjectionRecords.Where(x => x.id == irUI.id).FirstOrDefault();
                        if (irDB.dateInjection.CompareTo(irUI.dateInjection) != 0) irDB.dateInjection = irUI.dateInjection;
                        if (irDB.vaccineName != irUI.vaccineName) irDB.vaccineName = irUI.vaccineName;
                    }
                    else
                    {
                        irUI.quarantinePersonID = PersonID;
                        DataProvider.ins.db.InjectionRecords.Add(irUI);
                    }
                }

                foreach (var irInDB in IRList)
                {
                    if (!InjectionRecordList.Contains(irInDB))
                    {
                        //var irUI = InjectionRecordList.Where(x => x.id == irInDB.id).FirstOrDefault();
                        //if (irInDB.dateInjection.CompareTo(irUI.dateInjection) != 0) irInDB.dateInjection = irUI.dateInjection;
                        //if (irInDB.vaccineName != irUI.vaccineName) irInDB.vaccineName = irUI.vaccineName;

                        DataProvider.ins.db.InjectionRecords.Remove(irInDB);
                    }
                }

                DataProvider.ins.db.SaveChanges();
                InjectionRecordList = new ObservableCollection<InjectionRecord>(DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == PersonID));

            }
            else
            {

            }

        }


        void AddInjectionRecord()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    InjectionRecord injectionRecord = new InjectionRecord()
                    {
                        dateInjection = DateTime.Today,
                        vaccineName = "(ALO)",
                        quarantinePersonID = IRQuarantinePersonID,
                    };

                    DataProvider.ins.db.InjectionRecords.Add(injectionRecord);
                    DataProvider.ins.db.SaveChanges();

                    InjectionRecordList.Add(injectionRecord);

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

        void EditInjectionRecord()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    InjectionRecord PersonInjectionRecord = DataProvider.ins.db.InjectionRecords.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (PersonInjectionRecord == null) return;


                    InjectionRecordList[InjectionRecordList.IndexOf(PersonInjectionRecord)].dateInjection = SelectedItem.dateInjection;
                    InjectionRecordList[InjectionRecordList.IndexOf(PersonInjectionRecord)].vaccineName = SelectedItem.vaccineName;

                    PersonInjectionRecord.dateInjection = SelectedItem.dateInjection;
                    PersonInjectionRecord.vaccineName = SelectedItem.vaccineName;

                    DataProvider.ins.db.SaveChanges();

                    SelectedItem = PersonInjectionRecord;

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

        void DeleteInjectionRecord()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    InjectionRecord PersonInjectionRecord = DataProvider.ins.db.InjectionRecords.Where(x => x == SelectedItem).FirstOrDefault();
                    if (PersonInjectionRecord == null) return;

                    DataProvider.ins.db.InjectionRecords.Remove(PersonInjectionRecord);
                    DataProvider.ins.db.SaveChanges();

                    InjectionRecordList.Remove(PersonInjectionRecord);

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
        #endregion
    }
}
