using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HLGranite.Drawing;

namespace WorkOrderGUI
{
    public class PageViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private Object item;
        public Object Item
        {
            get { return this.item; }
            set
            {
                if (this.item != null)
                {
                    this.item = value;
                    this.OnPropertyChanged("Item");
                }
                else
                {
                    this.item = value;
                    this.OnPropertyChanged("Item");
                }
            }
        }

        public PageViewModel(Project project)
        {
            this.item = project;
        }
        public PageViewModel(DatabaseObject database)
        {
            this.item = database;
        }
        /// <summary>
        /// Returns a default key represent this PageViewModel.
        /// Normally is refer to it's primary key or Guid.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = base.ToString();
            if (this.item.GetType() == typeof(Project))
            {
                Project project = item as Project;
                output = project.CreatedAt.ToString("yyMMdd-HHmm");
                //<!-- &#x0a; line break -->
                //<!-- &#0d; tab -->
                if (project.WorkOrders.Count > 0 && project.WorkOrders[0].Items.Count > 0)
                {
                    if (project.WorkOrders[0].Items[0].Material.Name2.Length > 0)
                        output += "\n" + project.WorkOrders[0].Items[0].Material.Name2;
                    else if (project.WorkOrders[0].Items[0].Material.Name1.Length > 0)
                        output += "\n" + project.WorkOrders[0].Items[0].Material.Name1;
                }
                //output = (item as Project).Guid.ToString();
            }
            else if (this.item.GetType() == typeof(Stocks))
            {
                output = "Stock";
            }
            else if (this.item.GetType() == typeof(Warehouses))
            {
                output = "Warehouse";
            }
            else if (this.item.GetType() == typeof(Users))
            {
                output = "Contacts";
            }

            return output;
        }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}