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
    public class ToolbarManager : System.ComponentModel.INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<ToolbarViewModel> items;
        /// <summary>
        /// Toolbar collection.
        /// </summary>
        public ObservableCollection<ToolbarViewModel> Items
        {
            get { return this.items; }
            set
            {
                if (this.items != null)
                {
                    if (this.items.Equals(value) != true)
                    {
                        this.items = value;
                        this.OnPropertyChanged("Items");
                    }
                }
                else
                {
                    this.items = value;
                    this.OnPropertyChanged("Items");
                }
            }
        }
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
                    ToolbarViewModel viewModel = null;
                    if (panel.Children[i] is Shape)
                    {
                        viewModel = new ToolbarViewModel(panel.Children[i] as Shape);
                    }
                    if (panel.Children[i] is Panel)
                    {
                        Panel panel2 = (Panel)panel.Children[i];
                        if (panel2.Children.Count == 2)  //todo: finetune
                        {
                            viewModel = new ToolbarViewModel(panel2.Children[0] as Shape);
                            Panel panel3 = (Panel)panel2.Children[1];
                            //remove 2 children from host
                            panel2.Children.RemoveAt(0);
                            panel2.Children.RemoveAt(0);

                            for (int j = panel3.Children.Count - 1; j >= 0; j--)
                            {
                                ToolbarViewModel v = null;
                                if (panel3.Children[j] is Shape)
                                {
                                    v = new ToolbarViewModel(panel3.Children[j] as Shape);
                                    v.Parent = viewModel;
                                }
                                panel3.Children.RemoveAt(j);
                                viewModel.Children.Insert(0, v);
                            }
                        }
                    }

                    panel.Children.RemoveAt(i);
                    //last in last out
                    if (viewModel != null) this.items.Insert(0, viewModel);//this.items.Add(viewModel);
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

        #region INotifyPropertyChanged Members
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}