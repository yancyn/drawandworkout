using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class Stock
    {
        public Stock()
        {
            this.Code = string.Empty;
            this.Name1 = string.Empty;
            this.Name2 = string.Empty;
            this.Notes = string.Empty;
            this.Tag = new List<object>();
        }
        public override string ToString()
        {
            if (this.Name2.Length > 0)
                return this.Name1 + " " + this.Name2;
            else if (this.Name1.Length > 0)
                return this.Name1;
            else
                return this.Code.ToString();
            //return base.ToString();
        }
    }
}