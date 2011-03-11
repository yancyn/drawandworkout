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
using System.Windows.Markup;
using System.IO;

namespace WorkOrderGUI.Sandbox
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //UIElement element = ;
            System.Diagnostics.Debug.WriteLine(e.Device.Target.ToString());
            if (e.Device.Target is Rectangle)
            {
                Rectangle rect = (e.Device.Target as Rectangle);
                System.Diagnostics.Debug.WriteLine(rect.Width + "x" + rect.Height);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            WriteToXaml();
        }
        /// <summary>
        /// Write to xaml file.
        /// </summary>
        /// <see>http://msdn.microsoft.com/en-us/library/system.windows.markup.xamlwriter.aspx</see>
        private void WriteToXaml()
        {
            try
            {
                string xaml = XamlWriter.Save(this.DrawingArea);
                System.Diagnostics.Debug.WriteLine(xaml);

                Guid fileName = Guid.NewGuid();
                StreamWriter writer = File.AppendText(fileName.ToString() + ".xaml");
                writer.WriteLine(xaml);
                writer.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
