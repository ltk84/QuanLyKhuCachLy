using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhuCachLy.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {

        private Model.QuarantineArea _QuarantineArea;

        public Model.QuarantineArea QuarantineArea
        {
            get { return _QuarantineArea; }
            set { _QuarantineArea = value; }
        }

        private Model.Staff _Manager;

        public Model.Staff Manager
        {
            get { return _Manager; }
            set { _Manager = value; }
        }


        private string _QuarantineAreaAddress;

        public string QuarantineAreaAddress
        {
            get { return _QuarantineAreaAddress; }
            set { _QuarantineAreaAddress = value; }
        }



        public SettingViewModel()
        {
            if (DataProvider.ins.db.QuarantineAreas.Count() != 0)
            {
                QuarantineArea = DataProvider.ins.db.QuarantineAreas.First();
                QuarantineAreaAddress = $"{QuarantineArea.Address?.apartmentNumber} {QuarantineArea.Address?.streetName}, {QuarantineArea.Address.ward}, {QuarantineArea.Address.district}, {QuarantineArea.Address.province}";
                Manager = DataProvider.ins.db.Staffs.Where(staff => staff.id == QuarantineArea.managerID).First();
            }
        }
    }
}
