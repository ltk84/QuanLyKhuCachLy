using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class CannotSendPeopleViewModel : BaseViewModel
    {

        private List<Model.QuarantinePerson> _peopleList;
        public List<Model.QuarantinePerson> PeopleList
        {
            get { return _peopleList; }
            set { _peopleList = value; OnPropertyChanged(); }
        }

        public ICommand ToExportExcel { get; set; }

        public ICommand CancelCommand { get; set; }

        public CannotSendPeopleViewModel()
        {
            PeopleList = new List<Model.QuarantinePerson>();

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });


            ToExportExcel = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {

            });

        }
    }
}
