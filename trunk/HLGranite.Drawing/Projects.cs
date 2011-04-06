using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HLGranite.Drawing
{
    public partial class Projects
    {
        public Projects()
        {
            this.projectField = new System.Collections.ObjectModel.ObservableCollection<Project>();

            //initialize collection
            string directory = "Data" + Path.DirectorySeparatorChar + "Projects";
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                Guid guid = new Guid(Path.GetFileNameWithoutExtension(file.FullName));
                Project project = new Project(guid);
                this.projectField.Add(project);
            }
        }
    }
}