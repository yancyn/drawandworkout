using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows;
using System.IO;

namespace HLGranite.Drawing
{
    public partial class Project
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Project()
        {
            this.guidField = Guid.NewGuid();
            this.createdAtField = DateTime.Now;
            //todo: this.CreatedBy = current login user
            this.progressField = 0m;
            this.stageField = ProjectStage.Draft;
            this.workOrdersField = new List<WorkOrder>();
        }

        #region Methods
        public void Save(object sender)
        {
            //save database
            WriteToXaml(sender);
        }
        /// <summary>
        /// Write to xaml file.
        /// </summary>
        /// <param name="sender">Canvas, Panel or Grid object.</param>
        /// <see>http://msdn.microsoft.com/en-us/library/system.windows.markup.xamlwriter.aspx</see>
        private void WriteToXaml(object sender)
        {
            try
            {
                string xaml = XamlWriter.Save(sender);
                //System.Diagnostics.Debug.WriteLine(xaml);
                StreamWriter writer = File.AppendText(this.guidField + ".xaml");
                writer.WriteLine(xaml);
                writer.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }
        /// <summary>
        /// Load xaml file into current page.
        /// </summary>
        /// <example>
        /// <code>
        /// Project project = new Project("500368f8-3a38-449d-b645-c702332fb941");
        /// DependencyObject drawing = project.GetDrawing();
        /// this.AddChild(drawing);
        /// </code>
        /// </example>
        /// <see>http://blogs.msdn.com/b/ashish/archive/2007/08/14/dynamically-loading-xaml.aspx</see>
        public DependencyObject GetDrawing()
        {
            try
            {
                StreamReader reader = new StreamReader(this.guidField.ToString() + ".xaml", Encoding.UTF8);
                return XamlReader.Load(reader.BaseStream) as DependencyObject;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }
        #endregion
    }
}