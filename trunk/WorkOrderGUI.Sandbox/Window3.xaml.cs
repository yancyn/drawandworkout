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

namespace WorkOrderGUI.Sandbox
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();

            Window1 win1 = new Window1();
            Panel panel1 = (Panel)win1.Content;
            win1.Content = null;
            win1.Close();

            Window2 win2 = new Window2();
            Panel panel2 = (Panel)win2.Content;
            win2.Content = null;
            win2.Close();

            this.TabControl1.Items.Add(panel1);
            this.TabControl1.Items.Add(panel2);
        }
    }
}
