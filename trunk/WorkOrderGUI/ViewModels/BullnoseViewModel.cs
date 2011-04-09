using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WorkOrderGUI
{
    public class BullnoseViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        #region Properties
        private string model;
        public string Model
        {
            get { return this.model; }
            set
            {
                if (this.model != null)
                {
                    if (this.model.Equals(value) != true)
                    {
                        this.model = value;
                        this.OnPropertyChanged("Model");
                    }
                }
                else
                {
                    this.model = value;
                    this.OnPropertyChanged("Model");
                }
            }
        }
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != null)
                {
                    if (this.name.Equals(value) != true)
                    {
                        this.name = value;
                        this.OnPropertyChanged("Name");
                    }
                }
                else
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
        private StackPanel content;
        public StackPanel Content
        {
            get { return this.content; }
            set
            {
                if (this.content != null)
                {
                    if (this.content.Equals(value) != true)
                    {
                        this.content = value;
                        this.OnPropertyChanged("Content");
                    }
                }
                else
                {
                    this.content = value;
                    this.OnPropertyChanged("Content");
                }
            }
        }
        #endregion

        public BullnoseViewModel(string model, string name, StackPanel stackPanel)
        {
            this.model = model;
            this.name = name;
            this.content = stackPanel;//DeepClone<StackPanel>(stackPanel);
        }
        /// <summary>
        /// todo: fail to deep clone StackPanel.
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