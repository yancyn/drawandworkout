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
        #region Properties
        private ObservableCollection<ToolbarViewModel> items;
        /// <summary>
        /// Toolbar collection.
        /// </summary>
        public ObservableCollection<ToolbarViewModel> Items { get { return this.items; } }
        #endregion

        public ToolbarManager()
        {
            this.items = new ObservableCollection<ToolbarViewModel>();
            Initialize();
        }
        protected void Initialize()
        {
            StreamReader reader = new StreamReader("Toolbars.xaml");

            try
            {
                DependencyObject obj = XamlReader.Load(reader.BaseStream) as DependencyObject;
                Panel panel = ((System.Windows.Controls.Panel)((obj as Window).Content));
                for (int i = panel.Children.Count - 1; i >= 0; i--)
                {
                    if (panel.Children[i] is Shape)
                    {
                        ToolbarViewModel viewModel = new ToolbarViewModel(panel.Children[i] as Shape);
                        panel.Children.RemoveAt(i);
                        this.items.Add(viewModel);
                    }
                }
                (obj as Window).Close();//key: force hidden window close or dispose.
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }
    }
}