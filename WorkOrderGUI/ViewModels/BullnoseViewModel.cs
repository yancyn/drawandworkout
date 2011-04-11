using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using HLGranite.Drawing;

namespace WorkOrderGUI
{
    public class BullnoseViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        #region Properties
        private Bullnose bullnose;
        /// <summary>
        /// Gets or sets bullnose object.
        /// </summary>
        public Bullnose Bullnose
        {
            get { return this.bullnose; }
            set
            {
                if (this.bullnose != null)
                {
                    if (this.bullnose.Equals(value) != true)
                    {
                        this.bullnose = value;
                        this.OnPropertyChanged("Bullnose");
                    }
                }
                else
                {
                    this.bullnose = value;
                    this.OnPropertyChanged("Bullnose");
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

        public BullnoseViewModel(Bullnose bullnose, StackPanel stackPanel)
        {
            this.bullnose = bullnose;
            this.content = stackPanel;
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