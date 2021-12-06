using System.Windows;
using System.Windows.Controls;

namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for QuarantineRoom.xaml
    /// </summary>
    public partial class QuarantineRoom : System.Windows.Controls.UserControl
    {
        public QuarantineRoom()
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

        private void Button_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Button btn = sender as Button;
            //ContextMenu contextMenu = btn.ContextMenu;
            //MessageBox.Show(contextMenu.IsOpen.ToString());
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
