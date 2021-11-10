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

    public partial class QuarantinePerson : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuarantinePerson()
        {
            this.DestinationHistories = new HashSet<DestinationHistory>();
            this.InjectionRecords = new HashSet<InjectionRecord>();
            this.TestingResults = new HashSet<TestingResult>();
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

        private int _levelID;
        public int levelID
        {
            get => _levelID; set
            {
                _levelID = value;
                OnPropertyChanged();
            }
        }

        private System.DateTime _arrivedDate;
        public System.DateTime arrivedDate
        {
            get => _arrivedDate; set
            {
                _arrivedDate = value;
                OnPropertyChanged();
            }
        }
        private System.DateTime _leaveDate;
        public System.DateTime leaveDate
        {
            get => _leaveDate; set
            {
                _leaveDate = value;
                OnPropertyChanged();
            }
        }

        private int _quarantineDays;
        public int quarantineDays
        {
            get => _quarantineDays; set
            {
                _quarantineDays = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _addressID;
        public Nullable<int> addressID
        {
            get => _addressID; set
            {
                _addressID = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _healthInformationID;
        public Nullable<int> healthInformationID
        {
            get => _healthInformationID; set
            {
                _healthInformationID = value;
                OnPropertyChanged();
            }
        }

        private Nullable<int> _roomID;
        public Nullable<int> roomID
        {
            get => _roomID; set
            {
                _roomID = value;
                OnPropertyChanged();
            }
        }

        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DestinationHistory> DestinationHistories { get; set; }
        public virtual HealthInformation HealthInformation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InjectionRecord> InjectionRecords { get; set; }
        public virtual Severity Severity { get; set; }
        public virtual QuarantineRoom QuarantineRoom { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestingResult> TestingResults { get; set; }
        public bool CheckValidateProperty()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(sex) || string.IsNullOrWhiteSpace(citizenID)
                || string.IsNullOrWhiteSpace(nationality) || string.IsNullOrWhiteSpace(healthInsuranceID) || string.IsNullOrWhiteSpace(phoneNumber)
                || arrivedDate < leaveDate || dateOfBirth <= arrivedDate || quarantineDays < 0)
                return false;

            return true;
        }
    }
}