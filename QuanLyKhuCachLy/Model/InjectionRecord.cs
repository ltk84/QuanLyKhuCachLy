//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKhuCachLy.Model
{
    using QuanLyKhuCachLy.ViewModel;
    using System;
    using System.Collections.Generic;

    public partial class InjectionRecord : BaseViewModel
    {
        public int id { get; set; }

        private System.DateTime _dateInjection;
        public System.DateTime dateInjection
        {
            get => _dateInjection; set
            {
                _dateInjection = value;
                OnPropertyChanged();
            }
        }

        private string _vaccineName;
        public string vaccineName
        {
            get => _vaccineName; set
            {
                _vaccineName = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _quarantinePersonID;
        public Nullable<int> quarantinePersonID
        {
            get => _quarantinePersonID; set
            {
                _quarantinePersonID = value;
                OnPropertyChanged();
            }
        }

        public virtual QuarantinePerson QuarantinePerson { get; set; }

        public bool CheckValidateProperty()
        {
            if (string.IsNullOrWhiteSpace(vaccineName))
                return false;

            return true;
        }
    }
}
