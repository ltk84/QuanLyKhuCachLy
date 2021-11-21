using System.Windows.Controls;
using System;

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

        private void QuarantinedPersonTable_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1R6zuZB_xFuzWrCnl4j0JLZ3da5HtprRrmjeQ3LdxW44/edit#gid=0");
        }
    }
}
