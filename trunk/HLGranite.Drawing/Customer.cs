﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thought.vCards;

namespace HLGranite.Drawing
{
    public partial class Customer
    {
        public Customer()
            : base()
        {
            this.typeField = UserRole.Customer;
            //this.DeliveryAddresses.Add(new vCardDeliveryAddress());
        }
        public Customer(vCard card)
            : base(card)
        {
            this.typeField = UserRole.Customer;
            //this.DeliveryAddresses.Add(new vCardDeliveryAddress());
        }
        public Customer(User user)
            : base(user as vCard)
        {
            this.typeField = UserRole.Customer;
            //this.DeliveryAddresses.Add(new vCardDeliveryAddress());
        }
    }
}