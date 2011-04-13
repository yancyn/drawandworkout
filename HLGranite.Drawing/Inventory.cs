using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace HLGranite.Drawing
{
    public partial class Inventory
    {
        private string fileName;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Inventory()
            : base()
        {
            Initialize();
        }
        /// <summary>
        /// Cloning constructor.
        /// </summary>
        /// <param name="item"></param>
        public Inventory(Inventory item)
            : base()
        {
            Initialize();
            Copy(item);
        }
        /// <summary>
        /// Constructor for retrieve.
        /// </summary>
        /// <param name="guid"></param>
        public Inventory(Guid guid)
            : base()
        {
            Initialize();
            this.guidField = guid;
            this.fileName = this.guidField.ToString();
            Copy(LoadFromFile());
        }
        private void Initialize()
        {
            this.fileName = string.Empty;
            this.purchaseAtField = base.dateField;
            this.serialField = string.Empty;
            this.collectionField = new System.Collections.ObjectModel.ObservableCollection<InventoryWIP>();
        }
        protected void Copy(Inventory source)
        {
            this.heightField = source.Height;
            this.purchaseAtField = source.PurchaseAt;
            this.serialField = source.Serial;
            this.stockField = source.Stock;
            this.warehouseField = source.Warehouse;
            this.widthField = source.Width;
            foreach (InventoryWIP item in source.Collection)
                this.collectionField.Add(new InventoryWIP(item));
        }
        public void Save()
        {
            this.fileName = this.guidField.ToString() + ".xml";
            SaveToFile();
        }
        #region Serialize/Deserialize
        private System.Xml.Serialization.XmlSerializer serializer;
        private System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(Inventory));
                }
                return serializer;
            }
        }

        /// <summary>
        /// Serializes current Inventory object into an XML document
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
        /// Deserializes workflow markup into an Inventory object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output Inventory object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        protected bool Deserialize(string xml, out Inventory obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Inventory);
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
        protected bool Deserialize(string xml, out Inventory obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }
        public Inventory Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((Inventory)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes current Inventory object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }
        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }
        public virtual void SaveToFile()
        {
            SaveToFile(fileName, Encoding.UTF8);
        }
        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);

                string path = "Data";
                if (this.GetType() == typeof(Project))
                    path += Path.DirectorySeparatorChar + "Projects";
                else if (this.GetType() == typeof(Inventory))
                    path += Path.DirectorySeparatorChar + "Inventory";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += Path.DirectorySeparatorChar + fileName;

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

        /// <summary>
        /// Deserializes xml markup from file into an Inventory object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output Inventory object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        protected bool LoadFromFile(string fileName, System.Text.Encoding encoding, out Inventory obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Inventory);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }
        protected bool LoadFromFile(string fileName, out Inventory obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }
        protected bool LoadFromFile(string fileName, out Inventory obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }
        public Inventory LoadFromFile()
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }
        protected Inventory LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                string path = "Data";
                if (this.GetType() == typeof(Project))
                    path += Path.DirectorySeparatorChar + "Projects";
                else if (this.GetType() == typeof(Inventory))
                    path += Path.DirectorySeparatorChar + "Inventory";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += Path.DirectorySeparatorChar + fileName + ".xml";
                if (!File.Exists(path)) return null;

                file = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read);
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