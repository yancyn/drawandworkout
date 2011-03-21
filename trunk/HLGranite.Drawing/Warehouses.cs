using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace HLGranite.Drawing
{
    public partial class Warehouses
    {
        public Warehouses()
        {
            fileName = "Warehouses.xml";
            this.warehouseField = new ObservableCollection<Warehouse>();
        }
    }
}
