using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class WorkItem
    {
        public WorkItem()
        {
            this.createdAtField = base.dateField;
            this.elementsField = new List<WorkItem>();
            this.materialField = new Stock();
            this.maxHeightField = 0;
            this.progressField = 0;
            this.workedByField = new Employee();
        }
    }
}
