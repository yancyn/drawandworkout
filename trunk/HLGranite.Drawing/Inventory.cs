using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class Inventory
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Inventory()
            : base()
        {
            this.purchaseAtField = base.dateField;
        }
        /// <summary>
        /// Cloning constructor.
        /// </summary>
        /// <param name="item"></param>
        public Inventory(Inventory item)
            : base()
        {
            Copy(item);
        }
        protected void Copy(Inventory item)
        {
            this.heightField = item.Height;
            this.purchaseAtField = item.PurchaseAt;
            this.stockField = item.Stock;
            this.warehouseField = item.Warehouse;
            this.widthField = item.Width;
        }
    }
}