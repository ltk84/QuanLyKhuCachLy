namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for QuarantinedPersonInformationSecondPage.xaml
    /// </summary>
    public partial class QuarantinedPersonInformationSecondPage : System.Windows.Controls.UserControl
    {
        public QuarantinedPersonInformationSecondPage()
        {
            InitializeComponent();
        }

        private void vaccineInfo_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
