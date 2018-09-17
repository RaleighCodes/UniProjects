using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CPSC_481_PROJECT
{
    class PageSwitcher
    {
        //instantiation of MainWindow to switch UserControl pages on
        public static MainWindow pageSwitchWindow;
        
        /// <summary>
        /// Switch which UserControl Page for MainWindow to display
        /// </summary>
        /// <param name="newPage"></param>
        public static void Switch(UserControl newPage)
        {
            pageSwitchWindow.Navigate(newPage);
        }

    }
}
