using System.Windows.Controls;

namespace QuanLyKhuCachLy.CustomUserControl
{
    /// <summary>
    /// Interaction logic for ListQuarantinedPerson.xaml
    /// </summary>
    public partial class ListQuarantinedPerson : System.Windows.Controls.UserControl
    {
        public ListQuarantinedPerson()
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

        private void QuarantinedPersonTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
