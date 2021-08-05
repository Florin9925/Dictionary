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

namespace Dictionary
{
    /// <summary>
    /// Interaction logic for PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
        }

        private void ButtonAdministrator_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PageAdmin());
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PageSearch());
        }

        private void ButtonEntertainment_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new PageEntertainment());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            (Switcher.pageSwitcher as MainWindow).Close();
        }
    }
}
