using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class Employee : User
    {
        public Employee()
            : base()
        {
            this.typeField = UserRole.Employee;
        }
    }
}