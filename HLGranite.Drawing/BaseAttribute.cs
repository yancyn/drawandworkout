using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class BaseAttribute
    {
        protected DateTime dateField;

        public BaseAttribute()
        {
            this.guidField = Guid.NewGuid();
            this.notesField = string.Empty;
            this.dateField = DateTime.Now;
            this.tagField = new List<object>();
        }
    }
}
