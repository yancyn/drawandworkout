using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class LabelShape
    {
        public LabelShape()
            : base()
        {
            this.textField = string.Empty;
        }
        public LabelShape(string text, double left, double top)
            : base(string.Empty, left, top)
        {
            this.textField = text;
        }
    }
}