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
    public partial class DataTemplates : ResourceDictionary
    {
        private Point originalPoint;
        public DataTemplates()
        {
        }

        #region Events
        private void DrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DrawingArea_MouseLeftButtonDown: " + new Random().Next() + " " + e.Device.Target.ToString());
            Canvas canvas = (sender as Canvas);//"DrawingArea"
            PageManager pageManager = (App.Current.MainWindow.FindName("MainGrid") as Grid).DataContext as PageManager;

            this.originalPoint = e.GetPosition(canvas);
            if (e.Device.Target is Shape)
            {
                //this.selectedWorkItem = (e.Device.Target as Shape).DataContext as WorkItem;
                if (pageManager.CurrentPage.Item is Project)
                {
                    (pageManager.CurrentPage.Item as Project).WorkOrders[0].SelectedItem = (e.Device.Target as Shape).DataContext as WorkItem;
                    System.Diagnostics.Debug.WriteLine((pageManager.CurrentPage.Item as Project).WorkOrders[0].SelectedItem.ToString());
                }
            }
        }
        private void ScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DrawingArea_MouseLeftButtonUp" + new Random().Next());
            PageManager pageManager = (App.Current.MainWindow.FindName("MainGrid") as Grid).DataContext as PageManager;
            if ((pageManager.CurrentPage.Item as Project).WorkOrders[0].SelectedItem == null)
            {
                ToolbarManager toolbarManager = (App.Current.MainWindow.FindName("Toolbox") as ToolBar).DataContext as ToolbarManager;
                if (toolbarManager.SelectedToolbar != null)
                {
                    //todo: handle more WorkItem selection
                    if (toolbarManager.SelectedToolbar.Name.Contains("LShapeItem"))
                    {
                        LShapeItem wo = new LShapeItem();
                        wo.Model = toolbarManager.SelectedToolbar.Name;
                        (pageManager.CurrentPage.Item as Project).WorkOrders[0].Items.Add(wo);
                    }
                    else if (toolbarManager.SelectedToolbar.Name.Contains("RectItem"))
                    {
                        RectItem wo = new RectItem();
                        wo.Model = toolbarManager.SelectedToolbar.Name;
                        (pageManager.CurrentPage.Item as Project).WorkOrders[0].Items.Add(wo);
                    }
                }
            }
            else
            {
                Canvas canvas = (sender as Control).FindName("DrawingArea") as Canvas;
                Point newPosition = e.GetPosition(canvas);
                (pageManager.CurrentPage.Item as Project).WorkOrders[0].SelectedItem.Left += newPosition.X - originalPoint.X;
                (pageManager.CurrentPage.Item as Project).WorkOrders[0].SelectedItem.Top += newPosition.Y - originalPoint.Y;
                (pageManager.CurrentPage.Item as Project).WorkOrders[0].SelectedItem = null;//reset
            }
        }
        //todo: handle more other key event ie. left & right key for moving graphic object.
        private void ScrollViewer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                PageManager pageManager = (App.Current.MainWindow.FindName("MainGrid") as Grid).DataContext as PageManager;
                (pageManager.CurrentPage.Item as Project).WorkOrders[0].RemoveItem();
            }
        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            WorkOrderGUI.Properties.Settings.Default.Save();
        }
        #endregion

        private void BullnoseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ComboBox_SelectionChanged");
            if (((sender as ComboBox).DataContext as LengthItem).Type == null)
            {
                LengthItem source = ((sender as ComboBox).DataContext as LengthItem);
                LengthItem length = new LengthItem();
                length.Length = source.Length;
                length.Type = new Bullnose((sender as ComboBox).SelectedValue.ToString());
                (sender as ComboBox).DataContext = length;
                //((sender as ComboBox).DataContext as LengthItem).Type = new Bullnose(
                //    (sender as ComboBox).SelectedValue.ToString());
            }
        }
    }
}