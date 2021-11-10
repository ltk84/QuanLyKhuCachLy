using System.Windows.Controls;

namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for StaffList.xaml
    /// </summary>
    public partial class StaffList : System.Windows.Controls.UserControl
    {
        public StaffList()
        {
            InitializeComponent();
        }
        private void Button_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            button.ContextMenu.DataContext = button.DataContext;
            button.ContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }
}
