using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class BaseItem
    {
        protected double widthField;
        protected double heightField;

        public BaseItem()
            : base()
        {
            this.uomField = Unit.British;
            this.widthField = 0;
            this.heightField = 0;
        }
        public BaseItem(double width, double height)
            : base()
        {
            this.uomField = Unit.British;
            this.widthField = width;
            this.heightField = height;
        }
    }
}