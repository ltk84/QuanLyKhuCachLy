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
        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged();  }
        }

        public ICommand CancelCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public DeleteConfirmationViewModel()
        {
            Content = "Sau khi xóa sẽ không thể hoàn tác";
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
