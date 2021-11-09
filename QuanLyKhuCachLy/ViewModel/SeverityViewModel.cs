﻿using QuanLyKhuCachLy.Model;
using System.Collections.ObjectModel;
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
                MessageBox.Show(level);
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
        }

        private void RemoveSeverity(Severity severity)
        {
            ListSeverity.Remove(severity);
        }

        private void AddSeverity()
        {
            Severity newSeverity = new Severity()
            {
                level = "A",
                description = "B"
            };
            ListSeverity.Add(newSeverity);


        }

    }
}
