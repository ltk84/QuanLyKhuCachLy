using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhuCachLy.ViewModel
{
    public class NationViewModel
    {
        public static List<NATION> NationList { get; set; }

        static NationViewModel()
        {
            NationList = new List<NATION>(DataProvider.ins.db.NATIONs);
        }
    }
}
