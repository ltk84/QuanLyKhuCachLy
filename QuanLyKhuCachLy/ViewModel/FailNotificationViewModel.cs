using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class FailNotificationViewModel : BaseViewModel
    {
        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        public ICommand CancelCommand { get; set; }
        public FailNotificationViewModel()
        {
            Title = "Thất bại";
            Content = "Đã xảy ra lỗi khiến cho việc thực thi thất bại";
            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.DialogResult = false;
                p.Close();
            });
        }
    }
}
