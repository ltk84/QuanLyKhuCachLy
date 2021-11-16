namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for AddQuarantinedPersonThirdPage.xaml
    /// </summary>
    public partial class AddQuarantinedPersonThirdPage : System.Windows.Controls.UserControl
    {
        public AddQuarantinedPersonThirdPage()
        {
            InitializeComponent();
        }

        private void vaccinationInformation_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
