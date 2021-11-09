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
    using System;
    using System.Collections.Generic;

    public partial class QuarantineArea
    {
        public int id { get; set; }
        public string name { get; set; }
        public int testCycle { get; set; }
        public int requiredDayToFinish { get; set; }
        public int addressID { get; set; }
        public int managerID { get; set; }

        public virtual Address Address { get; set; }
        public virtual Staff Staff { get; set; }

        public bool CheckValidateProperty()
        {
            if (string.IsNullOrWhiteSpace(name) || testCycle < 0 || requiredDayToFinish < 0)
                return false;
            return true;
        }
    }
}