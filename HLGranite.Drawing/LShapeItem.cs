using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class LShapeItem
    {
        public LShapeItem()
            : base()
        {
        }
        /// <summary>
        /// Recommended constructor.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stock"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public LShapeItem(string model, Stock stock, double width, double height)
            : base(model, stock, width, height)
        {
            double length = 24;//a normal default kitchen top length
            this.elementsField.Add(new RectItem("RectItem00", stock, width, length));
            this.elementsField.Add(new RectItem("RectItem00", stock, length, height));

            this.lengthsField.Add(new LengthItem { Length = height });
            this.lengthsField.Add(new LengthItem { Length = length });
            this.lengthsField.Add(new LengthItem { Length = height - length });
            this.lengthsField.Add(new LengthItem { Length = width - length });
            this.lengthsField.Add(new LengthItem { Length = length });
            this.lengthsField.Add(new LengthItem { Length = width });
        }
    }
}