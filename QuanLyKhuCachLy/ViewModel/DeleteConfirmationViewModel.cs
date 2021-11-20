using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class DeleteConfirmationViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public DeleteConfirmationViewModel()
        {
            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.DialogResult = false;
                p.Close();
            });
            ConfirmCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.DialogResult = true;
                p.Close();
            });
        }
    }
}
