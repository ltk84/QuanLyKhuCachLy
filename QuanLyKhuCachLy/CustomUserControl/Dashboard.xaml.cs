using System.Windows.Controls;

namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : System.Windows.Controls.UserControl
    {
        public Dashboard()
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
        private void Button_PreviewMouseDown1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            button.ContextMenu.DataContext = button.DataContext;
            button.ContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }
}
