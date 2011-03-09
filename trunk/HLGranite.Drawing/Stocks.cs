using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace HLGranite.Drawing
{
    public partial class Stocks
    {
        public Stocks()
        {
            fileName = "Stocks.xml";
            this.stockField = new List<Stock>();
        }
    }
}