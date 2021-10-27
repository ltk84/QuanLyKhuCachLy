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
    public class QuarantineRoomViewModel : BaseViewModel
    {
        #region property
        private ObservableCollection<QuarantineRoom> _QARoom;
        public ObservableCollection<QuarantineRoom> QARoom
        {
            get => _QARoom; set
            {
                _QARoom = value;
                OnPropertyChanged();
            }
        }

        private QuarantineRoom _SelectedItem;
        public QuarantineRoom SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (_SelectedItem != null)
                {
                    QADisplayName = SelectedItem.displayName;
                    QACapacity = SelectedItem.capacity;
                    QALevel = SelectedItem.level;
                }
            }
        }

        private string _QADisplayName;
        public string QADisplayName
        {
            get => QADisplayName; set
            {
                QADisplayName = value;
                OnPropertyChanged();
            }
        }

        private int _QACapacity;
        public int QACapacity
        {
            get => _QACapacity; set
            {
                _QACapacity = value;
                OnPropertyChanged();
            }
        }

        private string _QALevel;
        public string QALevel
        {
            get => _QALevel; set
            {
                _QALevel = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion

        public QuarantineRoomViewModel()
        {
            QARoom = new ObservableCollection<QuarantineRoom>(DataProvider.ins.db.QuarantineRooms);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (!string.IsNullOrEmpty(QADisplayName) && !string.IsNullOrEmpty(QALevel)) return true;
                return false;
            }, (p) =>
            {

            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (!string.IsNullOrEmpty(QADisplayName) && !string.IsNullOrEmpty(QALevel)) return true;
                return false;
            }, (p) =>
            {

            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });
        }

        #region method
        #endregion
    }
}
