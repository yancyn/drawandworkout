using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Windows.Controls;

namespace WorkOrderGUI
{
    public class ToolbarManager
    {
        private ObservableCollection<ToolbarViewModel> items;
        public ObservableCollection<ToolbarViewModel> Items { get { return this.items; } }
        public ToolbarManager()
        {
            this.items = new ObservableCollection<ToolbarViewModel>();

            try
            {
                StreamReader reader = new StreamReader("Toolbars.xaml");
                DependencyObject obj = XamlReader.Load(reader.BaseStream) as DependencyObject;
                Panel panel = ((System.Windows.Controls.Panel)((obj as Window).Content));
                for (int i = 0; i < panel.Children.Count; i++)
                {
                    if (panel.Children[i] is Shape)
                    {
                        ToolbarViewModel viewModel = new ToolbarViewModel(panel.Children[i] as Shape);
                        this.items.Add(viewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return;
            }
        }
    }
}
