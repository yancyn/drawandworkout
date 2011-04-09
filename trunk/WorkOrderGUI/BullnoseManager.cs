using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace WorkOrderGUI
{
    public class BullnoseManager
    {
        private ObservableCollection<BullnoseViewModel> bullnoses;
        /// <summary>
        /// Gets bullnoses collection in graphics content.
        /// </summary>
        public ObservableCollection<BullnoseViewModel> Bullnoses { get { return this.bullnoses; } }
        public BullnoseManager()
        {
            this.bullnoses = new ObservableCollection<BullnoseViewModel>();
            Initialize();
        }
        private void Initialize()
        {
            StreamReader reader = new StreamReader("Bullnoses.xaml");
            DependencyObject obj = XamlReader.Load(reader.BaseStream) as DependencyObject;
            Panel panel = ((System.Windows.Controls.Panel)((obj as Window).Content));
            for (int i = panel.Children.Count - 1; i >= 0; i--)
            {
                if (panel.Children[i] is StackPanel)
                {
                    StackPanel item = panel.Children[i] as StackPanel;
                    //todo: get the name only by get rid of number at behind
                    this.bullnoses.Insert(0, new BullnoseViewModel(item.Name, item.Name, item));
                }
                panel.Children.RemoveAt(i);
            }
            (obj as Window).Close();
        }
    }
}