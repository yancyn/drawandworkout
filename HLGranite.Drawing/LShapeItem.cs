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
            Initialize();
        }
        public LShapeItem(string model)
            : base(model)
        {
            Initialize();
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
            Initialize();
            double length = 24;//a normal default kitchen top length
            this.elementsField[0] = new RectItem("RectItem00", stock, width, length);
            this.elementsField[1] = new RectItem("RectItem00", stock, length, height);

            this.lengthsField[0] = new LengthItem(height);
            this.lengthsField[1] = new LengthItem(length);
            this.lengthsField[2] = new LengthItem(height - length);
            this.lengthsField[3] = new LengthItem(width - length);
            this.lengthsField[4] = new LengthItem(length);
            this.lengthsField[5] = new LengthItem(width);
        }
        private void Initialize()
        {
            base.numberOfSides = 6;
            for (int i = 0; i < 2; i++)
                this.elementsField.Add(new RectItem("RectItem00", null, 0, 0));

            for (int i = 0; i < numberOfSides; i++)
                this.lengthsField.Add(new LengthItem());
        }
    }
}