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
using HLGranite.Drawing;

namespace WorkOrderGUI
{
    /// <summary>
    /// Page template selector based on PageViewModel.Item type.
    /// </summary>
    /// <see>http://msdn.microsoft.com/en-us/library/ms742521.aspx</see>
    public class PageTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            if (element != null && item != null && item is PageViewModel)
            {
                PageViewModel viewModel = item as PageViewModel;
                if (viewModel.Item is Project)
                    return element.FindResource("ProjectTemplate") as DataTemplate;
                else if (viewModel.Item is Stocks)
                    return element.FindResource("StockTemplate") as DataTemplate;
                else if (viewModel.Item is Warehouses)
                    return element.FindResource("WarehouseTemplate") as DataTemplate;
                else if (viewModel.Title == "Setting")
                    return element.FindResource("SettingTemplate") as DataTemplate;
                //else if (viewModel.Item is Users)
                //todo: return element.FindResource("UserTemplate") as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
    public class WorkItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            if (element != null && item is WorkItem)
            {
                WorkItem wo = item as WorkItem;
                if (wo.Tags.Count > 0)
                {
                    return element.FindResource(wo.Tags[0].ToString()) as DataTemplate;
                    //if (item is LShapeItem)
                    //    return element.FindResource("LShapeTemplate") as DataTemplate;
                    //else if (item is RectItem)
                    //    return element.FindResource("RectTemplate") as DataTemplate;
                }

                return base.SelectTemplate(item, container);
            }

            return base.SelectTemplate(item, container);
        }
    }
}