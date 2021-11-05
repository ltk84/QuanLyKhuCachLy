using System.Windows.Input;

namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for InitialSettingFirstPageView.xaml
    /// </summary>
    public partial class InitialSettingFirstPageView : System.Windows.Controls.UserControl
    {
        public InitialSettingFirstPageView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }
    }
}
