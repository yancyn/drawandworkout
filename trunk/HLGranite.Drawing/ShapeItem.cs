using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class ShapeItem
    {
        public ShapeItem()
        {
            this.leftField = 0d;
            this.topField = 0d;
            this.modelField = string.Empty;
        }
        public ShapeItem(string model, double left, double top)
        {
            this.modelField = model;
            this.leftField = left;
            this.topField = top;
        }
    }
}