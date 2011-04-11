using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace WorkOrderGUI
{
    public class BullnoseManager : System.ComponentModel.INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<BullnoseViewModel> bullnoses;
        /// <summary>
        /// Gets bullnoses collection in graphics content.
        /// </summary>
        public ObservableCollection<BullnoseViewModel> Bullnoses
        {
            get { return this.bullnoses; }
            set
            {
                if (this.bullnoses != null)
                {
                    if (this.bullnoses.Equals(value) != true)
                    {
                        this.bullnoses = value;
                        this.OnPropertyChanged("Bullnoses");
                    }
                }
                else
                {
                    this.bullnoses = value;
                    this.OnPropertyChanged("Bullnoses");
                }
            }
        }
        #endregion

        public BullnoseManager()
        {
            this.bullnoses = new ObservableCollection<BullnoseViewModel>();
            Initialize();
        }

        #region Methods
        private void Initialize()
        {
            StreamReader reader = new StreamReader("Bullnoses.xaml");
            DependencyObject obj = XamlReader.Load(reader.BaseStream) as DependencyObject;
            Panel panel = ((System.Windows.Controls.Panel)((obj as Window).Content));
            foreach (UIElement element in panel.Children)
            {
                if (element is StackPanel)
                {
                    StackPanel item = element as StackPanel;
                    string model = (item.Name == null) ? string.Empty : item.Name;
                    string name = (item.Name == null) ? string.Empty : item.Name;
                    //todo: get the name by get rid of number at behind
                    this.bullnoses.Add(new BullnoseViewModel(model, name, DeepClone(item)));
                }
            }
            /*for (int i = panel.Children.Count - 1; i >= 0; i--)
            {
                if (panel.Children[i] is StackPanel)
                {
                    StackPanel item = panel.Children[i] as StackPanel;
                    this.bullnoses.Insert(0, new BullnoseViewModel(item.Name, item.Name, item));
                }
                panel.Children.RemoveAt(i);
            }
            */

            (obj as Window).Close();
        }
        private StackPanel DeepClone(StackPanel from)
        {
            StackPanel output = new StackPanel();
            output.Name = from.Name;
            output.Width = from.Width;
            output.Height = from.Height;
            foreach (UIElement element in from.Children)
            {
                UIElement child = null;
                if (element is TextBlock)
                {
                    child = new TextBlock();
                    (child as TextBlock).Text = (element as TextBlock).Text;
                }
                else if (element is Canvas)
                {
                    child = DeepClone(element as Canvas);
                }

                if (child != null)
                    output.Children.Add(child);
            }

            return output;
        }
        private Canvas DeepClone(Canvas from)
        {
            Canvas output = new Canvas();
            foreach (UIElement element in from.Children)
            {
                UIElement child = null;
                if (element is System.Windows.Shapes.Path)
                {
                    child = new System.Windows.Shapes.Path();
                    (child as System.Windows.Shapes.Path).Stroke = (element as System.Windows.Shapes.Path).Stroke;
                    (child as System.Windows.Shapes.Path).StrokeThickness = (element as System.Windows.Shapes.Path).StrokeThickness;
                    (child as System.Windows.Shapes.Path).Fill = (element as System.Windows.Shapes.Path).Fill;
                    (child as System.Windows.Shapes.Path).Data = (element as System.Windows.Shapes.Path).Data;
                    (child as System.Windows.Shapes.Path).Margin = (element as System.Windows.Shapes.Path).Margin;
                }
                else if (element is System.Windows.Shapes.Rectangle)
                {
                    child = new System.Windows.Shapes.Rectangle();
                    (child as Rectangle).Width = (element as Rectangle).Width;
                    (child as Rectangle).Height = (element as Rectangle).Height;
                    (child as Rectangle).Stroke = (element as Rectangle).Stroke;
                    (child as Rectangle).StrokeThickness = (element as Rectangle).StrokeThickness;
                    (child as Rectangle).Margin = (element as Rectangle).Margin;
                }

                if (child != null)
                    output.Children.Add(child);
            }

            return output;
        }
        /// <summary>
        /// Fail to deep clone StackPanel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        /// <see>http://stackoverflow.com/questions/32541/how-can-you-clone-a-wpf-object</see>
        public static T DeepClone<T>(T from) where T : StackPanel
        {
            using (MemoryStream s = new MemoryStream())
            {
                BinaryFormatter f = new BinaryFormatter();
                f.Serialize(s, from);
                s.Position = 0;
                object clone = f.Deserialize(s);

                return (T)clone;
            }
        }
        #endregion
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