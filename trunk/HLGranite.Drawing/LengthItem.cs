using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class LengthItem
    {
        public LengthItem()
        {
            this.lengthField = 0d;
            this.typeField = new Bullnose();
        }
        public LengthItem(double length)
        {
            this.lengthField = length;
            this.typeField = new Bullnose();
        }
        public LengthItem(string model, double length)
        {
            this.lengthField = length;
            this.typeField = new Bullnose(model);
        }
    }
}