using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace HLGranite.Drawing
{
    public partial class BaseElement
    {
        public BaseElement()
        {
            this.codeField = string.Empty;
            this.name1Field = string.Empty;
            this.name2Field = string.Empty;
            this.notesField = string.Empty;
            this.tagField = new ObservableCollection<object>();
        }
        /// <summary>
        /// Compose a display ToString value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = string.Empty;
            if (this.codeField.Length > 0)
                output += this.codeField;
            if (this.name1Field.Length > 0)
            {
                if (this.codeField.Length > 0)
                    output += " - ";
                output += this.name1Field;
            }
            if (this.name2Field.Length > 0)
            {
                if (this.name1Field.Length > 0)
                    output += " ";
                output += this.name2Field;
            }

            return output;//return base.ToString();
        }
    }
}