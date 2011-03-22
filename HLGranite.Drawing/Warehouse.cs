using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thought.vCards;

namespace HLGranite.Drawing
{
    public partial class Warehouse
    {
        public Warehouse()
            : base()
        {
            this.Name1 = "<New Warehouse>";
            this.addressesField = new Thought.vCards.vCardDeliveryAddressCollection();
            //this.addressesField.Add(new vCardDeliveryAddress());
        }
    }
}