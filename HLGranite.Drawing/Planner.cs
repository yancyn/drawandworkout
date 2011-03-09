using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class Planner : User
    {
        public Planner()
            : base()
        {
            this.typeField = UserRole.Planner;
        }
    }
}