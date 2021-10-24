using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhuCachLy.ViewModel
{
    class InitialSettingViewModel : BaseViewModel
    {
        public RelayCommand<object> PreviousPageCommand { get; set; }
        public RelayCommand<object> NextPageCommand { get; set; }
        public InitialSettingFirstPageViewModel InitialSettingFirstPageVM { get; set; }
        public InitialSettingSecondPageViewModel InitialSettingSecondPageVM { get; set; }
        public InitialSettingThirdPageViewModel InitialSettingThirdPageVM { get; set; }

        private object _currentView;
        private int _index;
        private string _indexCount;
        private int _count;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string IndexCount
        {
            get { return _indexCount; }
            set
            {
                _indexCount = value;
                OnPropertyChanged();
            }
        }

        public InitialSettingViewModel()
        {
            InitialSettingFirstPageVM = new InitialSettingFirstPageViewModel();
            InitialSettingSecondPageVM = new InitialSettingSecondPageViewModel();
            InitialSettingThirdPageVM = new InitialSettingThirdPageViewModel();
            _count = 3;

            CurrentView = InitialSettingFirstPageVM;
            _index = 0;
            _indexCount = $"{_index + 1}/{_count}";

            PreviousPageCommand = new RelayCommand<object>((p) => { return (_index != 0); }, o =>
            {
                _index--;
                ChangeView();
            });

            NextPageCommand = new RelayCommand<object>((p) => { return (_index != _count - 1); }, o =>
            {
                _index++;
                ChangeView();
            });
        }

        private void ChangeView()
        {
            _indexCount = $"{_index + 1}/{_count}";
            switch (_index)
            {
                case 0: 
                    CurrentView = InitialSettingFirstPageVM;
                    break;
                case 1: 
                    CurrentView = InitialSettingSecondPageVM;
                    break;
                case 2:
                    CurrentView = InitialSettingThirdPageVM;
                    break;
                default:
                    break;
            }
        }

    }
}
