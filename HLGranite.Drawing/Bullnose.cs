using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class Bullnose
    {
        public Bullnose() : base() { }
        public Bullnose(string model) : base(model) { }
        public Bullnose(string model, double width, double height)
            : base(model)
        {
            this.widthField = width;
            this.heightField = height;
        }
    }
}