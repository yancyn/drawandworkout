﻿using System;
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
    public partial class CustomControls : ResourceDictionary
    {
        public CustomControls()
        {
        }
        //Obsolete
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CloseButton_Click");
            if (sender is Button)
            {
                TabItem item = ((System.Windows.FrameworkElement)((sender as Button).TemplatedParent)).TemplatedParent as TabItem;

                //It is fail on data binding where there is no physical host exist!
                (item.Parent as TabControl).Items.Remove(item);
            }
        }
    }
}