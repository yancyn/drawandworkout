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
using System.IO;
using System.Windows.Markup;

namespace WorkOrderGUI.Sandbox
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            LoadXamlFile("500368f8-3a38-449d-b645-c702332fb941.xaml");
        }
        /// <summary>
        /// Load xaml file into current page.
        /// </summary>
        /// <param name="fileName"></param>
        /// <see>http://blogs.msdn.com/b/ashish/archive/2007/08/14/dynamically-loading-xaml.aspx</see>
        private void LoadXamlFile(string fileName)
        {
            try
            {
                StreamReader mysr = new StreamReader(fileName, Encoding.UTF8);
                DependencyObject rootObject = XamlReader.Load(mysr.BaseStream) as DependencyObject;
                this.AddChild(rootObject);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}