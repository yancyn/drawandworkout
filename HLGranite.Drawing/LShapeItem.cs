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
            //new RectItem("RectItem00", stock, width, length);
            (this.elementsField[0] as RectItem).Material = stock;
            (this.elementsField[0] as RectItem).Width = width;
            (this.elementsField[0] as RectItem).Height = length;

            //this.elementsField[1] is VerticalLine
            //this.elementsField[2] = new RectItem("RectItem00", stock, length, height);
            (this.elementsField[2] as RectItem).Material = stock;
            (this.elementsField[2] as RectItem).Width = length;
            (this.elementsField[2] as RectItem).Height = height - length;

            this.lengthsField[0].Length = width;
            this.lengthsField[1].Length = length;
            this.lengthsField[2].Length = width - length;
            this.lengthsField[3].Length = height - length;
            this.lengthsField[4].Length = length;
            this.lengthsField[5].Length = height;
        }
        private void Initialize()
        {
            base.numberOfSides = 6;
            for (int i = 0; i < numberOfSides; i++)
                this.lengthsField.Add(new LengthItem());

            //for (int i = 0; i < 2; i++)
            base.AddElement();
            //base.AddElement(new RectItem("RectItem00", this.materialField, 0, 0));
        }
    }
}