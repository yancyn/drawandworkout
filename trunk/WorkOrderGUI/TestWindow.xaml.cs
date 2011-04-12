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
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
            //Margin margin = new Thickness();

            //Stocks stocks = new Stocks();
            //stocks = stocks.LoadFromFile() as Stocks;
            //this.ComboBox1.ItemsSource = stocks.Stock;
            //this.ComboBox2.ItemsSource = Enum.GetValues(typeof(ProjectStage));

            //BullnoseManager manager = new BullnoseManager();
            //this.ComboBox1.DataContext = manager;
            //this.ComboBox2.DataContext = manager;
            //this.ComboBox3.DataContext = manager;
            //this.ComboBox1.ItemsSource = manager.Bullnoses;
            //foreach (BullnoseViewModel viewModel in manager.Bullnoses)
            //    this.ComboBox2.Items.Add(viewModel.Content);
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine((sender as ComboBox).SelectedValue);
        }
    }
}