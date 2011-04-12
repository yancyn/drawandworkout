using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class VerticalLine
    {
        public VerticalLine()
            : base()
        {
        }
        public VerticalLine(double left)
            : base(string.Empty, left, 0)
        {
        }
        public VerticalLine(string model, double left)
            : base(model, left, 0)
        {
        }
    }
}