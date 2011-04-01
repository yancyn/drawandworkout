using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class RectItem
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RectItem()
            : base()
        {
        }
        public RectItem(Stock stock, double width, double height)
            : base(stock, width, height)
        {
        }
    }
}