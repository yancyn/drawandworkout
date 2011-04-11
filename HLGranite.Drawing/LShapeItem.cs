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
            for (int i = 0; i < 2; i++)
                this.elementsField.Add(new RectItem("RectItem00", null, 0, 0));

            for (int i = 0; i < 6; i++)
                this.lengthsField.Add(new LengthItem());
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

            this.lengthsField.Add(new LengthItem(height));
            this.lengthsField.Add(new LengthItem(length));
            this.lengthsField.Add(new LengthItem(height - length));
            this.lengthsField.Add(new LengthItem(width - length));
            this.lengthsField.Add(new LengthItem(length));
            this.lengthsField.Add(new LengthItem(width));
        }
    }
}