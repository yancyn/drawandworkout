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
using HLGranite.Drawing;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for PrintingWindow.xaml
    /// </summary>
    public partial class PrintingWindow : Window
    {
        public PrintingWindow()
        {
            InitializeComponent();
        }
        public PrintingWindow(Project project, object drawing)
        {
            InitializeComponent();
            this.DataContext = project;
            if (drawing is Canvas)
            {
                Canvas canvas = (drawing as Canvas);
                this.PrintingGrid.Children.Add(canvas);
                Grid.SetRow(canvas, 1);
                Grid.SetColumn(canvas, 0);
            }
        }
    }
}