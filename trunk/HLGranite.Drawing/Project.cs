using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows;
using System.IO;
using System.Xml;

namespace HLGranite.Drawing
{
    public partial class Project
    {
        private string fileName;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Project()
        {
            this.fileName = string.Empty;

            this.guidField = Guid.NewGuid();
            this.createdAtField = DateTime.Now;
            //todo: this.CreatedBy = current login user
            this.progressField = 0m;
            this.stageField = ProjectStage.Draft;
            this.workOrdersField = new List<WorkOrder>();
        }

        #region Methods
        /// <summary>
        /// Save into database together with the canvas drawing.
        /// </summary>
        /// <param name="sender">Canvas, Panel or UIElement object.</param>
        public void Save(object sender)
        {
            this.fileName = this.guidField.ToString();
            WriteToXaml(sender);
            SaveToFile();
        }
        public void Save()
        {
            this.fileName = this.guidField.ToString();
            //WriteToXaml(sender);
            SaveToFile();
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
                StreamWriter writer = File.AppendText(this.fileName + ".xaml");
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
            StreamReader reader = new StreamReader(this.guidField.ToString() + ".xaml", Encoding.UTF8);
            try
            {
                return XamlReader.Load(reader.BaseStream) as DependencyObject;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }


        private static System.Xml.Serialization.XmlSerializer serializer;
        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(Project));
                }
                return serializer;
            }
        }
        /// <summary>
        /// Serializes current project object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }
        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }
        /// <summary>
        /// Deserializes workflow markup into an Project object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output Project object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out Project obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Project);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }
        public static bool Deserialize(string xml, out Project obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }
        public static Project Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((Project)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }
        private void SaveToFile()
        {
            SaveToFile(fileName, Encoding.UTF8);
        }
        private void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);

                string path = "Data";
                if (this.GetType() == typeof(Project)) //todo: test save projects scenario
                    path += Path.DirectorySeparatorChar + "Projects";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += Path.DirectorySeparatorChar + fileName + ".xml";

                streamWriter = new System.IO.StreamWriter(path, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }
        private Project LoadFromFile()
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }
        private Project LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion
    }
}