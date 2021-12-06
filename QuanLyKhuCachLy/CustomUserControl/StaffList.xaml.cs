using System.Windows;
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

        private void ContextMenu_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var contextMenu = sender as System.Windows.Controls.ContextMenu;
            if (contextMenu == null) return;
            contextMenu.IsOpen = false;
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();

        }
    }
}
