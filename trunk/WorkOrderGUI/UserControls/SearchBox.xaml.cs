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
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
        }
        private void SearchImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SearchKeyword(this.SearchTextBox.Text);
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SearchKeyword((sender as TextBox).Text);
        }
        /// <summary>
        /// TODO: Search keyword.
        /// </summary>
        /// <param name="keyword"></param>
        private void SearchKeyword(string keyword)
        {
            System.Diagnostics.Debug.WriteLine("SearchImage_MouseLeftButtonDown");
        }
    }
}