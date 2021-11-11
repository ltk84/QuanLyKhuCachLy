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

    public partial class HealthInformation : BaseViewModel
    {
        public int id { get; set; }

        private bool _isFever;
        public bool isFever
        {
            get => _isFever; set
            {
                _isFever = value;
                OnPropertyChanged();
            }
        }

        private bool _isCough;
        public bool isCough
        {
            get => _isCough; set
            {
                _isCough = value;
                OnPropertyChanged();
            }
        }

        private bool _isSoreThroat;
        public bool isSoreThroat
        {
            get => _isSoreThroat; set
            {
                _isSoreThroat = value;
                OnPropertyChanged();
            }
        }

        private bool _isLossOfTatse;
        public bool isLossOfTatse
        {
            get => _isLossOfTatse; set
            {
                _isLossOfTatse = value;
                OnPropertyChanged();
            }
        }

        private bool _isTired;
        public bool isTired
        {
            get => _isTired; set
            {
                _isTired = value;
                OnPropertyChanged();
            }
        }

        private bool _isShortnessOfBreath;
        public bool isShortnessOfBreath
        {
            get => _isShortnessOfBreath; set
            {
                _isShortnessOfBreath = value;
                OnPropertyChanged();
            }
        }

        private bool _isOtherSymptoms;
        public bool isOtherSymptoms
        {
            get => _isOtherSymptoms; set
            {
                _isOtherSymptoms = value;
                OnPropertyChanged();
            }
        }

        private bool _isDisease;
        public bool isDisease
        {
            get => _isDisease; set
            {
                _isDisease = value;
                OnPropertyChanged();
            }
        }
        public int _quarantinePersonID { get; set; }
        public int quarantinePersonID
        {
            get => _quarantinePersonID; set
            {
                _quarantinePersonID = value;
                OnPropertyChanged();
            }
        }

        public virtual QuarantinePerson QuarantinePerson { get; set; }

    }
}


