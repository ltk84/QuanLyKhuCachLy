namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for QuarantineRoomInfomation.xaml
    /// </summary>
    public partial class QuarantineRoomInfomation : System.Windows.Controls.UserControl
    {
        public QuarantineRoomInfomation()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
