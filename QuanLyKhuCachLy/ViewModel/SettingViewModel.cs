using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        private Address _QAAdress;

        public Address QAAdress
        {
            get { return _QAAdress; }
            set { _QAAdress = value; }
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
            set { _QuarantineAreaAddress = value; OnPropertyChanged(); }
        }

        #region UI

        private Visibility _Tab1;
        public Visibility Tab1 { get => _Tab1; set { _Tab1 = value; OnPropertyChanged(); } }

        private Visibility _Tab2;
        public Visibility Tab2 { get => _Tab2; set { _Tab2 = value; OnPropertyChanged(); } }
        public int TabIndex { get; set; }

        private String _TabPostion;
        public String TabPosition
        {
            get => _TabPostion; set
            {
                _TabPostion = value; OnPropertyChanged();
            }
        }

        #endregion

        #region command
        public ICommand ToEditCommand { get; set; }
        public ICommand PreviousTabCommand { get; set; }
        public ICommand NextTabCommand { get; set; }
        public ICommand EditCommand { get; set; }

        #endregion

        #region validation rule

        private bool _QANameFieldHasError;
        public bool QANameFieldHasError { get => _QANameFieldHasError; set { _QANameFieldHasError = value; OnPropertyChanged(); } }

        private bool _QAProvinceFieldHasError;
        public bool QAProvinceFieldHasError { get => _QAProvinceFieldHasError; set { _QAProvinceFieldHasError = value; OnPropertyChanged(); } }

        private bool _QADistrictFieldHasError;
        public bool QADistrictFieldHasError { get => _QADistrictFieldHasError; set { _QADistrictFieldHasError = value; OnPropertyChanged(); } }

        private bool _QAWardFieldHasError;
        public bool QAWardFieldHasError { get => _QAWardFieldHasError; set { _QAWardFieldHasError = value; OnPropertyChanged(); } }

        private bool _QATestCycleFieldHasError;
        public bool QATestCycleFieldHasError { get => _QATestCycleFieldHasError; set { _QATestCycleFieldHasError = value; OnPropertyChanged(); } }

        private bool _QARequiredDayFieldHasError;
        public bool QARequiredDayFieldHasError { get => _QARequiredDayFieldHasError; set { _QARequiredDayFieldHasError = value; OnPropertyChanged(); } }


        #endregion

        #region list
        private ObservableCollection<string> _ProvinceList;
        public ObservableCollection<string> ProvinceList
        {
            get => _ProvinceList; set
            {
                _ProvinceList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _DistrictList;
        public ObservableCollection<string> DistrictList
        {
            get => _DistrictList; set
            {
                _DistrictList = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _WardList;
        public ObservableCollection<string> WardList
        {
            get => _WardList; set
            {
                _WardList = value; OnPropertyChanged();
            }
        }
        #endregion


        public SettingViewModel()
        {
            SetDefaultEditTab();

            ToEditCommand = new RelayCommand<object>((p) => { return true; }, (o) =>
            {
                EditQuarantineArea editScreen = new EditQuarantineArea();
                editScreen.ShowDialog();
            });

            EditCommand = new RelayCommand<Window>((p) => { return true; }, (o) =>
            {
                EditQuarantineAreaInformation();
                SetDefaultEditTab();
                o.Close();
            });

            PreviousTabCommand = new RelayCommand<object>((p) =>
            {
                if (TabIndex > 1) return true;
                return false;
            }, (o) =>
            {
                HandleChangeTab(TabIndex, "previous");
            });

            NextTabCommand = new RelayCommand<object>((p) =>
            {
                if (TabIndex <= 2)
                    return true;
                return false;
            }, (o) =>
            {
                HandleChangeTab(TabIndex, "next");
            });


            ProvinceList = new ObservableCollection<string>() {
                "Hồ Chí Minh", "Bình Dương", "Vĩnh Long"
            };
            DistrictList = new ObservableCollection<string>() {
                "Quận 1", "Quận 2", "Quận 3", "Quận 4"
            };
            WardList = new ObservableCollection<string>()
            {
                "Phú Thạnh", "Phú Thọ Hòa", "Bình Hưng Hòa"
            };

            if (DataProvider.ins.db.QuarantineAreas.Count() != 0)
            {
                QuarantineArea = DataProvider.ins.db.QuarantineAreas.First();
                QAAdress = QuarantineArea.Address;
                QuarantineAreaAddress = $"{QuarantineArea.Address?.apartmentNumber} {QuarantineArea.Address?.streetName}, {QuarantineArea.Address.ward}, {QuarantineArea.Address.district}, {QuarantineArea.Address.province}";
                Manager = DataProvider.ins.db.Staffs.Where(staff => staff.id == QuarantineArea.managerID).FirstOrDefault();
            }
        }

        #region method

        void SetDefaultEditTab()
        {
            Tab1 = Visibility.Visible;
            Tab2 = Visibility.Hidden;
            TabIndex = 1;
            TabPosition = $"{TabIndex}/2";
        }

        void EditQuarantineAreaInformation()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    DataProvider.ins.db.SaveChanges();

                    QuarantineAreaAddress = $"{QuarantineArea.Address?.apartmentNumber} {QuarantineArea.Address?.streetName}, {QuarantineArea.Address.ward}, {QuarantineArea.Address.district}, {QuarantineArea.Address.province}";

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db update";

                    MessageBox.Show(error);
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi validation";

                    MessageBox.Show(error);
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db đéo support";

                    MessageBox.Show(error);
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi db object disposed";

                    MessageBox.Show(error);
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    string error = "Lỗi invalid operation";

                    MessageBox.Show(error);
                }
            }
        }

        void HandleChangeTab(int index, string action)
        {
            if (action == "next")
            {
                TabIndex++;
            }
            else
            {
                TabIndex--;
            }
            if (TabIndex <= 2)
                TabPosition = $"{TabIndex}/2";

            switch (TabIndex)
            {
                case 1:
                    Tab1 = Visibility.Visible;
                    Tab2 = Visibility.Hidden;
                    break;
                case 2:
                    Tab1 = Visibility.Hidden;
                    Tab2 = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
