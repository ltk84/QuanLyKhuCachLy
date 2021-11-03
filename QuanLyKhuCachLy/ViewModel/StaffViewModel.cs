using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKhuCachLy.ViewModel
{
    public class StaffViewModel : BaseViewModel
    {
        private Visibility _Tab1;
        public Visibility Tab1
        {
            get => _Tab1; set
            {
                _Tab1 = value; OnPropertyChanged();
            }
        }

        private Visibility _Tab2;
        public Visibility Tab2
        {
            get => _Tab2; set
            {
                _Tab2 = value; OnPropertyChanged();
            }
        }
        public StaffViewModel() {
            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
        
        }
    }
}
