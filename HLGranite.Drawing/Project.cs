using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    public partial class Project
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Project()
        {
            this.createdAtField = DateTime.Now;
            //todo: this.CreatedBy = current login user
            this.progressField = 0m;
            this.stageField = ProjectStage.Draft;
            this.workOrdersField = new List<WorkOrder>();
        }
    }
}