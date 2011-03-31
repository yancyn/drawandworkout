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
        {
            this.lengthsField = new System.Collections.ObjectModel.ObservableCollection<LengthItem>();
        }
    }
}