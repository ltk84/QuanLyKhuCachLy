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

    public partial class Staff : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            this.QuarantineAreas = new HashSet<QuarantineArea>();
        }

        public int id { get; set; }

        private string _name;
        public string name
        {
            get => _name; set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private System.DateTime _dateOfBirth;
        public System.DateTime dateOfBirth
        {
            get => _dateOfBirth; set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        private string _sex;
        public string sex
        {
            get => _sex; set
            {
                _sex = value;
                OnPropertyChanged();
            }
        }

        private string _citizenID;
        public string citizenID
        {
            get => _citizenID; set
            {
                _citizenID = value;
                OnPropertyChanged();
            }
        }

        private string _nationality;
        public string nationality
        {
            get => _nationality; set
            {
                _nationality = value;
                OnPropertyChanged();
            }
        }

        private string _healthInsuranceID;
        public string healthInsuranceID
        {
            get => _healthInsuranceID; set
            {
                _healthInsuranceID = value;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber;
        public string phoneNumber
        {
            get => _phoneNumber; set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private int _addressID;
        public int addressID
        {
            get => _addressID; set
            {
                _addressID = value;
                OnPropertyChanged();
            }
        }

        private string _jobTitle;
        public string jobTitle
        {
            get => _jobTitle; set
            {
                _jobTitle = value;
                OnPropertyChanged();
            }
        }

        private string _department;
        public string department
        {
            get => _department; set
            {
                _department = value;
                OnPropertyChanged();
            }
        }


        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuarantineArea> QuarantineAreas { get; set; }

        public bool CheckValidateProperty()
        {
            if (string.IsNullOrWhiteSpace(sex) || string.IsNullOrWhiteSpace(citizenID) || string.IsNullOrWhiteSpace(nationality) || string.IsNullOrWhiteSpace(healthInsuranceID)
                || string.IsNullOrWhiteSpace(phoneNumber) || addressID < 0 || string.IsNullOrWhiteSpace(jobTitle) || string.IsNullOrWhiteSpace(department))
                return false;
            //if (sex.Length > LIMIT || citizenID.Length > LIMIT || nationality.Length > LIMIT || healthInsuranceID.Length > LIMIT || phoneNumber.Length > LIMIT
            //     || jobTitle.Length > LIMIT || department.Length > LIMIT)
            //    return false;
            return true;
        }
    }
}