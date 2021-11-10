using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class SeverityViewModel : BaseViewModel
    {


        private string _level;
        public string level
        {
            get => _level; set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string description
        {
            get => _description; set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Severity> _listSeverity;
        private Severity _selectedItem;

        public Severity SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (_selectedItem != null)
                {
                    level = SelectedItem.level;
                    description = SelectedItem.description;
                }
            }
        }

        public ObservableCollection<Severity> ListSeverity
        {
            get { return _listSeverity; }
            set { _listSeverity = value; OnPropertyChanged(); }
        }
        public ICommand RemoveSeverityCommand { get; set; }
        public ICommand AddSeverityCommand { get; set; }
        public ICommand EditSeverityCommand { get; set; }

        public SeverityViewModel()
        {
            Init();
        }

        private void Init()
        {
            ListSeverity = new ObservableCollection<Severity>(DataProvider.ins.db.Severities);

            RemoveSeverityCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                RemoveSeverity(SelectedItem);
            });

            AddSeverityCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                AddSeverity();
            });

            EditSeverityCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                EditSeverity();
            });
        }

        // Chua lam
        private void RemoveSeverity(Severity severity)
        {
            ListSeverity.Remove(severity);
        }

        private void EditSeverity()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var severity = DataProvider.ins.db.Severities.Where(x => x.id == SelectedItem.id).FirstOrDefault();

                    ListSeverity[ListSeverity.IndexOf(severity)].level = SelectedItem.level;
                    ListSeverity[ListSeverity.IndexOf(severity)].description = SelectedItem.description;

                    severity.level = SelectedItem.level;
                    severity.description = SelectedItem.description;

                    DataProvider.ins.db.SaveChanges();

                    SelectedItem = severity;

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

        private void AddSeverity()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    Severity newSeverity = new Severity()
                    {
                        level = "(Level)",
                        description = "(Description)"
                    };
                    DataProvider.ins.db.Severities.Add(newSeverity);
                    DataProvider.ins.db.SaveChanges();

                    ListSeverity.Add(newSeverity);

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

    }
}
