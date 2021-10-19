using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyKhuCachLy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // walk up the tree to get the parent window
            FrameworkElement parent = this.Parent;
            while (parent != null && !parent is Window)
            {
                parent = parent.Parent;
            }

            // if window found, set style
            if (parent != null && parent is Window)
            {
                parent.WindowStyle = WindowStyle.None;
            }
        }
    }
}
