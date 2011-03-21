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
using WorkOrderGUI;

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
            
            //sort collection by name1 alphabetically
            /*Stocks target = new Stocks();
            target = DatabaseObject.LoadFromFile() as Stocks;
            var hold = from f in target.Stock
                       select f;
            List<Stock> temp = hold.OrderBy(f => f.Name1).ToList();

            Stocks stocks = new Stocks();
            foreach (Stock s in temp)
                stocks.Stock.Add(s);
            */

            Stocks stocks = new Stocks();
            stocks = DatabaseObject.LoadFromFile() as Stocks;
            stocks.Refresh();
            PageViewModel page = new PageViewModel(stocks);

            PageManager pageManager = new PageManager();
            pageManager.Items.Add(page);
            MainGrid.DataContext = pageManager.Items[0];
            //this.StockView.ItemsSource = ((Stocks)pageManager.Items[0].Item).Stock;
        }

        private void StockWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(MainGrid.DataContext);
        }
    }
}