using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class Warehouse
    {
        public Warehouse()
            : base()
        {
            this.Name1 = "<New Warehouse>";
            this.addressesField = new Thought.vCards.vCardDeliveryAddressCollection();
        }
    }
}