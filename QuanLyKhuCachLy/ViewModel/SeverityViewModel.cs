using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyKhuCachLy.ViewModel
{
    public class SeverityViewModel : BaseViewModel
    {
        private ObservableCollection<Severity> _listSeverity;
        private Severity _selectedItem;

        public Severity SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
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

            RemoveSeverityCommand = new RelayCommand<object>((p) => { if (ListSeverity.Count > 1) return true; return false; }, (o) =>
             {
                 RemoveSeverity();
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


        private void RemoveSeverity()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var severity = DataProvider.ins.db.Severities.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (severity == null) return;

                    DataProvider.ins.db.Severities.Remove(severity);
                    DataProvider.ins.db.SaveChanges();

                    ListSeverity.Remove(severity);

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi db update";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi validation";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi db đéo support";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi db object disposed";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi invalid operation";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }

            }
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
                    //string error = "Lỗi db update";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi validation";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi db đéo support";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi db object disposed";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi invalid operation";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
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
                    //string error = "Lỗi db update";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi validation";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi db đéo support";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi db object disposed";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    //string error = "Lỗi invalid operation";

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
            }
        }

    }
}
