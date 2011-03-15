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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// <remarks>
    /// todo: still buggy for popup multiple toolbar.
    /// TODO: change the selected shape toolbar if there are muliple choices.
    /// todo: toolbox imcompatible on different screen resolution.
    /// </remarks>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //System.Diagnostics.Debug.WriteLine(rect1.Height);
            //rect1.Margin;
            //propertyGrid1.Instance = 
            //MainMenu.ItemsSource = BuildMenu();
        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// Obsolete. Build up menu for application.
        /// </summary>
        /// <returns></returns>
        private List<MenuItem> BuildMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            MenuItem item1 = new MenuItem { Name = "_File" };
            item1.Items.Add(new MenuItem { Name = "_New" });
            item1.Items.Add(new MenuItem { Name = "_Open" });
            item1.Items.Add(new MenuItem { Name = "_Save" });
            item1.Items.Add(new Separator());
            item1.Items.Add(new MenuItem { Name = "_Close" });

            MenuItem item2 = new MenuItem { Name = "_Edit" };
            //item.Command = "";
            item2.Items.Add(new MenuItem { Name = "_Cut" });
            item2.Items.Add(new MenuItem { Name = "_Copy" });
            item2.Items.Add(new MenuItem { Name = "_Paste" });

            MenuItem item3 = new MenuItem { Name = "_Help" };
            item3.Items.Add(new MenuItem { Name = "_Index" });
            item3.Items.Add(new MenuItem { Name = "_About" });

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            return items;
        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            string version = "ver " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MessageBox.Show("yancyn@gmail.com" + "\n" + version);
        }
    }
}