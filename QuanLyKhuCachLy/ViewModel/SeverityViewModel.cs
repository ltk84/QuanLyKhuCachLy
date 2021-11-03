using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public SeverityViewModel()
        {
            Init();
        }

        private void Init()
        {
            ListSeverity = new ObservableCollection<Severity>();
            Severity firstSeverity = new Severity () {
                level = "1",
                description = "F1, F2"
            };
            Severity secondSeverity = new Severity()
            {
                level = "2",
                description = "F0"
            };
            ListSeverity.Add(firstSeverity);
            ListSeverity.Add(secondSeverity);

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
                level = "(Mức độ)",
                description = "(Mô tả)"
            };
            ListSeverity.Add(newSeverity);
        }

    }
}
