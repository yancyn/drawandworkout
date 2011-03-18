using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class WorkOrder
    {
        public WorkOrder()
        {
            this.guidField = Guid.NewGuid();
            this.itemsField = new List<WorkItem>();
        }
    }
}