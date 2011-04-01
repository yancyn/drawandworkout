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
        public PolygonItem()
            : base()
        {
            this.lengthsField = new System.Collections.ObjectModel.ObservableCollection<LengthItem>();
        }
        /// <summary>
        /// Recommended constructor.
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public PolygonItem(Stock stock, double width, double height)
            : base(stock, width, height)
        {
            this.lengthsField = new System.Collections.ObjectModel.ObservableCollection<LengthItem>();
        }
    }
}