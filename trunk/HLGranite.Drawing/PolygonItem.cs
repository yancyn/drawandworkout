using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    /// <summary>
    /// An polygon item.
    /// </summary>
    public partial class PolygonItem
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PolygonItem() : base() { }
        public PolygonItem(string model) : base(model) { }
        /// <summary>
        /// Recommended constructor.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stock"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public PolygonItem(string model, Stock stock, double width, double height)
            : base(model, stock, width, height) { }
    }
}