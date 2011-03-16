using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HLGranite.Drawing;

namespace WorkOrderGUI
{
    public class PageViewModel
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
            if (this.item.GetType() == typeof(Project))
                return (item as Project).Guid.ToString();
            if (this.item.GetType() == typeof(DatabaseObject))
            {
                //todo: return (item as DatabaseObject).Name
            }

            return base.ToString();
        }
    }
}