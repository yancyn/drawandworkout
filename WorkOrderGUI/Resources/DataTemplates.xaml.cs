using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorkOrderGUI
{
    public partial class DataTemplates : ResourceDictionary
    {
        public DataTemplates()
        {
        }
        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            WorkOrderGUI.Properties.Settings.Default.Save();
        }
    }
}