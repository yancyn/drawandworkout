﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class Bullnoses
    {
        public Bullnoses()
        {
            base.fileName = "Bullnoses.xml";
            this.bullnoseField = new System.Collections.ObjectModel.ObservableCollection<Bullnose>();
        }
    }
}