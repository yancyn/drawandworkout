using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thought.vCards;

namespace HLGranite.Drawing
{
    public partial class Employee : User
    {
        public Employee()
            : base()
        {
            this.typeField = UserRole.Employee;
        }
        public Employee(vCard card)
            : base(card)
        {
            this.typeField = UserRole.Employee;
        }
        public Employee(User user)
            : base(user as vCard)
        {
            this.typeField = UserRole.Employee;
        }
    }
}