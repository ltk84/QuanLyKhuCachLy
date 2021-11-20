using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhuCachLy.ViewModel
{
    public class AddressViewModel
    {
        public static List<PROVINCE> ProvinceList { get; set; }
        public static List<DISTRICT> DistrictList { get; set; }
        public static List<WARD> WardList { get; set; }

        static AddressViewModel()
        {
            ProvinceList = new List<PROVINCE>(DataProvider.ins.db.PROVINCEs);

            DistrictList = new List<DISTRICT>();

            WardList = new List<WARD>();
        }

        public static void ProvinceSelectEvent(string provinceName)
        {
            var province = ProvinceList.Where(x => x.name == provinceName).FirstOrDefault();
            if (province == null) return;

            DistrictList = new List<DISTRICT>(DataProvider.ins.db.DISTRICTs.Where(x => x.provinceID == province.id));
        }

        public static void DistrictSelectEVent(string districtName)
        {
            var district = DistrictList.Where(x => x.name == districtName).FirstOrDefault();
            if (district == null) return;

            WardList = new List<WARD>(DataProvider.ins.db.WARDs.Where(x => x.districtID == district.id));
        }
    }
}
