using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for QuarantinePersonInRoomInformationSecondPage.xaml
    /// </summary>
    public partial class QuarantinePersonInRoomInformationSecondPage : UserControl
    {
        public QuarantinePersonInRoomInformationSecondPage()
        {
            InitializeComponent();
        }

        private void vaccineInfo_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
