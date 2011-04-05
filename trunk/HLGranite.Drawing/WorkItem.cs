using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace HLGranite.Drawing
{
    public partial class WorkItem
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WorkItem()
            : base()
        {
            Initialize();
        }
        /// <summary>
        /// Recommended constructor.
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public WorkItem(Stock stock, double width, double height)
            : base(width, height)
        {
            Initialize();
            this.materialField = stock;
        }
        protected void Initialize()
        {
            this.createdAtField = base.dateField;
            this.elementsField = new ObservableCollection<WorkItem>();
            this.lengthsField = new ObservableCollection<LengthItem>();
            this.leftField = 0;
            this.materialField = new Stock();
            this.progressField = 0;
            this.topField = 0;
            this.workedByField = new Employee();
        }
    }
}