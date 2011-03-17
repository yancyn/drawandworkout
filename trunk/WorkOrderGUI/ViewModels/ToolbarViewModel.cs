using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace WorkOrderGUI
{
    public class ToolbarViewModel : object, System.ComponentModel.INotifyPropertyChanged
    {
        private Shape item;
        public Shape Item { get { return this.item; } set { this.item = value; } }
        public ToolbarViewModel(Shape shape)
        {
            this.item = shape;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}