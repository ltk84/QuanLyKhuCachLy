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

    public partial class Severity : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Severity()
        {
            this.QuarantinePersons = new HashSet<QuarantinePerson>();
            this.QuarantineRooms = new HashSet<QuarantineRoom>();
        }

        private string _level;
        public string level
        {
            get => _level; set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string description
        {
            get => _description; set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuarantinePerson> QuarantinePersons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuarantineRoom> QuarantineRooms { get; set; }
    }
}