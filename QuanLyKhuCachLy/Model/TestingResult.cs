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

    public partial class TestingResult
    {
        public int id { get; set; }
        public System.DateTime dateTesting { get; set; }
        public bool isPositive { get; set; }
        public int quarantinePersonID { get; set; }

        public virtual QuarantinePerson QuarantinePerson { get; set; }
    }
}
