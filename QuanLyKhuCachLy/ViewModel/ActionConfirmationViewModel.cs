﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class ActionConfirmationViewModel : BaseViewModel
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
        public ICommand NotActionConfirmationCommand { get; set; }
        public ICommand DoActionConfirmationCommand { get; set; }
        public ActionConfirmationViewModel()
        {
            Title = "Thay đổi mức độ nhóm đối tượng";
            Content = "Bạn có muốn thay đổi nhóm đối tượng của những người trong phòng theo giá trị vừa thay đổi của phòng?";
            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.DialogResult = null;
                p.Close();
            });
            NotActionConfirmationCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.DialogResult = false;
                p.Close();
            });
            DoActionConfirmationCommand = new RelayCommand<Window>((p) =>
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
