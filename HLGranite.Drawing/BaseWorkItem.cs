using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class BaseWorkItem
    {
        public BaseWorkItem()
            : base()
        {
            Initialize();
        }
        public BaseWorkItem(string model)
            : base(model)
        {
            Initialize();
        }
        public BaseWorkItem(double width, double height)
            : base()
        {
            Initialize();
            this.uomField = Unit.British;
            this.widthField = width;
            this.heightField = height;
        }
        private void Initialize()
        {
            this.dateField = DateTime.Now;
            this.guidField = Guid.NewGuid();
            this.heightField = 0;
            this.notesField = string.Empty;
            this.tagsField = new ObservableCollection<object>();
            this.uomField = Unit.British;
            this.widthField = 0;
        }
    }
}