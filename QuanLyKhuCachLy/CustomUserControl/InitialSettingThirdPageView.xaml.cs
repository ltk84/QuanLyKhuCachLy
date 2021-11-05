using System.Windows.Input;

namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for InitialSettingSecondPageView.xaml
    /// </summary>
    public partial class InitialSettingThirdPageView : System.Windows.Controls.UserControl
    {
        public InitialSettingThirdPageView()
        {
            InitializeComponent();
        }

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }
    }
}
