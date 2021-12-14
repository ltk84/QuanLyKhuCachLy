using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class CannotSendPeopleViewModel : BaseViewModel
    {

        private List<Model.QuarantinePerson> _peopleList;
        public List<Model.QuarantinePerson> PeopleList
        {
            get { return _peopleList; }
            set { _peopleList = value; OnPropertyChanged(); }
        }

        public ICommand ToExportExcel { get; set; }

        public ICommand CancelCommand { get; set; }

        public CannotSendPeopleViewModel()
        {
            PeopleList = new List<Model.QuarantinePerson>();

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
            });


            ToExportExcel = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                ExportExcel();
            });

        }
        void ExportExcel()
        {
            int count = PeopleList.Count;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            Microsoft.Office.Interop.Excel.Workbook file = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            file.Sheets.Add();
            Microsoft.Office.Interop.Excel.Worksheet sheet = file.Worksheets[1];          
            sheet.Name = "Danh sách người cách ly";
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 25;
            sheet.Columns[3].ColumnWidth = 12;
            sheet.Columns[4].ColumnWidth = 9;
            sheet.Columns[5].ColumnWidth = 50;
            sheet.Columns[6].ColumnWidth = 12;
            sheet.Columns[7].ColumnWidth = 12;
            sheet.Columns[8].ColumnWidth = 10;
            sheet.Columns[9].ColumnWidth = 12;
            sheet.Columns[10].ColumnWidth = 5;
            sheet.Columns[11].ColumnWidth = 12;
            sheet.Columns[12].ColumnWidth = 12;
            sheet.Columns[13].ColumnWidth = 10;
            sheet.Columns[14].ColumnWidth = 7;
            sheet.Columns[15].ColumnWidth = 15;
            sheet.Columns[16].ColumnWidth = 15;
           
            sheet.Range["A1"].Value = "STT";
            sheet.Range["B1"].Value = "Họ và tên";
            sheet.Range["C1"].Value = "Ngày sinh";
            sheet.Range["D1"].Value = "Giới tính";
            sheet.Range["E1"].Value = "Địa chỉ";
            sheet.Range["F1"].Value = "MaBH";
            sheet.Range["G1"].Value = "CMND/CCCD";
            sheet.Range["H1"].Value = "Quốc tịch";
            sheet.Range["I1"].Value = "SĐT";
            sheet.Range["J1"].Value = "Nhóm đối tượng";
            sheet.Range["K1"].Value = "Ngày đến";
            sheet.Range["L1"].Value = "Ngày hoàn thành";
            sheet.Range["M1"].Value = "Số ngày\ncách ly";
            sheet.Range["N1"].Value = "Phòng";
            sheet.Range["O1"].Value = "Đã hoàn thành\ncách ly";
            sheet.Range["P1"].Value = "Số mũi vaccine\nđã tiêm";

            for (int i = 2; i <= count + 1; i++)
            {
                int addressID = PeopleList[i - 2].addressID.Value;
                Address address = DataProvider.ins.db.Addresses.Where(x => x.id == addressID).FirstOrDefault();
                String personAddress = "";
                if (address.apartmentNumber != null)
                    personAddress += address.apartmentNumber.ToString();
                if (address.streetName != null)
                    personAddress += " " + address.streetName.ToString();
                if (address.ward != null)
                    personAddress += ", " + address.ward.ToString();
                if (address.district != null)
                    personAddress += ", " + address.district.ToString();
                if (address.province != null)
                    personAddress += ", " + address.province.ToString();
                Severity severity = new Severity();
                if (PeopleList[i - 2].levelID != null)
                {
                    int severityID = PeopleList[i - 2].levelID.Value;
                    severity = DataProvider.ins.db.Severities.Where(x => x.id == severityID).FirstOrDefault();
                }
                int personId = PeopleList[i - 2].id; ;
                int countInjectionRecord = DataProvider.ins.db.InjectionRecords.Where(x => x.quarantinePersonID == personId).Count();
                QuanLyKhuCachLy.Model.QuarantineRoom room = new Model.QuarantineRoom();
                if (PeopleList[i - 2].roomID != null)
                {
                    int roomID = PeopleList[i - 2].roomID.Value;
                    room = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == roomID).FirstOrDefault();
                }
                sheet.Range["A" + i.ToString()].Value = (i - 1).ToString();
                sheet.Range["B" + i.ToString()].Value = PeopleList[i - 2].name;
                sheet.Range["C" + i.ToString()].Value = PeopleList[i - 2].dateOfBirth;
                sheet.Range["D" + i.ToString()].Value = PeopleList[i - 2].sex;
                sheet.Range["E" + i.ToString()].Value = personAddress;
                sheet.Range["F" + i.ToString()].Value = PeopleList[i - 2].healthInsuranceID;
                sheet.Range["G" + i.ToString()].Value = PeopleList[i - 2].citizenID;
                sheet.Range["H" + i.ToString()].Value = PeopleList[i - 2].nationality;
                sheet.Range["I" + i.ToString()].Value = PeopleList[i - 2].phoneNumber;
                sheet.Range["J" + i.ToString()].Value = severity.description != null ? severity.description : "";
                sheet.Range["K" + i.ToString()].Value = PeopleList[i - 2].arrivedDate;
                sheet.Range["L" + i.ToString()].Value = PeopleList[i - 2].leaveDate;
                sheet.Range["M" + i.ToString()].Value = PeopleList[i - 2].quarantineDays;
                sheet.Range["N" + i.ToString()].Value = PeopleList[i - 2].roomID != null ? room.displayName : "";
                sheet.Range["O" + i.ToString()].Value = PeopleList[i - 2].completeQuarantine == true ? "X" : "";
                sheet.Range["P" + i.ToString()].Value = countInjectionRecord;
              
            }
            file.Close();
        }
    }
    
}
