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
            Initialize();
        }
        public RectItem(string model)
            : base(model)
        {
            Initialize();
        }
        public RectItem(Stock stock)
            : base(stock)
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
        public RectItem(string model, Stock stock, double width, double height)
            : base(model, stock, width, height)
        {
            Initialize();

            this.lengthsField[0].Length = width;
            this.lengthsField[1].Length = height;
            this.lengthsField[2].Length = width;
            this.lengthsField[3].Length = height;
        }
        private void Initialize()
        {
            base.numberOfSides = 4;
            for (int i = 0; i < numberOfSides; i++)
                this.lengthsField.Add(new LengthItem());
        }
    }
}