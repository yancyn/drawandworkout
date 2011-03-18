using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HLGranite.Drawing;

namespace WorkOrderGUI
{
    public class PageViewModel : object, System.ComponentModel.INotifyPropertyChanged
    {
        private Object item;
        public Object Item { get { return this.item; } set { this.item = value; } }

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
            else if (this.item.GetType() == typeof(DatabaseObject))
            {
                //todo: return (item as DatabaseObject).Name
            }

            return output;
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