using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class BaseItem
    {
        protected int widthField;

        protected int heightField;
        public BaseItem():base()
        {
            this.uomField = Unit.British;
            this.widthField = 0;
            this.heightField = 0;
        }
    }
}