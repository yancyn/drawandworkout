using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HLGranite.Drawing
{
    public partial class Stock
    {
        private ObservableCollection<Inventory> inventoriesField;
        [XmlIgnore()]
        public ObservableCollection<Inventory> Inventories
        {
            get
            {
                return this.inventoriesField;
            }
            set
            {
                if ((this.inventoriesField != null))
                {
                    if ((inventoriesField.Equals(value) != true))
                    {
                        this.inventoriesField = value;
                        this.OnPropertyChanged("Inventories");
                    }
                }
                else
                {
                    this.inventoriesField = value;
                    this.OnPropertyChanged("Inventories");
                }
            }
        }

        public Stock()
            : base()
        {
            this.Name1 = "<New Stock>";
            this.inventoriesField = new ObservableCollection<Inventory>();
        }
    }
}