using System.Windows.Controls;

namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for QuarantinePersonFunctionMenu.xaml
    /// </summary>
    public partial class QuarantinePersonFunctionMenu : System.Windows.Controls.UserControl
    {
        public QuarantinePersonFunctionMenu()
        {
            InitializeComponent();
        }
        private void Button_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            button.ContextMenu.DataContext = button.DataContext;
            button.ContextMenu.IsOpen = true;
            button.ContextMenu.StaysOpen = true;
            e.Handled = true;
        }
    }
}
