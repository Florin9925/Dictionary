using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public static class Switcher
    {
        public static MainWindow pageSwitcher;
        public static void Switch(System.Windows.Controls.Page newPage)
        {
            pageSwitcher.Navigate(newPage);
        }
    }
}
