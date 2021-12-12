using QuanLyKhuCachLy.Model;
using QuanLyKhuCachLy.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

        private Nullable<int> _IRQuarantinePersonID;
        public Nullable<int> IRQuarantinePersonID
        {
            get => _IRQuarantinePersonID; set
            {
                _IRQuarantinePersonID = value;
                InjectionRecordList = new ObservableCollection<InjectionRecord>(DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == _IRQuarantinePersonID));
                OnPropertyChanged();
            }
        }

        private InjectionRecord _SelectedItem;
        public InjectionRecord SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
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

            //EditOnUICommand = new RelayCommand<object>((p) =>
            //{
            //    if (SelectedItem != null)
            //        return true;
            //    return false;
            //}, (p) =>
            //{
            //    EditInjectionRecordUI();
            //});

            DeleteOnUICommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteInjectionRecordUI();
            });


        }

        #region method

        void AddInjectionRecordUI()
        {
            InjectionRecord injectionRecord = new InjectionRecord()
            {
                id = -1,
                dateInjection = DateTime.Today,
                vaccineName = "(ALO)",
            };

            InjectionRecordList.Add(injectionRecord);
        }


        void DeleteInjectionRecordUI()
        {
            var injectionRecord = InjectionRecordList.Where(x => x == SelectedItem).FirstOrDefault();
            if (injectionRecord == null) return;
            InjectionRecordList.Remove(injectionRecord);
        }

        public void ApplyInjectionRecordToDB(int PersonID, string action)
        {
            if (action == "Add")
            {
                foreach (var ir in InjectionRecordList)
                {
                    ir.quarantinePersonID = PersonID;
                    DataProvider.ins.db.InjectionRecords.Add(ir);
                }

                DataProvider.ins.db.SaveChanges();
                ClearInjectionRecordList();
            }
            else
            {
                List<InjectionRecord> IRList = DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == PersonID).ToList();

                foreach (var irUI in InjectionRecordList)
                {
                    if (!IRList.Contains(irUI))
                    {
                        irUI.quarantinePersonID = PersonID;
                        DataProvider.ins.db.InjectionRecords.Add(irUI);
                        DataProvider.ins.db.SaveChanges();
                    }
                }

                foreach (var irInDB in IRList)
                {
                    if (!InjectionRecordList.Contains(irInDB))
                    {
                        DataProvider.ins.db.InjectionRecords.Remove(irInDB);
                    }
                }

                DataProvider.ins.db.SaveChanges();
                InjectionRecordList = new ObservableCollection<InjectionRecord>(DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == PersonID));
            }
        }

        public void RollbackTransaction(int PersonID)
        {
            //DataProvider.ins.db.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
            DBUtilityTracker.Rollback();
            InjectionRecordList = new ObservableCollection<InjectionRecord>(DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == PersonID));
        }



        public void ClearInjectionRecordList() => InjectionRecordList.Clear();

        //void AddInjectionRecord()
        //{
        //    using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            InjectionRecord injectionRecord = new InjectionRecord()
        //            {
        //                dateInjection = DateTime.Today,
        //                vaccineName = "(ALO)",
        //                quarantinePersonID = IRQuarantinePersonID,
        //            };

        //            DataProvider.ins.db.InjectionRecords.Add(injectionRecord);
        //            DataProvider.ins.db.SaveChanges();

        //            InjectionRecordList.Add(injectionRecord);

        //            transaction.Commit();
        //        }
        //        catch (DbUpdateException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db update";

        //            MessageBox.Show(error);
        //        }
        //        catch (DbEntityValidationException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi validation";

        //            MessageBox.Show(error);
        //        }
        //        catch (NotSupportedException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db đéo support";

        //            MessageBox.Show(error);
        //        }
        //        catch (ObjectDisposedException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db object disposed";

        //            MessageBox.Show(error);
        //        }
        //        catch (InvalidOperationException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi invalid operation";

        //            MessageBox.Show(error);
        //        }
        //    }
        //}

        //void EditInjectionRecord()
        //{
        //    using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            InjectionRecord PersonInjectionRecord = DataProvider.ins.db.InjectionRecords.Where(x => x.id == SelectedItem.id).FirstOrDefault();
        //            if (PersonInjectionRecord == null) return;


        //            InjectionRecordList[InjectionRecordList.IndexOf(PersonInjectionRecord)].dateInjection = SelectedItem.dateInjection;
        //            InjectionRecordList[InjectionRecordList.IndexOf(PersonInjectionRecord)].vaccineName = SelectedItem.vaccineName;

        //            PersonInjectionRecord.dateInjection = SelectedItem.dateInjection;
        //            PersonInjectionRecord.vaccineName = SelectedItem.vaccineName;

        //            DataProvider.ins.db.SaveChanges();

        //            SelectedItem = PersonInjectionRecord;

        //            transaction.Commit();
        //        }
        //        catch (DbUpdateException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db update";

        //            MessageBox.Show(error);
        //        }
        //        catch (DbEntityValidationException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi validation";

        //            MessageBox.Show(error);
        //        }
        //        catch (NotSupportedException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db đéo support";

        //            MessageBox.Show(error);
        //        }
        //        catch (ObjectDisposedException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db object disposed";

        //            MessageBox.Show(error);
        //        }
        //        catch (InvalidOperationException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi invalid operation";

        //            MessageBox.Show(error);
        //        }
        //    }
        //}

        //void DeleteInjectionRecord()
        //{
        //    using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            InjectionRecord PersonInjectionRecord = DataProvider.ins.db.InjectionRecords.Where(x => x == SelectedItem).FirstOrDefault();
        //            if (PersonInjectionRecord == null) return;

        //            DataProvider.ins.db.InjectionRecords.Remove(PersonInjectionRecord);
        //            DataProvider.ins.db.SaveChanges();

        //            InjectionRecordList.Remove(PersonInjectionRecord);

        //            transaction.Commit();
        //        }
        //        catch (DbUpdateException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db update";

        //            MessageBox.Show(error);
        //        }
        //        catch (DbEntityValidationException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi validation";

        //            MessageBox.Show(error);
        //        }
        //        catch (NotSupportedException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db đéo support";

        //            MessageBox.Show(error);
        //        }
        //        catch (ObjectDisposedException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi db object disposed";

        //            MessageBox.Show(error);
        //        }
        //        catch (InvalidOperationException e)
        //        {
        //            transaction.Rollback();
        //            string error = "Lỗi invalid operation";

        //            MessageBox.Show(error);
        //        }
        //    }
        //}
        #endregion
    }
}
