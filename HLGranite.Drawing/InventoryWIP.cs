using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class InventoryWIP
    {
        public InventoryWIP()
            : base()
        {
            this.workedAtField = DateTime.Now;
            //this.workedByField = source.WorkedBy;
        }
        public InventoryWIP(InventoryWIP item)
            : base()
        {
            Copy(item);
        }
        protected void Copy(InventoryWIP source)
        {
            this.heightField = source.Height;
            this.widthField = source.Width;
            this.workedAtField = source.WorkedAt;
            this.workedByField = source.WorkedBy;
        }
    }
}