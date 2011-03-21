using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace HLGranite.Drawing
{
    public partial class WorkItem
    {
        public WorkItem()
        {
            this.createdAtField = base.dateField;
            this.elementsField = new ObservableCollection<WorkItem>();
            this.materialField = new Stock();
            this.maxHeightField = 0;
            this.progressField = 0;
            this.workedByField = new Employee();
        }
    }
}
