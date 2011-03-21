using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace HLGranite.Drawing
{
    public partial class WorkOrder
    {
        public WorkOrder()
        {
            this.guidField = Guid.NewGuid();
            this.itemsField = new ObservableCollection<WorkItem>();
        }
    }
}