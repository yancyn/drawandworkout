using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class HorizontalLine
    {
        public HorizontalLine() : base() { }
        public HorizontalLine(string model, double top)
            : base(model, 0, top)
        {
        }
    }
}