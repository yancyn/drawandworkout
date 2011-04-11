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
            for (int i = 0; i < 4; i++)
                this.lengthsField.Add(new LengthItem());
        }
        /// <summary>
        /// Recommended constructor.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stock"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RectItem(string model, Stock stock, double width, double height)
            : base(model, stock, width, height)
        {
            this.lengthsField.Add(new LengthItem(height));
            this.lengthsField.Add(new LengthItem(width));
            this.lengthsField.Add(new LengthItem(height));
            this.lengthsField.Add(new LengthItem(width));
        }
    }
}